using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cosmos.System;
using Cosmos.System.Graphics;
using SipaaKernelV3.Core;
using SipaaKernelV3.Graphics;
using SipaaKernelV3.UI;

namespace SipaaKernelV3.Apps
{
    public class TerminalApp : Application
    {
        private class TerminalWindow : Window
        {
            public TerminalWindow() : base(Kernel.g, "Terminal", 300, 200, false, 150, 150, Resources.consolemode)
            {

            }
        }
        TerminalWindow window;
        public override void AppStart()
        {
            window = new TerminalWindow();
            window.visible = true;
        }

        public override void OnDraw(SipaVGA g)
        {
            window.Draw(g);
        }

        public override void OnUpdate()
        {
            window.Update();
        }

        public override void OnMessageReceived(Message m)
        {
            base.OnMessageReceived(m);
            switch (m)
            {
                case Message.CHANGEWINDOWVISIBILITY:
                    window.visible = !window.visible;
                    break;
            }
        }
    }
}
