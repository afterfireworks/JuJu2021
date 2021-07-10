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
    public class ReturnOfGoodsController : ApiController
    {
        JuJuLocaldbEntities db = new JuJuLocaldbEntities();

        // GET: api/ReturnOfGoods
        //public IQueryable<ReturnOfGoods> GetReturnOfGoods()
        //{
        //    return db.ReturnOfGoods;
        //}

        // GET: api/ReturnOfGoods/5
        [HttpGet]
        public ArrayList GetReturnOfGoods(string userAccount)
        {
            ArrayList ReturnOfGoodsData = new ArrayList();
            try
            {
                var data = from u in db.ReturnOfGoods
                           where u.Account == userAccount
                           select u;

                if (data.ToList() != null)
                {
                    foreach (var item in data)
                    {
                        //object Account = userAccount;
                        object SN = item.SN;
                        object ReceiptDate = item.ReceiptDate;
                        object LogisticsCompany = item.LogisticsCompany;
                        object Sign = item.Sign? "貨運已領取": "貨運未領取";
                        object CourierSign = item.CourierSign;


                        Object ReturnOfGoodsRow = new { SN, ReceiptDate, LogisticsCompany, Sign, CourierSign };
                        ReturnOfGoodsData.Add(ReturnOfGoodsRow);
                    }

                    //object errorMessages = "Success";
                    //Object ErrorMessages = new { errorMessages };
                    //CollectorData.Add(info);
                }

                else
                {
                    object errorMessages = "Error";
                    Object ErrorMessages = new { errorMessages };
                    ReturnOfGoodsData.Add(ErrorMessages);
                }
            }
            catch
            { }

            return ReturnOfGoodsData;
        }

        [ResponseType(typeof(ReturnOfGoods))]
        public IHttpActionResult GetReturnOfGoods(bool picked)
        {
            var returnOfGoods = db.ReturnOfGoods.Where(r => r.Sign == picked).ToList();
            if (returnOfGoods == null)
            {
                return NotFound();
            }

            return Ok(returnOfGoods);
        }



        ////////////////////////////////////////使用者只須抓取資料 ////////////////////////////////////////

        // PUT: api/ReturnOfGoods/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutReturnOfGoods(long id, ReturnOfGoods returnOfGoods)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != returnOfGoods.SN)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(returnOfGoods).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ReturnOfGoodsExists(id))
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

        // POST: api/ReturnOfGoods
        ////[ResponseType(typeof(ReturnOfGoods))]
        ////public IHttpActionResult PostReturnOfGoods(ReturnOfGoods returnOfGoods)
        ////{
        ////    if (!ModelState.IsValid)
        ////    {
        ////        return BadRequest(ModelState);
        ////    }

        ////    db.ReturnOfGoods.Add(returnOfGoods);
        ////    db.SaveChanges();

        ////    return CreatedAtRoute("DefaultApi", new { id = returnOfGoods.SN }, returnOfGoods);
        ////}

        // DELETE: api/ReturnOfGoods/5
        //[ResponseType(typeof(ReturnOfGoods))]
        //public IHttpActionResult DeleteReturnOfGoods(long id)
        //{
        //    ReturnOfGoods returnOfGoods = db.ReturnOfGoods.Find(id);
        //    if (returnOfGoods == null)
        //    {
        //        return NotFound();
        //    }

        //    db.ReturnOfGoods.Remove(returnOfGoods);
        //    db.SaveChanges();

        //    return Ok(returnOfGoods);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReturnOfGoodsExists(long id)
        {
            return db.ReturnOfGoods.Count(e => e.SN == id) > 0;
        }
    }
}