using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using AITECRM.Models;
using System.IO;
using System.Collections;
using Microsoft.Office.Interop.Excel;
using CrystalDecisions.CrystalReports.Engine;
using System.Net.Mail;
using System.Diagnostics;

namespace AITECRM.Controllers
{
    [Authorize(Roles = ("Admin,Operateur,Semi_Admin"))]
    public class EntreprisesController : Controller
    {
        private AITECRMContext db = new AITECRMContext();

        // GET: Entreprises
        public async Task<ActionResult> Index()
        {
            return View(await db.Entreprises.Include(e=>e.Contact).ToListAsync());
        }

        public ActionResult Importer()
        {
            return View("Importer");
        }

        [HttpPost]
        public ActionResult Importer_()
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    string newfile = Convert.ToString(DateTime.Now.Minute) + Convert.ToString(DateTime.Now.Hour) +
                                             Convert.ToString(DateTime.Now.Day) + Convert.ToString(DateTime.Now.Month) +
                                             Convert.ToString(DateTime.Now.Year);
                    var path = Path.Combine(Server.MapPath("~/App_Data/Upload/"), newfile + file.FileName);
                    Directory.CreateDirectory(Server.MapPath("~/App_Data/Upload/"));
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                    file.SaveAs(path);
                    try
                    {


                        ArrayList list = new ArrayList();
                        _Application fichier = new Application();
                        Workbook wbf;
                        wbf = fichier.Workbooks.Open(path);
                        list.Clear();
                        foreach (Worksheet worksheet in wbf.Worksheets)
                        {

                            int cdebut = 0;
                            int ldebut = 0;

                            var usedRange = worksheet.UsedRange;
                            int startRow = usedRange.Row + 1;
                            int endRow = startRow + usedRange.Rows.Count - 1;
                            int startColumn = usedRange.Column;
                            int endColumn = startColumn + usedRange.Columns.Count - 1;
                            string nomEntreprise = null;
                            string adresse = null;
                            string pays = null;
                            string codepostal = null;
                            string siteweb = null;
                            double Numtelephone = 0;
                            string adressemail = null;
                            string description = null;


                            for (int k = startRow; k <= endRow; k++)
                            {
                                for (int p = startColumn; p <= endColumn; p++)
                                {


                                    var cellValue = (worksheet.Cells[k, p]).Value;

                                    if (cellValue == 1)
                                    {

                                        cdebut = p;
                                        ldebut = k;

                                        for (int y = ldebut; y <= endRow; y++)
                                        {
                                            nomEntreprise = (string)(worksheet.Cells[y, cdebut + 1]).Value;
                                            nomEntreprise = nomEntreprise.Trim();
                                            pays = (string)(worksheet.Cells[y, cdebut + 2]).Value;
                                            pays = pays.Trim();
                                            adresse = (string)(worksheet.Cells[y, cdebut + 3]).Value;
                                            adresse = adresse.Trim();
                                            codepostal = (string)(worksheet.Cells[y, cdebut + 4]).Value;
                                            codepostal = codepostal.Trim();
                                            siteweb = (string)(worksheet.Cells[y, cdebut + 5]).Value;
                                            siteweb = siteweb.Trim();
                                            adressemail = (string)(worksheet.Cells[y, cdebut + 7]).Value;
                                            adressemail = adressemail.Trim();
                                            description = (string)(worksheet.Cells[y, cdebut + 8]).Value;
                                            description = description.Trim();
                                            Numtelephone = (worksheet.Cells[y, cdebut + 6]).Value;

                                            Entreprise ImportE = new Entreprise();

                                            ImportE.Nom = nomEntreprise;
                                            ImportE.Adresse = adresse;
                                            ImportE.Code_postal = codepostal;
                                            ImportE.Pays = pays;
                                            ImportE.Telephone = Convert.ToInt32(Numtelephone);
                                            ImportE.Adresse_Email = adressemail;
                                            ImportE.Description = description;
                                            ImportE.Date_enregistrement = DateTime.Now;
                                            ImportE.Site_web = siteweb;

                                            db.Entreprises.Add(ImportE); db.SaveChanges();



                                        }
                                        ArrayList l = new ArrayList();
                                        l = list;
                                        break;

                                    }

                                }

                            }

                        }
                        if (System.IO.File.Exists(path))
                        { System.IO.File.Delete(path); }
                    }
                    catch (Exception)
                    { }
                    finally { Redirect("Index"); }
                }
            }

            return Redirect("Index");
        }

        //Methode d'exportation des donnees

        public ActionResult ExportPdf()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Rapports/EntrepriseReport.rpt")));
            rd.SetDataSource(db.Entreprises.Select(p => new
            {
                Nom = p.Nom,
                Site_web = p.Site_web,
                Pays = p.Pays,
                Adresse = p.Adresse,
                Adresse_Email = p.Adresse_Email,
                Telephone = p.Telephone
            }).ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "ListEntreprise.pdf");
        }

        public ActionResult ExportExcel()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Rapports/EntrepriseReport.rpt")));
            rd.SetDataSource(db.Entreprises.Select(p => new
            {
                Nom = p.Nom,
                Site_web = p.Site_web,
                Pays = p.Pays,
                Adresse = p.Adresse,
                Adresse_Email = p.Adresse_Email,
                Telephone = p.Telephone
            }).ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/xls", "ListEntreprise.xls");
        }

        public ActionResult ExportWord()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Rapports/EntrepriseReport.rpt")));
            rd.SetDataSource(db.Entreprises.Select(p => new
            {
                Nom = p.Nom,
                Site_web = p.Site_web,
                Pays = p.Pays,
                Adresse = p.Adresse,
                Adresse_Email = p.Adresse_Email,
                Telephone = p.Telephone
            }).ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/doc", "ListEntreprise.doc");
        }
    

        // GET: Entreprises/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entreprise entreprise = await db.Entreprises.FindAsync(id);
            entreprise.Contact = await db.Contacts.Where(x => x.Entreprise_ == id).ToListAsync();
            if (entreprise == null)
            {
                return HttpNotFound();
            }
            return View(entreprise);
        }

        [HttpPost]
        public async Task<ActionResult> SendEmailsDetails(string message, string objet, string email, int IdContact)
        {
            try
            {
                EmailServeur ConfActuelle = await db.EmailServeurs.Where(x => x.EtatConfig.ToLower() == "actif").FirstOrDefaultAsync();
                Attachment[] listefichier = new Attachment[1];
                Email DetailEmail = new Email();
                DetailEmail.Message = message;
                DetailEmail.Addresse_ = IdContact;
            
                MailMessage mail = new MailMessage(ConfActuelle.AdresseCRM, email, objet, message);
                mail.IsBodyHtml = true;
                Directory.CreateDirectory(Server.MapPath("~/App_Data/email/"));
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

                SmtpClient client = new SmtpClient(ConfActuelle.ServeurAdresse, ConfActuelle.Port);
                client.UseDefaultCredentials = false;
                NetworkCredential cred = new NetworkCredential(ConfActuelle.AdresseCRM, ConfActuelle.PassWordCRM);
                client.Credentials = cred;
                client.EnableSsl = true;
                client.Send(mail);

                //EmailEntreprise Envoye = new EmailEntreprise
                //{
                //    Message = message,
                //    Addresse_ = IdContact,
                //    Message_object = objet,
                //    Date_envoie = DateTime.Now


                //};
                //db.EmailEntreprises.Add(Envoye); db.SaveChanges(); db.Dispose();
                return RedirectToAction("SuccesEmails");
        }
            catch (Exception)
            {
                return RedirectToAction("ErrorEmail");
    }

}

        [HttpPost]
        public async Task<ActionResult> SendSmsDetails(string message, int numero, string pays, int id)
        {
            try
            {
                Process myprocess = new Process();
                myprocess.StartInfo.FileName = @"C:\Program Files\Internet Explorer\iexplore.exe";
                myprocess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                string Indicatif = "";
                if (pays.Trim() == "Cameroun") { Indicatif = "237"; }
                else if (pays.Trim() == "Afrique_du_Sud") { Indicatif = "27"; }
                else if (pays.Trim() == "Algerie") { Indicatif = "213"; }
                else if (pays.Trim() == "Angola") { Indicatif = "244"; }
                else if (pays.Trim() == "Benin") { Indicatif = "229"; }
                else if (pays.Trim() == "Botswana") { Indicatif = "267"; }
                else if (pays.Trim() == "Burkina_Faso") { Indicatif = "226"; }
                else if (pays.Trim() == "Burundi") { Indicatif = "257"; }
                else if (pays.Trim() == "Cap_Vert") { Indicatif = "238"; }
                else if (pays.Trim() == "Comores") { Indicatif = "269"; }
                else if (pays.Trim() == "Côte_d_Ivoire") { Indicatif = "225"; }
                else if (pays.Trim() == "Djibouti") { Indicatif = "253"; }
                else if (pays.Trim() == "Egypte") { Indicatif = "20"; }
                else if (pays.Trim() == "Ethiopie") { Indicatif = "251"; }
                else if (pays.Trim() == "Gabon") { Indicatif = "241"; }
                else if (pays.Trim() == "Gambie") { Indicatif = "220"; }
                else if (pays.Trim() == "Ghana") { Indicatif = "233"; }
                else if (pays.Trim() == "Guinee") { Indicatif = "224"; }
                else if (pays.Trim() == "Guinee_Bissau") { Indicatif = "245"; }
                else if (pays.Trim() == "Guinee_Equatoriale") { Indicatif = "240"; }
                else if (pays.Trim() == "Kenya") { Indicatif = "254"; }
                else if (pays.Trim() == "Lesotho") { Indicatif = "266"; }
                else if (pays.Trim() == "Lybie") { Indicatif = "218"; }
                else if (pays.Trim() == "Madagascar") { Indicatif = "261"; }
                else if (pays.Trim() == "Malawi") { Indicatif = "265"; }
                else if (pays.Trim() == "Maroc") { Indicatif = "212"; }
                else if (pays.Trim() == "Mali") { Indicatif = ""; }
                else if (pays.Trim() == "Maurice") { Indicatif = "230"; }
                else if (pays.Trim() == "Mauritanie") { Indicatif = "222"; }
                else if (pays.Trim() == "Mozambique") { Indicatif = "258"; }
                else if (pays.Trim() == "Namibie") { Indicatif = "264"; }
                else if (pays.Trim() == "Niger") { Indicatif = "227"; }
                else if (pays.Trim() == "Nigeria") { Indicatif = "234"; }
                else if (pays.Trim() == "Ouganda") { Indicatif = "256"; }
                else if (pays.Trim() == "Congo") { Indicatif = "242"; }
                else if (pays.Trim() == "Congo_democratique") { Indicatif = "243"; }
                else if (pays.Trim() == "Rwanda") { Indicatif = "250"; }
                else if (pays.Trim() == "Sao_Tome_et_Principe") { Indicatif = "239"; }
                else if (pays.Trim() == "Senegal") { Indicatif = "221"; }
                else if (pays.Trim() == "Seychelles") { Indicatif = "248"; }
                else if (pays.Trim() == "Sierra_Leone") { Indicatif = "232"; }
                else if (pays.Trim() == "Somalie") { Indicatif = "252"; }
                else if (pays.Trim() == "Soudan") { Indicatif = "249"; }
                else if (pays.Trim() == "Swaziland") { Indicatif = "268"; }
                else if (pays.Trim() == "Tanzanie") { Indicatif = "255"; }
                else if (pays.Trim() == "Tchad") { Indicatif = "235"; }
                else if (pays.Trim() == "Togo") { Indicatif = "228"; }
                else if (pays.Trim() == "Tunisie") { Indicatif = "216"; }
                else if (pays.Trim() == "Zimbabwe") { Indicatif = "263"; }


                Sms DetailSms = new Sms();
                ServeurSms ConfActuelle = await db.ServeurSmss.Where(x => x.EtatConfig.ToLower() == "actif").FirstOrDefaultAsync();
                //string username = "aiteconsulting";
                //string password = "aitesolutionsarl";
                //string senderId = "AITE";"https://bulksms.vsms.net/eapi/submission/send_sms/2/2.0?username="
                string SMSText = message;
                string dest = Indicatif + numero;
                string url = ConfActuelle.ServerAdress + "username=" + ConfActuelle.Username + "&password=" + ConfActuelle.PasswordUser + "&sender=" + ConfActuelle.SenderId + "&message=" + SMSText + "&msisdn=" + dest;
                System.Net.Sockets.TcpClient clnt = new System.Net.Sockets.TcpClient("www.google.com", 80);
                clnt.Close();
                myprocess.StartInfo.Arguments = url;
                myprocess.Start();
                myprocess.WaitForExit(6000);
                SmsEntreprise Envoye = new SmsEntreprise
                {
                    Message = message,
                    Date_envoie = DateTime.Now,
                    Telephone_ = id
                };
                db.SmsEntreprises.Add(Envoye); db.SaveChanges(); db.Dispose();
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

        // GET: Entreprises/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Entreprises/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nom,Adresse,Pays,Code_postal,Site_web,Adresse_Email,Telephone,Description,Date_enregistrement")] Entreprise entreprise)
        {
            using (DAL dal = new DAL())
            {
                if (!dal.NumEntrepriseExiste(entreprise.Telephone))
                {
                    ModelState.AddModelError("Telephone", "Ce numero de telephone est deja utilisee");
                    return View(entreprise);
                }
                if (!dal.AdresseEntrepriseExiste(entreprise.Adresse_Email))
                {
                    ModelState.AddModelError("Adresse_Email", "Cette adreesse Email est deja utilisee");
                    return View(entreprise);
                }
                if (ModelState.IsValid)
                {
                    db.Entreprises.Add(entreprise);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
               
            return View(entreprise);
        }

        // GET: Entreprises/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entreprise entreprise = await db.Entreprises.FindAsync(id);
            if (entreprise == null)
            {
                return HttpNotFound();
            }
            return View(entreprise);
        }

        // POST: Entreprises/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nom,Adresse,Pays,Code_postal,Site_web,Adresse_Email,Telephone,Description,Date_enregistrement")] Entreprise entreprise)
        {
            using (DAL dal = new DAL())
            {
                if (!dal.NumEntrepriseEditExiste(entreprise.Telephone))
                {
                    ModelState.AddModelError("Telephone", "Ce numero de telephone est deja utilisee");
                    return View(entreprise);
                }
                if (!dal.AdresseEntrepriseEditExiste(entreprise.Adresse_Email))
                {
                    ModelState.AddModelError("Adresse_Email", "Cette adresse Email est deja utilisee");
                    return View(entreprise);
                }
                if (ModelState.IsValid)
                {
                    db.Entry(entreprise).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
          
            return View(entreprise);
        }

        // GET: Entreprises/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entreprise entreprise = await db.Entreprises.FindAsync(id);
            if (entreprise == null)
            {
                return HttpNotFound();
            }
            return View(entreprise);
        }

        // POST: Entreprises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Entreprise entreprise = await db.Entreprises.FindAsync(id);
            db.Entreprises.Remove(entreprise);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Semi_Admin")]
        public ActionResult Newdelete(int id)
        {
            try
            {
                Entreprise entreprise = db.Entreprises.Find(id);
                db.Entreprises.Remove(entreprise);
                db.SaveChanges();
                return Json(new { Suppression = "Ok" });
            }
            catch (Exception)
            {
                throw;
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
