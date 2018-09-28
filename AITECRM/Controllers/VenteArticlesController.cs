using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using AITECRM.Models;
using AITECRM.ViewModels;

namespace AITECRM.Controllers
{
    [Authorize(Roles = ("Admin,Operateur,Semi_Admin"))]
    public class VenteArticlesController : Controller
    {
        private AITECRMContext db = new AITECRMContext();

        // GET: VenteArticles
        public ActionResult Index()
        {
            IEnumerable<VenteProduitsViewModel> ListEmpty = new List<VenteProduitsViewModel>();
            ListEmpty = (from a in db.VenteArticles
                         join b in db.Contacts on a.Contact_ equals b.Id
                         join c in db.Entreprises on b.Entreprise_ equals c.Id
                         join d in db.Articles on a.Article_ equals d.Idp
                         join e in db.CategorieProduits on d.CategorieProduit_ equals e.Id
                         select new
                         {
                             Id = a.Id,
                             nomEntreprise = c.Nom,
                             nomClient = b.Contact_name,
                             Categorie = e.Nom,
                             Produit = d.Nom,
                             ProduitDescription = d.Descriptionp,
                             dateEnregistrement = a.DateAchat,
                             prix = d.Prix
                         })
                       .ToList()
                       .Select(x => new VenteProduitsViewModel()
                       {
                           NomEntreprise = x.nomEntreprise,
                           NomClient = x.nomClient,
                           NomProduit = x.Produit,
                           NomCategorie = x.Categorie,
                           VenteId = x.Id,
                           DateVente = x.dateEnregistrement,
                           DescriptionProduit = x.ProduitDescription,
                           Prix = x.prix
                       });
            return View(ListEmpty);
        }

        // GET: VenteArticles/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VenteArticle venteArticle = await db.VenteArticles.FindAsync(id);
            if (venteArticle == null)
            {
                return HttpNotFound();
            }
            return View(venteArticle);
        }

        // GET: VenteArticles/Create
        public ActionResult Create()
        {
            ViewBag.Contact_ = new SelectList(db.Contacts, "Id", "Contact_name");
            ViewBag.Article_ = new SelectList(db.Articles, "Idp", "Nom");
            return View();
        }

        // POST: VenteArticles/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Contact_,Article_,DateAchat")] VenteArticle venteArticle)
        {
            ViewBag.Contact_ = new SelectList(db.Contacts, "Id", "Contact_name", venteArticle.Contact_);
            ViewBag.Article_ = new SelectList(db.Articles, "Idp", "Nom", venteArticle.Article_);
              if (ModelState.IsValid)
            {
                db.VenteArticles.Add(venteArticle);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(venteArticle);
        }

        // GET: VenteArticles/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Contact_ = new SelectList(db.Contacts, "Id", "Contact_name");
            ViewBag.Article_ = new SelectList(db.Articles, "Idp", "Nom");
            VenteArticle venteArticle = await db.VenteArticles.FindAsync(id);
            if (venteArticle == null)
            {
                return HttpNotFound();
            }
            return View(venteArticle);
        }

        // POST: VenteArticles/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Contact_,Article_,DateAchat")] VenteArticle venteArticle)
        {
            ViewBag.Contact_ = new SelectList(db.Contacts, "Id", "Contact_name", venteArticle.Contact_);
            ViewBag.Article_ = new SelectList(db.Articles, "Idp", "Nom", venteArticle.Article_);
             if (ModelState.IsValid)
            {
               db.Entry(venteArticle).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(venteArticle);
        }

        // GET: VenteArticles/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VenteArticle venteArticle = await db.VenteArticles.FindAsync(id);
            if (venteArticle == null)
            {
                return HttpNotFound();
            }
            return View(venteArticle);
        }

        // POST: VenteArticles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            VenteArticle venteArticle = await db.VenteArticles.FindAsync(id);
            db.VenteArticles.Remove(venteArticle);
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
