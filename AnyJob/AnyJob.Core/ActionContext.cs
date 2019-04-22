﻿using AnyJob.Meta;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public class ActionContext:IValueProvider,IServiceProvider
    {
        private IValueProvider valueProvider;
        private IServiceProvider serviceProvider;
        public ActionContext(IValueProvider valueProvider,IServiceProvider serviceProvider)
        {
            this.valueProvider = valueProvider;
            this.serviceProvider = serviceProvider;
        }
        ActionParameters Parameters { get; }
        ActionMeta Meta { get; }

        public T GetService<T>()
        {
            throw new NotImplementedException();
        }

        public object GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }

        public object GetValue(string key)
        {
            throw new NotImplementedException();
        }
    }
}
