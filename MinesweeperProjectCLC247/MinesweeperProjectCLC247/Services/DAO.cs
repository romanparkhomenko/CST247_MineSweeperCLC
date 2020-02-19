using System.Collections.Generic;
using System.Data.SqlClient;
using MinesweeperProjectCLC247.Models;
using MinesweeperProjectCLC247.Constants;
using System.IO;
using Newtonsoft.Json;
using System.Data;
using System;

namespace MinesweeperProjectCLC247.Services {
    public class DAO {
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
        public int Login(string Username, string Password) {
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
        public bool Register(MinesweeperProjectCLC247.Models.UserModel user) {
            //create SqlCommand to insert new user into DB
            SqlCommand cmd = new SqlCommand("INSERT INTO dbo.Users(FirstName, LastName, Gender, Age, State, Email, Username, Password) VALUES('" + user.FirstName + "', '" + user.LastName + "', '" + user.Gender + "', '" + user.Age + "', '" + user.State + "', '" + user.Email + "', '" + user.Username + "', '" + user.Password + "')", this.conn);

            //open database connection
            conn.Open();

            //execute query
            int rows = cmd.ExecuteNonQuery();

            //check to see how many rows were affected
            if (rows >= 1) {
                return true;
            } else {
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
                string query = "SELECT * FROM dbo.Grids WHERE UserID=" + userID;
                SqlCommand cmd = new SqlCommand(query, this.conn);

                //open database connection
                conn.Open();

                //execute command
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows) {
                    while (reader.Read()) {
                        this.grid.ID = int.Parse(reader["ID"].ToString());
                        this.rows = int.Parse(reader["Rows"].ToString());
                        this.cols = int.Parse(reader["Cols"].ToString());
                        this.hasWon = bool.Parse(reader["HasWon"].ToString());
                    }
                } else {
                    previousGridExists = false;
                }

                // Close the connection
                conn.Close();

            } catch (SqlException e) {
                throw e;
            }

            grid = new GameBoardModel(this.rows, this.cols, this.hasWon);
            CellModel[,] cells = new CellModel[cols, rows];
            grid.Cells = cells;

            if (previousGridExists) {
                try {
                    string query = "SELECT * FROM dbo.Cells WHERE GridID=" + this.grid.ID;

                    SqlCommand cmd = new SqlCommand(query, this.conn);

                    // open database connection
                    conn.Open();

                    // Read through cells and save to grid.
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        int ID = int.Parse(reader["ID"].ToString());
                        int x = int.Parse(reader["Col"].ToString());
                        int y = int.Parse(reader["Row"].ToString());
                        bool isLive = bool.Parse(reader["IsLive"].ToString());
                        bool isVisited = bool.Parse(reader["IsVisited"].ToString());
                        int liveneighbors = int.Parse(reader["LiveNeighbors"].ToString());

                        CellModel cell = new CellModel(x, y);
                        cell.IsLive = isLive;
                        cell.IsVisited = isVisited;
                        cell.LiveNeighbors = liveneighbors;
                        grid.Cells[x, y] = cell;
                    }

                    // Close the connection
                    conn.Close();

                } catch (SqlException e) {
                    throw e;
                }
            }

            if (previousGridExists) {
                return grid;
            } else {
                return null;
            }

        }


        public List<PublishedGame> getAllStats() {
            List<PublishedGame> games = new List<PublishedGame>();

            try {
                string query = "SELECT * FROM dbo.Stats";
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

            } catch (SqlException e) {
                throw e;
            }

            return games;
        }


        public void CreateGrid(GameBoardModel grid, string userid) {

            int gridID;

            try {
                // Setup INSERT query with parameters
                string query = "INSERT INTO dbo.Grids (Rows, Cols, UserID, HasWon, Clicks) " +
                    "VALUES (@Rows, @Cols, @UserID, @HasWon, @Clicks) SELECT SCOPE_IDENTITY()";
                SqlCommand cmd = new SqlCommand(query, this.conn);

                cmd.Parameters.Add("@Rows", SqlDbType.Int, 11).Value = grid.Rows;
                cmd.Parameters.Add("@Cols", SqlDbType.Int, 11).Value = grid.Cols;
                cmd.Parameters.Add("@UserID", SqlDbType.Int, 11).Value = userid;
                cmd.Parameters.Add("@HasWon", SqlDbType.Bit).Value = grid.HasWon;
                cmd.Parameters.Add("@Clicks", SqlDbType.Int, 11).Value = grid.Clicks;

                conn.Open();

                gridID = Convert.ToInt32(cmd.ExecuteScalar());

                conn.Close();

            } catch (SqlException e) {
                throw e;
            }


            try {
                // Setup INSERT query with parameters
                string query = "INSERT INTO dbo.Cells (Col, Row, IsLive, IsVisited, LiveNeighbors, GridID) " +
                    "VALUES (@Col, @Row, @IsLive, @IsVisited, @LiveNeighbors, @GridID)";

                // Create connection and command
                for (int y = 0; y < grid.Rows; y++) {
                    for (int x = 0; x < grid.Cols; x++) {

                        SqlCommand cmd = new SqlCommand(query, this.conn);
                        cmd.Parameters.Add("@Col", SqlDbType.Int, 11).Value = grid.Cells[x, y].Column;
                        cmd.Parameters.Add("@Row", SqlDbType.Int, 11).Value = grid.Cells[x, y].Row;
                        cmd.Parameters.Add("@IsLive", SqlDbType.Bit).Value = grid.Cells[x, y].IsLive;
                        cmd.Parameters.Add("@IsVisited", SqlDbType.Bit).Value = grid.Cells[x, y].IsVisited;
                        cmd.Parameters.Add("@LiveNeighbors", SqlDbType.Int, 11).Value = grid.Cells[x, y].LiveNeighbors;
                        cmd.Parameters.Add("@GridID", SqlDbType.Int, 11).Value = gridID;

                        // Open the connection, execute INSERT, and close the connection
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }

            } catch (SqlException e) {
                throw e;
            }

        }

        public void UpdateGrid(GameBoardModel grid, string userid) {

           
            try {
                // Setup INSERT query with parameters
                string query = "UPDATE dbo.Grids SET ROWS = @Rows, COLS = @Cols, UserID = @UserID, HasWon = @HasWon, Clicks = @Clicks WHERE ID=@ID";

                SqlCommand cmd = new SqlCommand(query, this.conn);

                cmd.Parameters.Add("@Rows", SqlDbType.Int, 11).Value = grid.Rows;
                cmd.Parameters.Add("@Cols", SqlDbType.Int, 11).Value = grid.Cols;
                cmd.Parameters.Add("@UserID", SqlDbType.Int, 11).Value = userid;
                cmd.Parameters.Add("@HasWon", SqlDbType.Bit).Value = grid.HasWon;
                cmd.Parameters.Add("@Clicks", SqlDbType.Int, 11).Value = grid.Clicks;
                cmd.Parameters.Add("@ID", SqlDbType.Int, 11).Value = grid.ID;

                conn.Open();

                cmd.ExecuteNonQuery();

                conn.Close();

            } catch (SqlException e) {
                throw e;
            }


            try {
                // Setup INSERT query with parameters
                string query = "UPDATE dbo.Cells SET Col = @Col, Row = @Row, IsLive = @IsLive, IsVisited = @IsVisited, LiveNeighbors = @LiveNeighbors, " +
                   "GridID = @GridID WHERE ID=@ID";

                // Create connection and command
                for (int y = 0; y < grid.Rows; y++) {
                    for (int x = 0; x < grid.Cols; x++) {

                        SqlCommand cmd = new SqlCommand(query, this.conn);
                        cmd.Parameters.Add("@Col", SqlDbType.Int, 11).Value = grid.Cells[x, y].Column;
                        cmd.Parameters.Add("@Row", SqlDbType.Int, 11).Value = grid.Cells[x, y].Row;
                        cmd.Parameters.Add("@IsLive", SqlDbType.Bit).Value = grid.Cells[x, y].IsLive;
                        cmd.Parameters.Add("@IsVisited", SqlDbType.Bit).Value = grid.Cells[x, y].IsVisited;
                        cmd.Parameters.Add("@LiveNeighbors", SqlDbType.Int, 11).Value = grid.Cells[x, y].LiveNeighbors;
                        cmd.Parameters.Add("@GridID", SqlDbType.Int, 11).Value = grid.ID;
                        cmd.Parameters.Add("@ID", SqlDbType.Int, 11).Value = grid.Cells[x, y].ID;

                        // Open the connection, execute INSERT, and close the connection
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }

            } catch (SqlException e) {
                throw e;
            }

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
            } catch (SqlException e) {
                throw e;
            }

        }
    }

}
