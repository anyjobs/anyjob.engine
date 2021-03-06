﻿using System;
using System.Threading.Tasks;
using AnyJob.Runner.Workflow.Models;
using AnyJob.Runner.Workflow.Services;

namespace AnyJob.Runner.Workflow
{
    public class WorkflowAction : IAction
    {
        public WorkflowAction(WorkflowInfo workflowInfo)
        {
            this.WorkflowInfo = workflowInfo;
        }
        public WorkflowInfo WorkflowInfo { get; private set; }

        public object Run(IActionContext context)
        {
            var groupRunnerService = context.GetRequiredService<IGroupRunnerService>();
            var dynamicValueService = context.GetRequiredService<IDynamicValueService>();
            var publishValueService = context.GetRequiredService<IPublishValueService>();
            this.PublishGlobalVars(context, publishValueService);
            try
            {
                this.RunSetup(context, groupRunnerService).Wait();
                this.RunBody(context, groupRunnerService).Wait();
                return this.ParseResult(context, dynamicValueService);
            }
            finally
            {
                this.RunTeardown(context, groupRunnerService).Wait();
            }
        }
        protected virtual void PublishGlobalVars(IActionContext context, IPublishValueService publishValueService)
        {
            _ = context ?? throw new ArgumentNullException(nameof(context));
            if (this.WorkflowInfo == null || this.WorkflowInfo.GlobalVars == null) return;
            publishValueService.PublishGlobalVars(context.Parameters, this.WorkflowInfo.GlobalVars);
        }
        protected virtual Task RunSetup(IActionContext context, IGroupRunnerService groupRunnerService)
        {
            _ = context ?? throw new ArgumentNullException(nameof(context));
            _ = groupRunnerService ?? throw new ArgumentNullException(nameof(groupRunnerService));
            if (this.WorkflowInfo == null || this.WorkflowInfo.Setup == null) return Task.CompletedTask;
            return groupRunnerService.RunGroup(context, this.WorkflowInfo.Setup);
        }
        protected virtual Task RunBody(IActionContext context, IGroupRunnerService groupRunnerService)
        {
            _ = context ?? throw new ArgumentNullException(nameof(context));
            _ = groupRunnerService ?? throw new ArgumentNullException(nameof(groupRunnerService));
            if (this.WorkflowInfo == null || this.WorkflowInfo.Body == null) return Task.CompletedTask;
            return groupRunnerService.RunGroup(context, this.WorkflowInfo.Body);
        }
        protected virtual Task RunTeardown(IActionContext context, IGroupRunnerService groupRunnerService)
        {
            _ = context ?? throw new ArgumentNullException(nameof(context));
            _ = groupRunnerService ?? throw new ArgumentNullException(nameof(groupRunnerService));
            if (this.WorkflowInfo == null || this.WorkflowInfo.TearDown == null) return Task.CompletedTask;
            return groupRunnerService.RunGroup(context, this.WorkflowInfo.TearDown);
        }
        protected virtual object ParseResult(IActionContext context, IDynamicValueService dynamicValueService)
        {
            _ = context ?? throw new ArgumentNullException(nameof(context));
            _ = dynamicValueService ?? throw new ArgumentNullException(nameof(dynamicValueService));
            if (this.WorkflowInfo == null) return null;
            return dynamicValueService.GetDynamicValue(this.WorkflowInfo.Output, context.Parameters);
        }

    }
}
