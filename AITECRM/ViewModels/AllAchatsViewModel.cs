using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AITECRM.ViewModels
{
    public class AllAchatsViewModel
    {
        public string NomEntreprise { get; set; }
        public string NomProjet { get; set; }
        public string NomAcheteur { get; set; }
        public string TypeAcheteur { get; set; }
        public int Cout { get; set; }
        public DateTime Date_achat { get; set; }
        public int IdAchat { get; set; }
    }
}