using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AITECRM.ViewModels
{
    public class AllContactViewModel
    {
        //columns from Entreprise Table
        public string NomEntreprise { get; set; }
        //Columns from Contact table
        public int Idc { get; set; }
        public string NomContact { get; set; }
        public DateTime DateEnregistrement { get; set; }
        public DateTime DateNaissance { get; set; }
        public string AdresseMail { get; set; }
        //Columns from GroupContact table
        public string NomGroupeContact { get; set; }
        //Columns from Numero_telephone table
        public int Numero { get; set; }
        //Colums from EtatContact Table

        public int NombreActivites { get; set; }
        public int NombreVentes { get; set; }
        public string EtatContacts_ { get; set; }

    }
}