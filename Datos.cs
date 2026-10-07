using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace myApp01
{
    internal class Datos
    {
        SqlConnection conexion;
        string cadenaConexion = "Server=localhost,1433;Integrated Security=false;" +"User=sa;password=l12345678.;initial catalog=Agenda";

        private void conexionOpen()
        {
            try {
                conexion = new SqlConnection(cadenaConexion);
                conexion.Open();
            } catch (Exception ex) { Console.WriteLine("Error: " + ex.ToString()); }
        }

        private void conexionClose()
        {
            try
            {
                conexion.Close();
            }
            catch (Exception ex) { Console.WriteLine("Error: " + ex.ToString()); }
        }

        public bool insertar(string nombre, string paterno, string materno, string telefono, string correo)
        {
            try
            {
                conexionOpen();
                string comando = "Insert Into Datos(Nombre,Paterno,Materno,Telefono,Correo) Values('" + nombre + "','" + paterno + "','" + materno + "','" + telefono + "','" + correo + "')";
                SqlCommand sqlCommand = new SqlCommand(comando, conexion);
                sqlCommand.ExecuteNonQuery();
                return true;
            } catch(Exception ex){
                Console.WriteLine("Error: " + ex.ToString());
                return false;
            }
        }
    }
}
