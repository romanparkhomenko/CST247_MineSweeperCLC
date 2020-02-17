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


        //constructor to make board of given size
        public GameBoardModel(int rows, int cols, bool hasWon) {
            this.Rows = rows;
            this.Cols = cols;
            this.HasWon = hasWon;
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

    }
}