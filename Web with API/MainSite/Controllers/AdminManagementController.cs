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
    [CheckLoginState]
    public class AdminManagementController : Controller
    {
        JuJuLocaldbEntities db = new JuJuLocaldbEntities();

        // GET: AdminManagement
        public ActionResult Index()
        {
            var DataChairman = db.Chairman.Include(c => c.Resident).ToList();
            return View(DataChairman);
        }

        // GET: AdminManagement/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chairman chairman = db.Chairman.Find(id);
            if (chairman == null)
            {
                return HttpNotFound();
            }
            return View(chairman);
        }

        // GET: AdminManagement/Create
        public ActionResult Create()
        {
            ViewBag.Account = new SelectList(db.Resident, "Account", "Account");
            return View();
        }

        // POST: AdminManagement/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChairmanAccount,Account,Password,Working")] Chairman chairman)
        {
            if (ModelState.IsValid)
            {
                db.Chairman.Add(chairman);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Account = new SelectList(db.Resident, "Account", "Account", chairman.Account);
            return View(chairman);
        }

        // GET: AdminManagement/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chairman chairman = db.Chairman.Find(id);
            if (chairman == null)
            {
                return HttpNotFound();
            }
            ViewBag.Account = new SelectList(db.Resident, "Account", "Password", chairman.Account);
            return View(chairman);
        }

        // POST: AdminManagement/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChairmanAccount,Account,Password,Working")] Chairman chairman)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chairman).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Account = new SelectList(db.Resident, "Account", "Password", chairman.Account);
            return View(chairman);
        }

        // GET: AdminManagement/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chairman chairman = db.Chairman.Find(id);
            if (chairman == null)
            {
                return HttpNotFound();
            }
            return View(chairman);
        }

        // POST: AdminManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Chairman chairman = db.Chairman.Find(id);
            db.Chairman.Remove(chairman);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        ////////////////////////管理員////////////////////////
        public ActionResult DataJanitor()
        {
            var DataJanitor = db.Janitor.Include(j=>j.Chairman).ToList();
            return PartialView(DataJanitor);
        }

        public ActionResult CreateJanitor()
        {
            ViewBag.ChairmanAccount = new SelectList(db.Chairman, "ChairmanAccount", "ChairmanAccount");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateJanitor([Bind(Include = "JanitorAccount,Password,ChairmanAccount")] Janitor janitor)
        {
            if (ModelState.IsValid)
            {
                db.Janitor.Add(janitor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ChairmanAccount = new SelectList(db.Chairman, "ChairmanAccount", "ChairmanAccount", janitor.ChairmanAccount);//跟這句無關
            return View("Index");
            //return RedirectToAction("Index");
        }

        public ActionResult EditJanitor(string janitorAccount)
        {
            if (janitorAccount == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Janitor janitor = db.Janitor.Find(janitorAccount);
            if (janitor == null)
            {
                return HttpNotFound();
            }
            ViewBag.ChairmanAccount = new SelectList(db.Chairman, "ChairmanAccount", "ChairmanAccount", janitor.ChairmanAccount);
            return View(janitor);
        }

        // POST: AdminManagement/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditJanitor([Bind(Include = "JanitorAccount,Password,ChairmanAccount")] Janitor janitor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(janitor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ChairmanAccount = new SelectList(db.Chairman, "ChairmanAccount", "ChairmanAccount", janitor.ChairmanAccount);
            return View(janitor);
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
