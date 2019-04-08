using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AirlineReservation.Filter
{
    public class AirlineAuthorization : AuthorizeAttribute
    {
        public string UserRoles { get; set; }
        public AirlineAuthorization()
        {

        }
        public AirlineAuthorization(params string[] userRoles)
        {
            this.UserRoles = String.Join(",", userRoles);
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            bool authorized = false;
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                var userRoles = this.UserRoles.Split(',');

                foreach (var userrole in userRoles)
                {
                    if (filterContext.HttpContext.User.IsInRole(userrole))
                    {
                        authorized = true;
                        break;
                    }
                }

            }

            if (!authorized)
            {
                filterContext.Result = new RedirectToRouteResult(
                                                            new RouteValueDictionary {
                                                                { "controller", "Login" },
                                                                { "action", "Index" },
                                                                { "ReturnUrl", filterContext.HttpContext.Request.RawUrl }
                                                                });

                
            
            }

        }
    }
}