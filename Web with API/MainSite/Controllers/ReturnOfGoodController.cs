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
    public class ReturnOfGoodController : Controller
    {
        private JuJuLocalEntities db = new JuJuLocalEntities();

        // GET: ReturnOfGoods
        public ActionResult Index()
        {
            var returnOfGoods = db.ReturnOfGoods.Include(r => r.Resident).Where(p => p.Sign != true);
            return View(returnOfGoods.ToList());
        }
        public ActionResult Picked()
        {
            var returnOfGoods = db.ReturnOfGoods.Include(p => p.Resident).Where(p => p.Sign == true);
            return PartialView(returnOfGoods.ToList());
        }

        // GET: ReturnOfGoods/Details/5
        public ActionResult Details(long? sn,string account)
        {
            if (sn == null|| account==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReturnOfGoods returnOfGoods = db.ReturnOfGoods.Where(r=>r.SN==sn && r.Account==account).FirstOrDefault();
            if (returnOfGoods == null)
            {
                return HttpNotFound();
            }
            return View(returnOfGoods);
        }

        // GET: ReturnOfGoods/Create
        public ActionResult Create()
        {
            ViewBag.Account = new SelectList(db.Resident, "Account", "Password");
            return View();
        }

        // POST: ReturnOfGoods/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SN,Account,ReceiptDate,LogisticsCompany,Sign,CourierSign")] ReturnOfGoods returnOfGoods)
        {
            if (ModelState.IsValid)
            {
                db.ReturnOfGoods.Add(returnOfGoods);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Account = new SelectList(db.Resident, "Account", "Password", returnOfGoods.Account);
            return View(returnOfGoods);
        }

        // GET: ReturnOfGoods/Edit/5
        public ActionResult Edit(long? sn, string account)
        {
            if (sn == null || account == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReturnOfGoods returnOfGoods = db.ReturnOfGoods.Where(r => r.SN == sn && r.Account == account).FirstOrDefault();

            if (returnOfGoods == null)
            {
                return HttpNotFound();
            }
            ViewBag.Account = new SelectList(db.Resident, "Account", "Account", returnOfGoods.Account);
            return View(returnOfGoods);
        }

        // POST: ReturnOfGoods/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SN,Account,ReceiptDate,LogisticsCompany,Sign,CourierSign")] ReturnOfGoods returnOfGoods)
        {
            if (ModelState.IsValid)
            {
                db.Entry(returnOfGoods).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Account = new SelectList(db.Resident, "Account", "Account", returnOfGoods.Account);
            return View(returnOfGoods);
        }

        // GET: ReturnOfGoods/Delete/5
        public ActionResult Delete(long? sn, string account)
        {
            if (sn == null || account == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReturnOfGoods returnOfGoods = db.ReturnOfGoods.Where(r => r.SN == sn && r.Account == account).FirstOrDefault();
            if (returnOfGoods == null)
            {
                return HttpNotFound();
            }
            return View(returnOfGoods);
        }

        // POST: ReturnOfGoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long? sn, string account)
        {
            ReturnOfGoods returnOfGoods = db.ReturnOfGoods.Where(r => r.SN == sn && r.Account == account).FirstOrDefault();
            db.ReturnOfGoods.Remove(returnOfGoods);
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
