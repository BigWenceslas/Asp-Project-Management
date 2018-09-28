using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AITECRM.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Le nom du contact est requis")]
        [Display(Name = "Nom Contact")]
        public string Contact_name { get; set; }
        [Display(Name = "Poste Occupe")]
        public string Description { get; set; }
        public string Adresse { get; set; }
        public string Civilite { get; set; }
        public string Pays { get; set; }
        [Display(Name = "Code Postal")]
        public string Code_postal { get; set; }

        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$",
            ErrorMessage = "Le format de l'adresse email n'est pas correct")]
        [Required(ErrorMessage = "L'adresse Email est obligatoire")]
        [Display(Name = "Adresse Email")]
        public string Adresse_mail { get; set; }
        [Required(ErrorMessage = "Le numero de Telephone est requis")]
        [Index(IsUnique = true)]
        public int Telephone { get; set; }

        [Display(Name = "Date d'Enregistrement")]
        public DateTime Date_enregistrement { get; set; }
        [Display(Name = "Date de naissance")]
        public DateTime Date_naissance { get; set; }


        [Display(Name = "Entreprise")]
        public int Entreprise_ { get; set; }
        public Entreprise Entreprise { get; set; }
        [Required]
        [Display(Name = "Type du Contact")]
        public int Contactgroups { get; set; }
        public virtual ContactGroup Contactgroup { get; set; }
        public virtual List<Tache> Taches { get; set; }

        public virtual List<Activite> Activites { get; set; }
        public virtual List<VenteArticle> Ventes { get; set; }
        public virtual List<Email> List_Email { get; set; }
        public virtual List<Achat> list_Achat { get; set; }
        public Contact()
        {
            Contactgroup = null;
            Entreprise = null;
            Taches = null;
            Activites = null;
            Ventes = null;
            List_Email = null;
            list_Achat = null;

        }
    }
}