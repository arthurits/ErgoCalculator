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

namespace ErgoCalc
{
    public partial class frmResultsLiberty : Form, IChildResults
    {
        private object _data;
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


        public frmResultsLiberty(object data)
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

            //_modelTC = new CThermalModels(_data);
            // Call the DLL function
            try
            {
                //_modelTC.ThermalComfort();
                //_data = _modelTC.GetData;
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
                //rtbShowResult.Text = _modelTC.ToString();
                CreatePlots();
                FormatText();
            }
        }

        private void CreatePlots()
        {
            throw new NotImplementedException();
        }

        private void SerializeToJSON(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            writer.WriteString("Document type", "LM-MMH model");

            writer.Flush();
        }

        #endregion Private routines

        #region IChildResults inferface
        public void Save(string path)
        {
            throw new NotImplementedException();
        }

        public bool OpenFile(JsonDocument document)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void EditData()
        {
            throw new NotImplementedException();
        }

        public void Duplicate()
        {
            throw new NotImplementedException();
        }
        #endregion IChildResults inferface
    }
}
