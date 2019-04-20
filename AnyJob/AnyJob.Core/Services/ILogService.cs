﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public interface ILogService
    {
        void Debug(string message, params object[] args);
        void Info(string message, params object[] args);
        void Warn(string message, params object[] args);
        void Error(string message, params object[] args);
    }
}
