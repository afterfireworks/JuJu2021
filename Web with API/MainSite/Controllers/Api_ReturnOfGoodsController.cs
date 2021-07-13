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
    public class Api_ReturnOfGoodsController : ApiController
    {
        private JuJuLocaldbEntities db = new JuJuLocaldbEntities();

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
                        object Sign = item.Sign ? "貨運已領取" : "貨運未領取";
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