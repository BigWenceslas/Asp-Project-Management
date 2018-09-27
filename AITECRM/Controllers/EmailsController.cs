using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using AITECRM.Models;
using System.IO;
using System.Net.Mail;

namespace AITECRM.Controllers
{
    [Authorize(Roles = ("Admin,Communications,Semi_Admin"))]
    public class EmailsController : Controller
    {
        private AITECRMContext db = new AITECRMContext();

        // GET: Emails
        public async Task<ActionResult> Index()
        {
            return View(await db.Emails.ToListAsync());
        }

        // GET: Emails/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Email email = await db.Emails.FindAsync(id);
            if (email == null)
            {
                return HttpNotFound();
            }
            return View(email);
        }

        // GET: Emails/Create
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Campagne()
        {
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
        public ActionResult CampagneContactsEmails()
        {
            IEnumerable<Contact> categories = new List<Contact>();
            ViewBag.personnes = db.Contacts.ToList();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> email(string message, string objet)
        {
            try
            {
                EmailServeur ConfigActive = await db.EmailServeurs.Where(x => x.EtatConfig.ToLower() == "actif").FirstOrDefaultAsync();
                var emails = Request["email"].Split(',');
                for (int i = 0; i < emails.Length; i++)
                {
                    Attachment[] listefichier = new Attachment[1];
                    string transfertemail = emails[i];
                    Contact ContactWithId = new Contact();
                    ContactWithId = db.Contacts.SingleOrDefault(x => x.Adresse_mail == transfertemail);
                    var idid = ContactWithId.Id;
                    Email DetailEmail = new Email
                    {
                        Message = message,
                        Addresse_ = idid,
                        Message_object = "",
                        Date_envoie = DateTime.Now
                    };
                    MailMessage mail = new MailMessage(ConfigActive.AdresseCRM, transfertemail, objet, message);
                    mail.IsBodyHtml = true;
                    int p = Request.Files.Count;
                    if (p > 0)
                    {
                        listefichier = new Attachment[p];
                    }
                    for (int j = 0; j < p; j++)
                    {
                        var file = Request.Files[j];
                        if (file != null && file.ContentLength > 0)
                        {
                            var path = Path.Combine(Server.MapPath("~/App_Data/email/"), file.FileName);
                            file.SaveAs(path);

                            listefichier[j] = new Attachment(path);
                            mail.Attachments.Add(listefichier[j]);
                        }
                    }
                    SmtpClient client = new SmtpClient(ConfigActive.ServeurAdresse, ConfigActive.Port);
                    client.UseDefaultCredentials = false;
                    NetworkCredential cred = new NetworkCredential(ConfigActive.AdresseCRM, ConfigActive.PassWordCRM);
                    client.Credentials = cred;
                    client.EnableSsl = true;
                    client.Send(mail);
                    db.Emails.Add(DetailEmail);
                    db.SaveChanges();

                }
                db.Dispose();
                return RedirectToAction("SuccesEmails");
            }
            catch (Exception)
            {
                return RedirectToAction("ErrorEmail");
            }

        }
        // POST: Emails/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Addresse_,Message_object,Message,Date_envoie")] Email email)
        {
            if (ModelState.IsValid)
            {
                db.Emails.Add(email);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(email);
        }

        // GET: Emails/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Email email = await db.Emails.FindAsync(id);
            if (email == null)
            {
                return HttpNotFound();
            }
            return View(email);
        }

        // POST: Emails/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Addresse_,Message_object,Message,Date_envoie")] Email email)
        {
            if (ModelState.IsValid)
            {
                db.Entry(email).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(email);
        }

        // GET: Emails/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Email email = await db.Emails.FindAsync(id);
            if (email == null)
            {
                return HttpNotFound();
            }
            return View(email);
        }

        // POST: Emails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Email email = await db.Emails.FindAsync(id);
            db.Emails.Remove(email);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
