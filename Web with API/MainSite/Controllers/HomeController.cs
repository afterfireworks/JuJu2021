using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MainSite.Models;
using MessagingToolkit.QRCode;
using System.Drawing;
using System.Drawing.Imaging;
using PagedList;

namespace MainSite.Controllers
{
    [CheckLoginStateJ]
    public class HomeController : Controller
    {
        JuJuLocaldbEntities db = new JuJuLocaldbEntities();

        // GET: Home
        public ActionResult Index(int page = 1)
        {
            var Package = db.Package.Where(p => p.Sign != true).ToList();
            int pageSize = 9;
            int currentPage = page < 1 ? 1 : page;
            var pagedCust = Package.ToPagedList(currentPage, pageSize);

            return View(pagedCust);
        }
        public ActionResult Picked(int page = 1)
        {
            var Package = db.Package.Where(p => p.Sign == true).ToList();

            foreach (var item in Package)
            {
                if (item.Signer != null)
                {
                    item.Signer = db.Resident.Where(r => r.Account == item.Signer).FirstOrDefault().Name;
                }
            };

            int pageSize = 9;
            int currentPage = page < 1 ? 1 : page;
            var pagedCust = Package.ToPagedList(currentPage, pageSize);

            return View(pagedCust);
        }

        public ActionResult QRcodeProduce(long? sn)
        {
            var person = db.Package.Where(p => p.SN==sn).FirstOrDefault().Account;
            var pickerId = db.Collector.Where(c=>c.Account==person).FirstOrDefault();
            
            ViewBag.SN = sn;
            ViewBag.Person = person;

            if (pickerId == null) 
                ViewBag.PickerID = "";
            else
            ViewBag.PickerID = pickerId;

            return View(ViewBag);
        }

        //GET: Home/Details/5
        public ActionResult Details(long? sn, string account)
        {
            if (sn == null || account == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Package package = db.Package.Where(p => p.SN == sn && p.Account == account).FirstOrDefault();
            if (package == null)
            {
                return HttpNotFound();
            }
            return View(package);
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            ViewBag.Account = new SelectList(db.Resident, "Account", "Name", db.Resident);
            return View();
        }

        // POST: Home/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SN,Account,ArrivalDate,Sign,SignDate,COD")] Package package)
        {
            if (ModelState.IsValid)
            {
                db.Package.Add(package);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.Resident, "Account", "Name", db.Resident);
            return View(package);
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int? sn, string account)
        {
            if (sn == null || account == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Package package = db.Package.Where(p=>p.SN== sn && p.Account== account).FirstOrDefault();
            if (package == null)
            {
                return HttpNotFound();
            }
            ViewBag.Signer = new SelectList(db.Resident, "Account", "Name", db.Resident);
            return View(package);
        }

        // POST: Home/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SN,Account,ArrivalDate,Sign,SignDate,COD")] Package package)
        {
            if (ModelState.IsValid)
            {
                db.Entry(package).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Signer = new SelectList(db.Resident, "Account", "Name", db.Resident);
            return View(package);
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int? sn, String account)
        {
            if (sn == null || account == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var package = db.Package.Where(p => p.SN == sn && p.Account == account).FirstOrDefault();
            
            if (package == null)
            {
                return HttpNotFound();
            }
            return View(package);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? sn, string account)
        {
            Package package = db.Package.Where(p => p.SN == sn && p.Account == account).FirstOrDefault();
            db.Package.Remove(package);
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
    }
}
