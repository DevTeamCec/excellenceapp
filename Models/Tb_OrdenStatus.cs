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
    
    public partial class Tb_OrdenStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tb_OrdenStatus()
        {
            this.Tb_OrdenCompra = new HashSet<Tb_OrdenCompra>();
        }
    
        public System.Guid Lng_IdOrdenStatus { get; set; }
        public string Txt_Status { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tb_OrdenCompra> Tb_OrdenCompra { get; set; }
    }
}
