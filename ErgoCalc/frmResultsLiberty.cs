using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

using ErgoCalc.Models.LibertyMutual;

namespace ErgoCalc
{
    public partial class frmResultsLiberty : Form, IChildResults
    {
        private List<ModelLiberty> _data;
        private CLibertyMutual _modelLM;
        private readonly string _strPath;

        public ToolStrip ChildToolStrip { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public frmResultsLiberty()
        {
            _strPath = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            if (File.Exists(_strPath + @"\images\logo.ico")) this.Icon = new Icon(_strPath + @"\images\logo.ico");

            InitializeComponent();
            propertyGrid1.SelectedObject = new ResultsOptions(rtbShowResult);
            splitContainer1.Panel1Collapsed = false;
            splitContainer1.SplitterDistance = 0;
            splitContainer1.SplitterWidth = 1;
            splitContainer1.IsSplitterFixed = true;

            InitializePlot();

            // Initialize private variables
        }


        public frmResultsLiberty(List<ModelLiberty> data)
            : this()
        {
            _data = data;
        }

        private void frmResultsLiberty_Shown(object sender, EventArgs e)
        {
            ShowResults();
        }

        #region Private routines
        private void InitializePlot()
        {
            throw new NotImplementedException();
        }

        private void ShowResults()
        {
            Boolean error = false;

            _modelLM = new CLibertyMutual(_data);
            // Call the DLL function
            try
            {
                _modelLM.ComputeMMH();
                _data = _modelLM.GetData;
            }
            catch (EntryPointNotFoundException)
            {
                error = true;
                MessageBox.Show(
                    "The program calculation kernel's been tampered with.\nLM-MMH could not be computed.",
                    "LM-MMH error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (DllNotFoundException)
            {
                error = true;
                MessageBox.Show(
                    "DLL files are missing. Please\nreinstall the application.",
                    "LM-MMH error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                error = true;
                MessageBox.Show(
                    "Error in the LM-MMH calculation kernel:\n" + ex.ToString(),
                    "Unexpected error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            // Call the routine that shows the results
            if (error == false)
            {
                rtbShowResult.Text = _modelLM.ToString();
                FormatText();
                CreatePlots();
            }
        }

        private void CreatePlots()
        {
            return;
        }

        private void SerializeToJSON(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            writer.WriteString("Document type", "LM-MMH model");

            writer.WritePropertyName("Cases");
            writer.WriteStartArray();

            foreach (var data in _data)
            {
                writer.WriteStartObject();

                writer.WritePropertyName("Data");
                writer.WriteStartObject();
                writer.WriteNumber("Horizontal reach (m)", data.data.HorzReach);
                writer.WriteNumber("Vertical reach mean (m)", data.data.VRM);
                writer.WriteNumber("Vertical height (m)", data.data.VertHeight);
                writer.WriteNumber("Vertical distance (m)", data.data.DistVert);
                writer.WriteNumber("Horizontal distance (m)", data.data.DistHorz);
                writer.WriteNumber("Frequency (/m)", data.data.Freq);
                writer.WriteNumber("Type", (int)data.type);
                writer.WriteNumber("Gender", (int)data.gender);
                writer.WriteEndObject();

                writer.WritePropertyName("Results");
                writer.WriteStartObject();
                writer.WriteNumber("Coefficient of variation initial", data.results.CVInitial);
                writer.WriteNumber("Coefficient of variation sustained", data.results.CVSustained);
                writer.WriteNumber("Initial force (kg)", data.results.InitialF);
                writer.WriteNumber("Sustained force (kg)", data.results.SustainedF);
                writer.WriteNumber("Weight (kg)", data.results.Weight);
                writer.WriteEndObject();

                writer.WriteEndObject();
            }

            writer.WriteEndArray();
            writer.WriteEndObject();

            writer.Flush();
        }

        #endregion Private routines

        #region IChildResults inferface
        public void Save(string path)
        {
            // Displays a SaveFileDialog so the user can save the Image  
            SaveFileDialog SaveDlg = new SaveFileDialog
            {
                DefaultExt = "*.csv",
                Filter = "ERGO file (*.ergo)|*.ergo|RTF file (*.rtf)|*.rtf|Text file (*.txt)|*.txt|All files (*.*)|*.*",
                FilterIndex = 1,
                Title = "Save LM-MMH data",
                OverwritePrompt = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            DialogResult result;
            using (new CenterWinDialog(this))
            {
                result = SaveDlg.ShowDialog(this.Parent);
            }

            // If the file name is not an empty string open it for saving.  
            if (result == DialogResult.OK && SaveDlg.FileName != "")
            {
                using var fs = SaveDlg.OpenFile();

                switch (SaveDlg.FilterIndex)
                {
                    case 1:
                        if (fs != null)
                        {
                            using var writer = new Utf8JsonWriter(fs, options: new JsonWriterOptions { Indented = true });
                            SerializeToJSON(writer);
                            //var jsonString = JsonSerializer.Serialize(_datos[0]._points[0], new JsonSerializerOptions { WriteIndented = true });
                        }
                        break;
                    case 2:
                        rtbShowResult.SaveFile(fs, RichTextBoxStreamType.RichText);
                        break;
                    case 3:
                        rtbShowResult.SaveFile(fs, RichTextBoxStreamType.PlainText);
                        break;
                    case 4:
                        rtbShowResult.SaveFile(fs, RichTextBoxStreamType.UnicodePlainText);
                        break;
                }

                using (new CenterWinDialog(this.MdiParent))
                {
                    MessageBox.Show(this, "The file was successfully saved", "File saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        public bool OpenFile(JsonDocument document)
        {
            bool result = true;
            ModelLiberty data = new ModelLiberty();
            _data = new List<ModelLiberty>();
            JsonElement root = document.RootElement;

            try
            {
                foreach (JsonElement curve in root.GetProperty("Cases").EnumerateArray())
                {
                    data.data.HorzReach = curve.GetProperty("Data").GetProperty("Horizontal reach (m)").GetDouble();
                    data.data.VRM = curve.GetProperty("Data").GetProperty("Vertical reach mean (m)").GetDouble();
                    data.data.VertHeight = curve.GetProperty("Data").GetProperty("Vertical height (m)").GetDouble();
                    data.data.DistVert = curve.GetProperty("Data").GetProperty("Vertical distance (m)").GetDouble();
                    data.data.DistHorz = curve.GetProperty("Data").GetProperty("Horizontal distance (m)").GetDouble();
                    data.data.Freq = curve.GetProperty("Data").GetProperty("Frequency (/m)").GetDouble();
                    data.type = (MNType)curve.GetProperty("Data").GetProperty("Type").GetByte();
                    data.gender = (MNGender)curve.GetProperty("Data").GetProperty("Gender").GetByte();

                    data.results.CVInitial = curve.GetProperty("Results").GetProperty("Coefficient of variation initial").GetDouble();
                    data.results.CVSustained = curve.GetProperty("Results").GetProperty("Coefficient of variation sustained").GetDouble();
                    data.results.InitialF = curve.GetProperty("Results").GetProperty("Initial force (kg)").GetDouble();
                    data.results.SustainedF = curve.GetProperty("Results").GetProperty("Sustained force (kg)").GetDouble();
                    data.results.Weight = curve.GetProperty("Results").GetProperty("Weight (kg)").GetDouble();

                    _data.Add(data);
                }
            }
            catch (Exception)
            {
                result = false;
            }

            if (result)
            {
                //CalcularCurva();
                //PlotCurves();
                //_chartOptions.NúmeroCurva = chart.plt.GetPlottables().Count - 1;
            }

            return result;
        }

        public bool[] GetToolbarEnabledState()
        {
            return new bool[] { true, true, true, false, true, true, false, true, true, false, false, true, true, true }; ;
        }

        public void ShowHideSettings()
        {
            this.SuspendLayout();
            if (splitContainer1.SplitterDistance > 0)
            {
                Transitions.Transition.run(this.splitContainer1, "SplitterDistance", 0, new Transitions.TransitionType_Linear(200));
                splitContainer1.SplitterWidth = 1;
                splitContainer1.IsSplitterFixed = true;
            }
            else
            {
                Transitions.Transition.run(this.splitContainer1, "SplitterDistance", 200, new Transitions.TransitionType_Linear(200));
                splitContainer1.SplitterWidth = 4;
                splitContainer1.IsSplitterFixed = false;
            }
            this.ResumeLayout();
            return; ;
        }

        public bool PanelCollapsed()
        {
            return splitContainer1.SplitterDistance == 0 ? true : false; ;
        }

        public void FormatText()
        {
            
        }

        public void EditData()
        {
            using var frm = new frmDataLiberty(_data);

            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                _data = (List<ModelLiberty>)frm.GetData;
                ShowResults();
            }
        }

        public void Duplicate()
        {
            throw new NotImplementedException();
        }
        #endregion IChildResults inferface
    }
}
