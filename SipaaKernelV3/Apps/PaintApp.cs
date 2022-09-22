using Cosmos.System;
using Cosmos.System.Graphics;
using SipaaKernelV3.Core;
using SipaaKernelV3.Graphics;
using SipaaKernelV3.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernelV3.Apps
{
    public class PaintApp : Application
    {
        private class PaintWindow : Window
        {
            private struct PixelInfo
            {
                public uint X;
                public uint Y;
                public uint Color;

                public PixelInfo(uint x,uint y,uint color)
                {
                    this.X = x;
                    this.Y = y;
                    this.Color = color;
                }
            }

            private List<PixelInfo> pixels;

            public PaintWindow(SipaVGA vga) : base(vga, "Pixel Paint", 200, 200, false, 100, 100, Resources.paint)
            {
                pixels = new List<PixelInfo>();
            }

            public override void Draw(SipaVGA g)
            {
                base.Draw(g);

                if (visible)
                {
                    foreach (PixelInfo p in pixels)
                    {
                        g.DrawPixel(p.X, p.Y, p.Color);
                    }
                }
            }

            public override void Update()
            {
                base.Update();

                if (visible && MouseManager.MouseState == MouseState.Left && MouseManager.X > x && MouseManager.X < x+ width && MouseManager.Y > y && MouseManager.Y < y + height)
                {
                    pixels.Add(new PixelInfo(MouseManager.X, MouseManager.Y, 0xFFFFFF));
                }
            }
        }

        PaintWindow w;
        public override void AppStart()
        {
            w = new PaintWindow(Kernel.g);
            w.visible = true;
        }

        public override void OnMessageReceived(Message m)
        {
            base.OnMessageReceived(m);
            switch (m)
            {
                case Message.CHANGEWINDOWVISIBILITY:
                    w.visible = !w.visible;
                    break;
            }
        }

        public override void OnDraw(SipaVGA g)
        {
            w.Draw(g);
        }

        public override void OnUpdate()
        {
            w.Update();
            if (w.requestingQuit)
            {
                this.RequestQuit();
            }
        }
    }
}
