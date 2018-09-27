using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AITECRM.Models
{
    public class ServeurSms
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="Url Serveur")]
        public string ServerAdress { get; set; }
        [Required]
        [Display(Name = "Nom d'utilisateur")]
        public string Username { get; set; }
        [Required]
        [Display(Name = "Mot de passe")]
        public string PasswordUser { get; set; }
        [Required]
        [Display(Name = "Identifiant d'envoi")]
        public string SenderId { get; set; }
        [Required]
        public int Port { get; set; }
        [Required]
        [Display(Name = "Etat")]
        public string EtatConfig { get; set; }

    }
}