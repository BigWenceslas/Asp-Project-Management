using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AITECRM.Models
{
    public class EtatContact
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="L'intitule est requis")]
        public string Intitule { get; set; }
        public string Description { get; set; }
        [Display(Name = "Date")]
        public DateTime DateVariation { get; set; }

        public int ContactId { get; set; }
        public virtual Contact Contact { get; set; }
        public EtatContact()
        {
            Contact = null;
        }
    }
}