using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Entidades
{
    public class Persona
    {
        public int IdPersona { get; set; }
        public string TipoPersona { get; set; }
        public string Nombre { get; set; }
        public string TipoDocumento{ get; set; }
        public string NumDocumento { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
    }
}

//--Procecimiento Listar
//create proc persona_listar
//as
//select idpersona as Id, tipo_persona as Tipo_Persona, nombre as Nombre, tipo_documento as Tipo_Documento, num_documento as Num_Documento
//,direccion as Direccion, telefono as Telefono, email as Email from persona
//order by idpersona desc
//go
//--Procecimiento Listar Proveedores
//create proc persona_listar_proveedores
//as
//select idpersona as Id, tipo_persona as Tipo_Persona, nombre as Nombre, tipo_documento as Tipo_Documento, num_documento as Num_Documento
//,direccion as Direccion, telefono as Telefono, email as Email from persona
//where tipo_persona = 'Proveedor'
//order by idpersona desc
//go
//--Procecimiento Listar Clientes
//create proc persona_listar_clientes
//as
//select idpersona as Id, tipo_persona as Tipo_Persona, nombre as Nombre, tipo_documento as Tipo_Documento, num_documento as Num_Documento
//,direccion as Direccion, telefono as Telefono, email as Email from persona
//where tipo_persona = 'Cliente'
//order by idpersona desc
//go
//--Procecimiento Buscar
//create proc persona_buscar
//@valor varchar(50)
//as
//select idpersona as Id, tipo_persona as Tipo_Persona, nombre as Nombre, tipo_documento as Tipo_Documento, num_documento as Num_Documento
//,direccion as Direccion, telefono as Telefono, email as Email from persona
//where nombre like '%' + @valor + '%' Or email like '%' + @valor+'%'
//order by idpersona desc
//go