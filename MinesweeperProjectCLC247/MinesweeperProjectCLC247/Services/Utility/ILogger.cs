﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MinesweeperProjectCLC247.Services.Utility
{
    public interface ILogger
    {

        void Debug(string message, string arg = null);
        void Info(string message, string arg = null);
        void Warning(string message, string arg = null);
        void Error(string message, string arg = null);
    }
}