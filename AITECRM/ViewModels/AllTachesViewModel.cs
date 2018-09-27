using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AITECRM.ViewModels
{
    public class AllTachesViewModel
    {
        public string NomEntreprise { get; set; }
        public string GroupeContact { get; set; }
        //colonne provenant de la table Contact
        public string NomContact { get; set; }
        //colonne provenant de la table Projet
        public string NomProjet { get; set; }

        //colonne provenant de la table Contact
        public int IdTache { get; set; }
        public string Intitule { get; set; }
        public string Type_tache { get; set; }
        public int Cout { get; set; }
        public DateTime Date_debut { get; set; }
        public DateTime Date_Fin { get; set; }
        public DateTime LancementProjet { get; set; }
        public DateTime ClotureProjet { get; set; }



    }
}