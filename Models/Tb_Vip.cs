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
    
    public partial class Tb_Vip
    {
        public System.Guid Lng_IdVIP { get; set; }
        public Nullable<bool> Bol_Activo { get; set; }
        public string Txt_Nombre { get; set; }
        public string Txt_EMail { get; set; }
        public string Txt_Telefono { get; set; }
        public string Txt_Codigo { get; set; }
        public Nullable<int> Int_IdStatus { get; set; }
        public Nullable<int> Num_ContadorQR { get; set; }
        public Nullable<bool> Bol_Status { get; set; }
        public Nullable<System.Guid> Lng_IdVipCon { get; set; }
        public Nullable<int> Int_tipo { get; set; }
    }
}
