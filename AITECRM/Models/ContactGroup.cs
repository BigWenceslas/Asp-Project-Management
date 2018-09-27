using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AITECRM.Models
{
    public class ContactGroup
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Contact Type")]
        public string Type_name { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }

        public virtual List<Contact> listContact { get; set; }
        public ContactGroup()
        {
            listContact = null;
        }
    }
}