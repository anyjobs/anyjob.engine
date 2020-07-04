﻿using System;
using AnyJob.DependencyInjection;

namespace AnyJob.Impl
{
    [YS.Knife.ServiceClass(Lifetime = Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton)]
    public class DefaultConvertService : IConvertService
    {
        public object Convert(object value, Type targetType)
        {
            try
            {
                return System.Convert.ChangeType(value, targetType);
            }
            catch (Exception ex)
            {
                throw Errors.ConvertError(ex, value, targetType);
            }
        }
    }
}
