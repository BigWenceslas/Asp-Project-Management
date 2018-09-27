using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AITECRM.ViewModels
{
    public class AnalyseDonneesViewModel
    {

        public int Indicateur1 { get; set; }
        public int Indicateur2 { get; set; }
        public int Indicateur3 { get; set; }
        public int Indicateur4 { get; set; }
        public int Indicateur5 { get; set; }
        //Entreprises
        public Array EntrepriseNombre { get; set; }
        //Activites

        public Array ChiffreAffaire { get; set; }
       public Array TauxExecution { get; set; }
        public Array NombreActivites { get; set; }
        //Contacts


        public Array NombreTotalContactCategorie { get; set; }
        public Array NombreTotalContactMois { get; set; }


        //SMS

        public Array NombreTotalSmsMois { get; set; }

        //EMAILS
        public Array NombreTotalEmailsMois { get; set; }

        //VentesProduits
        public Array NombreTotalAchatTypeProduit { get; set; }
        public Array ChiffreAffaireTypeProduit { get; set; }
    }
}