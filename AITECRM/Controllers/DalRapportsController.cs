using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AITECRM.ViewModels;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using AITECRM.Models;

namespace AITECRM.Controllers
{
    [Authorize(Roles = ("Admin,Rapports,Semi_Admin"))]
    public class DalRapportsController : Controller
    {
        private DAL RetriveData;
        private AITECRMContext db;
        private ReportDocument rd; 
        public DalRapportsController()
        {
            db = new AITECRMContext();
            RetriveData = new DAL();
        }

     

        public IEnumerable<EntreprisesReportsViewModel> getEntreprises(string pays, string inf, string sup)
        {
            IEnumerable<EntreprisesReportsViewModel> listEmpty = new List<EntreprisesReportsViewModel>();

            //Rien n'est rempli
            if (pays.Trim().Length == 0 && inf.Trim().Length == 0 && sup.Trim().Length == 0)
            {
                 listEmpty = (from a in db.Entreprises
                                 select new
                                 {
                                     NomEntreprise = a.Nom,
                                     Pays = a.Pays,
                                     adresse = a.Adresse,
                                     codePostal = a.Code_postal,
                                     Numero = a.Telephone,
                                     Email = a.Adresse_Email,
                                     Date_Enregistrement = a.Date_enregistrement
                                 }).ToList().Select(x => new EntreprisesReportsViewModel()
                                 {
                                     Nom = x.NomEntreprise,
                                     Pays = x.Pays,
                                     Adresse_Email = x.Email,
                                     Code_postal = x.codePostal,
                                     Telephone = x.Numero,
                                     Date_enregistrement = x.Date_Enregistrement
                                 });
            }
            //Pays uniquement
            else if (inf.Trim().Length == 0 && sup.Trim().Length == 0 && pays.Trim().Length != 0)
            {

                listEmpty = (from a in db.Entreprises
                             where a.Pays == pays
                             select new
                             {
                                 NomEntreprise = a.Nom,
                                 Pays = a.Pays,
                                 adresse = a.Adresse,
                                 codePostal = a.Code_postal,
                                 Numero = a.Telephone,
                                 Email = a.Adresse_Email,
                                 Date_Enregistrement = a.Date_enregistrement
                             }).ToList().Select(x => new EntreprisesReportsViewModel()
                             {
                                 Nom = x.NomEntreprise,
                                 Pays = x.Pays,
                                 Adresse_Email = x.Email,
                                 Code_postal = x.codePostal,
                                 Telephone = x.Numero,
                                 Date_enregistrement = x.Date_Enregistrement
                             });

            }
            //2 dates
            else if (pays.Trim().Length == 0 && inf.Trim().Length != 0 && sup.Trim().Length != 0)
            {
                DateTime inft = Convert.ToDateTime(inf); DateTime supt = Convert.ToDateTime(sup);
                listEmpty = (from a in db.Entreprises
                             where (a.Date_enregistrement >= inft && a.Date_enregistrement <= supt)
                             select new
                             {
                                 NomEntreprise = a.Nom,
                                 Pays = a.Pays,
                                 adresse = a.Adresse,
                                 codePostal = a.Code_postal,
                                 Numero = a.Telephone,
                                 Email = a.Adresse_Email,
                                 Date_Enregistrement = a.Date_enregistrement
                             }).ToList().Select(x => new EntreprisesReportsViewModel()
                             {
                                 Nom = x.NomEntreprise,
                                 Pays = x.Pays,
                                 Adresse_Email = x.Email,
                                 Code_postal = x.codePostal,
                                 Telephone = x.Numero,
                                 Date_enregistrement = x.Date_Enregistrement
                             });
            }
            //date inf uniquement
            else if (pays.Trim().Length == 0 && inf.Trim().Length != 0 && sup.Trim().Length == 0)
            {
                DateTime inft = Convert.ToDateTime(inf);
                listEmpty = (from a in db.Entreprises
                             where (a.Date_enregistrement >= inft)
                             select new
                             {
                                 NomEntreprise = a.Nom,
                                 Pays = a.Pays,
                                 adresse = a.Adresse,
                                 codePostal = a.Code_postal,
                                 Numero = a.Telephone,
                                 Email = a.Adresse_Email,
                                 Date_Enregistrement = a.Date_enregistrement
                             }).ToList().Select(x => new EntreprisesReportsViewModel()
                             {
                                 Nom = x.NomEntreprise,
                                 Pays = x.Pays,
                                 Adresse_Email = x.Email,
                                 Code_postal = x.codePostal,
                                 Telephone = x.Numero,
                                 Date_enregistrement = x.Date_Enregistrement
                             });
            }
            // tout est rempli
            else if (sup.Trim().Length != 0 && pays.Trim().Length != 0 && inf.Trim().Length != 0)
            {
                DateTime inft = Convert.ToDateTime(inf); DateTime supt = Convert.ToDateTime(sup);
                listEmpty = (from a in db.Entreprises
                             where (a.Date_enregistrement >= inft && a.Date_enregistrement <= supt && a.Pays == pays)
                             select new
                             {
                                 NomEntreprise = a.Nom,
                                 Pays = a.Pays,
                                 adresse = a.Adresse,
                                 codePostal = a.Code_postal,
                                 Numero = a.Telephone,
                                 Email = a.Adresse_Email,
                                 Date_Enregistrement = a.Date_enregistrement
                             }).ToList().Select(x => new EntreprisesReportsViewModel()
                             {
                                 Nom = x.NomEntreprise,
                                 Pays = x.Pays,
                                 Adresse_Email = x.Email,
                                 Code_postal = x.codePostal,
                                 Telephone = x.Numero,
                                 Date_enregistrement = x.Date_Enregistrement
                             });
            }
            // supt uniquement
            else if (sup.Trim().Length != 0 && pays.Trim().Length == 0 && inf.Trim().Length == 0)
            {
                DateTime supt = Convert.ToDateTime(sup);
                listEmpty = (from a in db.Entreprises
                             where (a.Date_enregistrement <= supt)
                             select new
                             {
                                 NomEntreprise = a.Nom,
                                 Pays = a.Pays,
                                 adresse = a.Adresse,
                                 codePostal = a.Code_postal,
                                 Numero = a.Telephone,
                                 Email = a.Adresse_Email,
                                 Date_Enregistrement = a.Date_enregistrement
                             }).ToList().Select(x => new EntreprisesReportsViewModel()
                             {
                                 Nom = x.NomEntreprise,
                                 Pays = x.Pays,
                                 Adresse_Email = x.Email,
                                 Code_postal = x.codePostal,
                                 Telephone = x.Numero,
                                 Date_enregistrement = x.Date_Enregistrement
                             });
            }
            //supt et pays uniquement
            else if (sup.Trim().Length != 0 && pays.Trim().Length != 0 && inf.Trim().Length == 0)
            {
                DateTime supt = Convert.ToDateTime(sup);
                listEmpty = (from a in db.Entreprises
                             where (a.Date_enregistrement <= supt && a.Pays == pays)
                             select new
                             {
                                 NomEntreprise = a.Nom,
                                 Pays = a.Pays,
                                 adresse = a.Adresse,
                                 codePostal = a.Code_postal,
                                 Numero = a.Telephone,
                                 Email = a.Adresse_Email,
                                 Date_Enregistrement = a.Date_enregistrement
                             }).ToList().Select(x => new EntreprisesReportsViewModel()
                             {
                                 Nom = x.NomEntreprise,
                                 Pays = x.Pays,
                                 Adresse_Email = x.Email,
                                 Code_postal = x.codePostal,
                                 Telephone = x.Numero,
                                 Date_enregistrement = x.Date_Enregistrement
                             });
            }
            //pays et inft
            else if (sup.Trim().Length == 0 && pays.Trim().Length != 0 && inf.Trim().Length != 0)
            {
                DateTime inft = Convert.ToDateTime(inf);
                listEmpty = (from a in db.Entreprises
                             where (a.Date_enregistrement >= inft && a.Pays == pays)
                             select new
                             {
                                 NomEntreprise = a.Nom,
                                 Pays = a.Pays,
                                 adresse = a.Adresse,
                                 codePostal = a.Code_postal,
                                 Numero = a.Telephone,
                                 Email = a.Adresse_Email,
                                 Date_Enregistrement = a.Date_enregistrement
                             }).ToList().Select(x => new EntreprisesReportsViewModel()
                             {
                                 Nom = x.NomEntreprise,
                                 Pays = x.Pays,
                                 Adresse_Email = x.Email,
                                 Code_postal = x.codePostal,
                                 Telephone = x.Numero,
                                 Date_enregistrement = x.Date_Enregistrement
                             });
            }

            return (listEmpty);
        }

        public JsonResult getEntreprisesJs(string pays, string inf, string sup)
        {
            IEnumerable<EntreprisesReportsViewModel> empSummary = new List<EntreprisesReportsViewModel>();

            //Rien n'est rempli
            if (pays.Trim().Length == 0 && inf.Trim().Length == 0 && sup.Trim().Length == 0)
            {
                empSummary = (from a in db.Entreprises
                             select new
                             {
                                 NomEntreprise = a.Nom,
                                 Pays = a.Pays,
                                 adresse = a.Adresse,
                                 codePostal = a.Code_postal,
                                 Numero = a.Telephone,
                                 Email = a.Adresse_Email,
                                 Date_Enregistrement = a.Date_enregistrement
                             }).ToList().Select(x => new EntreprisesReportsViewModel()
                             {
                                 Nom = x.NomEntreprise,
                                 Pays = x.Pays,
                                 Adresse_Email = x.Email,
                                 Code_postal = x.codePostal,
                                 Telephone = x.Numero,
                                 Date_enregistrement = x.Date_Enregistrement
                             });
            }
            //Pays uniquement
            else if (inf.Trim().Length == 0 && sup.Trim().Length == 0 && pays.Trim().Length != 0)
            {

                empSummary = (from a in db.Entreprises
                             where a.Pays == pays
                             select new
                             {
                                 NomEntreprise = a.Nom,
                                 Pays = a.Pays,
                                 adresse = a.Adresse,
                                 codePostal = a.Code_postal,
                                 Numero = a.Telephone,
                                 Email = a.Adresse_Email,
                                 Date_Enregistrement = a.Date_enregistrement
                             }).ToList().Select(x => new EntreprisesReportsViewModel()
                             {
                                 Nom = x.NomEntreprise,
                                 Pays = x.Pays,
                                 Adresse_Email = x.Email,
                                 Code_postal = x.codePostal,
                                 Telephone = x.Numero,
                                 Date_enregistrement = x.Date_Enregistrement
                             });

            }
            //2 dates
            else if (pays.Trim().Length == 0 && inf.Trim().Length != 0 && sup.Trim().Length != 0)
            {
                DateTime inft = Convert.ToDateTime(inf); DateTime supt = Convert.ToDateTime(sup);
                empSummary = (from a in db.Entreprises
                             where (a.Date_enregistrement >= inft && a.Date_enregistrement <= supt)
                             select new
                             {
                                 NomEntreprise = a.Nom,
                                 Pays = a.Pays,
                                 adresse = a.Adresse,
                                 codePostal = a.Code_postal,
                                 Numero = a.Telephone,
                                 Email = a.Adresse_Email,
                                 Date_Enregistrement = a.Date_enregistrement
                             }).ToList().Select(x => new EntreprisesReportsViewModel()
                             {
                                 Nom = x.NomEntreprise,
                                 Pays = x.Pays,
                                 Adresse_Email = x.Email,
                                 Code_postal = x.codePostal,
                                 Telephone = x.Numero,
                                 Date_enregistrement = x.Date_Enregistrement
                             });
            }
            //date inf uniquement
            else if (pays.Trim().Length == 0 && inf.Trim().Length != 0 && sup.Trim().Length == 0)
            {
                DateTime inft = Convert.ToDateTime(inf);
                empSummary = (from a in db.Entreprises
                             where (a.Date_enregistrement >= inft)
                             select new
                             {
                                 NomEntreprise = a.Nom,
                                 Pays = a.Pays,
                                 adresse = a.Adresse,
                                 codePostal = a.Code_postal,
                                 Numero = a.Telephone,
                                 Email = a.Adresse_Email,
                                 Date_Enregistrement = a.Date_enregistrement
                             }).ToList().Select(x => new EntreprisesReportsViewModel()
                             {
                                 Nom = x.NomEntreprise,
                                 Pays = x.Pays,
                                 Adresse_Email = x.Email,
                                 Code_postal = x.codePostal,
                                 Telephone = x.Numero,
                                 Date_enregistrement = x.Date_Enregistrement
                             });
            }
            // tout est rempli
            else if (sup.Trim().Length != 0 && pays.Trim().Length != 0 && inf.Trim().Length != 0)
            {
                DateTime inft = Convert.ToDateTime(inf); DateTime supt = Convert.ToDateTime(sup);
                empSummary = (from a in db.Entreprises
                             where (a.Date_enregistrement >= inft && a.Date_enregistrement <= supt && a.Pays == pays)
                             select new
                             {
                                 NomEntreprise = a.Nom,
                                 Pays = a.Pays,
                                 adresse = a.Adresse,
                                 codePostal = a.Code_postal,
                                 Numero = a.Telephone,
                                 Email = a.Adresse_Email,
                                 Date_Enregistrement = a.Date_enregistrement
                             }).ToList().Select(x => new EntreprisesReportsViewModel()
                             {
                                 Nom = x.NomEntreprise,
                                 Pays = x.Pays,
                                 Adresse_Email = x.Email,
                                 Code_postal = x.codePostal,
                                 Telephone = x.Numero,
                                 Date_enregistrement = x.Date_Enregistrement
                             });
            }
            // supt uniquement
            else if (sup.Trim().Length != 0 && pays.Trim().Length == 0 && inf.Trim().Length == 0)
            {
                DateTime supt = Convert.ToDateTime(sup);
                empSummary = (from a in db.Entreprises
                             where (a.Date_enregistrement <= supt)
                             select new
                             {
                                 NomEntreprise = a.Nom,
                                 Pays = a.Pays,
                                 adresse = a.Adresse,
                                 codePostal = a.Code_postal,
                                 Numero = a.Telephone,
                                 Email = a.Adresse_Email,
                                 Date_Enregistrement = a.Date_enregistrement
                             }).ToList().Select(x => new EntreprisesReportsViewModel()
                             {
                                 Nom = x.NomEntreprise,
                                 Pays = x.Pays,
                                 Adresse_Email = x.Email,
                                 Code_postal = x.codePostal,
                                 Telephone = x.Numero,
                                 Date_enregistrement = x.Date_Enregistrement
                             });
            }
            //supt et pays uniquement
            else if (sup.Trim().Length != 0 && pays.Trim().Length != 0 && inf.Trim().Length == 0)
            {
                DateTime supt = Convert.ToDateTime(sup);
                empSummary = (from a in db.Entreprises
                             where (a.Date_enregistrement <= supt && a.Pays == pays)
                             select new
                             {
                                 NomEntreprise = a.Nom,
                                 Pays = a.Pays,
                                 adresse = a.Adresse,
                                 codePostal = a.Code_postal,
                                 Numero = a.Telephone,
                                 Email = a.Adresse_Email,
                                 Date_Enregistrement = a.Date_enregistrement
                             }).ToList().Select(x => new EntreprisesReportsViewModel()
                             {
                                 Nom = x.NomEntreprise,
                                 Pays = x.Pays,
                                 Adresse_Email = x.Email,
                                 Code_postal = x.codePostal,
                                 Telephone = x.Numero,
                                 Date_enregistrement = x.Date_Enregistrement
                             });
            }
            //pays et inft
            else if (sup.Trim().Length == 0 && pays.Trim().Length != 0 && inf.Trim().Length != 0)
            {
                DateTime inft = Convert.ToDateTime(inf);
                empSummary = (from a in db.Entreprises
                             where (a.Date_enregistrement >= inft && a.Pays == pays)
                             select new
                             {
                                 NomEntreprise = a.Nom,
                                 Pays = a.Pays,
                                 adresse = a.Adresse,
                                 codePostal = a.Code_postal,
                                 Numero = a.Telephone,
                                 Email = a.Adresse_Email,
                                 Date_Enregistrement = a.Date_enregistrement
                             }).ToList().Select(x => new EntreprisesReportsViewModel()
                             {
                                 Nom = x.NomEntreprise,
                                 Pays = x.Pays,
                                 Adresse_Email = x.Email,
                                 Code_postal = x.codePostal,
                                 Telephone = x.Numero,
                                 Date_enregistrement = x.Date_Enregistrement
                             });
            }

            return Json(empSummary, JsonRequestBehavior.AllowGet);
        }

        public IEnumerable<AllContactViewModel> getContacts(string pays, string inf,string sup)
        {
            IEnumerable<AllContactViewModel> ListEmpty = new List<AllContactViewModel>();

            //Rien n'est rempli
            if (inf.Trim().Length == 0 && pays.Trim().Length == 0 &&  sup.Trim().Length == 0)
            {
                 ListEmpty = (from gg in db.Entreprises
                           join c in db.Contacts on gg.Id equals c.Entreprise_
                           join e in db.ContactGroup on c.Contactgroups equals e.Id
                           join d in db.Activites on c.Id equals d.Contact_ into r
                           from d in r.DefaultIfEmpty()
                           join f in db.EtatContacts on c.Id equals f.ContactId into s
                           from f in s.DefaultIfEmpty()
                              select new
                           {
                               NomEntreprise = gg.Nom,
                               NomContact = c.Contact_name,
                               AdresseMail = c.Adresse_mail,
                               NomGroupeContact = e.Type_name,
                               Numero = c.Telephone,
                               EtatContacts_ = f == null ? string.Empty : f.Intitule
                           }).Select( x => new AllContactViewModel() {
                               NomEntreprise = x.NomEntreprise,
                               NomContact = x.NomContact,
                               AdresseMail = x.AdresseMail,
                               NomGroupeContact =x.NomGroupeContact,
                               Numero = x.Numero,
                               EtatContacts_ = x.EtatContacts_
                           });

                
            }

            //Pays uniquement
            else if (inf.Trim().Length == 0 && sup.Trim().Length == 0 && pays.Trim().Length != 0)
            {

                ListEmpty = (from gg in db.Entreprises
                             join c in db.Contacts on gg.Id equals c.Entreprise_
                             where c.Pays == pays
                             join e in db.ContactGroup on c.Contactgroups equals e.Id
                             join d in db.Activites on c.Id equals d.Contact_ into r
                             from d in r.DefaultIfEmpty()
                             join f in db.EtatContacts on c.Id equals f.ContactId into s
                             from f in s.DefaultIfEmpty()
                             select new
                             {
                                 NomEntreprise = gg.Nom,
                                 NomContact = c.Contact_name,
                                 AdresseMail = c.Adresse_mail,
                                 NomGroupeContact = e.Type_name,
                                 Numero = c.Telephone,
                                 EtatContacts_ = f == null ? string.Empty : f.Intitule
                             }).Select(x => new AllContactViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomContact = x.NomContact,
                                 AdresseMail = x.AdresseMail,
                                 NomGroupeContact = x.NomGroupeContact,
                                 Numero = x.Numero,
                                 EtatContacts_ = x.EtatContacts_
                             });

            }
            //2 dates
            else if (pays.Trim().Length == 0 && inf.Trim().Length != 0 && sup.Trim().Length != 0)
            {
                DateTime inft = Convert.ToDateTime(inf); DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from gg in db.Entreprises
                             join c in db.Contacts on gg.Id equals c.Entreprise_
                             where (c.Date_enregistrement >= inft && c.Date_enregistrement<= supt)
                             join e in db.ContactGroup on c.Contactgroups equals e.Id
                             join d in db.Activites on c.Id equals d.Contact_ into r
                             from d in r.DefaultIfEmpty()
                             join f in db.EtatContacts on c.Id equals f.ContactId into s
                             from f in s.DefaultIfEmpty()
                             select new
                             {
                                 NomEntreprise = gg.Nom,
                                 NomContact = c.Contact_name,
                                 AdresseMail = c.Adresse_mail,
                                 NomGroupeContact = e.Type_name,
                                 Numero = c.Telephone,
                                 EtatContacts_ = f == null ? string.Empty : f.Intitule
                             }).Select(x => new AllContactViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomContact = x.NomContact,
                                 AdresseMail = x.AdresseMail,
                                 NomGroupeContact = x.NomGroupeContact,
                                 Numero = x.Numero,
                                 EtatContacts_ = x.EtatContacts_
                             });
            }
            //date inf uniquement
            else if (pays.Trim().Length == 0 && inf.Trim().Length != 0 && sup.Trim().Length == 0)
            {
                DateTime inft = Convert.ToDateTime(inf);
                ListEmpty = (from gg in db.Entreprises
                             join c in db.Contacts on gg.Id equals c.Entreprise_
                             where c.Date_enregistrement >= inft
                             join e in db.ContactGroup on c.Contactgroups equals e.Id
                             join d in db.Activites on c.Id equals d.Contact_ into r
                             from d in r.DefaultIfEmpty()
                             join f in db.EtatContacts on c.Id equals f.ContactId into s
                             from f in s.DefaultIfEmpty()
                             select new
                             {
                                 NomEntreprise = gg.Nom,
                                 NomContact = c.Contact_name,
                                 AdresseMail = c.Adresse_mail,
                                 NomGroupeContact = e.Type_name,
                                 Numero = c.Telephone,
                                 EtatContacts_ = f == null ? string.Empty : f.Intitule
                             }).Select(x => new AllContactViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomContact = x.NomContact,
                                 AdresseMail = x.AdresseMail,
                                 NomGroupeContact = x.NomGroupeContact,
                                 Numero = x.Numero,
                                 EtatContacts_ = x.EtatContacts_
                             });
            }
            // tout est rempli
            else if (sup.Trim().Length != 0 && pays.Trim().Length != 0 && inf.Trim().Length != 0)
            {
                DateTime inft = Convert.ToDateTime(inf); DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from gg in db.Entreprises
                             join c in db.Contacts on gg.Id equals c.Entreprise_
                             where (c.Date_enregistrement >= inft && c.Date_enregistrement<=supt && c.Pays == pays)
                             join e in db.ContactGroup on c.Contactgroups equals e.Id
                             join d in db.Activites on c.Id equals d.Contact_ into r
                             from d in r.DefaultIfEmpty()
                             join f in db.EtatContacts on c.Id equals f.ContactId into s
                             from f in s.DefaultIfEmpty()
                             select new
                             {
                                 NomEntreprise = gg.Nom,
                                 NomContact = c.Contact_name,
                                 AdresseMail = c.Adresse_mail,
                                 NomGroupeContact = e.Type_name,
                                 Numero = c.Telephone,
                                 EtatContacts_ = f == null ? string.Empty : f.Intitule
                             }).Select(x => new AllContactViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomContact = x.NomContact,
                                 AdresseMail = x.AdresseMail,
                                 NomGroupeContact = x.NomGroupeContact,
                                 Numero = x.Numero,
                                 EtatContacts_ = x.EtatContacts_
                             });
            }
            // supt uniquement
            else if (sup.Trim().Length != 0 && pays.Trim().Length == 0 && inf.Trim().Length == 0)
            {
                DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from gg in db.Entreprises
                             join c in db.Contacts on gg.Id equals c.Entreprise_
                             where c.Date_enregistrement <= supt
                             join e in db.ContactGroup on c.Contactgroups equals e.Id
                             join d in db.Activites on c.Id equals d.Contact_ into r
                             from d in r.DefaultIfEmpty()
                             join f in db.EtatContacts on c.Id equals f.ContactId into s
                             from f in s.DefaultIfEmpty()
                             select new
                             {
                                 NomEntreprise = gg.Nom,
                                 NomContact = c.Contact_name,
                                 AdresseMail = c.Adresse_mail,
                                 NomGroupeContact = e.Type_name,
                                 Numero = c.Telephone,
                                 EtatContacts_ = f == null ? string.Empty : f.Intitule
                             }).Select(x => new AllContactViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomContact = x.NomContact,
                                 AdresseMail = x.AdresseMail,
                                 NomGroupeContact = x.NomGroupeContact,
                                 Numero = x.Numero,
                                 EtatContacts_ = x.EtatContacts_
                             });
            }
            //supt et pays uniquement
            else if (sup.Trim().Length != 0 && pays.Trim().Length != 0 && inf.Trim().Length == 0)
            {
                DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from gg in db.Entreprises
                             join c in db.Contacts on gg.Id equals c.Entreprise_
                             where c.Pays == pays && c.Date_enregistrement<= supt
                             join e in db.ContactGroup on c.Contactgroups equals e.Id
                             join d in db.Activites on c.Id equals d.Contact_ into r
                             from d in r.DefaultIfEmpty()
                             join f in db.EtatContacts on c.Id equals f.ContactId into s
                             from f in s.DefaultIfEmpty()
                             select new
                             {
                                 NomEntreprise = gg.Nom,
                                 NomContact = c.Contact_name,
                                 AdresseMail = c.Adresse_mail,
                                 NomGroupeContact = e.Type_name,
                                 Numero = c.Telephone,
                                 EtatContacts_ = f == null ? string.Empty : f.Intitule
                             }).Select(x => new AllContactViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomContact = x.NomContact,
                                 AdresseMail = x.AdresseMail,
                                 NomGroupeContact = x.NomGroupeContact,
                                 Numero = x.Numero,
                                 EtatContacts_ = x.EtatContacts_
                             });
            }
            //pays et inft
            else if (sup.Trim().Length == 0 && pays.Trim().Length != 0 && inf.Trim().Length != 0)
            {
                DateTime inft = Convert.ToDateTime(inf);
                ListEmpty = (from gg in db.Entreprises
                             join c in db.Contacts on gg.Id equals c.Entreprise_
                             where (c.Pays == pays && c.Date_enregistrement >=inft)
                             join e in db.ContactGroup on c.Contactgroups equals e.Id
                             join d in db.Activites on c.Id equals d.Contact_ into r
                             from d in r.DefaultIfEmpty()
                             join f in db.EtatContacts on c.Id equals f.ContactId into s
                             from f in s.DefaultIfEmpty()
                             select new
                             {
                                 NomEntreprise = gg.Nom,
                                 NomContact = c.Contact_name,
                                 AdresseMail = c.Adresse_mail,
                                 NomGroupeContact = e.Type_name,
                                 Numero = c.Telephone,
                                 EtatContacts_ = f == null ? string.Empty : f.Intitule
                             }).Select(x => new AllContactViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomContact = x.NomContact,
                                 AdresseMail = x.AdresseMail,
                                 NomGroupeContact = x.NomGroupeContact,
                                 Numero = x.Numero,
                                 EtatContacts_ = x.EtatContacts_
                             });
            }



            return (ListEmpty);
        }
        [HttpPost]
        public JsonResult getContactsJs(string pays, string inf, string sup)
        {
            IEnumerable<AllContactViewModel> empSummary = new List<AllContactViewModel>();

            //Rien n'est rempli
            if (inf.Trim().Length == 0 && pays.Trim().Length == 0 && sup.Trim().Length == 0)
            {
                empSummary = (from gg in db.Entreprises
                             join c in db.Contacts on gg.Id equals c.Entreprise_
                             join e in db.ContactGroup on c.Contactgroups equals e.Id
                             join d in db.Activites on c.Id equals d.Contact_ into r
                             from d in r.DefaultIfEmpty()
                             join f in db.EtatContacts on c.Id equals f.ContactId into s
                             from f in s.DefaultIfEmpty()
                             select new
                             {
                                 NomEntreprise = gg.Nom,
                                 NomContact = c.Contact_name,
                                 AdresseMail = c.Adresse_mail,
                                 NomGroupeContact = e.Type_name,
                                 Numero = c.Telephone,
                                 EtatContacts_ = f == null ? string.Empty : f.Intitule
                             }).Select(x => new AllContactViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomContact = x.NomContact,
                                 AdresseMail = x.AdresseMail,
                                 NomGroupeContact = x.NomGroupeContact,
                                 Numero = x.Numero,
                                 EtatContacts_ = x.EtatContacts_
                             });


            }

            //Pays uniquement
            else if (inf.Trim().Length == 0 && sup.Trim().Length == 0 && pays.Trim().Length != 0)
            {

                empSummary = (from gg in db.Entreprises
                             join c in db.Contacts on gg.Id equals c.Entreprise_
                             where c.Pays == pays
                             join e in db.ContactGroup on c.Contactgroups equals e.Id
                             join d in db.Activites on c.Id equals d.Contact_ into r
                             from d in r.DefaultIfEmpty()
                             join f in db.EtatContacts on c.Id equals f.ContactId into s
                             from f in s.DefaultIfEmpty()
                             select new
                             {
                                 NomEntreprise = gg.Nom,
                                 NomContact = c.Contact_name,
                                 AdresseMail = c.Adresse_mail,
                                 NomGroupeContact = e.Type_name,
                                 Numero = c.Telephone,
                                 EtatContacts_ = f == null ? string.Empty : f.Intitule
                             }).Select(x => new AllContactViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomContact = x.NomContact,
                                 AdresseMail = x.AdresseMail,
                                 NomGroupeContact = x.NomGroupeContact,
                                 Numero = x.Numero,
                                 EtatContacts_ = x.EtatContacts_
                             });

            }
            //2 dates
            else if (pays.Trim().Length == 0 && inf.Trim().Length != 0 && sup.Trim().Length != 0)
            {
                DateTime inft = Convert.ToDateTime(inf); DateTime supt = Convert.ToDateTime(sup);
                empSummary = (from gg in db.Entreprises
                             join c in db.Contacts on gg.Id equals c.Entreprise_
                             where (c.Date_enregistrement >= inft && c.Date_enregistrement <= supt)
                             join e in db.ContactGroup on c.Contactgroups equals e.Id
                             join d in db.Activites on c.Id equals d.Contact_ into r
                             from d in r.DefaultIfEmpty()
                             join f in db.EtatContacts on c.Id equals f.ContactId into s
                             from f in s.DefaultIfEmpty()
                             select new
                             {
                                 NomEntreprise = gg.Nom,
                                 NomContact = c.Contact_name,
                                 AdresseMail = c.Adresse_mail,
                                 NomGroupeContact = e.Type_name,
                                 Numero = c.Telephone,
                                 EtatContacts_ = f == null ? string.Empty : f.Intitule
                             }).Select(x => new AllContactViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomContact = x.NomContact,
                                 AdresseMail = x.AdresseMail,
                                 NomGroupeContact = x.NomGroupeContact,
                                 Numero = x.Numero,
                                 EtatContacts_ = x.EtatContacts_
                             });
            }
            //date inf uniquement
            else if (pays.Trim().Length == 0 && inf.Trim().Length != 0 && sup.Trim().Length == 0)
            {
                DateTime inft = Convert.ToDateTime(inf);
                empSummary = (from gg in db.Entreprises
                             join c in db.Contacts on gg.Id equals c.Entreprise_
                             where c.Date_enregistrement >= inft
                             join e in db.ContactGroup on c.Contactgroups equals e.Id
                             join d in db.Activites on c.Id equals d.Contact_ into r
                             from d in r.DefaultIfEmpty()
                             join f in db.EtatContacts on c.Id equals f.ContactId into s
                             from f in s.DefaultIfEmpty()
                             select new
                             {
                                 NomEntreprise = gg.Nom,
                                 NomContact = c.Contact_name,
                                 AdresseMail = c.Adresse_mail,
                                 NomGroupeContact = e.Type_name,
                                 Numero = c.Telephone,
                                 EtatContacts_ = f == null ? string.Empty : f.Intitule
                             }).Select(x => new AllContactViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomContact = x.NomContact,
                                 AdresseMail = x.AdresseMail,
                                 NomGroupeContact = x.NomGroupeContact,
                                 Numero = x.Numero,
                                 EtatContacts_ = x.EtatContacts_
                             });
            }
            // tout est rempli
            else if (sup.Trim().Length != 0 && pays.Trim().Length != 0 && inf.Trim().Length != 0)
            {
                DateTime inft = Convert.ToDateTime(inf); DateTime supt = Convert.ToDateTime(sup);
                empSummary = (from gg in db.Entreprises
                             join c in db.Contacts on gg.Id equals c.Entreprise_
                             where (c.Date_enregistrement >= inft && c.Date_enregistrement <= supt && c.Pays == pays)
                             join e in db.ContactGroup on c.Contactgroups equals e.Id
                             join d in db.Activites on c.Id equals d.Contact_ into r
                             from d in r.DefaultIfEmpty()
                             join f in db.EtatContacts on c.Id equals f.ContactId into s
                             from f in s.DefaultIfEmpty()
                             select new
                             {
                                 NomEntreprise = gg.Nom,
                                 NomContact = c.Contact_name,
                                 AdresseMail = c.Adresse_mail,
                                 NomGroupeContact = e.Type_name,
                                 Numero = c.Telephone,
                                 EtatContacts_ = f == null ? string.Empty : f.Intitule
                             }).Select(x => new AllContactViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomContact = x.NomContact,
                                 AdresseMail = x.AdresseMail,
                                 NomGroupeContact = x.NomGroupeContact,
                                 Numero = x.Numero,
                                 EtatContacts_ = x.EtatContacts_
                             });
            }
            // supt uniquement
            else if (sup.Trim().Length != 0 && pays.Trim().Length == 0 && inf.Trim().Length == 0)
            {
                DateTime supt = Convert.ToDateTime(sup);
                empSummary = (from gg in db.Entreprises
                             join c in db.Contacts on gg.Id equals c.Entreprise_
                             where c.Date_enregistrement <= supt
                             join e in db.ContactGroup on c.Contactgroups equals e.Id
                             join d in db.Activites on c.Id equals d.Contact_ into r
                             from d in r.DefaultIfEmpty()
                             join f in db.EtatContacts on c.Id equals f.ContactId into s
                             from f in s.DefaultIfEmpty()
                             select new
                             {
                                 NomEntreprise = gg.Nom,
                                 NomContact = c.Contact_name,
                                 AdresseMail = c.Adresse_mail,
                                 NomGroupeContact = e.Type_name,
                                 Numero = c.Telephone,
                                 EtatContacts_ = f == null ? string.Empty : f.Intitule
                             }).Select(x => new AllContactViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomContact = x.NomContact,
                                 AdresseMail = x.AdresseMail,
                                 NomGroupeContact = x.NomGroupeContact,
                                 Numero = x.Numero,
                                 EtatContacts_ = x.EtatContacts_
                             });
            }
            //supt et pays uniquement
            else if (sup.Trim().Length != 0 && pays.Trim().Length != 0 && inf.Trim().Length == 0)
            {
                DateTime supt = Convert.ToDateTime(sup);
                empSummary = (from gg in db.Entreprises
                             join c in db.Contacts on gg.Id equals c.Entreprise_
                             where c.Pays == pays && c.Date_enregistrement <= supt
                             join e in db.ContactGroup on c.Contactgroups equals e.Id
                             join d in db.Activites on c.Id equals d.Contact_ into r
                             from d in r.DefaultIfEmpty()
                             join f in db.EtatContacts on c.Id equals f.ContactId into s
                             from f in s.DefaultIfEmpty()
                             select new
                             {
                                 NomEntreprise = gg.Nom,
                                 NomContact = c.Contact_name,
                                 AdresseMail = c.Adresse_mail,
                                 NomGroupeContact = e.Type_name,
                                 Numero = c.Telephone,
                                 EtatContacts_ = f == null ? string.Empty : f.Intitule
                             }).Select(x => new AllContactViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomContact = x.NomContact,
                                 AdresseMail = x.AdresseMail,
                                 NomGroupeContact = x.NomGroupeContact,
                                 Numero = x.Numero,
                                 EtatContacts_ = x.EtatContacts_
                             });
            }
            //pays et inft
            else if (sup.Trim().Length == 0 && pays.Trim().Length != 0 && inf.Trim().Length != 0)
            {
                DateTime inft = Convert.ToDateTime(inf);
                empSummary = (from gg in db.Entreprises
                             join c in db.Contacts on gg.Id equals c.Entreprise_
                             where (c.Pays == pays && c.Date_enregistrement >= inft)
                             join e in db.ContactGroup on c.Contactgroups equals e.Id
                             join d in db.Activites on c.Id equals d.Contact_ into r
                             from d in r.DefaultIfEmpty()
                             join f in db.EtatContacts on c.Id equals f.ContactId into s
                             from f in s.DefaultIfEmpty()
                             select new
                             {
                                 NomEntreprise = gg.Nom,
                                 NomContact = c.Contact_name,
                                 AdresseMail = c.Adresse_mail,
                                 NomGroupeContact = e.Type_name,
                                 Numero = c.Telephone,
                                 EtatContacts_ = f == null ? string.Empty : f.Intitule
                             }).Select(x => new AllContactViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomContact = x.NomContact,
                                 AdresseMail = x.AdresseMail,
                                 NomGroupeContact = x.NomGroupeContact,
                                 Numero = x.Numero,
                                 EtatContacts_ = x.EtatContacts_
                             });
            }
        return Json(empSummary, JsonRequestBehavior.AllowGet);
        }

        public IEnumerable<ProduitViewModel> getProduits(string categorie, string Prixinf, string Prixsup)
        {
            //000
           
            IEnumerable<ProduitViewModel> ListEmpty = new List<ProduitViewModel>();
            if (categorie.Trim().Length == 0 && Prixinf.Trim().Length == 0 && Prixsup.Trim().Length == 0)
            {
                         ListEmpty = (from a in db.CategorieProduits
                                      join b in db.Articles on a.Id equals b.CategorieProduit_
                                      select new {
                                      Prix = b.Prix,
                                      nomCategorie = a.Nom,
                                      nomProduit = b.Nom,
                                      description = b.Descriptionp,
                                      dateEnregistrement = b.Date_Creation})
                                      .ToList().Select(x => new ProduitViewModel
                         {
                             Prix = x.Prix,
                             NomCategorie = x.nomCategorie,
                             NomProduit = x.nomProduit,
                             DateEnregistrement = x.dateEnregistrement,
                             Description = x.description
                         }).ToList();
            }
            //001
            if (categorie.Trim().Length == 0 && Prixinf.Trim().Length == 0 && Prixsup.Trim().Length != 0)
            {
                var Prixs = Convert.ToInt32(Prixsup); 
                
                ListEmpty = (from a in db.CategorieProduits
                             join b in db.Articles on a.Id equals b.CategorieProduit_
                             where b.Prix <= Prixs
                             select new
                             {
                                 Prix = b.Prix,
                                 nomCategorie = a.Nom,
                                 nomProduit = b.Nom,
                                 description = b.Descriptionp,
                                 dateEnregistrement = b.Date_Creation
                             })
                             .ToList().Select(x => new ProduitViewModel
                             {
                                 Prix = x.Prix,
                                 NomCategorie = x.nomCategorie,
                                 NomProduit = x.nomProduit,
                                 DateEnregistrement = x.dateEnregistrement,
                                 Description = x.description
                             }).ToList();
            }
            //010
            if (categorie.Trim().Length == 0 && Prixinf.Trim().Length != 0 && Prixsup.Trim().Length == 0)
            {
                var Prixi = Convert.ToInt32(Prixinf);

                ListEmpty = (from a in db.CategorieProduits
                             join b in db.Articles on a.Id equals b.CategorieProduit_
                             where b.Prix >= Prixi
                             select new
                             {
                                 Prix = b.Prix,
                                 nomCategorie = a.Nom,
                                 nomProduit = b.Nom,
                                 description = b.Descriptionp,
                                 dateEnregistrement = b.Date_Creation
                             })
                             .ToList().Select(x => new ProduitViewModel
                             {
                                 Prix = x.Prix,
                                 NomCategorie = x.nomCategorie,
                                 NomProduit = x.nomProduit,
                                 DateEnregistrement = x.dateEnregistrement,
                                 Description = x.description
                             }).ToList();
            }
            //011
            if (categorie.Trim().Length == 0 && Prixinf.Trim().Length != 0 && Prixsup.Trim().Length != 0)
            {
                var Prixs = Convert.ToInt32(Prixsup); var Prixi = Convert.ToInt32(Prixinf);

                ListEmpty = (from a in db.CategorieProduits
                             join b in db.Articles on a.Id equals b.CategorieProduit_
                             where (b.Prix <= Prixs && b.Prix >= Prixi)
                             select new
                             {
                                 Prix = b.Prix,
                                 nomCategorie = a.Nom,
                                 nomProduit = b.Nom,
                                 description = b.Descriptionp,
                                 dateEnregistrement = b.Date_Creation
                             })
                             .ToList().Select(x => new ProduitViewModel
                             {
                                 Prix = x.Prix,
                                 NomCategorie = x.nomCategorie,
                                 NomProduit = x.nomProduit,
                                 DateEnregistrement = x.dateEnregistrement,
                                 Description = x.description
                             }).ToList();
            }

            //100
            if (categorie.Trim().Length != 0 && Prixinf.Trim().Length == 0 && Prixsup.Trim().Length == 0)
            {
           

                ListEmpty = (from a in db.CategorieProduits
                             join b in db.Articles on a.Id equals b.CategorieProduit_
                             where (a.Nom == categorie)
                             select new
                             {
                                 Prix = b.Prix,
                                 nomCategorie = a.Nom,
                                 nomProduit = b.Nom,
                                 description = b.Descriptionp,
                                 dateEnregistrement = b.Date_Creation
                             })
                             .ToList().Select(x => new ProduitViewModel
                             {
                                 Prix = x.Prix,
                                 NomCategorie = x.nomCategorie,
                                 NomProduit = x.nomProduit,
                                 DateEnregistrement = x.dateEnregistrement,
                                 Description = x.description
                             }).ToList();
            }

            //101
            if (categorie.Trim().Length != 0 && Prixinf.Trim().Length == 0 && Prixsup.Trim().Length != 0)
            {
                var Prixs = Convert.ToInt32(Prixsup);

                ListEmpty = (from a in db.CategorieProduits
                             join b in db.Articles on a.Id equals b.CategorieProduit_
                             where (b.Prix <= Prixs && a.Nom == categorie)
                             select new
                             {
                                 Prix = b.Prix,
                                 nomCategorie = a.Nom,
                                 nomProduit = b.Nom,
                                 description = b.Descriptionp,
                                 dateEnregistrement = b.Date_Creation
                             })
                             .ToList().Select(x => new ProduitViewModel
                             {
                                 Prix = x.Prix,
                                 NomCategorie = x.nomCategorie,
                                 NomProduit = x.nomProduit,
                                 DateEnregistrement = x.dateEnregistrement,
                                 Description = x.description
                             }).ToList();
            }

            //110
            if (categorie.Trim().Length != 0 && Prixinf.Trim().Length != 0 && Prixsup.Trim().Length == 0)
            {
                var Prixi = Convert.ToInt32(Prixinf);

                ListEmpty = (from a in db.CategorieProduits
                             join b in db.Articles on a.Id equals b.CategorieProduit_
                             where (a.Nom == categorie && b.Prix >= Prixi)
                             select new
                             {
                                 Prix = b.Prix,
                                 nomCategorie = a.Nom,
                                 nomProduit = b.Nom,
                                 description = b.Descriptionp,
                                 dateEnregistrement = b.Date_Creation
                             })
                             .ToList().Select(x => new ProduitViewModel
                             {
                                 Prix = x.Prix,
                                 NomCategorie = x.nomCategorie,
                                 NomProduit = x.nomProduit,
                                 DateEnregistrement = x.dateEnregistrement,
                                 Description = x.description
                             }).ToList();
            }

            //111
            if (categorie.Trim().Length != 0 && Prixinf.Trim().Length != 0 && Prixsup.Trim().Length != 0)
            {
                var Prixs = Convert.ToInt32(Prixsup); var Prixi = Convert.ToInt32(Prixinf);

                ListEmpty = (from a in db.CategorieProduits
                             join b in db.Articles on a.Id equals b.CategorieProduit_
                             where (b.Prix <= Prixs && b.Prix >= Prixi && a.Nom == categorie)
                             select new
                             {
                                 Prix = b.Prix,
                                 nomCategorie = a.Nom,
                                 nomProduit = b.Nom,
                                 description = b.Descriptionp,
                                 dateEnregistrement = b.Date_Creation
                             })
                             .ToList().Select(x => new ProduitViewModel
                             {
                                 Prix = x.Prix,
                                 NomCategorie = x.nomCategorie,
                                 NomProduit = x.nomProduit,
                                 DateEnregistrement = x.dateEnregistrement,
                                 Description = x.description
                             }).ToList();
            }

            return (ListEmpty);
        }
        [HttpPost]
        public JsonResult getProduitsJs(string categorie, string Prixinf, string Prixsup)
        {
            //000

            IEnumerable<ProduitViewModel> empSummary = new List<ProduitViewModel>();
            if (categorie.Trim().Length == 0 && Prixinf.Trim().Length == 0 && Prixsup.Trim().Length == 0)
            {
                empSummary = (from a in db.CategorieProduits
                             join b in db.Articles on a.Id equals b.CategorieProduit_
                             select new
                             {
                                 Prix = b.Prix,
                                 nomCategorie = a.Nom,
                                 nomProduit = b.Nom,
                                 description = b.Descriptionp,
                                 dateEnregistrement = b.Date_Creation
                             })
                             .ToList().Select(x => new ProduitViewModel
                             {
                                 Prix = x.Prix,
                                 NomCategorie = x.nomCategorie,
                                 NomProduit = x.nomProduit,
                                 DateEnregistrement = x.dateEnregistrement,
                                 Description = x.description
                             }).ToList();
            }
            //001
            if (categorie.Trim().Length == 0 && Prixinf.Trim().Length == 0 && Prixsup.Trim().Length != 0)
            {
                var Prixs = Convert.ToInt32(Prixsup);

                empSummary = (from a in db.CategorieProduits
                             join b in db.Articles on a.Id equals b.CategorieProduit_
                             where b.Prix <= Prixs
                             select new
                             {
                                 Prix = b.Prix,
                                 nomCategorie = a.Nom,
                                 nomProduit = b.Nom,
                                 description = b.Descriptionp,
                                 dateEnregistrement = b.Date_Creation
                             })
                             .ToList().Select(x => new ProduitViewModel
                             {
                                 Prix = x.Prix,
                                 NomCategorie = x.nomCategorie,
                                 NomProduit = x.nomProduit,
                                 DateEnregistrement = x.dateEnregistrement,
                                 Description = x.description
                             }).ToList();
            }
            //010
            if (categorie.Trim().Length == 0 && Prixinf.Trim().Length != 0 && Prixsup.Trim().Length == 0)
            {
                var Prixi = Convert.ToInt32(Prixinf);

                empSummary = (from a in db.CategorieProduits
                             join b in db.Articles on a.Id equals b.CategorieProduit_
                             where b.Prix >= Prixi
                             select new
                             {
                                 Prix = b.Prix,
                                 nomCategorie = a.Nom,
                                 nomProduit = b.Nom,
                                 description = b.Descriptionp,
                                 dateEnregistrement = b.Date_Creation
                             })
                             .ToList().Select(x => new ProduitViewModel
                             {
                                 Prix = x.Prix,
                                 NomCategorie = x.nomCategorie,
                                 NomProduit = x.nomProduit,
                                 DateEnregistrement = x.dateEnregistrement,
                                 Description = x.description
                             }).ToList();
            }
            //011
            if (categorie.Trim().Length == 0 && Prixinf.Trim().Length != 0 && Prixsup.Trim().Length != 0)
            {
                var Prixs = Convert.ToInt32(Prixsup); var Prixi = Convert.ToInt32(Prixinf);

                empSummary = (from a in db.CategorieProduits
                             join b in db.Articles on a.Id equals b.CategorieProduit_
                             where (b.Prix <= Prixs && b.Prix >= Prixi)
                             select new
                             {
                                 Prix = b.Prix,
                                 nomCategorie = a.Nom,
                                 nomProduit = b.Nom,
                                 description = b.Descriptionp,
                                 dateEnregistrement = b.Date_Creation
                             })
                             .ToList().Select(x => new ProduitViewModel
                             {
                                 Prix = x.Prix,
                                 NomCategorie = x.nomCategorie,
                                 NomProduit = x.nomProduit,
                                 DateEnregistrement = x.dateEnregistrement,
                                 Description = x.description
                             }).ToList();
            }

            //100
            if (categorie.Trim().Length != 0 && Prixinf.Trim().Length == 0 && Prixsup.Trim().Length == 0)
            {


                empSummary = (from a in db.CategorieProduits
                             join b in db.Articles on a.Id equals b.CategorieProduit_
                             where (a.Nom == categorie)
                             select new
                             {
                                 Prix = b.Prix,
                                 nomCategorie = a.Nom,
                                 nomProduit = b.Nom,
                                 description = b.Descriptionp,
                                 dateEnregistrement = b.Date_Creation
                             })
                             .ToList().Select(x => new ProduitViewModel
                             {
                                 Prix = x.Prix,
                                 NomCategorie = x.nomCategorie,
                                 NomProduit = x.nomProduit,
                                 DateEnregistrement = x.dateEnregistrement,
                                 Description = x.description
                             }).ToList();
            }

            //101
            if (categorie.Trim().Length != 0 && Prixinf.Trim().Length == 0 && Prixsup.Trim().Length != 0)
            {
                var Prixs = Convert.ToInt32(Prixsup);

                empSummary = (from a in db.CategorieProduits
                             join b in db.Articles on a.Id equals b.CategorieProduit_
                             where (b.Prix <= Prixs && a.Nom == categorie)
                             select new
                             {
                                 Prix = b.Prix,
                                 nomCategorie = a.Nom,
                                 nomProduit = b.Nom,
                                 description = b.Descriptionp,
                                 dateEnregistrement = b.Date_Creation
                             })
                             .ToList().Select(x => new ProduitViewModel
                             {
                                 Prix = x.Prix,
                                 NomCategorie = x.nomCategorie,
                                 NomProduit = x.nomProduit,
                                 DateEnregistrement = x.dateEnregistrement,
                                 Description = x.description
                             }).ToList();
            }

            //110
            if (categorie.Trim().Length != 0 && Prixinf.Trim().Length != 0 && Prixsup.Trim().Length == 0)
            {
                var Prixi = Convert.ToInt32(Prixinf);

                empSummary = (from a in db.CategorieProduits
                             join b in db.Articles on a.Id equals b.CategorieProduit_
                             where (a.Nom == categorie && b.Prix >= Prixi)
                             select new
                             {
                                 Prix = b.Prix,
                                 nomCategorie = a.Nom,
                                 nomProduit = b.Nom,
                                 description = b.Descriptionp,
                                 dateEnregistrement = b.Date_Creation
                             })
                             .ToList().Select(x => new ProduitViewModel
                             {
                                 Prix = x.Prix,
                                 NomCategorie = x.nomCategorie,
                                 NomProduit = x.nomProduit,
                                 DateEnregistrement = x.dateEnregistrement,
                                 Description = x.description
                             }).ToList();
            }

            //111
            if (categorie.Trim().Length != 0 && Prixinf.Trim().Length != 0 && Prixsup.Trim().Length != 0)
            {
                var Prixs = Convert.ToInt32(Prixsup); var Prixi = Convert.ToInt32(Prixinf);

                empSummary = (from a in db.CategorieProduits
                             join b in db.Articles on a.Id equals b.CategorieProduit_
                             where (b.Prix <= Prixs && b.Prix >= Prixi && a.Nom == categorie)
                             select new
                             {
                                 Prix = b.Prix,
                                 nomCategorie = a.Nom,
                                 nomProduit = b.Nom,
                                 description = b.Descriptionp,
                                 dateEnregistrement = b.Date_Creation
                             })
                             .ToList().Select(x => new ProduitViewModel
                             {
                                 Prix = x.Prix,
                                 NomCategorie = x.nomCategorie,
                                 NomProduit = x.nomProduit,
                                 DateEnregistrement = x.dateEnregistrement,
                                 Description = x.description
                             }).ToList();
            }

            return Json(empSummary,JsonRequestBehavior.AllowGet);
        }

        //Dynamic Selected list Concact

        public JsonResult getListContacts(int Entreprise)
        {
            List<Contact> ContactList = db.Contacts.Where(x => x.Entreprise_ == Entreprise).ToList();
            return Json(ContactList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getListProduits(int Categorie)
        {
            List<Article> ProduitList = db.Articles.Where(x => x.CategorieProduit_ == Categorie).ToList();
            return Json(ProduitList, JsonRequestBehavior.AllowGet);
        }
        public IEnumerable<ActiviteReportViewModel> getActivites(string Entreprises, string etat, string inf, string sup)
        {
            IEnumerable<ActiviteReportViewModel> ListEmpty = new List<ActiviteReportViewModel>();

            //Rien n'est rempli
            if (inf.Trim().Length == 0 && etat.Trim().Length == 0 && sup.Trim().Length == 0  && Entreprises == null)
            {
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             join d in db.Activites on b.Id equals d.Contact_
                             join c in db.CategorieActivites on d.Categorie_ equals c.Id
                             select new
                             {
                                 NomEntreprise = a.Nom,
                                 NomContact = b.Contact_name,
                                 AdresseMail = b.Adresse_mail,
                                 Numero = b.Telephone,
                                 Emails = b.Adresse_mail,
                                 CategorieActivite = c.Nom,
                                 EtatActivite = d.Etat,
                                 Prix = d.Prix,
                                 AdresseEmail_ = b.Adresse_mail,
                                 NomActivite = d.Nom
                             }).ToList().Select(x => new ActiviteReportViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomContact = x.NomContact,
                                 EtatActivite = x.EtatActivite,
                                 CategorieActivite = x.CategorieActivite,
                                 NomActivite = x.NomActivite,
                                 Prix = x.Prix,
                                 Numerotel = x.Numero,
                                 AdresseEmail = x.AdresseEmail_
                             });
            }

            else if (inf.Trim().Length == 0 && etat.Trim().Length == 0 && sup.Trim().Length != 0 && Entreprises == null)
            {
                DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             join d in db.Activites on b.Id equals d.Contact_
                             where d.DacteCreation <= supt
                             join c in db.CategorieActivites on d.Categorie_ equals c.Id
                             select new
                             {
                                 NomEntreprise = a.Nom,
                                 NomContact = b.Contact_name,
                                 AdresseMail = b.Adresse_mail,
                                 Numero = b.Telephone,
                                 Emails = b.Adresse_mail,
                                 CategorieActivite = c.Nom,
                                 EtatActivite = d.Etat,
                                 Prix = d.Prix,
                                 AdresseEmail_ = b.Adresse_mail,
                                 NomActivite = d.Nom
                             }).ToList().Select(x => new ActiviteReportViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomContact = x.NomContact,
                                 EtatActivite = x.EtatActivite,
                                 CategorieActivite = x.CategorieActivite,
                                 NomActivite = x.NomActivite,
                                 Prix = x.Prix,
                                 Numerotel = x.Numero,
                                 AdresseEmail = x.AdresseEmail_
                             });
            }
            else if (inf.Trim().Length != 0 && etat.Trim().Length == 0 && sup.Trim().Length == 0 && Entreprises == null)
            {
                DateTime inft = Convert.ToDateTime(inf);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             join d in db.Activites on b.Id equals d.Contact_
                             where d.DacteCreation >= inft
                             join c in db.CategorieActivites on d.Categorie_ equals c.Id
                             select new
                             {
                                 NomEntreprise = a.Nom,
                                 NomContact = b.Contact_name,
                                 AdresseMail = b.Adresse_mail,
                                 Numero = b.Telephone,
                                 Emails = b.Adresse_mail,
                                 CategorieActivite = c.Nom,
                                 EtatActivite = d.Etat,
                                 Prix = d.Prix,
                                 AdresseEmail_ = b.Adresse_mail,
                                 NomActivite = d.Nom
                             }).ToList().Select(x => new ActiviteReportViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomContact = x.NomContact,
                                 EtatActivite = x.EtatActivite,
                                 CategorieActivite = x.CategorieActivite,
                                 NomActivite = x.NomActivite,
                                 Prix = x.Prix,
                                 Numerotel = x.Numero,
                                 AdresseEmail = x.AdresseEmail_
                             });
            }
            else if (inf.Trim().Length != 0 && etat.Trim().Length == 0 && sup.Trim().Length != 0 && Entreprises == null)
            {
                DateTime supt = Convert.ToDateTime(sup);DateTime inft = Convert.ToDateTime(inf);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             join d in db.Activites on b.Id equals d.Contact_
                             where (d.DacteCreation <= supt && d.DacteCreation>= inft)
                             join c in db.CategorieActivites on d.Categorie_ equals c.Id
                             select new
                             {
                                 NomEntreprise = a.Nom,
                                 NomContact = b.Contact_name,
                                 AdresseMail = b.Adresse_mail,
                                 Numero = b.Telephone,
                                 Emails = b.Adresse_mail,
                                 CategorieActivite = c.Nom,
                                 EtatActivite = d.Etat,
                                 Prix = d.Prix,
                                 AdresseEmail_ = b.Adresse_mail,
                                 NomActivite = d.Nom
                             }).ToList().Select(x => new ActiviteReportViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomContact = x.NomContact,
                                 EtatActivite = x.EtatActivite,
                                 CategorieActivite = x.CategorieActivite,
                                 NomActivite = x.NomActivite,
                                 Prix = x.Prix,
                                 Numerotel = x.Numero,
                                 AdresseEmail = x.AdresseEmail_
                             });
            }
            else if (inf.Trim().Length == 0 && etat.Trim().Length != 0 && sup.Trim().Length == 0 && Entreprises == null)
            {
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             join d in db.Activites on b.Id equals d.Contact_
                             where d.Etat == etat
                             join c in db.CategorieActivites on d.Categorie_ equals c.Id
                             select new
                             {
                                 NomEntreprise = a.Nom,
                                 NomContact = b.Contact_name,
                                 AdresseMail = b.Adresse_mail,
                                 Numero = b.Telephone,
                                 Emails = b.Adresse_mail,
                                 CategorieActivite = c.Nom,
                                 EtatActivite = d.Etat,
                                 Prix = d.Prix,
                                 AdresseEmail_ = b.Adresse_mail,
                                 NomActivite = d.Nom
                             }).ToList().Select(x => new ActiviteReportViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomContact = x.NomContact,
                                 EtatActivite = x.EtatActivite,
                                 CategorieActivite = x.CategorieActivite,
                                 NomActivite = x.NomActivite,
                                 Prix = x.Prix,
                                 Numerotel = x.Numero,
                                 AdresseEmail = x.AdresseEmail_
                             });
            }
            else if (inf.Trim().Length == 0 && etat.Trim().Length != 0 && sup.Trim().Length != 0 && Entreprises == null)
            {
                DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             join d in db.Activites on b.Id equals d.Contact_
                             where (d.DacteCreation <= supt && d.Etat == etat)
                             join c in db.CategorieActivites on d.Categorie_ equals c.Id
                             select new
                             {
                                 NomEntreprise = a.Nom,
                                 NomContact = b.Contact_name,
                                 AdresseMail = b.Adresse_mail,
                                 Numero = b.Telephone,
                                 Emails = b.Adresse_mail,
                                 CategorieActivite = c.Nom,
                                 EtatActivite = d.Etat,
                                 Prix = d.Prix,
                                 AdresseEmail_ = b.Adresse_mail,
                                 NomActivite = d.Nom
                             }).ToList().Select(x => new ActiviteReportViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomContact = x.NomContact,
                                 EtatActivite = x.EtatActivite,
                                 CategorieActivite = x.CategorieActivite,
                                 NomActivite = x.NomActivite,
                                 Prix = x.Prix,
                                 Numerotel = x.Numero,
                                 AdresseEmail = x.AdresseEmail_
                             });
            }
            else if (inf.Trim().Length != 0 && etat.Trim().Length != 0 && sup.Trim().Length != 0 && Entreprises == null)
            {
                DateTime inft = Convert.ToDateTime(inf);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             join d in db.Activites on b.Id equals d.Contact_
                             where (d.DacteCreation >= inft && d.Etat == etat)
                             join c in db.CategorieActivites on d.Categorie_ equals c.Id
                             select new
                             {
                                 NomEntreprise = a.Nom,
                                 NomContact = b.Contact_name,
                                 AdresseMail = b.Adresse_mail,
                                 Numero = b.Telephone,
                                 Emails = b.Adresse_mail,
                                 CategorieActivite = c.Nom,
                                 EtatActivite = d.Etat,
                                 Prix = d.Prix,
                                 AdresseEmail_ = b.Adresse_mail,
                                 NomActivite = d.Nom
                             }).ToList().Select(x => new ActiviteReportViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomContact = x.NomContact,
                                 EtatActivite = x.EtatActivite,
                                 CategorieActivite = x.CategorieActivite,
                                 NomActivite = x.NomActivite,
                                 Prix = x.Prix,
                                 Numerotel = x.Numero,
                                 AdresseEmail = x.AdresseEmail_
                             });
            }
            else if (inf.Trim().Length != 0 && etat.Trim().Length != 0 && sup.Trim().Length != 0 && Entreprises == null)
            {
                DateTime supt = Convert.ToDateTime(sup); DateTime inft = Convert.ToDateTime(inf);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             join d in db.Activites on b.Id equals d.Contact_
                             where (d.DacteCreation <= supt && d.DacteCreation >= inft && d.Etat == etat)
                             join c in db.CategorieActivites on d.Categorie_ equals c.Id
                             select new
                             {
                                 NomEntreprise = a.Nom,
                                 NomContact = b.Contact_name,
                                 AdresseMail = b.Adresse_mail,
                                 Numero = b.Telephone,
                                 Emails = b.Adresse_mail,
                                 CategorieActivite = c.Nom,
                                 EtatActivite = d.Etat,
                                 Prix = d.Prix,
                                 AdresseEmail_ = b.Adresse_mail,
                                 NomActivite = d.Nom
                             }).ToList().Select(x => new ActiviteReportViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomContact = x.NomContact,
                                 EtatActivite = x.EtatActivite,
                                 CategorieActivite = x.CategorieActivite,
                                 NomActivite = x.NomActivite,
                                 Prix = x.Prix,
                                 Numerotel = x.Numero,
                                 AdresseEmail = x.AdresseEmail_
                             });
            }
            else if (Entreprises != null && etat.Trim().Length == 0 && inf.Trim().Length == 0  && sup.Trim().Length == 0  )
            {
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             join d in db.Activites on b.Id equals d.Contact_
                             where a.Nom == Entreprises
                             join c in db.CategorieActivites on d.Categorie_ equals c.Id
                             select new
                             {
                                 NomEntreprise = a.Nom,
                                 NomContact = b.Contact_name,
                                 AdresseMail = b.Adresse_mail,
                                 Numero = b.Telephone,
                                 Emails = b.Adresse_mail,
                                 CategorieActivite = c.Nom,
                                 EtatActivite = d.Etat,
                                 Prix = d.Prix,
                                 AdresseEmail_ = b.Adresse_mail,
                                 NomActivite = d.Nom
                             }).ToList().Select(x => new ActiviteReportViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomContact = x.NomContact,
                                 EtatActivite = x.EtatActivite,
                                 CategorieActivite = x.CategorieActivite,
                                 NomActivite = x.NomActivite,
                                 Prix = x.Prix,
                                 Numerotel = x.Numero,
                                 AdresseEmail = x.AdresseEmail_
                             });
            }
            else if (Entreprises != null && etat.Trim().Length == 0 && inf.Trim().Length == 0 && sup.Trim().Length != 0)
            {
                DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             join d in db.Activites on b.Id equals d.Contact_
                             where (a.Nom == Entreprises && d.DacteCreation <=supt)
                             join c in db.CategorieActivites on d.Categorie_ equals c.Id
                             select new
                             {
                                 NomEntreprise = a.Nom,
                                 NomContact = b.Contact_name,
                                 AdresseMail = b.Adresse_mail,
                                 Numero = b.Telephone,
                                 Emails = b.Adresse_mail,
                                 CategorieActivite = c.Nom,
                                 EtatActivite = d.Etat,
                                 Prix = d.Prix,
                                 AdresseEmail_ = b.Adresse_mail,
                                 NomActivite = d.Nom
                             }).ToList().Select(x => new ActiviteReportViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomContact = x.NomContact,
                                 EtatActivite = x.EtatActivite,
                                 CategorieActivite = x.CategorieActivite,
                                 NomActivite = x.NomActivite,
                                 Prix = x.Prix,
                                 Numerotel = x.Numero,
                                 AdresseEmail = x.AdresseEmail_
                             });
            }
            else if (Entreprises != null && etat.Trim().Length == 0 && inf.Trim().Length != 0 && sup.Trim().Length == 0)
            {
                DateTime inft = Convert.ToDateTime(inf);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             join d in db.Activites on b.Id equals d.Contact_
                             where (a.Nom == Entreprises && d.DacteCreation >=inft)
                             join c in db.CategorieActivites on d.Categorie_ equals c.Id
                             select new
                             {
                                 NomEntreprise = a.Nom,
                                 NomContact = b.Contact_name,
                                 AdresseMail = b.Adresse_mail,
                                 Numero = b.Telephone,
                                 Emails = b.Adresse_mail,
                                 CategorieActivite = c.Nom,
                                 EtatActivite = d.Etat,
                                 Prix = d.Prix,
                                 AdresseEmail_ = b.Adresse_mail,
                                 NomActivite = d.Nom
                             }).ToList().Select(x => new ActiviteReportViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomContact = x.NomContact,
                                 EtatActivite = x.EtatActivite,
                                 CategorieActivite = x.CategorieActivite,
                                 NomActivite = x.NomActivite,
                                 Prix = x.Prix,
                                 Numerotel = x.Numero,
                                 AdresseEmail = x.AdresseEmail_
                             });
            }
            else if (Entreprises != null && etat.Trim().Length == 0 && inf.Trim().Length != 0 && sup.Trim().Length != 0)
            {
                DateTime inft = Convert.ToDateTime(inf); DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             join d in db.Activites on b.Id equals d.Contact_
                             where (a.Nom == Entreprises && d.DacteCreation<=supt && d.DacteCreation>= inft)
                             join c in db.CategorieActivites on d.Categorie_ equals c.Id
                             select new
                             {
                                 NomEntreprise = a.Nom,
                                 NomContact = b.Contact_name,
                                 AdresseMail = b.Adresse_mail,
                                 Numero = b.Telephone,
                                 Emails = b.Adresse_mail,
                                 CategorieActivite = c.Nom,
                                 EtatActivite = d.Etat,
                                 Prix = d.Prix,
                                 AdresseEmail_ = b.Adresse_mail,
                                 NomActivite = d.Nom
                             }).ToList().Select(x => new ActiviteReportViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomContact = x.NomContact,
                                 EtatActivite = x.EtatActivite,
                                 CategorieActivite = x.CategorieActivite,
                                 NomActivite = x.NomActivite,
                                 Prix = x.Prix,
                                 Numerotel = x.Numero,
                                 AdresseEmail = x.AdresseEmail_
                             });
            }
            else if (Entreprises != null && etat.Trim().Length != 0 && inf.Trim().Length == 0 && sup.Trim().Length == 0)
            {
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             join d in db.Activites on b.Id equals d.Contact_
                             where a.Nom == Entreprises && d.Etat == etat
                             join c in db.CategorieActivites on d.Categorie_ equals c.Id
                             select new
                             {
                                 NomEntreprise = a.Nom,
                                 NomContact = b.Contact_name,
                                 AdresseMail = b.Adresse_mail,
                                 Numero = b.Telephone,
                                 Emails = b.Adresse_mail,
                                 CategorieActivite = c.Nom,
                                 EtatActivite = d.Etat,
                                 Prix = d.Prix,
                                 AdresseEmail_ = b.Adresse_mail,
                                 NomActivite = d.Nom
                             }).ToList().Select(x => new ActiviteReportViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomContact = x.NomContact,
                                 EtatActivite = x.EtatActivite,
                                 CategorieActivite = x.CategorieActivite,
                                 NomActivite = x.NomActivite,
                                 Prix = x.Prix,
                                 Numerotel = x.Numero,
                                 AdresseEmail = x.AdresseEmail_
                             });
            }
            else if (Entreprises != null && etat.Trim().Length != 0 && inf.Trim().Length == 0 && sup.Trim().Length != 0)
            {
                DateTime supt = Convert.ToDateTime(sup);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             join d in db.Activites on b.Id equals d.Contact_
                             where (a.Nom == Entreprises && d.Etat == etat && d.DacteCreation <= supt)
                             join c in db.CategorieActivites on d.Categorie_ equals c.Id
                             select new
                             {
                                 NomEntreprise = a.Nom,
                                 NomContact = b.Contact_name,
                                 AdresseMail = b.Adresse_mail,
                                 Numero = b.Telephone,
                                 Emails = b.Adresse_mail,
                                 CategorieActivite = c.Nom,
                                 EtatActivite = d.Etat,
                                 Prix = d.Prix,
                                 AdresseEmail_ = b.Adresse_mail,
                                 NomActivite = d.Nom
                             }).ToList().Select(x => new ActiviteReportViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomContact = x.NomContact,
                                 EtatActivite = x.EtatActivite,
                                 CategorieActivite = x.CategorieActivite,
                                 NomActivite = x.NomActivite,
                                 Prix = x.Prix,
                                 Numerotel = x.Numero,
                                 AdresseEmail = x.AdresseEmail_
                             });
            }
            else if (Entreprises != null && etat.Trim().Length != 0 && inf.Trim().Length == 0 && sup.Trim().Length == 0)
            {
                DateTime inft = Convert.ToDateTime(inf);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             join d in db.Activites on b.Id equals d.Contact_
                             where (a.Nom == Entreprises && d.Etat == etat && d.DacteCreation >= inft)
                             join c in db.CategorieActivites on d.Categorie_ equals c.Id
                             select new
                             {
                                 NomEntreprise = a.Nom,
                                 NomContact = b.Contact_name,
                                 AdresseMail = b.Adresse_mail,
                                 Numero = b.Telephone,
                                 Emails = b.Adresse_mail,
                                 CategorieActivite = c.Nom,
                                 EtatActivite = d.Etat,
                                 Prix = d.Prix,
                                 AdresseEmail_ = b.Adresse_mail,
                                 NomActivite = d.Nom
                             }).ToList().Select(x => new ActiviteReportViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomContact = x.NomContact,
                                 EtatActivite = x.EtatActivite,
                                 CategorieActivite = x.CategorieActivite,
                                 NomActivite = x.NomActivite,
                                 Prix = x.Prix,
                                 Numerotel = x.Numero,
                                 AdresseEmail = x.AdresseEmail_
                             });
            }
            else if (Entreprises != null && etat.Trim().Length != 0 && inf.Trim().Length != 0 && sup.Trim().Length != 0)
            {
                DateTime supt = Convert.ToDateTime(sup); DateTime inft = Convert.ToDateTime(inf);
                ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             join d in db.Activites on b.Id equals d.Contact_
                             where (a.Nom == Entreprises && d.Etat == etat && d.DacteCreation <= supt && d.DacteCreation >= inft)
                             join c in db.CategorieActivites on d.Categorie_ equals c.Id
                             select new
                             {
                                 NomEntreprise = a.Nom,
                                 NomContact = b.Contact_name,
                                 AdresseMail = b.Adresse_mail,
                                 Numero = b.Telephone,
                                 Emails = b.Adresse_mail,
                                 CategorieActivite = c.Nom,
                                 EtatActivite = d.Etat,
                                 Prix = d.Prix,
                                 AdresseEmail_ = b.Adresse_mail,
                                 NomActivite = d.Nom
                             }).ToList().Select(x => new ActiviteReportViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomContact = x.NomContact,
                                 EtatActivite = x.EtatActivite,
                                 CategorieActivite = x.CategorieActivite,
                                 NomActivite = x.NomActivite,
                                 Prix = x.Prix
                                 //Numerotel = x.Numero,
                                 //AdresseEmail = x.AdresseEmail_
                             });
            }
            return (ListEmpty);
            
        }
        [HttpPost]
        public JsonResult getActivitesJs(string Entreprises, string etat, string inf, string sup)
        {
            IEnumerable<ActiviteReportViewModel> empSummary = new List<ActiviteReportViewModel>();

            //Rien n'est rempli
            if (inf.Trim().Length == 0 && etat.Trim().Length == 0 && sup.Trim().Length == 0 && Entreprises == null)
            {
                empSummary = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             join d in db.Activites on b.Id equals d.Contact_
                             join c in db.CategorieActivites on d.Categorie_ equals c.Id
                             select new
                             {
                                 NomEntreprise = a.Nom,
                                 NomContact = b.Contact_name,
                                 AdresseMail = b.Adresse_mail,
                                 Numero = b.Telephone,
                                 Emails = b.Adresse_mail,
                                 CategorieActivite = c.Nom,
                                 EtatActivite = d.Etat,
                                 Prix = d.Prix,
                                 AdresseEmail_ = b.Adresse_mail,
                                 NomActivite = d.Nom
                             }).ToList().Select(x => new ActiviteReportViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomContact = x.NomContact,
                                 EtatActivite = x.EtatActivite,
                                 CategorieActivite = x.CategorieActivite,
                                 NomActivite = x.NomActivite,
                                 Prix = x.Prix,
                                 Numerotel = x.Numero,
                                 AdresseEmail = x.AdresseEmail_
                             });
            }

            else if (inf.Trim().Length == 0 && etat.Trim().Length == 0 && sup.Trim().Length != 0 && Entreprises == null)
            {
                DateTime supt = Convert.ToDateTime(sup);
                empSummary = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             join d in db.Activites on b.Id equals d.Contact_
                             where d.DacteCreation <= supt
                             join c in db.CategorieActivites on d.Categorie_ equals c.Id
                             select new
                             {
                                 NomEntreprise = a.Nom,
                                 NomContact = b.Contact_name,
                                 AdresseMail = b.Adresse_mail,
                                 Numero = b.Telephone,
                                 Emails = b.Adresse_mail,
                                 CategorieActivite = c.Nom,
                                 EtatActivite = d.Etat,
                                 Prix = d.Prix,
                                 AdresseEmail_ = b.Adresse_mail,
                                 NomActivite = d.Nom
                             }).ToList().Select(x => new ActiviteReportViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomContact = x.NomContact,
                                 EtatActivite = x.EtatActivite,
                                 CategorieActivite = x.CategorieActivite,
                                 NomActivite = x.NomActivite,
                                 Prix = x.Prix,
                                 Numerotel = x.Numero,
                                 AdresseEmail = x.AdresseEmail_
                             });
            }
            else if (inf.Trim().Length != 0 && etat.Trim().Length == 0 && sup.Trim().Length == 0 && Entreprises == null)
            {
                DateTime inft = Convert.ToDateTime(inf);
                empSummary = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             join d in db.Activites on b.Id equals d.Contact_
                             where d.DacteCreation >= inft
                             join c in db.CategorieActivites on d.Categorie_ equals c.Id
                             select new
                             {
                                 NomEntreprise = a.Nom,
                                 NomContact = b.Contact_name,
                                 AdresseMail = b.Adresse_mail,
                                 Numero = b.Telephone,
                                 Emails = b.Adresse_mail,
                                 CategorieActivite = c.Nom,
                                 EtatActivite = d.Etat,
                                 Prix = d.Prix,
                                 AdresseEmail_ = b.Adresse_mail,
                                 NomActivite = d.Nom
                             }).ToList().Select(x => new ActiviteReportViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomContact = x.NomContact,
                                 EtatActivite = x.EtatActivite,
                                 CategorieActivite = x.CategorieActivite,
                                 NomActivite = x.NomActivite,
                                 Prix = x.Prix,
                                 Numerotel = x.Numero,
                                 AdresseEmail = x.AdresseEmail_
                             });
            }
            else if (inf.Trim().Length != 0 && etat.Trim().Length == 0 && sup.Trim().Length != 0 && Entreprises == null)
            {
                DateTime supt = Convert.ToDateTime(sup); DateTime inft = Convert.ToDateTime(inf);
                empSummary = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             join d in db.Activites on b.Id equals d.Contact_
                             where (d.DacteCreation <= supt && d.DacteCreation >= inft)
                             join c in db.CategorieActivites on d.Categorie_ equals c.Id
                             select new
                             {
                                 NomEntreprise = a.Nom,
                                 NomContact = b.Contact_name,
                                 AdresseMail = b.Adresse_mail,
                                 Numero = b.Telephone,
                                 Emails = b.Adresse_mail,
                                 CategorieActivite = c.Nom,
                                 EtatActivite = d.Etat,
                                 Prix = d.Prix,
                                 AdresseEmail_ = b.Adresse_mail,
                                 NomActivite = d.Nom
                             }).ToList().Select(x => new ActiviteReportViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomContact = x.NomContact,
                                 EtatActivite = x.EtatActivite,
                                 CategorieActivite = x.CategorieActivite,
                                 NomActivite = x.NomActivite,
                                 Prix = x.Prix,
                                 Numerotel = x.Numero,
                                 AdresseEmail = x.AdresseEmail_
                             });
            }
            else if (inf.Trim().Length == 0 && etat.Trim().Length != 0 && sup.Trim().Length == 0 && Entreprises == null)
            {
                empSummary = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             join d in db.Activites on b.Id equals d.Contact_
                             where d.Etat == etat
                             join c in db.CategorieActivites on d.Categorie_ equals c.Id
                             select new
                             {
                                 NomEntreprise = a.Nom,
                                 NomContact = b.Contact_name,
                                 AdresseMail = b.Adresse_mail,
                                 Numero = b.Telephone,
                                 Emails = b.Adresse_mail,
                                 CategorieActivite = c.Nom,
                                 EtatActivite = d.Etat,
                                 Prix = d.Prix,
                                 AdresseEmail_ = b.Adresse_mail,
                                 NomActivite = d.Nom
                             }).ToList().Select(x => new ActiviteReportViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomContact = x.NomContact,
                                 EtatActivite = x.EtatActivite,
                                 CategorieActivite = x.CategorieActivite,
                                 NomActivite = x.NomActivite,
                                 Prix = x.Prix,
                                 Numerotel = x.Numero,
                                 AdresseEmail = x.AdresseEmail_
                             });
            }
            else if (inf.Trim().Length == 0 && etat.Trim().Length != 0 && sup.Trim().Length != 0 && Entreprises == null)
            {
                DateTime supt = Convert.ToDateTime(sup);
                empSummary = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             join d in db.Activites on b.Id equals d.Contact_
                             where (d.DacteCreation <= supt && d.Etat == etat)
                             join c in db.CategorieActivites on d.Categorie_ equals c.Id
                             select new
                             {
                                 NomEntreprise = a.Nom,
                                 NomContact = b.Contact_name,
                                 AdresseMail = b.Adresse_mail,
                                 Numero = b.Telephone,
                                 Emails = b.Adresse_mail,
                                 CategorieActivite = c.Nom,
                                 EtatActivite = d.Etat,
                                 Prix = d.Prix,
                                 AdresseEmail_ = b.Adresse_mail,
                                 NomActivite = d.Nom
                             }).ToList().Select(x => new ActiviteReportViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomContact = x.NomContact,
                                 EtatActivite = x.EtatActivite,
                                 CategorieActivite = x.CategorieActivite,
                                 NomActivite = x.NomActivite,
                                 Prix = x.Prix,
                                 Numerotel = x.Numero,
                                 AdresseEmail = x.AdresseEmail_
                             });
            }
            else if (inf.Trim().Length != 0 && etat.Trim().Length != 0 && sup.Trim().Length != 0 && Entreprises == null)
            {
                DateTime inft = Convert.ToDateTime(inf);
                empSummary = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             join d in db.Activites on b.Id equals d.Contact_
                             where (d.DacteCreation >= inft && d.Etat == etat)
                             join c in db.CategorieActivites on d.Categorie_ equals c.Id
                             select new
                             {
                                 NomEntreprise = a.Nom,
                                 NomContact = b.Contact_name,
                                 AdresseMail = b.Adresse_mail,
                                 Numero = b.Telephone,
                                 Emails = b.Adresse_mail,
                                 CategorieActivite = c.Nom,
                                 EtatActivite = d.Etat,
                                 Prix = d.Prix,
                                 AdresseEmail_ = b.Adresse_mail,
                                 NomActivite = d.Nom
                             }).ToList().Select(x => new ActiviteReportViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomContact = x.NomContact,
                                 EtatActivite = x.EtatActivite,
                                 CategorieActivite = x.CategorieActivite,
                                 NomActivite = x.NomActivite,
                                 Prix = x.Prix,
                                 Numerotel = x.Numero,
                                 AdresseEmail = x.AdresseEmail_
                             });
            }
            else if (inf.Trim().Length != 0 && etat.Trim().Length != 0 && sup.Trim().Length != 0 && Entreprises == null)
            {
                DateTime supt = Convert.ToDateTime(sup); DateTime inft = Convert.ToDateTime(inf);
                empSummary = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             join d in db.Activites on b.Id equals d.Contact_
                             where (d.DacteCreation <= supt && d.DacteCreation >= inft && d.Etat == etat)
                             join c in db.CategorieActivites on d.Categorie_ equals c.Id
                             select new
                             {
                                 NomEntreprise = a.Nom,
                                 NomContact = b.Contact_name,
                                 AdresseMail = b.Adresse_mail,
                                 Numero = b.Telephone,
                                 Emails = b.Adresse_mail,
                                 CategorieActivite = c.Nom,
                                 EtatActivite = d.Etat,
                                 Prix = d.Prix,
                                 AdresseEmail_ = b.Adresse_mail,
                                 NomActivite = d.Nom
                             }).ToList().Select(x => new ActiviteReportViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomContact = x.NomContact,
                                 EtatActivite = x.EtatActivite,
                                 CategorieActivite = x.CategorieActivite,
                                 NomActivite = x.NomActivite,
                                 Prix = x.Prix,
                                 Numerotel = x.Numero,
                                 AdresseEmail = x.AdresseEmail_
                             });
            }
            else if (Entreprises != null && etat.Trim().Length == 0 && inf.Trim().Length == 0 && sup.Trim().Length == 0)
            {
                empSummary = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             join d in db.Activites on b.Id equals d.Contact_
                             where a.Nom == Entreprises
                             join c in db.CategorieActivites on d.Categorie_ equals c.Id
                             select new
                             {
                                 NomEntreprise = a.Nom,
                                 NomContact = b.Contact_name,
                                 AdresseMail = b.Adresse_mail,
                                 Numero = b.Telephone,
                                 Emails = b.Adresse_mail,
                                 CategorieActivite = c.Nom,
                                 EtatActivite = d.Etat,
                                 Prix = d.Prix,
                                 AdresseEmail_ = b.Adresse_mail,
                                 NomActivite = d.Nom
                             }).ToList().Select(x => new ActiviteReportViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomContact = x.NomContact,
                                 EtatActivite = x.EtatActivite,
                                 CategorieActivite = x.CategorieActivite,
                                 NomActivite = x.NomActivite,
                                 Prix = x.Prix,
                                 Numerotel = x.Numero,
                                 AdresseEmail = x.AdresseEmail_
                             });
            }
            else if (Entreprises != null && etat.Trim().Length == 0 && inf.Trim().Length == 0 && sup.Trim().Length != 0)
            {
                DateTime supt = Convert.ToDateTime(sup);
                empSummary = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             join d in db.Activites on b.Id equals d.Contact_
                             where (a.Nom == Entreprises && d.DacteCreation <= supt)
                             join c in db.CategorieActivites on d.Categorie_ equals c.Id
                             select new
                             {
                                 NomEntreprise = a.Nom,
                                 NomContact = b.Contact_name,
                                 AdresseMail = b.Adresse_mail,
                                 Numero = b.Telephone,
                                 Emails = b.Adresse_mail,
                                 CategorieActivite = c.Nom,
                                 EtatActivite = d.Etat,
                                 Prix = d.Prix,
                                 AdresseEmail_ = b.Adresse_mail,
                                 NomActivite = d.Nom
                             }).ToList().Select(x => new ActiviteReportViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomContact = x.NomContact,
                                 EtatActivite = x.EtatActivite,
                                 CategorieActivite = x.CategorieActivite,
                                 NomActivite = x.NomActivite,
                                 Prix = x.Prix,
                                 Numerotel = x.Numero,
                                 AdresseEmail = x.AdresseEmail_
                             });
            }
            else if (Entreprises != null && etat.Trim().Length == 0 && inf.Trim().Length != 0 && sup.Trim().Length == 0)
            {
                DateTime inft = Convert.ToDateTime(inf);
                empSummary = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             join d in db.Activites on b.Id equals d.Contact_
                             where (a.Nom == Entreprises && d.DacteCreation >= inft)
                             join c in db.CategorieActivites on d.Categorie_ equals c.Id
                             select new
                             {
                                 NomEntreprise = a.Nom,
                                 NomContact = b.Contact_name,
                                 AdresseMail = b.Adresse_mail,
                                 Numero = b.Telephone,
                                 Emails = b.Adresse_mail,
                                 CategorieActivite = c.Nom,
                                 EtatActivite = d.Etat,
                                 Prix = d.Prix,
                                 AdresseEmail_ = b.Adresse_mail,
                                 NomActivite = d.Nom
                             }).ToList().Select(x => new ActiviteReportViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomContact = x.NomContact,
                                 EtatActivite = x.EtatActivite,
                                 CategorieActivite = x.CategorieActivite,
                                 NomActivite = x.NomActivite,
                                 Prix = x.Prix,
                                 Numerotel = x.Numero,
                                 AdresseEmail = x.AdresseEmail_
                             });
            }
            else if (Entreprises != null && etat.Trim().Length == 0 && inf.Trim().Length != 0 && sup.Trim().Length != 0)
            {
                DateTime inft = Convert.ToDateTime(inf); DateTime supt = Convert.ToDateTime(sup);
                empSummary = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             join d in db.Activites on b.Id equals d.Contact_
                             where (a.Nom == Entreprises && d.DacteCreation <= supt && d.DacteCreation >= inft)
                             join c in db.CategorieActivites on d.Categorie_ equals c.Id
                             select new
                             {
                                 NomEntreprise = a.Nom,
                                 NomContact = b.Contact_name,
                                 AdresseMail = b.Adresse_mail,
                                 Numero = b.Telephone,
                                 Emails = b.Adresse_mail,
                                 CategorieActivite = c.Nom,
                                 EtatActivite = d.Etat,
                                 Prix = d.Prix,
                                 AdresseEmail_ = b.Adresse_mail,
                                 NomActivite = d.Nom
                             }).ToList().Select(x => new ActiviteReportViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomContact = x.NomContact,
                                 EtatActivite = x.EtatActivite,
                                 CategorieActivite = x.CategorieActivite,
                                 NomActivite = x.NomActivite,
                                 Prix = x.Prix,
                                 Numerotel = x.Numero,
                                 AdresseEmail = x.AdresseEmail_
                             });
            }
            else if (Entreprises != null && etat.Trim().Length != 0 && inf.Trim().Length == 0 && sup.Trim().Length == 0)
            {
                empSummary = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             join d in db.Activites on b.Id equals d.Contact_
                             where a.Nom == Entreprises && d.Etat == etat
                             join c in db.CategorieActivites on d.Categorie_ equals c.Id
                             select new
                             {
                                 NomEntreprise = a.Nom,
                                 NomContact = b.Contact_name,
                                 AdresseMail = b.Adresse_mail,
                                 Numero = b.Telephone,
                                 Emails = b.Adresse_mail,
                                 CategorieActivite = c.Nom,
                                 EtatActivite = d.Etat,
                                 Prix = d.Prix,
                                 AdresseEmail_ = b.Adresse_mail,
                                 NomActivite = d.Nom
                             }).ToList().Select(x => new ActiviteReportViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomContact = x.NomContact,
                                 EtatActivite = x.EtatActivite,
                                 CategorieActivite = x.CategorieActivite,
                                 NomActivite = x.NomActivite,
                                 Prix = x.Prix,
                                 Numerotel = x.Numero,
                                 AdresseEmail = x.AdresseEmail_
                             });
            }
            else if (Entreprises != null && etat.Trim().Length != 0 && inf.Trim().Length == 0 && sup.Trim().Length != 0)
            {
                DateTime supt = Convert.ToDateTime(sup);
                empSummary = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             join d in db.Activites on b.Id equals d.Contact_
                             where (a.Nom == Entreprises && d.Etat == etat && d.DacteCreation <= supt)
                             join c in db.CategorieActivites on d.Categorie_ equals c.Id
                             select new
                             {
                                 NomEntreprise = a.Nom,
                                 NomContact = b.Contact_name,
                                 AdresseMail = b.Adresse_mail,
                                 Numero = b.Telephone,
                                 Emails = b.Adresse_mail,
                                 CategorieActivite = c.Nom,
                                 EtatActivite = d.Etat,
                                 Prix = d.Prix,
                                 AdresseEmail_ = b.Adresse_mail,
                                 NomActivite = d.Nom
                             }).ToList().Select(x => new ActiviteReportViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomContact = x.NomContact,
                                 EtatActivite = x.EtatActivite,
                                 CategorieActivite = x.CategorieActivite,
                                 NomActivite = x.NomActivite,
                                 Prix = x.Prix,
                                 Numerotel = x.Numero,
                                 AdresseEmail = x.AdresseEmail_
                             });
            }
            else if (Entreprises != null && etat.Trim().Length != 0 && inf.Trim().Length == 0 && sup.Trim().Length == 0)
            {
                DateTime inft = Convert.ToDateTime(inf);
                empSummary = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             join d in db.Activites on b.Id equals d.Contact_
                             where (a.Nom == Entreprises && d.Etat == etat && d.DacteCreation >= inft)
                             join c in db.CategorieActivites on d.Categorie_ equals c.Id
                             select new
                             {
                                 NomEntreprise = a.Nom,
                                 NomContact = b.Contact_name,
                                 AdresseMail = b.Adresse_mail,
                                 Numero = b.Telephone,
                                 Emails = b.Adresse_mail,
                                 CategorieActivite = c.Nom,
                                 EtatActivite = d.Etat,
                                 Prix = d.Prix,
                                 AdresseEmail_ = b.Adresse_mail,
                                 NomActivite = d.Nom
                             }).ToList().Select(x => new ActiviteReportViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomContact = x.NomContact,
                                 EtatActivite = x.EtatActivite,
                                 CategorieActivite = x.CategorieActivite,
                                 NomActivite = x.NomActivite,
                                 Prix = x.Prix,
                                 Numerotel = x.Numero,
                                 AdresseEmail = x.AdresseEmail_
                             });
            }
            else if (Entreprises != null && etat.Trim().Length != 0 && inf.Trim().Length != 0 && sup.Trim().Length != 0)
            {
                DateTime supt = Convert.ToDateTime(sup); DateTime inft = Convert.ToDateTime(inf);
                empSummary = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             join d in db.Activites on b.Id equals d.Contact_
                             where (a.Nom == Entreprises && d.Etat == etat && d.DacteCreation <= supt && d.DacteCreation >= inft)
                             join c in db.CategorieActivites on d.Categorie_ equals c.Id
                             select new
                             {
                                 NomEntreprise = a.Nom,
                                 NomContact = b.Contact_name,
                                 AdresseMail = b.Adresse_mail,
                                 Numero = b.Telephone,
                                 Emails = b.Adresse_mail,
                                 CategorieActivite = c.Nom,
                                 EtatActivite = d.Etat,
                                 Prix = d.Prix,
                                 AdresseEmail_ = b.Adresse_mail,
                                 NomActivite = d.Nom
                             }).ToList().Select(x => new ActiviteReportViewModel()
                             {
                                 NomEntreprise = x.NomEntreprise,
                                 NomContact = x.NomContact,
                                 EtatActivite = x.EtatActivite,
                                 CategorieActivite = x.CategorieActivite,
                                 NomActivite = x.NomActivite,
                                 Prix = x.Prix,
                                 Numerotel = x.Numero,
                                 AdresseEmail = x.AdresseEmail_
                             });
            }
            return Json(empSummary,JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult getVentesJs(string Entreprise, string ContactId, string Categorie, string ProduitId, string Prixinf, string Prixsup, string inf, string sup)
        {
            IEnumerable<VenteProduitsViewModel> empSummary = new List<VenteProduitsViewModel>();
            empSummary = RetriveData.RapportVente(Entreprise, ContactId, Categorie, ProduitId, Prixinf, Prixsup, inf, sup);
             return Json(empSummary, JsonRequestBehavior.AllowGet);
        }

        // GET: DalRapports
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Entreprises()
        {
            return View();
        }

        public ActionResult Contacts()
        {
            return View();
        }

        public ActionResult Activites()
        {
            var listEntreprises = (from a in db.Entreprises
                                   select new SelectListItem
                                   {
                                       Text = a.Nom,
                                       Value = a.Nom
                                   });

            ViewData["Entreprises"] = listEntreprises;
            return View();
        }
       
        public ActionResult Produits()
        {
            var listCategories = (from a in db.CategorieProduits
                                   select new SelectListItem
                                   {
                                       Text = a.Nom,
                                       Value = a.Nom
                                   });
         
            ViewData["Categorie"] = listCategories;
            return View();
        }
        public ActionResult Ventes()
        {
            ViewBag.Categorie = new SelectList(db.CategorieActivites, "Id", "Nom");
            ViewBag.Entreprise = new SelectList(db.Entreprises, "Id", "Nom");
            return View();
        }

        //**************************************Entreprises**************************************
        [HttpPost]
        public ActionResult EntreprisesPdf(string pays,string inf,string sup)
        {
            rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Rapports/EntreprisesReports.rpt")));
            //Rien n'est rempli
            
            rd.SetDataSource(getEntreprises(pays,inf, sup));
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();rd.Dispose();

            return File(stream, "application/pdf", "ListEntreprise.pdf");
        }
        [HttpPost]
        public ActionResult EntreprisesExcel(string pays, string inf, string sup)
        {
            rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Rapports/EntreprisesReports.rpt")));
            rd.SetDataSource(getEntreprises(pays, inf, sup));
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close(); rd.Dispose();

            return File(stream, "application/xls", "ListEntreprise.xls");
        }
        [HttpPost]
        public ActionResult EntreprisesWord(string pays, string inf, string sup)
        {
            rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Rapports/EntreprisesReports.rpt")));
            rd.SetDataSource(getEntreprises(pays, inf, sup));
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close(); rd.Dispose();

            return File(stream, "application/doc", "ListEntreprise.doc");
        }

       
      


        //************************************************CONTACTS***********************************************************
        [HttpPost]
        public ActionResult ContactsPdf(string pays, string inf, string sup)
        {
            rd = new ReportDocument();
            var test = getContacts(pays, inf, sup);
           rd.Load(Path.Combine(Server.MapPath("~/Rapports/AllContacts.rpt")));
            //Rien n'est rempli   
            rd.SetDataSource(test);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close(); rd.Dispose();

            return File(stream, "application/pdf", "ListeContacts.pdf");
        }
        [HttpPost]
        public ActionResult ContactsExcel(string pays, string inf, string sup)
        {
            rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Rapports/AllContacts.rpt")));
            //Rien n'est rempli
            var test = getContacts(pays, inf, sup);

            rd.SetDataSource(test);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close(); rd.Dispose();

            return File(stream, "application/xls", "ListeContacts.xls");
        }
        [HttpPost]
        public ActionResult ContactsWord(string pays, string inf, string sup)
        {

            rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Rapports/AllContacts.rpt")));
            //Rien n'est rempli
            var test = getContacts(pays, inf, sup);

            rd.SetDataSource(test);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();rd.Dispose();

            return File(stream, "application/doc", "ListeContacts.doc");
        }
        //**************************************************ACTIVITES********************************************************
       

        [HttpPost]
        public ActionResult ActivitesPdf(string entreprise ,string etat ,string inf, string sup)
        {

            rd = new ReportDocument();
            var test = getActivites(entreprise,etat, inf, sup);
            rd.Load(Path.Combine(Server.MapPath("~/Rapports/ActivitesSimple.rpt")));

            rd.SetDataSource(test);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();

            return File(stream, "application/pdf", "AllActivites.pdf");
        }
        [HttpPost]
        public ActionResult ActivitesExcel(string entreprise, string etat, string inf, string sup)
        {
            rd = new ReportDocument();
            var test = getActivites( entreprise, etat, inf, sup);
            rd.Load(Path.Combine(Server.MapPath("~/Rapports/ActivitesSimple.rpt")));

            rd.SetDataSource(test);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();

            return File(stream, "application/xls", "AllActivites.xls");
        }
        [HttpPost]
        public ActionResult ActivitesWord(string entreprise, string etat, string inf, string sup)
        {
            rd = new ReportDocument();
            var test = getActivites( entreprise, etat, inf, sup);
            rd.Load(Path.Combine(Server.MapPath("~/Rapports/ActivitesSimple.rpt")));

            rd.SetDataSource(test);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close(); rd.Dispose();

            return File(stream, "application/doc", "AllActivites.doc");
        }

        //****************************************************PRODUITS*******************************************************


        [HttpPost]
        public ActionResult ProduitsPdf(string categorie, string Prixinf, string Prixsup)
        {
            rd = new ReportDocument();
            var test = getProduits(categorie, Prixinf, Prixsup);
            rd.Load(Path.Combine(Server.MapPath("~/Rapports/ProduitReport.rpt")));

            rd.SetDataSource(test);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close(); rd.Dispose();

            return File(stream, "application/pdf", "Produits.pdf");
        }
        [HttpPost]
        public ActionResult ProduitsExcel(string categorie, string Prixinf, string Prixsup)
        {
            rd = new ReportDocument();
            var test = getProduits( categorie, Prixinf, Prixsup);
            rd.Load(Path.Combine(Server.MapPath("~/Rapports/ProduitReport.rpt")));

            rd.SetDataSource(test);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close(); rd.Dispose();

            return File(stream, "application/xls", "Produits.xls");
        }
        [HttpPost]
        public ActionResult ProduitsWord(string categorie, string Prixinf, string Prixsup)
        {
            rd = new ReportDocument();
            var test = getProduits(categorie, Prixinf, Prixsup);
            rd.Load(Path.Combine(Server.MapPath("~/Rapports/ProduitReport.rpt")));

            rd.SetDataSource(test);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close(); rd.Dispose();

            return File(stream, "application/doc", "Produits.doc");
        }

        //***************************************************VENTES***********************************************************

        public ActionResult VentesPdf(string Entreprise, string ContactId, string Categorie, string ProduitId, string Prixinf, string Prixsup, string inf, string sup)
        {
            rd = new ReportDocument();
            var test = RetriveData.RapportVente (Entreprise, ContactId, Categorie, ProduitId, Prixinf, Prixsup, inf, sup);
            rd.Load(Path.Combine(Server.MapPath("~/Rapports/VentesRapport.rpt")));

            rd.SetDataSource(test);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close(); rd.Dispose();

            return File(stream, "application/pdf", "Ventes.pdf");
        }
        public ActionResult VentesExcel(string Entreprise, string ContactId, string Categorie, string ProduitId, string Prixinf, string Prixsup, string inf, string sup)
        {
            rd = new ReportDocument();
            var test = RetriveData.RapportVente(Entreprise, ContactId, Categorie, ProduitId, Prixinf, Prixsup, inf, sup);

            rd.Load(Path.Combine(Server.MapPath("~/Rapports/VentesRapport.rpt")));

            rd.SetDataSource(test);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();

            return File(stream, "application/xls", "Produits.xls");
        }
        public ActionResult VentesWord(string Entreprise, string ContactId, string Categorie, string ProduitId, string Prixinf, string Prixsup, string inf, string sup)
        {
            rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Rapports/VentesRapport.rpt")));
            rd.SetDataSource(RetriveData.RapportVente(Entreprise, ContactId, Categorie, ProduitId, Prixinf, Prixsup, inf, sup));
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close(); rd.Dispose();

            return File(stream, "application/doc", "Produits.doc");
        }

        //**************************************************************************

    }
}