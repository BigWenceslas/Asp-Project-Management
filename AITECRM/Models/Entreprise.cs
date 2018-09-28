using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AITECRM.Models
{
    public class Entreprise
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Le nom de l'entreprise est requis")]
        [Display(Name = "Entreprise")]
        public string Nom { get; set; }
        public string Adresse { get; set; }
        public string Pays { get; set; }

        [Display(Name = "Code postal")]
        public string Code_postal { get; set; }
        [Display(Name = "Site Web")]
        public string Site_web { get; set; }

        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$",
            ErrorMessage = "Le format de l'adresse email n'est pas correct")]
        [Required(ErrorMessage = "L'adresse Email est obligatoire")]
        [Display(Name = "Adresse Email")]
        public string Adresse_Email { get; set; }
        [Index(IsUnique = true)]
        [Required(ErrorMessage = "Le numero de Telephone est requis")]
        [RegularExpression(@"(^6[0-9]{8}$|^00[0-9]{11,13}$)", ErrorMessage = "Le format du numero de telephone n'est pas correct")]
        public int Telephone { get; set; }
        [Display(Name = "Secteur d'activites")]
        public string Description { get; set; }
        [Display(Name = "Date d'Enregistrement")]
        public DateTime Date_enregistrement { get; set; }

        public virtual List<Contact> Contact { get; set; }
        public Entreprise()
        {
            Contact = null;
        }
    }
}