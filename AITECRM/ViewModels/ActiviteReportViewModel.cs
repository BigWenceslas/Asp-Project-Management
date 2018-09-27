using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AITECRM.ViewModels
{
    public class ActiviteReportViewModel
    {
        //ACTIVITES
    
        public string NomActivite { get; set; }
        public string EtatActivite { get; set; }
        public string CategorieActivite { get; set; }
        public DateTime DateCreation { get; set; }
        public int Prix { get; set; }


        //Contacts
        public int Numerotel { get; set; }
        public string AdresseEmail { get; set; }
        public string NomContact { get; set; }
        //Entreprise
        public string NomEntreprise { get; set; }
        //Categorie Activite

    }
}