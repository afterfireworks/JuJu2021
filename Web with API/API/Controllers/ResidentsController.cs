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
    public class ResidentsController : ApiController
    {
        private JuJuLocalApiEntities db = new JuJuLocalApiEntities();

        // GET: api/Residents
        public IQueryable<Resident> GetResident()
        {
            return db.Resident;
        }

        // GET: api/Residents/5
        [ResponseType(typeof(Resident))]
        public IHttpActionResult GetResident(string uesrAccount)
        {
            Resident resident = db.Resident.Find(uesrAccount);
            if (resident == null)
            {
                return NotFound();
            }

            return Ok(resident);
        }

        // PUT: api/Residents/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutResident(string uesrAccount, Resident resident)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (uesrAccount != resident.Account)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(resident).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ResidentExists(uesrAccount))
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

        // POST: api/Residents
        [ResponseType(typeof(Resident))]
        public IHttpActionResult PostResident(Resident resident)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Resident.Add(resident);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ResidentExists(resident.Account))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { uesrAccount = resident.Account }, resident);
        }

        // DELETE: api/Residents/5
        //[ResponseType(typeof(Resident))]
        //public IHttpActionResult DeleteResident(string uesrAccount)
        //{
        //    Resident resident = db.Resident.Find(uesrAccount);
        //    if (resident == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Resident.Remove(resident);
        //    db.SaveChanges();

        //    return Ok(resident);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ResidentExists(string uesrAccount)
        {
            return db.Resident.Count(e => e.Account == uesrAccount) > 0;
        }
    }
}