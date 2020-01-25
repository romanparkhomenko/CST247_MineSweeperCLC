using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinesweeperProjectCLC247.Models {
    public class CellModel {
        public bool IsClicked { get; set; } = false;
        public int LiveNeighbors { get; set; } = 0;
        public string ImageLocation { get; set; } = null;
        public bool IsLive { get; set; } = false;
        public bool IsVisited { get; set; } = false;
        public int Row { get; set; }
        public int Column { get; set; }
        public string Text { get; set; }

        /*Expected usage: 
         *
         * When Cell is created, init row and column properties
         * 
         * */
        public CellModel(int column = -1, int row = -1) {
            Reset(column, row);
        }

        public CellModel(int column, int row, int liveNeighbors, bool visited, bool live) {
            this.Column = column;
            this.Row = row;
            this.LiveNeighbors = liveNeighbors;
            this.IsVisited = visited;
            this.IsLive = live;
        }

        public void Reset(int column, int row) {
            this.Column = column;
            this.Row = row;
            this.IsVisited = false;
            this.IsLive = false;
            this.LiveNeighbors = 0;
        }
    }
}