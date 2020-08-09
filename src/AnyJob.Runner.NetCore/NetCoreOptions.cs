﻿using System;
using System.Collections.Generic;
using System.Text;
using YS.Knife;

namespace AnyJob.Runner.NetCore
{

    [OptionsClass("netcore")]
    public class NetCoreOptions
    {
        public string DotnetPath { get; set; } = "dotnet";
        public string WrapperPath { get; set; } = "global/netcore/3.1/NetCore_Wrapper.dll";
    }
}