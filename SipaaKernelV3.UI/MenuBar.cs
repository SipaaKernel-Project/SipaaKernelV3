using Cosmos.HAL;
using SipaaKernelV3.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernelV3.UI
{
    public class MenuBar
    {
        public void Draw(SGraphics g)
        {
            var theme = SysTheme.ThemeManager.GetCurrentTheme();
            g.DrawFilledRectangle(theme.BackColor, new Position(0, 0), new Size(g.GetWidth(), 24));
            g.DrawString("SipaaKernel V3", theme.ForeColor, new Position(4, 4));
            string time = RTC.Hour.ToString() + ":" + RTC.Minute.ToString();
            g.DrawString(time, theme.ForeColor, new Position(g.GetWidth() / 2 - g.Font.GetStringWidth(time) / 2, 4));
        }

        public void Update()
        {
        }
    }
}
