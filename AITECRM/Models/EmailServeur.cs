using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AITECRM.Models
{
    public class EmailServeur
    {
        public int Id { get; set; }
        [Required]
        public int Port { get; set; }
        public DateTime DateAjout { get; set; }
        [Required]
        public string ServeurAdresse { get; set; }
        [Required]
        public string AdresseCRM { get; set; }
        [Required]
        public string PassWordCRM { get; set; }
        [Required]
        public string EtatConfig { get; set; }
    }
}