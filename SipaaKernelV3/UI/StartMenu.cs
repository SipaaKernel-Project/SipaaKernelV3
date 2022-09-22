using Cosmos.System.Graphics.Fonts;
using SipaaKernelV3.Apps;
using SipaaKernelV3.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernelV3.UI
{
    public class StartMenu
    {
        private Button openPaint, openSipad, openTerminal, openUILibrary, shutdown;
        private static uint tbheight = 40;
        private static uint width = 534;
        private static uint height = 392;
        private static uint vgaHeight = 0;
        private uint x = 0;
        private uint y = 0;
        public bool visible = false;
        public StartMenu(SipaVGA vga, Taskbar tb)
        {
            vgaHeight = vga.GetResolution().ScreenHeight;
            y = vgaHeight - tbheight - height;
            tbheight = tb.height;
            openPaint = new Button("Pixel Paint", 0, vga.GetResolution().ScreenHeight - tbheight - height + 40, 220, 40);
            openSipad = new Button("Sipad", 0, vga.GetResolution().ScreenHeight - tbheight - height + 80, 220, 40);
            openTerminal = new Button("Terminal", 0, vga.GetResolution().ScreenHeight - tbheight - height + 120, 220, 40);
            openUILibrary = new Button("UI Library", 0, vga.GetResolution().ScreenHeight - tbheight - height + 160, 220, 40);
            shutdown = new Button("S", width - tbheight, y + height - tbheight, tbheight, tbheight);
            openPaint.Bitmap = Resources.paint;
            openSipad.Bitmap = Resources.Sipad;
            openTerminal.Bitmap = Resources.consolemode;
            openUILibrary.Bitmap = Resources.consolemode;
            //returnToConsole.Bitmap = Resources.consolemode;
        }
        public void Draw(SipaVGA v)
        {
            if (visible)
            {
                v.DrawFilledRectangle(x, y, width, height, (uint)SysTheme.ThemeManager.GetCurrentTheme().HoveredBackColor.ToArgb());
                v.DrawFilledRectangle(x + 220, y, width - 220, height, (uint)SysTheme.ThemeManager.GetCurrentTheme().BackColor.ToArgb());
                v.DrawString("Hello, SipaaKernel User", PCScreenFont.Default, (uint)SysTheme.ThemeManager.GetCurrentTheme().ForeColor.ToArgb(), 12, y + 12);
                v.DrawString("Featured Apps", PCScreenFont.Default, (uint)SysTheme.ThemeManager.GetCurrentTheme().ForeColor.ToArgb(), 232, y + 12);
                // (useless line) v.DrawString("Taskbar", PCScreenFont.Default, 0xFFFFFF, v.GetResolution().ScreenWidth / 2 - PCScreenFont.Default.GetStringWidth("Taskbar") / 2, v.GetResolution().ScreenHeight - height / 2);
                openPaint.Draw(v);
                openSipad.Draw(v);
                openTerminal.Draw(v);
                openUILibrary.Draw(v);
                shutdown.Draw(v);
            }
        }

        public void Update()
        {
            if (visible)
            {
                openPaint.Update();
                openTerminal.Update();
                openUILibrary.Update();
                openSipad.Update();
                shutdown.Update();
                if (openPaint.ButtonState == ButtonState.Clicked) { Kernel.SKOpenApp(new PaintApp(), Resources.paint); visible = false; }
                if (openSipad.ButtonState == ButtonState.Clicked) { Kernel.SKOpenApp(new NotepadApp(), Resources.Sipad); visible = false; }
                if (openUILibrary.ButtonState == ButtonState.Clicked) { Kernel.SKOpenApp(new UILibrary(), Resources.consolemode); visible = false; }
                if (openTerminal.ButtonState == ButtonState.Clicked) { Kernel.SKOpenApp(new TerminalApp(), Resources.consolemode ); visible = false; }
                if (shutdown.ButtonState == ButtonState.Clicked) { Cosmos.System.Power.Shutdown(); }
            }
        }
    }
}
