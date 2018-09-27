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
    public class ContactGroupsController : Controller
    {
        private AITECRMContext db = new AITECRMContext();

        // GET: ContactGroups
        public async Task<ActionResult> Index()
        {
            return View(await db.ContactGroup.ToListAsync());
        }

        // GET: ContactGroups/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactGroup contactGroup = await db.ContactGroup.FindAsync(id);
            contactGroup.listContact = await db.Contacts.Where(x => x.Contactgroups == id).ToListAsync();
            if (contactGroup == null)
            {
                return HttpNotFound();
            }
            return View(contactGroup);
        }

        // GET: ContactGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContactGroups/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Type_name,Description")] ContactGroup contactGroup)
        {
            if (ModelState.IsValid)
            {
                db.ContactGroup.Add(contactGroup);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(contactGroup);
        }

        // GET: ContactGroups/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactGroup contactGroup = await db.ContactGroup.FindAsync(id);
            if (contactGroup == null)
            {
                return HttpNotFound();
            }
            return View(contactGroup);
        }

        // POST: ContactGroups/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Type_name,Description")] ContactGroup contactGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contactGroup).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(contactGroup);
        }

        // GET: ContactGroups/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactGroup contactGroup = await db.ContactGroup.FindAsync(id);
            if (contactGroup == null)
            {
                return HttpNotFound();
            }
            return View(contactGroup);
        }

        // POST: ContactGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ContactGroup contactGroup = await db.ContactGroup.FindAsync(id);
            db.ContactGroup.Remove(contactGroup);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Newdelete(int id)
        {
            ContactGroup contact = db.ContactGroup.Find(id);
            db.ContactGroup.Remove(contact);
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
