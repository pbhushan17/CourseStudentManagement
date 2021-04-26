using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using CourseStudentManagement.Models;
using System.Web.Configuration;

namespace CourseStudentManagement.Controllers
{
    public class HomeController : Controller
    {
        private CourseStudentManagementEntities db = new CourseStudentManagementEntities();

        //GET: studentSearch
        public ActionResult Index(string search, int? i)
        {
            List<student> student = db.students.ToList();
            return View(db.students.Where(s => s.firstname.StartsWith(search) || s.surname.StartsWith(search) || search == null).ToList().ToPagedList(i ?? Convert.ToInt32(WebConfigurationManager.AppSettings["PageStart"]), Convert.ToInt32(WebConfigurationManager.AppSettings["PageSize"])));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}