using System;
using System.Data;
using System.Windows.Forms;

using ErgoCalc.Models.MetabolicRate;

namespace ErgoCalc;

public partial class FrmDataMet : Form, IChildData
{
    private Job _job;
    public object GetData => _job;

    public FrmDataMet()
    {
        // VS Designer initialization routine
        InitializeComponent();

        // Create a table to store the values for cboOcupaciones
            DataTable datCboOcupaciones = new DataTable();
            datCboOcupaciones.Columns.Add("Display", typeof(String));
            datCboOcupaciones.Columns.Add("Value", typeof(Int32));
            datCboOcupaciones.Rows.Add("Agricultura", 1);
            datCboOcupaciones.Rows.Add("Artes gráficas", 2);
            datCboOcupaciones.Rows.Add("Artesanos", 3);
            datCboOcupaciones.Rows.Add("Industria del hierro y del acero", 4);
            datCboOcupaciones.Rows.Add("Industria del metal", 5);
            datCboOcupaciones.Rows.Add("Minería", 6);
            datCboOcupaciones.Rows.Add("Ocupaciones varias", 7);
            datCboOcupaciones.Rows.Add("Trabajo de oficina", 8);
            datCboOcupaciones.Rows.Add("Transporte", 9);

            // Associate the table to cboOcupaciones
            cboOcupaciones.DisplayMember = "Display";
            cboOcupaciones.ValueMember = "Value";
            cboOcupaciones.DataSource = datCboOcupaciones;
            cboOcupaciones.SelectedValue = 0;

        // Create a table to store the values for cboCategorias
            DataTable datCboClase = new DataTable();
            datCboClase.Columns.Add("Display", typeof(String));
            datCboClase.Columns.Add("Value", typeof(Int32));
            datCboClase.Rows.Add("Descanso", 1);
            datCboClase.Rows.Add("Tasa metabólica baja", 2);
            datCboClase.Rows.Add("Tasa metabólica moderada", 3);
            datCboClase.Rows.Add("Tasa metabólica alta", 4);
            datCboClase.Rows.Add("Tasa metabólica muy alta", 5);

            // Associate the table to cboCategorias
            cboClase.DisplayMember = "Display";
            cboClase.ValueMember = "Value";
            cboClase.DataSource = datCboClase;
            cboClase.SelectedValue = 0;

        // Initialize the DataGridView
            gridLevel2A.Columns.Add("col1","Parte del cuerpo");
            gridLevel2A.Columns.Add("col2","Carga de trabajo");
            gridLevel2A.Columns.Add("col3","Postura");

            AddRow(0);


    }

    public FrmDataMet(Job job)
        :this()
    {
        _job = job;
        //DataToGrid();
    }

    private void cboOcupaciones_SelectedValueChanged(object sender, EventArgs e)
    {
        Int32 nSelection = Convert.ToInt32(cboOcupaciones.SelectedValue);
        DataTable datCboData = new DataTable();
        datCboData.Columns.Add("Display", typeof(String));
        datCboData.Columns.Add("Value", typeof(Int32));

        switch (nSelection)
        {
            case 1:     // Agricultura
                datCboData.Rows.Add("Jardinero", 1);
                datCboData.Rows.Add("Tractorista", 2);
                break;
            case 2:     // Artes gráficas
                datCboData.Rows.Add("Componedor a mano", 3);
                datCboData.Rows.Add("Encuadernador", 4);
                break;
            case 3:     // Artesanos
                datCboData.Rows.Add("Albañil", 5);
                datCboData.Rows.Add("Carnicero", 6);
                datCboData.Rows.Add("Carpintero", 7);
                datCboData.Rows.Add("Cristalero", 8);
                datCboData.Rows.Add("Panadero", 9);
                datCboData.Rows.Add("Pintor", 10);
                datCboData.Rows.Add("Relojero", 11);
                break;
            case 4:     // Industria del hierro y del acero
                datCboData.Rows.Add("Fundidor", 12);
                datCboData.Rows.Add("Moldeo a máquina", 13);
                datCboData.Rows.Add("Modeo manual", 14);
                datCboData.Rows.Add("Operador de alto horno", 15);
                datCboData.Rows.Add("Operador de horno eléctrico", 16);
                break;
            case 5:     // Industria del metal
                datCboData.Rows.Add("Fresador", 17);
                datCboData.Rows.Add("Herrero", 18);
                datCboData.Rows.Add("Mecánico de precisión", 19);
                datCboData.Rows.Add("Soldador", 20);
                datCboData.Rows.Add("Tornero", 21);
                break;
            case 6:     // Minería
                datCboData.Rows.Add("Operador de horno de coque", 22);
                datCboData.Rows.Add("Operador de vagoneta", 23);
                datCboData.Rows.Add("Picador de carbón", 24);
                break;
            case 7:     // Ocupaciones varias
                datCboData.Rows.Add("Ayudante de laboratorio", 25);
                datCboData.Rows.Add("Dependiente de comercio", 26);
                datCboData.Rows.Add("Profesor", 27);
                datCboData.Rows.Add("Secretario", 28);
                break;
            case 8:     // Trabajo de oficina
                datCboData.Rows.Add("Conserje", 29);
                datCboData.Rows.Add("Trabajo administrativo", 30);
                datCboData.Rows.Add("Trabajo sedentario", 31);
                break;
            case 9:     // Transporte
                datCboData.Rows.Add("Conductor de autobús", 32);
                datCboData.Rows.Add("Conductor de automóvil", 33);
                datCboData.Rows.Add("Conductor de tranvía", 34);
                datCboData.Rows.Add("Operador de grúa", 35);
                break;
            default:        
                break;
        }

        cboCategorias.DataSource = datCboData;
        cboCategorias.DisplayMember = "Display";
        cboCategorias.ValueMember = "Value";

    }

    private void cboOcupaciones_Leave(object sender, EventArgs e)
    {
        if (cboOcupaciones.SelectedValue == null)
        {
            cboCategorias.SelectedValue = 0;
            cboOcupaciones_SelectedValueChanged(sender, e);
        }
        
    }


    private void cboClase_SelectedValueChanged(object sender, EventArgs e)
    {
        Int32 nSelection = Convert.ToInt32(cboClase.SelectedValue);
        String strTexto = String.Empty;

        switch (nSelection)
        {
            case 1:     // Descanso
                strTexto = "Descansado, sentado cómodamente";
                break;
            case 2:     // Tasa metabólica baja
                strTexto = "Trabajo manual ligero (escribir, teclear, dibujar, coser, anotar contabilidad); trabajo ";
                strTexto += "con brazos y manos (herramientas pequeñas, inspección, montaje o clasificación de materiales ";
                strTexto += "ligeros); trabajo con pie y piernas (conducción de vehículos en condiciones normales, ";
                strTexto += "empleo de pedales de accionamiento).\n\n";
                strTexto += "De pie, taladrado (piezas pequeñas); fresado (piezas pequeñas); enrollado de bobinas y de pequeñas ";
                strTexto += "armaduras; mecanizado con herramientas de pequeña potencia; caminar sin prisa (velocidad de hasta 2.5 km/h).";
                break;
            case 3:     // Tasa metabólica moderada
                strTexto = "Trabajo sostenido con manos y brazos (clavar clavos, limar); trabajo con brazos y piernas ";
                strTexto += "(conducción de camiones, tractores o máquinas de obras públicas en obras); trabajo con tronco y brazos ";
                strTexto += "(martillos neumáticos, acoplamiento de aperos a tractor, enyesado, manejo intermitante de pesos moderados, ";
                strTexto += "escardar, usar la azada, recoger frutas y verduras, tirar de o empujar carretillas ligeras, caminar";
                strTexto += "a una velocidad de 2.5 km/h hasta 5.5 km/h, trabajos en forja).";
                break;
            case 4:     // Industria del hierro y del acero
                strTexto = "Trabajo intenso con brazos y tronco; transporte de materiales pesados; palear; empleo de macho o maza; ";
                strTexto += "empleo de sierra: cepillado o escopleado de madera dura: corte de hierba o cavado manual; caminar a ";
                strTexto += "una velocidad de 5.5 km/h hasta 7 km/h.\n\n";
                strTexto += "Empujar o tirar de carretillas o carros de mano muy cargados; desbarbado de fundición; ";
                strTexto += "colocación de bloques de hormigón.";
                break;
            case 5:     // Tasa metabólica muy alta
                strTexto = "Actividad muy intensa a ritmo de muy rápido a máximo; trabajo con hacha; cavado o paleado intenso; ";
                strTexto += "subir escaleras, rampas o escalas; caminar rápidamente a pequeños pasos; correr, caminar a una ";
                strTexto += "velocidad superior a los 7 km/h.";
                break;
            default:                    
                break;
        }

        // Associate the text to the label
        lblClaseExp.Text = strTexto;

    }

    private void cboClase_Leave(object sender, EventArgs e)
    {
        if (cboClase.SelectedValue == null)
            lblClaseExp.Text = "";
    }

    private void updTasks_ValueChanged(object sender, EventArgs e)
    {
        Int32 row = Convert.ToInt32(updTasks.Value);

        // Add or remove DataGridViewRows
        if (row > gridLevel2A.RowCount)
            AddRow(row - 1);
        else if (row < gridLevel2A.RowCount)
            gridLevel2A.Rows.RemoveAt(row);

        return;
    }

    private void btnAccept_Click(object sender, EventArgs e)
    {
        // Store the user selections in the public variables in order to be accessed later from another WinForm
        _job = new Job();
        _job.Tasks = new Task[1];
        _job.Tasks[0] = new Task();
        _job.Tasks[0].Data.Category = Convert.ToInt32(cboCategorias.SelectedValue) - 1;
        _job.Tasks[0].Data.Type = Convert.ToInt32(cboClase.SelectedValue) - 1;
    }

    #region Private routines

    /// <summary>
    /// Adds a column to the DataGrid View and formates it
    /// </summary>
    /// <param name="col">Column number (zero based)</param>
    private void AddColumn(Int32 col)
    {
        String[] strTasks = new String[] { "A", "B", "C", "D", "E" };

        // Create the new column
        gridLevel2A.Columns.Add("Column" + (col + 1).ToString(), "Task " + strTasks[col]);
        gridLevel2A.Columns[col].SortMode = DataGridViewColumnSortMode.NotSortable;
        gridLevel2A.Columns[col].Width = 70;

        // Give format to the cells
        if (col > 0)
            gridLevel2A.Rows[7].Cells[col] = (DataGridViewComboBoxCell)gridLevel2A.Rows[7].Cells[col - 1].Clone();

        return;
    }

    private void AddRow(Int32 row)
    {
        String[] strTasks = new String[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20" };

        // Create the new row
        gridLevel2A.Rows.Add();
        gridLevel2A.Rows[row].HeaderCell.Value = "Tarea "+ strTasks[row];

        // Give format to the cells
        if (row == 0)
        {
            // Create custom cells with combobox display
                DataGridViewComboBoxCell celdaColumn1 = new DataGridViewComboBoxCell();
                DataGridViewComboBoxCell celdaColumn2 = new DataGridViewComboBoxCell();
                DataGridViewComboBoxCell celdaColumn3 = new DataGridViewComboBoxCell();
                DataTable tableColumn1 = new DataTable();
                DataTable tableColumn2 = new DataTable();
                DataTable tableColumn3 = new DataTable();

            // Create the tables containing the data
                tableColumn1.Columns.Add("Display", typeof(String));
                tableColumn1.Columns.Add("Value", typeof(Int32));
                tableColumn1.Rows.Add("Ambas manos", 1);
                tableColumn1.Rows.Add("Un brazo", 2);
                tableColumn1.Rows.Add("Ambos brazos", 3);
                tableColumn1.Rows.Add("Cuerpo entero", 4);

                tableColumn2.Columns.Add("Display", typeof(String));
                tableColumn2.Columns.Add("Value", typeof(Int32));
                tableColumn2.Rows.Add("Ligera", 1);
                tableColumn2.Rows.Add("Moderada", 2);
                tableColumn2.Rows.Add("Pesada", 3);

                tableColumn3.Columns.Add("Display", typeof(String));
                tableColumn3.Columns.Add("Value", typeof(Int32));
                tableColumn3.Rows.Add("Sentado", 1);
                tableColumn3.Rows.Add("De pie", 2);
                tableColumn3.Rows.Add("En cuclillas", 3);

            // Associate the tables to the combobox
                celdaColumn1.DataSource = tableColumn1;
                celdaColumn1.DisplayMember = "Display";
                celdaColumn1.ValueMember = "Value";

                celdaColumn2.DataSource = tableColumn2;
                celdaColumn2.DisplayMember = "Display";
                celdaColumn2.ValueMember = "Value";

                celdaColumn3.DataSource = tableColumn3;
                celdaColumn3.DisplayMember = "Display";
                celdaColumn3.ValueMember = "Value";

            // Associate the comboboxes tho the DataGridView
                gridLevel2A.Rows[row].Cells[0] = celdaColumn1;
                gridLevel2A.Rows[row].Cells[1] = celdaColumn2;
                gridLevel2A.Rows[row].Cells[2] = celdaColumn3;
        }
        else
        {
            for (Int16 i = 0; i < gridLevel2A.ColumnCount; i++)
                gridLevel2A.Rows[row].Cells[i] = (DataGridViewComboBoxCell)gridLevel2A.Rows[row - 1].Cells[i].Clone();
        }
    }

    public void LoadExample()
    {
    }

    #endregion
}
