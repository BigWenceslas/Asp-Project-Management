using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AITECRM.Models
{
    public class AITECRMContext:DbContext
    {
        public AITECRMContext(): base("DefaultConnection"){ }

        public DbSet<Entreprise> Entreprises { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactGroup> ContactGroup { get; set; }
        public DbSet<Projet> Projets { get; set; }
        public DbSet<Tache> Taches { get; set; }
        public DbSet<Achat> Achats { get; set; }
        public DbSet<EtatContact> EtatContacts { get; set; }
        public DbSet<Activite> Activites { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<FileDetailsActivite> FileDetailsActivites { get; set; }
        public DbSet<FileDetailsProjet> FileDetailsProjets { get; set; }
        public DbSet<CategorieArticle> CategorieProduits { get; set; }
        public DbSet<CategorieActivite> CategorieActivites { get; set; }
        public DbSet<CategorieArticleParente> CategorieAPar { get; set; }
        public DbSet<CategorieActiviteParente> CategorieActPar { get; set; }
        public DbSet<VenteArticle> VenteArticles { get; set; }
        public DbSet<EmailServeur> EmailServeurs { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<EmailEntreprise> EmailEntreprises { get; set; }
        public DbSet<ServeurSms> ServeurSmss { get; set; }
        public DbSet<Sms> Smss { get; set; }
        public DbSet<SmsEntreprise> SmsEntreprises { get; set; }
    }
}