using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MinesweeperProjectCLC247.Models;

namespace MinesweeperProjectCLC247.Services
{
    public class DAObusiness
    {

        /**********************************
         *                                *
         *     Login Business Method      *
         *                                *
         *    Call this method to Login   * 
         **********************************/
        public int Login(string Username, string Password)
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
        public bool Register(MinesweeperProjectCLC247.Models.UserModel newUser)
        {
            DAO dao = new DAO();

            return dao.Register(newUser);
        }


        public GameBoardModel FindGrid(int userID) {
            DAO dao = new DAO();

            return dao.FindGrid(userID);
        }

        public List<PublishedGame> getAllStats() {
            DAO dao = new DAO();

            //saves game stats to db
            return dao.getAllStats();
        }

        public void SaveGame(GameBoardModel grid, string userID) {
            DAO dao = new DAO();
            dao.SaveGame(grid, userID);
        }

    }
}