// UI Library of SipaaKernel, like the WinUI 2 gallery
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SipaaKernelV3.Core;
using SipaaKernelV3.Graphics;
using SipaaKernelV3.UI;

namespace SipaaKernelV3.Apps
{
    public class UILibrary : Application
    {
        public class UILibraryWindow : Window
        {
            public UILibraryWindow() : base(Kernel.g /**💀**/, "UI Library", 200, 200, false, 150, 150)
            {

            }
        }

        UILibraryWindow window;

        public override void AppStart()
        {
            window = new UILibraryWindow();
            window.visible = true;
        }

        public override void OnDraw(SipaVGA g)
        {
            window.Draw(g);
        }

        public override void OnUpdate()
        {
            window.Update();
            if (window.requestingQuit) { this.RequestQuit(); }
        }

        public override void OnMessageReceived(Message m)
        {
            base.OnMessageReceived(m);
            if (m == Message.CHANGEWINDOWVISIBILITY)
            {
                window.visible = !window.visible;
            }
        }
    }
}
