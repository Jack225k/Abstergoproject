using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using WebApp_OpenIDConnect_DotNet.Context;
using System.IO;
using WebApp_OpenIDConnect_DotNet.Models;

namespace WebApp_OpenIDConnect_DotNet.Controllers
{
    public class StudentController : Controller
    {

        private dbContext _db = new dbContext();
        
        // GET: Student
        [Authorize]
        public ActionResult Index(string query)
        {
            if (query != null)
            {
                return View(_db.Students.Where(x => x.StudentNumber == query || query == x.Name || query == x.Surname).ToList());
            }

            if(_db.Students.ToList().Count < 0)
            {
                return View();
            }
            return View(_db.Students.ToList());
        }


        public ActionResult Search(string query)
        {
            //Note: A query could be mapped to search for student name, surname or number.
            return null;
        }

        public ActionResult New()
        {
            ViewData["Request"] = "invalid";
            ViewBag.Title = "Create New Student Entry";
            return View("StudentForm");
        }

        [HttpPost]
        
        public ActionResult Save(Models.StudentViewModel model)
        {
            if(ModelState.IsValid)
            {
                if (model.Student.Id == 0)
                {
                    //new
                    var stu = new Models.Student();
                    //Note: Map file and model together.
                    stu.Name = model.Student.Name;
                    stu.Surname = model.Student.Surname;
                    stu.Email = model.Student.Email;
                    stu.StudentNumber = model.Student.StudentNumber;
                    stu.MobileNumber = model.Student.MobileNumber;
                    stu.TelNumber = model.Student.TelNumber;
                    stu.IsActive = model.Student.IsActive;
                    if (model.File != null)
                    {
                        string ext = Path.GetExtension(model.File.FileName);
                        stu.FilePath = @"~/datafolder/images/" + model.Student.ImageName + ext;
                        if (!Directory.Exists(Server.MapPath("/datafolder/images/")))
                        {
                            Directory.CreateDirectory(Server.MapPath("/datafolder/images/"));
                        }
                        model.File.SaveAs(Server.MapPath(stu.FilePath.Remove(0, 1)));
                        Blob.StorageHandler.Upload(Server.MapPath(stu.FilePath.Remove(0, 1)), stu.FilePath);
                    }

                    _db.Students.Add(stu);
                }
                else
                {
                    //edit
                    var studentindb = _db.Students.Single(s => s.Id == model.Student.Id);
                    //Note: Map file and model together.
                    studentindb.Name = model.Student.Name;
                    studentindb.Surname = model.Student.Surname;
                    studentindb.Email = model.Student.Email;
                    studentindb.StudentNumber = model.Student.StudentNumber;
                    studentindb.MobileNumber = model.Student.MobileNumber;
                    studentindb.TelNumber = model.Student.TelNumber;
                    studentindb.IsActive = model.Student.IsActive;
                    if (model.File != null)
                    {
                        string ext = Path.GetExtension(model.File.FileName);
                        studentindb.FilePath = @"~/datafolder/images/" + model.Student.ImageName + ext;
                        model.File.SaveAs(Server.MapPath(studentindb.FilePath));
                        Blob.StorageHandler.Upload(Server.MapPath(studentindb.FilePath), studentindb.FilePath);
                    }
                }
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Request = true;
            var student = _db.Students.SingleOrDefault(c => c.Id == id);
            if (student == null)
            {
                return HttpNotFound();
            }
            var svm = new StudentViewModel();
            svm.Student = student;
            return View("StudentForm", svm);
        }


        public ActionResult Delete(int id)
        {
            if (id != 0)
            {
                var student = _db.Students.SingleOrDefault(c => c.Id == id);
                _db.Students.Remove(student);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}