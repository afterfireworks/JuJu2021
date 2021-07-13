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
    public class Api_CollectorsController : ApiController
    {
        private JuJuLocaldbEntities db = new JuJuLocaldbEntities();

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
                    foreach (var item in data)
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CollectorExists(long id)
        {
            return db.Collector.Count(e => e.SN == id) > 0;
        }
    }
}