using NewsArticle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsArticle.Controllers
{
    public class ArticleController : Controller
    {
        // GET: Article
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNew(string title, string description)
        {
            ArticleRepository repo = new ArticleRepository();
            ArticleModel model = new ArticleModel();
            model.CreatedBy = User.Identity.Name;
            DateTime now = DateTime.Now;
            model.CreatedDate = now;
            model.LastModifiedDate = now;
            model.Id = Guid.NewGuid();
            model.Title = title;
            model.Description = description;
            repo.AddNewArticle(model);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            ArticleRepository repo = new ArticleRepository();
            var r = repo.GetAllArticles();
            return Json(r, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetMyArticles()
        {
            ArticleRepository repo = new ArticleRepository();
            var r = repo.GetMyArticles(User.Identity.Name);
            return Json(r, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteArticle(string id)
        {
            ArticleRepository repo = new ArticleRepository();
            repo.DeleteArticle(id);
            return Json(true , JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EditArticle(string id, string title, string description)
        {
            ArticleRepository repo = new ArticleRepository();
            repo.EditArticle(id, title, description);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}