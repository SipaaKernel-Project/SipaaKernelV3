using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using SipaaKernelV3.Apps;
using SipaaKernelV3.Core;
using SipaaKernelV3.Graphics;
using SipaaKernelV3.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernelV3.UI
{
    public class Taskbar
    {
        private Button startMenu;
        private List<Button> taskbarButtons = new List<Button>();
        public uint height = 40;
        public uint buttonWidth = 40;
        public uint buttonHeight = 40;
        public uint x = 0;
        public uint y = 0;

        public Taskbar(SipaVGA vga, uint height = 40)
        {
            this.y = vga.GetResolution().ScreenHeight - height;
            this.height = height;
            startMenu = new Button("", 0, vga.GetResolution().ScreenHeight - height, buttonWidth, buttonHeight);
            startMenu.Bitmap = Resources.SipaaKernelLogo32;
        }

        private void AddX(uint valuetoadd)
        {
            x += valuetoadd;
        }

        private void RemoveX(uint valuetoremove)
        {
            x -= valuetoremove;
        }

        public void AddTaskbarButton(Application app, Bitmap icon)
        {
            AddX(buttonWidth);
            Button b = new Button("", x, y, buttonWidth, buttonHeight);
            b.Bitmap = icon;
            b.Tag = app;
            taskbarButtons.Add(b);
        }

        public void RemoveTaskbarButton(Application app)
        {
            for (int i = 0; i < taskbarButtons.Count; i++)
            {
                var b = taskbarButtons[i];

                if (b.Tag == app)
                {
                    taskbarButtons.Remove(b);
                    RemoveX(buttonWidth);
                }
            }
        }

        public void Draw(SipaVGA v)
        {
            v.DrawFilledRectangle(0, v.GetResolution().ScreenHeight - height, v.GetResolution().ScreenWidth, height, (uint)SysTheme.ThemeManager.GetCurrentTheme().BackColor.ToArgb());
            var time = Cosmos.HAL.RTC.Hour + " : " + Cosmos.HAL.RTC.Minute;
            var timex = v.GetResolution().ScreenWidth - 20 - PCScreenFont.Default.GetStringWidth(time);
            var timey = v.GetResolution().ScreenHeight - height / 2 - (uint)PCScreenFont.Default.Height / 2;
            v.DrawString(time, PCScreenFont.Default, (uint)SysTheme.ThemeManager.GetCurrentTheme().ForeColor.ToArgb(), timex, timey);
            // (useless line) v.DrawString("Taskbar", PCScreenFont.Default, 0xFFFFFF, v.GetResolution().ScreenWidth / 2 - PCScreenFont.Default.GetStringWidth("Taskbar") / 2, v.GetResolution().ScreenHeight - height / 2);
            startMenu.Draw(v);
            foreach (Button b in taskbarButtons)
            {
                b.Draw(v);
            }
            v.DrawHorizontalLine(0, v.GetResolution().ScreenHeight - height - 1, v.GetResolution().ScreenWidth, (uint)SysTheme.ThemeManager.GetCurrentTheme().ClickedBackColor.ToArgb());
        }

        public void Update(StartMenu sm)
        {
            startMenu.Update();
            if (startMenu.ButtonState == ButtonState.Clicked) { sm.visible = !sm.visible; }
            foreach (Button b in taskbarButtons)
            {
                b.Update();
                if (b.ButtonState == ButtonState.Clicked)
                {
                    var app = (Application)b.Tag;
                    app.SendMessage(Message.CHANGEWINDOWVISIBILITY);
                }
            }
        }
    }
}
