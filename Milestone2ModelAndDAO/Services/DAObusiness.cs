using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Milestone2.Services
{
    public class DAObusiness
    {

        /**********************************
         *                                *
         *     Login Business Method      *
         *                                *
         *    Call this method to Login   * 
         **********************************/
        public bool Login(string Username, string Password)
        {
            DAO dao = new DAO();

            return dao.Login(Username, Password);
        }


        /**********************************
        *                                *
        *          Register Method       *
        *                                *
        *   Use with form generated User *  
        *             obj                *
        *                                *
        *  Call this method to Register  *
        *           new User             * 
        ********************************** */
        public bool register(Milestone2.Models.User newUser)
        {
            DAO dao = new DAO();

            return dao.Register(newUser);
        }

    }
}