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
    public class AdminUsersPanelController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AdminUsersPanel
        public ActionResult Index(int ? page)
        {
            var roleId = db.Roles.Where(m => m.Name == "users").Select(m => m.Id).SingleOrDefault();
            var count = db.Users.Where(u => u.Roles.Any(r => r.RoleId == roleId)).Count();
            ViewBag.count = count;
            return View(db.Users.Where(u => u.Roles.Any(r => r.RoleId == roleId)).ToList().ToPagedList(page ?? 1, 10));
        }

        // GET: AdminUsersPanel/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }


        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }
        // POST: AdminUsersPanel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);
            db.Users.Remove(applicationUser);
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
