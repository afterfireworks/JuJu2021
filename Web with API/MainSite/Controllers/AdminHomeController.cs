using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MainSite.Models;

namespace MainSite.Controllers
{
    public class AdminHomeController : Controller
    {
        //JuJu2021Entities db = new JuJu2021Entities();
        JuJuLocalEntities db = new JuJuLocalEntities();
        // GET: HomeAdmin
        public ActionResult Index()
        {
            var ComplaintData = db.Complaint.ToList();
            return View(ComplaintData);
        }

        public ActionResult Create()
        {
            ViewBag.Account = new SelectList(db.Resident, "Account", "Account");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Complaint complaint)
        {
        //    var Account = new List<SelectListItem>()
        //{
        //new SelectListItem {Text="text-1", Value="value-1" },
        //new SelectListItem {Text="text-2", Value="value-2" },
        //new SelectListItem {Text="text-3", Value="value-3" },
        // };

            //預設選擇哪一筆
            //Account.Where(q => q.Value == "value-1").First().Selected = true;

            ViewBag.Account = new SelectList(db.Resident, "Account", "Account");

            if (ModelState.IsValid)
            {

                db.Complaint.Add(complaint);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(complaint);

        }

        public ActionResult Details(long? sn, string account)
        {
            if (sn == null|| account==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Complaint Complaint = db.Complaint.Where(c=>c.SN==sn && c.Account==account).FirstOrDefault();
            if (Complaint == null)
            {
                return HttpNotFound();
            }
            return View(Complaint);
        }
    }
}