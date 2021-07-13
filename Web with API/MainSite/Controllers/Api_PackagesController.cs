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
using MainSite.Models;

namespace MainSite.Controllers
{
    public class Api_PackagesController : ApiController
    {
        private JuJuLocaldbEntities db = new JuJuLocaldbEntities();

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
                        var collector = db.Resident.Where(r => r.Account == item.Signer).FirstOrDefault();

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

        [HttpGet] //ok 穆
        [ResponseType(typeof(bool))]
        public IHttpActionResult PickPackage(long sn, string userAccount, string recipient)
        {
            var thisPackage = db.Package.Where(p => p.SN == sn).FirstOrDefault();
            var canPicker = db.Collector.Where(c => c.Account == userAccount).ToList();
            var pickman = db.Resident.Find(recipient);
            bool isSigned = false;
            DateTime TimeNow = DateTime.Now;

            if (thisPackage.Account != recipient && canPicker.Where(c => c.ID == pickman.ID).FirstOrDefault() == null)
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
                thisPackage.Signer = pickman.Account;

                db.Entry(thisPackage).State = EntityState.Modified;
                db.SaveChanges();
                isSigned = true;
            }
            return Ok(isSigned);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PackageExists(long id)
        {
            return db.Package.Count(e => e.SN == id) > 0;
        }
    }
}