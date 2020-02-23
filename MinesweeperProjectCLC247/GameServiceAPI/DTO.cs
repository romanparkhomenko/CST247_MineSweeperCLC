using System.Collections.Generic;
using System.Runtime.Serialization;
using MinesweeperProjectCLC247.Models;

namespace GameServiceAPI
{
    //Data Transfer Object used to transfer data from site to DB and back
    [DataContract]
    public class DTO
    {
        public DTO(int ErrorCode, string ErrorMsg, List<PublishedGame> Data)
        {
            this.ErrorCode = ErrorCode;
            this.ErrorMsg = ErrorMsg;
            this.Data = Data;
        }

        [DataMember]
        public int ErrorCode { get; set; }
        [DataMember]
        public string ErrorMsg { get; set; }
        [DataMember]
        public List<PublishedGame> Data { get; set; }

    }
}