using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using API.Models;

namespace API.Controllers
{
    public class AnnouncementsController : ApiController
    {
        public JuJuLocaldbEntities db = new JuJuLocaldbEntities();

        // GET: api/Announcements
        //public IQueryable<Announcement> GetAnnouncement()
        //{
        //    return db.Announcement;
        //}

        // GET: api/Announcements/5
        [HttpGet]
        public ArrayList GetAnnouncements()
        {
            ArrayList AnnouncementData = new ArrayList();
            try
            {
                DateTime currenteDate = DateTime.UtcNow.Date.AddDays(-30);
                var data = db.Announcement.Where(a => a.Date >= currenteDate).ToList();

                if (data != null)
                {
                    foreach (var item in data)
                    {
                        object SN = item.SN;
                        object Date = item.Date;
                        object Category = item.Category;
                        object Title = item.Title;
                        object Description = item.Description;
                        object Picture = item.Picture;


                        Object AnnouncementRow = new { SN, Date, Category, Title, Description, Picture };
                        AnnouncementData.Add(AnnouncementRow);
                    }
                }
                else
                {
                    object errorMessages = "Error";
                    Object ErrorMessages = new { errorMessages };
                    AnnouncementData.Add(ErrorMessages);
                }
            }
            catch
            { }

            return AnnouncementData;
        }

        // PUT: api/Announcements/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutAnnouncement(long id, Announcement announcement)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != announcement.SN)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(announcement).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!AnnouncementExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/Announcements
        //[ResponseType(typeof(Announcement))]
        //public IHttpActionResult PostAnnouncement(Announcement announcement)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Announcement.Add(announcement);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = announcement.SN }, announcement);
        //}

        //// DELETE: api/Announcements/5
        //[ResponseType(typeof(Announcement))]
        //public IHttpActionResult DeleteAnnouncement(long id)
        //{
        //    Announcement announcement = db.Announcement.Find(id);
        //    if (announcement == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Announcement.Remove(announcement);
        //    db.SaveChanges();

        //    return Ok(announcement);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AnnouncementExists(long id)
        {
            return db.Announcement.Count(e => e.SN == id) > 0;
        }
    }
}