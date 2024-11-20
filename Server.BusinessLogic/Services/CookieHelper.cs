using Microsoft.AspNetCore.Http;
using Server.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.BusinessLogic.Services
{
    public class CookieHelper : ICookieHelper
    {
        public void SetCookie(HttpResponse response, string key, string value, CookieOptions options)
        {
            response.Cookies.Append(key, value, options);
        }
    }
}
