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
    public class ResidentController : Controller
    {
        JuJuLocaldbEntities db = new JuJuLocaldbEntities();

        //GET: Residents
        public ActionResult Index(int page = 1)
        {
            var resident = db.Resident.ToList();
            int pageSize = 5;
            int currentPage = page < 1 ? 1 : page;
            var pagedCust = resident.ToPagedList(currentPage, pageSize);

            return View(pagedCust);
        }

        //ActionResult
        [ActionName("Serch")]
        public ActionResult Index(string option, string search, int page = 1)
        {
            var resident = db.Resident.ToList();
            int pageSize = 6;
            int currentPage = page < 1 ? 1 : page;
            
            //if a user choose the radio button option as Subject

            if (option == "id")
            {
                resident = db.Resident.Where(r => r.ID == search || search == null).ToList();
            }
            else if (option == "account")
            {
                resident = db.Resident.Where(r => r.Account == search || search == null).ToList();
            }
            else if (option == "name")
            {
                resident = db.Resident.Where(r => r.Name.StartsWith(search) || search == null).ToList();
            }
            else
            {
                resident = db.Resident.ToList();
            }

            var pagedCust = resident.ToPagedList(currentPage, pageSize);
            return View("Index", pagedCust);
        }

        public FileContentResult GetImage(string Account)
        {
            var person = db.Resident.Find(Account);
            return File(person.Photo, "image/jpeg");

        }
       
        //GET: Residents/Details/5
        //public ActionResult Details(string account)
        //{
        //    if (account == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Resident resident = db.Resident.Find(account);
        //    if (resident == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(resident);
        //}


        /////////////////////管理員只能檢視/////////////////////


        // GET: Residents/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Residents/Create
        //// 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        //// 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Account,Password,ID,Name,Tel,Address,Photo,Committee")] Resident resident)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Resident.Add(resident);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(resident);
        //}

        //// GET: Residents/Edit/5
        //public ActionResult Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Resident resident = db.Resident.Find(id);
        //    if (resident == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(resident);
        //}

        //// POST: Residents/Edit/5
        //// 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        //// 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Account,Password,ID,Name,Tel,Address,Photo,Committee")] Resident resident)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(resident).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(resident);
        //}

        //// GET: Residents/Delete/5
        //public ActionResult Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Resident resident = db.Resident.Find(id);
        //    if (resident == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(resident);
        //}

        //// POST: Residents/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(string id)
        //{
        //    Resident resident = db.Resident.Find(id);
        //    db.Resident.Remove(resident);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
