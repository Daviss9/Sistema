using System;
using System.Data.SqlClient;
using System.Data;
using Sistema.Entidades;

namespace Sistema.Datos
{
    public class DIngreso
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
                SqlCommand Comando = new SqlCommand("ingreso_listar", SqlCon);
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
                SqlCommand Comando = new SqlCommand("ingreso_buscar", SqlCon);
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
        public string Insertar(Ingreso Obj)
        {
            string Rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon = Conexion.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("ingreso_insertar", SqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                //Indicamos que el SP espera parametros Nombre y Descripcion para realizar la consulta, tipo Varchar, Este value llega de la capa Negocios
                Comando.Parameters.Add("@idproveedor", SqlDbType.Int).Value = Obj.IdProveedor;
                Comando.Parameters.Add("@idusuario", SqlDbType.Int).Value = Obj.IdUsuario;
                Comando.Parameters.Add("@tipo_comprobante", SqlDbType.VarChar).Value = Obj.TipoComprobante;
                Comando.Parameters.Add("@serie_comprobante", SqlDbType.VarChar).Value = Obj.SerieComprobante;
                Comando.Parameters.Add("@num_comprobante", SqlDbType.VarChar).Value = Obj.NumComprobante;
                Comando.Parameters.Add("@impuesto", SqlDbType.Decimal).Value = Obj.Impuesto;
                Comando.Parameters.Add("@total", SqlDbType.Decimal).Value = Obj.Total;
                Comando.Parameters.Add("@detalle", SqlDbType.Structured).Value = Obj.Detalles;
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

        public string Anular(int Id)
        {
            string Rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon = Conexion.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("ingreso_anular", SqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                //Indicamos que el SP espera parametros Nombre y Descripcion para realizar la consulta, tipo Varchar, Este value llega de la capa Negocios
                Comando.Parameters.Add("@idingreso", SqlDbType.Int).Value = Id;
                //Abrimos la Conexion
                SqlCon.Open();
                //Ejecutamos el SP, usamos el If Ternario, para indicar si se guardo o no
                Rpta = Comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo Anular el registro";
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
