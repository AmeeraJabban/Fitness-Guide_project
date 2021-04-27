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
using Microsoft.AspNet.Identity;

namespace FitnessAmeera.Controllers
{
    [Authorize(Roles = "SuperUsers")]
    public class FitnessTipsController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FitnessTips
        public ActionResult Index(int? page)
        {
            var UserID = User.Identity.GetUserId();
            var fitnessTips = db.FitnessTips.Include(f => f.ArticlesTypes);
            var count = fitnessTips.ToList().Where(a => a.UserID == UserID).Count();
            ViewBag.count = count;
            return View(fitnessTips.ToList().Where(a => a.UserID == UserID).OrderByDescending(a=>a.TipDate).ToPagedList(page ?? 1, 10));
        }

        // GET: FitnessTips/Details/5
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

        // GET: FitnessTips/Create
        public ActionResult Create()
        {
            ViewBag.TipStatus = new SelectList(new[] { "slim", "perfect", "overweight" });
            ViewBag.ArticalesTypesID = new SelectList(db.ArticalesTypes, "Id", "Type");
            return View();
        }

        // POST: FitnessTips/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FitnessTips fitnessTips, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                var UserID = User.Identity.GetUserId();
                fitnessTips.UserID = UserID;

                fitnessTips.TipDate = DateTime.Now;
                string pic = System.IO.Path.GetFileName(upload.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/uploads"), pic);
                upload.SaveAs(path);
                fitnessTips.TipImage = pic;
                db.FitnessTips.Add(fitnessTips);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TipStatus = new SelectList(new[] { "slim", "perfect", "overweight" });
            ViewBag.ArticalesTypesID = new SelectList(db.ArticalesTypes, "Id", "Type", fitnessTips.ArticalesTypesID);
            return View(fitnessTips);
        }

        // GET: FitnessTips/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.TipStatus = new SelectList(new[] { "slim", "perfect", "overweight" });
            ViewBag.ArticalesTypesID = new SelectList(db.ArticalesTypes, "Id", "Type", fitnessTips.ArticalesTypesID);
            return View(fitnessTips);
        }

        // POST: FitnessTips/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FitnessTips fitnessTips, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            { string oldpath = System.IO.Path.Combine(Server.MapPath("~/uploads"), fitnessTips.TipImage);
                if (upload != null)
                {
                    System.IO.File.Delete(oldpath);

                    string pic = System.IO.Path.GetFileName(upload.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/uploads"), pic);
                    upload.SaveAs(path);
                    fitnessTips.TipImage = pic;
                }
                var UserID = User.Identity.GetUserId();
                fitnessTips.UserID = UserID;
                fitnessTips.TipDate = DateTime.Now;
                db.Entry(fitnessTips).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ViewBag.ArticalesTypesID = new SelectList(db.ArticalesTypes, "Id", "Type", fitnessTips.ArticalesTypesID);
            return View(fitnessTips);
        }

        // GET: FitnessTips/Delete/5
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

        // POST: FitnessTips/Delete/5
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
