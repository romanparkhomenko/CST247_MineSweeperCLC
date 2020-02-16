using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

using GameBoard = MinesweeperProjectCLC247.Models.GameBoardModel;

/*
 * 
 * 
 * 
 * Store save games in database as JSON with PK on int ID row and FK on UserTable.UserID row
 *
 *  WILL NOT WORK DUE TO INABILITY TO HANDLE TWO D ARRAYS!!!!!!!!!!!
 * 
 */






namespace GameAPI
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IGameService
    {
        /*Use in controller to retrieve game board from DB*/
        //GET: GameBoard from DB
        [OperationContract]
        [WebGet(UriTemplate = "GetGameBoard")]
        DTO GetGameBoard();

        /*user in controller to put games in DB*/
        //PUT: GambeBoard into DB
        [OperationContract]
        [WebInvoke(UriTemplate = "PutGameBoard")]
        bool PutGameBoard();

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetData/{value}")]
        string GetData(string value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
