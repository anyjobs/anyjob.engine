﻿using AnyJob.Meta;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public interface IActionEntry
    {
        IActionMeta MetaInfo { get; }

        IAction CreateInstance(IActionParameters parameters);
    }
}