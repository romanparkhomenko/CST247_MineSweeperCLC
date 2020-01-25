using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace MinesweeperProjectCLC247.Models {
    public class GameBoardModel {

        //public List<CellModel> Board = new List<CellModel>();
        public int Rows { get; set; }
        public int Cols { get; set; }
        public bool HasWon { get; set; }
        public CellModel[,] Cells { get; set; }
        public Stopwatch stopwatch = new Stopwatch();


        //constructor to make board of given size
        public GameBoardModel(int rows, int cols, bool hasWon) {
            this.Rows = rows;
            this.Cols = cols;
            this.HasWon = hasWon;

            //CellModel[,] cells = new CellModel[cols, rows];

            //for (int y = 0; y < rows; y++) {
            //    for (int x = 0; x < cols; x++) {
            //        cells[x, y] = new CellModel(x, y);
            //    }
            //}

            //this.stopwatch.Start();
        }



        public void SetLiveCount() {
            for (int i = 0; i < Rows; i++) {
                for (int j = 0; j < Cols; j++) {
                    // North (-1, 0)
                    if (i - 1 >= 0) {
                        if (Cells[i - 1, j].IsLive == true) {
                            Cells[i, j].LiveNeighbors += 1;
                        }
                    }

                    // NorthWest (-1, -1)
                    if (i - 1 >= 0 && j - 1 >= 0) {
                        if (Cells[i - 1, j - 1].IsLive == true) {
                            Cells[i, j].LiveNeighbors += 1;
                        }
                    }

                    // NorthEast (-1, +1)
                    if (i - 1 >= 0 && j + 1 < Cols) {
                        if (Cells[i - 1, j + 1].IsLive == true) {
                            Cells[i, j].LiveNeighbors += 1;
                        }
                    }

                    // South (+1, 0)
                    if (i + 1 < Rows) {
                        if (Cells[i + 1, j].IsLive == true) {
                            Cells[i, j].LiveNeighbors += 1;
                        }
                    }

                    // SouthWest (+1, -1)
                    if (i + 1 < Rows && j - 1 >= 0) {
                        if (Cells[i + 1, j - 1].IsLive == true) {
                            Cells[i, j].LiveNeighbors += 1;
                        }
                    }

                    // SouthEast (+1, +1)
                    if (i + 1 < Rows && j + 1 < Cols) {
                        if (Cells[i + 1, j + 1].IsLive == true) {
                            Cells[i, j].LiveNeighbors += 1;
                        }
                    }

                    // Check East (0, +1)
                    if (j + 1 < Cols) {
                        if (Cells[i, j + 1].IsLive == true) {
                            Cells[i, j].LiveNeighbors += 1;
                        }
                    }

                    // Check West (0, -1)
                    if (j - 1 >= 0) {
                        if (Cells[i, j - 1].IsLive == true) {
                            Cells[i, j].LiveNeighbors += 1;
                        }
                    }
                }
            }
        }


        /*
         * 
         * Method to fill 15% of random cells 
         * 
         */
        //public void FillRandomCells() {
        //    Random rand = new Random();
        //    int FillAmnt = (int)(this.Size * this.Size * 0.15);
        //    int NumFilled = 0;

        //    while (NumFilled <= FillAmnt) {
        //        this.Board[rand.Next(0, this.Size)][rand.Next(0, Size)].IsLive = true;
        //        NumFilled++;
        //    }
        //}

        /*
         * Expected Usage:
         * 
         * Loop through entire game board and set all cells live neighbor count
         * 
         */
        //public void CountLiveNeighbors(int row, int column) {

        //    int count = 0;
        //    //check left cell
        //    if (row - 1 >= 0 && this.Board[row - 1][column].IsLive)
        //        count++;
        //    //check up left cell
        //    if (row - 1 >= 0 && column - 1 >= 0 && this.Board[row - 1][column - 1].IsLive)
        //        count++;
        //    //check down cell
        //    if (column - 1 >= 0 && this.Board[row][column - 1].IsLive)
        //        count++;
        //    //check right cell
        //    if (row + 1 < this.Size && this.Board[row + 1][column].IsLive)
        //        count++;
        //    //check up right cell
        //    if (row + 1 < this.Size && column + 1 < this.Size && this.Board[row + 1][column + 1].IsLive)
        //        count++;
        //    //check up cell
        //    if (column + 1 < this.Size && this.Board[row][column + 1].IsLive)
        //        count++;
        //    //check left down cell
        //    if (row - 1 >= 0 && column + 1 < this.Size && this.Board[row - 1][column + 1].IsLive)
        //        count++;
        //    //check right down cell
        //    if (row + 1 < this.Size && column - 1 >= 0 && this.Board[row + 1][column - 1].IsLive)
        //        count++;

        //    //set LiveNeighbors
        //    this.Board[row][column].LiveNeighbors = count;
        //}



        /*
         * Expected Usage
         *  
         * Once cell is clicked and is determined to be not live, check all cells
         * around do see if they are live
         * 
         */
        //public void ShowNeighbors(int row, int column) {
        //    CellModel CurrCell = this.Board[row][column];
        //    CurrCell.IsVisited = true;
        //    CurrCell.Text = " ~ ";

        //    if (CurrCell.LiveNeighbors > 0) {
        //        CurrCell.Text = CurrCell.LiveNeighbors.ToString();
        //        return;
        //    } else if (CurrCell.LiveNeighbors == 0) {
        //        CurrCell.Text = " ~ ";

        //        //check up cell
        //        if (column + 1 < this.Size && !this.Board[row][column + 1].IsVisited)
        //            ShowNeighbors(row, column + 1);
        //        //check right cell
        //        if (row + 1 < this.Size && !this.Board[row + 1][column].IsVisited)
        //            ShowNeighbors(row + 1, column);
        //        //check down cell
        //        if (column - 1 >= 0 && !this.Board[row][column - 1].IsVisited)
        //            ShowNeighbors(row, column - 1);
        //        //check left cell
        //        if (row - 1 >= 0 && !this.Board[row - 1][column].IsVisited)
        //            ShowNeighbors(row - 1, column);
        //    }

        //    return;
        //}



        /*
         * 
         * Expected Usage:
         * 
         * On every click check to see if user has won the game by checking
         * to see if every cell has been visited
         * 
         * 
         */
        //public System.Web.Mvc.ActionResult CheckWin() {
        //    bool HasWon;

        //    for (int i = 0; i < this.Size; i++) {
        //        for (int j = 0; j < this.Size; j++) {
        //            if (!Board[i][j].IsVisited && !Board[i][j].IsLive) {
        //                HasWon = false;
        //            }
        //        }
        //    }

        //    HasWon = true;


        //    // If statement to check win condition
        //    if (!HasWon) {
        //        // Stop Timer and get TimeSpan object.
        //        this.stopwatch.Stop();
        //        TimeSpan ts = stopwatch.Elapsed;

        //        // Format and display the TimeSpan value.
        //        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
        //        string text = "I don't know how you did it, but YOU WON!" + Environment.NewLine + "Time elapsed: " + elapsedTime + "seconds.";


        //    }

        //    /****************************************************************************************
        //     *                                                                                      *
        //     *                                                                                      *
        //     * Here insert portion of code to return partial view telling the user that they've won *
        //     *                                                                                      *
        //     *                                                                                      *       
        //     ****************************************************************************************/

        //    return _PartialView();
        //}
    }
}