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
    public class Api_AnnouncementsController : ApiController
    {
        private JuJuLocaldbEntities db = new JuJuLocaldbEntities();

        [HttpGet]
        public ArrayList GetAnnouncements()
        {
            ArrayList AnnouncementData = new ArrayList();
            try
            {
                DateTime currenteDate = DateTime.UtcNow.Date.AddDays(-30);
                var data = db.Announcement.Where(a => a.Date >= currenteDate).ToList();

                if (data != null)
                {
                    foreach (var item in data)
                    {
                        object SN = item.SN;
                        object Date = item.Date;
                        object Category = item.Category;
                        object Title = item.Title;
                        object Description = item.Description;
                        object Picture = item.Picture;

                        Object AnnouncementRow = new { SN, Date, Category, Title, Description, Picture };
                        AnnouncementData.Add(AnnouncementRow);
                    }
                }
                else
                {
                    object errorMessages = "Error";
                    Object ErrorMessages = new { errorMessages };
                    AnnouncementData.Add(ErrorMessages);
                }
            }
            catch
            { }

            return AnnouncementData;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AnnouncementExists(long id)
        {
            return db.Announcement.Count(e => e.SN == id) > 0;
        }
    }
}