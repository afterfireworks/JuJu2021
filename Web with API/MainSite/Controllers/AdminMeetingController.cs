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
    public class AdminMeetingController : Controller
    {
        JuJuLocaldbEntities db = new JuJuLocaldbEntities();

        //GET: AdminMeeting
        //public ActionResult Index()
        //{
        //    return View(db.Meeting.ToList());
        //}
        public ActionResult Index(int page = 1)
        {
            var meetingData = db.Meeting.ToList();
            int pageSize = 10;
            int currentPage = page < 1 ? 1 : page;
            var pagedCust = meetingData.ToPagedList(currentPage, pageSize);

            return View(pagedCust);
            //return View(residenttData);
        }




        // GET: AdminMeeting/Details/5
        //public ActionResult Details(long? sn)
        //{
        //    if (sn == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Meeting meeting = db.Meeting.Find(sn);
        //    if (meeting == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(meeting);
        //}

        // GET: AdminMeeting/Create
        public ActionResult Create()
        {
            ViewBag.ChairmanAccount = new SelectList(db.Chairman, "ChairmanAccount", "ChairmanAccount");
            return View();
        }

        // POST: AdminMeeting/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SN,Date,Title,ChairmanAccount,URL")] Meeting meeting)
        {
            if (ModelState.IsValid)
            {
                db.Meeting.Add(meeting);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ChairmanAccount = new SelectList(db.Chairman, "ChairmanAccount", "ChairmanAccount");
            return View(meeting);
        }

        // GET: AdminMeeting/Edit/5
        public ActionResult Edit(long? sn)
        {
            if (sn == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meeting meeting = db.Meeting.Find(sn);
            if (meeting == null)
            {
                return HttpNotFound();
            }
            ViewBag.ChairmanAccount = new SelectList(db.Chairman, "ChairmanAccount", "ChairmanAccount");
            return View(meeting);
        }

        // POST: AdminMeeting/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SN,ChairmanAccount,Date,Title,URL")] Meeting meeting)
        {
            if (ModelState.IsValid)
            {
                db.Entry(meeting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ChairmanAccount = new SelectList(db.Chairman, "ChairmanAccount", "ChairmanAccount");
            return View(meeting);
        }

        // GET: AdminMeeting/Delete/5
        public ActionResult Delete(long? sn)
        {
            if (sn == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meeting meeting = db.Meeting.Find(sn);
            if (meeting == null)
            {
                return HttpNotFound();
            }
            return View(meeting);
        }

        // POST: AdminMeeting/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long sn)
        {
            Meeting meeting = db.Meeting.Find(sn);
            db.Meeting.Remove(meeting);
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
