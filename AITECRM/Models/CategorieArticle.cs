using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AITECRM.Models
{
    public class CategorieArticle
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        [Required]
        public int CategorieParente_ { get; set; }
        public virtual CategorieArticleParente CategorieAr { get; set; }

        public virtual List<Article> Articles { get; set; }
        public CategorieArticle()
        {
            Articles = null;
        }
    }
}