using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class MagazineController : Controller
    {
        private MagazineContext db = new MagazineContext();

        //
        // GET: /Support/
        public ActionResult Article()
        {
            
            return View(db.Magazines.ToList());
        }

        public ActionResult Index()
        {
            var magazines = db.Magazines.Include(m => m.Topic);
            return View(db.Magazines.ToList());
        }

        //
        // GET: /Support/Create

        public ActionResult Create()
        {
            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "TopicName");
            return View(new Magazine { Createby = User.Identity.Name, MagazinePostDate = DateTime.Now });
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Magazine magazine, string receiver)
        {
            
            if (ModelState.IsValid)
            {
                List<FileDetail> fileDetails = new List<FileDetail>();
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        FileDetail fileDetail = new FileDetail()
                        {
                            FileName = fileName,
                            Extension = Path.GetExtension(fileName),
                            Id = Guid.NewGuid()
                        };
                        fileDetails.Add(fileDetail);

                        var path = Path.Combine(Server.MapPath("~/App_Data/Upload/"), fileDetail.Id + fileDetail.Extension);
                        file.SaveAs(path);
                    }
                }
                ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "TopicName", magazine.TopicID);
                magazine.FileDetails = fileDetails;
                db.Magazines.Add(magazine);
                db.SaveChanges();

                
                string subject = "Your student has submitted their post submission for your Topic";
                string body = "Hi Marketing Coordinator, " + "Your student has submitted their post submission for your Topic: " +magazine.TopicName + " Student's name: " + User.Identity.Name + " Submit Date: " + DateTime.Now;
                WebMail.Send(receiver,subject, body);

                return RedirectToAction("Index");
            }

            return View(magazine);
        }

        //
        // GET: /Support/Edit/5

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Magazine magazine = db.Magazines.Include(s => s.FileDetails).SingleOrDefault(x => x.MagazineID == id);
            if (magazine == null)
            {
                return HttpNotFound();
            }
            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "TopicName", magazine.TopicID);
            return View(magazine);
        }


        public FileResult Download(String p, String d)
        {
            return File(Path.Combine(Server.MapPath("~/App_Data/Upload/"), p), System.Net.Mime.MediaTypeNames.Application.Octet, d);
        }



        //
        // POST: /Support/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Magazine magazine, string receiver)
        {
            if (ModelState.IsValid)
            {

                //New Files
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        FileDetail fileDetail = new FileDetail()
                        {
                            FileName = fileName,
                            Extension = Path.GetExtension(fileName),
                            Id = Guid.NewGuid(),
                            MagazineID = magazine.MagazineID
                        };
                        var path = Path.Combine(Server.MapPath("~/App_Data/Upload/"), fileDetail.Id + fileDetail.Extension);
                        file.SaveAs(path);

                        db.Entry(fileDetail).State = EntityState.Added;
                    }
                }
                ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "TopicName", magazine.TopicID);
                db.Entry(magazine).State = EntityState.Modified;
                db.SaveChanges();
                string subject = "Your student has Delete their post submission for your Topic";
                string body = "Hi Marketing Coordinator," + "Your student has editted their post submission for your Topic: " + magazine.TopicName+ " Student's name: " + User.Identity.Name + " Submit Date: " + DateTime.Now;
                WebMail.Send(receiver, subject, body);
                return RedirectToAction("Index");
            }
            return View(magazine);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Magazine topic = db.Magazines.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }
        [HttpPost]

        public JsonResult DeleteFile(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Result = "Error" });
            }
            try
            {
                Guid guid = new Guid(id);
                FileDetail fileDetail = db.FileDetails.Find(guid);
                if (fileDetail == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Json(new { Result = "Error" });
                }

                //Remove from database
                db.FileDetails.Remove(fileDetail);
                db.SaveChanges();

                //Delete file from the file system
                var path = Path.Combine(Server.MapPath("~/App_Data/Upload/"), fileDetail.Id + fileDetail.Extension);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }





        //
        // POST: /Support/Delete/5

        [HttpPost]
        public JsonResult Delete(int id, string receiver)
        {
            try
            {
                Magazine magazine = db.Magazines.Find(id);
                if (magazine == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Json(new { Result = "Error" });
                }

                //delete files from the file system

                foreach (var item in magazine.FileDetails)
                {
                    String path = Path.Combine(Server.MapPath("~/App_Data/Upload/"), item.Id + item.Extension);
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                }

                db.Magazines.Remove(magazine);
                db.SaveChanges();
                string subject = "Your student has Deleted their post submission for your Topic";
                string body = "Hi Marketing Coordinator" + " Your student has deleted their post submission for your Topic: " + magazine.TopicName + "Student's name: " + User.Identity.Name + " Submit Date: " + DateTime.Now;
                WebMail.Send(receiver, subject, body);

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });

            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}