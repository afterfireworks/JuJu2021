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
    [CheckLoginState]
    public class AdminResidentController : Controller
    {
        JuJuLocaldbEntities db = new JuJuLocaldbEntities();

        public ActionResult Index(int page = 1)
        {
            var residenttData = db.Resident.ToList();
            int pageSize = 5;
            int currentPage = page < 1 ? 1 : page;
            var pagedCust = residenttData.ToPagedList(currentPage, pageSize);

            return View(pagedCust);
        }

        [ActionName("Serch")]
        public ActionResult Index(string option, string search, int page = 1)
        {
            var resident = db.Resident.ToList();
            int pageSize = 5;
            int currentPage = page < 1 ? 1 : page;

            //if a user choose the radio button option as Subject

            if (option == "account")
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


        // GET: AdminResident/Details/5
        //public ActionResult Details(string id)
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

        // GET: AdminResident/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminResident/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Account,Password,ID,Name,Tel,Address,Photo,Committee")] Resident resident, HttpPostedFileBase image)
        {
            if (image != null)
            {
                resident.Photo = new byte[image.ContentLength];
                image.InputStream.Read(resident.Photo, 0, image.ContentLength);
            }

            if (ModelState.IsValid)
            {
                
                db.Resident.Add(resident);
                db.SaveChanges();

                return RedirectToAction("Index");
            }


            return View(resident);
        }

        // GET: AdminResident/Edit/5
        public ActionResult Edit(string Account)
        {
            if (Account == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Resident resident = db.Resident.Find(Account);

            if (resident == null)
            {
                return HttpNotFound();
            }

            TempData["oldPhoto"] = resident.Photo;

            return View(resident);
        }

        // POST: AdminResident/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Account,Password,ID,Name,Tel,Address,Photo,Committee")] Resident resident, HttpPostedFileBase image)
        {
            ModelState.Remove("Account");

            if (image != null)
            {
                resident.Photo = new byte[image.ContentLength];
                image.InputStream.Read(resident.Photo, 0, image.ContentLength);
            }
            else
            {
                resident.Photo = (byte[])TempData["oldPhoto"];
            }

            if (ModelState.IsValid)
            {
                db.Entry(resident).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(resident);
        }

        // GET: AdminResident/Delete/5
        public ActionResult Delete(string Account)
        {
            if (Account == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resident resident = db.Resident.Find(Account);
            if (resident == null)
            {
                return HttpNotFound();
            }
            return View(resident);
        }

        // POST: AdminResident/Delete/5
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string Account)
        {
            Resident resident = db.Resident.Find(Account);
            db.Resident.Remove(resident);
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
