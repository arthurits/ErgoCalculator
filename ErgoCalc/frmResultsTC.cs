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
            throw new NotImplementedException();
        }

        public bool PanelCollapsed()
        {
            throw new NotImplementedException();
        }

        public void Save(string path)
        {
            throw new NotImplementedException();
        }

        public void ShowHideSettings()
        {
            throw new NotImplementedException();
        }

        #endregion IChildResults interface

    }
}
