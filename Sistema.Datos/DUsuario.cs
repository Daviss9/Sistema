using Sistema.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sistema.Datos
{
    public class DUsuario
    {
        public DataTable Listar()
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                //Instanciamos la conexion
                SqlCon = Conexion.getInstancia().CrearConexion();
                //Llamamos el USP Categoria Listar
                SqlCommand Comando = new SqlCommand("usuario_listar", SqlCon);
                //Indicamos que es de tipo SP
                Comando.CommandType = CommandType.StoredProcedure;
                //Abrimos la Conexion
                SqlCon.Open();
                //Ejecutamos el SP
                Resultado = Comando.ExecuteReader();
                //Cargamos la respuesta a la variable Tabla
                Tabla.Load(Resultado);
                return Tabla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
        }

        public DataTable Buscar(string Valor)
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                //Instanciamos la conexion
                SqlCon = Conexion.getInstancia().CrearConexion();
                //Llamamos el USP Categoria Listar
                SqlCommand Comando = new SqlCommand("usuario_buscar", SqlCon);
                //Indicamos que es de tipo SP
                Comando.CommandType = CommandType.StoredProcedure;
                //Indicamos que el SP espera parametros para realizar la consulta, tipo Varchar, Este value llega de la capa Negocios
                Comando.Parameters.Add("@valor", SqlDbType.VarChar).Value = Valor;
                //Abrimos la Conexion
                SqlCon.Open();
                //Ejecutamos el SP
                Resultado = Comando.ExecuteReader();
                //Cargamos la respuesta a la variable Tabla
                Tabla.Load(Resultado);
                return Tabla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
        }
 // create proc usuario_login
 //   @email varchar(50),
	//@clave varchar(50)
	//as
	//select u.idusuario, u.idrol, r.nombre as rol, u.nombre, u.estado from usuario u inner join rol r on u.idrol = r.idrol
 //   where u.email= @email and u.clave= HASHBYTES('SHA2_256', @clave)
 //   go
        public DataTable Login(string Email, string Clave)
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                //Instanciamos la conexion
                SqlCon = Conexion.getInstancia().CrearConexion();
                //Llamamos el USP Categoria Listar
                SqlCommand Comando = new SqlCommand("usuario_login", SqlCon);
                //Indicamos que es de tipo SP
                Comando.CommandType = CommandType.StoredProcedure;
                //Indicamos que el SP espera parametros para realizar la consulta, tipo Varchar, Este value llega de la capa Negocios
                Comando.Parameters.Add("@email", SqlDbType.VarChar).Value = Email;
                Comando.Parameters.Add("@clave", SqlDbType.VarChar).Value = Clave;
                //Abrimos la Conexion
                SqlCon.Open();
                //Ejecutamos el SP
                Resultado = Comando.ExecuteReader();
                //Cargamos la respuesta a la variable Tabla
                Tabla.Load(Resultado);
                return Tabla;
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
        }

        public string Existe(string Valor)
        {
            string Rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon = Conexion.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("usuario_existe", SqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                //Indicamos que el SP espera parametros Nombre y Descripcion para realizar la consulta, tipo Varchar, Este value llega de la capa Negocios
                Comando.Parameters.Add("@valor", SqlDbType.VarChar).Value = Valor;
                //Recibimos respuesta del Output del SP
                SqlParameter ParExiste = new SqlParameter();
                //Indico el parametro de salida
                ParExiste.ParameterName = "@existe";
                //Tipo de parametro de salida
                ParExiste.SqlDbType = SqlDbType.Int;
                //Indico que es parametro de Salida
                ParExiste.Direction = ParameterDirection.Output;
                //Agrego el parametro de Salida
                Comando.Parameters.Add(ParExiste);
                //Abrimos la Conexion
                SqlCon.Open();
                //Ejecutamos el SP, usamos el If Ternario, para indicar si se guardo o no
                Comando.ExecuteNonQuery();
                Rpta = Convert.ToString(ParExiste.Value);
            }
            catch (Exception ex)
            {
                Rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
            return Rpta;

        }
        public string Insertar(Usuario Obj)
        {
            string Rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon = Conexion.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("usuario_insertar", SqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                //Indicamos que el SP espera parametros Nombre y Descripcion para realizar la consulta, tipo Varchar, Este value llega de la capa Negocios
                Comando.Parameters.Add("@idrol", SqlDbType.Int).Value = Obj.IdRol;
                Comando.Parameters.Add("@nombre", SqlDbType.VarChar).Value = Obj.Nombre;
                Comando.Parameters.Add("@tipo_documento", SqlDbType.VarChar).Value = Obj.TipoDocumento;
                Comando.Parameters.Add("@num_documento", SqlDbType.VarChar).Value = Obj.NumDocumento;
                Comando.Parameters.Add("@direccion", SqlDbType.VarChar).Value = Obj.Direccion;
                Comando.Parameters.Add("@telefono", SqlDbType.VarChar).Value = Obj.Telefono;
                Comando.Parameters.Add("@email", SqlDbType.VarChar).Value = Obj.Email;
                Comando.Parameters.Add("@clave", SqlDbType.VarChar).Value = Obj.Clave;
                //Abrimos la Conexion
                SqlCon.Open();
                //Ejecutamos el SP, usamos el If Ternario, para indicar si se guardo o no
                Rpta = Comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo guardar el Registro";
            }
            catch (Exception ex)
            {
                Rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
            return Rpta;

        }
        public string Actualizar(Usuario Obj)
        {
            string Rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon = Conexion.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("usuario_actualizar", SqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                //Indicamos que el SP espera parametros Nombre y Descripcion para realizar la consulta, tipo Varchar, Este value llega de la capa Negocios
                Comando.Parameters.Add("@idusuario", SqlDbType.Int).Value = Obj.IdUsuario;
                Comando.Parameters.Add("@idrol", SqlDbType.Int).Value = Obj.IdRol;
                Comando.Parameters.Add("@nombre", SqlDbType.VarChar).Value = Obj.Nombre;
                Comando.Parameters.Add("@tipo_documento", SqlDbType.VarChar).Value = Obj.TipoDocumento;
                Comando.Parameters.Add("@num_documento", SqlDbType.VarChar).Value = Obj.NumDocumento;
                Comando.Parameters.Add("@direccion", SqlDbType.VarChar).Value = Obj.Direccion;
                Comando.Parameters.Add("@telefono", SqlDbType.VarChar).Value = Obj.Telefono;
                Comando.Parameters.Add("@email", SqlDbType.VarChar).Value = Obj.Email;
                Comando.Parameters.Add("@clave", SqlDbType.VarChar).Value = Obj.Clave;
                //Abrimos la Conexion
                SqlCon.Open();
                //Ejecutamos el SP, usamos el If Ternario, para indicar si se actualizo o no
                Rpta = Comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo actualizar el Registro";
            }
            catch (Exception ex)
            {
                Rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
            return Rpta;
        }
        public string Eliminar(int Id)
        {
            string Rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon = Conexion.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("usuario_eliminar", SqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                //Indicamos que el SP espera parametros Nombre y Descripcion para realizar la consulta, tipo Varchar, Este value llega de la capa Negocios
                Comando.Parameters.Add("@idusuario", SqlDbType.Int).Value = Id;
                //Abrimos la Conexion
                SqlCon.Open();
                //Ejecutamos el SP, usamos el If Ternario, para indicar si se guardo o no
                Rpta = Comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo eliminar el Registro";
            }
            catch (Exception ex)
            {
                Rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
            return Rpta;
        }
        public string Activar(int Id)
        {
            string Rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon = Conexion.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("usuario_activar", SqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                //Indicamos que el SP espera parametros Nombre y Descripcion para realizar la consulta, tipo Varchar, Este value llega de la capa Negocios
                Comando.Parameters.Add("@idusuario", SqlDbType.Int).Value = Id;
                //Abrimos la Conexion
                SqlCon.Open();
                //Ejecutamos el SP, usamos el If Ternario, para indicar si se guardo o no
                Rpta = Comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo activar el Registro";
            }
            catch (Exception ex)
            {
                Rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
            return Rpta;
        }
        public string Desactivar(int Id)
        {
            string Rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon = Conexion.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("usuario_desactivar", SqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                //Indicamos que el SP espera parametros Nombre y Descripcion para realizar la consulta, tipo Varchar, Este value llega de la capa Negocios
                Comando.Parameters.Add("@idusuario", SqlDbType.Int).Value = Id;
                //Abrimos la Conexion
                SqlCon.Open();
                //Ejecutamos el SP, usamos el If Ternario, para indicar si se guardo o no
                Rpta = Comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo Desactivar el Registro";
            }
            catch (Exception ex)
            {
                Rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
            return Rpta;
        }

    }
}
