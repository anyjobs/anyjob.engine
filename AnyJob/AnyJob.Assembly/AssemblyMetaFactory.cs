﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using AnyJob.Meta;
using System.Linq;
using AnyJob.Assembly.Meta;

namespace AnyJob.Assembly
{
    public class AssemblyMetaFactory : IMetaFactory
    {
        private Dictionary<string, IActionMeta> metaMaps = new Dictionary<string, IActionMeta>(StringComparer.CurrentCultureIgnoreCase);

        #region 构造函数
        public AssemblyMetaFactory()
        {
            LoadCurrentDomain();
            AppDomain.CurrentDomain.AssemblyLoad += (sender, args) => { LoadAssemblyActions(args.LoadedAssembly); };
        }
        #endregion

        #region 接口实现
        public int Priority => 99999;

        public IActionMeta GetMeta(string refId)
        {
            if (metaMaps.TryGetValue(refId, out var meta))
            {
                return meta;
            }
            return null;
        }

        #endregion

        #region 收集Meta信息
        private IActionOutputMeta CreateOutput(Type actionType)
        {
            var outputAttribute = actionType.GetCustomAttribute<ActionOutputAttribute>();
            if (outputAttribute != null)
            {
                return new ActionOutputMeta()
                {
                    Description = outputAttribute.Description,
                    IsRequired = outputAttribute.IsRequired,
                    Type = outputAttribute.Type.AssemblyQualifiedName,
                };
            }
            else
            {
                return new ActionOutputMeta()
                {
                    Description = string.Empty,
                    IsRequired = false,
                    Type = typeof(object).AssemblyQualifiedName
                };
            }
        }

        private IActionInputMeta CreateInput(PropertyInfo property)
        {
            var inputAttribute = property.GetCustomAttribute<ActionInputAttribute>();
            return new ActionInputMeta()
            {
                Name = property.Name.ToLower(),
                DefaultValue = inputAttribute.Default,
                Description = inputAttribute.Description,
                IsRequired = inputAttribute.IsRequired,
                Type = property.PropertyType.AssemblyQualifiedName,
                IsSecret = inputAttribute.IsSecret
            };
        }

        private void LoadCurrentDomain()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                LoadAssemblyActions(assembly);
            }
        }
        private void LoadAssemblyActions(System.Reflection.Assembly assembly)
        {
            var datas = from p in assembly.GetTypes()
                        where p.IsClass && !p.IsAbstract && typeof(IAction).IsAssignableFrom(p) && Attribute.IsDefined(p, typeof(ActionAttribute))
                        let attr = Attribute.GetCustomAttribute(p, typeof(ActionAttribute)) as ActionAttribute
                        let outputMeta = CreateOutput(p)
                        let inputMetas = p.GetProperties().Where(t => Attribute.IsDefined(t, typeof(ActionInputAttribute))).Select(CreateInput)
                        select new ActionMeta()
                        {
                            Ref = attr.RefName,
                            Description = attr.Description,
                            DisplayFormat = attr.DisplayFormat,
                            EntryPoint = p.AssemblyQualifiedName,
                            Kind = ConstCode.ASSEMBLY_ACTION_TYPE,
                            Output = outputMeta,
                            InputMetas = inputMetas
                        };
            //foreach (var map in datas)
            //{
            //    actionMaps[map.Name] = map.Item;
            //}


        }
        #endregion

        #region InnerClass
        public class ActionMeta : IActionMeta
        {
            public string Ref { get; set; }

            public string Description { get; set; }

            public string DisplayFormat { get; set; }

            public string Kind { get; set; }

            public string EntryPoint { get; set; }

            public IEnumerable<IActionInputMeta> InputMetas { get; set; }

            public IActionOutputMeta Output { get; set; }
        }
        public class ActionInputMeta : IActionInputMeta
        {
            public string Name { get; set; }

            public string Description { get; set; }

            public object DefaultValue { get; set; }

            public bool IsRequired { get; set; }

            public bool IsSecret { get; set; }

            public string Type { get; set; }
        }
        public class ActionOutputMeta : IActionOutputMeta
        {
            public string Type { get; set; }

            public string Description { get; set; }

            public bool IsRequired { get; set; }
        }

        #endregion
    }
}
