using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using Cosmos.System.Graphics;
using SipaaKernelV3.Graphics;
using Cosmos.System.Graphics.Fonts;
using SipaaKernelV3.Core.Keyboard;
using Cosmos.HAL;

namespace TestKernel
{
    
    public class Kernel : Sys.Kernel
    {
        VGADriver driver;
        protected override void BeforeRun()
        {
            driver = new VGADriver();
            driver.SetGraphicsMode()
        }

        int delta, frames, fps;

        protected override void Run()
        {
            if (delta != Cosmos.HAL.RTC.Second)
            {
                delta = Cosmos.HAL.RTC.Second;
                fps = frames;
                this.mDebugger.Send("[INFO] VGA FPS : " + fps);
                frames = 0;
            }
        }
    }
}
