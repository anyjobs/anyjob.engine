﻿using AnyJob.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace AnyJob.Python
{
    [ServiceImplClass(Key = "python")]
    public class PythonActionFactory : IActionFactoryService
    {
        public PythonActionFactory(IOptions<PythonOption> option)
        {
            this.option = option;
        }
        private IOptions<PythonOption> option;
        public IAction CreateAction(IActionContext actionContext)
        {
            var entryFile = actionContext.MetaInfo.EntryPoint;
            this.AssertEntryFileExits(entryFile, actionContext);
            return new PythonAction(option.Value, entryFile);
        }

        private void AssertEntryFileExits(string entryFile, IActionContext actionContext)
        {
            if (System.IO.Path.IsPathRooted(entryFile))
            {

            }
            string fullPath = System.IO.Path.GetFullPath(entryFile, actionContext.RuntimeInfo.WorkingDirectory);
            if (!System.IO.File.Exists(fullPath))
            {

            }
        }
        
    }
}