using System.Collections.Generic;
using System.Data.SqlClient;
using MinesweeperProjectCLC247.Models;
using MinesweeperProjectCLC247.Constants;
using System.IO;
using Newtonsoft.Json;

namespace MinesweeperProjectCLC247.Services {
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
        public int Login(string Username, string Password)
        {
            //create new SqlCommand to SELECT user with username and password
            SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE Username = '" + Username + "' AND Password = '" + Password + "'", this.conn);
            int userid = 0;

            //open database connection
            conn.Open();

            //execute command
            SqlDataReader reader = cmd.ExecuteReader();

            //check to see if execution returned any rows
            if (reader.HasRows) {
                while (reader.Read()) {
                    userid = int.Parse(reader["ID"].ToString());
                }
            }

            if (userid == 0) {
                return 0;
            } else {
                return userid;
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


        public GameBoardModel grid = null;
        public int rows;
        public int cols;
        public bool hasWon;
        public bool previousGridExists = true;

        public GameBoardModel FindGrid(int userID) {
            

            try {
                string query = "SELECT * FROM dbo.Grids WHERE UserID=" + userID.ToString();
                SqlCommand cmd = new SqlCommand(query, this.conn);

                //open database connection
                conn.Open();

                //execute command
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows) {
                    while (reader.Read()) {
                        this.rows = int.Parse(reader["rows"].ToString());
                        this.cols = int.Parse(reader["cols"].ToString());
                        this.hasWon = bool.Parse(reader["haswon"].ToString());
                    }
                } else {
                    previousGridExists = false;
                }

                // Close the connection
                conn.Close();

            } catch(SqlException e) {
                throw e;
            }

            grid = new GameBoardModel(this.rows, this.cols, this.hasWon);
            CellModel[,] cells = new CellModel[cols, rows];

            for (int y = 0; y < rows; y++) {
                for (int x = 0; x < cols; x++) {
                    cells[x, y] = new CellModel(x, y);
                }
            }

            grid.Cells = cells;

            if (previousGridExists) {
                return grid;
            } else {
                return null;
            }
            
        }


        public List<PublishedGame> getAllStats() {
            List<PublishedGame> games = new List<PublishedGame>();

            try {
                string query = "SELECT * FROM dbo.stats";
                SqlCommand cmd = new SqlCommand(query, this.conn);

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    int ID = int.Parse(reader["ID"].ToString());
                    int userid = int.Parse(reader["UserID"].ToString());
                    int clicks = int.Parse(reader["Clicks"].ToString());
                    string time = reader["Time"].ToString();

                    games.Add(new PublishedGame(ID, userid, clicks, time));
                }

                conn.Close();

            }
            catch (SqlException e) {
                throw e;
            }

            return games;
        }


        public void SaveGame(GameBoardModel grid, string userID) {
            //open file stream
            StreamWriter file = File.CreateText(@"D:\game.json");
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(file, grid);

            try {
                string query = "UPDATE dbo.Grids SET gridObject=D:\\game.json WHERE UserID=" + userID;
                SqlCommand cmd = new SqlCommand(query, this.conn);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                string statsQuery = "INSERT INTO dbo.Stats VALUES(" + userID + "," + Globals.numberClicks + "," + Globals.timer.ToString() + ")";
                SqlCommand statsCMD = new SqlCommand(statsQuery, this.conn);

                conn.Open();
                statsCMD.ExecuteNonQuery();
                conn.Close();
            }
            catch (SqlException e) {
                throw e;
            }

        }
    }

}
