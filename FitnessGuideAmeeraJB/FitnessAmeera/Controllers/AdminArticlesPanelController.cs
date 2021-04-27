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
    public class AdminArticlesPanelController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AdminArticlesPanel
        public ActionResult Index(int ? page)
        {
            var articles = db.Articles.Include(a => a.ArticlesTypes).Include(a=>a.User);
            ViewBag.count = articles.Count();
            return View(articles.ToList().OrderByDescending(a=> a.ArticleDate).ToPagedList(page ?? 1,10));
        }

        // GET: AdminArticlesPanel/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articles articles = db.Articles.Find(id);
            if (articles == null)
            {
                return HttpNotFound();
            }
            return View(articles);
        } 

        // GET: AdminArticlesPanel/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articles articles = db.Articles.Find(id);
            if (articles == null)
            {
                return HttpNotFound();
            }
            return View(articles);
        }

        // POST: AdminArticlesPanel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Articles articles = db.Articles.Find(id);
            db.Articles.Remove(articles);
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
