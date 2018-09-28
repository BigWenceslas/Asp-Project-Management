
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
    public class ArticlesController : Controller
    {
        private AITECRMContext db = new AITECRMContext();

        // GET: Articles
        public ActionResult Index()
        {
            var ListEmpty = (from a in db.Articles
                             join b in db.CategorieProduits on a.CategorieProduit_ equals b.Id
                             select new
                             {
                                 Id = a.Idp,
                                 nomCategorie = b.Nom,
                                 nomProduit = a.Nom,
                                 prix = a.Prix,
                                 description = a.Descriptionp

                             })
                       .ToList()
                       .Select(x => new ProduitViewModel()
                       {
                           ProduitId = x.Id,
                           NomCategorie = x.nomCategorie,
                           NomProduit = x.nomProduit,
                           Prix = x.prix,
                           Description = x.description,
                       });
            db.Dispose();
            return View(ListEmpty);
        }

        // GET: Articles/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = await db.Articles.FindAsync(id);
            article.CategorieProduit = await db.CategorieProduits.Where(x => x.Id == article.CategorieProduit_).FirstOrDefaultAsync();
            article.Ventes = await db.VenteArticles.Where(x => x.Article_ == id).ToListAsync();
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: Articles/Create
        public ActionResult Create()
        {
            ViewBag.CategorieProduit_ = new SelectList(db.CategorieProduits, "Id", "Nom");
            return View();
        }

        // POST: Articles/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Idp,Nom,Descriptionp,Prix,Date_Creation,CategorieProduit_")] Article article)
        {
            ViewBag.CategorieProduit_ = new SelectList(db.CategorieProduits, "Id", "Nom", article.CategorieProduit_);
            if (ModelState.IsValid)
            {
                db.Articles.Add(article);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(article);
        }

        // GET: Articles/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.CategorieProduit_ = new SelectList(db.CategorieProduits, "Id", "Nom");
            Article article = await db.Articles.FindAsync(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Idp,Nom,Descriptionp,Prix,Date_Creation,CategorieProduit_")] Article article)
        {
            ViewBag.CategorieProduit_ = new SelectList(db.CategorieProduits, "Id", "Nom", article.CategorieProduit_);
            if (ModelState.IsValid)
            {
               db.Entry(article).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(article);
        }

        // GET: Articles/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = await db.Articles.FindAsync(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Article article = await db.Articles.FindAsync(id);
            db.Articles.Remove(article);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Newdelete(int id)
        {
            Article contact = db.Articles.Find(id);
            db.Articles.Remove(contact);
            db.SaveChanges();
            return Json(new { Suppression = "Ok" });
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
