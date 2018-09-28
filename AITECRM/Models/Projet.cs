using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AITECRM.Models
{
    public class Projet
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Project Name")]
        public string Titre { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        public int Cout { get; set; }
        [Display(Name = "Date de lancement")]
        public DateTime Date_debut { get; set; }
        [Display(Name = "Date de Cloture")]
        public DateTime Date_fin { get; set; }
        public string EtatProjet_ { get; set; }

        public virtual List<Achat> listAchat { get; set; }
        public virtual List<Tache> listTaches { get; set; }

        public virtual List<FileDetailsProjet> FileDetails { get; set; }
        public Projet()
        {
            listAchat = null;
            listTaches = null;
            FileDetails = null;
        }
    }
}