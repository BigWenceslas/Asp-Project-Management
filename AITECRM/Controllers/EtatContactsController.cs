using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using AITECRM.Models;

namespace AITECRM.Controllers
{
    [Authorize(Roles = ("Admin,Operateur,Semi_Admin"))]
    public class EtatContactsController : Controller
    {
        private AITECRMContext db = new AITECRMContext();

        // GET: EtatContacts
        public async Task<ActionResult> Index()
        {
            var etatContacts = db.EtatContacts.Include(e => e.Contact);
            return View(await etatContacts.ToListAsync());
        }

        // GET: EtatContacts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EtatContact etatContact = await db.EtatContacts.FindAsync(id);
            if (etatContact == null)
            {
                return HttpNotFound();
            }
            return View(etatContact);
        }

        // GET: EtatContacts/Create
        public ActionResult Create()
        {
            ViewBag.ContactId = new SelectList(db.Contacts, "Id", "Contact_name");
            return View();
        }

        // POST: EtatContacts/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Intitule,Description,DateVariation,ContactId")] EtatContact etatContact)
        {
            ViewBag.ContactId = new SelectList(db.Contacts, "Id", "Contact_name", etatContact.ContactId);
            if (ModelState.IsValid)
            {
              db.EtatContacts.Add(etatContact);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
             return View(etatContact);
        }

        // GET: EtatContacts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EtatContact etatContact = await db.EtatContacts.FindAsync(id);
            if (etatContact == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContactId = new SelectList(db.Contacts, "Id", "Contact_name", etatContact.ContactId);
            return View(etatContact);
        }

        // POST: EtatContacts/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Intitule,Description,DateVariation,ContactId")] EtatContact etatContact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(etatContact).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ContactId = new SelectList(db.Contacts, "Id", "Contact_name", etatContact.ContactId);
            return View(etatContact);
        }

        // GET: EtatContacts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EtatContact etatContact = await db.EtatContacts.FindAsync(id);
            if (etatContact == null)
            {
                return HttpNotFound();
            }
            return View(etatContact);
        }

        // POST: EtatContacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            EtatContact etatContact = await db.EtatContacts.FindAsync(id);
            db.EtatContacts.Remove(etatContact);
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
