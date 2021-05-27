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
    public class ResidentController : Controller
    {
        private JuJuLocalEntities db = new JuJuLocalEntities();

        // GET: Residents
        public ActionResult Index()
        {
            var resident = db.Resident.Include(c => c.Collector);
            return View(resident.ToList());
        }

        [ActionName("Serch")]
        public ActionResult Index(string option, string search)
        {
            var data = db.Resident;
            //if a user choose the radio button option as Subject  
            if (option == "id")
            {
                //Index action method will return a view with a student records based on what a user specify the value in textbox  
                return View("Index", data.Where(r => r.ID == search || search == null).ToList());
            }
            else if (option == "account")
            {
                return View("Index", data.Where(r => r.Account == search || search == null).ToList());
            }
            else if (option == "name")
            {
                return View("Index", data.Where(r => r.Name.StartsWith(search) || search == null).ToList());
            }
            else
            {
                return View("Index", data.ToList());
            }
        }

        public FileContentResult GetImage(String Account)
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
