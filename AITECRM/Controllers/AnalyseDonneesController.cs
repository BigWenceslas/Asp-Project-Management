using System;
using System.Web.Mvc;
using AITECRM.ViewModels;
using AITECRM.Models;

namespace AITECRM.Controllers
{
    [Authorize(Roles = ("Admin,Analyse,Semi_Admin"))]
    public class AnalyseDonneesController : Controller
    {
        private DAL dal = new DAL();
        private AnalyseDonneesViewModel AnalyseModel = new AnalyseDonneesViewModel();
        private AITECRMContext db = new AITECRMContext();

        // GET: AnalyseDonnees Index



     
         public ActionResult NombreTotalContactPays()
        {
            var data1 = dal.NombreTotalContactPays();
            return Json(data1.ToArray(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index()
        {

            return View();
        }
        //Entreprises
 

        [HttpPost]
        public ActionResult NombreTotalEntreprisePays(DateTime inf, DateTime sup)
        {
            var data = dal.NombreTotalEntreprisePays(inf,sup);
            db.Dispose();
            return Json(data.ToArray(), JsonRequestBehavior.AllowGet);
           
        }
        [HttpGet]
        public ActionResult NombreTotalEntreprisePays()
        {
            var data = dal.NombreTotalEntreprisePays();
            db.Dispose();
            return Json(data.ToArray(), JsonRequestBehavior.AllowGet);
        }
        //*************************************************************************************
        //Contacts
        public ActionResult Contacts() {
            
            return View();
        }
        public ActionResult NombreTotalContactCategorie()
        {
         var data = dal.NombreTotalContactCategorie();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult NombreTotalContactMois()
        {
            var data = dal.NombreTotalContactMois();
            db.Dispose();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult NombreTotalContactCategorie(DateTime inf, DateTime sup)
        {
            var data = dal.NombreTotalContactCategorie(inf,sup);
            db.Dispose();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult NombreTotalContactMois(DateTime inf, DateTime sup)
        {
            var data = dal.NombreTotalContactMois(inf, sup);
            db.Dispose();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //Activites
        [HttpGet]
        public ActionResult NombreActivites()
        {
            var data = dal.NombreActivites();
            db.Dispose();
            return Json(data,JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult TauxExecutionActivite()
        {
                var data = dal.TauxExecution();
            db.Dispose();
            return Json(data, JsonRequestBehavior.AllowGet);
            
        }
        [HttpGet]
        public ActionResult ChiffreAffaireActivite()
        {
            var data = dal.ChiffreAffaire();
            db.Dispose();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ChiffreAffaireActivite(DateTime inf, DateTime sup)
        {
            var data = dal.ChiffreAffaire(inf,sup);
            db.Dispose();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult NombreActivites(DateTime inf, DateTime sup)
        {
            var data = dal.NombreActivites(inf, sup);
            db.Dispose();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult TauxExecutionActivite(DateTime inf, DateTime sup)
        {
            var data = dal.TauxExecution(inf, sup);
            db.Dispose();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Activites()
        {

            return View();
        }

        //*************************VENTES******************************

        public ActionResult Produits()
        {

            return View();
        }

        public ActionResult VentesProduits()
        {
            return View();
        }
        [HttpGet]
        public ActionResult NombreTotalAchatTypeProduit()
        {
            var data = dal.NombreTotalAchatTypeProduit();
            db.Dispose();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult ChiffreAffaireTypeProduit()
        {
            var data = dal.ChiffreAffaireTypeProduit();
            db.Dispose();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult VentesProduits(DateTime inf, DateTime sup)
        {
          var data = dal.NombreTotalAchatTypeProduit(inf, sup);
            db.Dispose();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult VentesProduitsChiffre(DateTime inf, DateTime sup)
        {
           var data = dal.ChiffreAffaireTypeProduit(inf, sup);
            db.Dispose();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //*****************************************************************************

        public ActionResult Sms()
        {
            return View();
        }
        [HttpGet]
        public ActionResult NombreTotalSmsMois()
        {
            var data = dal.NombreTotalSmsMois();
            db.Dispose();
            return Json(data,JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult NombreTotalSmsEntrepriseMois()
        {
            var data1 = dal.NombreTotalSmsEntrepriseMois();
            db.Dispose();
            return Json(data1,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult NombreTotalSmsMois(DateTime inf, DateTime sup)
        {
            var data = dal.NombreTotalSmsMois(inf,sup);
            db.Dispose();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult NombreTotalSmsEntrepriseMois(DateTime inf, DateTime sup)
        {
            var data = dal.NombreTotalSmsEntrepriseMois(inf, sup);
            db.Dispose();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //*********************Emails*************************************
        public ActionResult Email()
        {
            return View();
        }
        [HttpGet]
        public ActionResult NombreTotalEmailsMois()
        {
           var data = dal.NombreTotalEmailsMois();
            db.Dispose();
            return Json(data,JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult NombreTotalEmailsEntrepriseMois()
        {
            var data1 = dal.NombreTotalEmailsEntrepriseMois();
            db.Dispose();
            return Json(data1, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult NombreTotalEmailsMois(DateTime inf, DateTime sup)
        {
           var data = dal.NombreTotalEmailsMois(inf,sup);
            db.Dispose();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult NombreTotalEmailsEntrepriseMois(DateTime inf, DateTime sup)
        {
            var data1 = dal.NombreTotalEmailsMois(inf, sup);
            db.Dispose();
            return Json(data1, JsonRequestBehavior.AllowGet);
        }
    }
}