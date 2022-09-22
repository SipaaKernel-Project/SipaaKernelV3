using Cosmos.System;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using SipaaKernelV3.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernelV3.UI
{
    public class Window
    {
        public bool HasWindowMoving = false;

        public Bitmap Icon;

        public readonly uint baseWidth;
        public readonly uint baseHeight;
        public readonly uint width;
        public readonly uint height;

        public uint dockX;
        public uint dockY;
        public uint dockWidth = 40;
        public uint dockHeight = 30;
        public uint borderRadius = 11;
        public uint baseX;
        public uint baseY;
        public uint x;
        public uint y;
        public string text;
        public bool visible = false;
        public bool requestingQuit = false;
        public bool fullscreenWindow = false;

        int px;
        int py;
        bool lck = false;

        bool pressed;

        const int TitleBarHeight = 32;
        Button closeButton;

        public Window(SipaVGA vga, string text, uint width, uint height,bool fullscreenMode = false, uint x = 0, uint y = 0, Bitmap icon = null)
        {
            if (fullscreenMode)
            {
                fullscreenWindow = true;
                baseWidth = vga.GetResolution().ScreenWidth;
                baseHeight = vga.GetResolution().ScreenHeight;
                baseX = 0;
                baseY = 0;
            }
            else
            {
                baseWidth = width;
                baseHeight = height;
                baseX = x;
                baseY = y;
            }
            this.x = x + 2;
            this.y = y + TitleBarHeight;
            this.width = width - 4;
            this.height = height - TitleBarHeight - 1;
            this.Icon = icon;
            this.text = text;
            this.closeButton = new Button("X", x + width - 32, baseY, 32, 32);
        }

        public virtual void Draw(SipaVGA g)
        {
            if (visible)
            {
                var theme = SysTheme.ThemeManager.GetCurrentTheme();
                g.DrawFilledRectangle(baseX, baseY, baseWidth, baseHeight, (uint)theme.AppBackColor.ToArgb());
                g.DrawFilledRectangle(baseX, baseY, baseWidth, TitleBarHeight, (uint)theme.BackColor.ToArgb());
                if (Icon != null)
                {
                    g.DrawImageAlpha(Icon, baseX + 6, baseY + TitleBarHeight / 2 - (uint)Icon.Height / 2, (uint)Color.MakeArgb(0, 0,0,0));
                    g.DrawString(text, PCScreenFont.Default, (uint)theme.ForeColor.ToArgb(), baseX + Icon.Width + 10, baseY + TitleBarHeight / 2 - (uint)PCScreenFont.Default.Height / 2);
                }
                else
                {
                    g.DrawString(text, PCScreenFont.Default, (uint)theme.ForeColor.ToArgb(), baseX + 6, baseY + TitleBarHeight / 2 - (uint)PCScreenFont.Default.Height / 2);
                }
                closeButton.Draw(g);
            }
        }

        public virtual void Update()
        {
            if (visible)
            {
                if (MouseManager.MouseState == MouseState.Left)
                {
                    if (!HasWindowMoving && !fullscreenWindow & MouseManager.X > baseX && MouseManager.X < baseX + baseWidth && MouseManager.Y > baseY && MouseManager.Y < baseY + TitleBarHeight)
                    {
                        HasWindowMoving = true;

                        this.pressed = true;
                        if (!lck)
                        {
                            px = (int)((int)MouseManager.X - this.baseX);
                            py = (int)((int)MouseManager.Y - this.baseY);
                            lck = true;
                        }
                    }
                }
                else
                {
                    pressed = false;
                    lck = false;
                    HasWindowMoving = false;
                }

                if (pressed)
                {
                    baseX = (uint)(MouseManager.X - px);
                    baseY = (uint)(MouseManager.Y - py);

                    x = (uint)(MouseManager.X - px + 2);
                    y = (uint)(MouseManager.Y - py + TitleBarHeight);
                }
                closeButton.X = x + width - closeButton.Width;
                closeButton.Y = baseY;
                closeButton.Update();
                if (closeButton.ButtonState == ButtonState.Clicked)
                {
                    visible = !visible;
                    requestingQuit = true;
                }
            }
        }
    }
}
