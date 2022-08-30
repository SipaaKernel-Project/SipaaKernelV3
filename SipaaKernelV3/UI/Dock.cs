using SipaaKernelV3.Apps;
using SipaaKernelV3.Core;
using SipaaKernelV3.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernelV3.UI
{
    public class Dock
    {
        public Button openAbout, returnToConsole;

        public Dock(SGraphics g)
        {
            openAbout = new Button("", 40, g.GetHeight() - 48, 40, 40);
            returnToConsole = new Button("", g.GetWidth() - 80, g.GetHeight() - 48, 40, 40);

            returnToConsole.Bitmap = Resources.consolemode;
            openAbout.Bitmap = Resources.about;
        }
        public void Draw(SGraphics g)
        {
            var theme = SysTheme.ThemeManager.GetCurrentTheme();
            g.DrawFilledRectangle(theme.BackColor, new Position(40, g.GetHeight() - 48), new Size(g.GetWidth() - 40 * 2, 40), 11);
            openAbout.Draw(g);
            returnToConsole.Draw(g);
        }

        public void Update(AppManager appManager)
        {
            openAbout.Update();
            returnToConsole.Update();
            if (openAbout.ButtonState == ButtonState.Clicked) { appManager.OpenApp(new AboutApp()); }
            if (returnToConsole.ButtonState == ButtonState.Clicked) { Kernel.SKLoadConsole(); }
        }
    }
}
