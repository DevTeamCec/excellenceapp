using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Curso.Models
{
    public class usuarioslista
    {
        public int Int_IdUsuario { get; set; }
        public string Txt_Usuario { get; set; }
        public string Txt_Password { get; set; }
        public string Txt_Email { get; set; }
        public int Int_IdPlaza { get; set; }
        public string Txt_Plaza { get; set; }
        public int Int_IdNivel { get; set; }
        public string Txt_Nivel { get; set; }
        public string Fec_Inicio { get; set; }
        public string Fec_Fin { get; set; }
        public int Int_IdGerente { get; set;}
        public string Txt_Gerente { get; set; }
        public int Bol_Activo { get; set; }
    }
}