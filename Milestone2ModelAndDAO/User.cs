using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace Milestone2.Models
{
    public class User
    {
        [HiddenInput] //hide id in forms when html helpers are used
        [Required(ErrorMessage = "Problem generating ID for User")]
        public int UserID { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "You must provide a first name")]
        [StringLength(200)] //set max length to 200
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "You must provide a last name")]
        [StringLength(200)]  //set max length to 200
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "Username is a required field")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is a required field")]
        [StringLength(60, MinimumLength = 8, ErrorMessage = "Your password must be longer than 8 characters and shorter than 60")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        public User(string firstname, string lastname, string username, string password)
        {
            this.FirstName = firstname;
            this.LastName = lastname;
            this.Username = username;
            this.Password = password;
        }
    }
}