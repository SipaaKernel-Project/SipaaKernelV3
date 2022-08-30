using Cosmos.System;
using Cosmos.System.Graphics;
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
        public static bool HasWindowMoving = false;

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

        int px;
        int py;
        bool lck = false;

        bool pressed;

        const int TitleBarHeight = 32;
        Button closeButton;

        public Window(string text, uint width, uint height, uint x = 0, uint y = 0, Bitmap icon = null)
        {
            baseWidth = width;
            baseHeight = height;
            baseX = x;
            baseY = y;

            this.x = x + 2;
            this.y = y + TitleBarHeight;
            this.width = width - 4;
            this.height = height - TitleBarHeight - 1;
            this.Icon = icon;
            this.text = text;
            this.closeButton = new Button("X", x + width - 32, baseY, 32, 32);
        }

        public virtual void Draw(SGraphics g) 
        {
            if (visible)
            {
                var theme = SysTheme.ThemeManager.GetCurrentTheme();
                g.DrawFilledRectangle(theme.ClickedBackColor, new Position(baseX, baseY), new Size(baseWidth, baseHeight), borderRadius);
                g.DrawFilledRectangle(theme.BackColor, new Position(baseX, baseY), new Size(baseWidth, TitleBarHeight), borderRadius);
                if (Icon != null)
                {
                    g.DrawBitmap(Icon, new Position(baseX + 6, baseY + TitleBarHeight / 2 - (uint)Icon.Height / 2), true);
                    g.DrawString(text, theme.ForeColor, new Position(baseX + Icon.Width + 10, baseY + TitleBarHeight / 2 - (uint)g.Font.Height / 2));
                }
                else
                {
                    g.DrawString(text, theme.ForeColor, new Position(baseX + 6, baseY + TitleBarHeight / 2 - (uint)g.Font.Height / 2));
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
                    if (!HasWindowMoving && MouseManager.X > baseX && MouseManager.X < baseX + baseWidth && MouseManager.Y > baseY && MouseManager.Y < baseY + TitleBarHeight)
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
