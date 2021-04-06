using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class ArticlesCommentsController : Controller
    {
        private MagazineContext db = new MagazineContext();

        // GET: ArticlesComments
        public ActionResult Index()
        {
            var articlesComments = db.ArticlesComments.Include(a => a.Magazine);
            return RedirectToAction("Article", "Magazine");

        }
        public ActionResult Indexs()
        {
            var articlesComments = db.ArticlesComments.Include(a => a.Magazine);
            return View(articlesComments.ToList());
            
        }

        // GET: ArticlesComments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticlesComment articlesComment = db.ArticlesComments.Find(id);
            if (articlesComment == null)
            {
                return HttpNotFound();
            }
            return View(articlesComment);
        }

        // GET: ArticlesComments/Create
        public ActionResult Create()
        {
            ViewBag.MagazineID = new SelectList(db.Magazines, "MagazineID", "MagazineName");
            return RedirectToAction("Article", "Magazine");
        }

        // POST: ArticlesComments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CommentId,Comment,CommentOn,CommentBy,MagazineID")] ArticlesComment articlesComment)
        {
            if (ModelState.IsValid)
            {
                db.ArticlesComments.Add(articlesComment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MagazineID = new SelectList(db.Magazines, "MagazineID", "MagazineName", articlesComment.MagazineID);
            return View(articlesComment);
        }

        // GET: ArticlesComments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticlesComment articlesComment = db.ArticlesComments.Find(id);
            if (articlesComment == null)
            {
                return HttpNotFound();
            }
            ViewBag.MagazineID = new SelectList(db.Magazines, "MagazineID", "MagazineName", articlesComment.MagazineID);
            return View(articlesComment);
        }

        // POST: ArticlesComments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CommentId,Comment,CommentOn,CommentBy,MagazineID")] ArticlesComment articlesComment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(articlesComment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MagazineID = new SelectList(db.Magazines, "MagazineID", "MagazineName", articlesComment.MagazineID);
            return View(articlesComment);
        }

        // GET: ArticlesComments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticlesComment articlesComment = db.ArticlesComments.Find(id);
            if (articlesComment == null)
            {
                return HttpNotFound();
            }
            return View(articlesComment);
        }

        // POST: ArticlesComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ArticlesComment articlesComment = db.ArticlesComments.Find(id);
            db.ArticlesComments.Remove(articlesComment);
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
