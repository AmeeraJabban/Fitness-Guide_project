using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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
    public class ArticlesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Articles
        public ActionResult Index(int? page)
        {
            var UserID = User.Identity.GetUserId();
            var articles = db.Articles.Include(a => a.ArticlesTypes).Include(a => a.User.Email);
            var count = db.Articles.ToList().Where(a => a.UserID == UserID).Count();
            ViewBag.count = count;
            return View(db.Articles.ToList().Where(a => a.UserID == UserID).OrderByDescending(a=>a.ArticleDate).ToPagedList(page ?? 1, 10));
        }



        // GET: Articles/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articles articles = await db.Articles.FindAsync(id);
            if (articles == null)
            {
                return HttpNotFound();
            }
            return View(articles);
        }

        // GET: Articles/Create
        public ActionResult Create()
        {
            ViewBag.ArticalesTypesID = new SelectList(db.ArticalesTypes, "Id", "Type");
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create( Articles articles, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                var UserID = User.Identity.GetUserId();
                articles.UserID = UserID;
                articles.ArticleDate = DateTime.Now;
                string pic = System.IO.Path.GetFileName(upload.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/uploads"), pic);
                upload.SaveAs(path);
                articles.ArticleImage = pic;
                db.Articles.Add(articles);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ArticalesTypesID = new SelectList(db.ArticalesTypes, "Id", "Type", articles.ArticalesTypesID);
            return View(articles);
        }

        // GET: Articles/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articles articles = await db.Articles.FindAsync(id);
            if (articles == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArticalesTypesID = new SelectList(db.ArticalesTypes, "Id", "Type", articles.ArticalesTypesID);
            return View(articles);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Articles articles, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                string oldpath = System.IO.Path.Combine(Server.MapPath("~/uploads"), articles.ArticleImage);
                if (upload != null) {
                   System.IO.File.Delete(oldpath);
                    
                    string pic = System.IO.Path.GetFileName(upload.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/uploads"), pic);
                    upload.SaveAs(path);
                    articles.ArticleImage = pic;
                }

                var UserID = User.Identity.GetUserId();
                articles.UserID = UserID;
                articles.ArticleDate = DateTime.Now;
                db.Entry(articles).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ArticalesTypesID = new SelectList(db.ArticalesTypes, "Id", "Type", articles.ArticalesTypesID);
            return View(articles);
        }

        // GET: Articles/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articles articles = await db.Articles.FindAsync(id);
            if (articles == null)
            {
                return HttpNotFound();
            }
            return View(articles);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Articles articles = await db.Articles.FindAsync(id);
            db.Articles.Remove(articles);
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
