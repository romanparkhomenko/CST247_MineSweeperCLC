using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

using Cell = MinesweeperProjectCLC247.Models.CellModel;
using Grid = MinesweeperProjectCLC247.Models.GameBoardModel;

namespace APIService
{
    //Data Transfer Object used to transfer data from site to DB and back
    [DataContract]
    public class DTO
    {
        [DataMember]
        List<Cell> Cells { get; set; }

        [DataMember]
        double timer { get; set; }

        [DataMember]
        int Clicks { get; set; }

        public DTO(Grid grid, double time, int clicks)
        {

            //loop through rows and columns of passed in game board and create list of all cells; save in "Cells" var
            for (int row = 0; row < grid.Rows; row++)
            {
                for (int col = 0; col < grid.Cols; col++)
                {
                    Cells.Add(grid.Cells[row, col]);
                }
            }
            this.timer = time;
            this.Clicks = clicks;
        }

    }
}