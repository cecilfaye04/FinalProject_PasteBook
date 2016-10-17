using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PasteBook
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //Source: https://www.asp.net/mvc/overview/older-versions-1/controllers-and-routing/creating-a-route-constraint-cs
            routes.MapRoute(
    "PasteBook",
    "PasteBook/{username}",
    new { controller = "PasteBook", action = "Profile" },
    new { username = @"\w+" }
 );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "Index", id = UrlParameter.Optional }
            );
        }


    }
}
