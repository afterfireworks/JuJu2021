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
    public class Api_ResidentsController : ApiController
    {
        private JuJuLocaldbEntities db = new JuJuLocaldbEntities();

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

                    Object ResidentRow = new { Account, ID, Name, Tel, Address, Photo, errorMessages };
                    ResidentData.Add(ResidentRow);
                }

                else
                {
                    object errorMessages = "Error";
                    Object ErrorMessages = new { errorMessages };
                    ResidentData.Add(ErrorMessages);
                }
            }
            catch
            { }

            return ResidentData;
        }

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