using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Sistema.Datos;
using System.Runtime.InteropServices.WindowsRuntime;
using Sistema.Entidades;

namespace Sistema.Negocio
{
    public class NIngreso
    {
        public static DataTable Listar()
        {
            DIngreso Datos = new DIngreso();
            return Datos.Listar(); 
        }
        public static DataTable Buscar(string Valor)
        {
            DIngreso Datos = new DIngreso();
            return Datos.Buscar(Valor);
        }
        public static String Insertar(int IdProveedor, int IdUsuario, string TipoComprobante,
            string SerieComprobante,string NumComprobante,decimal Impuesto, decimal Total, DataTable Detalles)
        {
            DIngreso Datos = new DIngreso();
            Ingreso Obj = new Ingreso();
            Obj.IdProveedor = IdProveedor;
            Obj.IdUsuario = IdUsuario;
            Obj.TipoComprobante = TipoComprobante;
            Obj.SerieComprobante= SerieComprobante;
            Obj.NumComprobante = NumComprobante;
            Obj.Impuesto = Impuesto;
            Obj.Total = Total;
            Obj.Detalles = Detalles;
            return Datos.Insertar(Obj);
        }
        public static string Anular(int Id)
        {
            DIngreso Datos = new DIngreso();
            return Datos.Anular(Id);
        }

    }
}
