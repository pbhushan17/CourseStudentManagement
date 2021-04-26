using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using CourseStudentManagement.Extensions;
using CourseStudentManagement.Models;

namespace CourseStudentManagement.Controllers
{
    public class StudentCourseRegistrationController : Controller
    {
        private CourseStudentManagementEntities db = new CourseStudentManagementEntities();
        private List<student> student;
        private List<course> course;

        // GET: StudentCourseRegistration
        public ActionResult Index()
        {
            var studentcourseregistrations = db.studentcourseregistrations.Include(s => s.course).Include(s => s.student);
            return View(studentcourseregistrations.ToList());
        }

        // GET: StudentCourseRegistration/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            studentcourseregistration studentcourseregistration = db.studentcourseregistrations.Find(id);
            if (studentcourseregistration == null)
            {
                return HttpNotFound();
            }
            return View(studentcourseregistration);
        }

        // GET: StudentCourseRegistration/Create
        public ActionResult Create()
        {
            ViewBag.course_id = new SelectList(db.courses, "id", "coursename");
            ViewBag.student_id = new SelectList(db.students, "id", "firstname");
            
            return View();
        }

        // POST: StudentCourseRegistration/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,student_id,course_id")] studentcourseregistration studentcourseregistration)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.studentcourseregistrations.Add(studentcourseregistration);
                    student = db.students.ToList();
                    course = db.courses.ToList();
                    if (student.Where(s => s.id == studentcourseregistration.student_id).FirstOrDefault().noofcourse == Convert.ToInt32(WebConfigurationManager.AppSettings["MaximunNoOfCourse"]))
                    {
                        this.AddNotification("Student can enroll maximun of 5 course only.", NotificationType.WARNING);
                    }
                    else if (course.Where(c => c.id == studentcourseregistration.course_id).FirstOrDefault().courseseats == Convert.ToInt32(WebConfigurationManager.AppSettings["MinimumCourseSeat"]))
                    {
                        this.AddNotification("No more seats available for "+ studentcourseregistration .course.coursename + " course, kindly select any other course.", NotificationType.WARNING);
                    }
                    else
                    {
                        student.Where(s => s.id == studentcourseregistration.student_id).FirstOrDefault().noofcourse += Convert.ToInt32(WebConfigurationManager.AppSettings["IncrementDecrementParameter"]);
                        course.Where(c => c.id == studentcourseregistration.course_id).FirstOrDefault().courseseats -= Convert.ToInt32(WebConfigurationManager.AppSettings["IncrementDecrementParameter"]);
                        db.Entry(studentcourseregistration.student).State = EntityState.Modified;
                        db.Entry(studentcourseregistration.course).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            ViewBag.course_id = new SelectList(db.courses, "id", "coursename", studentcourseregistration.course_id);
            ViewBag.student_id = new SelectList(db.students, "id", "firstname", studentcourseregistration.student_id);
            return View(studentcourseregistration);
        }

        // GET: StudentCourseRegistration/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            studentcourseregistration studentcourseregistration = db.studentcourseregistrations.Find(id);
            if (studentcourseregistration == null)
            {
                return HttpNotFound();
            }
            ViewBag.course_id = new SelectList(db.courses, "ìd", "coursename", studentcourseregistration.course_id);
            ViewBag.student_id = new SelectList(db.students, "id", "firstname", studentcourseregistration.student_id);
            return View(studentcourseregistration);
        }

        // POST: StudentCourseRegistration/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,student_id,course_id")] studentcourseregistration studentcourseregistration)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(studentcourseregistration).State = EntityState.Modified;
                    student = db.students.ToList();
                    course = db.courses.ToList();
                    if (student.Where(s => s.id == studentcourseregistration.student_id).FirstOrDefault().noofcourse ==  Convert.ToInt32(WebConfigurationManager.AppSettings["MaximunNoOfCourse"]))
                    {
                        this.AddNotification("Student can enroll maximun of 5 course only.", NotificationType.WARNING);
                    }
                    else if (course.Where(c => c.id == studentcourseregistration.course_id).FirstOrDefault().courseseats == Convert.ToInt32(WebConfigurationManager.AppSettings["MinimumCourseSeat"]))
                    {
                        this.AddNotification("No more seats available for " + studentcourseregistration.course.coursename + " course, kindly select any other course.", NotificationType.WARNING);
                    }
                    else
                    {
                        student.Where(s => s.id == studentcourseregistration.student_id).FirstOrDefault().noofcourse += Convert.ToInt32(WebConfigurationManager.AppSettings["IncrementDecrementParameter"]);
                        course.Where(c => c.id == studentcourseregistration.course_id).FirstOrDefault().courseseats -= Convert.ToInt32(WebConfigurationManager.AppSettings["IncrementDecrementParameter"]);
                        db.Entry(studentcourseregistration.student).State = EntityState.Modified;
                        db.Entry(studentcourseregistration.course).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            ViewBag.course_id = new SelectList(db.courses, "id", "coursename", studentcourseregistration.course_id);
            ViewBag.student_id = new SelectList(db.students, "id", "firstname", studentcourseregistration.student_id);
            return View(studentcourseregistration);
        }

        // GET: StudentCourseRegistration/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            studentcourseregistration studentcourseregistration = db.studentcourseregistrations.Find(id);
            if (studentcourseregistration == null)
            {
                return HttpNotFound();
            }
            return View(studentcourseregistration);
        }

        // POST: StudentCourseRegistration/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            studentcourseregistration studentcourseregistration = db.studentcourseregistrations.Find(id);
            student = db.students.ToList();
            course = db.courses.ToList();
            student.Where(s => s.id == studentcourseregistration.student_id).FirstOrDefault().noofcourse -= Convert.ToInt32(WebConfigurationManager.AppSettings["IncrementDecrementParameter"]);
            course.Where(c => c.id == studentcourseregistration.course_id).FirstOrDefault().courseseats += Convert.ToInt32(WebConfigurationManager.AppSettings["IncrementDecrementParameter"]);
            db.Entry(studentcourseregistration.student).State = EntityState.Modified;
            db.Entry(studentcourseregistration.course).State = EntityState.Modified;
            db.studentcourseregistrations.Remove(studentcourseregistration);
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
                                this.AddNotification("Student is already enrolled for this course, please select other course.", NotificationType.WARNING);
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
