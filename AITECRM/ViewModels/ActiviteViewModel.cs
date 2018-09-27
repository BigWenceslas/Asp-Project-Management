using AITECRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AITECRM.ViewModels
{
    public class ActiviteViewModel
    {
        //ACTIVITES
        public int ActiviteId { get; set; }

        public string NomActivite { get; set; }
        public string EtatActivite { get; set; }
        public string Description { get; set; }
        public string CategorieActivite { get; set; }
        public DateTime DateCreation { get; set; }
        public int NombreFichiers { get; set; }
        public int Prix { get; set; }

        public virtual ICollection<FileDetailsActivite> FileDetailsVM { get; set; }

    //Contacts

    public string NomContact { get; set; }
        public int Numero { get; set; }
        public string AdresseEmail { get; set; }
        public int ContactId { get; set; }
        public string pays { get; set; }
        //Entreprise
        public string NomEntreprise { get; set; }
        public int NumEntreprise { get; set; }
        public string AdresseEmailEntreprise { get; set; }
        //Categorie Activite
        



    }
}