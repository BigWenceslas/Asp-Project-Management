using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AITECRM.Models;
using System.Web.Hosting;
using System.Net.Mail;
using System.Net;

namespace AITECRM.Controllers
{
    public class BackTaskController : Controller
    {
        private AITECRMContext db = new AITECRMContext();
        private ApplicationDbContext test = new ApplicationDbContext();
        // GET: BackTask
        public ActionResult Index()
        {
            return View();
        }

        public string EmailTemplate(string template)
        {
            string body = System.IO.File.ReadAllText(HostingEnvironment
                .MapPath("~/Views/Home/") + template + ".cshtml");
            return body.ToString();
        }

        public async Task<ActionResult> SendEmail()
        {
            var ddate = DateTime.Now;var dydate = DateTime.Now.AddDays(-1);
            var nbrEntreprises = db.Entreprises.Where(x => x.Date_enregistrement > dydate &&  x.Date_enregistrement< ddate)
                .Count().ToString();
            var nbrContacts = db.Contacts.Where(x => x.Date_enregistrement > dydate && x.Date_enregistrement < ddate)
                .Count().ToString();
            var nbrActivites = db.Activites.Where(x => x.DacteCreation > dydate && x.DacteCreation < ddate)
                .Count().ToString();
            var nbrVentes = db.VenteArticles.Where(x => x.DateAchat > dydate && x.DateAchat < ddate)
                .Count().ToString();

            var message = EmailTemplate("Email");
            message = message.Replace("@ViewBag.Entreprises", nbrEntreprises);
            message = message.Replace("@ViewBag.Contacts", nbrContacts);
            message = message.Replace("@ViewBag.Activites", nbrActivites);
            message = message.Replace("@ViewBag.Ventes", nbrVentes);
           
            await SendEmailAsync(message);

            return Json(0, JsonRequestBehavior.AllowGet);
        }
        public async static Task SendEmailAsync( string message)
        {
            var adresses = BackTaskController.EmailConf();
            try {
             
                    var _Email = adresses.AdresseCRM;
                var _epass = adresses.PassWordCRM;
                var dispname = "Rapport journalier AITE CRM";
                MailMessage myMessage = new MailMessage();
                var sent = BackTaskController.EmailAdresses();
                foreach (var item in sent)
                {
                    myMessage.To.Add(item);

                }
                myMessage.From = new MailAddress(_Email, dispname);
                myMessage.Subject = dispname;
                myMessage.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.EnableSsl = true;
                    smtp.Host = adresses.ServeurAdresse;
                    smtp.Port = adresses.Port;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(_Email, _epass);
                    smtp.SendCompleted += (s, e) => { smtp.Dispose(); };
                    await smtp.SendMailAsync(myMessage);
                }
           
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public static List<string> EmailAdresses()
        {
            ApplicationDbContext data = new ApplicationDbContext();
        var ttt = data.Users.Select(a => a.Email).ToList();
            return (ttt);
        }
        public static EmailServeur EmailConf()
        {
            AITECRMContext data = new AITECRMContext();
        var ttt = data.EmailServeurs.Where(x => x.EtatConfig.ToLower() == "actif").FirstOrDefault();
            return (ttt);
        }

    }
}