using Cosmos.System;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using SipaaKernelV3.Graphics;

namespace SipaaKernelV3.UI
{
    public class Button : Control
    {
        private string text = "Button";
        private uint width = 150, height = 40, borderradius = 11;
        private ButtonState state;
        private Bitmap bmp;
        private Object tag;

        public Object Tag { get => tag; set => tag = value; }
        public string Text { get { return text; } set { text = value; } }
        public uint Width { get { return width; } set { width = value; } }
        public uint Height { get { return height; } set { height = value; } }
        public uint BorderRadius { get { return borderradius; } set { borderradius = value; } }
        public ButtonState ButtonState { get { return state; } }
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

        public override void Draw(SipaVGA c)
        {
            var theme = SysTheme.ThemeManager.GetCurrentTheme();
                // Draw button rectangle 
                switch (this.state)
                {
                    case ButtonState.Idle:
                        c.DrawFilledRectangle(X, Y,width, height, (uint)theme.BackColor.ToArgb());
                        break;
                    case ButtonState.Hover:
                        c.DrawFilledRectangle(X, Y, width, height, (uint)theme.HoveredBackColor.ToArgb());
                        break;
                    case ButtonState.Clicked:
                        c.DrawFilledRectangle(X, Y,width, height, (uint)theme.ClickedBackColor.ToArgb());
                        break;
                }
            // Draw text & bitmap under the button
            uint sx = (uint)(X + (width / 2) - (text.Length * PCScreenFont.Default.Width / 2));
            c.DrawString(Text, PCScreenFont.Default, (uint)theme.ForeColor.ToArgb(), sx, (uint)this.Y + (uint)this.Height / 2 - (uint)PCScreenFont.Default.Height / 2);
            if (bmp != null & !String.IsNullOrEmpty(Text))
            {
                c.DrawImageAlpha(bmp, X + 8, this.Y + (uint)this.Height / 2 - (uint)bmp.Height / 2, (uint)Color.MakeArgb(0, 0, 0, 0));
            }else if (bmp != null & String.IsNullOrEmpty(Text))
            {
                c.DrawImageAlpha(bmp, this.X + this.Width / 2 - bmp.Width / 2, this.Y + this.Height / 2 - (uint)bmp.Height / 2, (uint)Color.MakeArgb(0,0,0,0));
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