using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using AITECRM.Models;

namespace AITECRM.Controllers
{
    [Authorize(Roles = ("Admin"))]
    public class EmailServeursController : Controller
    {
        private AITECRMContext db = new AITECRMContext();

        // GET: EmailServeurs
        public async Task<ActionResult> Index()
        {
            return View(await db.EmailServeurs.ToListAsync());
        }

        // GET: EmailServeurs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailServeur emailServeur = await db.EmailServeurs.FindAsync(id);
            if (emailServeur == null)
            {
                return HttpNotFound();
            }
            return View(emailServeur);
        }

        // GET: EmailServeurs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmailServeurs/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Port,DateAjout,ServeurAdresse,AdresseCRM,PassWordCRM,EtatConfig")] EmailServeur emailServeur)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in db.EmailServeurs.Where(x=>x.EtatConfig=="actif"))
                {
                    if (item!=null)
                    {
                        item.EtatConfig = "inactif";
                    }
                }
                db.EmailServeurs.Add(emailServeur);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(emailServeur);
        }

        // GET: EmailServeurs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailServeur emailServeur = await db.EmailServeurs.FindAsync(id);
            if (emailServeur == null)
            {
                return HttpNotFound();
            }
            return View(emailServeur);
        }

        // POST: EmailServeurs/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Port,DateAjout,ServeurAdresse,AdresseCRM,PassWordCRM,EtatConfig")] EmailServeur emailServeur)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emailServeur).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(emailServeur);
        }

        // GET: EmailServeurs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailServeur emailServeur = await db.EmailServeurs.FindAsync(id);
            if (emailServeur == null)
            {
                return HttpNotFound();
            }
            return View(emailServeur);
        }

        // POST: EmailServeurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            EmailServeur emailServeur = await db.EmailServeurs.FindAsync(id);
            db.EmailServeurs.Remove(emailServeur);
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
