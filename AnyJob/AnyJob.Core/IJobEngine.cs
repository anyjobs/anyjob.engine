﻿using AnyJob.DI;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public interface IJobEngine: IServiceContainer,IJobService
    {

    }
}
