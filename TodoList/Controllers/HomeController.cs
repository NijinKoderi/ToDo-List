using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TodoList.Models;

namespace TodoList.Controllers
{
    public class HomeController : Controller
    {
        Registration RegObj = new Registration();
        Task taskObj = new Task();
        [HttpGet][SessionExpire]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            return View("Login");
        }

        [HttpPost]
        public ActionResult CheckLogin(Login model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var Result = model.CheckLogin();
                  if (Result.EmployeeID != 0)
                    {
                        Session["EmployeeID"] = Result.EmployeeID;
                        Session["EmployeeName"] = Result.EmployeeName;

                        return RedirectToAction("Index", "Home");
                    }
                    return RedirectToAction("Login", "Home");
                }
                else
                {
                    return new JsonResult { Data = "Failed !!" };
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserRegistration(string userName, string userEmail, string password)
        {
            return new JsonResult() { Data = RegObj.UserRegistration(userName, userEmail, password) };
        }
    }
}