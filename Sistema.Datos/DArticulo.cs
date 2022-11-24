using System;
using System.Data;
using System.Data.SqlClient;
using Sistema.Entidades;

namespace Sistema.Datos
{
    public class DArticulo
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
                SqlCommand Comando = new SqlCommand("articulo_listar", SqlCon);
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
                SqlCommand Comando = new SqlCommand("articulo_buscar", SqlCon);
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

        public DataTable BuscarCodigo(string Valor)
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                //Instanciamos la conexion
                SqlCon = Conexion.getInstancia().CrearConexion();
                //Llamamos el USP Categoria Listar
                SqlCommand Comando = new SqlCommand("articulo_buscar_codigo", SqlCon);
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
                SqlCommand Comando = new SqlCommand("articulo_existe", SqlCon);
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
        public string Insertar(Articulo Obj)
        {
            string Rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon = Conexion.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("articulo_insertar", SqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                //Indicamos que el SP espera parametros Nombre y Descripcion para realizar la consulta, tipo Varchar, Este value llega de la capa Negocios
                Comando.Parameters.Add("@idcategoria", SqlDbType.Int).Value = Obj.IdCategoria;
                Comando.Parameters.Add("@codigo", SqlDbType.VarChar).Value = Obj.Codigo;
                Comando.Parameters.Add("@nombre", SqlDbType.VarChar).Value = Obj.Nombre;
                Comando.Parameters.Add("@precio_venta", SqlDbType.Decimal).Value = Obj.PrecioVenta;
                Comando.Parameters.Add("@stock", SqlDbType.Int).Value = Obj.Stock;
                Comando.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = Obj.Descripcion;
                Comando.Parameters.Add("@imagen", SqlDbType.VarChar).Value = Obj.Imagen;
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
        public string Actualizar(Articulo Obj)
        {
            string Rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon = Conexion.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("articulo_actualizar", SqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                //Indicamos que el SP espera parametros Nombre y Descripcion para realizar la consulta, tipo Varchar, Este value llega de la capa Negocios
                Comando.Parameters.Add("@idarticulo", SqlDbType.Int).Value = Obj.IdArticulo;
                Comando.Parameters.Add("@idcategoria", SqlDbType.Int).Value = Obj.IdCategoria;
                Comando.Parameters.Add("@codigo", SqlDbType.VarChar).Value = Obj.Codigo;
                Comando.Parameters.Add("@nombre", SqlDbType.VarChar).Value = Obj.Nombre;
                Comando.Parameters.Add("@precio_venta", SqlDbType.Decimal).Value = Obj.PrecioVenta;
                Comando.Parameters.Add("@stock", SqlDbType.Int).Value = Obj.Stock;
                Comando.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = Obj.Descripcion;
                Comando.Parameters.Add("@imagen", SqlDbType.VarChar).Value = Obj.Imagen;
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
                SqlCommand Comando = new SqlCommand("articulo_eliminar", SqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                //Indicamos que el SP espera parametros Nombre y Descripcion para realizar la consulta, tipo Varchar, Este value llega de la capa Negocios
                Comando.Parameters.Add("@idarticulo", SqlDbType.Int).Value = Id;
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
                SqlCommand Comando = new SqlCommand("articulo_activar", SqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                //Indicamos que el SP espera parametros Nombre y Descripcion para realizar la consulta, tipo Varchar, Este value llega de la capa Negocios
                Comando.Parameters.Add("@idarticulo", SqlDbType.Int).Value = Id;
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
                SqlCommand Comando = new SqlCommand("articulo_desactivar", SqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                //Indicamos que el SP espera parametros Nombre y Descripcion para realizar la consulta, tipo Varchar, Este value llega de la capa Negocios
                Comando.Parameters.Add("@idarticulo", SqlDbType.Int).Value = Id;
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
