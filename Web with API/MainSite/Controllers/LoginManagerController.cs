using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Data;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using MainSite.Models;

namespace MainSite.Controllers
{
    public class LoginManagerController : Controller
    {
        JuJuLocaldbEntities db = new JuJuLocaldbEntities();

        public ActionResult Login()
        {
            return View();
        }


        public ActionResult LoginAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginAdmin(string ChairmanAccount, string Password)
        {
            var userA = db.Chairman.Where(u => u.ChairmanAccount == ChairmanAccount && u.Password == Password).FirstOrDefault();
            if (userA == null)
            {
                ViewBag.Msg = "帳號或密碼有誤!!";
                return View();
            }
            Session["userA"] = userA;
            return RedirectToAction("Index", "AdminHome");
        }
        public ActionResult LoginJanitor()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginJanitor(string JanitorAccount, string Password)
        {
            var userJ = db.Janitor.Where(u => u.JanitorAccount == JanitorAccount && u.Password == Password).FirstOrDefault();
            if (userJ == null)
            {
                ViewBag.Msg = "帳號或密碼有誤!!";
                return View();
            }
            Session["userJ"] = userJ;
            return RedirectToAction("Index", "Home");

        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}