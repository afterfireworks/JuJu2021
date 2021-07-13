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
using MainSite.Models;

namespace MainSite.Controllers
{
    public class Api_ComplaintsController : ApiController
    {
        private JuJuLocaldbEntities db = new JuJuLocaldbEntities();

        // GET: api/Complaints/5
        [ResponseType(typeof(Complaint))]
        public IHttpActionResult GetComplaint(string userAccount)
        {
            var complaint = db.Complaint.Where(c => c.Account == userAccount).ToList();
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

            //return CreatedAtRoute("DefaultApi", new { sn = complaint.SN }, complaint);
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

        private bool ComplaintExists(long id)
        {
            return db.Complaint.Count(e => e.SN == id) > 0;
        }
    }
}