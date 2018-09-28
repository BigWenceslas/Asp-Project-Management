using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using AITECRM.Models;
using System.Net.Mail;
using System.IO;

namespace AITECRM.Controllers
{
    [Authorize(Roles = ("Admin,Communications,Semi_Admin"))]
    public class EmailEntreprisesController : Controller
    {
        private AITECRMContext db = new AITECRMContext();

        // GET: EmailEntreprises
        public async Task<ActionResult> Index()
        {
            return View(await db.EmailEntreprises.ToListAsync());
        }

        // GET: EmailEntreprises/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailEntreprise emailEntreprise = await db.EmailEntreprises.FindAsync(id);
            if (emailEntreprise == null)
            {
                return HttpNotFound();
            }
            return View(emailEntreprise);
        }

        // GET: EmailEntreprises/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmailEntreprises/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Addresse_,Message_object,Message,Date_envoie")] EmailEntreprise emailEntreprise)
        {
            if (ModelState.IsValid)
            {
                db.EmailEntreprises.Add(emailEntreprise);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(emailEntreprise);
        }

        // GET: EmailEntreprises/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailEntreprise emailEntreprise = await db.EmailEntreprises.FindAsync(id);
            if (emailEntreprise == null)
            {
                return HttpNotFound();
            }
            return View(emailEntreprise);
        }

        // POST: EmailEntreprises/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Addresse_,Message_object,Message,Date_envoie")] EmailEntreprise emailEntreprise)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emailEntreprise).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(emailEntreprise);
        }

        // GET: EmailEntreprises/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailEntreprise emailEntreprise = await db.EmailEntreprises.FindAsync(id);
            if (emailEntreprise == null)
            {
                return HttpNotFound();
            }
            return View(emailEntreprise);
        }

        // POST: EmailEntreprises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            EmailEntreprise emailEntreprise = await db.EmailEntreprises.FindAsync(id);
            db.EmailEntreprises.Remove(emailEntreprise);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public ActionResult CampagneEntreprisesEmails()
        {
            IEnumerable<Entreprise> categories = new List<Entreprise>();
            categories = db.Entreprises.ToList();
            ViewBag.personnes = categories;
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
                    Entreprise EntrepriseWithId = new Entreprise();
                    EntrepriseWithId = db.Entreprises.SingleOrDefault(x => x.Adresse_Email == transfertemail);
                    var idid = EntrepriseWithId.Id;
                    EmailEntreprise DetailEmail = new EmailEntreprise
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
                    db.EmailEntreprises.Add(DetailEmail);
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

        public ActionResult SuccesEmails()
        {
            ViewBag.m1 = "Les Emails ont ete envoyes";
            return View();
        }
        public ActionResult ErrorEmail()
        {
            ViewBag.m1 = "Les Emails n'ont pas pu etre envoyes";
            ViewBag.m2 = "Verifiez votre connexion Internet";
            return View();
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
