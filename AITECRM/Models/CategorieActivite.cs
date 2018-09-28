using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AITECRM.Models
{
    public class CategorieActivite
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        [Required]
        public int CategorieParente_ { get; set; }
        public virtual CategorieActiviteParente CategoriePar { get; set; }
        public virtual List<Activite> Activites { get; set; }
        public CategorieActivite()
        {
            Activites = null;
        }
    }
}