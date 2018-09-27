using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AITECRM.Models
{
    public class Achat
    {
        [Key]
        public int Id { get; set; }


        [Display(Name = "Projet")]
        public int Projet_ { get; set; }
        public Projet Projet { get; set; }
        /**    **/

        [Display(Name = "Nom Contact")]
        public int Contact_ { get; set; }
        public Contact Contact { get; set; }
        /**   **/

        [Display(Name = "Prix")]
        public int Cout { get; set; }

        [Display(Name = "Date")]
        public DateTime Date_Achat { get; set; }

        public Achat()
        {
            Contact = null;
            Projet = null;
        }
    }
}