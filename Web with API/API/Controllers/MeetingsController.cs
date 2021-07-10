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
    public class MeetingsController : ApiController
    {
        JuJuLocaldbEntities db = new JuJuLocaldbEntities();

        // GET: api/Meetings
        //public IQueryable<Meeting> GetMeeting()
        //{
        //    return db.Meeting;
        //}

        //var serch = db.Meeting.Where(m => m.Date.Equals(date)).ToList();

        // GET: api/Meetings/5

        [HttpGet]
        public ArrayList GetMeeting()
        {
            ArrayList MeetingData = new ArrayList();
            try
            {
                var data = db.Meeting.ToList();

                if (data != null)
                {
                    foreach (var item in data)
                    {
                        object SN = item.SN;
                        object Date = item.Date;
                        object Title = item.Title;
                        object URL = item.URL;
                        
                        Object MeetingRow = new { SN, Date, Title,URL};
                        MeetingData.Add(MeetingRow);
                    }
                }
                else
                {
                    object errorMessages = "Error";
                    Object ErrorMessages = new { errorMessages };
                    MeetingData.Add(ErrorMessages);
                }
            }
            catch
            { }

            return MeetingData;
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