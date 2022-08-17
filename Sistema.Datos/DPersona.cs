using System;
using System.Data.SqlClient;
using System.Data;
using Sistema.Entidades;

namespace Sistema.Datos
{
    public class DPersona
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
                SqlCommand Comando = new SqlCommand("persona_listar", SqlCon);
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
        public DataTable ListarProveedores()
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                //Instanciamos la conexion
                SqlCon = Conexion.getInstancia().CrearConexion();
                //Llamamos el USP Categoria Listar
                SqlCommand Comando = new SqlCommand("persona_listar_proveedores", SqlCon);
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
        public DataTable ListarClientes()
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                //Instanciamos la conexion
                SqlCon = Conexion.getInstancia().CrearConexion();
                //Llamamos el USP Categoria Listar
                SqlCommand Comando = new SqlCommand("persona_listar_clientes", SqlCon);
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
                SqlCommand Comando = new SqlCommand("persona_buscar", SqlCon);
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
        public DataTable BuscarProveedores(string Valor)
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                //Instanciamos la conexion
                SqlCon = Conexion.getInstancia().CrearConexion();
                //Llamamos el USP Categoria Listar
                SqlCommand Comando = new SqlCommand("persona_buscar_proveedores", SqlCon);
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
        public DataTable BuscarClientes(string Valor)
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                //Instanciamos la conexion
                SqlCon = Conexion.getInstancia().CrearConexion();
                //Llamamos el USP Categoria Listar
                SqlCommand Comando = new SqlCommand("persona_buscar_clientes", SqlCon);
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
        public string Existe(string Valor)
        {
            string Rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon = Conexion.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("persona_existe", SqlCon);
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
        public string Insertar(Persona Obj)
        {
            string Rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon = Conexion.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("persona_insertar", SqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                //Indicamos que el SP espera parametros Nombre y Descripcion para realizar la consulta, tipo Varchar, Este value llega de la capa Negocios
                Comando.Parameters.Add("@tipo_persona", SqlDbType.VarChar).Value = Obj.TipoPersona;
                Comando.Parameters.Add("@nombre", SqlDbType.VarChar).Value = Obj.Nombre;
                Comando.Parameters.Add("@tipo_documento", SqlDbType.VarChar).Value = Obj.TipoDocumento;
                Comando.Parameters.Add("@num_documento", SqlDbType.VarChar).Value = Obj.NumDocumento;
                Comando.Parameters.Add("@direccion", SqlDbType.VarChar).Value = Obj.Direccion;
                Comando.Parameters.Add("@telefono", SqlDbType.VarChar).Value = Obj.Telefono;
                Comando.Parameters.Add("@email", SqlDbType.VarChar).Value = Obj.Email;
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
        public string Actualizar(Persona Obj)
        {
            string Rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon = Conexion.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("persona_actualizar", SqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                //Indicamos que el SP espera parametros Nombre y Descripcion para realizar la consulta, tipo Varchar, Este value llega de la capa Negocios
                Comando.Parameters.Add("@idpersona", SqlDbType.Int).Value = Obj.IdPersona;
                Comando.Parameters.Add("@tipo_persona", SqlDbType.VarChar).Value = Obj.TipoPersona;
                Comando.Parameters.Add("@nombre", SqlDbType.VarChar).Value = Obj.Nombre;
                Comando.Parameters.Add("@tipo_documento", SqlDbType.VarChar).Value = Obj.TipoDocumento;
                Comando.Parameters.Add("@num_documento", SqlDbType.VarChar).Value = Obj.NumDocumento;
                Comando.Parameters.Add("@direccion", SqlDbType.VarChar).Value = Obj.Direccion;
                Comando.Parameters.Add("@telefono", SqlDbType.VarChar).Value = Obj.Telefono;
                Comando.Parameters.Add("@email", SqlDbType.VarChar).Value = Obj.Email;
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
        public string Eliminar(int Id)
        {
            string Rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon = Conexion.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("persona_eliminar", SqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                //Indicamos que el SP espera parametros Nombre y Descripcion para realizar la consulta, tipo Varchar, Este value llega de la capa Negocios
                Comando.Parameters.Add("@idpersona", SqlDbType.Int).Value = Id;
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
        
    }
}
