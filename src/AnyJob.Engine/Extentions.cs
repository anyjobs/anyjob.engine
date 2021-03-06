﻿using System;
using Microsoft.Extensions.DependencyInjection;

namespace AnyJob
{
    public static class Extentions
    {
        public static T GetService<T>(this IActionContext context)
        {
            _ = context ?? throw new ArgumentNullException(nameof(context));
            return context.ServiceProvider.GetService<T>();
        }
        public static T GetRequiredService<T>(this IActionContext context)
        {
            _ = context ?? throw new ArgumentNullException(nameof(context));
            return context.ServiceProvider.GetRequiredService<T>();
        }

        public static string ToUnixPath(this string path)
        {
            _ = path ?? throw new ArgumentNullException(nameof(path));
            return path.Replace('\\', '/');
        }
    }
}
