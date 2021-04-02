using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ErgoCalc.Models.WRmodel;

namespace ErgoCalc
{
    public partial class frmDataWRmodel : Form, IChildData
    {
        // Propiedades de la clase
        public List<datosWR> _data;
        private System.ComponentModel.ComponentResourceManager _resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDataWRmodel));

        public object GetData => _data;

        public frmDataWRmodel()
        {
            InitializeComponent();

            AddColumn();

            // Create the header rows
            gridVariables.RowCount = 7;
            gridVariables.Rows[0].HeaderCell.Value = "Name";
            gridVariables.Rows[1].HeaderCell.Value = "Maximum voluntary contraction (%)";
            gridVariables.Rows[2].HeaderCell.Value = "Maximum holding time (min)";
            gridVariables.Rows[3].HeaderCell.Value = "Working times (min)";
            gridVariables.Rows[4].HeaderCell.Value = "Rest times (min)";
            gridVariables.Rows[5].HeaderCell.Value = "Number of cycles";
            gridVariables.Rows[6].HeaderCell.Value = "Numeric step";

            gridVariables[0, 6].Value = 0.1;

            var cell = new DataGridViewCellStyle();
            cell.BackColor = Color.White;
            cell.Alignment = DataGridViewContentAlignment.MiddleCenter;
            cell.SelectionBackColor = Color.White;
            cell.SelectionForeColor = Color.Gray;
            cell.ForeColor = Color.Gray;
            gridVariables.Rows[2].DefaultCellStyle = cell;
            gridVariables.Rows[2].ReadOnly = true;

            // Initialize private variables
            _data = new List<datosWR>();

            // Rellenar el valor por defecto de los cuadros de texto
            txtMVC.Text = "15";
            txtTT.Text = "4 3 2";
            txtTD.Text = "4 3 2";
            txtCiclos.Text = "2";
            txtPaso.Text = "0,01";
        }

        public frmDataWRmodel (List<datosWR> data)
            :this()
        {
            DataToGrid(data);

            updTasks.Value = data.Count;

            // Definición de variables
            //String strTexto = "";
            //String strEspacio = " ";

            // Pasar los datos recibidos a los controles del formulario
            txtMVC.Text = data[0]._dMVC.ToString();
            lblTDmax.Text = data[0]._dMHT.ToString("##.00", System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));
            txtTT.Text = string.Join (" ", data[0]._dWorkRest[0]);
            txtTD.Text = string.Join (" ", data[0]._dWorkRest[1]);
            txtCiclos.Text = data[0]._bCiclos.ToString();
            txtPaso.Text = data[0]._dPaso.ToString();

            //foreach (double d in data._dWorkRest[0])
            //    strTexto += d.ToString() + strEspacio;
            //txtTT.Text = strTexto.TrimEnd(strEspacio.ToCharArray());

            //strTexto = "";
            //foreach (double d in data._dWorkRest[1])
            //    strTexto += d.ToString() + strEspacio;
            //txtTD.Text = strTexto.TrimEnd(strEspacio.ToCharArray());

            //_data._dPaso = data._dPaso;

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // The form does not return unless all fields are validated
            this.DialogResult = DialogResult.None;

            datosWR item = new datosWR();
            for (int i = 0; i < gridVariables.ColumnCount; i++)
            {
                item._strLegend = gridVariables[i, 0].Value?.ToString();
                // Validation routines
                if (!Validation.IsValidRange(gridVariables[i, 1].Value, 0, 100, true)) { gridVariables.CurrentCell = gridVariables[i, 1]; gridVariables.BeginEdit(true); return; }
                item._dMVC = Validation.ValidateNumber(gridVariables[i, 1].Value);
                item._dMHT = 5710.0 / Math.Pow(item._dMVC, 2.14);
                gridVariables[i, 2].Value = item._dMHT;

                item._dWorkRest = new double[2][];
                var arr = gridVariables[i, 3].Value.ToString().Split(' ');
                foreach( var number in arr)
                {
                    if (!Validation.IsValidRange(number, 0, item._dMHT, true)) { gridVariables.CurrentCell = gridVariables[i, 3]; gridVariables.BeginEdit(true); return; }
                }
                item._dWorkRest[0] = arr.Select(double.Parse).ToArray();

                arr = gridVariables[i, 4].Value.ToString().Split(' ');
                foreach (var number in arr)
                {
                    if (!Validation.IsValidRange(number, 0, item._dMHT, true)) { gridVariables.CurrentCell = gridVariables[i, 4]; gridVariables.BeginEdit(true); return; }
                }
                item._dWorkRest[1] = arr.Select(double.Parse).ToArray();

                //if (ValidarTT(item) != true) { return; }
                if (!Validation.IsValidRange(gridVariables[i, 5].Value, 0, 10, true)) { gridVariables.CurrentCell = gridVariables[i, 5]; gridVariables.BeginEdit(true); return; }
                if (!Validation.IsValidRange(gridVariables[i, 6].Value, 0.001, 0.1, true)) { gridVariables.CurrentCell = gridVariables[i, 6]; gridVariables.BeginEdit(true); return; }

                //item._dWorkRest[0];
                item._bCiclos = (byte)Validation.ValidateNumber(gridVariables[i, 5].Value);
                item._dPaso = Validation.ValidateNumber(gridVariables[i, 6].Value);

                item._nPuntos = ComputeNumberOfPoints(item);
                item._dPoints = new double[2][];
                item._dPoints[0] = new double[item._nPuntos];
                item._dPoints[1] = new double[item._nPuntos];

                //if (ValidarMVC() != true) { txtMVC.Focus(); return; }
                //if (ValidarTT() != true) { return; }
                //if (ValidarC() != true) { txtCiclos.Focus(); return; }

                // Save the column data once it's been approved
                _data.Add(item);

            }
            // Return OK and close the dialog
            this.DialogResult = DialogResult.OK;
        }

        #region Private routines
        /// <summary>
        /// Adds a column to the DataGrid View and formates it
        /// </summary>
        /// <param name="col">Column number (zero based)</param>
        private void AddColumn(Int32 col)
        {
            // By default, the DataGrid always contains a single column
            //if (col == 0) return;
            if (gridVariables.Columns.Contains("Column" + (col).ToString())) return;

            string strName = "Case ";
            //if (_index != IndexType.RSI) strName = "SubTask ";

            // Create the new column
            gridVariables.Columns.Add("Column" + (col).ToString(), strName + ((char)('A' + col)).ToString());
            gridVariables.Columns[col].SortMode = DataGridViewColumnSortMode.NotSortable;
            gridVariables.Columns[col].Width = 70;

            if (col > 0) gridVariables[col, 5].Value = 0.1;

            return;
        }

        /// <summary>
        /// Adds a column to the DataGrid View and formates it
        /// </summary>
        private void AddColumn()
        {
            AddColumn(gridVariables.Columns.Count);
        }

        /// <summary>
        /// Places the data into the grid control, creating a new column for each case
        /// </summary>
        /// <param name="data">List containing the data</param>
        private void DataToGrid(List<datosWR> data)
        {
            int nDataNumber = data.Count;

            for (var j = 0; j < nDataNumber; j++)
            {
                //Column 0 is already created in the constructor;
                if (j > 0) AddColumn();

                // Populate the DataGridView with data
                gridVariables[j, 0].Value = data[j]._strLegend;
                gridVariables[j, 1].Value = (int)data[j]._dMVC;
                gridVariables[j, 2].Value = data[j]._dMHT.ToString("0.##");
                gridVariables[j, 3].Value = string.Join(" ", data[j]._dWorkRest[0]);
                gridVariables[j, 4].Value = string.Join(" ", data[j]._dWorkRest[1]);
                gridVariables[j, 5].Value = data[j]._bCiclos;
                gridVariables[j, 6].Value = data[j]._dPaso;
            }
        }

        #endregion Private routines

        #region Eventos de los TextBox

        private void txtMVC_Validating(object sender, CancelEventArgs e)
        {
            if (txtMVC.Text.Length > 0)
            {
                /*if (ValidarMVC() == false)
                    this.txtMVC.Focus();
                else*/
                    //this.lblTDmax.Text = _data._dMHT.ToString("##.00", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES"));
            }
        }

        private void txtMVC_Enter(object sender, EventArgs e)
        {
            txtMVC.SelectAll();
        }

        private void txtTT_Enter(object sender, EventArgs e)
        {
            txtTT.SelectAll();
        }

        private void txtTD_Enter(object sender, EventArgs e)
        {
            txtTD.SelectAll();
        }

        private void txtCiclos_Enter(object sender, EventArgs e)
        {
            txtCiclos.SelectAll();
        }

        private void txtPaso_Enter(object sender, EventArgs e)
        {
            txtPaso.SelectAll();
        }

        #endregion

        #region Rutinas de validación

        /// <summary>
        /// Validar la entrada del usuario del dato MVC
        /// </summary>
        /// <returns>"True" si no ha habido problemas y "False" si los ha habido</returns>
        private Boolean ValidarMVC(datosWR data)
        {
            //Models.WRmodel.cWRmodel modelo = new Models.WRmodel.cWRmodel();
            try
            {
                // Comprobar que se ha introducido texto en el campo
                if (txtMVC.Text.Length == 0)
                    throw new FieldLength(_resources.GetString("errorValidarMVCFieldLength"));

                // Pasar el dato a doble
                data._dMVC = Convert.ToDouble(txtMVC.Text);

                // Comprobar que el valor está comprendido entre 0 y 100
                if (data._dMVC <= 0 | data._dMVC >= 100)
                {
                    //throw new InvalidRange("El valor de MVC debe estar\n comprendido entre 0 y 100");
                    throw new InvalidRange(_resources.GetString("errorValidarMVCInvalidRange"));
                }

                // Una vez comprobado que el dato es correcto, se calcula el MHT
                //_datos._dMHT = modelo.Sjogaard(_datos._dMVC);
                data._dMHT = 5710.0 / Math.Pow(data._dMVC, 2.14);

                // Finalizar
                return true;
            }
            catch (Exception e)
            {
                // Lo normal es que sólo se den dos tipos de errores:
                // InvalidRange, FieldLength y FormatException
                MessageBox.Show(e.Message, _resources.GetString("errorValidarMVCTítulo"), MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Indicar que se ha producido un error
                return false;
            }
        }

        /// <summary>
        /// Validar la entrada del usuario de los datos de tiempo de trabajo
        /// </summary>
        /// <returns>0 si no ha habido problemas y 1 si los ha habido</returns>
        private Boolean ValidarTT(datosWR data)
        {
            try
            {
                String cadenaT = txtTT.Text.Trim();
                String cadenaD = txtTD.Text.Trim();
                String subcadena;
                String strError;
                Double número = 0;
                Byte bPosición = 0;
                Byte bPosiciónAnt = 0;
                Byte bEspacio = 0;

                // Comprobar que la longitud de las cadenas no es superior a 255
                if (cadenaT.Length > 255 || cadenaD.Length > 255)
                {
                    //strError = String.Format("Sólo se admite un máximo \n de 255 caracteres");
                    throw new FieldLength(_resources.GetString("errorValidarTTFieldLength"));
                }

                // Contar el número de espacios en blanco en ambas cadenas
                foreach (char c in cadenaT)
                {
                    if (char.IsWhiteSpace(c)) { ++bPosición; }
                }

                foreach (char c in cadenaD)
                {
                    if (char.IsWhiteSpace(c)) { ++bPosiciónAnt; }
                }

                if (bPosición != bPosiciónAnt)
                {
                    //strError = String.Format("El número de ciclos de trabajo ({0:0}) debe ser\nigual al número de ciclos de descanso ({1:0})", bPosición+1, bPosiciónAnt+1);
                    strError = String.Format(_resources.GetString("errorValidarTTDifferentSize"), ++bPosición, ++bPosiciónAnt);
                    throw new DifferentSize(strError);
                }

                // Dimensionar las matrices (elementos: espacios_blanco + 1)
                data._dWorkRest = new double[2][];
                data._dWorkRest[0] = new double[++bPosición];
                data._dWorkRest[1] = new double[bPosición];
                //_datos._dWorkRestDrop = new double[bPosición];
                //_datos._dWorkRestY = new double[2][];
                //_datos._dWorkRestY[0] = new double[bPosición];
                //_datos._dWorkRestY[1] = new double[bPosición];

                // Extraer los caracteres entre los espacios en blanco y pasarlos a los datos
                bPosición = 0;
                bPosiciónAnt = 0;
                foreach (char c in cadenaT)
                {
                    if (char.IsWhiteSpace(c))
                    {
                        subcadena = cadenaT.Substring(bPosiciónAnt, bPosición - bPosiciónAnt);
                        número = Convert.ToDouble(subcadena);
                        if (número > data._dMHT)
                        {
                            //strError = String.Format("Los tiempos de trabajo deben ser\ninferiores a {0:0.00} min", datos._dMHT);
                            strError = String.Format(_resources.GetString("errorValidarTTInvalidRange"), data._dMHT);
                            throw new InvalidRange(strError);
                        }

                        data._dWorkRest[0][bEspacio] = número;
                        //_datos._dWorkRestDrop[bEspacio] = 100 * número / _datos._dMHT;

                        bPosiciónAnt = bPosición;
                        ++bPosiciónAnt;
                        ++bEspacio;
                    }
                    ++bPosición;
                }
                subcadena = cadenaT.Substring(bPosiciónAnt, bPosición - bPosiciónAnt);
                número = Convert.ToDouble(subcadena);
                if (número > data._dMHT)
                {
                    //strError = String.Format("Los tiempos de trabajo deben ser\ninferiores a {0:0.00} min", datos._dMHT);
                    strError = String.Format(_resources.GetString("errorValidarTTInvalidRange"), data._dMHT);
                    throw new InvalidRange(strError);
                }
                data._dWorkRest[0][bEspacio] = Convert.ToDouble(subcadena);
                //_datos._dWorkRestDrop[bEspacio] = 100 * _datos._dWorkRest[0][bEspacio] / _datos._dMHT;
                
                bEspacio = 0;
                bPosición = 0;
                bPosiciónAnt = 0;
                foreach (char c in cadenaD)
                {
                    if (char.IsWhiteSpace(c))
                    {
                        subcadena = cadenaD.Substring(bPosiciónAnt, bPosición - bPosiciónAnt);
                        número = Convert.ToDouble(subcadena);
                        data._dWorkRest[1][bEspacio] = número;
                        //_datos._dWorkRestY[1][bEspacio] = 100 * número / _datos._dMHT;
                        bPosiciónAnt = bPosición;
                        ++bPosiciónAnt;
                        ++bEspacio;
                    }
                    ++bPosición;
                }
                subcadena = cadenaD.Substring(bPosiciónAnt, bPosición - bPosiciónAnt);
                data._dWorkRest[1][bEspacio] = Convert.ToDouble(subcadena);
                //_datos._dWorkRestY[1][bEspacio] = 100 * _datos._dWorkRest[1][bEspacio] / _datos._dMHT;

                // Finalizar
                return true;
            }
            catch (Exception e)
            {
                // Lo normal es que sólo se den cuatro tipos de errores:
                // FieldLength, DifferentSize, InvalidRange y FormatException                
                MessageBox.Show(e.Message, _resources.GetString("errorValidarTTTítulo"), MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Indicar que se ha producido un error
                return false;
            }
        }

        /// <summary>
        /// Validar la entrada del número de ciclos
        /// </summary>
        /// <returns>"True" si no ha habido problemas y "False" si los ha habido</returns>
        private Boolean ValidarC(datosWR data)
        {
            try
            {
                // Pasar el dato a byte
                if (txtCiclos.Text.Length > 0)
                    data._bCiclos = Convert.ToByte(txtCiclos.Text);
                else
                {
                    txtCiclos.Text = "1";
                    //datos._bCiclos = 1;
                    throw new FieldLength();
                }

                // Finalizar
                return true;
            }
            catch (Exception e)
            {
                // Lo normal es que sólo se den dos tipos de errores:
                // FieldLength y FormatException
                MessageBox.Show(e.Message, _resources.GetString("errorValidarCTítulo"), MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Finalizar
                return false;
            }

        }

        /// <summary>
        /// Validar la entrada del paso
        /// </summary>
        /// <returns>"True" si no ha habido problemas y "False" si los ha habido</returns>
        private Boolean ValidarPaso(datosWR data)
        {
            Boolean result = true;
            Double nSize = 0;

            try
            {
                // Comprobar que se ha introducido algún dato en el campo
                if (txtPaso.Text.Length == 0)
                {
                    txtPaso.Text = "0,01";
                    //datos._dPaso = 0.01;
                    throw new FieldLength(_resources.GetString("errorValidarPasoFieldLength"));
                }

                // Pasar el dato a doble
                data._dPaso = Convert.ToDouble(txtPaso.Text);

                // Comprobar que es un dato positivo
                if (data._dPaso <= 0)
                    throw new InvalidRange(_resources.GetString("errorValidarPasoInvalidRange"));

                // Cálculo del número de puntos de la curva y comprobar que cada tiempo de descanso
                //  entre el paso es un entero
                nSize = data._bCiclos * data._dWorkRest[0].Length + 1;
                foreach (double d in data._dWorkRest[1])
                {
                    // La opción más corta sería utilizar el operador %, pero no funciona del todo bien
                    if (Math.Abs(d / data._dPaso - Math.Floor(d / data._dPaso)) >= 0.001)
                        throw new NotAnInteger(_resources.GetString("errorValidarPasoNotAnInteger"));

                    nSize += ((d / data._dPaso)) * data._bCiclos;
                }

                data._nPuntos = Convert.ToInt32(nSize);
                data._dPoints = new double[2][];
                data._dPoints[0] = new double[data._nPuntos];
                data._dPoints[1] = new double[data._nPuntos];

            }
            catch (Exception e)
            {
                // Lo normal es que sólo se den dos tipos de errores:
                // FieldLength, InvalidRange y FormatException
                MessageBox.Show(e.Message, _resources.GetString("errorValidarPasoTítulo"), MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Finalizar
                result = false;
            }
            
            return result;
        }

        private int ComputeNumberOfPoints(datosWR data)
        {
            // Cálculo del número de puntos de la curva y comprobar que cada tiempo de descanso
            //  entre el paso es un entero
            double nSize = data._bCiclos * data._dWorkRest[0].Length + 1;
            foreach (double d in data._dWorkRest[1])
            {
                // La opción más corta sería utilizar el operador %, pero no funciona del todo bien
                if (Math.Abs(d / data._dPaso - Math.Floor(d / data._dPaso)) >= 0.001)
                    throw new NotAnInteger(_resources.GetString("errorValidarPasoNotAnInteger"));

                nSize += ((d / data._dPaso)) * data._bCiclos;
            }

            return Convert.ToInt32(nSize);
        }

        #endregion

        private void updTasks_ValueChanged(object sender, EventArgs e)
        {
            Int32 col = Convert.ToInt32(updTasks.Value);

            // Add or remove columns
            if (col > gridVariables.ColumnCount)
                for (int i = gridVariables.ColumnCount; i < col; i++) AddColumn(i);
            else if (col < gridVariables.ColumnCount)
                for (int i = gridVariables.ColumnCount - 1; i >= col; i--) gridVariables.Columns.RemoveAt(i);
        }
    }
}
