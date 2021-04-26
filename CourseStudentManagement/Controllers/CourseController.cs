using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CourseStudentManagement.Extensions;
using CourseStudentManagement.Models;

namespace CourseStudentManagement.Controllers
{
    public class CourseController : Controller
    {
        private CourseStudentManagementEntities db = new CourseStudentManagementEntities();

        // GET: course
        public ActionResult Index()
        {
            return View(db.courses.ToList());
        }

        // GET: course/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            course course = db.courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: course/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: course/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,coursename,coursecode,courseseats,teachername,startdate,enddate")] course course)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.courses.Add(course);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex) 
            {
                HandleException(ex);
            }
            return View(course);
        }

        // GET: course/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            course course = db.courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: course/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,coursename,coursecode,courseseats,teachername,startdate,enddate")] course course)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(course).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            return View(course);
        }

        // GET: course/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            course course = db.courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            course course = db.courses.Find(id);
            db.courses.Remove(course);
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
        public virtual void HandleException(Exception exception)
        {
            if (exception is DbUpdateException dbUpdateEx)
            {
                if (dbUpdateEx.InnerException != null
                        && dbUpdateEx.InnerException.InnerException != null)
                {
                    if (dbUpdateEx.InnerException.InnerException is SqlException sqlException)
                    {
                        switch (sqlException.Number)
                        {
                            case 2627:  // Unique constraint error
                                this.AddNotification("Course with same Name and Code already exist.", NotificationType.WARNING);
                                break;
                            default:
                                // A custom exception of yours for other DB issues
                                this.AddNotification("Error occured in db", NotificationType.ERROR);
                                break;
                        }
                    }
                }
            }
        }
    }
}
