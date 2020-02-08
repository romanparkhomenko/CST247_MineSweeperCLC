using MinesweeperProjectCLC247.Models;
using MinesweeperProjectCLC247.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MinesweeperProjectCLC247.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        // POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "FirstName,LastName,Gender,Age,State,Email,Username,Password,ConfirmPassword")] Models.UserModel submission) {
            // Check for errors
            if (!ModelState.IsValid) {
                return View("Index");
            }

            return CreateUser(submission);
        }

        private ActionResult CreateUser(Models.UserModel user) {
            // Get database service
            DAObusiness service = new DAObusiness();

            // Add user to database.
            if (service.Register(user)) {
                // If successful...
                return View("Success");
            } else {
                // If not successful
                return View("Failure");
            }
        }

    }
}