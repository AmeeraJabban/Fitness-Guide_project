using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FitnessAmeera.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using PagedList;
using PagedList.Mvc;

namespace FitnessAmeera.Controllers
{
    [Authorize(Roles = "Admins")]
    public class AdminSuperUsersPanelController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
       
        // GET: AdminSuperUsersPanel
        public ActionResult Index(int? page)
        {
            var roleId = db.Roles.Where(m => m.Name == "SuperUsers").Select(m => m.Id).SingleOrDefault();
            var count = db.Users.Where(u => u.Roles.Any(r => r.RoleId == roleId)).Count();
            ViewBag.count = count;
            return View(db.Users.Where(u => u.Roles.Any(r => r.RoleId == roleId)).ToList().ToPagedList(page ?? 1,10));
        }

        // GET: AdminSuperUsersPanel/Details/5
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

        // GET: AdminSuperUsersPanel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminSuperUsersPanel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ApplicationUser applicationUser)
        {
            var rolemanger = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var usermanger = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            IdentityRole role = new IdentityRole();

            if (ModelState.IsValid)
            {
                ApplicationUser user2 = new ApplicationUser();
                user2.Email = applicationUser.Email;
                user2.UserName = applicationUser.Email;
                var check2 = usermanger.Create(user2, applicationUser.Email);
                if (check2.Succeeded)
                {
                    usermanger.AddToRole(user2.Id, "SuperUsers");
                }
                db.Users.Add(user2);
                return RedirectToAction("Index");
            }

            return View(applicationUser);
        }

        // GET: AdminSuperUsersPanel/Edit/5
        public ActionResult Edit(string id)
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

        // POST: AdminSuperUsersPanel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Age,Sex,Weight,Heigh,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicationUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(applicationUser);
        }

        // GET: AdminSuperUsersPanel/Delete/5
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

        // POST: AdminSuperUsersPanel/Delete/5
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
