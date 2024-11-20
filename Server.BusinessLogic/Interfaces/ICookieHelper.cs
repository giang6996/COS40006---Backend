// ICookieHelper.cs
using Microsoft.AspNetCore.Http;
namespace Server.BusinessLogic.Interfaces
{
    public interface ICookieHelper
    {
        void SetCookie(HttpResponse response, string key, string value, CookieOptions options);
    }
}
