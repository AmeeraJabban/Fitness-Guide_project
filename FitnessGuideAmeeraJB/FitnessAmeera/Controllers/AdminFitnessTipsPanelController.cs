using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FitnessAmeera.Models;
using PagedList;
using PagedList.Mvc;

namespace FitnessAmeera.Controllers
{
    [Authorize(Roles = "Admins")]
    public class AdminFitnessTipsPanelController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AdminFitnessTipsPanel
        public ActionResult Index(int? page)
        {
            var fitnessTips = db.FitnessTips.Include(f => f.ArticlesTypes).Include(f => f.User);
            ViewBag.count = fitnessTips.Count();
            return View(fitnessTips.ToList().OrderByDescending(a=>a.TipDate).ToPagedList(page ?? 1, 10));
        }

        // GET: AdminFitnessTipsPanel/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FitnessTips fitnessTips = db.FitnessTips.Find(id);
            if (fitnessTips == null)
            {
                return HttpNotFound();
            }
            return View(fitnessTips);
        }


        // GET: AdminFitnessTipsPanel/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FitnessTips fitnessTips = db.FitnessTips.Find(id);
            if (fitnessTips == null)
            {
                return HttpNotFound();
            }
            return View(fitnessTips);
        }

        // POST: AdminFitnessTipsPanel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FitnessTips fitnessTips = db.FitnessTips.Find(id);
            db.FitnessTips.Remove(fitnessTips);
            db.SaveChanges();
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
