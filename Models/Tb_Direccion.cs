//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Curso.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tb_Direccion
    {
        public System.Guid Lng_IdDireccion { get; set; }
        public string Txt_Calle { get; set; }
        public string Txt_Colonia { get; set; }
        public string Txt_Ciudad { get; set; }
        public string Txt_Estado { get; set; }
        public string Txt_Pais { get; set; }
        public string Txt_CodigoPostal { get; set; }
        public Nullable<System.Guid> Lng_IdUsuario { get; set; }
    }
}
