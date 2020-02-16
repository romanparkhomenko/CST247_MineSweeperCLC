/*
 * 
 * 
 * RUN ON: http://localhost:56495/GameService.svc
 * 
 * 
 * 
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using MinesweeperProjectCLC247.Constants;

using globals = MinesweeperProjectCLC247.Constants.Globals;
using Grid = MinesweeperProjectCLC247.Models.GameBoardModel;

namespace APIService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class GameService : IGameService
    {
        public Grid GetGrid()
        {
            return globals.Grid;
        }

        public string GetData(string value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
