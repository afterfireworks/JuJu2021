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
    public class ComplaintsController : ApiController
    {
        private JuJuLocalApiEntities db = new JuJuLocalApiEntities();

        // GET: api/Complaints
        public IQueryable<Complaint> GetComplaint()
        {
            return db.Complaint;
        }

        // GET: api/Complaints/5
        [ResponseType(typeof(Complaint))]
        public IHttpActionResult GetComplaint(long id)
        {
            Complaint complaint = db.Complaint.Find(id);
            if (complaint == null)
            {
                return NotFound();
            }

            return Ok(complaint);
        }

        // POST: api/Complaints
        [ResponseType(typeof(Complaint))]
        public IHttpActionResult PostComplaint(Complaint complaint)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Complaint.Add(complaint);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { sn = complaint.SN }, complaint);
        }

        // DELETE: api/Complaints/5
        [ResponseType(typeof(Complaint))]
        public IHttpActionResult DeleteComplaint(long sn)
        {
            Complaint complaint = db.Complaint.Find(sn);
            if (complaint == null)
            {
                return NotFound();
            }

            db.Complaint.Remove(complaint);
            db.SaveChanges();

            return Ok(complaint);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ComplaintExists(long sn)
        {
            return db.Complaint.Count(e => e.SN == sn) > 0;
        }
    }
}