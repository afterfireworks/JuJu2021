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
    public class PackagesController : ApiController
    {
        JuJuLocaldbEntities db = new JuJuLocaldbEntities();

        // GET: api/Packages
        //public IQueryable<Package> GetPackage()
        //{
        //    return db.Package;
        //}

        // GET: api/Packages/5
        [HttpGet]
        public ArrayList GetPackage(string userAccount)
        {
            ArrayList PackagesData = new ArrayList();
            try
            {
                var data = db.Package.Where(p => p.Account == userAccount).ToList();

                if (data != null)
                {
                    foreach (var item in data)
                    {
                        var collector = db.Resident.Where(r => r.ID == item.Signer).FirstOrDefault();
                        
                        //object Account = userAccount;
                        object SN = item.SN;
                        object ArrivalDate = item.ArrivalDate;
                        object Sign = item.Sign ? "包裹已領取" : "包裹未領取";
                        object Signer;

                        if (collector != null)
                        {
                            Signer = collector.Name;
                        }
                        else { Signer = item.Signer; }

                        object SignDate = item.SignDate;
                        object COD = item.COD;

                        Object PackageRow = new { SN, ArrivalDate, Sign, Signer, SignDate, COD };
                        PackagesData.Add(PackageRow);
                    }

                    //object errorMessages = "Success";
                    //Object ErrorMessages = new { errorMessages };
                    //CollectorData.Add(info);
                }

                else
                {
                    object errorMessages = "Error";
                    Object ErrorMessages = new { errorMessages };
                    PackagesData.Add(ErrorMessages);
                }
            }
            catch
            { }

            return PackagesData;
        }

        //[ResponseType(typeof(Package))]
        //public IHttpActionResult GetPackage(DateTime date)
        //{
        //    var package = db.Package.Where(p => p.ArrivalDate == date).ToList();
        //    if (package == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(package);
        //}

        [HttpGet] //ok 穆
        [ResponseType(typeof(bool))]
        public IHttpActionResult PickPackage(long sn, string userAccount, string recipient)
        {
            var thisPackage = db.Package.Where(p => p.SN == sn).FirstOrDefault();
            var canPicker = db.Collector.Where(c => c.Account == userAccount).ToList();
            var pickmanID = db.Resident.Find(recipient).ID;
            bool isSigned = false;
            DateTime TimeNow = DateTime.Now;

            if (thisPackage.Account != recipient && canPicker.Where(c => c.ID == pickmanID).FirstOrDefault()==null)
            {
                return NotFound();
            }
            else
            {
                thisPackage.SN = thisPackage.SN;
                thisPackage.Account = thisPackage.Account;
                thisPackage.ArrivalDate = thisPackage.ArrivalDate;
                thisPackage.Sign = true;
                thisPackage.SignDate = TimeNow;
                thisPackage.COD = thisPackage.COD;
                thisPackage.Signer = pickmanID;

                db.Entry(thisPackage).State = EntityState.Modified;
                db.SaveChanges();
                isSigned = true;
            }
            return Ok(isSigned);
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