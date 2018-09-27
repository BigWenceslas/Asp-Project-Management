using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using AITECRM.Models;
using System.IO;

namespace AITECRM.Controllers
{
    [Authorize(Roles = ("Admin,Operateur,Semi_Admin"))]
    public class ProjetsController : Controller
    {
        private AITECRMContext db = new AITECRMContext();

        // GET: Projets
        public async Task<ActionResult> Index()
        {
            return View(await db.Projets.Include(w=>w.listTaches).ToListAsync());
        }

        // GET: Projets/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projet projet = await db.Projets.FindAsync(id);
            projet.listTaches = await db.Taches.Where(x => x.Projet_ == id).ToListAsync();
            if (projet == null)
            {
                return HttpNotFound();
            }
            return View(projet);
        }

        // GET: Projets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projets/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Titre,Description,Cout,Date_debut,Date_fin,EtatProjet_")] Projet projet)
        {
            if (ModelState.IsValid)
            {
                List<FileDetailsProjet> fileDetails = new List<FileDetailsProjet>();
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        FileDetailsProjet fileDetail = new FileDetailsProjet()
                        {
                            FileName = fileName,
                            Extension = Path.GetExtension(fileName),
                            Id = Guid.NewGuid()
                        };
                        fileDetails.Add(fileDetail);
                        var path = Path.Combine(Server.MapPath("~/App_Data/UploadProjets/"), fileDetail.Id + fileDetail.Extension);
                        file.SaveAs(path);
                    }
                }
                projet.FileDetails = fileDetails;
                db.Projets.Add(projet);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(projet);
        }

        // GET: Projets/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projet projet = await db.Projets.FindAsync(id);
            if (projet == null)
            {
                return HttpNotFound();
            }
            return View(projet);
        }

        // POST: Projets/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Titre,Description,Cout,Date_debut,Date_fin,EtatProjet_")] Projet projet)
        {
           if (ModelState.IsValid)
            {
                List<FileDetailsProjet> fileDetails = new List<FileDetailsProjet>();
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        FileDetailsProjet fileDetail = new FileDetailsProjet()
                        {
                            FileName = fileName,
                            Extension = Path.GetExtension(fileName),
                            Id = Guid.NewGuid(),
                            SupportId = projet.Id
                        };
                        fileDetails.Add(fileDetail);
                        projet.FileDetails = fileDetails;
                        var path = Path.Combine(Server.MapPath("~/App_Data/UploadProjets/"), fileDetail.Id + fileDetail.Extension);
                        file.SaveAs(path);

                        db.Entry(fileDetail).State = EntityState.Added;
                    }
                }
                db.Entry(projet).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(projet);
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Semi_Admin")]
        public JsonResult DeleteFile(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Result = "Error" });
            }
            try
            {
                Guid guid = new Guid(id);
                FileDetailsProjet fileDetail = db.FileDetailsProjets.Find(guid);
                if (fileDetail == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Json(new { Result = "Error" });
                }

                //Remove from database
                db.FileDetailsProjets.Remove(fileDetail);
                db.SaveChanges();

                //Delete file from the file system
                var path = Path.Combine(Server.MapPath("~/App_Data/UploadProjets/"), fileDetail.Id + fileDetail.Extension);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERREUR", Message = ex.Message });
            }
        }

        // GET: Projets/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projet projet = await db.Projets.FindAsync(id);
            if (projet == null)
            {
                return HttpNotFound();
            }
            return View(projet);
        }

        // POST: Projets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Projet projet = await db.Projets.FindAsync(id);
            db.Projets.Remove(projet);
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
