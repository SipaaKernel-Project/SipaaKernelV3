using Cosmos.System;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using SipaaKernelV3.Graphics;
using SipaaKernelV3.UI.SysTheme;

namespace SipaaKernelV3.UI
{
    public class Button : Control
    {
        private string text = "Button";
        private uint width = 150, height = 40, borderradius = 11;
        private ButtonState state;
        private ThemeBase theme = ThemeManager.GetCurrentTheme();
        private Bitmap bmp;

        public string Text { get { return text; } set { text = value; } }
        public uint Width { get { return width; } set { width = value; } }
        public uint Height { get { return height; } set { height = value; } }
        public uint BorderRadius { get { return borderradius; } set { borderradius = value; } }
        public ButtonState ButtonState { get { return state; } }
        public ThemeBase Theme { get { return theme; } set { theme = value; } }
        public Bitmap Bitmap { get { return bmp; } set { bmp = value; } }

        public Button(string text, uint x, uint y, uint width, uint height)
        {
            this.text = text;
            this.X = x;
            this.Y = y;
            this.width = width;
            this.height = height;
        }
        public Button(string text, uint x, uint y)
        {
            this.text = text;
            this.X = x;
            this.Y = y;
        }

        public override void Draw(SGraphics c)
        {
            if (borderradius >= 1)
            {
                // Draw button rectangle 
                switch (this.state)
                {
                    case ButtonState.Idle:
                        c.DrawFilledRectangle(theme.BackColor, new Position(X, Y),new Size(width, height), borderradius);
                        break;
                    case ButtonState.Hover:
                        c.DrawFilledRectangle(theme.HoveredBackColor, new Position(X, Y), new Size(width, height), borderradius);
                        break;
                    case ButtonState.Clicked:
                        c.DrawFilledRectangle(theme.ClickedBackColor, new Position(X, Y), new Size(width, height), borderradius);
                        break;
                }
            }
            else
            {
                // Draw button rectangle 
                switch (this.state)
                {
                    case ButtonState.Idle:
                        c.DrawFilledRectangle(theme.BackColor, new Position(X, Y), new Size(width, height));
                        break;
                    case ButtonState.Hover:
                        c.DrawFilledRectangle(theme.HoveredBackColor, new Position(X, Y), new Size(width, height));
                        break;
                    case ButtonState.Clicked:
                        c.DrawFilledRectangle(theme.ClickedBackColor, new Position(X, Y), new Size(width, height));
                        break;
                }
            }
            if (theme.BorderSize > 1)
            {
                c.DrawRectangle(theme.BorderColor, theme.BorderSize, new Position(X, Y), new Size(width, height));
            }
            // Draw text & bitmap under the button
            uint sx = (uint)(X + (width / 2) - (text.Length * PCScreenFont.Default.Width / 2));
            c.DrawString(Text, theme.ForeColor, new Position(sx, (uint)this.Y + (uint)this.Height / 2 - (uint)PCScreenFont.Default.Height / 2));
            if (bmp != null)
            {
                c.DrawBitmap(bmp, new Position(X + 8, this.Y + (uint)this.Height / 2 - (uint)bmp.Height / 2), true);
            }
        }

        public override void Update()
        {
            if (MouseManager.X > X && MouseManager.X < X + Width && MouseManager.Y > Y && MouseManager.Y < Y + Height)
            {
                if (MouseManager.MouseState == MouseState.Left)
                {
                    state = ButtonState.Clicked;
                }
                else
                {
                    state = ButtonState.Hover;
                }
            }
            else
            {
                state = ButtonState.Idle;
            }
        }
    }
    public enum ButtonState
    {
        Idle,
        Clicked,
        Hover
    }
}