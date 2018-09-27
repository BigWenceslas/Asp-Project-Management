using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AITECRM.Models
{
    public class CategorieArticleParente
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public virtual List<CategorieArticle> CategorieArticle { get; set; }
        public CategorieArticleParente()
        {
            CategorieArticle = null;
        }
    }
}