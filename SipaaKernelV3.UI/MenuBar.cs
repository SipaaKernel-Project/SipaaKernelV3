using Cosmos.HAL;
using Cosmos.System.Graphics.Fonts;
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
        public void Draw(SipaVGA g)
        {
            var theme = SysTheme.ThemeManager.GetCurrentTheme();
            g.DrawFilledRectangle(0, 0, g.GetResolution().ScreenWidth, 24, (uint)theme.BackColor.ToArgb());
            g.DrawString("SipaaKernel V3",PCScreenFont.Default, (uint)theme.ForeColor.ToArgb(), 4, 4);
            string time = RTC.Hour.ToString() + ":" + RTC.Minute.ToString();
            g.DrawString(time, PCScreenFont.Default, (uint)theme.ForeColor.ToArgb(),g.GetResolution().ScreenWidth / 2 - PCScreenFont.Default.GetStringWidth(time) / 2, 4);
        }

        public void Update()
        {
        }
    }
}
