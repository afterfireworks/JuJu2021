using System;
using System.Collections.Generic;
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
    public class MeetingsController : ApiController
    {
        private JuJuLocalApiEntities db = new JuJuLocalApiEntities();

        // GET: api/Meetings
        public IQueryable<Meeting> GetMeeting()
        {
            return db.Meeting.Include(m=>m.MeetingDetails);
        }

        // GET: api/Meetings/5
        [ResponseType(typeof(Meeting))]
        public IHttpActionResult GetMeeting(String userAccount)
        {
            var serch = db.MeetingDetails.Where(m => m.Account == userAccount).FirstOrDefault();
            
            //Meeting meeting = (Meeting)db.Meeting.Include(m => m.MeetingDetails);

            if (serch == null)
            {
                return NotFound();
            }

            return Ok(serch);
        }

        public IHttpActionResult GetMeeting(DateTime date)
        {
            var serch = db.Meeting.Where(m => m.Date.Equals(date)).ToList();


            if (serch == null)
            {
                return NotFound();
            }

            return Ok(serch);
        }

        ////////////////////////////////////////使用者只須抓取資料 ////////////////////////////////////////

        // PUT: api/Meetings/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutMeeting(long id, Meeting meeting)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != meeting.SN)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(meeting).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!MeetingExists(id))
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

        // POST: api/Meetings
        //[ResponseType(typeof(Meeting))]
        //public IHttpActionResult PostMeeting(Meeting meeting)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Meeting.Add(meeting);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = meeting.SN }, meeting);
        //}

        // DELETE: api/Meetings/5
        //[ResponseType(typeof(Meeting))]
        //public IHttpActionResult DeleteMeeting(long id)
        //{
        //    Meeting meeting = db.Meeting.Find(id);
        //    if (meeting == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Meeting.Remove(meeting);
        //    db.SaveChanges();

        //    return Ok(meeting);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MeetingExists(long id)
        {
            return db.Meeting.Count(e => e.SN == id) > 0;
        }
    }
}