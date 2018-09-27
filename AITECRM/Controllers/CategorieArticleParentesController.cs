using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using AITECRM.Models;

namespace AITECRM.Controllers
{
    public class CategorieArticleParentesController : Controller
    {
        private AITECRMContext db = new AITECRMContext();

        // GET: CategorieArticleParentes
        public async Task<ActionResult> Index()
        {
            return View(await db.CategorieAPar.ToListAsync());
        }

        // GET: CategorieArticleParentes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategorieArticleParente categorieArticleParente = await db.CategorieAPar.FindAsync(id);
            if (categorieArticleParente == null)
            {
                return HttpNotFound();
            }
            return View(categorieArticleParente);
        }

        // GET: CategorieArticleParentes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategorieArticleParentes/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nom,Description")] CategorieArticleParente categorieArticleParente)
        {
            if (ModelState.IsValid)
            {
                db.CategorieAPar.Add(categorieArticleParente);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(categorieArticleParente);
        }

        // GET: CategorieArticleParentes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategorieArticleParente categorieArticleParente = await db.CategorieAPar.FindAsync(id);
            if (categorieArticleParente == null)
            {
                return HttpNotFound();
            }
            return View(categorieArticleParente);
        }

        // POST: CategorieArticleParentes/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nom,Description")] CategorieArticleParente categorieArticleParente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categorieArticleParente).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(categorieArticleParente);
        }

        // GET: CategorieArticleParentes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategorieArticleParente categorieArticleParente = await db.CategorieAPar.FindAsync(id);
            if (categorieArticleParente == null)
            {
                return HttpNotFound();
            }
            return View(categorieArticleParente);
        }

        // POST: CategorieArticleParentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CategorieArticleParente categorieArticleParente = await db.CategorieAPar.FindAsync(id);
            db.CategorieAPar.Remove(categorieArticleParente);
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
