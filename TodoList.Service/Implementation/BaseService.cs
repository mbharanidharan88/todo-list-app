using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Localization;
using TodoList.ResourceLibrary;

namespace TodoList.Service
{
    public abstract class BaseService
    {
        #region Fields

        protected readonly HttpClient httpClient;
        protected readonly IStringLocalizer<SharedResource> sharedLocalizer;
        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion

        protected BaseService(IStringLocalizer<SharedResource> sharedLocalizer,
                                HttpClient httpClient, 
                                IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            this.sharedLocalizer = sharedLocalizer;
            this.httpClient = httpClient;
            this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
        }

        protected string AccessToken => GetAccessToken().Result;

        private async Task<string> GetAccessToken()
        {

            return await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
        }
    }
}
