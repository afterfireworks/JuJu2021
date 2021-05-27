using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MainSite.Models;

namespace MainSite.Controllers
{
    public class HomeController : Controller
    {
        private JuJuLocalEntities db = new JuJuLocalEntities();

        // GET: Home
        public ActionResult Index()
        {
            var package = db.Package.Where(p => p.Sign != true).Include(p => p.Resident);
            return View(package.ToList());
        }
        public ActionResult Picked()
        {
            var package = db.Package.Where(p => p.Sign == true).Include(p => p.Resident);
            return PartialView(package.ToList());
        }

        public ActionResult QRcodeProduce(long? sn)
        {
            var person = db.Package.Where(p => p.SN.Equals(sn)).FirstOrDefault().Account;
            var pickerId = db.Collector.Where(c=>c.Account==person).FirstOrDefault();
            
            ViewBag.SN = sn;
            ViewBag.Person = person;

            if (pickerId == null) 
                ViewBag.PickerID = "";
            else
            ViewBag.PickerID = pickerId;

            return View(ViewBag);
        }

        // GET: Home/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Package package = db.Package.Find(id);
            if (package == null)
            {
                return HttpNotFound();
            }
            return View(package);
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            ViewBag.Account = new SelectList(db.Resident, "Account", "Password");
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

            ViewBag.Account = new SelectList(db.Resident, "Account", "Password", package.Account);
            return View(package);
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int? sn, String account)
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
            ViewBag.Account = new SelectList(db.Resident, "Account", "Password", package.Account);
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
            ViewBag.Account = new SelectList(db.Resident, "Account", "Password", package.Account);
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
        public ActionResult DeleteConfirmed(int? sn, String account)
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
