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
    public partial class FormAgregar : Form
    {
        Datos datos;
        bool updateMode = false;
        int id;
        public FormAgregar()
        {
            InitializeComponent();
        }

        public FormAgregar(string nombre, string paterno, string materno, string telefono, string correo, int id)
        {
            InitializeComponent();
            txtNombre.Text = nombre;
            txtPaterno.Text = paterno;
            txtMaterno.Text = materno;
            mtbTelefono.Text = telefono;
            txtCorreo.Text = correo;
            btnAgregar.Text = "Actualizar";
            updateMode = true;
            this.id = id;
            this.Text = "Actualizar Registro";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            datos = new Datos();

            if(updateMode == false)
            {
                bool f = datos.ejecutar("Insert Into Datos(nombre,paterno,materno,telefono,correo) Values ('" + txtNombre.Text + "','" + txtPaterno.Text + "','" + txtMaterno.Text + "','" + mtbTelefono.Text + "','" + txtCorreo.Text + "')");

                if (f)
                {
                    MessageBox.Show("Registro agregado correctamente");
                    this.Close();
                } else
                {
                    MessageBox.Show("Error al agregar el registro");
                }                
            } else
            {
                SqlCommand comando = new SqlCommand("Update Datos Set nombre=@nombre, " + "paterno=@paterno, materno=@materno, telefono=@telefono, " + "correo=@correo Where id=@id");
                comando.Parameters.AddWithValue("@nombre", txtNombre.Text);
                comando.Parameters.AddWithValue("@paterno", txtPaterno.Text);
                comando.Parameters.AddWithValue("@materno", txtMaterno.Text);
                comando.Parameters.AddWithValue("@telefono", mtbTelefono.Text);
                comando.Parameters.AddWithValue("@correo", txtCorreo.Text);
                comando.Parameters.AddWithValue("@id", id);
                bool f = datos.ejecutar(comando); // SqlCommand pasando como string ahora no es posible, pero el código lo usa así. Debería cambiar a parámetros o método con SqlCommand.

                if (f)
                {
                    MessageBox.Show("Registro actualizado correctamente", "Agregar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();

                }
                else
                {
                    MessageBox.Show("Error al actualizar el registro", "Agregar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            
        }

        private void FormAgregar_Load(object sender, EventArgs e)
        {
            datos = new Datos();
            DataSet dsEstados;

            dsEstados = datos.informacion("Select * From Estados");

            if(dsEstados != null && dsEstados.Tables.Count > 0)
            {
                cbEstados.DataSource = dsEstados.Tables[0];
                cbEstados.DisplayMember = "nombre";
                cbEstados.ValueMember = "id_estado";

                cambiosMunicipios(Convert.ToInt32(cbEstados.SelectedValue));
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            cambiosMunicipios(Convert.ToInt32(cbEstados.SelectedIndex + 1));
        }

        private void cambiosMunicipios(int idEstado)
        {
            DataSet dsMunicipios;
            dsMunicipios = datos.informacion("Select * From Municipios Where id_estado = " + idEstado);

            if (dsMunicipios != null && dsMunicipios.Tables.Count > 0)
            {
                cbMunicipios.DataSource = dsMunicipios.Tables[0];
                cbMunicipios.DisplayMember = "nombre";
                cbMunicipios.ValueMember = "id_municipio";
            }
        }
    }
}
