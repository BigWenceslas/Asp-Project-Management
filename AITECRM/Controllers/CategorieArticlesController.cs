using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using AITECRM.Models;

namespace AITECRM.Controllers
{
    [Authorize(Roles = ("Admin,Operateur,Semi_Admin"))]
    public class CategorieArticlesController : Controller
    {
        private AITECRMContext db = new AITECRMContext();

        // GET: CategorieArticles
        public async Task<ActionResult> Index()
        {
            return View(await db.CategorieProduits.ToListAsync());
        }

        // GET: CategorieArticles/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategorieArticle categorieArticle = await db.CategorieProduits.FindAsync(id);
            categorieArticle.Articles = await db.Articles.Where(x => x.CategorieProduit_ == id).ToListAsync();
            categorieArticle.CategorieAr = await db.CategorieAPar.Where(x => x.Id == categorieArticle.CategorieParente_).FirstOrDefaultAsync();
            if (categorieArticle == null)
            {
                return HttpNotFound();
            }
            return View(categorieArticle);
        }

        // GET: CategorieArticles/Create
        public ActionResult Create()
        {
            ViewBag.CategorieParente_ = new SelectList(db.CategorieAPar, "Id", "Nom");
            return View();
        }

        // POST: CategorieArticles/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nom,Description,CategorieParente_")] CategorieArticle categorieArticle)
        {

            ViewBag.CategorieParente_ = new SelectList(db.CategorieAPar, "Id", "Nom", categorieArticle.CategorieParente_);
            if (ModelState.IsValid)
            {
                db.CategorieProduits.Add(categorieArticle);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(categorieArticle);
        }

        // GET: CategorieArticles/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.CategorieParente_ = new SelectList(db.CategorieAPar, "Id", "Nom");
            CategorieArticle categorieArticle = await db.CategorieProduits.FindAsync(id);
            if (categorieArticle == null)
            {
                return HttpNotFound();
            }
            return View(categorieArticle);
        }

        // POST: CategorieArticles/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nom,Description,CategorieParente_")] CategorieArticle categorieArticle)
        {
            ViewBag.CategorieParente_ = new SelectList(db.CategorieAPar, "Id", "Nom", categorieArticle.CategorieParente_);
            if (ModelState.IsValid)
            {
                db.Entry(categorieArticle).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(categorieArticle);
        }

        // GET: CategorieArticles/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategorieArticle categorieArticle = await db.CategorieProduits.FindAsync(id);
            if (categorieArticle == null)
            {
                return HttpNotFound();
            }
            return View(categorieArticle);
        }

        // POST: CategorieArticles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CategorieArticle categorieArticle = await db.CategorieProduits.FindAsync(id);
            db.CategorieProduits.Remove(categorieArticle);
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
