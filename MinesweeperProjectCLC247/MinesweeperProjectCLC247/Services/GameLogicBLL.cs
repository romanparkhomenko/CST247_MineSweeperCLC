using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MinesweeperProjectCLC247.Models;
using MinesweeperProjectCLC247.Constants;

namespace MinesweeperProjectCLC247.Services {
    public class GameLogicBLL {

        // Find an existing saved grid and save it to storage
        public GameBoardModel FindGrid(int userID) {
            DAObusiness businessService = new DAObusiness();
            Globals.Grid = businessService.FindGrid(userID);

            if (Globals.Grid != null) {
                return Globals.Grid;
            } else {
                return null;
            }
        }

        // Function to create GLOBAL Grid and fill it with random cells.
        public GameBoardModel CreateGrid(int width, int height, int userID) {
            Globals.timer.Start();

            Globals.Grid = new GameBoardModel(width, height, false);
            CellModel[,] cells = new CellModel[width, height];

            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    cells[x, y] = new CellModel(x, y);
                }
            }

            // Activate the cells
            FillRandomCells(20, width, height, cells, Globals.Grid);

            // Send new Grid to DB
            DAObusiness businessService = new DAObusiness();
            businessService.CreateGrid(Globals.Grid, userID);

            return Globals.Grid;
        }

        public void UpdateGrid(GameBoardModel grid, int userID) {
           
            // Send updated Grid to DB
            DAObusiness businessService = new DAObusiness();
            businessService.UpdateGrid(grid, userID);
            
        }

        public void PublishGameStats(GameBoardModel grid, int userID, string elapsedTime) {
            DAObusiness businessService = new DAObusiness();
            businessService.publishGameStats(grid, userID, elapsedTime);
        }

        public void deleteGrid(int userID) {
            DAObusiness businessService = new DAObusiness();
            businessService.deleteGrid(userID);
        }

        public void FillRandomCells(int percentage, int width, int height, CellModel[,] mineField, GameBoardModel grid) {
            Random random = new Random();

            int TotalLiveCount = 0;

            if (percentage > 100 || percentage < 1) {
                percentage = 20;
            } else {
                percentage = (int)((width * height) * (percentage / 100.00));
                TotalLiveCount = percentage;
            }

            while (percentage > 0) {
                var cell = mineField[
                    random.Next(0, width),
                    random.Next(0, height)];

                if (cell.IsLive == false) {
                    cell.IsLive = true;
                    percentage -= 1;
                } else {
                    continue;
                }
            }

            grid.Cells = mineField;
            grid.SetLiveCount();
        }


        // Recursive solution to go through grid and reveal open neighbors.
        public bool showNeighbors(GameBoardModel grid, int x, int y) {
            CellModel cell = grid.Cells[x, y];
            cell.IsVisited = true;

            if (cell.LiveNeighbors > 0) {
                return false;
            }

            if (cell.Column - 1 >= 0) {
                var westLocation = grid.Cells[cell.Column - 1, cell.Row];

                if (!westLocation.IsLive && !westLocation.IsVisited) {
                    if (westLocation.LiveNeighbors == 0) {
                        showNeighbors(grid, westLocation.Column, westLocation.Row);
                    } else {
                        westLocation.IsVisited = true;
                    }
                }
            }

            if (cell.Row - 1 >= 0) {
                var northLocation = grid.Cells[cell.Column, cell.Row - 1];

                if (!northLocation.IsLive && !northLocation.IsVisited) {
                    if (northLocation.LiveNeighbors == 0) {
                        showNeighbors(grid, northLocation.Column, northLocation.Row);
                    } else {
                        northLocation.IsVisited = true;
                    }
                }
            }

            if (cell.Row + 1 < grid.Cols) {
                var southLocation = grid.Cells[cell.Column, cell.Row + 1];

                if (!southLocation.IsLive && !southLocation.IsVisited) {
                    if (southLocation.LiveNeighbors == 0) {
                        showNeighbors(grid, southLocation.Column, southLocation.Row);
                    } else {
                        southLocation.IsVisited = true;
                    }
                }
            }

            if (cell.Column + 1 < grid.Rows) {
                var eastLocation = grid.Cells[cell.Column + 1, cell.Row];

                if (!eastLocation.IsLive && !eastLocation.IsVisited) {
                    if (eastLocation.LiveNeighbors == 0) {
                        showNeighbors(grid, eastLocation.Column, eastLocation.Row);
                    } else {
                        eastLocation.IsVisited = true;
                    }
                }
            }

            return false;
        }

    }
}