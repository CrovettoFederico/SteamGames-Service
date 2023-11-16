using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;

namespace ApiSteam {
    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    public class ApiSteamAuthAttribute : Attribute, IAuthorizationFilter {

        private string ApiSteamAuthCode = "Fede123";

        public void OnAuthorization(AuthorizationFilterContext context) {
            if (context == null) { }
            var request = context.HttpContext.Request;
            var authorizationCode = request.Headers["AuthCode"].ToString();

            if (!IsAuthorized(authorizationCode)) {
                context.Result = new ContentResult() {
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                    Content = JsonSerializer.Serialize(new {
                        title = "Unauthorized",
                        status = 401,
                        message = "Unauthorized"
                    }
                ),
                    ContentType = "application/json"
                };
            }
        }

        private bool IsAuthorized(string authorizationCode) {
            if (authorizationCode == ApiSteamAuthCode) return true;
            else return false;
        }
    }
}
