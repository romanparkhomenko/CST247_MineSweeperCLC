using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MinesweeperProjectCLC247 {
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Login",
                url: "{Login}",
                defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
                name: "MainMenu",
                url: "{MainMenu}",
                defaults: new { controller = "MainMenu", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
                name: "Game",
                url: "{Game}",
                defaults: new { controller = "Game", action = "Index", id = UrlParameter.Optional });
        }
    }
}
