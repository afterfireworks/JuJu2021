using System;
using System.Collections;
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
        private JuJuLocaldbEntities db = new JuJuLocaldbEntities();

        // GET: api/Residents
        //public IQueryable<Resident> GetResident()
        //{
        //    return db.Resident;
        //}

        // GET: api/Residents/5
        [HttpGet]
        public ArrayList GetResident(string userAccount)
        {
            ArrayList ResidentData = new ArrayList();
            try
            {
                var data = from u in db.Resident
                           where u.Account == userAccount 
                           select u;

                if (data.FirstOrDefault() != null)
                {
                    object Account = data.FirstOrDefault().Account;
                    object ID = data.FirstOrDefault().ID;
                    object Name = data.FirstOrDefault().Name;
                    object Tel = data.FirstOrDefault().Tel;
                    object Address = data.FirstOrDefault().Address;
                    object Photo = data.FirstOrDefault().Photo;
                    object errorMessages = "Success";

                    Object ResidentRow = new { Account, ID, Name , Tel, Address, Photo, errorMessages };
                    ResidentData.Add(ResidentRow);
                }

                else
                {
                    object errorMessages = "Error";
                    Object ErrorMessages = new {  errorMessages };
                    ResidentData.Add(ErrorMessages);
                }
            }
            catch
            { }

            return ResidentData;
        }

        // PUT: api/Residents/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutResident(string id, Resident resident)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != resident.Account)
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
        //        if (!ResidentExists(id))
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
        //[ResponseType(typeof(Resident))]
        //public IHttpActionResult PostResident(Resident resident)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Resident.Add(resident);

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (ResidentExists(resident.Account))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtRoute("DefaultApi", new { id = resident.Account }, resident);
        //}

        // DELETE: api/Residents/5
        //[ResponseType(typeof(Resident))]
        //public IHttpActionResult DeleteResident(string id)
        //{
        //    Resident resident = db.Resident.Find(id);
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

        private bool ResidentExists(string id)
        {
            return db.Resident.Count(e => e.Account == id) > 0;
        }
    }
}