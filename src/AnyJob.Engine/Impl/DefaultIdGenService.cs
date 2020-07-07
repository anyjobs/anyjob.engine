﻿using System;
using AnyJob.DependencyInjection;

namespace AnyJob.Impl
{
    [YS.Knife.ServiceClass(Lifetime = Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton)]
    public class DefaultIdGenService : IIdGenService
    {
        public string NewId()
        {
            return NewSequenceGuid().ToString();
        }

        private Guid NewSequenceGuid()
        {
            byte[] timestampBytes = BitConverter.GetBytes(DateTimeOffset.Now.Ticks);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(timestampBytes);
            }
            var byteArray = Guid.NewGuid().ToByteArray();
            Buffer.BlockCopy(timestampBytes, 0, byteArray, 0, timestampBytes.Length);
            return new Guid(byteArray);
        }
    }
}