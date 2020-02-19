using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinesweeperProjectCLC247.Models {
    public class CellModel {
        public int ID { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public int LiveNeighbors { get; set; } = 0;
        public bool IsLive { get; set; } = false;
        public bool IsVisited { get; set; } = false;

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