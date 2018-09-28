using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AITECRM.Models
{
    public class Article
    {
        [Key]
        public int Idp { get; set; }
        public string Nom { get; set; }
        [Display(Name = "Description")]
        public string Descriptionp { get; set; }
        public int Prix { get; set; }
        [Display(Name = "Date d'Enregistrement")]
        public DateTime Date_Creation { get; set; }

        public virtual List<VenteArticle> Ventes { get; set; }

        public int CategorieProduit_ { get; set; }
        public virtual CategorieArticle CategorieProduit { get; set; }
        public Article()
        {
            CategorieProduit = null;
            Ventes = null;
        }
    }
}