using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using LinqKit;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MainSite.Models;
using MainSite.ViewModels;
using PagedList;

namespace MainSite.Controllers
{
    [CheckLoginStateJ]
    public class CollectorController : Controller
    {
        JuJuLocaldbEntities db = new JuJuLocaldbEntities();

        //GET: Collectors
        //public ActionResult Index(int page = 1)
        //{
        //    var data = db.Resident;
        //    var collectorData = db.Collector.ToList();

        //    foreach (var item in collectorData)
        //    {
        //        item.Account = data.Where(d => d.Account == item.Account).FirstOrDefault().Name;
        //        item.ID = data.Where(d => d.ID == item.ID).FirstOrDefault().Name;
        //        //item.Resident.Account = data.Where(d => d.Account == item.Account).FirstOrDefault().Name;
        //        //item.Resident.ID = data.Where(d => d.ID == item.ID).FirstOrDefault().Name;
        //    }

        //    int pageSize = 8;
        //    int currentPage = page < 1 ? 1 : page;
        //    var pagedCust = collectorData.ToPagedList(currentPage, pageSize);
        //    return View(pagedCust);
        //}

        public ActionResult Index(int page = 1)
        {
            IEnumerable<VmCollector> vmCollectors =
            (
                from row in db.Collector
                join SelectByAccount in db.Resident on row.Account equals SelectByAccount.Account
                join SelectByID in db.Resident on row.ID equals SelectByID.ID
                //where
                //orderby

                select new VmCollector
                    {
                        Account = row.Account,
                        ID = row.ID,
                        AccountName = SelectByAccount.Name,
                        IDName = SelectByID.Name,
                    }
            );

            var data = vmCollectors.ToList();
            int pageSize = 8;
            int currentPage = page < 1 ? 1 : page;
            var pagedCust = data.ToPagedList(currentPage, pageSize);

            return View(pagedCust);

        }


        //    from 
        //    Collector-account id;resident name   

        //public ActionResult Index(int page = 1,int x = 1)
        //{
        //    IEnumerable<VmCollector> vmCollectors =
        //    (
        //    from collector as col
        //    inner join resident as r on col.Account = r.Account
        //    inner join resident as re on col.ID = re.ID
        //    where 
        //    orderby 

        //    select new VmCollector
        //    {
        //        Account = Resident.Account,
        //        ID = Resident.ID,
        //        AccountName = Collector.Account,
        //        IDName = Collector.ID,
        //    }
        //    );

        //    return View(vmCollectors);
        //}

        [ActionName("Serch")]
        public ActionResult Index(string option, string search, int page = 1)
        {
            var data = db.Resident;
            var collectorData = db.Collector.ToList();

            if (option == "id")
            {
                var personAccount = db.Resident.Where(r => r.ID == search).FirstOrDefault().Account;
                collectorData = db.Collector.Where(c => c.Account == personAccount || personAccount == null).ToList();
            }
            else if (option == "account")
            {
                collectorData = db.Collector.Where(c => c.Account == search || search == null).ToList();
            }
            else
            {
                //collectorData = collectorData;
            }

            foreach (var item in collectorData)
            {
                item.Resident.Account = data.Where(d => d.Account == item.Account).FirstOrDefault().Name;
                item.Resident.ID = data.Where(d => d.ID == item.ID).FirstOrDefault().Name;
            }

            int pageSize = 8;
            int currentPage = page < 1 ? 1 : page;
            var pagedCust = collectorData.ToPagedList(currentPage, pageSize);
            return View("Index", pagedCust);

        }

        // GET: Collectors/Create
        public ActionResult Create()
        {
            ViewBag.Account = new SelectList(db.Resident, "Account", "Name", db.Resident);
            ViewBag.ID = new SelectList(db.Resident, "ID", "Name", db.Resident);
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

            ViewBag.Account = new SelectList(db.Resident, "Account", "Name", collector.Account);
            ViewBag.ID = new SelectList(db.Resident, "ID", "Name", collector.ID);
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
            ViewBag.Account = new SelectList(db.Resident, "Account", "Name");
            ViewBag.ID = new SelectList(db.Resident, "ID", "Name");
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

            collector.Resident.Account = db.Resident.Find(account).Name;
            var collectorName = db.Resident.Where(r => r.ID == collector.ID).FirstOrDefault().Name;
            
            collector.Resident.ID = collectorName;

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
