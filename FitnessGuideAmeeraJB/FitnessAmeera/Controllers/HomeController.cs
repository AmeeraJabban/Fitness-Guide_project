using FitnessAmeera.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using Microsoft.AspNet.Identity;
using System.Data;
using System.Data.Entity;


namespace FitnessAmeera.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(int? page)
        {
            var Articles = db.Articles.ToList().Where(a => a.ArticlesTypes.Type != "معالجات" && a.ArticlesTypes.Type != "أطعمة" && a.ArticlesTypes.Type != "القيم الغذائية").OrderByDescending(a => a.ArticleDate).Take(4); /*&& a.ArticlesTypes.Type =="رياضي" && a.ArticlesTypes.Type =="غذائي");*/
            ViewBag.Articles= Articles;
            var Articles2 = db.Articles.ToList().OrderByDescending(a => a.ArticleDate).Where(a=>a.ArticlesTypes.Type=="معالجات").Take(4);
            ViewBag.Articles2 = Articles2;
            var Articles3 = db.Articles.ToList().OrderByDescending(a => a.ArticleDate).Where(a => a.ArticlesTypes.Type == "أطعمة").Take(4);
            ViewBag.Articles3 = Articles3;
            var Articles4 = db.Articles.ToList().OrderByDescending(a => a.ArticleDate).Where(a => a.ArticlesTypes.Type == "القيم الغذائية").Take(4);
            ViewBag.Articles4 = Articles4;
            

                var UserId = User.Identity.GetUserId();
                var UserStatus = db.Users.Where(a => a.Id == UserId).Select(a => a.Status).SingleOrDefault();
                var CurrentUser = db.Users.Where(a => a.Id == UserId).SingleOrDefault();
                ViewBag.UserStatus = UserStatus;
                var fitnesstips = db.FitnessTips.ToList().Where(a => a.TipStatus == UserStatus);
                ViewBag.fitnesstips = fitnesstips;




            return View();
        }

        public ActionResult Details(int articleId)
        {
            var article = db.Articles.Find(articleId);
            if (article == null)
            {
                return HttpNotFound();
            }
            Session["articleid"] = articleId;
            Session["articletype"] = article.ArticlesTypes.Type;
            return View(article);
        }
        
        public ActionResult Articles(int? page)
        {
            var Articles = db.Articles.ToList().Where(a => a.ArticlesTypes.Type != "معالجات" && a.ArticlesTypes.Type != "أطعمة" && a.ArticlesTypes.Type != "القيم الغذائية").OrderByDescending(a => a.ArticleDate); /*&& a.ArticlesTypes.Type =="رياضي" && a.ArticlesTypes.Type =="غذائي");*/

            return View(Articles.ToPagedList(page ?? 1, 12));
        }

        public ActionResult Articles2(int? page)
        {
            var Articles2 = db.Articles.ToList().OrderByDescending(a => a.ArticleDate).Where(a => a.ArticlesTypes.Type == "معالجات");

            return View(Articles2.ToList().ToPagedList(page ?? 1, 12));
        }

        public ActionResult Articles3(int? page)
        {
            var Articles3 = db.Articles.ToList().OrderByDescending(a => a.ArticleDate).Where(a => a.ArticlesTypes.Type == "أطعمة");

            return View(Articles3.ToList().ToPagedList(page ?? 1, 12));
        }

        public ActionResult Articles4(int? page)
        {
            var Articles4 = db.Articles.ToList().OrderByDescending(a => a.ArticleDate).Where(a => a.ArticlesTypes.Type == "القيم الغذائية");

            return View(Articles4.ToList().ToPagedList(page ?? 1, 12));
        }



        public ActionResult Test(RegisterViewModel model)
        {
            Double s = model.Weight / ((model.Heigh * model.Heigh) * 0.0001);
            ViewBag.Sex = new SelectList(new[] { "ذكر", "انثى" });
            if (model.Sex == "ذكر")
            {
                if (s <= 20)
                {
                    ViewBag.Status = "أنت تعاني من النحالة .. يرجى الإشتراك لنقدم لك برنامجك الصحي والرياضي الخاص للحصول على الوزن المثالي.";
                }
                else if (s > 20 && s <= 30)
                {
                    ViewBag.Status = "أنت ضمن الوزن الطبيعي .. يرجى الإشتراك لنقدم لك برنامجك الصحي والرياضي الخاص للمحافظة على الوزن المثالي.";
                }
                else if (s > 30)
                {
                    ViewBag.Status = "أنت تعاني من السمنة .. يرجى الإشتراك لنقدم لك برنامجك الصحي والرياضي الخاص للحصول على الوزن المثالي.";
                }
            }
            else
            {
                if (s <= 18.5)
                {
                    ViewBag.Status = "أنتِ تعاني من النحالة .. يرجى الإشتراك لنقدم لك برنامجك الصحي والرياضي الخاص للحصول على الوزن المثالي.";
                }
                else if (s > 18.5 && s <= 25.3)
                {
                    ViewBag.Status = "أنتِ ضمن الوزن الطبيعي .. يرجى الإشتراك لنقدم لك برنامجك الصحي والرياضي الخاص للمحافظة على الوزن المثالي.";
                }
                else if (s > 25.3)
                {
                    ViewBag.Status = "أنتِ تعاني من السمنة .. يرجى الإشتراك لنقدم لك برنامجك الصحي والرياضي الخاص للحصول على الوزن المثالي.";
                }
            }
            return View();
        }





        [Authorize]
        public ActionResult Save(MySavedArticles savedArticles)
        {
            var user = User.Identity.GetUserId();
            var article = (int)Session["articleid"];
            var check = db.MySavedArticles.Where(a => a.articlesId == article && a.UserId == user).ToList();
            if (check.Count < 1)
            {
                savedArticles.UserId = user;
                savedArticles.articlesId = article;
                db.MySavedArticles.Add(savedArticles);
                db.SaveChanges();
            }
            else {

            }
            if (Session["articletype"].ToString() != "معالجات" && Session["articletype"].ToString() != "أطعمة" && Session["articletype"].ToString() != "القيم الغذائية")
            {
                return RedirectToAction("Articles");
            }
            else if (Session["articletype"].ToString() == "معالجات")
            {
                return RedirectToAction("Articles2");

            }
            else if (Session["articletype"].ToString() == "القيم الغذائية")
            {
                return RedirectToAction("Articles4");

            }
            else if (Session["articletype"].ToString() == "أطعمة")
            {

                return RedirectToAction("Articles3");
            }
            else {
                return RedirectToAction("Articles");
            }

        }

        [Authorize]
        public ActionResult GetMySavedArticles(int? page) {

            var userid = User.Identity.GetUserId();
            var articles = db.MySavedArticles.Where(a => a.UserId == userid);

            return View(articles.ToList().ToPagedList(page ?? 1,9));
        }

        [Authorize]
        public ActionResult SavedArticlesDetails(int articleId)
        {

            var article = db.Articles.Find(articleId);
            if (article == null)
            {
                return HttpNotFound();
            }
            var saved = db.MySavedArticles.Where(a => a.articlesId == articleId).Select(a=>a.Id).SingleOrDefault();
            ViewBag.saved = saved;
            Session["articleid"] = articleId;
            Session["articletype"] = article.ArticlesTypes.Type;
            return View(article);
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            var mySaved = db.MySavedArticles.Find(id);
            if (mySaved == null)
            {
                return HttpNotFound();
            }
            return View(mySaved);
        }


        [Authorize]
        [HttpPost]
        public ActionResult Delete(MySavedArticles mySaved)
        {
            var mySavedid = db.MySavedArticles.Find(mySaved.Id);
            db.MySavedArticles.Remove(mySavedid);
            db.SaveChanges();
            return RedirectToAction("GetMySavedArticles");

        }




        

        // GET: FitnessTipsPanel/Details
        public ActionResult FitnessTipsDetails(int TipId)
        {

            var fitnessTips = db.FitnessTips.Find(TipId);
            if (fitnessTips == null)
            {
                return HttpNotFound();
            }

            return View(fitnessTips);
        }


        public ActionResult Contact()
        {
            return View();
        }




    }
}

