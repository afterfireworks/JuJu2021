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
    public class CollectorsController : ApiController
    {
        JuJuLocaldbEntities db = new JuJuLocaldbEntities();

        // GET: api/Collectors
        //public IQueryable<Collector> GetCollector()
        //{
        //    return db.Collector;
        //}

        // GET: api/Collectors/5
        [HttpGet]
        public ArrayList GetCollector(string userAccount)
        {
            ArrayList CollectorData = new ArrayList();
            try
            {
                var data = from u in db.Collector
                           where u.Account == userAccount
                           select u;

                if (data.ToList() != null)
                {
                    foreach(var item in data)
                    {
                        object SN = item.SN;
                        object CollectorName = db.Resident.Where(r => r.ID == item.ID).FirstOrDefault().Name;
                    

                    Object CollectorRow = new { SN, CollectorName };
                    CollectorData.Add(CollectorRow);
                    }

                    //object errorMessages = "Success";
                    //Object ErrorMessages = new { errorMessages };
                    //CollectorData.Add(info);
                }

                else
                {
                    object errorMessages = "Error";
                    Object ErrorMessages = new { errorMessages };
                    CollectorData.Add(ErrorMessages);
                }
            }
            catch
            { }

            return CollectorData;
        }

        // PUT: api/Collectors/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutCollector(string id, Collector collector)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != collector.ID)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(collector).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CollectorExists(id))
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

        // POST: api/Collectors
        //[ResponseType(typeof(Collector))]
        //public IHttpActionResult PostCollector(Collector collector)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Collector.Add(collector);

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (CollectorExists(collector.ID))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtRoute("DefaultApi", new { id = collector.ID }, collector);
        //}

        // DELETE: api/Collectors/5
        //[ResponseType(typeof(Collector))]
        //public IHttpActionResult DeleteCollector(string id)
        //{
        //    Collector collector = db.Collector.Find(id);
        //    if (collector == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Collector.Remove(collector);
        //    db.SaveChanges();

        //    return Ok(collector);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CollectorExists(string id)
        {
            return db.Collector.Count(e => e.ID == id) > 0;
        }
    }
}