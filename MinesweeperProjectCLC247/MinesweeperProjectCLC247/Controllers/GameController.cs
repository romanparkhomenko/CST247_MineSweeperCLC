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

    public class GameController : Controller {
        private static MyLogger1 logger = MyLogger1.GetInstance();


        // GET: Game
        [CustomAuthorization]
        [HttpGet]
        public ActionResult Index() {
            int USERID = int.Parse(new JavaScriptSerializer().Serialize(Session["userid"]));
            GameLogicBLL gameService = new GameLogicBLL();

            if (Session["user"] != null) {
                Globals.Grid = gameService.FindGrid(USERID);

                if (Globals.Grid == null) {
                    Globals.Grid = gameService.CreateGrid(25, 25, USERID);
                }

            } else {
                ModelError e = new ModelError("You must be logged in to access this page.");
            }

            return View("Index", Globals.Grid);
        }


        public string ElapsedTime() {
            TimeSpan ts = Globals.timer.Elapsed;
            Globals.timer.Stop();
            string elapsedtime = String.Format("{0:00}:{1:00}.{2:00}", ts.Minutes, ts.Seconds, ts.Milliseconds / 10);

            return elapsedtime;
        }

        [HttpPost]
        public PartialViewResult ActivateCell(string x, string y) {
            int USERID = int.Parse(new JavaScriptSerializer().Serialize(Session["userid"]));
            int Column = int.Parse(x.Trim());
            int Row = int.Parse(y.Trim());
            GameLogicBLL gameService = new GameLogicBLL();
            GameBoardModel grid = Globals.Grid;

            grid.Cells[Column, Row].IsVisited = true;
            
            Globals.numberClicks++;
            grid.Clicks = Globals.numberClicks;

            if (grid.Cells[Column, Row].IsLive) {
                gameService.PublishGameStats(grid, USERID, this.ElapsedTime());
                return EndGame();
            } else {
                if (grid.Cells[Column, Row].LiveNeighbors == 0) {
                    gameService.showNeighbors(Globals.Grid, Globals.Grid.Cells[Column, Row].Column, Globals.Grid.Cells[Column, Row].Row);
                }
                gameService.UpdateGrid(grid, USERID);
            }
            
            return PartialView("Index", grid);
        }


        private PartialViewResult EndGame() {
            RevealAll();
            return PartialView("Index", Globals.Grid);
        }

        // Function to show the entire grid after losing.
        private void RevealAll() {
            Globals.numberClicks++;

            logger.Info("Number of Clicks = " + Globals.numberClicks.ToString());
            logger.Info("Time Spent in Game = " + ElapsedTime());
            GameBoardModel grid = Globals.Grid;

            grid.HasWon = true;

            for (int i = 0; i < grid.Rows; i++) {
                for (int j = 0; j < grid.Cols; j++) {
                    grid.Cells[i, j].IsVisited = true;
                }
            }
        }


        // Function to reset the grid and Game.
        [HttpGet]
        public ActionResult resetGame() {
            int USERID = int.Parse(new JavaScriptSerializer().Serialize(Session["userid"]));
            GameLogicBLL gameService = new GameLogicBLL();
            gameService.deleteGrid(USERID);
            Globals.numberClicks = 0;
            return View("Index", gameService.CreateGrid(25, 25, USERID));
        }




    }
}