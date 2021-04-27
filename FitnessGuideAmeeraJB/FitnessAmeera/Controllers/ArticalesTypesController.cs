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

namespace FitnessAmeera.Controllers
{
    [Authorize(Roles = "Admins")]
    public class ArticalesTypesController : Controller
    {       
        private ApplicationDbContext db = new ApplicationDbContext();

                                    // GET: ArticalesTypes
        public ActionResult Index(int ? page)
        {
            return View(db.ArticalesTypes.ToList().ToPagedList(page ?? 1 , 5));
        }

                                 // GET: ArticalesTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticalesTypes articalesTypes = await db.ArticalesTypes.FindAsync(id);
            if (articalesTypes == null)
            {
                return HttpNotFound();
            }
            return View(articalesTypes);
        }

                              // GET: ArticalesTypes/Create
        public ActionResult Create()
        {
            return View();
        }

                            // POST: ArticalesTypes/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Type,Description")] ArticalesTypes articalesTypes)
        {
            if (ModelState.IsValid)
            {
                db.ArticalesTypes.Add(articalesTypes);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(articalesTypes);
        }

        // GET: ArticalesTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticalesTypes articalesTypes = await db.ArticalesTypes.FindAsync(id);
            if (articalesTypes == null)
            {
                return HttpNotFound();
            }
            return View(articalesTypes);
        }

        // POST: ArticalesTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Type,Description")] ArticalesTypes articalesTypes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(articalesTypes).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(articalesTypes);
        }

        // GET: ArticalesTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticalesTypes articalesTypes = await db.ArticalesTypes.FindAsync(id);
            if (articalesTypes == null)
            {
                return HttpNotFound();
            }
            return View(articalesTypes);
        }

        // POST: ArticalesTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ArticalesTypes articalesTypes = await db.ArticalesTypes.FindAsync(id);
            db.ArticalesTypes.Remove(articalesTypes);
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
