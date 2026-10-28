using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
        int filaSeleccionada = -1;
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

        private void dgvInformacion_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                filaSeleccionada = e.RowIndex;

                dgvInformacion.ClearSelection();
                dgvInformacion.Rows[e.RowIndex].Selected = true;

                dgvInformacion.CurrentCell = dgvInformacion.Rows[e.RowIndex].Cells[e.ColumnIndex];
            }
        }

        private void actualizarToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (filaSeleccionada < 0)
                return;

            DataGridViewRow fila = dgvInformacion.Rows[filaSeleccionada];

            FormAgregar actualizar = new FormAgregar(fila.Cells[1].Value.ToString(),
                fila.Cells[2].Value.ToString(),
                fila.Cells[3].Value.ToString(),
                fila.Cells[4].Value.ToString(),
                fila.Cells[5].Value.ToString(),
                Convert.ToInt32(fila.Cells[0].Value));

            actualizar.Show();
        }

        private void eliminarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (filaSeleccionada < 0)
                return;

            DataGridViewRow fila = dgvInformacion.Rows[filaSeleccionada];

            int id = Convert.ToInt32(fila.Cells[0].Value);

            DialogResult resultado = MessageBox.Show("¿Desea eliminar el registro seleccionado?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                datos = new Datos();

                SqlCommand comando = new SqlCommand("Delete From Datos Where Id = @id");
                comando.Parameters.AddWithValue("@id", id);

                bool eliminado = datos.ejecutar(comando);

                if (eliminado)
                {
                    MessageBox.Show("Registro eliminado correctamente", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                } else
                {
                    MessageBox.Show("Error al eliminar el registro", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
