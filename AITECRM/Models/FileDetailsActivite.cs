using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AITECRM.Models
{
    public class FileDetailsActivite
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public int SupportId { get; set; }
        public virtual Activite Activite { get; set; }
        public FileDetailsActivite()
        {
            Activite = null;
        }
    }
}