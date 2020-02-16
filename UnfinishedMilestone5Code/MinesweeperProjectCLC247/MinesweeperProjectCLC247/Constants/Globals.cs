using MinesweeperProjectCLC247.Models;
using System;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace MinesweeperProjectCLC247.Constants
{
    [DataContract]
    public class Globals
    {
        [DataMember]
        public static Stopwatch timer = new Stopwatch();

        [DataMember]
        public static int numberClicks;

        [DataMember]
        public static GameBoardModel Grid { get; set; }
    }
}