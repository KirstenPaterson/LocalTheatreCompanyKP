using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LocalTheatreCompany.Models;

namespace LocalTheatreCompany.Controllers
{
    public class ArticlesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Articles
        [HttpPost,ActionName("details")]
        public ActionResult Details(LocalTheatreCompany.Models.Comment cmt)
        {
            cmt.commentAuthor = User.Identity.Name;
            cmt.CommentDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Comments.Add(cmt);
                db.SaveChanges();
                return RedirectToAction("Details", cmt.articleId);
            }
            return View();
        }
        public ActionResult Index()
        {
            var articles= db.Articles
                .Include("Comments")
                .ToList();
            return View(articles);
        }

        // GET: Articles/Details/5
        public ActionResult Details(int? id)
        {
            var article = db.Articles.Where(b => b.articleId == id)
                .Include("Comments")
                .FirstOrDefault();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: Articles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Article article)
        {  
            article.postDate = DateTime.Now;
            article.UserName = User.Identity.Name;
            db.Articles.Add(article);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
       

        // GET: Articles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var article = db.Articles.Where(b => b.articleId == id).Include("Comments").FirstOrDefault();
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public ActionResult Edit( Article article)
        {
                // setting name to user name and date to current time 
                article.UserName = User.Identity.Name;
                article.postDate = DateTime.Now;

                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
           
        }

        //Delete Comments 
        [ValidateAntiForgeryToken]
        [HttpPost,ActionName("DeleteComment")]
        public ActionResult DeleteComment(Comment cmt)
        {
            var modeltodelete = db.Comments.Find(cmt.commentId);
            if (ModelState.IsValid)
            {
                db.Comments.Remove(modeltodelete);
                db.SaveChanges();
                return RedirectToAction("Edit", "Articles", new { @id = cmt.articleId });
            }

            return View();
        }


        // GET: Articles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var article = db.Articles.Where(b => b.articleId == id).Include("Comments").FirstOrDefault();
            
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var article = db.Articles.Where(b=> b.articleId == id).Include("Comments").FirstOrDefault();
            db.Articles.Remove(article);
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
