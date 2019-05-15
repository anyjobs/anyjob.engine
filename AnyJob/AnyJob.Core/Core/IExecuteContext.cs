﻿using System;
using System.Threading;

namespace AnyJob
{
    /// <summary>
    /// 表示一次执行的上下文环境
    /// </summary>
    public interface IExecuteContext
    {
        /// <summary>
        /// 获取Action的执行参数
        /// </summary>
        IActionParameters ActionParameters { get; }
        /// <summary>
        /// 或者执行的Action的Ref名称
        /// </summary>
        string ActionRef { get; }
        /// <summary>
        /// 获取取消执行的Token
        /// </summary>
        CancellationToken Token { get; }
        /// <summary>
        /// 获取执行的路径
        /// </summary>
        IExecutePath ExecutePath { get; }

        /// <summary>
        /// 表示失败重试次数
        /// </summary>
        int ActionRetryCount { get; }
    }
}