using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace myApp01
{
    public partial class FormInformacion : Form
    {
        Datos datos;
        public FormInformacion()
        {
            InitializeComponent();
        }

        private void FormInformacion_Load(object sender, EventArgs e)
        {
            datos = new Datos();
            DataSet ds = datos.informacion("Select * From Datos");
            if(ds != null)
            {
              //  dgvInformacion.DataSource = ds.Tables[0];
              actualizaDatos(ds);
            } else
            {
                MessageBox.Show("Error al cargar información", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void actualizaDatos(DataSet ds)
        {
            dgvInformacion.AutoGenerateColumns = false;
            dgvInformacion.Columns[0].DataPropertyName = "Id";
            dgvInformacion.Columns[1].DataPropertyName = "Nombre";
            dgvInformacion.Columns[2].DataPropertyName = "Paterno";
            dgvInformacion.Columns[3].DataPropertyName = "Materno";
            dgvInformacion.Columns[4].DataPropertyName = "Telefono";
            dgvInformacion.Columns[5].DataPropertyName = "Correo";

            dgvInformacion.DataSource = ds.Tables[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormAgregar agregar = new FormAgregar();
            agregar.Show();
        }

        private void FormInformacion_Activated(object sender, EventArgs e)
        {
            datos = new Datos();
            DataSet ds = datos.informacion("Select * From Datos");
            actualizaDatos(ds);
        }
    }
}
