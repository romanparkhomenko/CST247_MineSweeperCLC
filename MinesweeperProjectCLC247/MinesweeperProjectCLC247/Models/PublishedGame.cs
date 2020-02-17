using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MinesweeperProjectCLC247.Models {
    [DataContract]
    public class PublishedGame {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int userID { get; set; }
        [DataMember]
        public int Clicks { get; set; }
        [DataMember]
        public string Time { get; set; }

        public PublishedGame(int id, int userid, int clicks, string time) {
            this.ID = id;
            this.userID = userid;
            this.Clicks = clicks;
            this.Time = time;
        }
    }
}