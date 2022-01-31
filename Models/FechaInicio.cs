using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Curso.Models
{
    public class FechaInicio
    {
        public FechaInicio()
        {
            this.Fec_Inicio = DateTime.Now;
        }

        public DateTime Fec_Inicio { get; set; }
    }
}