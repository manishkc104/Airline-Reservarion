using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirlineReservation.Common
{
    public class Common
    {
        public static int GetAuthenticatedUser()
        {
            var authenticatedUser = 0;
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                authenticatedUser = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            }
            return authenticatedUser;
        }
    }
}