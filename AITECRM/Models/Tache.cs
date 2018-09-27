using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AITECRM.Models
{
    public class Tache
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Responsable")]
        public int Contact_ { get; set; }
        public virtual Contact Contact { get; set; }


        [Display(Name = "Project")]
        public int Projet_ { get; set; }
        public virtual Projet Projet { get; set; }


        [Display(Name = "Task")]
        public string Intitule { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Type de tache")]
        public string Type_tache { get; set; }
        public int Cout { get; set; }
        [Display(Name = "Date de Debut")]
        public DateTime Date_debut { get; set; }
        [Display(Name = "Date de Fin")]
        public DateTime Date_fin { get; set; }
        public Tache()
        {
            Projet = null;
            Contact = null;
        }
        

    }
}