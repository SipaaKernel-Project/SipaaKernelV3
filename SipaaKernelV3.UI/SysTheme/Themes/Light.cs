using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SipaaKernelV3.Graphics;

namespace SipaaKernelV3.UI.SysTheme.Themes
{
    public class Light : ThemeBase
    {
        public Light()
        {
            ThemeId = 1;
            ThemeName = "SipaaKernel Light";

            BorderSize = 0;
            BorderColor = Color.White;

            BackColor = new Color(223, 223, 223);
            HoveredBackColor = new Color(191, 191, 191);
            ClickedBackColor = new Color(243, 243, 243);
            AppBackColor = Color.White;

            ForeColor = Color.Black;
        }
    }
}
