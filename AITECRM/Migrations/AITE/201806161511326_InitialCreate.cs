namespace AITECRM.Migrations.AITE
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Achats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Projet_ = c.Int(nullable: false),
                        Contact_ = c.Int(nullable: false),
                        Cout = c.Int(nullable: false),
                        Date_Achat = c.DateTime(nullable: false),
                        Contact_Id = c.Int(),
                        Projet_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contacts", t => t.Contact_Id)
                .ForeignKey("dbo.Projets", t => t.Projet_Id)
                .Index(t => t.Contact_Id)
                .Index(t => t.Projet_Id);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Contact_name = c.String(nullable: false),
                        Description = c.String(),
                        Adresse = c.String(),
                        Civilite = c.String(),
                        Pays = c.String(),
                        Code_postal = c.String(),
                        Adresse_mail = c.String(nullable: false),
                        Telephone = c.Int(nullable: false),
                        Date_enregistrement = c.DateTime(nullable: false),
                        Date_naissance = c.DateTime(nullable: false),
                        Entreprise_ = c.Int(nullable: false),
                        Contactgroups = c.Int(nullable: false),
                        Contactgroup_Id = c.Int(),
                        Entreprise_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContactGroups", t => t.Contactgroup_Id)
                .ForeignKey("dbo.Entreprises", t => t.Entreprise_Id)
                .Index(t => t.Telephone, unique: true)
                .Index(t => t.Contactgroup_Id)
                .Index(t => t.Entreprise_Id);
            
            CreateTable(
                "dbo.Activites",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Etat = c.String(),
                        Description = c.String(),
                        Categorie_ = c.Int(nullable: false),
                        Contact_ = c.Int(nullable: false),
                        DacteCreation = c.DateTime(nullable: false),
                        Prix = c.Int(nullable: false),
                        Categorie_Id = c.Int(),
                        Contact_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CategorieActivites", t => t.Categorie_Id)
                .ForeignKey("dbo.Contacts", t => t.Contact_Id)
                .Index(t => t.Categorie_Id)
                .Index(t => t.Contact_Id);
            
            CreateTable(
                "dbo.CategorieActivites",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FileDetailsActivites",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FileName = c.String(),
                        Extension = c.String(),
                        SupportId = c.Int(nullable: false),
                        Activite_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Activites", t => t.Activite_Id)
                .Index(t => t.Activite_Id);
            
            CreateTable(
                "dbo.ContactGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type_name = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Entreprises",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(nullable: false),
                        Adresse = c.String(),
                        Pays = c.String(),
                        Code_postal = c.String(),
                        Site_web = c.String(),
                        Adresse_Email = c.String(nullable: false),
                        Telephone = c.Int(nullable: false),
                        Description = c.String(),
                        Date_enregistrement = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Telephone, unique: true);
            
            CreateTable(
                "dbo.Emails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Addresse_ = c.Int(nullable: false),
                        Message_object = c.String(),
                        Message = c.String(),
                        Date_envoie = c.DateTime(nullable: false),
                        Adresse_Mail_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contacts", t => t.Adresse_Mail_Id)
                .Index(t => t.Adresse_Mail_Id);
            
            CreateTable(
                "dbo.Taches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Contact_ = c.Int(nullable: false),
                        Projet_ = c.Int(nullable: false),
                        Intitule = c.String(),
                        Description = c.String(),
                        Type_tache = c.String(),
                        Cout = c.Int(nullable: false),
                        Date_debut = c.DateTime(nullable: false),
                        Date_fin = c.DateTime(nullable: false),
                        Contact_Id = c.Int(),
                        Projet_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contacts", t => t.Contact_Id)
                .ForeignKey("dbo.Projets", t => t.Projet_Id)
                .Index(t => t.Contact_Id)
                .Index(t => t.Projet_Id);
            
            CreateTable(
                "dbo.Projets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titre = c.String(nullable: false),
                        Description = c.String(),
                        Cout = c.Int(nullable: false),
                        Date_debut = c.DateTime(nullable: false),
                        Date_fin = c.DateTime(nullable: false),
                        EtatProjet_ = c.Int(nullable: false),
                        EtatProjet_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EtatProjets", t => t.EtatProjet_Id)
                .Index(t => t.EtatProjet_Id);
            
            CreateTable(
                "dbo.EtatProjets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NomEtat = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FileDetailsProjets",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FileName = c.String(),
                        Extension = c.String(),
                        SupportId = c.Int(nullable: false),
                        Projet_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projets", t => t.Projet_Id)
                .Index(t => t.Projet_Id);
            
            CreateTable(
                "dbo.VenteArticles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Contact_ = c.Int(nullable: false),
                        Article_ = c.Int(nullable: false),
                        DateAchat = c.DateTime(nullable: false),
                        Article_Idp = c.Int(),
                        Contact_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.Article_Idp)
                .ForeignKey("dbo.Contacts", t => t.Contact_Id)
                .Index(t => t.Article_Idp)
                .Index(t => t.Contact_Id);
            
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Idp = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Descriptionp = c.String(),
                        Prix = c.Int(nullable: false),
                        Date_Creation = c.DateTime(nullable: false),
                        CategorieProduit_ = c.Int(nullable: false),
                        CategorieProduit_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Idp)
                .ForeignKey("dbo.CategorieArticles", t => t.CategorieProduit_Id)
                .Index(t => t.CategorieProduit_Id);
            
            CreateTable(
                "dbo.CategorieArticles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EmailEntreprises",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Addresse_ = c.Int(nullable: false),
                        Message_object = c.String(),
                        Message = c.String(),
                        Date_envoie = c.DateTime(nullable: false),
                        Adresse_Mail_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Entreprises", t => t.Adresse_Mail_Id)
                .Index(t => t.Adresse_Mail_Id);
            
            CreateTable(
                "dbo.EmailServeurs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Port = c.Int(nullable: false),
                        DateAjout = c.DateTime(nullable: false),
                        ServeurAdresse = c.String(nullable: false),
                        AdresseCRM = c.String(nullable: false),
                        PassWordCRM = c.String(nullable: false),
                        EtatConfig = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EtatContacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Intitule = c.String(nullable: false),
                        Description = c.String(),
                        DateVariation = c.DateTime(nullable: false),
                        ContactId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contacts", t => t.ContactId, cascadeDelete: true)
                .Index(t => t.ContactId);
            
            CreateTable(
                "dbo.ServeurSms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ServerAdress = c.String(nullable: false),
                        Username = c.String(nullable: false),
                        PasswordUser = c.String(nullable: false),
                        SenderId = c.String(nullable: false),
                        Port = c.Int(nullable: false),
                        EtatConfig = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SmsEntreprises",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Telephone_ = c.Int(nullable: false),
                        Message = c.String(),
                        Date_envoie = c.DateTime(nullable: false),
                        Telephone_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Entreprises", t => t.Telephone_Id)
                .Index(t => t.Telephone_Id);
            
            CreateTable(
                "dbo.Sms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Telephone_ = c.Int(nullable: false),
                        Message = c.String(),
                        Date_envoie = c.DateTime(nullable: false),
                        Telephone_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contacts", t => t.Telephone_Id)
                .Index(t => t.Telephone_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sms", "Telephone_Id", "dbo.Contacts");
            DropForeignKey("dbo.SmsEntreprises", "Telephone_Id", "dbo.Entreprises");
            DropForeignKey("dbo.EtatContacts", "ContactId", "dbo.Contacts");
            DropForeignKey("dbo.EmailEntreprises", "Adresse_Mail_Id", "dbo.Entreprises");
            DropForeignKey("dbo.VenteArticles", "Contact_Id", "dbo.Contacts");
            DropForeignKey("dbo.VenteArticles", "Article_Idp", "dbo.Articles");
            DropForeignKey("dbo.Articles", "CategorieProduit_Id", "dbo.CategorieArticles");
            DropForeignKey("dbo.Taches", "Projet_Id", "dbo.Projets");
            DropForeignKey("dbo.Achats", "Projet_Id", "dbo.Projets");
            DropForeignKey("dbo.FileDetailsProjets", "Projet_Id", "dbo.Projets");
            DropForeignKey("dbo.Projets", "EtatProjet_Id", "dbo.EtatProjets");
            DropForeignKey("dbo.Taches", "Contact_Id", "dbo.Contacts");
            DropForeignKey("dbo.Emails", "Adresse_Mail_Id", "dbo.Contacts");
            DropForeignKey("dbo.Achats", "Contact_Id", "dbo.Contacts");
            DropForeignKey("dbo.Contacts", "Entreprise_Id", "dbo.Entreprises");
            DropForeignKey("dbo.Contacts", "Contactgroup_Id", "dbo.ContactGroups");
            DropForeignKey("dbo.FileDetailsActivites", "Activite_Id", "dbo.Activites");
            DropForeignKey("dbo.Activites", "Contact_Id", "dbo.Contacts");
            DropForeignKey("dbo.Activites", "Categorie_Id", "dbo.CategorieActivites");
            DropIndex("dbo.Sms", new[] { "Telephone_Id" });
            DropIndex("dbo.SmsEntreprises", new[] { "Telephone_Id" });
            DropIndex("dbo.EtatContacts", new[] { "ContactId" });
            DropIndex("dbo.EmailEntreprises", new[] { "Adresse_Mail_Id" });
            DropIndex("dbo.Articles", new[] { "CategorieProduit_Id" });
            DropIndex("dbo.VenteArticles", new[] { "Contact_Id" });
            DropIndex("dbo.VenteArticles", new[] { "Article_Idp" });
            DropIndex("dbo.FileDetailsProjets", new[] { "Projet_Id" });
            DropIndex("dbo.Projets", new[] { "EtatProjet_Id" });
            DropIndex("dbo.Taches", new[] { "Projet_Id" });
            DropIndex("dbo.Taches", new[] { "Contact_Id" });
            DropIndex("dbo.Emails", new[] { "Adresse_Mail_Id" });
            DropIndex("dbo.Entreprises", new[] { "Telephone" });
            DropIndex("dbo.FileDetailsActivites", new[] { "Activite_Id" });
            DropIndex("dbo.Activites", new[] { "Contact_Id" });
            DropIndex("dbo.Activites", new[] { "Categorie_Id" });
            DropIndex("dbo.Contacts", new[] { "Entreprise_Id" });
            DropIndex("dbo.Contacts", new[] { "Contactgroup_Id" });
            DropIndex("dbo.Contacts", new[] { "Telephone" });
            DropIndex("dbo.Achats", new[] { "Projet_Id" });
            DropIndex("dbo.Achats", new[] { "Contact_Id" });
            DropTable("dbo.Sms");
            DropTable("dbo.SmsEntreprises");
            DropTable("dbo.ServeurSms");
            DropTable("dbo.EtatContacts");
            DropTable("dbo.EmailServeurs");
            DropTable("dbo.EmailEntreprises");
            DropTable("dbo.CategorieArticles");
            DropTable("dbo.Articles");
            DropTable("dbo.VenteArticles");
            DropTable("dbo.FileDetailsProjets");
            DropTable("dbo.EtatProjets");
            DropTable("dbo.Projets");
            DropTable("dbo.Taches");
            DropTable("dbo.Emails");
            DropTable("dbo.Entreprises");
            DropTable("dbo.ContactGroups");
            DropTable("dbo.FileDetailsActivites");
            DropTable("dbo.CategorieActivites");
            DropTable("dbo.Activites");
            DropTable("dbo.Contacts");
            DropTable("dbo.Achats");
        }
    }
}
