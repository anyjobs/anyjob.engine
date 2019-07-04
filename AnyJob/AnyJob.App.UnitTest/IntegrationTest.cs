﻿using AnyJob.UnitTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace AnyJob.App
{
    [TestClass]
    public class IntegrationTest : JobTestBase
    {
        [TestMethod]
        public void TestPing()
        {
            var inputs = new Dictionary<string, object>()
            {
                { "host" , "127.0.0.1" },
                { "count" , 5 }
            };
            var job = JobEngine.Start("apppack.ping", inputs);
            var result = job.Task.Result;
            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(result.Result is string);
        }
        [TestMethod]
        public void TestFromPackBin()
        {
            var job = JobEngine.Start("apppack.abc", null);
            var result = job.Task.Result;

            Assert.IsTrue(result.IsSuccess);
            var text = Convert.ToString(result.Result);
            Assert.AreEqual("abc", text.Trim());

        }
        [TestMethod]
        public void TestFromGlobalBin()
        {
            var job = JobEngine.Start("apppack.bcd", null);
            var result = job.Task.Result;
            Assert.IsTrue(result.IsSuccess);
            var text = Convert.ToString(result.Result);
            Assert.AreEqual("bcd", text.Trim());
        }
        [TestMethod]
        public void TestOS()
        {
            OSPlatform os = new OSPlatform();
            var win = OSPlatform.Windows;
            string text = os.ToString();

        }
    }
}
