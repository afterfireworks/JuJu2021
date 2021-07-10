using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MainSite.Models;
using PagedList;

namespace MainSite.Controllers
{
    [CheckLoginState]
    public class AdminHomeController : Controller
    {
        JuJuLocaldbEntities db = new JuJuLocaldbEntities();
        // GET: HomeAdmin

        //public ActionResult Index()
        //{

        //    var ComplaintData = db.Complaint.Where(c=>c.Solved==false).ToList();
        //    return View(ComplaintData);
        //}
        public ActionResult Index(int page = 1)
        {
            var ComplaintData = db.Complaint.Where(c => c.Solved == false).ToList();
            int pageSize = 9;
            int currentPage = page < 1 ? 1 : page;
            var pagedCust = ComplaintData.ToPagedList(currentPage, pageSize);

            ViewBag.btnTitle = "過往已處理";
            ViewBag.btnAction = "Solved";
            ViewBag.btnAttr = "d-block";
            ViewBag.pageAction = "Index";

            return View(pagedCust);
            
        }

        public ActionResult Solved(int page = 1)
        {
            var ComplaintData = db.Complaint.Where(c => c.Solved == true).ToList();
            int pageSize = 9;
            int currentPage = page < 1 ? 1 : page;
            var pagedCust = ComplaintData.ToPagedList(currentPage, pageSize);

            ViewBag.btnTitle = "目前待處理";
            ViewBag.btnAction = "Index";
            ViewBag.btnAttr = "d-none";
            ViewBag.pageAction = "Solved";

            return View("Index", pagedCust);

        }

        public ActionResult Details(long? sn, string account)
        {
            if (sn == null || account == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Complaint Complaint = db.Complaint.Where(c => c.SN == sn && c.Account == account).FirstOrDefault();
            if (Complaint == null)
            {
                return HttpNotFound();
            }
            return View(Complaint);
        }

        public ActionResult TagSolved(long? sn, string account)
        {
            if (sn == null || account == null )
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Complaint complaint = db.Complaint.Where(c => c.SN == sn && c.Account == account).FirstOrDefault();
            //complaint.Account = complaint.Account;
            //complaint.Date = complaint.Date;
            //complaint.Title = complaint.Title;
            //complaint.Description = complaint.Description;
            complaint.Solved = true;
            

            db.Entry(complaint).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "AdminHome");
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
        
    }
}