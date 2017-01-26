﻿using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.Core.CPUInfo;
using Sys = Cosmos.System;
using Cosmos.TestRunner;

namespace Cosmos.Kernel.Tests.CPUInfo
{
    public class Kernel : Sys.Kernel
    {
        protected override void BeforeRun()
        {
            Console.WriteLine("Cosmos booted successfully. Type a line of text to get it echoed back.");
            Console.WriteLine(Sys.CPUInfo.GetVendorName());
        }

        protected override void Run()
        {
            //Sys.CPUInfo.ParseSMBIOS();
            mDebugger.Send("Test completed");
            TestController.Completed();
        }
    }
}