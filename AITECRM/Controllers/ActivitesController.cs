using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AITECRM.Models;
using AITECRM.Controllers;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Net.Mail;
using System.Diagnostics;
using AITECRM.ViewModels;
using Hangfire;

namespace AITECRM.Controllers
{
    [Authorize(Roles =("Admin,Operateur,Semi_Admin"))]
    public class ActivitesController : Controller
    {
        private AITECRMContext db = new AITECRMContext();

       // GET: Activites
        public ActionResult Index()
        {
           
            IEnumerable<ActiviteViewModel> ListActivites = new List<ActiviteViewModel>();
            ListActivites = (from a in db.Activites
                             join b in db.CategorieActivites on a.Categorie_ equals b.Id
                             join c in db.Contacts on a.Contact_ equals c.Id
                             join d in db.Entreprises on c.Entreprise_ equals d.Id
                             select new
                             {
                                 IdA = a.Id,
                                 NomA = a.Nom,
                                 Etat_A = a.Etat,
                                 Nombref = a.FileDetails.Count(),
                                 CategorieA = b.Nom,
                                 DateA = a.DacteCreation,
                                 NomEntreprise = d.Nom,
                                 //TelE = d.Telephone,
                                 //EmailE = d.Adresse_Email,
                                 NomContact = c.Contact_name,
                                 //TelA = c.Telephone,
                                 Description = a.Description,
                                 // EmailA = c.Adresse_mail,
                                 prix = a.Prix,
                                 Dsave = a.DacteCreation
                             }).ToList()
                                 .Select(p => new ActiviteViewModel()
                                 {
                                     ActiviteId = p.IdA,
                                     NomActivite = p.NomA,
                                     EtatActivite = p.Etat_A,
                                     NombreFichiers = p.Nombref,
                                     CategorieActivite = p.CategorieA,
                                      //DateCreation = p.DateA,
                                      NomEntreprise = p.NomEntreprise,
                                      //NumEntreprise = p.TelE,
                                      //AdresseEmailEntreprise = p.EmailE,
                                      NomContact = p.NomContact,
                                      //Numero = p.TelA,
                                      //AdresseEmail = p.EmailA,
                                      Prix = p.prix,
                                     Description = p.Description,
                                     DateCreation = p.Dsave
                                 }).ToList();

            return View( ListActivites);
        }

        public ActionResult ExportPdf()
        {
            try
            {
                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/Rapports/ActivitesSimple.rpt")));
                var ListActivites = (from a in db.Activites
                                     join b in db.CategorieActivites on a.Categorie_ equals b.Id
                                     join c in db.Contacts on a.Contact_ equals c.Id
                                     join d in db.Entreprises on c.Entreprise_ equals d.Id
                                     select new
                                     {
                                         IdA = a.Id,
                                         NomA = a.Nom,
                                         Etat_A = a.Etat,
                                         Nombref = a.FileDetails.Count(),
                                         CategorieA = b.Nom,
                                         DateA = a.DacteCreation,
                                         NomEntreprise = d.Nom,
                                         TelE = d.Telephone,
                                         EmailE = d.Adresse_Email,
                                         NomContact = c.Contact_name,
                                         TelA = c.Telephone,
                                         EmailA = c.Adresse_mail,
                                     }).ToList()
                                       .Select(p => new ActiviteViewModel()
                                       {
                                           ActiviteId = p.IdA,
                                           NomActivite = p.NomA,
                                           EtatActivite = p.Etat_A,
                                           NombreFichiers = p.Nombref,
                                           CategorieActivite = p.CategorieA,
                                           DateCreation = p.DateA,
                                           NomEntreprise = p.NomEntreprise,
                                           NumEntreprise = p.TelE,
                                           AdresseEmailEntreprise = p.EmailE,
                                           NomContact = p.NomContact,
                                           Numero = p.TelA,
                                           AdresseEmail = p.EmailA
                                       });
                rd.SetDataSource(ListActivites.Select(p => new
                {
                    NomEntreprise = p.NomEntreprise,
                    NomContact = p.NomContact,
                    CategorieActivite = p.CategorieActivite,
                    Numero = p.NomActivite,
                    EtatActivite = p.EtatActivite,
                    DateCreation = p.DateCreation,
                    NombreFichiers = p.NombreFichiers
                }));
                db.Dispose();
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                db.Dispose();
                return File(stream, "application/pdf", "ListActivites.pdf");

            }
            catch (Exception)
            {

                return RedirectToAction("ErreurExportation");
            }


        }
        public ActionResult ExportExcel()
        {
            try
            {
                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/Rapports/ActivitesSimple.rpt")));
                var ListActivites = (from a in db.Activites
                                     join b in db.CategorieActivites on a.Categorie_ equals b.Id
                                     join c in db.Contacts on a.Contact_ equals c.Id
                                     join d in db.Entreprises on c.Entreprise_ equals d.Id
                                     select new
                                     {
                                         IdA = a.Id,
                                         NomA = a.Nom,
                                         Etat_A = a.Etat,
                                         Nombref = a.FileDetails.Count(),
                                         CategorieA = b.Nom,
                                         DateA = a.DacteCreation,
                                         NomEntreprise = d.Nom,
                                         TelE = d.Telephone,
                                         EmailE = d.Adresse_Email,
                                         NomContact = c.Contact_name,
                                         TelA = c.Telephone,
                                         EmailA = c.Adresse_mail,
                                     }).ToList()
                                       .Select(p => new ActiviteViewModel()
                                       {
                                           ActiviteId = p.IdA,
                                           NomActivite = p.NomA,
                                           EtatActivite = p.Etat_A,
                                           NombreFichiers = p.Nombref,
                                           CategorieActivite = p.CategorieA,
                                           DateCreation = p.DateA,
                                           NomEntreprise = p.NomEntreprise,
                                           NumEntreprise = p.TelE,
                                           AdresseEmailEntreprise = p.EmailE,
                                           NomContact = p.NomContact,
                                           Numero = p.TelA,
                                           AdresseEmail = p.EmailA
                                       });
                rd.SetDataSource(ListActivites.Select(p => new
                {
                    NomEntreprise = p.NomEntreprise,
                    NomContact = p.NomContact,
                    CategorieActivite = p.CategorieActivite,
                    Numero = p.NomActivite,
                    EtatActivite = p.EtatActivite,
                    DateCreation = p.DateCreation,
                    NombreFichiers = p.NombreFichiers
                }));
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                stream.Seek(0, SeekOrigin.Begin);
                db.Dispose();
                return File(stream, "application/xls", "ListActivites.xls");
            }
            catch (Exception)
            {

                return RedirectToAction("ErreurExportation");
            }


        }

        public ActionResult ExportWord()
        {
            try
            {
                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/Rapports/ActivitesSimple.rpt")));
                var ListActivites = (from a in db.Activites
                                     join b in db.CategorieActivites on a.Categorie_ equals b.Id
                                     join c in db.Contacts on a.Contact_ equals c.Id
                                     join d in db.Entreprises on c.Entreprise_ equals d.Id
                                     select new
                                     {
                                         IdA = a.Id,
                                         NomA = a.Nom,
                                         Etat_A = a.Etat,
                                         Nombref = a.FileDetails.Count(),
                                         CategorieA = b.Nom,
                                         DateA = a.DacteCreation,
                                         NomEntreprise = d.Nom,
                                         TelE = d.Telephone,
                                         EmailE = d.Adresse_Email,
                                         NomContact = c.Contact_name,
                                         TelA = c.Telephone,
                                         EmailA = c.Adresse_mail,
                                     }).ToList()
                                       .Select(p => new ActiviteViewModel()
                                       {
                                           ActiviteId = p.IdA,
                                           NomActivite = p.NomA,
                                           EtatActivite = p.Etat_A,
                                           NombreFichiers = p.Nombref,
                                           CategorieActivite = p.CategorieA,
                                           DateCreation = p.DateA,
                                           NomEntreprise = p.NomEntreprise,
                                           NumEntreprise = p.TelE,
                                           AdresseEmailEntreprise = p.EmailE,
                                           NomContact = p.NomContact,
                                           Numero = p.TelA,
                                           AdresseEmail = p.EmailA
                                       });
                rd.SetDataSource(ListActivites.Select(p => new
                {
                    NomEntreprise = p.NomEntreprise,
                    NomContact = p.NomContact,
                    CategorieActivite = p.CategorieActivite,
                    Numero = p.NomActivite,
                    EtatActivite = p.EtatActivite,
                    DateCreation = p.DateCreation,
                    NombreFichiers = p.NombreFichiers
                }));
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
                stream.Seek(0, SeekOrigin.Begin);
                db.Dispose();
                return File(stream, "application/doc", "ListActivites.doc");
            }
            catch (Exception)
            {

                return RedirectToAction("ErreurExportation");
            }


        }


        //GET: Activites/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activite activite = await db.Activites.FindAsync(id);
            activite.Categorie = db.CategorieActivites.Where(x => x.Id == activite.Categorie_).FirstOrDefault();
            activite.FileDetails = db.FileDetailsActivites.Where(x => x.SupportId == id).ToList();
            activite.Contact = db.Contacts.Where(x => x.Id == activite.Contact_).FirstOrDefault();
            if (activite == null)
            {
                return HttpNotFound();
            }
            return View(activite);
        }

        // GET: Activites/Create
        public ActionResult Create()
        {
            ViewBag.Categorie_ = new SelectList(db.CategorieActivites, "Id", "Nom");
            ViewBag.Contact_ = new SelectList(db.Contacts, "Id", "Contact_name");
            return View();
        }

        // POST: Activites/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nom,Etat,Description,Categorie_,Contact_,DacteCreation,Prix")] Activite activite)
        {
            ViewBag.Categorie_ = new SelectList(db.CategorieActivites, "Id", "Nom", activite.Categorie_);
            ViewBag.Contact_ = new SelectList(db.Contacts, "Id", "Contact_name", activite.Contact_);
                        if (ModelState.IsValid)
            {
               List<FileDetailsActivite> fileDetails = new List<FileDetailsActivite>();
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        FileDetailsActivite fileDetail = new FileDetailsActivite()
                        {
                            FileName = fileName,
                            Extension = Path.GetExtension(fileName),
                            Id = Guid.NewGuid()
                        };
                        fileDetails.Add(fileDetail);
                        var path = Path.Combine(Server.MapPath("~/App_Data/Upload/"), fileDetail.Id + fileDetail.Extension);
                        file.SaveAs(path);
                    }
                }
                activite.FileDetails = fileDetails;

                db.Activites.Add(activite);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(activite);
        }

        // GET: Activites/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Categorie_ = new SelectList(db.CategorieActivites, "Id", "Nom");
            ViewBag.Contact_ = new SelectList(db.Contacts, "Id", "Contact_name");
            Activite activite = await db.Activites.FindAsync(id);
            if (activite == null)
            {
                return HttpNotFound();
            }
            return View(activite);
        }

        // POST: Activites/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nom,Etat,Description,Categorie_,Contact_,DacteCreation,Prix")] Activite activite)
        {
            ViewBag.Categorie_ = new SelectList(db.CategorieActivites, "Id", "Nom", activite.Categorie_);
            ViewBag.Contact_ = new SelectList(db.Contacts, "Id", "Contact_name", activite.Contact_);
                if (ModelState.IsValid)
            {
                List<FileDetailsActivite> fileDetails = new List<FileDetailsActivite>();
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        FileDetailsActivite fileDetail = new FileDetailsActivite()
                        {
                            FileName = fileName,
                            Extension = Path.GetExtension(fileName),
                            Id = Guid.NewGuid(),
                            SupportId = activite.Id
                        };
                        fileDetails.Add(fileDetail);
                        activite.FileDetails = fileDetails;
                        var path = Path.Combine(Server.MapPath("~/App_Data/Upload/"), fileDetail.Id + fileDetail.Extension);
                        file.SaveAs(path);

                        db.Entry(fileDetail).State = EntityState.Added;
                    }
                }

                db.Entry(activite).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(activite);
        }
        public ActionResult SuccesSms()
        {
            ViewBag.m1 = "Les SMS ont ete envoyes";
            return View();
        }
        public ActionResult ErreurSms()
        {
            ViewBag.m1 = "Les SMS n'ont pas ete envoyes";
            ViewBag.m2 = "Veuillez verifier votre connexion internet";
            return View();
        }

        public ActionResult ErreurExportation()
        {
            ViewBag.m1 = "Les rapports n'ont pas ete generes";
            ViewBag.m2 = "Veuillez verifier la connexion a la base de donnees";
            return View();
        }

        public ActionResult ErrorEmail()
        {
            ViewBag.m1 = "Les Emails n'ont pas pu etre envoyes";
            ViewBag.m2 = "Verifiez votre connexion Internet";
            return View();
        }
        public ActionResult SuccesEmails()
        {
            ViewBag.m1 = "Les Emails ont ete envoyes";
            return View();
        }
        [HttpPost]
        public ActionResult SendEmailsDetails(string message, string email, int IdContact, string NomActivite)
        {
            Directory.CreateDirectory(Server.MapPath("~/App_Data/email/"));
            EmailServeur ConfActive = db.EmailServeurs.Where(x => x.EtatConfig == "actif").FirstOrDefault();
            try
            {
                Attachment[] listefichier = new Attachment[1];
                Email DetailEmail = new Email();
                DetailEmail.Message = message;
                DetailEmail.Addresse_ = IdContact;

                MailMessage mail = new MailMessage(ConfActive.AdresseCRM, email, NomActivite, message);
                mail.IsBodyHtml = true;

                int p = Request.Files.Count;
                if (p > 0)
                {
                    listefichier = new Attachment[p];
                }
                for (int i = 0; i < p; i++)
                {
                    var file = Request.Files[i];
                    if (file != null && file.ContentLength > 0)
                    {
                        var path = Path.Combine(Server.MapPath("~/App_Data/email/"), file.FileName);
                        file.SaveAs(path);

                        listefichier[i] = new Attachment(path);
                        mail.Attachments.Add(listefichier[i]);

                    }
                }

                SmtpClient client = new SmtpClient(ConfActive.ServeurAdresse, ConfActive.Port);
                client.UseDefaultCredentials = false;
                NetworkCredential cred = new NetworkCredential(ConfActive.AdresseCRM,ConfActive.PassWordCRM);
                client.Credentials = cred;
                client.EnableSsl = true;
                client.Send(mail);

                Email Envoye = new Email
                {
                    Message = message,
                    Addresse_ = IdContact,
                    Message_object = "",
                    Date_envoie = DateTime.Now


                };
                db.Emails.Add(Envoye); db.SaveChanges();
                db.Dispose();
                return RedirectToAction("SuccesEmails");
            }
            catch (Exception)
            {
                return RedirectToAction("ErrorEmail");
            }

        }
        [HttpPost]

        public ActionResult SendSmsDetails(string message, int numero)
        {
            ServeurSms SmsActiv = db.ServeurSmss.Where(x => x.EtatConfig == "actif").FirstOrDefault();
            try
            {
                Process myprocess = new Process();
                myprocess.StartInfo.FileName = @"C:\Program Files\Internet Explorer\iexplore.exe";
                myprocess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                Sms DetailSms = new Sms();
                //string username = "aiteconsulting";
                //string password = "aitesolutionsarl";
                //string senderId = "AITE";string url = "https://bulksms.vsms.net/eapi/submission/send_sms/2/2.0?username="

                string SMSText = message;
                string dest = "+237" + numero;
                string url = SmsActiv.ServerAdress + SmsActiv.Username + "&password=" + SmsActiv.PasswordUser + "&sender=" + SmsActiv.SenderId + "&message=" + SMSText + "&msisdn=" + dest;
                System.Net.Sockets.TcpClient clnt = new System.Net.Sockets.TcpClient(SmsActiv.ServerAdress, SmsActiv.Port);
                clnt.Close();
                myprocess.StartInfo.Arguments = url;
                myprocess.Start();
                Process.Start(url);
                myprocess.WaitForExit(6000);
                db.Dispose();
                return RedirectToAction("SuccesSms");
            }
            catch (Exception)
            {
                return RedirectToAction("ErreurSms");

            }


        }

        public FileResult Download(String p, String d)
        {
            return File(Path.Combine(Server.MapPath("~/App_Data/Upload/"), p), System.Net.Mime.MediaTypeNames.Application.Octet, d);
        }
        //// GET: Activites/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Activite activite = await db.Activites.FindAsync(id);
        //    if (activite == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(activite);
        //}

        //// POST: Activites/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    Activite activite = await db.Activites.FindAsync(id);
        //    db.Activites.Remove(activite);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}
        [HttpPost]
        public JsonResult DeleteFile(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Result = "Error" });
            }
            try
            {
                Guid guid = new Guid(id);
                FileDetailsActivite fileDetail = db.FileDetailsActivites.Find(guid);
                if (fileDetail == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Json(new { Result = "Error" });
                }

                //Remove from database
                db.FileDetailsActivites.Remove(fileDetail);
                db.SaveChanges();

                //Delete file from the file system
                var path = Path.Combine(Server.MapPath("~/App_Data/Upload/"), fileDetail.Id + fileDetail.Extension);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERREUR", Message = ex.Message });
            }
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles ="Admin,Semi_Admin")]
        public JsonResult Delete(int id)
        {
            try
            {
                Activite support = db.Activites.Find(id);
                if (support == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Json(new { Result = "Error" });
                }

                //delete files from the file system

                foreach (var item in support.FileDetails)
                {
                    String path = Path.Combine(Server.MapPath("~/App_Data/Upload/"), item.Id + item.Extension);
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                }

                db.Activites.Remove(support);
                db.SaveChanges();
                db.Dispose();
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
