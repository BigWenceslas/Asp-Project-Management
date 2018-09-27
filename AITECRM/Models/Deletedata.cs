using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AITECRM.Models
{
    public class Deletedata:IDisposable
    {
        private AITECRMContext db = new AITECRMContext();
       

        public void  deleteEntreprisedata(int id) { }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}