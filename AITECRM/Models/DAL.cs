using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AITECRM.ViewModels;
using System.Data.Entity;
using System.Web.Mvc;

namespace AITECRM.Models
{
    public class DAL:IDisposable
    {
        private AITECRMContext db;
        public DAL()
        {
          db = new AITECRMContext();
        }

        public bool AdresseEntrepriseExiste(string num)
        {
            var ListEmpty = db.Entreprises.Where(x => x.Adresse_Email == num).FirstOrDefault();
            if (ListEmpty != null)
            {
                return (false);
            }
            else return true;

        }
        public bool AdresseEntrepriseEditExiste(string num)
        {
            var ListEmpty = db.Entreprises.Where(x => x.Adresse_Email == num).ToList();
            if (ListEmpty.Count() > 1)
            {
                return (false);
            }
            else return true;
            
        }
        public bool AdresseContactExiste(string num)
        {
            var ListEmpty = db.Contacts.Where(x => x.Adresse_mail == num).FirstOrDefault();
            if (ListEmpty != null)
            {
                return (false);
            }
            else return true;
        }
        public bool AdresseContactEditExiste(string num)
        {
            var ListEmpty = db.Contacts.Where(x => x.Adresse_mail == num).ToList();
            if (ListEmpty.Count() > 1)
            {
                return (false);
            }
            else return true;
        }
        public bool NumContactExiste(int num)
        {
            if (db.Contacts.Where(x => x.Telephone == num).FirstOrDefault() != null)
            {
                return (false);
            }
            else return true;
        }
        public bool NumContactEditExiste(int num)
        {
            var ListEmpty = db.Contacts.Where(x => x.Telephone == num).ToList();
            if (ListEmpty.Count() > 1)
            {
                return (false);
            }
            else return true;
          
        }
        public bool NumEntrepriseExiste(int num)
        {
            if (db.Entreprises.Where(x => x.Telephone == num).FirstOrDefault() != null)
            {
                return (false);
            }
            else return true;
        }
        public bool NumEntrepriseEditExiste(int num)
        {
            var ListEmpty = db.Entreprises.Where(x => x.Telephone == num).ToList();
            if (ListEmpty.Count() > 1 )
            {
                return (false);
            }
            else return true;
        }
        public List<g1> ChiffreAffaire(DateTime inf, DateTime sup)
        {
            var ListActChiffre = (from a in db.CategorieActivites
                                  join b in db.Activites on a.Id equals b.Categorie_
                                  where b.Etat == "Reussi"
                                  where b.DacteCreation >= inf && b.DacteCreation <= sup
                                  select new {
                                      NomCategorie = a.Nom,Prix = b.Prix
                                  } into ve
                                  group ve by ve.NomCategorie into g
                                  select new { Categorie = g.Key, prix = g.Select(e=>e.Prix).Sum()}
                ).ToList().Select(x => new g1()
                {
                    name = x.Categorie,
                    data = x.prix
                }).ToList();
            return (ListActChiffre);
        }


        public List<g1> ChiffreAffaire()
        {
            var ListActChiffre = (from a in db.CategorieActivites
                                  join b in db.Activites on a.Id equals b.Categorie_
                                  where b.Etat == "Reussi"
                                  select new
                                  {
                                      NomCategorie = a.Nom,
                                      Prix = b.Prix
                                  } into ve
                                  group ve by ve.NomCategorie into g
                                  select new { Categorie = g.Key, prix = g.Select(e => e.Prix).Sum() }
                ).ToList().Select(x => new g1()
                {
                    name = x.Categorie,
                    data = x.prix
                }).ToList();

            return (ListActChiffre);
        }

        public List<g1> NombreExecutionParEtat()
        {
            var NombreExecutionParEtat = (from b in db.Activites
                                  orderby b.Etat 
                                  select new
                                  {
                                      NomCategorie = b.Etat,
                                      Prix = b.Prix
                                  } into ve
                                  group ve by ve.NomCategorie into g
                                  select new { Categorie = g.Key, prix = g.Select(e => e.Prix).Sum() }
                ).ToList().Select(x => new g1()
                {
                    name = x.Categorie,
                    data = x.prix
                }).ToList();

            return (NombreExecutionParEtat);
        }



    

        public int CoutMoyenFontionTypeProjet(string typeprojet)
        {
            throw new NotImplementedException();
        }

        public int CoutMoyenProjet()
        {
            
             throw new NotImplementedException();
        }

        public int CoutMoyenProjetFonctionDates(DateTime inf, DateTime sup)
        {
            throw new NotImplementedException();
        }

        public int CoutMoyenTypeProjetDates(DateTime inf, DateTime sup, string typeprojet)
        {
            throw new NotImplementedException();
        }

        public void DateAnniversaireEmploye()
        {
            var datePrevenir = (from a in db.Contacts
                                join b in db.ContactGroup on a.Contactgroups equals b.Id
                                select new
                                {
                                    categorie = b.Type_name,
                                    nomcontact = a.Contact_name,
                                    civilite = a.Civilite,
                                    Anniversaire = a.Date_naissance
                                }).ToList();
                                

            throw new NotImplementedException();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public List<RapportEmployes> EvaluationEmploye(string Employe)
        {
            throw new NotImplementedException();
        }

        public int GainMoyenProjet()
        {
            throw new NotImplementedException();
        }

        public int GainMoyenProjetDate(DateTime inf, DateTime sup)
        {
            throw new NotImplementedException();
        }

        public List<g1> NombreActivites()
        {
            var Listactivites = (from a in db.Activites
                                 join b in db.CategorieActivites on a.Categorie_ equals b.Id
                                 select new { Nomcategorie = b.Nom, nomactivite = a.Nom, } into ve
                                 group ve by ve.Nomcategorie into g
                                 select new
                                 {
                                     categorie = g.Key,
                                     nombre = g.Select(e => e.nomactivite).Count()
                                 }).ToList().Select(x => new g1()
                                 {
                                     name = x.categorie,
                                     data = x.nombre
                                 }).ToList();
            return (Listactivites);
        }

        public List<g1> NombreActivites(DateTime inf, DateTime sup)
        {
            var Listactivites = (from a in db.Activites
                                 where a.DacteCreation >= inf && a.DacteCreation >= sup
                                 join b in db.CategorieActivites on a.Categorie_ equals b.Id
                                 select new { Nomcategorie = b.Nom, nomactivite = a.Nom, } into ve
                                 group ve by ve.Nomcategorie into g
                                 select new
                                 {
                                     categorie = g.Key,
                                     nombre = g.Select(e => e.nomactivite).Count()
                                 }).ToList().Select(x => new g1()
                                 {
                                     name = x.categorie,
                                     data = x.nombre
                                 }).ToList();
            return (Listactivites);
        }

        //****************************Contacts********************

        public List<g1> NombreTotalContactCategorie()
        {
            var ListContacts = (from a in db.Contacts
                                join b in db.ContactGroup on a.Contactgroups equals b.Id
                                select new { NomCategorie = b.Type_name, nomcontact = a.Contact_name } into ve
                                group ve by ve.NomCategorie into g
                                select new { categorie = g.Key,nombre = g.Select(e=>e.nomcontact).Count()}
                                ).ToList().Select(x => new g1()
                                {
                                    name = x.categorie,
                                    data = x.nombre
                                }).ToList();
            return (ListContacts);
        }

        public List<g1> NombreTotalContactCategorie(DateTime inf, DateTime sup)
        {
            var ListContacts = (from a in db.Contacts
                                join b in db.ContactGroup on a.Contactgroups equals b.Id
                                where(a.Date_enregistrement >= inf && a.Date_enregistrement <= sup)
                                select new { NomCategorie = b.Type_name, nomcontact = a.Contact_name } into ve
                                group ve by ve.NomCategorie into g
                                select new { categorie = g.Key, nombre = g.Select(e => e.nomcontact).Count() }
                              ).ToList().Select(x => new g1()
                              {
                                  name = x.categorie,
                                  data = x.nombre
                              }).ToList();
            return (ListContacts);
        }

        public List<g1> NombreTotalContactMois()
        {
            
           string Moist = DateTime.Now.ToString("MMM");
            var ListContacts = (from a in db.Contacts
                                where a.Date_enregistrement.Year == DateTime.Now.Year
                                select new {
                                    nomcontact = a.Contact_name, dateEngistrement = a.Date_enregistrement.Month,
                                     
                                } into ve
                                group ve by ve.dateEngistrement into g
                                select new { mois = g.Key, nombre = g.Select(e => e.nomcontact).Count() }
                                           ).ToList().Select(x => new g1()
                                           {
                                               name = x.mois.ToString(),
                                               data = x.nombre
                                           }).ToList();
            
            return (ListContacts);
        }


        public List<g1> NombreTotalContactMois(DateTime inf, DateTime sup)
        {

            string Moist = DateTime.Now.ToString("MMM");
            var ListContacts = (from a in db.Contacts
                                where (a.Date_enregistrement >= inf && a.Date_enregistrement <= sup)
                                select new
                                {
                                    nomcontact = a.Contact_name,
                                    dateEngistrement = a.Date_enregistrement.Month,

                                } into ve
                                group ve by ve.dateEngistrement into g
                                select new { mois = g.Key, nombre = g.Select(e => e.nomcontact).Count() }
                                           ).ToList().Select(x => new g1()
                                           {
                                               name = x.mois.ToString(),
                                               data = x.nombre
                                           }).ToList();

            return (ListContacts);
        }

        //************************************Emails****************************************************
        public List<g1> NombreTotalEmailsMois()
        {
            var ListEmails = (from a in db.Emails
                              where a.Date_envoie.Year == DateTime.Now.Year
                              select new
                              {
                                  email = a.Id,
                                  dateEngistrement = a.Date_envoie.Month
                              } into ve
                              group ve by ve.dateEngistrement into g
                              select new { mois = g.Key, nombre = g.Select(e => e.email).Count() }
                                           ).ToList().Select(x => new g1()
                                           {
                                               name = x.mois.ToString(),
                                               data = x.nombre
                                           }).ToList();
            return (ListEmails);
        }
        public List<g1> NombreTotalEmailsEntrepriseMois()
        {
            var ListEmails = (from a in db.EmailEntreprises
                              where a.Date_envoie.Year == DateTime.Now.Year
                              select new
                              {
                                  email = a.Id,
                                  dateEngistrement = a.Date_envoie.Month
                              } into ve
                              group ve by ve.dateEngistrement into g
                              select new { mois = g.Key, nombre = g.Select(e => e.email).Count() }
                                           ).ToList().Select(x => new g1()
                                           {
                                               name = x.mois.ToString(),
                                               data = x.nombre
                                           }).ToList();
            return (ListEmails);
        }



        public List<g1> NombreTotalEmailsMois(DateTime inf, DateTime sup)
        {
            var ListEmails = (from a in db.Emails
                                where (a.Date_envoie >= inf & a.Date_envoie<= sup)
                                select new
                                {
                                    email = a.Id,
                                    dateEngistrement = a.Date_envoie.Month
                                } into ve
                                group ve by ve.dateEngistrement into g
                                select new { mois = g.Key, nombre = g.Select(e => e.email).Count() }
                                           ).ToList().Select(x => new g1()
                                           {
                                               name = x.mois.ToString(),
                                               data = x.nombre
                                           }).ToList();
            return (ListEmails);
        }
        public List<g1> NombreTotalEmailsEntrepriseMois(DateTime inf, DateTime sup)
        {
            var ListEmails = (from a in db.EmailEntreprises
                              where (a.Date_envoie >= inf && a.Date_envoie <= sup
                              && a.Date_envoie >= inf & a.Date_envoie <= sup)
                              select new
                              {
                                  email = a.Id,
                                  dateEngistrement = a.Date_envoie.Month
                              } into ve
                              group ve by ve.dateEngistrement into g
                              select new { mois = g.Key, nombre = g.Select(e => e.email).Count() }
                                           ).ToList().Select(x => new g1()
                                           {
                                               name = x.mois.ToString(),
                                               data = x.nombre
                                           }).ToList();
            return (ListEmails);
        }

        //**********************************************Email****End*********************************************

        public List<g1> NombreTotalEntreprisePays()
        {
            var ListEntreprises = (from a in db.Entreprises
                                   orderby a.Pays
                                   select new { nom = a.Nom, pays = a.Pays } into ve
                                   group ve by ve.pays into g
                                   select new { pays = g.Key, nombre = g.Select(e => e.nom).Count() })
                                   .ToList().Select(x => new g1()
                                   {
                                      name = x.pays,
                                       data = x.nombre
                                   }).ToList();
  
            return(ListEntreprises);
        }

        public List<g1> NombreTotalEntreprisePays(DateTime inf, DateTime sup)
        {
            var ListEntreprises = (from a in db.Entreprises
                                   where (a.Date_enregistrement>= inf && a.Date_enregistrement<=sup)
                                   orderby a.Pays
                                   select new { nom = a.Nom, pays = a.Pays } into ve
                                   group ve by ve.pays into g
                                   select new { pays = g.Key, nombre = g.Select(e => e.nom).Count() })
                                   .ToList().Select(x => new g1()
                                   {
                                       name = x.pays,
                                       data = x.nombre
                                   }).ToList();

            return (ListEntreprises);
                }
        public List<g1> NombreTotalContactPays()
        {
            var ListContacts = (from a in db.Contacts
                                   orderby a.Pays
                                   select new { nom = a.Contact_name, pays = a.Pays } into ve
                                   group ve by ve.pays into g
                                   select new { pays = g.Key, nombre = g.Select(e => e.nom).Count() })
                                   .ToList().Select(x => new g1()
                                   {
                                       name = x.pays,
                                       data = x.nombre
                                   }).ToList();

            return (ListContacts);
        }
        public List<g1> NombreTotalContactPays(DateTime inf ,DateTime sup)
        {
            var ListContacts = (from a in db.Contacts
                                where (a.Date_enregistrement >= inf && a.Date_enregistrement <= sup)
                                orderby a.Pays
                                select new { nom = a.Contact_name, pays = a.Pays } into ve
                                group ve by ve.pays into g
                                select new { pays = g.Key, nombre = g.Select(e => e.nom).Count() })
                                   .ToList().Select(x => new g1()
                                   {
                                       name = x.pays,
                                       data = x.nombre
                                   }).ToList();

            return (ListContacts);
        }



        //***********************SMS********************************************

        public List<g1> NombreTotalSmsMois()
        {
            var ListSms = (from a in db.Smss
                           where a.Date_envoie.Year == DateTime.Now.Year
                           select new
                           {
                               Sms = a.Id,
                               dateEngistrement = a.Date_envoie.Month.ToString()
                           } into ve
                           group ve by ve.dateEngistrement into g
                           select new { mois = g.Key, nombre = g.Select(e => e.Sms).Count() }
                                           ).ToList().Select(x => new g1()
                                           {
                                               name = x.mois,
                                               data = x.nombre
                                           }).ToList();
            return (ListSms);
        }

        public List<g1> NombreTotalSmsEntrepriseMois()
        {
            var ListSms = (from a in db.EmailEntreprises
                           where a.Date_envoie.Year == DateTime.Now.Year
                           select new
                           {
                               Sms = a.Id,
                               dateEngistrement = a.Date_envoie.Month.ToString()
                           } into ve
                           group ve by ve.dateEngistrement into g
                           select new { mois = g.Key, nombre = g.Select(e => e.Sms).Count() }
                                           ).ToList().Select(x => new g1()
                                           {
                                               name = x.mois,
                                               data = x.nombre
                                           }).ToList();
            return (ListSms);
        }
        public List<g1> NombreTotalSmsMois(DateTime inf, DateTime sup)
        {
            var ListSms = (from a in db.Smss
                                where (a.Date_envoie>= inf && a.Date_envoie <= sup)
                           select new
                                {
                                    Sms = a.Id,
                                    dateEngistrement = a.Date_envoie.Month.ToString()
                                } into ve
                                group ve by ve.dateEngistrement into g
                                select new { mois = g.Key, nombre = g.Select(e => e.Sms).Count() }
                                           ).ToList().Select(x => new g1()
                                           {
                                               name = x.mois,
                                               data = x.nombre
                                           }).ToList();
            return (ListSms);
        }
        public List<g1> NombreTotalSmsEntrepriseMois(DateTime inf, DateTime sup)
        {
            var ListSms = (from a in db.EmailEntreprises
                           where (a.Date_envoie >= inf && a.Date_envoie <= sup)
                           select new
                           {
                               Sms = a.Id,
                               dateEngistrement = a.Date_envoie.Month.ToString()
                           } into ve
                           group ve by ve.dateEngistrement into g
                           select new { mois = g.Key, nombre = g.Select(e => e.Sms).Count() }
                                           ).ToList().Select(x => new g1()
                                           {
                                               name = x.mois,
                                               data = x.nombre
                                           }).ToList();
            return (ListSms);
        }

        //*****************************************SMS****End**************************

        public List<AllProjet> ObtientTousLesProjets()
        {
            throw new NotImplementedException();
        }

        public List<g1> TauxExecution()
        {
            var TauxExecution = (from a in db.Activites
                                select new
                                {
                                    NomActivite = a.Nom,
                                    Etat = a.Etat
                                } into ve
                                group ve by ve.Etat into g
                                select new { Etat = g.Key, Nombre = g.Select(e => e.NomActivite).Count() }
                            ).ToList().Select(x => new g1()
                            {
                                name = x.Etat,
                                data = x.Nombre
                            }).ToList();

            return (TauxExecution);
        }

        public List<g1> TauxExecution(DateTime inf, DateTime sup)
        {
            var TauxExecution = (from a in db.Activites
                                 where a.DacteCreation>= inf && a.DacteCreation <= sup
                                 select new
                                 {
                                     NomActivite = a.Nom,
                                     Etat = a.Etat
                                 } into ve
                                 group ve by ve.Etat into g
                                 select new { Etat = g.Key, Nombre = g.Select(e => e.NomActivite).Count() }
                            ).ToList().Select(x => new g1()
                            {
                                name = x.Etat,
                                data = x.Nombre
                            }).ToList();

            return (TauxExecution);
        }

        // Rapport Ventes d'articles

        public List<g1> ChiffreAffaireTypeProduit()
        {
            var ListVentes = (from a in db.CategorieProduits
                              join b in db.Articles on a.Id equals b.CategorieProduit_
                              join c in db.VenteArticles on b.Idp equals c.Article_
                              select new
                              {
                                  NomCategorie = a.Nom,
                                  Chiffre = b.Prix
                              } into ve
                              group ve by ve.NomCategorie into g
                              select new { categorie = g.Key, chiffreAffaire = g.Select(e => e.Chiffre).Sum() }
                              ).ToList().Select(x => new g1()
                              {
                                  name = x.categorie,
                                  data = x.chiffreAffaire
                              }).ToList();

            return (ListVentes);
        }
        public List<g1> ChiffreAffaireTypeProduit(DateTime inf, DateTime sup)
        {
            var ListVentes = (from a in db.CategorieProduits
                              join b in db.Articles on a.Id equals b.CategorieProduit_
                              join c in db.VenteArticles on b.Idp equals c.Article_
                              where c.DateAchat >= inf && c.DateAchat <= sup
                              select new
                              {
                                  NomCategorie = a.Nom,
                                  Chiffre = b.Prix
                              } into ve
                              group ve by ve.NomCategorie into g
                              select new { categorie = g.Key, chiffreAffaire = g.Select(e => e.Chiffre).Sum() }
                              ).ToList().Select(x => new g1()
                              {
                                  name = x.categorie,
                                  data = x.chiffreAffaire
                              }).ToList();

            return (ListVentes);
        }

        

        public List<g1> NombreTotalAchatTypeProduit()
        {

            var NombreVentes = (from a in db.CategorieProduits
                                join b in db.Articles on a.Id equals b.CategorieProduit_
                                select new
                                {
                                    NomCategorie = a.Nom,
                                    Nom = b.Nom
                                } into ve
                                group ve by ve.NomCategorie into g
                                select new { categorie = g.Key, chiffreAffaire = g.Select(e => e.Nom).Count() }
                            ).ToList().Select(x => new g1()
                            {
                                name = x.categorie,
                                data = x.chiffreAffaire
                            }).ToList();

            return (NombreVentes);

        }

        public List<g1> NombreTotalAchatTypeProduit(DateTime inf, DateTime sup)
        {
            var NombreVentes = (from a in db.CategorieProduits
                                join b in db.Articles on a.Id equals b.CategorieProduit_
                                join c in db.VenteArticles on b.Idp equals c.Article_
                                where c.DateAchat >= inf && c.DateAchat <= c.DateAchat
                                select new
                                {
                                    NomCategorie = a.Nom,
                                    Nom = b.Nom,
                                    prix = b.Prix
                                } into ve
                                group ve by ve.NomCategorie into g
                                select new { categorie = g.Key, chiffreAffaire = g.Select(e => e.Nom).Count() }
                             ).ToList().Select(x => new g1()
                             {
                                 name = x.categorie,
                                 data = x.chiffreAffaire
                             }).ToList();

            return (NombreVentes);
        }


        //**************************************

        public List<RapportEmployes> VitesseExecTacheProjet(string NomProjet)
        {
            throw new NotImplementedException();
        }

        public List<RapportEmployes> VitesseExecutionTachesGeneral()
        {
            throw new NotImplementedException();
        }

        public List<RapportEmployes> VitesseExecutionTachesTemps(DateTime inf, DateTime sup)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<VenteProduitsViewModel> RapportVente(string Entreprise, string Contact, string Categorie, string Produit, string Prixinf, string Prixsup, string inf, string sup)
        {
            IEnumerable<VenteProduitsViewModel> ListEmpty = new List<VenteProduitsViewModel>();

            //00000000
            if (Prixinf.Trim().Length == 0 && Contact.Trim().Length == 0 &&
                Categorie.Trim().Length == 0 && Produit.Trim().Length == 0 &&
                Prixinf.Trim().Length == 0 && Prixsup.Trim().Length == 0 &&
                inf.Trim().Length == 0 && sup.Trim().Length == 0)
            {
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel() {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }
            //00000001
            else if (Entreprise.Trim().Length == 0 && Contact.Trim().Length == 0 &&
                Categorie.Trim().Length == 0 && Produit.Trim().Length == 0 &&
                Prixinf.Trim().Length == 0 && Prixsup.Trim().Length == 0 &&
                inf.Trim().Length == 0 && sup.Trim().Length != 0)
            {
                DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (e.DateAchat <= supt)
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }
            //00000010
            else if (Entreprise.Trim().Length == 0 && Contact.Trim().Length == 0 &&
                Categorie.Trim().Length == 0 && Produit.Trim().Length == 0 &&
                Prixinf.Trim().Length == 0 && Prixsup.Trim().Length == 0 &&
                inf.Trim().Length != 0 && sup.Trim().Length == 0)
            {
                DateTime inft = Convert.ToDateTime(inf);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (e.DateAchat >= inft)
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }
            //00000011
            else if (Entreprise.Trim().Length == 0 && Contact.Trim().Length == 0 &&
                Categorie.Trim().Length == 0 && Produit.Trim().Length == 0 &&
                Prixinf.Trim().Length == 0 && Prixsup.Trim().Length == 0 &&
                inf.Trim().Length != 0 && sup.Trim().Length != 0)
            {
                DateTime supt = Convert.ToDateTime(sup); DateTime inft = Convert.ToDateTime(inf);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (e.DateAchat <= supt && e.DateAchat >= inft)
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }
            //00000100
            else if (Entreprise.Trim().Length == 0 && Contact.Trim().Length == 0 &&
                Categorie.Trim().Length == 0 && Produit.Trim().Length == 0 &&
                Prixinf.Trim().Length == 0 && Prixsup.Trim().Length != 0 &&
                inf.Trim().Length == 0 && sup.Trim().Length == 0)
            {
                int prix = Convert.ToInt32(Prixsup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (d.Prix <= prix)
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }
            //00000101
            else if (Entreprise.Trim().Length == 0 && Contact.Trim().Length == 0 &&
                Categorie.Trim().Length == 0 && Produit.Trim().Length == 0 &&
                Prixinf.Trim().Length == 0 && Prixsup.Trim().Length != 0 &&
                inf.Trim().Length == 0 && sup.Trim().Length != 0)
            {
                int prix = Convert.ToInt32(Prixsup);DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (d.Prix <= prix && e.DateAchat <= supt)
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }
            //00000110
            else if (Entreprise.Trim().Length == 0 && Contact.Trim().Length == 0 &&
                Categorie.Trim().Length == 0 && Produit.Trim().Length == 0 &&
                Prixinf.Trim().Length == 0 && Prixsup.Trim().Length != 0 &&
                inf.Trim().Length != 0 && sup.Trim().Length == 0)
            {
                int prix = Convert.ToInt32(Prixsup); DateTime inft = Convert.ToDateTime(inf);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (d.Prix <= prix && e.DateAchat >= inft)
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }
            //00000111
            else if (Entreprise.Trim().Length == 0 && Contact.Trim().Length == 0 &&
                Categorie.Trim().Length == 0 && Produit.Trim().Length == 0 &&
                Prixinf.Trim().Length == 0 && Prixsup.Trim().Length != 0 &&
                inf.Trim().Length != 0 && sup.Trim().Length != 0)
            {
                int prix = Convert.ToInt32(Prixsup);
                DateTime inft = Convert.ToDateTime(inf);
                DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (d.Prix <= prix && e.DateAchat >= inft && e.DateAchat <= supt)
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }
            //00001000
            else if (Entreprise.Trim().Length == 0 && Contact.Trim().Length == 0 &&
                Categorie.Trim().Length == 0 && Produit.Trim().Length == 0 &&
                Prixinf.Trim().Length != 0 && Prixsup.Trim().Length == 0 &&
                inf.Trim().Length == 0 && sup.Trim().Length == 0)
            {
                int prixi = Convert.ToInt32(Prixinf);
                //DateTime inft = Convert.ToDateTime(inf);
                //DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (d.Prix >= prixi)
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }
            //00001001
            else if (Entreprise.Trim().Length == 0 && Contact.Trim().Length == 0 &&
               Categorie.Trim().Length == 0 && Produit.Trim().Length == 0 &&
               Prixinf.Trim().Length != 0 && Prixsup.Trim().Length == 0 &&
               inf.Trim().Length == 0 && sup.Trim().Length != 0)
            {
                int prixi = Convert.ToInt32(Prixinf);
                //DateTime inft = Convert.ToDateTime(inf);
                DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (d.Prix >= prixi && e.DateAchat <= supt)
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }
            //00001010
            else if (Entreprise.Trim().Length == 0 && Contact.Trim().Length == 0 &&
               Categorie.Trim().Length == 0 && Produit.Trim().Length == 0 &&
               Prixinf.Trim().Length != 0 && Prixsup.Trim().Length == 0 &&
               inf.Trim().Length != 0 && sup.Trim().Length == 0)
            {
                int prixi = Convert.ToInt32(Prixinf);
                DateTime inft = Convert.ToDateTime(inf);
                //DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (d.Prix >= prixi && e.DateAchat >= inft)
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }

            //00001011
            else if (Entreprise.Trim().Length == 0 && Contact.Trim().Length == 0 &&
               Categorie.Trim().Length == 0 && Produit.Trim().Length == 0 &&
               Prixinf.Trim().Length != 0 && Prixsup.Trim().Length == 0 &&
               inf.Trim().Length != 0 && sup.Trim().Length != 0)
            {
                int prixi = Convert.ToInt32(Prixinf);
                DateTime inft = Convert.ToDateTime(inf);
                DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (d.Prix >= prixi && e.DateAchat <= supt && e.DateAchat >= inft)
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }
            //00001100
            else if (Entreprise.Trim().Length == 0 && Contact.Trim().Length == 0 &&
               Categorie.Trim().Length == 0 && Produit.Trim().Length == 0 &&
               Prixinf.Trim().Length != 0 && Prixsup.Trim().Length != 0 &&
               inf.Trim().Length == 0 && sup.Trim().Length == 0)
            {
                int prixi = Convert.ToInt32(Prixinf);
                int prixs = Convert.ToInt32(Prixsup);
                //DateTime inft = Convert.ToDateTime(inf);
                //DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (d.Prix >= prixi && d.Prix <= prixs)
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }

            //00001101
            else if (Entreprise.Trim().Length == 0 && Contact.Trim().Length == 0 &&
               Categorie.Trim().Length == 0 && Produit.Trim().Length == 0 &&
               Prixinf.Trim().Length != 0 && Prixsup.Trim().Length != 0 &&
               inf.Trim().Length == 0 && sup.Trim().Length != 0)
            {
                int prixi = Convert.ToInt32(Prixinf);
                int prixs = Convert.ToInt32(Prixsup);
                //DateTime inft = Convert.ToDateTime(inf);
                DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (d.Prix >= prixi && d.Prix <= prixs && e.DateAchat <= supt)
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }
            //00001110
            else if (Entreprise.Trim().Length == 0 && Contact.Trim().Length == 0 &&
               Categorie.Trim().Length == 0 && Produit.Trim().Length == 0 &&
               Prixinf.Trim().Length != 0 && Prixsup.Trim().Length != 0 &&
               inf.Trim().Length != 0 && sup.Trim().Length == 0)
            {
                int prixi = Convert.ToInt32(Prixinf);
                int prixs = Convert.ToInt32(Prixsup);
                DateTime inft = Convert.ToDateTime(inf);
                //DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (d.Prix >= prixi && d.Prix <= prixs && e.DateAchat >= inft)
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }
            //00001111
            else if (Entreprise.Trim().Length == 0 && Contact.Trim().Length == 0 &&
               Categorie.Trim().Length == 0 && Produit.Trim().Length == 0 &&
               Prixinf.Trim().Length != 0 && Prixsup.Trim().Length != 0 &&
               inf.Trim().Length != 0 && sup.Trim().Length != 0)
            {
                int prixi = Convert.ToInt32(Prixinf);
                int prixs = Convert.ToInt32(Prixsup);
                DateTime inft = Convert.ToDateTime(inf);
                DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (d.Prix >= prixi && d.Prix <= prixs
                             && e.DateAchat>= inft && e.DateAchat <= supt)
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }
            //00110000
            else if (Entreprise.Trim().Length == 0 && Contact.Trim().Length == 0 &&
               Categorie.Trim().Length != 0 && Produit.Trim().Length != 0 &&
               Prixinf.Trim().Length == 0 && Prixsup.Trim().Length == 0 &&
               inf.Trim().Length == 0 && sup.Trim().Length == 0)
            {

                //int prixi = Convert.ToInt32(Prixinf);
                //int prixs = Convert.ToInt32(Prixsup);
                //DateTime inft = Convert.ToDateTime(inf);
                //DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (d.Nom == Produit)
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }
            //00110001
            else if (Entreprise.Trim().Length == 0 && Contact.Trim().Length == 0 &&
               Categorie.Trim().Length != 0 && Produit.Trim().Length != 0 &&
               Prixinf.Trim().Length == 0 && Prixsup.Trim().Length == 0 &&
               inf.Trim().Length == 0 && sup.Trim().Length != 0)
            {

                //int prixi = Convert.ToInt32(Prixinf);
                //int prixs = Convert.ToInt32(Prixsup);
                //DateTime inft = Convert.ToDateTime(inf);
                DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (d.Nom == Produit
                                    //&& d.Prix >= prixi 
                                    //&& d.Prix <= prixs
                                    //&& e.DateAchat >= inft 
                                    && e.DateAchat <= supt
                              )
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }

            //00110010
            else if (Entreprise.Trim().Length == 0 && Contact.Trim().Length == 0 &&
               Categorie.Trim().Length != 0 && Produit.Trim().Length != 0 &&
               Prixinf.Trim().Length == 0 && Prixsup.Trim().Length == 0 &&
               inf.Trim().Length != 0 && sup.Trim().Length == 0)
            {

                //int prixi = Convert.ToInt32(Prixinf);
                //int prixs = Convert.ToInt32(Prixsup);
                DateTime inft = Convert.ToDateTime(inf);
               // DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (d.Nom == Produit
                                    //&& d.Prix >= prixi 
                                    //&& d.Prix <= prixs
                                    && e.DateAchat >= inft 
                                    //&& e.DateAchat <= supt
                              )
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }

            //00110011
            else if (Entreprise.Trim().Length == 0 && Contact.Trim().Length == 0 &&
               Categorie.Trim().Length != 0 && Produit.Trim().Length != 0 &&
               Prixinf.Trim().Length == 0 && Prixsup.Trim().Length == 0 &&
               inf.Trim().Length != 0 && sup.Trim().Length != 0)
            {

                //int prixi = Convert.ToInt32(Prixinf);
                //int prixs = Convert.ToInt32(Prixsup);
                DateTime inft = Convert.ToDateTime(inf);
                DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (d.Nom == Produit
                                    //&& d.Prix >= prixi 
                                    //&& d.Prix <= prixs
                                    && e.DateAchat >= inft 
                                    && e.DateAchat <= supt
                              )
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }
            //00110100
            else if (Entreprise.Trim().Length == 0 && Contact.Trim().Length == 0 &&
               Categorie.Trim().Length != 0 && Produit.Trim().Length != 0 &&
               Prixinf.Trim().Length == 0 && Prixsup.Trim().Length != 0 &&
               inf.Trim().Length == 0 && sup.Trim().Length == 0)
            {

                //int prixi = Convert.ToInt32(Prixinf);
                int prixs = Convert.ToInt32(Prixsup);
                //DateTime inft = Convert.ToDateTime(inf);
                //DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (d.Nom == Produit
                                    //&& d.Prix >= prixi 
                                    && d.Prix <= prixs
                                    //&& e.DateAchat >= inft 
                                    //&& e.DateAchat <= supt
                              )
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }
            //00110101
            else if (Entreprise.Trim().Length == 0 && Contact.Trim().Length == 0 &&
               Categorie.Trim().Length != 0 && Produit.Trim().Length != 0 &&
               Prixinf.Trim().Length == 0 && Prixsup.Trim().Length != 0 &&
               inf.Trim().Length == 0 && sup.Trim().Length != 0)
            {

                //int prixi = Convert.ToInt32(Prixinf);
                int prixs = Convert.ToInt32(Prixsup);
                //DateTime inft = Convert.ToDateTime(inf);
                DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (d.Nom == Produit
                                    //&& d.Prix >= prixi 
                                    && d.Prix <= prixs
                                    //&& e.DateAchat >= inft 
                                    && e.DateAchat <= supt
                              )
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }
            //00110110
            else if (Entreprise.Trim().Length == 0 && Contact.Trim().Length == 0 &&
               Categorie.Trim().Length != 0 && Produit.Trim().Length != 0 &&
               Prixinf.Trim().Length == 0 && Prixsup.Trim().Length != 0 &&
               inf.Trim().Length != 0 && sup.Trim().Length == 0)
            {

                //int prixi = Convert.ToInt32(Prixinf);
                int prixs = Convert.ToInt32(Prixsup);
                DateTime inft = Convert.ToDateTime(inf);
                //DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (d.Nom == Produit
                                    //&& d.Prix >= prixi 
                                    && d.Prix <= prixs
                                    && e.DateAchat >= inft 
                                   // && e.DateAchat <= supt
                              )
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }
            //00110111
            else if (Entreprise.Trim().Length == 0 && Contact.Trim().Length == 0 &&
               Categorie.Trim().Length != 0 && Produit.Trim().Length != 0 &&
               Prixinf.Trim().Length == 0 && Prixsup.Trim().Length != 0 &&
               inf.Trim().Length != 0 && sup.Trim().Length != 0)
            {

                //int prixi = Convert.ToInt32(Prixinf);
                int prixs = Convert.ToInt32(Prixsup);
                DateTime inft = Convert.ToDateTime(inf);
                DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (d.Nom == Produit
                                    //&& d.Prix >= prixi 
                                    && d.Prix <= prixs
                                    && e.DateAchat >= inft 
                                    && e.DateAchat <= supt
                              )
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }
            //00111000
            else if (Entreprise.Trim().Length == 0 && Contact.Trim().Length == 0 &&
               Categorie.Trim().Length != 0 && Produit.Trim().Length != 0 &&
               Prixinf.Trim().Length != 0 && Prixsup.Trim().Length == 0 &&
               inf.Trim().Length == 0 && sup.Trim().Length == 0)
            {

                int prixi = Convert.ToInt32(Prixinf);
                //int prixs = Convert.ToInt32(Prixsup);
                //DateTime inft = Convert.ToDateTime(inf);
                //DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (d.Nom == Produit
                                    && d.Prix >= prixi 
                                    //&& d.Prix <= prixs
                                    //&& e.DateAchat >= inft 
                                    //&& e.DateAchat <= supt
                              )
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }
            //00111001
            else if (Entreprise.Trim().Length == 0 && Contact.Trim().Length == 0 &&
               Categorie.Trim().Length != 0 && Produit.Trim().Length != 0 &&
               Prixinf.Trim().Length != 0 && Prixsup.Trim().Length == 0 &&
               inf.Trim().Length == 0 && sup.Trim().Length != 0)
            {

                int prixi = Convert.ToInt32(Prixinf);
                //int prixs = Convert.ToInt32(Prixsup);
                //DateTime inft = Convert.ToDateTime(inf);
                DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (d.Nom == Produit
                                    && d.Prix >= prixi
                              //&& d.Prix <= prixs
                              //&& e.DateAchat >= inft 
                              && e.DateAchat <= supt
                              )
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }
            //00111010
            else if (Entreprise.Trim().Length == 0 && Contact.Trim().Length == 0 &&
               Categorie.Trim().Length != 0 && Produit.Trim().Length != 0 &&
               Prixinf.Trim().Length != 0 && Prixsup.Trim().Length == 0 &&
               inf.Trim().Length != 0 && sup.Trim().Length == 0)
            {

                int prixi = Convert.ToInt32(Prixinf);
                //int prixs = Convert.ToInt32(Prixsup);
                DateTime inft = Convert.ToDateTime(inf);
                //DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (d.Nom == Produit
                                    && d.Prix >= prixi
                              //&& d.Prix <= prixs
                              && e.DateAchat >= inft 
                              //&& e.DateAchat <= supt
                              )
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }
            //00111011
            else if (Entreprise.Trim().Length == 0 && Contact.Trim().Length == 0 &&
               Categorie.Trim().Length != 0 && Produit.Trim().Length != 0 &&
               Prixinf.Trim().Length != 0 && Prixsup.Trim().Length == 0 &&
               inf.Trim().Length == 0 && sup.Trim().Length == 0)
            {

                int prixi = Convert.ToInt32(Prixinf);
                //int prixs = Convert.ToInt32(Prixsup);
                DateTime inft = Convert.ToDateTime(inf);
                DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (d.Nom == Produit
                                    && d.Prix >= prixi
                              //&& d.Prix <= prixs
                              && e.DateAchat >= inft 
                              && e.DateAchat <= supt
                              )
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }
            //00111100
            else if (Entreprise.Trim().Length == 0 && Contact.Trim().Length == 0 &&
               Categorie.Trim().Length != 0 && Produit.Trim().Length != 0 &&
               Prixinf.Trim().Length != 0 && Prixsup.Trim().Length != 0 &&
               inf.Trim().Length == 0 && sup.Trim().Length == 0)
            {

                int prixi = Convert.ToInt32(Prixinf);
                int prixs = Convert.ToInt32(Prixsup);
                //DateTime inft = Convert.ToDateTime(inf);
                //DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (d.Nom == Produit
                                    && d.Prix >= prixi
                                    && d.Prix <= prixs
                              //&& e.DateAchat >= inft 
                              //&& e.DateAchat <= supt
                              )
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }
            //00111101
            else if (Entreprise.Trim().Length == 0 && Contact.Trim().Length == 0 &&
               Categorie.Trim().Length != 0 && Produit.Trim().Length != 0 &&
               Prixinf.Trim().Length != 0 && Prixsup.Trim().Length != 0 &&
               inf.Trim().Length == 0 && sup.Trim().Length != 0)
            {

                int prixi = Convert.ToInt32(Prixinf);
                int prixs = Convert.ToInt32(Prixsup);
                //DateTime inft = Convert.ToDateTime(inf);
                DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (d.Nom == Produit
                                    && d.Prix >= prixi
                                    && d.Prix <= prixs
                              //&& e.DateAchat >= inft 
                                    && e.DateAchat <= supt
                              )
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }
            //00111110
            else if (Entreprise.Trim().Length == 0 && Contact.Trim().Length == 0 &&
               Categorie.Trim().Length != 0 && Produit.Trim().Length != 0 &&
               Prixinf.Trim().Length != 0 && Prixsup.Trim().Length != 0 &&
               inf.Trim().Length != 0 && sup.Trim().Length == 0)
            {

                int prixi = Convert.ToInt32(Prixinf);
                int prixs = Convert.ToInt32(Prixsup);
                DateTime inft = Convert.ToDateTime(inf);
                //DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (d.Nom == Produit
                                    && d.Prix >= prixi
                                    && d.Prix <= prixs
                                    && e.DateAchat >= inft 
                              //&& e.DateAchat <= supt
                              )
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }
            //00111111
            else if (Entreprise.Trim().Length == 0 && Contact.Trim().Length == 0 &&
               Categorie.Trim().Length != 0 && Produit.Trim().Length != 0 &&
               Prixinf.Trim().Length != 0 && Prixsup.Trim().Length != 0 &&
               inf.Trim().Length != 0 && sup.Trim().Length != 0)
            {

                int prixi = Convert.ToInt32(Prixinf);
                int prixs = Convert.ToInt32(Prixsup);
                DateTime inft = Convert.ToDateTime(inf);
                DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (d.Nom == Produit
                                    && d.Prix >= prixi
                                      && d.Prix <= prixs
                                      && e.DateAchat >= inft
                                      && e.DateAchat <= supt
                              )
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }

            //11000000
            else if (Entreprise.Trim().Length != 0 && Contact.Trim().Length != 0 &&
               Categorie.Trim().Length == 0 && Produit.Trim().Length == 0 &&
               Prixinf.Trim().Length == 0 && Prixsup.Trim().Length == 0 &&
               inf.Trim().Length == 0 && sup.Trim().Length == 0)
            {

                //int prixi = Convert.ToInt32(Prixinf);
                //int prixs = Convert.ToInt32(Prixsup);
                //DateTime inft = Convert.ToDateTime(inf);
                //DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (b.Contact_name == Contact
                                      //  d.Nom == Produit
                                      //  && d.Prix >= prixi
                                      //&& d.Prix <= prixs
                                      //&& e.DateAchat >= inft
                                      //&& e.DateAchat <= supt
                              )
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }

            //11000001
            else if (Entreprise.Trim().Length != 0 && Contact.Trim().Length != 0 &&
               Categorie.Trim().Length == 0 && Produit.Trim().Length == 0 &&
               Prixinf.Trim().Length == 0 && Prixsup.Trim().Length == 0 &&
               inf.Trim().Length == 0 && sup.Trim().Length != 0)
            {

                //int prixi = Convert.ToInt32(Prixinf);
                //int prixs = Convert.ToInt32(Prixsup);
                //DateTime inft = Convert.ToDateTime(inf);
                DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (b.Contact_name == Contact
                              //  d.Nom == Produit
                              //  && d.Prix >= prixi
                              //&& d.Prix <= prixs
                              //&& e.DateAchat >= inft
                              && e.DateAchat <= supt
                              )
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }

            //11000010
            else if (Entreprise.Trim().Length != 0 && Contact.Trim().Length != 0 &&
               Categorie.Trim().Length == 0 && Produit.Trim().Length == 0 &&
               Prixinf.Trim().Length == 0 && Prixsup.Trim().Length == 0 &&
               inf.Trim().Length != 0 && sup.Trim().Length == 0)
            {

                //int prixi = Convert.ToInt32(Prixinf);
                //int prixs = Convert.ToInt32(Prixsup);
                DateTime inft = Convert.ToDateTime(inf);
                //DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (b.Contact_name == Contact
                              //  d.Nom == Produit
                              //  && d.Prix >= prixi
                              //&& d.Prix <= prixs
                              && e.DateAchat >= inft
                              //&& e.DateAchat <= supt
                              )
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }
            //11000011
            else if (Entreprise.Trim().Length != 0 && Contact.Trim().Length != 0 &&
               Categorie.Trim().Length == 0 && Produit.Trim().Length == 0 &&
               Prixinf.Trim().Length == 0 && Prixsup.Trim().Length == 0 &&
               inf.Trim().Length != 0 && sup.Trim().Length != 0)
            {

                //int prixi = Convert.ToInt32(Prixinf);
                //int prixs = Convert.ToInt32(Prixsup);
                DateTime inft = Convert.ToDateTime(inf);
                DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (b.Contact_name == Contact
                              //  d.Nom == Produit
                              //  && d.Prix >= prixi
                             // && d.Prix <= prixs
                              && e.DateAchat >= inft
                              && e.DateAchat <= supt
                              )
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }

            //11000100
            else if (Entreprise.Trim().Length != 0 && Contact.Trim().Length != 0 &&
               Categorie.Trim().Length == 0 && Produit.Trim().Length == 0 &&
               Prixinf.Trim().Length == 0 && Prixsup.Trim().Length != 0 &&
               inf.Trim().Length == 0 && sup.Trim().Length == 0)
            {

                //int prixi = Convert.ToInt32(Prixinf);
                int prixs = Convert.ToInt32(Prixsup);
                //DateTime inft = Convert.ToDateTime(inf);
                //DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (b.Contact_name == Contact
                              //  d.Nom == Produit
                              //  && d.Prix >= prixi
                              && d.Prix <= prixs
                              //&& e.DateAchat >= inft
                              //&& e.DateAchat <= supt
                              )
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }
            //11000101
            else if (Entreprise.Trim().Length != 0 && Contact.Trim().Length != 0 &&
               Categorie.Trim().Length == 0 && Produit.Trim().Length == 0 &&
               Prixinf.Trim().Length == 0 && Prixsup.Trim().Length != 0 &&
               inf.Trim().Length == 0 && sup.Trim().Length != 0)
            {

                //int prixi = Convert.ToInt32(Prixinf);
                int prixs = Convert.ToInt32(Prixsup);
                //DateTime inft = Convert.ToDateTime(inf);
                DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (b.Contact_name == Contact
                              //  d.Nom == Produit
                              //  && d.Prix >= prixi
                              && d.Prix <= prixs
                              //&& e.DateAchat >= inft
                              && e.DateAchat <= supt
                              )
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }

            //11000110
            else if (Entreprise.Trim().Length != 0 && Contact.Trim().Length != 0 &&
               Categorie.Trim().Length == 0 && Produit.Trim().Length == 0 &&
               Prixinf.Trim().Length == 0 && Prixsup.Trim().Length != 0 &&
               inf.Trim().Length != 0 && sup.Trim().Length == 0)
            {

                //int prixi = Convert.ToInt32(Prixinf);
                int prixs = Convert.ToInt32(Prixsup);
                DateTime inft = Convert.ToDateTime(inf);
                //DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (b.Contact_name == Contact
                              //  d.Nom == Produit
                              //  && d.Prix >= prixi
                              && d.Prix <= prixs
                              && e.DateAchat >= inft
                              //&& e.DateAchat <= supt
                              )
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }

            //11000111
            else if (Entreprise.Trim().Length != 0 && Contact.Trim().Length != 0 &&
               Categorie.Trim().Length == 0 && Produit.Trim().Length == 0 &&
               Prixinf.Trim().Length == 0 && Prixsup.Trim().Length != 0 &&
               inf.Trim().Length != 0 && sup.Trim().Length != 0)
            {

                //int prixi = Convert.ToInt32(Prixinf);
                int prixs = Convert.ToInt32(Prixsup);
                DateTime inft = Convert.ToDateTime(inf);
                DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (b.Contact_name == Contact
                              //  d.Nom == Produit
                              //  && d.Prix >= prixi
                              && d.Prix <= prixs
                              && e.DateAchat >= inft
                              && e.DateAchat <= supt
                              )
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }

            //11001000
            else if (Entreprise.Trim().Length != 0 && Contact.Trim().Length != 0 &&
               Categorie.Trim().Length == 0 && Produit.Trim().Length == 0 &&
               Prixinf.Trim().Length != 0 && Prixsup.Trim().Length == 0 &&
               inf.Trim().Length == 0 && sup.Trim().Length == 0)
            {

                int prixi = Convert.ToInt32(Prixinf);
                //int prixs = Convert.ToInt32(Prixsup);
                //DateTime inft = Convert.ToDateTime(inf);
                //DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (b.Contact_name == Contact
                              //  d.Nom == Produit
                                   && d.Prix >= prixi
                              //&& d.Prix <= prixs
                              //&& e.DateAchat >= inft
                              //&& e.DateAchat <= supt
                              )
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }

            //11001001
            else if (Entreprise.Trim().Length != 0 && Contact.Trim().Length != 0 &&
               Categorie.Trim().Length == 0 && Produit.Trim().Length == 0 &&
               Prixinf.Trim().Length != 0 && Prixsup.Trim().Length == 0 &&
               inf.Trim().Length == 0 && sup.Trim().Length != 0)
            {

                int prixi = Convert.ToInt32(Prixinf);
                //int prixs = Convert.ToInt32(Prixsup);
                //DateTime inft = Convert.ToDateTime(inf);
                DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (b.Contact_name == Contact
                                //  d.Nom == Produit
                                && d.Prix >= prixi
                              //&& d.Prix <= prixs
                              //&& e.DateAchat >= inft
                              && e.DateAchat <= supt
                              )
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }

            //11001010
            else if (Entreprise.Trim().Length != 0 && Contact.Trim().Length != 0 &&
               Categorie.Trim().Length == 0 && Produit.Trim().Length == 0 &&
               Prixinf.Trim().Length != 0 && Prixsup.Trim().Length == 0 &&
               inf.Trim().Length != 0 && sup.Trim().Length == 0)
            {

                int prixi = Convert.ToInt32(Prixinf);
                //int prixs = Convert.ToInt32(Prixsup);
                DateTime inft = Convert.ToDateTime(inf);
                //DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (b.Contact_name == Contact
                                //  d.Nom == Produit
                                && d.Prix >= prixi
                              //&& d.Prix <= prixs
                              && e.DateAchat >= inft
                              //&& e.DateAchat <= supt
                              )
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }

            //11001011
            else if (Entreprise.Trim().Length != 0 && Contact.Trim().Length != 0 &&
               Categorie.Trim().Length == 0 && Produit.Trim().Length == 0 &&
               Prixinf.Trim().Length != 0 && Prixsup.Trim().Length == 0 &&
               inf.Trim().Length != 0 && sup.Trim().Length != 0)
            {

                int prixi = Convert.ToInt32(Prixinf);
                //int prixs = Convert.ToInt32(Prixsup);
                DateTime inft = Convert.ToDateTime(inf);
                DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (b.Contact_name == Contact
                                //  d.Nom == Produit
                                && d.Prix >= prixi
                              //&& d.Prix <= prixs
                              && e.DateAchat >= inft
                              && e.DateAchat <= supt
                              )
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }

            //11001100
            else if (Entreprise.Trim().Length != 0 && Contact.Trim().Length != 0 &&
               Categorie.Trim().Length == 0 && Produit.Trim().Length == 0 &&
               Prixinf.Trim().Length != 0 && Prixsup.Trim().Length != 0 &&
               inf.Trim().Length == 0 && sup.Trim().Length == 0)
            {

                int prixi = Convert.ToInt32(Prixinf);
                int prixs = Convert.ToInt32(Prixsup);
                //DateTime inft = Convert.ToDateTime(inf);
                //DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (b.Contact_name == Contact
                                //  d.Nom == Produit
                                && d.Prix >= prixi
                              && d.Prix <= prixs
                              //&& e.DateAchat >= inft
                              //&& e.DateAchat <= supt
                              )
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }

            //11001101
            else if (Entreprise.Trim().Length != 0 && Contact.Trim().Length != 0 &&
               Categorie.Trim().Length == 0 && Produit.Trim().Length == 0 &&
               Prixinf.Trim().Length != 0 && Prixsup.Trim().Length != 0 &&
               inf.Trim().Length == 0 && sup.Trim().Length != 0)
            {

                int prixi = Convert.ToInt32(Prixinf);
                int prixs = Convert.ToInt32(Prixsup);
                //DateTime inft = Convert.ToDateTime(inf);
                DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (b.Contact_name == Contact
                                //  d.Nom == Produit
                                && d.Prix >= prixi
                              && d.Prix <= prixs
                              //&& e.DateAchat >= inft
                              && e.DateAchat <= supt
                              )
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }

            //11001110
            else if (Entreprise.Trim().Length != 0 && Contact.Trim().Length != 0 &&
               Categorie.Trim().Length == 0 && Produit.Trim().Length == 0 &&
               Prixinf.Trim().Length != 0 && Prixsup.Trim().Length != 0 &&
               inf.Trim().Length != 0 && sup.Trim().Length == 0)
            {

                int prixi = Convert.ToInt32(Prixinf);
                int prixs = Convert.ToInt32(Prixsup);
                DateTime inft = Convert.ToDateTime(inf);
                //DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (b.Contact_name == Contact
                                //  d.Nom == Produit
                                && d.Prix >= prixi
                              && d.Prix <= prixs
                              && e.DateAchat >= inft
                              //&& e.DateAchat <= supt
                              )
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }

            //11001111
            else if (Entreprise.Trim().Length != 0 && Contact.Trim().Length != 0 &&
               Categorie.Trim().Length == 0 && Produit.Trim().Length == 0 &&
               Prixinf.Trim().Length != 0 && Prixsup.Trim().Length != 0 &&
               inf.Trim().Length != 0 && sup.Trim().Length != 0)
            {

                int prixi = Convert.ToInt32(Prixinf);
                int prixs = Convert.ToInt32(Prixsup);
                DateTime inft = Convert.ToDateTime(inf);
                DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (b.Contact_name == Contact
                                //  d.Nom == Produit
                                      && d.Prix >= prixi
                                      && d.Prix <= prixs
                                      && e.DateAchat >= inft
                                      && e.DateAchat <= supt
                              )
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }

            //11110000
            else if (Entreprise.Trim().Length != 0 && Contact.Trim().Length != 0 &&
               Categorie.Trim().Length != 0 && Produit.Trim().Length != 0 &&
               Prixinf.Trim().Length == 0 && Prixsup.Trim().Length == 0 &&
               inf.Trim().Length == 0 && sup.Trim().Length == 0)
            {

                //int prixi = Convert.ToInt32(Prixinf);
                //int prixs = Convert.ToInt32(Prixsup);
                //DateTime inft = Convert.ToDateTime(inf);
                //DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (b.Contact_name == Contact
                                    && d.Nom == Produit
                              //  && d.Prix >= prixi
                              //&& d.Prix <= prixs
                              //&& e.DateAchat >= inft
                              //&& e.DateAchat <= supt
                              )
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }

            //11110001
            else if (Entreprise.Trim().Length != 0 && Contact.Trim().Length != 0 &&
               Categorie.Trim().Length != 0 && Produit.Trim().Length != 0 &&
               Prixinf.Trim().Length == 0 && Prixsup.Trim().Length == 0 &&
               inf.Trim().Length == 0 && sup.Trim().Length != 0)
            {

                //int prixi = Convert.ToInt32(Prixinf);
                //int prixs = Convert.ToInt32(Prixsup);
                //DateTime inft = Convert.ToDateTime(inf);
                DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (b.Contact_name == Contact
                                    && d.Nom == Produit
                              //  && d.Prix >= prixi
                              //&& d.Prix <= prixs
                              //&& e.DateAchat >= inft
                              && e.DateAchat <= supt
                              )
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }

            //11110010
            else if (Entreprise.Trim().Length != 0 && Contact.Trim().Length != 0 &&
               Categorie.Trim().Length != 0 && Produit.Trim().Length != 0 &&
               Prixinf.Trim().Length == 0 && Prixsup.Trim().Length == 0 &&
               inf.Trim().Length != 0 && sup.Trim().Length == 0)
            {

                //int prixi = Convert.ToInt32(Prixinf);
                //int prixs = Convert.ToInt32(Prixsup);
                DateTime inft = Convert.ToDateTime(inf);
                //DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (b.Contact_name == Contact
                                    && d.Nom == Produit
                              //  && d.Prix >= prixi
                              //&& d.Prix <= prixs
                              && e.DateAchat >= inft
                              //&& e.DateAchat <= supt
                              )
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }

            //11110011
            else if (Entreprise.Trim().Length != 0 && Contact.Trim().Length != 0 &&
               Categorie.Trim().Length != 0 && Produit.Trim().Length != 0 &&
               Prixinf.Trim().Length == 0 && Prixsup.Trim().Length == 0 &&
               inf.Trim().Length != 0 && sup.Trim().Length != 0)
            {

                //int prixi = Convert.ToInt32(Prixinf);
                //int prixs = Convert.ToInt32(Prixsup);
                DateTime inft = Convert.ToDateTime(inf);
                DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (b.Contact_name == Contact
                                    && d.Nom == Produit
                              //  && d.Prix >= prixi
                              //&& d.Prix <= prixs
                              && e.DateAchat >= inft
                              && e.DateAchat <= supt
                              )
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }

            //11110100
            else if (Entreprise.Trim().Length != 0 && Contact.Trim().Length != 0 &&
               Categorie.Trim().Length != 0 && Produit.Trim().Length != 0 &&
               Prixinf.Trim().Length == 0 && Prixsup.Trim().Length != 0 &&
               inf.Trim().Length == 0 && sup.Trim().Length == 0)
            {

                //int prixi = Convert.ToInt32(Prixinf);
                int prixs = Convert.ToInt32(Prixsup);
                //DateTime inft = Convert.ToDateTime(inf);
                //DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (b.Contact_name == Contact
                                    && d.Nom == Produit
                              //  && d.Prix >= prixi
                              && d.Prix <= prixs
                              //&& e.DateAchat >= inft
                              //&& e.DateAchat <= supt
                              )
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }

            //11110101
            else if (Entreprise.Trim().Length != 0 && Contact.Trim().Length != 0 &&
               Categorie.Trim().Length != 0 && Produit.Trim().Length != 0 &&
               Prixinf.Trim().Length == 0 && Prixsup.Trim().Length != 0 &&
               inf.Trim().Length == 0 && sup.Trim().Length != 0)
            {

                //int prixi = Convert.ToInt32(Prixinf);
                int prixs = Convert.ToInt32(Prixsup);
                //DateTime inft = Convert.ToDateTime(inf);
                DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (b.Contact_name == Contact
                                    && d.Nom == Produit
                              //  && d.Prix >= prixi
                              && d.Prix <= prixs
                              //&& e.DateAchat >= inft
                              && e.DateAchat <= supt
                              )
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }

            //11110110
            else if (Entreprise.Trim().Length != 0 && Contact.Trim().Length != 0 &&
               Categorie.Trim().Length != 0 && Produit.Trim().Length != 0 &&
               Prixinf.Trim().Length == 0 && Prixsup.Trim().Length != 0 &&
               inf.Trim().Length != 0 && sup.Trim().Length == 0)
            {

                //int prixi = Convert.ToInt32(Prixinf);
                int prixs = Convert.ToInt32(Prixsup);
                DateTime inft = Convert.ToDateTime(inf);
                //DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (b.Contact_name == Contact
                                    && d.Nom == Produit
                              //  && d.Prix >= prixi
                              && d.Prix <= prixs
                              && e.DateAchat >= inft
                              //&& e.DateAchat <= supt
                              )
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }

            //11110111
            else if (Entreprise.Trim().Length != 0 && Contact.Trim().Length != 0 &&
               Categorie.Trim().Length != 0 && Produit.Trim().Length != 0 &&
               Prixinf.Trim().Length == 0 && Prixsup.Trim().Length != 0 &&
               inf.Trim().Length != 0 && sup.Trim().Length != 0)
            {

                //int prixi = Convert.ToInt32(Prixinf);
                int prixs = Convert.ToInt32(Prixsup);
                DateTime inft = Convert.ToDateTime(inf);
                DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (b.Contact_name == Contact
                                    && d.Nom == Produit
                              //  && d.Prix >= prixi
                              && d.Prix <= prixs
                              && e.DateAchat >= inft
                              && e.DateAchat <= supt
                              )
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }

            //11111000
            else if (Entreprise.Trim().Length != 0 && Contact.Trim().Length != 0 &&
               Categorie.Trim().Length != 0 && Produit.Trim().Length != 0 &&
               Prixinf.Trim().Length != 0 && Prixsup.Trim().Length == 0 &&
               inf.Trim().Length == 0 && sup.Trim().Length == 0)
            {

                int prixi = Convert.ToInt32(Prixinf);
                //int prixs = Convert.ToInt32(Prixsup);
                //DateTime inft = Convert.ToDateTime(inf);
                //DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (b.Contact_name == Contact
                                    && d.Nom == Produit
                                && d.Prix >= prixi
                              //&& d.Prix <= prixs
                              //&& e.DateAchat >= inft
                              //&& e.DateAchat <= supt
                              )
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }

            //11111001
            else if (Entreprise.Trim().Length != 0 && Contact.Trim().Length != 0 &&
               Categorie.Trim().Length != 0 && Produit.Trim().Length != 0 &&
               Prixinf.Trim().Length != 0 && Prixsup.Trim().Length == 0 &&
               inf.Trim().Length == 0 && sup.Trim().Length != 0)
            {

                int prixi = Convert.ToInt32(Prixinf);
                //int prixs = Convert.ToInt32(Prixsup);
                //DateTime inft = Convert.ToDateTime(inf);
                DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (b.Contact_name == Contact
                                    && d.Nom == Produit
                                && d.Prix >= prixi
                              //&& d.Prix <= prixs
                              //&& e.DateAchat >= inft
                              && e.DateAchat <= supt
                              )
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }

            //11111010
            else if (Entreprise.Trim().Length != 0 && Contact.Trim().Length != 0 &&
               Categorie.Trim().Length != 0 && Produit.Trim().Length != 0 &&
               Prixinf.Trim().Length != 0 && Prixsup.Trim().Length == 0 &&
               inf.Trim().Length != 0 && sup.Trim().Length == 0)
            {

                int prixi = Convert.ToInt32(Prixinf);
                //int prixs = Convert.ToInt32(Prixsup);
                DateTime inft = Convert.ToDateTime(inf);
                //DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (b.Contact_name == Contact
                                    && d.Nom == Produit
                                && d.Prix >= prixi
                              //&& d.Prix <= prixs
                              && e.DateAchat >= inft
                              //&& e.DateAchat <= supt
                              )
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }

            //11111011
            else if (Entreprise.Trim().Length != 0 && Contact.Trim().Length != 0 &&
               Categorie.Trim().Length != 0 && Produit.Trim().Length != 0 &&
               Prixinf.Trim().Length != 0 && Prixsup.Trim().Length == 0 &&
               inf.Trim().Length != 0 && sup.Trim().Length != 0)
            {

                int prixi = Convert.ToInt32(Prixinf);
                //int prixs = Convert.ToInt32(Prixsup);
                DateTime inft = Convert.ToDateTime(inf);
                DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (b.Contact_name == Contact
                                    && d.Nom == Produit
                                && d.Prix >= prixi
                              //&& d.Prix <= prixs
                              && e.DateAchat >= inft
                              && e.DateAchat <= supt
                              )
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }

            //11111100
            else if (Entreprise.Trim().Length != 0 && Contact.Trim().Length != 0 &&
               Categorie.Trim().Length != 0 && Produit.Trim().Length != 0 &&
               Prixinf.Trim().Length != 0 && Prixsup.Trim().Length != 0 &&
               inf.Trim().Length == 0 && sup.Trim().Length == 0)
            {

                int prixi = Convert.ToInt32(Prixinf);
                int prixs = Convert.ToInt32(Prixsup);
                //DateTime inft = Convert.ToDateTime(inf);
                //DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (b.Contact_name == Contact
                                    && d.Nom == Produit
                                      && d.Prix >= prixi
                                     && d.Prix <= prixs
                              //&& e.DateAchat >= inft
                              //&& e.DateAchat <= supt
                              )
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }

            //11111101
            else if (Entreprise.Trim().Length != 0 && Contact.Trim().Length != 0 &&
               Categorie.Trim().Length != 0 && Produit.Trim().Length != 0 &&
               Prixinf.Trim().Length != 0 && Prixsup.Trim().Length != 0 &&
               inf.Trim().Length == 0 && sup.Trim().Length != 0)
            {

                int prixi = Convert.ToInt32(Prixinf);
                int prixs = Convert.ToInt32(Prixsup);
                //DateTime inft = Convert.ToDateTime(inf);
                DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (b.Contact_name == Contact
                                    && d.Nom == Produit
                                      && d.Prix >= prixi
                                     && d.Prix <= prixs
                              //&& e.DateAchat >= inft
                              && e.DateAchat <= supt
                              )
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }

            //11111110
            else if (Entreprise.Trim().Length != 0 && Contact.Trim().Length != 0 &&
               Categorie.Trim().Length != 0 && Produit.Trim().Length != 0 &&
               Prixinf.Trim().Length != 0 && Prixsup.Trim().Length != 0 &&
               inf.Trim().Length != 0 && sup.Trim().Length == 0)
            {

                int prixi = Convert.ToInt32(Prixinf);
                int prixs = Convert.ToInt32(Prixsup);
                DateTime inft = Convert.ToDateTime(inf);
                //DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (b.Contact_name == Contact
                                    && d.Nom == Produit
                                      && d.Prix >= prixi
                                     && d.Prix <= prixs
                                        && e.DateAchat >= inft
                              //&& e.DateAchat <= supt
                              )
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }

            //11111111
            else if (Entreprise.Trim().Length != 0 && Contact.Trim().Length != 0 &&
               Categorie.Trim().Length != 0 && Produit.Trim().Length != 0 &&
               Prixinf.Trim().Length != 0 && Prixsup.Trim().Length != 0 &&
               inf.Trim().Length == 0 && sup.Trim().Length == 0)
            {

                int prixi = Convert.ToInt32(Prixinf);
                int prixs = Convert.ToInt32(Prixsup);
                DateTime inft = Convert.ToDateTime(inf);
                DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             from c in db.CategorieProduits
                             join d in db.Articles on c.Id equals d.CategorieProduit_
                             join e in db.VenteArticles on d.Idp equals e.Article_
                             where (b.Contact_name == Contact
                                    && d.Nom == Produit
                                      && d.Prix >= prixi
                                     && d.Prix <= prixs
                              && e.DateAchat >= inft
                              && e.DateAchat <= supt
                              )
                             select new
                             {
                                 NomClient = b.Contact_name,
                                 NomEntreprise = a.Nom,
                                 NomCategorie = c.Nom,
                                 NomProduit = d.Nom,
                                 Prix = d.Prix,
                                 DateVente = e.DateAchat
                             }).ToList().Select(x => new VenteProduitsViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomClient = x.NomClient,
                                 NomCategorie = x.NomCategorie,
                                 NomProduit = x.NomProduit,
                                 Prix = x.Prix,
                                 DateVente = x.DateVente
                             });
            }

            return (ListEmpty);
        }

      
    }
}