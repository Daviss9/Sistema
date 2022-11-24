using System;
//Agregar Libreria -> trabajar con BD SQL Server
using System.Data.SqlClient;

namespace Sistema.Datos
{
    public class Conexion
    {
        //Agregamos las propiedades, estas no se van compartir con las demas clases
        //Lo que se comparte es la cadena de conexion

        //nombre de la base de datos
        private string Base;
        //nombre del servidor
        private string Servidor;
        //nombre del usuario
        private string Usuario;
        //clave de acceso
        private string Clave;
        //Elegimos si trabajamos con auth de windows o SQL
        private bool Seguridad;

        //Declaramos el objeto conexion que instancie a la clase conexion, privado y estatico
        private static Conexion Con = null;

        //Esta clase no pueda ser instanciada en otra clase
        private Conexion()
        {
            this.Base = "dbsistema";
            this.Servidor = "DESKTOP-54P5F6N"; //Casa
            //this.Servidor = "DESKTOP-DO5T73H"; //Jale
            this.Usuario = "sa";
            this.Clave = "1234569";
            //this.Seguridad = true; //Casa ->indico que sera la seguridad de windows
            this.Seguridad = false; //Jale -> indicando inicio con SQL Autenticacion
        }
        public SqlConnection CrearConexion()
        {
            SqlConnection Cadena = new SqlConnection();
            try
            {
                //creamos la cadena de conexion a la DB
                Cadena.ConnectionString = "Server=" + this.Servidor + ";Database=" + this.Base + ";";
                if (this.Seguridad)
                {
                    //Con autenticacion windows -> Es mejor utilizar el Integrated Security SSPI "Seguridad integrada de windows"
                    Cadena.ConnectionString = Cadena.ConnectionString + "Integrated Security = SSPI";
                }
                else
                {
                    //Con autenticacion SQL
                    Cadena.ConnectionString = Cadena.ConnectionString + "User Id=" + this.Usuario + ";Password=" + this.Clave;
                }
            }
            catch (Exception ex)
            {
                Cadena = null;
                throw ex;
            }
            return Cadena;
        }
        public static Conexion getInstancia()
        {
            if (Con == null)
            {
                Con = new Conexion();
            }
            return Con;
        }

    }
}
