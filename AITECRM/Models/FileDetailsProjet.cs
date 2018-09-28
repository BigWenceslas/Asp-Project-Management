using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AITECRM.Models
{
    public class FileDetailsProjet
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public int SupportId { get; set; }
        public virtual Projet Projet { get; set; }
        public FileDetailsProjet()
        {
            Projet = null;
        }
    }
}