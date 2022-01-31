using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Curso.Models
{
    public class Activo
    {
        public Activo()
        {
            UsuarioLogin = new HashSet<UsuarioLogin>();
        }
        
        public int Bol_Activo { get; set; }
        [Required]
        public string Txt_Activo { get; set; }

        //PROPIEDAD DE NAVEGACION
        public ICollection<UsuarioLogin> UsuarioLogin { get; set; }
    }
}