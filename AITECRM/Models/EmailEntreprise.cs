using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AITECRM.Models
{
    public class EmailEntreprise
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Destinataire")]
        public int Addresse_ { get; set; }
        public virtual Entreprise Adresse_Mail { get; set; }

        [Display(Name = "Objet")]
        public string Message_object { get; set; }
        [Display(Name = "Message")]
        public string Message { get; set; }
        [Display(Name = "Date d'Envoie")]
        public DateTime Date_envoie { get; set; }
        public EmailEntreprise()
        {
            Adresse_Mail = null;
        }
    }
}