using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ErgoCalc.DLL.MetRate;

namespace ErgoCalc
{
    public partial class frmMetResult : Form
    {
        // Variable definition
        private Int32[] _nOpciones;
        private Double[] _dResults;

        // Default constructor
        public frmMetResult()
        {
            // VS initializer
            InitializeComponent();

            // Inicializar las variables miembro
            _dResults = new Double[5];
        }

        // Overloaded constructor
        public frmMetResult(Int32[] nDatos)
            : this() // Call the base constructor
        {
            _nOpciones=nDatos;
        }

        private void frmMetResult_Load(object sender, EventArgs e)
        {
            // Definir las variables
            cMetRate cMet = new cMetRate();

            // Realizar los cálculos
            unsafe
            {
                fixed (Int32* _pOpciones = _nOpciones)
                {
                    fixed (Double* _pResults = _dResults) cMet.CalculateMetRate(_pOpciones, _pResults);
                }
            }

            // Presentar los resultados
            ShowResults();
        }


        #region Private routines

        private void ShowResults()
        {
            // Escribir los resultados en la ventana
            rtbShowResult.Text = "These are the metabolic rate results according to the norm UNE-EN ISO 8996:2004 criteria.";
            rtbShowResult.AppendText("\n\n");

            // Nivel 1A
            rtbShowResult.AppendText("Level 1.a computation for a" + "\n");
            rtbShowResult.AppendText("Metabolic rate range: " + _dResults[0].ToString("0") + " to " + _dResults[1].ToString("0"));
            rtbShowResult.AppendText("\n\n");

            // Nivel 1B
            rtbShowResult.AppendText("Level 1.b computation for a" + "\n");
            rtbShowResult.AppendText("Mean metabolic rate: " + _dResults[2].ToString("0") + "\n");
            rtbShowResult.AppendText("Metabolic rate range: " + _dResults[3].ToString("0") + " to " + _dResults[4].ToString("0")+ "\n");
            rtbShowResult.AppendText("\n\n");

            // Nivel 2A
        }
        #endregion





    }
}
