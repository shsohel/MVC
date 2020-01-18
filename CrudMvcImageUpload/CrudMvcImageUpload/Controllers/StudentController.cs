using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


using CrudMvcImageUpload.Models;

namespace CrudMvcImageUpload.Controllers
{
    public class StudentController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Student
        public ActionResult Index()
        {

            return View(db.Students.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student std)
        {

            if (ModelState.IsValid)
            {

                string fileName = Path.GetFileNameWithoutExtension(std.PhotoPath.FileName);
                string extension = Path.GetExtension(std.PhotoPath.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                std.StudentPhoto = "~/Images/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                std.PhotoPath.SaveAs(fileName);



                db.Students.Add(std);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(std);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }



            var std = db.Students.Find(id);

            //Session["ImagPath"] = std.StudentPhoto;
            if (std == null)
            {
                return HttpNotFound();
            }

            return View(std);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Student std)
        {
            //if (ModelState.IsValid)
            //{

            //    if (std.PhotoPath != null)
            //    {
            //        string fileName = Path.GetFileNameWithoutExtension(std.PhotoPath.FileName);
            //        string extension = Path.GetExtension(std.PhotoPath.FileName);
            //        fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            //        std.StudentPhoto = "~/Images/" + fileName;
            //        std.PhotoPath.SaveAs(Path.Combine(Server.MapPath("~/Images/"), fileName));

            //    }


            //    db.Entry(std).State = EntityState.Modified;
            //    db.SaveChanges();



            //    return RedirectToAction("Index");
            //}

            //return View();



            var validImageTypes = new string[]
            {
              "image/gif",
              "image/jpeg",
              "image/pjpeg",
              "image/png"
            };


            if (std.PhotoPath != null)
            {
                if (std.PhotoPath.ContentLength > 0)
                {

                    if (!validImageTypes.Contains(std.PhotoPath.ContentType))
                        ModelState.AddModelError("PhotoPath", "Please choose either a GIF, JPG or PNG image.");
                }
            }

            if (ModelState.IsValid)
            {
                var image = db.Students.Find(id);
                if (image == null)
                {
                    return new HttpNotFoundResult();
                }

               

                if (std.PhotoPath != null && std.PhotoPath.ContentLength > 0)
                {
                    // TODO: clear previous image before saving the new one...
                    var uploadDir = "~/images";
                    var imagePath = Path.Combine(Server.MapPath(uploadDir), std.PhotoPath.FileName);
                    var imageUrl = Path.Combine(uploadDir, std.PhotoPath.FileName);
                    std.PhotoPath.SaveAs(imagePath);
                    image.StudentPhoto = imageUrl;
                }

                db.Entry(image).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(std);


        }




    }
}