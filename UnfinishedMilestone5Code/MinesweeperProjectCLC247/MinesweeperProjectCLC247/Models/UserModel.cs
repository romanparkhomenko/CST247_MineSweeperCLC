using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace MinesweeperProjectCLC247.Models
{
    public class UserModel
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

        [Required]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Age must be Numeric")]
        [Display(Name = "Age")]
        public int Age { get; set; }

        [Required]
        [Display(Name = "State")]
        public string State { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Username is a required field")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public UserModel() {
            // Parameterless Constructor because C# is weird?
        }

        public UserModel(string firstname, string lastname, string gender, int age, string state, string email, string username, string password)
        {
            this.FirstName = firstname;
            this.LastName = lastname;
            this.Gender = gender;
            this.Age = age;
            this.State = state;
            this.Email = email;
            this.Username = username;
            this.Password = password;
        }
    }
}