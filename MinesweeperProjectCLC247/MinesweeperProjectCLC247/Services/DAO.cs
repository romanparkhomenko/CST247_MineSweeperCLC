using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace MinesweeperProjectCLC247.Services
{
    public class DAO
    {
        //supply SqlConnection object as class member variable
        private readonly SqlConnection conn = new SqlConnection("data source=(localdb)\\MSSQLLocalDB; database=Test; integrated security = SSPI");


         /*********************************
         *                                *
         *          Login DAO Method      *
         *                                *  
         *       To login User call       *
         *      Login Business Method     *    
         *                                * 
         **********************************/
        public bool Login(string Username, string Password)
        {
            //create new SqlCommand to SELECT user with username and password
            SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE Username = '" + Username + "' AND Password = '" + Password + "'", this.conn);

            //open database connection
            conn.Open();

            //execute command
            SqlDataReader reader = cmd.ExecuteReader();

            //check to see if execution returned any rows
            if (reader.HasRows)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


       /**********************************
        *                                *
        *      Register DAO Method       *
        *                                *
        *   Use with form generated User *  
        *               obj              *
        *                                *
        *    To Register new User call   *
        *     Register Business Method   *
        *                                * 
        **********************************/
        public bool Register(MinesweeperProjectCLC247.Models.UserModel user)
        {
            //create SqlCommand to insert new user into DB
            SqlCommand cmd = new SqlCommand("INSERT INTO dbo.Users(FirstName, LastName, Gender, Age, State, Email, Username, Password) VALUES('" + user.FirstName + "', '" + user.LastName + "', '" + user.Gender + "', '" + user.Age + "', '" + user.State + "', '" + user.Email + "', '" + user.Username + "', '" + user.Password + "')", this.conn);

            //open database connection
            conn.Open();

            //execute query
            int rows = cmd.ExecuteNonQuery();

            //check to see how many rows were affected
            if (rows >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}