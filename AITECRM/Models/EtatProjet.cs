using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AITECRM.Models
{
    public class EtatProjet
    {
        public int Id { get; set; }
        [Display(Name = "Intitule")]
        public string NomEtat { get; set; }
        public string Description { get; set; }
        public virtual List<Projet> Projets { get; set; }
        public EtatProjet()
        {
            Projets = null;
        }
    }
}