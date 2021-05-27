using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MainSite.Models;

namespace MainSite.Controllers
{
    
    public class AdminResidentController : Controller
    {
        //JuJu2021Entities db = new JuJu2021Entities();
        JuJuLocalEntities db = new JuJuLocalEntities();

        // GET: Resident
        public ActionResult Index()
        {
            var residenttData = db.Resident.ToList();
            return View(residenttData);
        }

        public FileContentResult GetImage(String Account)
        {
            var person = db.Resident.Find(Account);

            return File(person.Photo, "image/jpeg");

        }

        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Resident resident, HttpPostedFileBase image)
        {
            if (image != null)
            {
                //resident.ImageMimeType = image.ContentType;
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
            return View(resident);
        }

        //POST: Customers/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]

        //[Bind(Include = "CustomerID,CompanyName,ContactName,ContactTitle,Address,City,Region,PostalCode,Country,Phone,Fax")]
        public ActionResult Edit(Resident resident, HttpPostedFileBase image, string Account)
        {
            //var tepPhoto = db.Resident.Where(m => m.Account == Account).FirstOrDefault().Photo;
          
            ModelState.Remove("Account");
            if (image == null)
            {
                //resident.Photo = tepPhoto;
            }
            else
            {
 //resident.ImageMimeType = image.ContentType;
                resident.Photo = new byte[image.ContentLength];
                image.InputStream.Read(resident.Photo, 0, image.ContentLength); 
            }


            if (ModelState.IsValid)
            {

                db.Entry(resident).State = EntityState.Modified;
                db.SaveChanges();

                //string sql = "UPDATE Customers SET ";
                //sql += " [CompanyName] = '" + resident.ID;
                //sql += " ' ,[ContactName] = '" + resident.Password;
                //sql += " ' ,[ContactTitle] = '" + resident.Tel;
                //sql += " ' ,[Address] = '" + resident.Address;
                //sql += " ' ,[City] = '" + resident.Photo;
                //sql += "  ' ,[Region] = '" + resident.Committee;
                //sql += "  ',[PostalCode] = '" + resident.Name;
                //sql += "'  WHERE Account = '" + resident.Account + "'";

                //dc.executeSql(sql);



                return RedirectToAction("Index");
            }
            return View(resident);
        }

        public ActionResult Delete(string Account)
        {
            if (Account == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var person = db.Resident.Find(Account);

            if (person == null)
            {
                return HttpNotFound();

            }
            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string Account)
        {
            var person = db.Resident.Find(Account);
            db.Resident.Remove(person);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}