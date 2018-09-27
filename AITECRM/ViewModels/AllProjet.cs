using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AITECRM.ViewModels
{
    public class AllProjet
    {
        public int Id { get; set; }
        public string Commanditaire { get; set; }
        public string NomProjet { get; set; }
        public string EtatProjet { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public int Cout { get; set; }
        public int RentabiliteMoyenne { get; set; }
    }
}