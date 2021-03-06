﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyJob.NetCore.Demo
{
    public class TaskResult
    {
        public Task<int> Add(int num1, int num2)
        {
            return Task.FromResult(num1 + num2);
        }

        public Task<string> Concat(string a, string b)
        {
            return Task.FromResult(a + b);
        }
        public Task Hello(string name)
        {
            Console.WriteLine($"Hello,{name}");
            return Task.CompletedTask;
        }

        public Task<List<PersonInfo>> Merge(List<PersonInfo> persons, PersonInfo other)
        {
            var result = new List<PersonInfo>();

            result.AddRange(persons ?? Enumerable.Empty<PersonInfo>());
            if (other != null)
            {
                result.Add(other);
            }
            return Task.FromResult(result);
        }
    }
}
