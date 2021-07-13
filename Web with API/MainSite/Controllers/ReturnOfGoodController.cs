using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MainSite.Models;
using PagedList;

namespace MainSite.Controllers
{
    [CheckLoginStateJ]
    public class ReturnOfGoodController : Controller
    {
        JuJuLocaldbEntities db = new JuJuLocaldbEntities();

        // GET: ReturnOfGoods
        public ActionResult Index(int page = 1)
        {
            var ReturnOfGoods = db.ReturnOfGoods.Include(r => r.Resident).Where(p => p.Sign != true).ToList();
            //return View(ReturnOfGoods);
            int pageSize = 5;
            int currentPage = page < 1 ? 1 : page;
            var pagedCust = ReturnOfGoods.ToPagedList(currentPage, pageSize);
            return View(pagedCust);
        }
        public ActionResult Picked(int page = 1)
        {
            var ReturnOfGoods = db.ReturnOfGoods.Include(p => p.Resident).Where(p => p.Sign == true).ToList();
            //return PartialView(ReturnOfGoods);
            int pageSize = 5;
            int currentPage = page < 1 ? 1 : page;
            var pagedCust = ReturnOfGoods.ToPagedList(currentPage, pageSize);
            return View(pagedCust);
        }

        public FileContentResult GetImage(long? sn, string account)
        {
            var returnOfGood = db.ReturnOfGoods.Where(r=>r.SN==sn&&r.Account==account).FirstOrDefault();

            return File(returnOfGood.CourierSign, "image/jpeg");

        }

        // GET: ReturnOfGoods/Details/5
        public ActionResult Details(long? sn, string account)
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

        // GET: ReturnOfGoods/Create
        public ActionResult Create()
        {
            ViewBag.Account = new SelectList(db.Resident, "Account", "Name");
            return View();
        }

        // POST: ReturnOfGoods/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SN,Account,ReceiptDate,LogisticsCompany,Sign,CourierSign")] ReturnOfGoods returnOfGoods, HttpPostedFileBase image)
        {
            if (image != null)
            {
                returnOfGoods.CourierSign = new byte[image.ContentLength];
                image.InputStream.Read(returnOfGoods.CourierSign, 0, image.ContentLength);
            }

            if (ModelState.IsValid)
            {
                db.ReturnOfGoods.Add(returnOfGoods);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.Account = new SelectList(db.Resident, "Account", "Name", returnOfGoods.Account);
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

            TempData["oldCourierSign"] = returnOfGoods.CourierSign;
            ViewBag.Account = new SelectList(db.Resident, "Account", "Name", returnOfGoods.Account);

            return View(returnOfGoods);
        }

        // POST: ReturnOfGoods/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SN,Account,ReceiptDate,LogisticsCompany,Sign,CourierSign")] ReturnOfGoods returnOfGoods, HttpPostedFileBase image)
        {
            ModelState.Remove("SN");

            if (image != null)
            {
                returnOfGoods.CourierSign = new byte[image.ContentLength];
                image.InputStream.Read(returnOfGoods.CourierSign, 0, image.ContentLength);
            }
            else
            {
                returnOfGoods.CourierSign = (byte[])TempData["oldCourierSign"];
            }

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
