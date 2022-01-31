using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Curso.Models
{
    public class Plaza
    {
        public Plaza()
        {
            UsuarioLogin = new HashSet<UsuarioLogin>();
        }

        public int Int_IdPlaza { get; set; }
        [Required]
        public string Txt_Plaza { get; set; }

        //PROPIEDAD DE NAVEGACION
        public ICollection<UsuarioLogin> UsuarioLogin { get; set; }
    }
}
