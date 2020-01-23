using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinesweeperProjectCLC247.Models
{
    public class GameBoard
    {
        private List<List<Cell>> Board { get; set; }
        private int Size { get; set; }

        public GameBoard(int size)
        {
            this.Size = size;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Board[i].Add(new Cell(i, j));
                }
            }
        }



        public void OnCellLeftClick(int row, int column)
        {
            Cell currCell = Board[row][column];

            if (currCell.IsLive)
            {
                currCell.ImageLocation = "~/Images/flag_icon.ico";
            }
            else
            {
                ShowNeighbors(row, column);
            }
        }

        /*
         * Expected Usage:
         * 
         * Loop through entire game board and set all cells live neighbor count
         * 
         * 
         * */
        public void CountLiveNeighbors(int row, int column)
        {
            
            int count = 0;
            //check left cell
            if (row - 1 >= 0 && this.Board[row - 1][column].IsLive)
                count++;
            //check up left cell
            if (row - 1 >= 0 && column - 1 >= 0 && this.Board[row - 1][column - 1].IsLive)
                count++;
            //check down cell
            if (column - 1 >= 0 && this.Board[row][column - 1].IsLive)
                count++;
            //check right cell
            if (row + 1 < this.Size && this.Board[row + 1][column].IsLive)
                count++;
            //check up right cell
            if (row + 1 < this.Size && column + 1 < this.Size && this.Board[row + 1][column + 1].IsLive)
                count++;
            //check up cell
            if (column + 1 < this.Size && this.Board[row][column + 1].IsLive)
                count++;
            //check left down cell
            if (row - 1 >= 0 && column + 1 < this.Size && this.Board[row - 1][column + 1].IsLive)
                count++;
            //check right down cell
            if (row + 1 < this.Size && column - 1 >= 0 && this.Board[row + 1][column - 1].IsLive)
                count++;

            //set LiveNeighbors
            this.Board[row][column].LiveNeighbors = count;
        }

        public void ShowNeighbors(int row, int column)
        {
            Cell currCell = Board[row][column];
            //assuming current cell is NOT live
            if (currCell.LiveNeighbors > 0)
            {
                currCell.ImageLocation = null;

            }
            if (currCell.LiveNeighbors == 0)
            {
                row += 1;
                ShowNeighbors(row, column);
            }

        }
    }
}