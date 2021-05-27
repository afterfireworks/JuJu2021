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
    public class PackagesController : ApiController
    {
        private JuJuLocalApiEntities db = new JuJuLocalApiEntities();

        // GET: api/Packages
        public IQueryable<Package> GetPackage()
        {
            return db.Package;
        }

        // GET: api/Packages/5
        [ResponseType(typeof(Package))]
        public IHttpActionResult GetPackage(string userAccount)
        {
            var package = db.Package.Where(p=>p.Account== userAccount).ToList();
            if (package == null)
            {
                return NotFound();
            }

            return Ok(package);
        }

        [ResponseType(typeof(Package))]
        public IHttpActionResult GetPackage(bool picked)
        {
            var package = db.Package.Where(p => p.Sign == picked).ToList();
            if (package == null)
            {
                return NotFound();
            }

            return Ok(package);
        }

        [ResponseType(typeof(Package))]
        public IHttpActionResult GetPackage(DateTime date)
        {
            var package = db.Package.Where(p => p.ArrivalDate == date).ToList();
            if (package == null)
            {
                return NotFound();
            }

            return Ok(package);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult PickPackage(long sn, string person, string picker)
        {
            var thisPackage = db.Package.Where(p => p.SN == sn).FirstOrDefault();
            var canPicker = db.Collector.Where(c => c.Account == person).FirstOrDefault().ID;

            Package pakeage = new Package();

            if (thisPackage.Account != picker && canPicker != picker)
            {
                //之後改自訂頁面
                return "true";
            }
            else
            {
                pakeage.SN = thisPackage.SN;
                pakeage.Account = thisPackage.Account;
                pakeage.ArrivalDate = thisPackage.ArrivalDate;
                pakeage.Sign = true;
                pakeage.SignDate = thisPackage.SignDate;
                pakeage.COD = thisPackage.COD;
                pakeage.Signer = picker;

                db.Package.Add(pakeage);
                db.SaveChanges();

                //之後改自訂頁面
                //return HttpNotFound();
            }
        }

        ////////////////////////////////////////使用者只須抓取資料 ////////////////////////////////////////

            // PUT: api/Packages/5
            //[ResponseType(typeof(void))]
            //public IHttpActionResult PutPackage(long id, Package package)
            //{
            //    if (!ModelState.IsValid)
            //    {
            //        return BadRequest(ModelState);
            //    }

            //    if (id != package.SN)
            //    {
            //        return BadRequest();
            //    }

            //    db.Entry(package).State = EntityState.Modified;

            //    try
            //    {
            //        db.SaveChanges();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!PackageExists(id))
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

            // POST: api/Packages
            //[ResponseType(typeof(Package))]
            //public IHttpActionResult PostPackage(Package package)
            //{
            //    if (!ModelState.IsValid)
            //    {
            //        return BadRequest(ModelState);
            //    }

            //    db.Package.Add(package);
            //    db.SaveChanges();

            //    return CreatedAtRoute("DefaultApi", new { id = package.SN }, package);
            //}

            // DELETE: api/Packages/5
            //[ResponseType(typeof(Package))]
            //public IHttpActionResult DeletePackage(long id)
            //{
            //    Package package = db.Package.Find(id);
            //    if (package == null)
            //    {
            //        return NotFound();
            //    }

            //    db.Package.Remove(package);
            //    db.SaveChanges();

            //    return Ok(package);
            //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PackageExists(long sn)
        {
            return db.Package.Count(e => e.SN == sn) > 0;
        }
    }
}