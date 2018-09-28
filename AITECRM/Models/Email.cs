using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AITECRM.Models
{
    public class Email
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Destinataire")]
        public int Addresse_ { get; set; }
        public virtual Contact Adresse_Mail { get; set; }

        [Display (Name = "Objet")]
        public string Message_object { get; set; }
        [Display(Name = "Message")]
        public string Message { get; set; }
        [Display(Name = "Date d'Envoie")]
        public DateTime Date_envoie { get; set; }
        public Email()
        {
            Adresse_Mail = null;
        }




    }
}