using CourseStudentManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseStudentManagement.Controllers
{
    public class LoginController : Controller
    {
        CourseStudentManagementEntities db = new CourseStudentManagementEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(user user)
        {
            if (ModelState.IsValid)
            {
                using (CourseStudentManagementEntities db = new CourseStudentManagementEntities());
                var userObj = db.users.Where(a => a.username.Equals(user.username) && a.password.Equals(user.password)).FirstOrDefault();

                if (userObj != null)
                {
                    Session["UserId"] = userObj.id.ToString();
                    Session["UserName"] = userObj.username.ToString();
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "The UserName or Password is Incorrect");

            }

            return View(user);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}