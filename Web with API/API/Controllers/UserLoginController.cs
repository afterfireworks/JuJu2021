using API.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class UserLoginController : ApiController
    {
        JuJuLocaldbEntities db = new JuJuLocaldbEntities();
        [HttpGet]
        public ArrayList UserLogin(string LoginAccount, string LoginPassword) 
        {
            ArrayList UserCheck = new ArrayList();
            try
            {
                var data = from u in db.Resident
                           where u.Account == LoginAccount && u.Password == LoginPassword
                           select u;

                if (data.FirstOrDefault() != null)
                {
                    object userAccount = data.FirstOrDefault().Account;
                    object userName = data.FirstOrDefault().Name;
                    object isLogin = 1;
                    object errorMessages = "登入成功";
                    Object loginState = new { userAccount, userName, isLogin, errorMessages };
                    UserCheck.Add(loginState);
                }

                else 
                {
                    object isLogin = 0;
                    object errorMessages = "驗證錯誤";
                    Object loginState = new { isLogin, errorMessages };
                    UserCheck.Add(loginState);
                }
            }
            catch 
            { }

            return UserCheck;
        }

    }
}
