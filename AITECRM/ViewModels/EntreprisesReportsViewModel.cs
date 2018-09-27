using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AITECRM.ViewModels
{
    public class EntreprisesReportsViewModel
    {
        public string Nom { get; set; }
        public string Adresse { get; set; }
        public string Pays { get; set; }
        
        public string Code_postal { get; set; }
        public string Site_web { get; set; }

        public string Adresse_Email { get; set; }

        public int Telephone { get; set; }

        public string Description { get; set; }

        public DateTime Date_enregistrement { get; set; }
        public int NombreContacts { get; set; }
    }
}