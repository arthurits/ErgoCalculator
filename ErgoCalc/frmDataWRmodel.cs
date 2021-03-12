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
    public partial class frmDataWRmodel : Form
    {
        // Propiedades de la clase
        public Models.WRmodel.datosWR _datos;
        private System.ComponentModel.ComponentResourceManager _resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDataWRmodel));

        public frmDataWRmodel()
        {
            InitializeComponent();

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

            foreach (double d in datos._dTrabajoDescanso[0])
                strTexto += d.ToString() + strEspacio;
            txtTT.Text = strTexto.TrimEnd(strEspacio.ToCharArray());

            strTexto = "";
            foreach (double d in datos._dTrabajoDescanso[1])
                strTexto += d.ToString() + strEspacio;
            txtTD.Text = strTexto.TrimEnd(strEspacio.ToCharArray());

            _datos._dPaso = datos._dPaso;

            // Funciona pero requiere C# 3
            /*long[] numbers = new long[] { 1, 2, 3};
            string s = String.Join(",", Array.ConvertAll(numbers, i => i.ToString()));*/

        }

        public Models.WRmodel.datosWR getData()
        {
            return _datos;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

            if (ValidarMVC() != true) { txtMVC.Focus(); return; }
            if (ValidarTT() != true) { return; }
            if (ValidarC() != true) { txtCiclos.Focus(); return; }
            if (ValidarPaso() != true) { txtPaso.Focus(); return; }

            // Calcular

            // Salir y cerrar el formulario
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        #region Eventos de los TextBox

        private void txtMVC_Validating(object sender, CancelEventArgs e)
        {
            if (txtMVC.Text.Length > 0)
            {
                /*if (ValidarMVC() == false)
                    this.txtMVC.Focus();
                else*/
                    this.lblTDmax.Text = _datos._dMHT.ToString("##.00", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES"));
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
                _datos._dMVC = Convert.ToDouble(txtMVC.Text);

                // Comprobar que el valor está comprendido entre 0 y 100
                if (_datos._dMVC <= 0 | _datos._dMVC >= 100)
                {
                    //throw new InvalidRange("El valor de MVC debe estar\n comprendido entre 0 y 100");
                    throw new InvalidRange(_resources.GetString("errorValidarMVCInvalidRange"));
                }

                // Una vez comprobado que el dato es correcto, se calcula el MHT
                //_datos._dMHT = modelo.Sjogaard(_datos._dMVC);
                _datos._dMHT = 5710.0 / Math.Pow(_datos._dMVC, 2.14);

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
                _datos._dTrabajoDescanso = new double[2][];
                _datos._dTrabajoDescanso[0] = new double[++bPosición];
                _datos._dTrabajoDescanso[1] = new double[bPosición];
                _datos._dTrabajoDescansop = new double[2][];
                _datos._dTrabajoDescansop[0] = new double[bPosición];
                _datos._dTrabajoDescansop[1] = new double[bPosición];

                // Extraer los caracteres entre los espacios en blanco y pasarlos a los datos
                bPosición = 0;
                bPosiciónAnt = 0;
                foreach (char c in cadenaT)
                {
                    if (char.IsWhiteSpace(c))
                    {
                        subcadena = cadenaT.Substring(bPosiciónAnt, bPosición - bPosiciónAnt);
                        número = Convert.ToDouble(subcadena);
                        if (número > _datos._dMHT)
                        {
                            //strError = String.Format("Los tiempos de trabajo deben ser\ninferiores a {0:0.00} min", datos._dMHT);
                            strError = String.Format(_resources.GetString("errorValidarTTInvalidRange"), _datos._dMHT);
                            throw new InvalidRange(strError);
                        }

                        _datos._dTrabajoDescanso[0][bEspacio] = número;
                        _datos._dTrabajoDescansop[0][bEspacio] = 100 * número / _datos._dMHT;

                        bPosiciónAnt = bPosición;
                        ++bPosiciónAnt;
                        ++bEspacio;
                    }
                    ++bPosición;
                }
                subcadena = cadenaT.Substring(bPosiciónAnt, bPosición - bPosiciónAnt);
                número = Convert.ToDouble(subcadena);
                if (número > _datos._dMHT)
                {
                    //strError = String.Format("Los tiempos de trabajo deben ser\ninferiores a {0:0.00} min", datos._dMHT);
                    strError = String.Format(_resources.GetString("errorValidarTTInvalidRange"), _datos._dMHT);
                    throw new InvalidRange(strError);
                }
                _datos._dTrabajoDescanso[0][bEspacio] = Convert.ToDouble(subcadena);
                _datos._dTrabajoDescansop[0][bEspacio] = 100 * _datos._dTrabajoDescanso[0][bEspacio] / _datos._dMHT;
                
                bEspacio = 0;
                bPosición = 0;
                bPosiciónAnt = 0;
                foreach (char c in cadenaD)
                {
                    if (char.IsWhiteSpace(c))
                    {
                        subcadena = cadenaD.Substring(bPosiciónAnt, bPosición - bPosiciónAnt);
                        número = Convert.ToDouble(subcadena);
                        _datos._dTrabajoDescanso[1][bEspacio] = número;
                        _datos._dTrabajoDescansop[1][bEspacio] = 100 * número / _datos._dMHT;
                        bPosiciónAnt = bPosición;
                        ++bPosiciónAnt;
                        ++bEspacio;
                    }
                    ++bPosición;
                }
                subcadena = cadenaD.Substring(bPosiciónAnt, bPosición - bPosiciónAnt);
                _datos._dTrabajoDescanso[1][bEspacio] = Convert.ToDouble(subcadena);
                _datos._dTrabajoDescansop[1][bEspacio] = 100 * _datos._dTrabajoDescanso[1][bEspacio] / _datos._dMHT;

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
                    _datos._bCiclos = Convert.ToByte(txtCiclos.Text);
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
                _datos._dPaso = Convert.ToDouble(txtPaso.Text);

                // Comprobar que es un dato positivo
                if (_datos._dPaso <= 0)
                    throw new InvalidRange(_resources.GetString("errorValidarPasoInvalidRange"));

                // Cálculo del número de puntos de la curva y comprobar que cada tiempo de descanso
                //  entre el paso es un entero
                nSize = _datos._bCiclos * _datos._dTrabajoDescanso[0].Length + 1;
                foreach (double d in _datos._dTrabajoDescanso[1])
                {
                    // La opción más corta sería utilizar el operador %, pero no funciona del todo bien
                    if (Math.Abs(d / _datos._dPaso - Math.Floor(d / _datos._dPaso)) >= 0.001)
                        throw new NotAnInteger(_resources.GetString("errorValidarPasoNotAnInteger"));

                    nSize += ((d / _datos._dPaso)) * _datos._bCiclos;
                }

                _datos._nPuntos = Convert.ToInt32(nSize);
                _datos._points = new double[2][];
                _datos._points[0] = new double[_datos._nPuntos];
                _datos._points[1] = new double[_datos._nPuntos];

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
    }
}
