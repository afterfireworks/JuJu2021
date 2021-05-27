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
    public class CollectorController : Controller
    {
        private JuJuLocalEntities db = new JuJuLocalEntities();

        // GET: Collectors
        public ActionResult Index()
        {
            var collector = db.Collector.Include(c => c.Resident);
            return View(collector.ToList());
        }

        // GET: Collectors/Details/5
        //public ActionResult Details(string account)
        //{
        //    if (account == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Collector collector = db.Collector.Find(account);
        //    if (collector == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(collector);
        //}

        // GET: Collectors/Create
        public ActionResult Create()
        {
            ViewBag.Account = new SelectList(db.Resident, "Account", "Account");
            return View();
        }

        // POST: Collectors/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Account,ID")] Collector collector)
        {
            if (ModelState.IsValid)
            {
                db.Collector.Add(collector);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Account = new SelectList(db.Resident, "Account", "Account", collector.Account);
            return View(collector);
        }

        // GET: Collectors/Edit/5
        public ActionResult Edit(string account, string id)
        {
            if (account == null || id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var collector = db.Collector.Where(c=>c.Account== account&&c.ID==id).FirstOrDefault();
            if (collector == null)
            {
                return HttpNotFound();
            }
            
            return View(collector);
        }

        // POST: Collectors/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Account,ID")] Collector collector)
        {
            if (ModelState.IsValid)
            {
                db.Entry(collector).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(collector);
        }

        // GET: Collectors/Delete/5
        public ActionResult Delete(string account, string id)
        {
            if (account == null || id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var collector = db.Collector.Where(c => c.Account == account && c.ID == id).FirstOrDefault();
            if (collector == null)
            {
                return HttpNotFound();
            }
            return View(collector);
        }

        // POST: Collectors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string account, string id)
        {
            Collector collector = db.Collector.Where(c => c.Account == account && c.ID == id).FirstOrDefault();
            db.Collector.Remove(collector);
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
