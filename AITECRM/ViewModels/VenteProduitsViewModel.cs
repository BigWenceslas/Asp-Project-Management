using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AITECRM.ViewModels
{
    public class VenteProduitsViewModel
    {
        public int VenteId { get; set; }
        public string NomClient { get; set; }
        public string NomEntreprise { get; set; }
        public string NomCategorie { get; set; }
        public string NomProduit { get; set; }
        public string DescriptionProduit { get; set; }
        public int Prix { get; set; }
        public DateTime DateVente { get; set; }
    }
}