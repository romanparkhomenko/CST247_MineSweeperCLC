﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinesweeperProjectCLC247.Models
{
    public class Cell: Html.Button
    {
        public bool IsClicked { get; set; } = false;
        public int LiveNeighbors { get; set; } = 0;
        public string ImageLocation { get; set; } = null;
        public bool IsLive { get; set; } = false;
        public bool IsVisited { get; set; } = false;
        public int Row { get; set; }
        public int Column { get; set; }

        /*Expected usage: 
         *
         * When Cell is created, init row and column properties
         * 
         * */
        public Cell(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }
        
        public void OnClick()
        {

        }

        
    }
}