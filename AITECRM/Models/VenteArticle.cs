using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AITECRM.Models
{
    public class VenteArticle
    {
        public int Id { get; set; }
        [Display(Name = "Contact")]

        public int Contact_ { get; set; }
        public virtual Contact Contact { get; set; }
        [Display(Name = "Article")]

        public int Article_ { get; set; }
        public virtual Article Article { get; set; }

        public DateTime DateAchat { get; set; }
        public VenteArticle()
        {
            Contact = null;
            Article = null;
        }
    }
}