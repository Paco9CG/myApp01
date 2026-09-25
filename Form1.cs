using CsvHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsvHelper.Configuration;

namespace myApp01
{
    public partial class Form1 : Form
    {
        List<Persona> registros = new List<Persona>();
        public string rutaCSV = "";
        public Form1()
        {
            InitializeComponent();
            btnGuardar.Enabled = false;
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            if (ofdCSV.ShowDialog() == DialogResult.OK)
            {
                rutaCSV = ofdCSV.FileName;
                var reader = new StreamReader(rutaCSV);
                var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                registros = csv.GetRecords<Persona>().ToList();

                reader.Close();

                dgvRegistros.Rows.Clear();

                foreach (var registro in registros)
                {
                    dgvRegistros.Rows.Add(registro.id, registro.name, registro.email);
                }
            }
        }

        private void dgvRegistros_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Form2 editar = new Form2(
                dgvRegistros.Rows[e.RowIndex].Cells[1].Value.ToString(),
                dgvRegistros.Rows[e.RowIndex].Cells[2].Value.ToString()
                );
            
            if(editar.ShowDialog() == DialogResult.OK)
            {
                string nombre = editar.actualizaNombre;
                string correo = editar.actualizaCorreo;
                dgvRegistros.Rows[e.RowIndex].Cells[1].Value = nombre;
                dgvRegistros.Rows[e.RowIndex].Cells[2].Value = correo;

                btnGuardar.Enabled = true;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            registros.Clear();

            foreach(DataGridViewRow fila in dgvRegistros.Rows)
            {
                Persona persona = new Persona();

                persona.id = Convert.ToInt32(fila.Cells[0].Value);
                persona.name = fila.Cells[1].Value.ToString();
                persona.email = fila.Cells[2].Value.ToString();

                registros.Add(persona);
            }

            var Writer = new StreamWriter(rutaCSV);
            var csv = new CsvWriter(Writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(registros);

            Writer.Close();

            btnGuardar.Enabled = false;

            MessageBox.Show("Cambios guardados correctmente.", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
