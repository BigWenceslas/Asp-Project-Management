using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AITECRM.Models
{
    public class Sms
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Destinataire")]
        public int Telephone_ { get; set; }
        public virtual Contact Telephone { get; set; }

        [Display(Name = "Message")]
        public string Message { get; set; }
        public DateTime Date_envoie { get; set; }
        public Sms()
        {
            Telephone = null;
        }

    }
}