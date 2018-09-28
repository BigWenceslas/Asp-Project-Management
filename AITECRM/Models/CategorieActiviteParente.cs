using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AITECRM.Models
{
    public class CategorieActiviteParente
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public virtual List<CategorieActivite> CategorieActivite { get; set; }
        public CategorieActiviteParente()
        {
            CategorieActivite = null;
        }
    }
}