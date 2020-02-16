using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using GameBoard = MinesweeperProjectCLC247.Models.GameBoardModel;
using Cell = MinesweeperProjectCLC247.Models.CellModel;

namespace MinesweeperProjectCLC247.Controllers
{
    public class APIController : Controller
    {
        // GET: API
        public ActionResult Index()
        {
            //create object model from consumed GameService.svc/GetGrid
            GameAPI.GameServiceClient APIClient = new GameAPI.GameServiceClient();
            int SizeX = APIClient.GetGameBoard().SizeX;
            int SizeY = APIClient.GetGameBoard().SizeY;
            GameBoard board = new GameBoard(SizeX, SizeY, false);

            for (int i = 0; i < (SizeX * SizeY); i++) {
                for (int row = 0; row < SizeY; row++)
                {
                    for (int col = 0; col < SizeX; col++)
                    {
                        var APICell = APIClient.GetGameBoard().Cells[i];
                        Cell cell = new Cell(APICell.Column, APICell.Row, APICell.LiveNeighbors, APICell.IsVisited, APICell.IsLive);

                        board.Cells[row, col] = cell;
                    }
                }
            }
            return View("~/Views/Game/Game", board);
        }
    }
}