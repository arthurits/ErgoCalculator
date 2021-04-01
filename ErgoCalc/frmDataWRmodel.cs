using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ErgoCalc
{
    public partial class frmDataWRmodel : Form, IChildData
    {
        // Propiedades de la clase
        public Models.WRmodel.datosWR _data;
        private System.ComponentModel.ComponentResourceManager _resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDataWRmodel));

        public object GetData => _data;

        public frmDataWRmodel()
        {
            InitializeComponent();

            AddColumn();

            // Create the header rows
            gridVariables.RowCount = 5;
            gridVariables.Rows[0].HeaderCell.Value = "Maximum voluntary contraction (MVC)";
            gridVariables.Rows[1].HeaderCell.Value = "Working times (min)";
            gridVariables.Rows[2].HeaderCell.Value = "Rest times (min)";
            gridVariables.Rows[3].HeaderCell.Value = "Number of cycles";
            gridVariables.Rows[4].HeaderCell.Value = "Numeric step";

            gridVariables[0, 4].Value = 0.1;

            // Rellenar el valor por defecto de los cuadros de texto
            txtMVC.Text = "15";
            txtTT.Text = "4 3 2";
            txtTD.Text = "4 3 2";
            txtCiclos.Text = "2";
            txtPaso.Text = "0,01";
        }

        public frmDataWRmodel (Models.WRmodel.datosWR datos)
        {
            // Inicializar el control
            InitializeComponent();

            // Definición de variables
            String strTexto = "";
            String strEspacio = " ";

            // Pasar los datos recibidos a los controles del formulario
            txtMVC.Text = datos._dMVC.ToString();
            lblTDmax.Text = datos._dMHT.ToString("##.00", System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));
            //txtTT.Text = string.Join (" ", datos._dTrabajoDescanso[0]);
            //txtTD.Text = string.Join (" ", datos._dTrabajoDescanso[1]);
            txtCiclos.Text = datos._bCiclos.ToString();
            txtPaso.Text = datos._dPaso.ToString();

            foreach (double d in datos._dWorkRest[0])
                strTexto += d.ToString() + strEspacio;
            txtTT.Text = strTexto.TrimEnd(strEspacio.ToCharArray());

            strTexto = "";
            foreach (double d in datos._dWorkRest[1])
                strTexto += d.ToString() + strEspacio;
            txtTD.Text = strTexto.TrimEnd(strEspacio.ToCharArray());

            _data._dPaso = datos._dPaso;

            // Funciona pero requiere C# 3
            /*long[] numbers = new long[] { 1, 2, 3};
            string s = String.Join(",", Array.ConvertAll(numbers, i => i.ToString()));*/

        }

        public Models.WRmodel.datosWR getData()
        {
            return _data;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // The form does not return unless all fields are validated
            this.DialogResult = DialogResult.None;

            // Validation routines
            if (ValidarMVC() != true) { txtMVC.Focus(); return; }
            if (ValidarTT() != true) { return; }
            if (ValidarC() != true) { txtCiclos.Focus(); return; }
            if (ValidarPaso() != true) { txtPaso.Focus(); return; }

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

            if (col > 0) gridVariables[col, 4].Value = 0.1;

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
        /// Shows the data into the grid control
        /// </summary>
        //private void DataToGrid(List<ModelLiberty> data)
        //{
        //    int nDataNumber = data.Count;

        //    updTasks.Value = nDataNumber;

        //    for (var j = 0; j < nDataNumber; j++)
        //    {
        //        //Column 0 is already created in the constructor;
        //        if (j > 0) AddColumn();

        //        // Populate the DataGridView with data
        //        gridVariables[j, 0].Value = (int)data[j].data.type;
        //        gridVariables[j, 1].Value = data[j].data.HorzReach.ToString();
        //        gridVariables[j, 2].Value = data[j].data.VertRangeM.ToString();
        //        gridVariables[j, 3].Value = data[j].data.DistHorz.ToString();
        //        gridVariables[j, 4].Value = data[j].data.DistVert.ToString();
        //        gridVariables[j, 5].Value = data[j].data.VertHeight.ToString();
        //        gridVariables[j, 6].Value = data[j].data.Freq.ToString();
        //        gridVariables[j, 7].Value = (int)data[j].data.gender;

        //        if ((data[j].data.type == MNType.Pulling) || (data[j].data.type == MNType.Pushing))
        //        {
        //            gridVariables[j, 1].Value = "——";
        //            gridVariables[j, 2].Value = "——";
        //            gridVariables[j, 4].Value = "——";
        //        }
        //        else
        //        {
        //            gridVariables[j, 3].Value = "——";
        //            gridVariables[j, 5].Value = "——";
        //        }

        //    }
        //}

        #endregion Private routines

        #region Eventos de los TextBox

        private void txtMVC_Validating(object sender, CancelEventArgs e)
        {
            if (txtMVC.Text.Length > 0)
            {
                /*if (ValidarMVC() == false)
                    this.txtMVC.Focus();
                else*/
                    this.lblTDmax.Text = _data._dMHT.ToString("##.00", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES"));
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
        private Boolean ValidarMVC()
        {
            //Models.WRmodel.cWRmodel modelo = new Models.WRmodel.cWRmodel();
            try
            {
                // Comprobar que se ha introducido texto en el campo
                if (txtMVC.Text.Length == 0)
                    throw new FieldLength(_resources.GetString("errorValidarMVCFieldLength"));

                // Pasar el dato a doble
                _data._dMVC = Convert.ToDouble(txtMVC.Text);

                // Comprobar que el valor está comprendido entre 0 y 100
                if (_data._dMVC <= 0 | _data._dMVC >= 100)
                {
                    //throw new InvalidRange("El valor de MVC debe estar\n comprendido entre 0 y 100");
                    throw new InvalidRange(_resources.GetString("errorValidarMVCInvalidRange"));
                }

                // Una vez comprobado que el dato es correcto, se calcula el MHT
                //_datos._dMHT = modelo.Sjogaard(_datos._dMVC);
                _data._dMHT = 5710.0 / Math.Pow(_data._dMVC, 2.14);

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
        private Boolean ValidarTT()
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
                if (cadenaT.Length > 255 | cadenaD.Length > 255)
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
                _data._dWorkRest = new double[2][];
                _data._dWorkRest[0] = new double[++bPosición];
                _data._dWorkRest[1] = new double[bPosición];
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
                        if (número > _data._dMHT)
                        {
                            //strError = String.Format("Los tiempos de trabajo deben ser\ninferiores a {0:0.00} min", datos._dMHT);
                            strError = String.Format(_resources.GetString("errorValidarTTInvalidRange"), _data._dMHT);
                            throw new InvalidRange(strError);
                        }

                        _data._dWorkRest[0][bEspacio] = número;
                        //_datos._dWorkRestDrop[bEspacio] = 100 * número / _datos._dMHT;

                        bPosiciónAnt = bPosición;
                        ++bPosiciónAnt;
                        ++bEspacio;
                    }
                    ++bPosición;
                }
                subcadena = cadenaT.Substring(bPosiciónAnt, bPosición - bPosiciónAnt);
                número = Convert.ToDouble(subcadena);
                if (número > _data._dMHT)
                {
                    //strError = String.Format("Los tiempos de trabajo deben ser\ninferiores a {0:0.00} min", datos._dMHT);
                    strError = String.Format(_resources.GetString("errorValidarTTInvalidRange"), _data._dMHT);
                    throw new InvalidRange(strError);
                }
                _data._dWorkRest[0][bEspacio] = Convert.ToDouble(subcadena);
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
                        _data._dWorkRest[1][bEspacio] = número;
                        //_datos._dWorkRestY[1][bEspacio] = 100 * número / _datos._dMHT;
                        bPosiciónAnt = bPosición;
                        ++bPosiciónAnt;
                        ++bEspacio;
                    }
                    ++bPosición;
                }
                subcadena = cadenaD.Substring(bPosiciónAnt, bPosición - bPosiciónAnt);
                _data._dWorkRest[1][bEspacio] = Convert.ToDouble(subcadena);
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
        private Boolean ValidarC()
        {
            try
            {
                // Pasar el dato a byte
                if (txtCiclos.Text.Length > 0)
                    _data._bCiclos = Convert.ToByte(txtCiclos.Text);
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
        private Boolean ValidarPaso()
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
                _data._dPaso = Convert.ToDouble(txtPaso.Text);

                // Comprobar que es un dato positivo
                if (_data._dPaso <= 0)
                    throw new InvalidRange(_resources.GetString("errorValidarPasoInvalidRange"));

                // Cálculo del número de puntos de la curva y comprobar que cada tiempo de descanso
                //  entre el paso es un entero
                nSize = _data._bCiclos * _data._dWorkRest[0].Length + 1;
                foreach (double d in _data._dWorkRest[1])
                {
                    // La opción más corta sería utilizar el operador %, pero no funciona del todo bien
                    if (Math.Abs(d / _data._dPaso - Math.Floor(d / _data._dPaso)) >= 0.001)
                        throw new NotAnInteger(_resources.GetString("errorValidarPasoNotAnInteger"));

                    nSize += ((d / _data._dPaso)) * _data._bCiclos;
                }

                _data._nPuntos = Convert.ToInt32(nSize);
                _data._dPoints = new double[2][];
                _data._dPoints[0] = new double[_data._nPuntos];
                _data._dPoints[1] = new double[_data._nPuntos];

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
