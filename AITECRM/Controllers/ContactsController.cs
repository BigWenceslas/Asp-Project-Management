using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using AITECRM.Models;
using AITECRM.ViewModels;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using System.Collections;
using Microsoft.Office.Interop.Excel;
using System.Net.Mail;
using System.Diagnostics;

namespace AITECRM.Controllers
{
    [Authorize(Roles = ("Admin,Operateur,Semi_Admin"))]
    public class ContactsController : Controller
    {
        private AITECRMContext db = new AITECRMContext();

        // GET: Contacts
        public ActionResult Index()
        {
            var ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_

                             join d in db.ContactGroup on b.Contactgroups equals d.Id
                             select new
                             {
                                 nomE = a.Nom,
                                 nomContact = b.Contact_name,
                                 contactType = d.Type_name,
                                 Adressemail = b.Adresse_mail,
                                 tel = b.Telephone,
                                 dateEnregistrement = b.Date_enregistrement,
                                 dateNaissance = b.Date_naissance,
                                 Idcontact = b.Id
                             })
                       .Select(x => new AllContactViewModel()
                       {
                           NomEntreprise = x.nomE,
                           NomContact = x.nomContact,
                           NomGroupeContact = x.contactType,
                           AdresseMail = x.Adressemail,
                           Numero = x.tel,
                           DateEnregistrement = x.dateEnregistrement,
                           DateNaissance = x.dateNaissance,
                           Idc = x.Idcontact
                       }).ToList();

            return View(ListEmpty);
        }

        // GET: Contacts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = await db.Contacts.FindAsync(id);
            contact.Entreprise = await db.Entreprises.FindAsync(contact.Entreprise_);
            contact.Ventes = await db.VenteArticles.Where(x => x.Contact_ == id).ToListAsync();
            contact.Activites = await db.Activites.Where(x => x.Contact_ == id).ToListAsync();

            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        //Methode d'exportation des donnees

        public ActionResult ExportPdf()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Rapports/AllContacts.rpt")));
            var ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             join d in db.ContactGroup on b.Contactgroups equals d.Id
                             select new
                             {
                                 nomE = a.Nom,
                                 nomContact = b.Contact_name,
                                 contactType = d.Type_name,
                                 Adressemail = b.Adresse_mail,
                                 Numero = b.Telephone,
                                 dateEnregistrement = b.Date_enregistrement,
                                 Idcontact = b.Id
                             })
                        .ToList()
                        .Select(x => new AllContactViewModel()
                        {
                            NomEntreprise = x.nomE,
                            NomContact = x.nomContact,
                            NomGroupeContact = x.contactType,
                            AdresseMail = x.Adressemail,
                            Numero = x.Numero,
                            DateEnregistrement = x.dateEnregistrement,
                            Idc = x.Idcontact
                        });
            rd.SetDataSource(ListEmpty.Select(p => new {
                NomEntreprise = p.NomEntreprise,
                NomContact = p.NomContact,
                NomGroupeContact = p.NomGroupeContact,
                Numero = p.Numero,
                AdresseMail = p.AdresseMail,
                DateEnregistrement = p.DateEnregistrement
            }));
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "ListContacts.pdf");
        }

        public ActionResult ExportExcel()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Rapports/AllContacts.rpt")));
            var ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_

                             join d in db.ContactGroup on b.Contactgroups equals d.Id
                             select new
                             {
                                 nomE = a.Nom,
                                 nomContact = b.Contact_name,
                                 contactType = d.Type_name,
                                 Adressemail = b.Adresse_mail,
                                 Numero = b.Telephone,
                                 dateEnregistrement = b.Date_enregistrement,
                                 Idcontact = b.Id
                             })
                        .ToList()
                        .Select(x => new AllContactViewModel()
                        {
                            NomEntreprise = x.nomE,
                            NomContact = x.nomContact,
                            NomGroupeContact = x.contactType,
                            AdresseMail = x.Adressemail,
                            Numero = x.Numero,
                            DateEnregistrement = x.dateEnregistrement,
                            Idc = x.Idcontact
                        });
            rd.SetDataSource(ListEmpty.Select(p => new {
                NomEntreprise = p.NomEntreprise,
                NomContact = p.NomContact,
                NomGroupeContact = p.NomGroupeContact,
                Numero = p.Numero,
                AdresseMail = p.AdresseMail,
                DateEnregistrement = p.DateEnregistrement
            }));
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/xls", "ListContacts.xls");
        }

        public ActionResult ExportWord()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Rapports/AllContacts.rpt")));
            var ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_

                             join d in db.ContactGroup on b.Contactgroups equals d.Id
                             select new
                             {
                                 nomE = a.Nom,
                                 nomContact = b.Contact_name,
                                 contactType = d.Type_name,
                                 Adressemail = b.Adresse_mail,
                                 tel = b.Telephone,
                                 dateEnregistrement = b.Date_enregistrement,
                                 Idcontact = b.Id
                             })
                        .ToList()
                        .Select(x => new AllContactViewModel()
                        {
                            NomEntreprise = x.nomE,
                            NomContact = x.nomContact,
                            NomGroupeContact = x.contactType,
                            AdresseMail = x.Adressemail,
                            Numero = x.tel,
                            DateEnregistrement = x.dateEnregistrement,
                            Idc = x.Idcontact
                        });
            rd.SetDataSource(ListEmpty);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/doc", "ListContacts.doc");
        }


        // GET: Contacts/Create
        public ActionResult Create()
        {
            ViewBag.Contactgroups = new SelectList(db.ContactGroup, "Id", "Type_name");
            ViewBag.Entreprise_ = new SelectList(db.Entreprises, "Id", "Nom");
            return View();
        }

        public JsonResult VerifierNumero(int num)
        {
            System.Threading.Thread.Sleep(200);
            var search = db.Contacts.Where(x => x.Telephone == num).SingleOrDefault();
            if (search != null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }
        }


        // POST: Contacts/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Contact_name,Description,Adresse,Civilite,Pays,Code_postal,Adresse_mail,Telephone,Date_enregistrement,Date_naissance,Entreprise_,Contactgroups")] Contact contact)
        {
            ViewBag.Entreprise_ = new SelectList(db.Entreprises, "Id", "Nom", contact.Entreprise_);
            ViewBag.Contactgroups = new SelectList(db.ContactGroup, "Id", "Type_name", contact.Contactgroups);
            using (DAL dal = new DAL())
            {
                if (!dal.NumContactExiste(contact.Telephone))
                {
                    ModelState.AddModelError("Telephone", "Ce numero de telephone est deja utilisee");
                    return View(contact);
                }
                if (!dal.AdresseContactExiste(contact.Adresse_mail))
                {
                    ModelState.AddModelError("Adresse_mail", "Cette adreesse Email est deja utilisee");
                    return View(contact);
                }
                if (ModelState.IsValid)
                {
                    db.Contacts.Add(contact);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }

            return View(contact);
        }

        // GET: Contacts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Contactgroups = new SelectList(db.ContactGroup, "Id", "Type_name");
            ViewBag.Entreprise_ = new SelectList(db.Entreprises, "Id", "Nom");
            Contact contact = await db.Contacts.FindAsync(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Contact_name,Description,Adresse,Civilite,Pays,Code_postal,Adresse_mail,Telephone,Date_enregistrement,Date_naissance,Entreprise_,Contactgroups")] Contact contact)
        {
            ViewBag.Entreprise_ = new SelectList(db.Entreprises, "Id", "Nom", contact.Entreprise_);
            ViewBag.Contactgroups = new SelectList(db.ContactGroup, "Id", "Type_name", contact.Contactgroups);
            using (DAL dal = new DAL())
            {
                if (!dal.NumContactEditExiste(contact.Telephone))
                {
                    ModelState.AddModelError("Telephone", "Ce numero de telephone est deja utilisee");
                    return View(contact);
                }
                if (!dal.AdresseContactEditExiste(contact.Adresse_mail))
                {
                    ModelState.AddModelError("Adresse_mail", "Cette adresse Email est deja utilisee");
                    return View(contact);
                }
                if (ModelState.IsValid)
                {
                    db.Entry(contact).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
          
            return View(contact);
        }

        public ActionResult Importer()
        {
            return View("Importer");
        }

        public static Entreprise VerifierEntreprise(string name)
        {
            AITECRMContext DBV = new AITECRMContext();
            Entreprise verifE = new Entreprise();
            Entreprise ImportEntreprise = new Entreprise();
            verifE = DBV.Entreprises.Where(o => o.Nom.ToLower() == name.ToLower()).FirstOrDefault();
            if (verifE == null)
            {
                ImportEntreprise.Nom = "importer";
                ImportEntreprise.Date_enregistrement = DateTime.Now;
                ImportEntreprise.Adresse = "importer";
                ImportEntreprise.Adresse_Email = "importer@gmail.com";
                ImportEntreprise.Telephone = 699999999;
                ImportEntreprise.Code_postal = "importer";
                ImportEntreprise.Description = "importer";
                ImportEntreprise.Pays = "importer";
                ImportEntreprise.Site_web = "www.importer.com";
            }
            DBV.Dispose();
            return (ImportEntreprise);

        }

        public static ContactGroup VerifierContact(string name)
        {
            AITECRMContext DBV = new AITECRMContext();
            ContactGroup verifG = new ContactGroup();
            ContactGroup ImportGroups = new ContactGroup();
            verifG = DBV.ContactGroup.Where(o => o.Type_name.ToLower() == name.ToLower()).FirstOrDefault();
            if (verifG == null)
            {
                ImportGroups.Type_name = "importer";
                ImportGroups.Description = "importer";
            }
            return (ImportGroups);
        }


        [HttpPost]
        public ActionResult Importer_()
        {
            Directory.CreateDirectory(Server.MapPath("~/App_Data/Upload/"));
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
                            // MessageBox.Show(worksheet.Name);
                            var usedRange = worksheet.UsedRange;
                            int startRow = usedRange.Row + 1;
                            int endRow = startRow + usedRange.Rows.Count - 1;
                            int startColumn = usedRange.Column;
                            int endColumn = startColumn + usedRange.Columns.Count - 1;
                            string nomContact = null;
                            string adresse = null;
                            string pays = null;
                            string codepostal = null;
                            double Numtelephone = 0;
                            string adressemail = null;
                            string description = null;
                            string civilite = null;

                            string name = "importer";
                            int EntrepriseG; int groups;
                            Entreprise verifE = new Entreprise();
                            verifE = db.Entreprises.Where(o => o.Nom.ToLower() == name.ToLower()).FirstOrDefault();

                            ContactGroup verifG = new ContactGroup();
                            verifG = db.ContactGroup.Where(o => o.Type_name.ToLower() == name.ToLower()).FirstOrDefault();
                            if (VerifierEntreprise(name) == null && VerifierContact(name) == null)
                            {
                                Entreprise ImportEntreprise = VerifierEntreprise(name);
                                ContactGroup ImportGroup = VerifierContact(name);

                                EntrepriseG = db.Entreprises.Where(o => o.Nom == name).FirstOrDefault().Id;
                                groups = db.ContactGroup.Where(o => o.Type_name == name).FirstOrDefault().Id;

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
                                                nomContact = (string)(worksheet.Cells[y, cdebut + 1]).Value;
                                                nomContact = nomContact.Trim();
                                                pays = (string)(worksheet.Cells[y, cdebut + 2]).Value;
                                                pays = pays.Trim();
                                                adresse = (string)(worksheet.Cells[y, cdebut + 3]).Value;
                                                adresse = adresse.Trim();
                                                codepostal = (string)(worksheet.Cells[y, cdebut + 4]).Value;
                                                codepostal = codepostal.Trim();
                                                adressemail = (string)(worksheet.Cells[y, cdebut + 6]).Value;
                                                adressemail = adressemail.Trim();
                                                description = (string)(worksheet.Cells[y, cdebut + 7]).Value;
                                                description = description.Trim();
                                                Numtelephone = (worksheet.Cells[y, cdebut + 5]).Value;
                                                civilite = (string)(worksheet.Cells[y, cdebut + 8]).Value;
                                                civilite = description.Trim();



                                                Contact ImportE = new Contact();

                                                ImportE.Contact_name = nomContact;
                                                ImportE.Adresse = adresse;
                                                ImportE.Code_postal = codepostal;
                                                ImportE.Pays = pays;
                                                ImportE.Telephone = Convert.ToInt32(Numtelephone);
                                                ImportE.Adresse_mail = adressemail;
                                                ImportE.Description = description;
                                                ImportE.Date_enregistrement = DateTime.Now;
                                                ImportE.Date_naissance = DateTime.Now;
                                                ImportE.Entreprise_ = EntrepriseG;
                                                ImportE.Contactgroups = groups;
                                                db.Contacts.Add(ImportE); db.SaveChanges();



                                            }
                                            ArrayList l = new ArrayList();
                                            l = list;
                                            break;

                                        }

                                    }

                                }
                                if (System.IO.File.Exists(path))
                                { System.IO.File.Delete(path); }
                            }
                            else if (verifE == null && verifG != null)
                            {
                                Entreprise ImportEntreprise = VerifierEntreprise(name); ;
                                db.Entreprises.Add(ImportEntreprise);
                                db.SaveChanges();
                                groups = verifG.Id;
                                EntrepriseG = db.Entreprises.Where(o => o.Nom == name).FirstOrDefault().Id;

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
                                                nomContact = (string)(worksheet.Cells[y, cdebut + 1]).Value;
                                                nomContact = nomContact.Trim();
                                                pays = (string)(worksheet.Cells[y, cdebut + 2]).Value;
                                                pays = pays.Trim();
                                                adresse = (string)(worksheet.Cells[y, cdebut + 3]).Value;
                                                adresse = adresse.Trim();
                                                codepostal = (string)(worksheet.Cells[y, cdebut + 4]).Value;
                                                codepostal = codepostal.Trim();
                                                adressemail = (string)(worksheet.Cells[y, cdebut + 6]).Value;
                                                adressemail = adressemail.Trim();
                                                description = (string)(worksheet.Cells[y, cdebut + 7]).Value;
                                                description = description.Trim();
                                                Numtelephone = (worksheet.Cells[y, cdebut + 5]).Value;
                                                civilite = (string)(worksheet.Cells[y, cdebut + 8]).Value;
                                                civilite = description.Trim();



                                                Contact ImportE = new Contact();

                                                ImportE.Contact_name = nomContact;
                                                ImportE.Adresse = adresse;
                                                ImportE.Code_postal = codepostal;
                                                ImportE.Pays = pays;
                                                ImportE.Telephone = Convert.ToInt32(Numtelephone);
                                                ImportE.Adresse_mail = adressemail;
                                                ImportE.Description = description;
                                                ImportE.Date_enregistrement = DateTime.Now;
                                                ImportE.Date_naissance = DateTime.Now;
                                                ImportE.Entreprise_ = EntrepriseG;
                                                ImportE.Contactgroups = groups;

                                                db.Contacts.Add(ImportE); db.SaveChanges();



                                            }
                                            ArrayList l = new ArrayList();
                                            l = list;
                                            break;

                                        }

                                    }

                                }
                                if (System.IO.File.Exists(path))
                                { System.IO.File.Delete(path); }
                            }
                            if (verifE != null && verifG == null)
                            {
                                ContactGroup ImportGroup = VerifierContact(name);
                                db.ContactGroup.Add(ImportGroup);
                                db.SaveChanges();

                                EntrepriseG = verifE.Id;
                                groups = db.ContactGroup.Where(o => o.Type_name == name).FirstOrDefault().Id;

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
                                                nomContact = (string)(worksheet.Cells[y, cdebut + 1]).Value;
                                                nomContact = nomContact.Trim();
                                                pays = (string)(worksheet.Cells[y, cdebut + 2]).Value;
                                                pays = pays.Trim();
                                                adresse = (string)(worksheet.Cells[y, cdebut + 3]).Value;
                                                adresse = adresse.Trim();
                                                codepostal = (string)(worksheet.Cells[y, cdebut + 4]).Value;
                                                codepostal = codepostal.Trim();
                                                adressemail = (string)(worksheet.Cells[y, cdebut + 6]).Value;
                                                adressemail = adressemail.Trim();
                                                description = (string)(worksheet.Cells[y, cdebut + 7]).Value;
                                                description = description.Trim();
                                                Numtelephone = (worksheet.Cells[y, cdebut + 5]).Value;
                                                civilite = (string)(worksheet.Cells[y, cdebut + 8]).Value;
                                                civilite = description.Trim();



                                                Contact ImportE = new Contact();

                                                ImportE.Contact_name = nomContact;
                                                ImportE.Adresse = adresse;
                                                ImportE.Code_postal = codepostal;
                                                ImportE.Pays = pays;
                                                ImportE.Telephone = Convert.ToInt32(Numtelephone);
                                                ImportE.Adresse_mail = adressemail;
                                                ImportE.Description = description;
                                                ImportE.Date_enregistrement = DateTime.Now;
                                                ImportE.Date_naissance = DateTime.Now;
                                                ImportE.Entreprise_ = EntrepriseG;
                                                ImportE.Contactgroups = groups;
                                                db.Contacts.Add(ImportE); db.SaveChanges();



                                            }
                                            ArrayList l = new ArrayList();
                                            l = list;
                                            break;

                                        }

                                    }

                                }
                                if (System.IO.File.Exists(path))
                                { System.IO.File.Delete(path); }
                            }
                            if (verifE != null && verifG != null)
                            {
                                groups = verifG.Id; EntrepriseG = verifE.Id;

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
                                                nomContact = (string)(worksheet.Cells[y, cdebut + 1]).Value;
                                                nomContact = nomContact.Trim();
                                                pays = (string)(worksheet.Cells[y, cdebut + 2]).Value;
                                                pays = pays.Trim();
                                                adresse = (string)(worksheet.Cells[y, cdebut + 3]).Value;
                                                adresse = adresse.Trim();
                                                codepostal = (string)(worksheet.Cells[y, cdebut + 4]).Value;
                                                codepostal = codepostal.Trim();
                                                adressemail = (string)(worksheet.Cells[y, cdebut + 6]).Value;
                                                adressemail = adressemail.Trim();
                                                description = (string)(worksheet.Cells[y, cdebut + 7]).Value;
                                                description = description.Trim();
                                                Numtelephone = (worksheet.Cells[y, cdebut + 5]).Value;
                                                civilite = (string)(worksheet.Cells[y, cdebut + 8]).Value;
                                                civilite = description.Trim();



                                                Contact ImportE = new Contact();

                                                ImportE.Contact_name = nomContact;
                                                ImportE.Adresse = adresse;
                                                ImportE.Code_postal = codepostal;
                                                ImportE.Pays = pays;
                                                ImportE.Telephone = Convert.ToInt32(Numtelephone);
                                                ImportE.Adresse_mail = adressemail;
                                                ImportE.Description = description;
                                                ImportE.Date_enregistrement = DateTime.Now;
                                                ImportE.Date_naissance = DateTime.Now;
                                                ImportE.Entreprise_ = EntrepriseG;
                                                ImportE.Contactgroups = groups;
                                                db.Contacts.Add(ImportE); db.SaveChanges();



                                            }
                                            ArrayList l = new ArrayList();
                                            l = list;
                                            break;

                                        }

                                    }

                                }
                                if (System.IO.File.Exists(path))
                                { System.IO.File.Delete(path); }
                            }



                        }

                    }
                    catch (Exception)
                    { }
                    finally { Redirect("Index"); }
                }
            }

            return Redirect("Index");
        }

        // GET: Contacts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = await db.Contacts.FindAsync(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        [HttpPost]
        public async Task<ActionResult> SendEmailsDetails(string message, string objet, string email, int IdContact)
        {
            Directory.CreateDirectory(Server.MapPath("~/App_Data/email/"));
            try
            {
                EmailServeur ConfigActive = await db.EmailServeurs.Where(x => x.EtatConfig.ToLower() == "actif").FirstOrDefaultAsync();
                Attachment[] listefichier = new Attachment[1];
                Email DetailEmail = new Email();
                DetailEmail.Message = message;
                DetailEmail.Addresse_ = IdContact;

                MailMessage mail = new MailMessage(ConfigActive.AdresseCRM, email, objet, message);
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

                SmtpClient client = new SmtpClient(ConfigActive.ServeurAdresse, ConfigActive.Port);
                client.UseDefaultCredentials = false;
                NetworkCredential cred = new NetworkCredential(ConfigActive.AdresseCRM, ConfigActive.PassWordCRM);
                client.Credentials = cred;
                client.EnableSsl = true;
                client.Send(mail);
                Email Envoye = new Email
                {
                    Message = message,
                    Addresse_ = IdContact,
                    Message_object = objet,
                    Date_envoie = DateTime.Now


                };
                db.Emails.Add(Envoye);
                db.SaveChanges();
                db.Dispose();

                return RedirectToAction("SuccesEmails");
            }
            catch (Exception)
            {
                return RedirectToAction("ErrorEmail");
            }

        }
        [HttpPost]
        public async Task<ActionResult> SendSmsDetails(string message, int numero)
        {
            try
            {
                ServeurSms ConfigActiveSms = await db.ServeurSmss.Where(x => x.EtatConfig.ToLower() == "actif").FirstOrDefaultAsync();
                Process myprocess = new Process();
                myprocess.StartInfo.FileName = @"C:\Program Files\Internet Explorer\iexplore.exe";
                myprocess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                Sms DetailSms = new Sms();
                //string username = "aiteconsulting";
                //string password = "aitesolutionsarl";
                //string senderId = "AITE";"https://bulksms.vsms.net/eapi/submission/send_sms/2/2.0?username="
                string SMSText = message;
                string dest = "+237" + numero;
                string url = ConfigActiveSms.ServerAdress + "username=" + ConfigActiveSms.Username + "&password=" + ConfigActiveSms.PasswordUser + "&sender=" + ConfigActiveSms.SenderId + "&message=" + SMSText + "&msisdn=" + dest;
                System.Net.Sockets.TcpClient clnt = new System.Net.Sockets.TcpClient("www.google.com", 80);
                clnt.Close();
                myprocess.StartInfo.Arguments = url;
                myprocess.Start();
                myprocess.WaitForExit(6000);
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
        [HttpPost]
        [Authorize(Roles = "Admin,Semi_Admin")]
        public ActionResult Newdelete(int id)
        {
            try
            {
                Contact contact = db.Contacts.Find(id);
                db.Contacts.Remove(contact);
                db.SaveChanges();
                return Json(new { Suppression = "Ok" });
            }
            catch (Exception)
            {

                throw;
            }


        }


        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Contact contact = await db.Contacts.FindAsync(id);
            db.Contacts.Remove(contact);
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
