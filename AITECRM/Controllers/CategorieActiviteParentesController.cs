using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using AITECRM.Models;

namespace AITECRM.Controllers
{
    public class CategorieActiviteParentesController : Controller
    {
        private AITECRMContext db = new AITECRMContext();

        // GET: CategorieActiviteParentes
        public async Task<ActionResult> Index()
        {
            return View(await db.CategorieActPar.ToListAsync());
        }

        // GET: CategorieActiviteParentes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategorieActiviteParente categorieActiviteParente = await db.CategorieActPar.FindAsync(id);
            if (categorieActiviteParente == null)
            {
                return HttpNotFound();
            }
            return View(categorieActiviteParente);
        }

        // GET: CategorieActiviteParentes/Create
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Accueil()
        {
            return View();
        }

        // POST: CategorieActiviteParentes/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nom,Description")] CategorieActiviteParente categorieActiviteParente)
        {
            if (ModelState.IsValid)
            {
                db.CategorieActPar.Add(categorieActiviteParente);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(categorieActiviteParente);
        }

        // GET: CategorieActiviteParentes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategorieActiviteParente categorieActiviteParente = await db.CategorieActPar.FindAsync(id);
            if (categorieActiviteParente == null)
            {
                return HttpNotFound();
            }
            return View(categorieActiviteParente);
        }

        // POST: CategorieActiviteParentes/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nom,Description")] CategorieActiviteParente categorieActiviteParente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categorieActiviteParente).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(categorieActiviteParente);
        }

        // GET: CategorieActiviteParentes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategorieActiviteParente categorieActiviteParente = await db.CategorieActPar.FindAsync(id);
            if (categorieActiviteParente == null)
            {
                return HttpNotFound();
            }
            return View(categorieActiviteParente);
        }

        // POST: CategorieActiviteParentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CategorieActiviteParente categorieActiviteParente = await db.CategorieActPar.FindAsync(id);
            db.CategorieActPar.Remove(categorieActiviteParente);
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
