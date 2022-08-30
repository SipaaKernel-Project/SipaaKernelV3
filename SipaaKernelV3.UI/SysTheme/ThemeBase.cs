using SipaaKernelV3.Graphics;

namespace SipaaKernelV3.UI.SysTheme
{
    public class ThemeBase
    {
        private int themeId;
        private string themeName;
        private Color backColor, hoveredBackColor, clickedBackColor, foreColor, borderColor, appBackColor;
        private uint borderSize;

        public int ThemeId { get { return themeId; } set { themeId = value; } }
        public uint BorderSize { get { return borderSize; } set { borderSize = value; } }
        public string ThemeName { get { return themeName; } set { themeName = value; } }
        public Color BackColor { get { return backColor; } set { backColor = value; } }
        public Color HoveredBackColor { get { return hoveredBackColor; } set { hoveredBackColor = value; } }
        public Color ClickedBackColor { get { return clickedBackColor; } set { clickedBackColor = value; } }
        public Color ForeColor { get { return foreColor; } set { foreColor = value; } }
        public Color BorderColor { get { return borderColor; } set { borderColor = value; } }
        public Color AppBackColor { get { return appBackColor; } set { appBackColor = value; } }
    }
}