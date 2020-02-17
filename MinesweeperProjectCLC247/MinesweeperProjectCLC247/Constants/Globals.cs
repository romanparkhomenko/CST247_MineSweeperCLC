using MinesweeperProjectCLC247.Models;
using System;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinesweeperProjectCLC247.Constants
{
    public class Globals
    {
        public static Stopwatch timer = new Stopwatch();
        public static int numberClicks;
        public static GameBoardModel Grid { get; set; }
    }
}