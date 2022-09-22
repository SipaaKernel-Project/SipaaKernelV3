using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cosmos.System;
using Cosmos.System.Graphics.Fonts;
using SipaaKernelV3.Core.Keyboard;
using SipaaKernelV3.Graphics;
using SipaaKernelV3.UI.SysTheme;

namespace SipaaKernelV3.UI
{
    public class TextBox : Control
    {
        // Fields, properties and constructors
        private string text = "";
        private uint width = 150, height = 40;
        private bool focus = false;
        private bool multiline = false;
        private bool unfocusOnEnter;
        private KBStringReader r;
        private byte tick;
        private byte prevSec;
        private bool cursor;
        private bool passwordFilter;
        private int textW;

        public string Text { get { return text; } }
        public bool Focus { get { return focus; } set { focus = value; } }
        public uint Width { get { return width; } set { width = value; } }
        public uint Height { get { return height; } set { height = value; } }
        public bool Multiline { get => multiline; set { multiline = value; r.acceptNewLine = value; } }
        public bool PasswordFilter { get => passwordFilter; set => passwordFilter = value; }
        public bool UnfocusOnEnter { get => unfocusOnEnter; set => unfocusOnEnter = value; }

        public TextBox(uint x, uint y)
        {
            r = new KBStringReader();
            this.X = x;
            this.Y = y;
        }

        public override void Draw(SipaVGA c)
        {
            var theme = ThemeManager.GetCurrentTheme();
            c.DrawFilledRectangle(X, Y, width, height, (uint)theme.BackColor.ToArgb());

            if (text.Length > 0)
            {
                if (!passwordFilter && multiline)
                {
                    c.DrawString(text, PCScreenFont.Default, (uint)theme.ForeColor.ToArgb(), X + 4, Y + 4);
                }
                else if (!passwordFilter)
                {
                    c.DrawString(text, PCScreenFont.Default, (uint)theme.ForeColor.ToArgb(), X + 4, Y + (height / 2) - 4);
                }
                else if (passwordFilter && multiline)
                {
                    uint sx = X + 4;
                    for (int i = 0; i < text.Length; i++)
                    {
                        c.DrawChar('*', PCScreenFont.Default, (uint)theme.ForeColor.ToArgb(), sx, Y + 4);
                        sx += PCScreenFont.Default.Width;
                    }
                }
                else if (passwordFilter)
                {
                    uint sx = X + 4;
                    for (int i = 0; i < text.Length; i++)
                    {
                        c.DrawChar('*', PCScreenFont.Default, (uint)theme.ForeColor.ToArgb(), sx, Y + 4);
                        sx += PCScreenFont.Default.Width;
                    }
                }
            }

        }

        public override void Update()
        {
            textW = text.Length * PCScreenFont.Default.Width;
            if (textW < 0) { textW = 0; }

            if (MouseManager.X > X && MouseManager.X < X + Width && MouseManager.Y > Y && MouseManager.Y < Y + Height)
            {
                if (MouseManager.MouseState == MouseState.Left)
                {
                    focus = true;
                }
            }


            // when focused
            if (focus)
            {
                if (KBPS2.IsKeyDown(ConsoleKeyEx.Escape)) { focus = false; }
                else
                {
                    tick = Cosmos.HAL.RTC.Second;
                    if (tick != prevSec)
                    {
                        cursor = !cursor;
                        prevSec = tick;
                    }
                    r.GetInput();
                    text = r.output;
                }
            }
        }
    }
}
