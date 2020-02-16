using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

using AppGlobals = MinesweeperProjectCLC247.Constants.Globals;
using GameBoard = MinesweeperProjectCLC247.Models.GameBoardModel;

namespace GameAPI
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class GameService : IGameService
    {
        //GET: GameBoard from DB
        public DTO GetGameBoard()
        {
            /*
             * 
             * 
             * Add button to either navbar or somewhere on /Game to get saved game from db
             * 
             * 
             */


            //put code here to get game board from db
            return new DTO(AppGlobals.Grid, AppGlobals.timer.Elapsed.TotalSeconds, AppGlobals.numberClicks);
        }

        //PUT: GameBoard into DB
        public bool PutGameBoard()
        {
            /*
             * 
             * Add button to bottom of /Game to save game into db
             * 
             * 
             */


            //put code here to add game board to db
            return true;
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
