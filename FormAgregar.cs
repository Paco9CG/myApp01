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
    public partial class FormAgregar : Form
    {
        Datos datos;
        public FormAgregar()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            datos = new Datos();
            bool f = datos.insertar(txtNombre.Text, txtPaterno.Text, txtMaterno.Text, mtbTelefono.Text, txtCorreo.Text);

            if (f)
            {
                MessageBox.Show("Registro agregado correctamente", "Agregar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();

            }
            else
            {
                MessageBox.Show("Error al agregar el registro", "Agregar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
