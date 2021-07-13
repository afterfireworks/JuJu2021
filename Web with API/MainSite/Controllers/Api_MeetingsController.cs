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
    public class Api_MeetingsController : ApiController
    {
        private JuJuLocaldbEntities db = new JuJuLocaldbEntities();

        [HttpGet]
        public ArrayList GetMeeting()
        {
            ArrayList MeetingData = new ArrayList();
            try
            {
                var data = db.Meeting.ToList();

                if (data != null)
                {
                    foreach (var item in data)
                    {
                        object SN = item.SN;
                        object Date = item.Date;
                        object Title = item.Title;
                        object URL = item.URL;

                        Object MeetingRow = new { SN, Date, Title, URL };
                        MeetingData.Add(MeetingRow);
                    }
                }
                else
                {
                    object errorMessages = "Error";
                    Object ErrorMessages = new { errorMessages };
                    MeetingData.Add(ErrorMessages);
                }
            }
            catch
            { }

            return MeetingData;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MeetingExists(long id)
        {
            return db.Meeting.Count(e => e.SN == id) > 0;
        }
    }
}