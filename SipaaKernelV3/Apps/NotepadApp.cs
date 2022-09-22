using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cosmos.System;
using Cosmos.System.Graphics;
using SipaaKernelV3.Core;
using SipaaKernelV3.Graphics;
using SipaaKernelV3.UI;

namespace SipaaKernelV3.Apps
{
    public class NotepadApp : Application
    {
        private class NotepadWindow : Window
        {
            TextBox SipadTB;

            public NotepadWindow() : base(Kernel.g, "Sipad", 200, 200, false, 120, 120, Resources.Sipad)
            {
                SipadTB = new TextBox(x, y);
                SipadTB.Multiline = true;
                SipadTB.Width = width;
                SipadTB.Height = height;
            }

            public override void Draw(SipaVGA g)
            {
                base.Draw(g);
                if (visible)
                    SipadTB.Draw(g);
            }

            public override void Update()
            {
                base.Update();
                if (visible)
                {
                    SipadTB.Update();
                    SipadTB.X = x;
                    SipadTB.Y = y;
                }
            }
        }

        NotepadWindow window;

        public override void AppStart()
        {
            window = new NotepadWindow();
            window.visible = true;

        }

        public override void OnMessageReceived(Message m)
        {
            base.OnMessageReceived(m);
            switch (m)
            {
                case Message.CHANGEWINDOWVISIBILITY:
                    window.visible = !window.visible;
                    break;
            }
        }

        public override void OnDraw(SipaVGA g)
        {
            window.Draw(g);
        }

        public override void OnUpdate()
        {
            window.Update();
            if (window.requestingQuit)
            {
                RequestQuit();
            }
        }
    }
}
