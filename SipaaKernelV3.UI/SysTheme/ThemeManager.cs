using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernelV3.UI.SysTheme
{
    public class ThemeManager
    {
        private static ThemeBase currentTheme = new Themes.Dark();

        public static void SetCurrentTheme(ThemeBase theme) { if (currentTheme == null) { return; } currentTheme = theme; }

        public static ThemeBase GetCurrentTheme() { return currentTheme; }
    }
}
