using Cosmos.System.Graphics;
using SipaaKernelV3.Core;
using SipaaKernelV3.Graphics;
using SipaaKernelV3.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernelV3.Apps
{
    public class AboutApp : Application
    {
        private class AboutWindow : Window
        {
            public AboutWindow() : base("About SipaaKernel", 250, 125, 100, 100, Resources.about)
            {
            }
            public override void Draw(SGraphics g)
            {
                base.Draw(g);
                var theme = UI.SysTheme.ThemeManager.GetCurrentTheme();
                uint x = this.x + 4;
                uint y = this.y + 4;
                g.DrawString(OSInfo.OSName, theme.ForeColor, new Position(x,y));
                y += g.Font.Height;
                g.DrawString("Version " + OSInfo.OSVersion + " (build " + OSInfo.OSBuild + ")", theme.ForeColor, new Position(x, y));
                y += (uint)g.Font.Height;
                g.DrawString("Thanks to : ", theme.ForeColor, new Position(x, y));
                y += (uint)g.Font.Height * 2;
                foreach (string contributor in OSInfo.thanks)
                {
                    g.DrawString(contributor, theme.ForeColor, new Position(x, y));
                    y += g.Font.Height;
                }
            }
        }
        private AboutWindow window;

        public override void AppStart()
        {
            window = new AboutWindow();
            window.visible = true;
        }

        public override void OnDraw(SGraphics g)
        {
            window.Draw(g);
        }

        public override void OnUpdate()
        {
            window.Update();
            if (window.requestingQuit) { RequestQuit(); }
        }
    }
}
