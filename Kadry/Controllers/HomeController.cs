using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kadry.Models;

namespace Kadry.Controllers
{
    public class HomeController : Controller
    {
        private readonly SQLConnection _sqlConnection = new SQLConnection();

        public ActionResult Index()
        {
            return View(new Login());
        }

        public ActionResult About(int id)
        {
            Employeer employer = _sqlConnection.GetEmployer(id);
            return View(employer);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {
                if (_sqlConnection.IsPasswordCorrect(login.Username, login.Password))
                {
                    int loginid = _sqlConnection.GetLoginId(login.Username);

                   HttpCookie cookie = new HttpCookie("LoginCookie", _sqlConnection.GetUserId(loginid).ToString());
                   Response.SetCookie(cookie);
                    return RedirectToAction("About", new  { id = _sqlConnection.GetUserId ( loginid ) } );
                }
            }
            
                return RedirectToAction("Index");
            
        }
    }
}