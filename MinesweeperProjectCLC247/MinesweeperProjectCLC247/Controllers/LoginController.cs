﻿using MinesweeperProjectCLC247.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MinesweeperProjectCLC247.Services.Utility;
using NLog;

namespace MinesweeperProjectCLC247.Controllers
{
    public class LoginController : Controller {

        private static MyLogger1 logger = MyLogger1.GetInstance();

        // GET: Login
        [HttpGet]
        public ActionResult Index() {
            return View("Login");
        }

        // POST: Login/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(FormCollection collection) {
            try {

                // Get the values from the Login form and trim any whitespace.
                string username = collection["username"].Trim();
                string password = collection["password"].Trim();

                // Verify that password and username aren't empty.
                if (String.IsNullOrEmpty(username)) {
                    ModelState.AddModelError("username", "Whoops, you're missing a username!");
                }


                if (String.IsNullOrEmpty(password)) {
                    ModelState.AddModelError("password", "Whoops, your password is missing.");
                }

                // If there aren't errors, proceed to authenticate the user.
                if (!ModelState.IsValid) {
                    return View("Login");
                }

                // Validate User and get view
                return AuthenticateUser(username, password);
            }
            catch {
                return View();
            }
        }

        private ActionResult AuthenticateUser(string username, string password) {
            DAObusiness service = new DAObusiness();
            
            int authorized = service.Login(username, password);

            logger.Info("Authorized: " + authorized);

            if (authorized != 0) {
                Session["user"] = username;
                Session["userid"] = authorized;
                Session.Timeout = 20;

                ViewBag.Username = Session["user"];
                ViewBag.UserID = Session["userid"];
                return View("LoginPassed");
            } else {
                return View("LoginFailed");
            }
        }


    }
}