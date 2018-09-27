using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using AITECRM.Models;

namespace AITECRM.Controllers
{
    [Authorize(Roles = ("Admin"))]
    public class ServeurSmsController : Controller
    {
        private AITECRMContext db = new AITECRMContext();

        // GET: ServeurSms
        public async Task<ActionResult> Index()
        {
            return View(await db.ServeurSmss.ToListAsync());
        }

        // GET: ServeurSms/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServeurSms serveurSms = await db.ServeurSmss.FindAsync(id);
            if (serveurSms == null)
            {
                return HttpNotFound();
            }
            return View(serveurSms);
        }

        // GET: ServeurSms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServeurSms/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,ServerAdress,Username,PasswordUser,SenderId,Port,EtatConfig")] ServeurSms serveurSms)
        {
            if (ModelState.IsValid)
            {
                db.ServeurSmss.Add(serveurSms);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(serveurSms);
        }

        // GET: ServeurSms/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServeurSms serveurSms = await db.ServeurSmss.FindAsync(id);
            if (serveurSms == null)
            {
                return HttpNotFound();
            }
            return View(serveurSms);
        }

        // POST: ServeurSms/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,ServerAdress,Username,PasswordUser,SenderId,Port,EtatConfig")] ServeurSms serveurSms)
        {
            if (ModelState.IsValid)
            {
                db.Entry(serveurSms).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(serveurSms);
        }

        // GET: ServeurSms/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServeurSms serveurSms = await db.ServeurSmss.FindAsync(id);
            if (serveurSms == null)
            {
                return HttpNotFound();
            }
            return View(serveurSms);
        }

        // POST: ServeurSms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ServeurSms serveurSms = await db.ServeurSmss.FindAsync(id);
            db.ServeurSmss.Remove(serveurSms);
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
