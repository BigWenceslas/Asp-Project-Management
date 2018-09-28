using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AITECRM.ViewModels
{
    public class ProduitViewModel
    {
        public int ProduitId { get; set; }
        public string NomCategorie { get; set; }
        public string NomProduit { get; set; }
        public string Description { get; set; }
        public int Prix { get; set; }
        public DateTime DateEnregistrement { get; set; }


    }
}