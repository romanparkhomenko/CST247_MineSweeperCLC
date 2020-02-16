using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

using Grid = MinesweeperProjectCLC247.Models.GameBoardModel;

namespace GameAPI
{
    //Data Transfer Object used to transfer data from site to DB and back
    [DataContract]
    public class DTO
    {
        [DataMember]
        List<MinesweeperProjectCLC247.Models.CellModel> Cells { get; set; }

        [DataMember]
        double timer { get; set; }

        [DataMember]
        int Clicks { get; set; }

        [DataMember]
        public int SizeX { get; set; }

        [DataMember]
        public int SizeY { get; set; }

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
            this.SizeX = grid.Cols;
            this.SizeY = grid.Rows;
        }

    }
}