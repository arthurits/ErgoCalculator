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

using ErgoCalc.Models.ThermalComfort;

namespace ErgoCalc
{
    public partial class frmResultsTC : Form, IChildResults
    {
        private List<ModelTC> _data;
        private CThermalModels _modelTC;
        private string _strPath;

        public frmResultsTC()
        {
            InitializeComponent();

            _strPath = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            if (File.Exists(_strPath + @"\images\logo.ico")) this.Icon = new Icon(_strPath + @"\images\logo.ico");

            // Initialize private variables
            _modelTC = new CThermalModels();
        }

        public frmResultsTC(object data)
            :this()
        {
            _data = (List<ModelTC>)data;
            _modelTC = new CThermalModels(_data);
        }

        private void frmResultsTC_Shown(object sender, EventArgs e)
        {
            Boolean error = false;

            // Call the DLL function
            try
            {
                //_classDLL.StrainIndex(_classDLL.Parameters, orden, ref nSize);
                //_classDLL.RSI(_subtasks, orden, ref nSize);
                _modelTC.ThermalComfort();
                _data = _modelTC.GetData;
            }
            catch (EntryPointNotFoundException)
            {
                error = true;
                MessageBox.Show(
                    "The program calculation kernel's been tampered with.\nThe RSI could not be computed.",
                    "RSI index error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (DllNotFoundException)
            {
                error = true;
                MessageBox.Show(
                    "DLL files are missing. Please\nreinstall the application.",
                    "RSI index error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                error = true;
                MessageBox.Show(
                    "Error in the calculation kernel:\n" + ex.ToString(),
                    "Unexpected error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            // Call the routine that shows the results
            if (error == false)
            {
                rtbShowResult.Text = _modelTC.ToString();
                //CreatePlots();
                FormatText();
            }
        }

        private void SerializeToJSON(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            writer.WriteString("Document type", "Thermal comfort model");
            
            writer.WritePropertyName("Cases");
            writer.WriteStartArray();

            foreach (var data in _data)
            {
                writer.WriteStartObject();

                writer.WritePropertyName("Data");
                writer.WriteStartObject();
                writer.WriteNumber("Air temperature", data.data.TempAir);
                writer.WriteNumber("Radiant temperature", data.data.TempRad);
                writer.WriteNumber("Air velocity", data.data.Velocity);
                writer.WriteNumber("Relative humidity", data.data.RelHumidity);
                writer.WriteNumber("Clothing", data.data.Clothing);
                writer.WriteNumber("Metabolic rate", data.data.MetRate);
                writer.WriteNumber("External work", data.data.ExternalWork);
                writer.WriteEndObject();

                writer.WritePropertyName("Results");
                writer.WriteStartObject();
                writer.WriteNumber("PMV", data.factors.PMV);
                writer.WriteNumber("PPD", data.factors.PPD);
                writer.WriteNumber("Heat loss - Skin", data.factors.HL_Skin);
                writer.WriteNumber("Heat loss - Sweating", data.factors.HL_Sweating);
                writer.WriteNumber("Heat loss - Latent", data.factors.HL_Latent);
                writer.WriteNumber("Heat loss - Dry", data.factors.HL_Dry);
                writer.WriteNumber("Heat loss - Radiation", data.factors.HL_Radiation);
                writer.WriteNumber("Heat loss - Convection", data.factors.HL_Convection);
                writer.WriteEndObject();

                writer.WriteEndObject();
            }

            writer.WriteEndArray();
            writer.WriteEndObject();

            writer.Flush();
        }

        #region IChildResults interface
        public ToolStrip ChildToolStrip { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Duplicate()
        {
            throw new NotImplementedException();
        }

        public void EditData()
        {
            throw new NotImplementedException();
        }

        public void FormatText()
        {
            throw new NotImplementedException();
        }

        public bool[] GetToolbarEnabledState()
        {
            throw new NotImplementedException();
        }

        public bool OpenFile(JsonDocument document)
        {
            bool result = true;
            int Length;
            ModelTC data = new ModelTC();
            _data = new List<ModelTC>();
            JsonElement root = document.RootElement;

            try
            {
                foreach (JsonElement curve in root.GetProperty("Cases").EnumerateArray())
                {
                    data.data.TempAir = curve.GetProperty("Data").GetProperty("Air temperature").GetDouble();
                    data.data.TempRad = curve.GetProperty("Data").GetProperty("Radiant temperature").GetDouble();
                    data.data.Velocity = curve.GetProperty("Data").GetProperty("Air velocity").GetDouble();
                    data.data.RelHumidity = curve.GetProperty("Data").GetProperty("Relative humidity").GetDouble();
                    data.data.Clothing = curve.GetProperty("Data").GetProperty("Clothing").GetDouble();
                    data.data.MetRate = curve.GetProperty("Data").GetProperty("Metabolic rate").GetDouble();
                    data.data.ExternalWork = curve.GetProperty("Data").GetProperty("External work").GetDouble();

                    data.factors.PMV = curve.GetProperty("Results").GetProperty("PMV").GetDouble();
                    data.factors.PPD = curve.GetProperty("Results").GetProperty("PPD").GetDouble();
                    data.factors.HL_Skin = curve.GetProperty("Results").GetProperty("Heat loss - Skin").GetDouble();
                    data.factors.HL_Sweating = curve.GetProperty("Results").GetProperty("Heat loss - Sweating").GetDouble();
                    data.factors.HL_Latent = curve.GetProperty("Results").GetProperty("Heat loss - Latent").GetDouble();
                    data.factors.HL_Dry = curve.GetProperty("Results").GetProperty("Heat loss - Dry").GetDouble();
                    data.factors.HL_Radiation = curve.GetProperty("Results").GetProperty("Heat loss - Radiation").GetDouble();
                    data.factors.HL_Convection = curve.GetProperty("Results").GetProperty("Heat loss - Convection").GetDouble();

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

        public bool PanelCollapsed()
        {
            throw new NotImplementedException();
        }

        public void Save(string path)
        {
            // Displays a SaveFileDialog so the user can save the Image  
            SaveFileDialog SaveDlg = new SaveFileDialog
            {
                DefaultExt = "*.csv",
                Filter = "ERGO file (*.ergo)|*.ergo|CSV file (*.csv)|*.csv|Text file (*.txt)|*.txt|All files (*.*)|*.*",
                FilterIndex = 1,
                Title = "Save scatter-plot data",
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
                switch (SaveDlg.FilterIndex)
                {
                    case 1:
                        var fs = SaveDlg.OpenFile();
                        if (fs != null)
                        {
                            using var writer = new Utf8JsonWriter(fs, options: new JsonWriterOptions { Indented = true });
                            SerializeToJSON(writer);
                            //var jsonString = JsonSerializer.Serialize(_datos[0]._points[0], new JsonSerializerOptions { WriteIndented = true });
                        }
                        break;
                    case 2:
                        
                        break;
                }
                using (new CenterWinDialog(this.MdiParent))
                {
                    MessageBox.Show(this, "The file was successfully saved", "File saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            return;
        }

        public void ShowHideSettings()
        {
            throw new NotImplementedException();
        }

        #endregion IChildResults interface

    }
}
