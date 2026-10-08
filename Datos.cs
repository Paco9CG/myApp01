using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace myApp01
{
    internal class Datos
    {
        string cadenaConexion = "Server=localhost,1433;Integrated Security=false;" +"User=sa;password=l12345678.;initial catalog=Agenda";

        private SqlConnection conexionOpen()
        {
            try {
                SqlConnection conexion = new SqlConnection(cadenaConexion);
                conexion.Open();
                return conexion;
            } catch (Exception ex) { 
                Console.WriteLine("Error: " + ex.ToString());
                return null;
            }
        }

        private SqlConnection conexionClose(SqlConnection conexion)
        {
            try
            {
                conexion.Close();
                return conexion;
            }
            catch (Exception ex) { 
                Console.WriteLine("Error: " + ex.ToString());
                return null;
            }
        }

        public bool insertar(string nombre, string paterno, string materno, string telefono, string correo)
        {
            try
            {
                SqlConnection conectar = conexionOpen();
                string comando = "Insert Into Datos(Nombre,Paterno,Materno,Telefono,Correo) Values('" + nombre + "','" + paterno + "','" + materno + "','" + telefono + "','" + correo + "')";
                SqlCommand sqlCommand = new SqlCommand(comando, conectar);
                sqlCommand.ExecuteNonQuery();
                conexionClose(conectar);
                return true;
            } catch(Exception ex){
                Console.WriteLine("Error: " + ex.ToString());
                return false;
            }
        }

        public DataSet informacion(string comando)
        {
            DataSet ds = new DataSet();
            try {
                SqlConnection conecta = conexionOpen();
                SqlDataAdapter da = new SqlDataAdapter(comando, conecta);
                da.Fill(ds);
                conexionClose(conecta);
                return ds;

            } catch(Exception ex){
                Console.WriteLine("Error: " + ex.ToString());
                return null;
            }
        }
    }
}
