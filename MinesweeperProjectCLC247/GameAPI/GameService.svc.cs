using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

using MinesweeperProjectCLC247.Models;
using MinesweeperProjectCLC247.Services;

namespace GameAPI
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class GameService : IGameService {

        List<PublishedGame> games = new List<PublishedGame>();
        Boolean serverTest = false;

        public GameService() {
            DAObusiness gameService = new DAObusiness();
            games = gameService.getAllStats();
        }

        public DTO GetAllStats() {
            DTO dto;

            if (!serverTest) {
                dto = new DTO(1, "Data Server down", null);
            } else if (!games.Any()) {
                dto = new DTO(-1, "No Games found", null);
            } else {
                dto = new DTO(0, "OK", games);
            }
            return dto;
        }

        public string GetData(int value) {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite) {
            if (composite == null) {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue) {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
