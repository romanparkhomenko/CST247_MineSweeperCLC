using MinesweeperProjectCLC247.Models;
using MinesweeperProjectCLC247.Constants;
using MinesweeperProjectCLC247.Services;
using MinesweeperProjectCLC247.Services.Utility;
using System;
using System.Diagnostics;
using System.Threading;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using NLog;

namespace MinesweeperProjectCLC247.Controllers {

  

    public class GameController : Controller
    {
        private static MyLogger1 logger = MyLogger1.GetInstance();

        // GET: Game
        [CustomAuthorization]
        [HttpGet]
        public ActionResult Index() {
            GameBoardModel grid = null;
            GameLogicBLL gameService = new GameLogicBLL();

            if (Session["user"] != null) {
                int userID = int.Parse(new JavaScriptSerializer().Serialize(Session["userid"]));
                grid = gameService.FindGrid(userID);
                
                if (grid == null) {
                    grid = gameService.CreateGrid(25, 25);
                }

            } else {
                ModelError e = new ModelError("You must be logged in to access this page.");
            }

            //logger.Info("Grid: " + new JavaScriptSerializer().Serialize(grid));
            
            return View("Index", grid);
        }


        public string ElapsedTime()
        {
            TimeSpan ts = Globals.timer.Elapsed;
            Globals.timer.Stop();
            string elapsedtime = String.Format("{0:00}:{1:00}.{2:00}", ts.Minutes, ts.Seconds, ts.Milliseconds / 10);

            return elapsedtime;
        }

        [HttpPost]
        public PartialViewResult ActivateCell(string x, string y) {
            int Column = int.Parse(x.Trim());
            int Row = int.Parse(y.Trim());
            GameBoardModel grid = Globals.Grid;
            GameLogicBLL gameService = new GameLogicBLL();

            CellModel cell = grid.Cells[Column, Row];

            cell.IsVisited = true;

            if (cell.IsLive) {
                return EndGame();
            } else {
                if (cell.LiveNeighbors == 0) {
                    gameService.showNeighbors(grid, cell.Column, cell.Row);
                }
            }
            Globals.numberClicks++;
            return PartialView("Index", grid);
        }
       

        private PartialViewResult EndGame() {
         
            RevealAll();
            return PartialView("Index", Globals.Grid);
        }

        // Function to show the entire grid after losing.
        private void RevealAll()
        {
            Globals.numberClicks++;
           
           
            logger.Info("Number of Clicks = "+Globals.numberClicks.ToString());
            logger.Info("Time Spent in Game = "+ElapsedTime()) ;
            GameBoardModel grid = Globals.Grid;

            grid.HasWon = true;

            for (int i = 0; i < grid.Rows; i++)
            {
                for (int j = 0; j < grid.Cols; j++)
                {
                    grid.Cells[i, j].IsVisited = true;
                }
            }
        }

        [HttpGet]
        public ActionResult resetGame() {
            GameLogicBLL gameService = new GameLogicBLL();
            return View("Index", gameService.CreateGrid(25,25));
        }

        [HttpPost]
        public ActionResult saveGame() {
            DAObusiness gameService = new DAObusiness();
            gameService.SaveGame(Globals.Grid, Session["userid"].ToString());
            return View("Index", Globals.Grid);
        }




    }
}