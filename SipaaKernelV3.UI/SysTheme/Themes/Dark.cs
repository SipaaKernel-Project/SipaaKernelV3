using SipaaKernelV3.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernelV3.UI.SysTheme.Themes
{
    public class Dark : ThemeBase
    {
        public Dark()
        {
            ThemeId = 0;
            ThemeName = "SipaaKernel Dark";

            BorderSize = 0;
            BorderColor = Color.Black;

            BackColor = new Color(32, 32, 32);
            HoveredBackColor = new Color(64, 64, 64);
            ClickedBackColor = new Color(12, 12, 12);
            AppBackColor = Color.Black;

            ForeColor = Color.White;
        }
    }
}
