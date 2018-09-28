using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AITECRM.Models
{
    public class Activite
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Etat { get; set; }
        public string Description { get; set; }

        public int Categorie_ { get; set; }
        public virtual CategorieActivite Categorie { get; set; }

        public int Contact_ { get; set; }
        public Contact Contact { get; set; }
        [Display(Name = "Date de Creation")]
        public DateTime DacteCreation { get; set; }
        public int Prix { get; set; }
        public virtual ICollection<FileDetailsActivite> FileDetails { get; set; }

        public Activite() {
            Categorie = null;
            FileDetails = null;
            Contact = null;
        }
    }
    
}