using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebAPIRevisited
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                string authToken = actionContext.Request.Headers.Authorization.Parameter;
                var decodedAuthToken = Encoding.UTF8.GetString(Convert.FromBase64String(authToken));
                var userNamePasswordArray = decodedAuthToken.Split(':');
                var userName = userNamePasswordArray[0];
                var password = userNamePasswordArray[1];

                if (EmployeeSecurity.Login(userName,password))
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(userName),null );
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
        }
    }
}