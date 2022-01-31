using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Curso.Models
{
    public class Nivel
    {
        public Nivel()
        {
            UsuarioLogin = new HashSet<UsuarioLogin>();
        }

        public int Int_IdNivel { get; set; }
        [Required]
        public string Txt_Nivel { get; set; }

        //PROPIEDAD DE NAVEGACION
        public ICollection<UsuarioLogin> UsuarioLogin { get; set; }
    }
}
