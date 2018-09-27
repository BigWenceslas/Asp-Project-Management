using System.Collections.Generic;
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

namespace AITECRM.Controllers
{
    [Authorize(Roles = ("Admin,Operateur,Semi_Admin"))]
    public class TachesController : Controller
    {
        private AITECRMContext db = new AITECRMContext();

        // GET: Taches
        public  ActionResult Index()
        {
            IEnumerable<AllTachesViewModel> ListEmpty = new List<AllTachesViewModel>();
            ListEmpty = (from e in db.Contacts
                         join a in db.Entreprises on e.Entreprise_ equals a.Id
                         join b in db.ContactGroup on e.Contactgroups equals b.Id
                         join c in db.Taches on e.Id equals c.Contact_
                         join d in db.Projets on c.Projet_ equals d.Id
                         select new
                         {
                             nomE = a.Nom,
                             nomContact = e.Contact_name,
                             contactType = b.Type_name,
                             IntituleTache = c.Intitule,
                             dateDebutTache = c.Date_debut,
                             dateFinTache = c.Date_fin,
                             dateDebutProjet = d.Date_debut,
                             dateFinProjet = d.Date_fin,
                             type = c.Type_tache,
                             prix = c.Cout,
                             idtache = c.Id
                         })
                        .ToList()
                        .Select(x => new AllTachesViewModel()
                        {
                            NomEntreprise = x.nomE,
                            NomContact = x.nomContact,
                            GroupeContact = x.contactType,
                            Intitule = x.IntituleTache,
                            Date_debut = x.dateDebutTache,
                            Date_Fin = x.dateFinTache,
                            LancementProjet = x.dateDebutProjet,
                            ClotureProjet = x.dateFinProjet,
                            Type_tache = x.type,
                            Cout = x.prix,
                            IdTache = x.idtache
                        }).ToList();


            return View(ListEmpty);
        }

        //Exportation aux differents formats

        public ActionResult ExportPdf()
        {


            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Rapports/AllTasks.rpt")));
            var ListEmpty = (from e in db.Entreprises
                             join a in db.Contacts on e.Id equals a.Entreprise_
                             join b in db.ContactGroup on a.Contactgroups equals b.Id
                             join c in db.Taches on a.Id equals c.Contact_
                             join d in db.Projets on c.Projet_ equals d.Id
                             select new
                             {
                                 nomE = e.Nom,
                                 nomContact = a.Contact_name,
                                 contactType = b.Type_name,
                                 IntituleTache = c.Intitule,
                                 dateDebutTache = c.Date_debut,
                                 dateFinTache = c.Date_fin,
                                 dateDebutProjet = d.Date_debut,
                                 dateFinProjet = d.Date_fin,
                                 IdTache = c.Id
                             })
                       .ToList()
                       .Select(x => new AllTachesViewModel()
                       {
                           NomEntreprise = x.nomE,
                           NomContact = x.nomContact,
                           GroupeContact = x.contactType,
                           Intitule = x.IntituleTache,
                           Date_debut = x.dateDebutTache,
                           Date_Fin = x.dateFinTache,
                           LancementProjet = x.dateDebutProjet,
                           ClotureProjet = x.dateFinProjet,
                           IdTache = x.IdTache
                       });
            rd.SetDataSource(ListEmpty.Select(p => new
            {
                NomEntreprise = p.NomEntreprise,
                NomProjet = p.NomProjet,
                NomAcheteur = p.NomContact,
                TypeAcheteur = p.GroupeContact,
                Date_debut = p.Date_debut,
                Date_Fin = p.Date_Fin,
                LancementProjet = p.LancementProjet,
                ClotureProjet = p.ClotureProjet

            }).ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "ListContacts.pdf");
        }

        //Exporter au format Excel

        public ActionResult ExportExcel()
        {


            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Rapports/AllTasks.rpt")));
            var ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             join f in db.ContactGroup on b.Contactgroups equals f.Id
                             join d in db.Achats on b.Id equals d.Contact_
                             join e in db.Projets on d.Projet_ equals e.Id
                             select new
                             {
                                 nomE = a.Nom,
                                 Nomp = e.Titre,
                                 nomContact = b.Contact_name,
                                 contactType = f.Type_name,
                                 coutAchat = d.Cout,
                                 dateAchat = d.Date_Achat,
                                 IdVente = d.Id
                             })
                       .ToList()
                       .Select(x => new AllAchatsViewModel()
                       {
                           NomEntreprise = x.nomE,
                           NomProjet = x.Nomp,
                           NomAcheteur = x.nomContact,
                           TypeAcheteur = x.contactType,
                           Cout = x.coutAchat,
                           Date_achat = x.dateAchat,
                           IdAchat = x.IdVente
                       });
            rd.SetDataSource(ListEmpty.Select(p => new
            {
                NomEntreprise = p.NomEntreprise,
                NomProjet = p.NomProjet,
                NomAcheteur = p.NomAcheteur,
                TypeAcheteur = p.TypeAcheteur,
                Cout = p.Cout,
                Date_achat = p.Date_achat

            }).ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/xls", "ListContacts.xls");
        }

        //Exporter au format Word

        public ActionResult ExportWord()
        {


            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Rapports/AllTasks.rpt")));
            var ListEmpty = (from a in db.Entreprises
                             join b in db.Contacts on a.Id equals b.Entreprise_
                             join f in db.ContactGroup on b.Contactgroups equals f.Id
                             join d in db.Achats on b.Id equals d.Contact_
                             join e in db.Projets on d.Projet_ equals e.Id
                             select new
                             {
                                 nomE = a.Nom,
                                 Nomp = e.Titre,
                                 nomContact = b.Contact_name,
                                 contactType = f.Type_name,
                                 coutAchat = d.Cout,
                                 dateAchat = d.Date_Achat,
                                 IdVente = d.Id
                             })
                       .ToList()
                       .Select(x => new AllAchatsViewModel()
                       {
                           NomEntreprise = x.nomE,
                           NomProjet = x.Nomp,
                           NomAcheteur = x.nomContact,
                           TypeAcheteur = x.contactType,
                           Cout = x.coutAchat,
                           Date_achat = x.dateAchat,
                           IdAchat = x.IdVente
                       });
            rd.SetDataSource(ListEmpty.Select(p => new
            {
                NomEntreprise = p.NomEntreprise,
                NomProjet = p.NomProjet,
                NomAcheteur = p.NomAcheteur,
                TypeAcheteur = p.TypeAcheteur,
                Cout = p.Cout,
                Date_achat = p.Date_achat

            }).ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/doc", "ListContacts.doc");
        }

        // GET: Taches/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tache tache = await db.Taches.FindAsync(id);
            tache.Projet = await db.Projets.Where(e => e.Id == tache.Projet_).FirstOrDefaultAsync();
            if (tache == null)
            {
                return HttpNotFound();
            }
            return View(tache);
        }

        // GET: Taches/Create
        public ActionResult Create()
        {
            ViewBag.Projet_ = new SelectList(db.Projets, "Id", "Titre");
            ViewBag.Contact_ = new SelectList(db.Contacts, "Id", "Contact_name");
            return View();
        }

        // POST: Taches/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Contact_,Projet_,Intitule,Description,Type_tache,Cout,Date_debut,Date_fin")] Tache tache)
        {
            ViewBag.Projet_ = new SelectList(db.Projets, "Id", "Titre", tache.Projet_);
            ViewBag.Contact_ = new SelectList(db.Contacts, "Id", "Contact_name", tache.Contact_);
             if (ModelState.IsValid)
            {
                db.Taches.Add(tache);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tache);
        }

        // GET: Taches/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Projet_ = new SelectList(db.Projets, "Id", "Titre");
            ViewBag.Contact_ = new SelectList(db.Contacts, "Id", "Contact_name");
            Tache tache = await db.Taches.FindAsync(id);
            if (tache == null)
            {
                return HttpNotFound();
            }
            return View(tache);
        }

        // POST: Taches/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Contact_,Projet_,Intitule,Description,Type_tache,Cout,Date_debut,Date_fin")] Tache tache)
        {
            ViewBag.Projet_ = new SelectList(db.Projets, "Id", "Titre", tache.Projet_);
            ViewBag.Contact_ = new SelectList(db.Contacts, "Id", "Contact_name", tache.Contact_);
                        if (ModelState.IsValid)
            {
                db.Entry(tache).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tache);
        }

        // GET: Taches/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tache tache = await db.Taches.FindAsync(id);
            if (tache == null)
            {
                return HttpNotFound();
            }
            return View(tache);
        }

        // POST: Taches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Tache tache = await db.Taches.FindAsync(id);
            db.Taches.Remove(tache);
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
