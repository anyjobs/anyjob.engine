﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public interface IConvertService
    {
        object Convert(object value, Type targetType);
    }
}
