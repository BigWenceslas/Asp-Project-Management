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
    public class CategorieActivitesController : Controller
    {
        private AITECRMContext db = new AITECRMContext();

        // GET: CategorieActivites
        public async Task<ActionResult> Index()
        {
            return View(await db.CategorieActivites.ToListAsync());
        }

        // GET: CategorieActivites/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategorieActivite categorieActivite = await db.CategorieActivites.FindAsync(id);
            categorieActivite.Activites = await db.Activites.Where(x => x.Categorie_ == id).ToListAsync();
            categorieActivite.CategoriePar = await db.CategorieActPar.Where(x=>x.Id == categorieActivite.CategorieParente_).FirstOrDefaultAsync();
            if (categorieActivite == null)
            {
                return HttpNotFound();
            }
            return View(categorieActivite);
        }

        // GET: CategorieActivites/Create
        public ActionResult Create()
        {
            ViewBag.CatActPar = new SelectList(db.CategorieActPar, "Id", "Nom");
            return View();
        }

        // POST: CategorieActivites/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nom,Description,CategorieParente_")] CategorieActivite categorieActivite)
        {
            if (ModelState.IsValid)
            {
                ViewBag.CatActPar = new SelectList(db.CategorieActPar, "Id", "Nom",categorieActivite.CategorieParente_);
                db.CategorieActivites.Add(categorieActivite);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(categorieActivite);
        }

        // GET: CategorieActivites/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            ViewBag.CatActPar = new SelectList(db.CategorieActPar, "Id", "Nom");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategorieActivite categorieActivite = await db.CategorieActivites.FindAsync(id);
            if (categorieActivite == null)
            {
                return HttpNotFound();
            }
            return View(categorieActivite);
        }

        // POST: CategorieActivites/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nom,Description,CategorieParente_")] CategorieActivite categorieActivite)
        {
            if (ModelState.IsValid)
            {
                ViewBag.CatActPar = new SelectList(db.CategorieActPar, "Id", "Nom",categorieActivite.CategorieParente_);
                db.Entry(categorieActivite).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(categorieActivite);
        }

        // GET: CategorieActivites/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategorieActivite categorieActivite = await db.CategorieActivites.FindAsync(id);
            if (categorieActivite == null)
            {
                return HttpNotFound();
            }
            return View(categorieActivite);
        }

        // POST: CategorieActivites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CategorieActivite categorieActivite = await db.CategorieActivites.FindAsync(id);
            db.CategorieActivites.Remove(categorieActivite);
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
