using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FitnessAmeera.Models;

namespace FitnessAmeera.Controllers
{
    public class MySavedArticlesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MySavedArticles
        public ActionResult Index()
        {
            var mySavedArticles = db.MySavedArticles.Include(m => m.articles).Include(m => m.User);
            return View(mySavedArticles.ToList());
        }

        // GET: MySavedArticles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MySavedArticles mySavedArticles = db.MySavedArticles.Find(id);
            if (mySavedArticles == null)
            {
                return HttpNotFound();
            }
            return View(mySavedArticles);
        }

        // GET: MySavedArticles/Create
        public ActionResult Create()
        {
            ViewBag.articlesId = new SelectList(db.Articles, "Id", "ArticleSummary");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Age");
            return View();
        }

        // POST: MySavedArticles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,articlesId,UserId")] MySavedArticles mySavedArticles)
        {
            if (ModelState.IsValid)
            {
                db.MySavedArticles.Add(mySavedArticles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.articlesId = new SelectList(db.Articles, "Id", "ArticleSummary", mySavedArticles.articlesId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Age", mySavedArticles.UserId);
            return View(mySavedArticles);
        }

        // GET: MySavedArticles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MySavedArticles mySavedArticles = db.MySavedArticles.Find(id);
            if (mySavedArticles == null)
            {
                return HttpNotFound();
            }
            ViewBag.articlesId = new SelectList(db.Articles, "Id", "ArticleSummary", mySavedArticles.articlesId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Age", mySavedArticles.UserId);
            return View(mySavedArticles);
        }

        // POST: MySavedArticles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,articlesId,UserId")] MySavedArticles mySavedArticles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mySavedArticles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.articlesId = new SelectList(db.Articles, "Id", "ArticleSummary", mySavedArticles.articlesId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Age", mySavedArticles.UserId);
            return View(mySavedArticles);
        }

        // GET: MySavedArticles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MySavedArticles mySavedArticles = db.MySavedArticles.Find(id);
            if (mySavedArticles == null)
            {
                return HttpNotFound();
            }
            return View(mySavedArticles);
        }

        // POST: MySavedArticles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MySavedArticles mySavedArticles = db.MySavedArticles.Find(id);
            db.MySavedArticles.Remove(mySavedArticles);
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
