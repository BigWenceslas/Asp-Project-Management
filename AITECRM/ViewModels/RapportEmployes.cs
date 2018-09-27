using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AITECRM.ViewModels
{
    public class RapportEmployes
    {
        public int IdContact { get; set; }
        public string NomResponsable { get; set; }
        public string Categorie { get; set; }
        public DateTime TempsMoyenRestant { get; set; }
        public int NombreTachesExecutes { get; set; }
        public int NombreDeProjets { get; set; }

    }
}