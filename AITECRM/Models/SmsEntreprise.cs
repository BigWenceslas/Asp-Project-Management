using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AITECRM.Models
{
    public class SmsEntreprise
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Destinataire")]
        public int Telephone_ { get; set; }
        public virtual Entreprise Telephone { get; set; }

        [Display(Name = "Message")]
        public string Message { get; set; }
        public DateTime Date_envoie { get; set; }
        public SmsEntreprise()
        {
            Telephone = null;
        }
    }
}