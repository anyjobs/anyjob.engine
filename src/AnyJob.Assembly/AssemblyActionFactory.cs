﻿using System;
using System.Linq;
using AnyJob.DependencyInjection;
namespace AnyJob.Assembly
{
    [YS.Knife.ServiceClass(Lifetime = Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton)]
    [YS.Knife.DictionaryKey("assembly")]
    public class AssemblyActionFactory : IActionFactoryService
    {
        public AssemblyActionFactory(IConvertService convertService)
        {
            this.convertService = convertService;
        }
        private IConvertService convertService;

        public IAction CreateAction(IActionContext actionContext)
        {
            string typeName = actionContext.MetaInfo.EntryPoint;
            var actionType = Type.GetType(typeName);
            var instance = Activator.CreateInstance(actionType);
            SetInputProperties(actionContext, actionType, instance);
            return instance as IAction;
        }
        protected void SetInputProperties(IActionContext actionContext, Type type, object instance)
        {
            var propsMap = type.GetProperties().ToDictionary(p => p.Name, StringComparer.CurrentCultureIgnoreCase);
            foreach (var kv in actionContext.Parameters.Arguments)
            {
                var prop = propsMap[kv.Key];
                var value = this.convertService.Convert(kv.Value, prop.PropertyType);
                prop.SetValue(instance, value);
            }
        }




    }
}