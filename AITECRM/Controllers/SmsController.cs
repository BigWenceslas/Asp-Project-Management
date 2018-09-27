using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using AITECRM.Models;
using System.Diagnostics;

namespace AITECRM.Controllers
{
    [Authorize(Roles = ("Admin,Communications,Semi_Admin"))]
    public class SmsController : Controller
    {
     
        private AITECRMContext db = new AITECRMContext();

        // GET: Sms
        public async Task<ActionResult> Index()
        {
            return View(await db.Smss.ToListAsync());
        }

        // GET: Sms/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sms sms = await db.Smss.FindAsync(id);
            if (sms == null)
            {
                return HttpNotFound();
            }
            return View(sms);
        }

        // GET: Sms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sms/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Telephone_,Message,Date_envoie")] Sms sms)
        {
            if (ModelState.IsValid)
            {
                db.Smss.Add(sms);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(sms);
        }

        // GET: Sms/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sms sms = await db.Smss.FindAsync(id);
            if (sms == null)
            {
                return HttpNotFound();
            }
            return View(sms);
        }

        // POST: Sms/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Telephone_,Message,Date_envoie")] Sms sms)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sms).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(sms);
        }

        //Campagne

        public ActionResult Campagne()
        {
            return View();
        }

        [HttpPost]
        public ActionResult smscontacts(string message)
        {
            try
            {

                ServeurSms ConfActuelle = db.ServeurSmss.Where(x => x.EtatConfig.ToLower() == "actif").FirstOrDefault();
                Process myprocess = new Process();
                myprocess.StartInfo.FileName = @"C:\Program Files\Internet Explorer\iexplore.exe";
                myprocess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                var send = Request["telephone"].Split(',');
                for (int i = 0; i < send.Length; i++)
                {
                    int transferttel = Convert.ToInt32(send[i].Trim());
                    Contact EntrepriseWithId = new Contact();
                    EntrepriseWithId = db.Contacts.SingleOrDefault(x => x.Telephone == transferttel);
                    var idid = EntrepriseWithId.Id;
                    Sms DetailSMS = new Sms
                    {
                        Message = message,
                        Telephone_ = idid,
                        Date_envoie = DateTime.Now
                    };

                    //string username = "aiteconsulting";
                    //string password = "aitesolutionsarl";
                    //string senderId = "AITE"; "https://bulksms.vsms.net/eapi/submission/send_sms/2/2.0?username="
                    string SMSText = message;
                    string dest = "+237" + EntrepriseWithId.Telephone;
                    string url = ConfActuelle.ServerAdress + "username=" + ConfActuelle.Username + "&password=" + ConfActuelle.PasswordUser + "&sender=" + ConfActuelle.SenderId + "&message=" + SMSText + "&msisdn=" + dest;
                    System.Net.Sockets.TcpClient clnt = new System.Net.Sockets.TcpClient("www.google.com", ConfActuelle.Port);
                    clnt.Close();
                    myprocess.StartInfo.Arguments = url;
                    myprocess.Start();
                    db.Smss.Add(DetailSMS);
                    db.SaveChanges();
                    myprocess.WaitForExit(6000);

                }
                db.Dispose();
                return RedirectToAction("SuccesSms");
            }
            catch (Exception)
            {
                return RedirectToAction("ErreurSms");
            }
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
        public ActionResult CampagneContactsSms()
        {
            IEnumerable<Contact> categories = new List<Contact>();
            categories = db.Contacts.ToList();
            ViewBag.personnes = categories;
            return View();
        }

        // GET: Sms/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sms sms = await db.Smss.FindAsync(id);
            if (sms == null)
            {
                return HttpNotFound();
            }
            return View(sms);
        }

        // POST: Sms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Sms sms = await db.Smss.FindAsync(id);
            db.Smss.Remove(sms);
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
