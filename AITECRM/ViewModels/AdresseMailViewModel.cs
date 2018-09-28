using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AITECRM.Models;

namespace AITECRM.ViewModels
{
    public class AdresseMailViewModel
    {
      
        public string AdressesContacts { get; set; }
        public int ContactId { get; set; }
        public string NomContact { get; set; }
        public List<AdresseMailViewModel> ListAdresses { get; set; }

    }
}