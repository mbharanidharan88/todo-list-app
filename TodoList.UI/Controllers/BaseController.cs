using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using TodoList.UI.Models;
using System.Diagnostics;

namespace TodoList.UI.Controllers
{
    public class BaseController : Controller
    {
        
        #region Properties

        public string UserName => User.FindFirst("Name")?.Value;

        public string UserId => User.FindFirst("Sub")?.Value;

        public string AccessToken => GetAccessToken().Result;

        #endregion

        #region Public Methods

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #endregion


        #region Private Methods

        private async Task<string> GetAccessToken() {

            return await HttpContext.GetTokenAsync("access_token");
        }

        #endregion
    }
}
