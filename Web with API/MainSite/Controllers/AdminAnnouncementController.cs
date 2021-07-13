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
    public class AdminAnnouncementController : Controller
    {  
        JuJuLocaldbEntities db = new JuJuLocaldbEntities();

        public ActionResult Index(int page = 1)
        {
            var announcement = db.Announcement.Include(a => a.Chairman).ToList();
            int pageSize = 6;
            int currentPage = page < 1 ? 1 : page;
            var pagedCust = announcement.ToPagedList(currentPage, pageSize);

            return View(pagedCust);
        }

        public FileContentResult GetImage(long? sn, DateTime date)
        {
            var announcement = db.Announcement.Where(a => a.SN == sn && a.Date == date).FirstOrDefault();

            return File(announcement.Picture, "image/*");
        }

        // GET: AdminAnnouncements/Details/5
        public ActionResult Details(long? sn, DateTime date)
        {
            if (sn == null || date == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Announcement announcement = db.Announcement.Where(a => a.SN == sn && a.Date == date).Include(a=>a.Chairman).FirstOrDefault();
            if (announcement == null)
            {
                return HttpNotFound();
            }
            return View(announcement);
        }

        // GET: AdminAnnouncements/Create
        public ActionResult Create()
        {
            ViewBag.ChairmanAccount = new SelectList(db.Chairman, "ChairmanAccount", "ChairmanAccount");
            return View();
        }

        // POST: AdminAnnouncements/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SN,Date,ChairmanAccount,Category,Title,Description,Picture")] Announcement announcement, HttpPostedFileBase image)
        {
            if (image != null) 
            {
                announcement.Picture = new byte[image.ContentLength];
                image.InputStream.Read(announcement.Picture, 0, image.ContentLength);
            }

            if (ModelState.IsValid)
            {
                db.Announcement.Add(announcement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ChairmanAccount = new SelectList(db.Chairman, "ChairmanAccount", "ChairmanAccount", announcement.ChairmanAccount);
            return View(announcement);
        }

        // GET: AdminAnnouncements/Edit/5
        public ActionResult Edit(long? sn,DateTime date)
        {
            if (sn == null|| date == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Announcement announcement = db.Announcement.Where(a => a.SN == sn && a.Date == date).FirstOrDefault();
            
            if (announcement == null)
            {
                return HttpNotFound();
            }
            TempData["oldPicture"] = announcement.Picture;
            ViewBag.ChairmanAccount = new SelectList(db.Chairman, "ChairmanAccount", "Account", announcement.ChairmanAccount);
            return View(announcement);
        }

        // POST: AdminAnnouncements/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SN,Date,ChairmanAccount,Category,Title,Description")] Announcement announcement, HttpPostedFileBase image)
        {

            if (image != null)
            {
                announcement.Picture = new byte[image.ContentLength];
                image.InputStream.Read(announcement.Picture, 0, image.ContentLength);
            }
            else 
            { 
                announcement.Picture = (byte[])TempData["oldPicture"];
            }

            if (ModelState.IsValid)
            {
                db.Entry(announcement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ChairmanAccount = new SelectList(db.Chairman, "ChairmanAccount", "Account", announcement.ChairmanAccount);
            return View(announcement);
        }

        // GET: AdminAnnouncements/Delete/5
        public ActionResult Delete(long? sn, DateTime date)
        {
            if (sn == null || date == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Announcement announcement = db.Announcement.Where(a => a.SN == sn && a.Date == date).FirstOrDefault();
            if (announcement == null)
            {
                return HttpNotFound();
            }
            return View(announcement);
        }

        // POST: AdminAnnouncements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long? sn, DateTime date)
        {
            Announcement announcement = db.Announcement.Where(a => a.SN == sn && a.Date == date).FirstOrDefault();
            db.Announcement.Remove(announcement);
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
