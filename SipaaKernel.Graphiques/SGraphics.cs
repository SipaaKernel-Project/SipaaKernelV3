using Cosmos.HAL;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;

namespace SipaaKernelV3.Graphics
{
    /// <summary>
    /// Old Sipaa Graphics
    /// 
    /// I recommand switching to SipaVGA, its 50% same, and it have better performance
    /// </summary>
    public class SGraphics
    {
        private Font font = PCScreenFont.Default;
        private Canvas c;
        private Mode m = new Mode(1024, 768, ColorDepth.ColorDepth32);
        private int frames, fps, tick;
        private bool guiEnabled = false;

        public Font Font { get => font; set => font = value; }

        public void Init()
        {
            c = FullScreenCanvas.GetFullScreenCanvas(m);
            guiEnabled = true;
        }

        public void Disable()
        {
            c.Disable();
            guiEnabled = false;
        }

        public void Update()
        {
            if (tick != RTC.Second)
            {
                fps = frames;
                frames = 0;
                tick = RTC.Second;
            }

            frames++;

            c.Display();
        }

        public void SetResolution(uint width, uint height)
        {
            m = new Mode((int)width, (int)height, ColorDepth.ColorDepth32);
            if (c != null) { c.Mode = m; }
        }

        ////    DRAW METHODS    ////
        
        public void Clear() { c.Clear(new Color(0,0,0).ToSystemDrawingColor()); }
        public void Clear(Color col) { c.Clear(col.ToSystemDrawingColor()); }
        public void DrawRectangle(Color col, Position p, Size s) { c.DrawRectangle(new Pen(col.ToSystemDrawingColor()), p.X, p.Y, s.Width, s.Height); }
        public void DrawRectangle(Color col, uint width, Position p, Size s) { c.DrawRectangle(new Pen(col.ToSystemDrawingColor(), (int)width), p.X, p.Y, s.Width, s.Height); }
        public void DrawRectangle(Color col, Rectangle r) { c.DrawRectangle(new Pen(col.ToSystemDrawingColor()), (int)r.Position.X, (int)r.Position.Y, (int)r.Size.Width, (int)r.Size.Height); }
        public void DrawRectangle(Color col, uint width, Rectangle r) { c.DrawRectangle(new Pen(col.ToSystemDrawingColor(), (int)width), (int)r.Position.X, (int)r.Position.Y, (int)r.Size.Width, (int)r.Size.Height); }
        public void DrawFilledRectangle(Color col, Position p, Size s) { c.DrawFilledRectangle(new Pen(col.ToSystemDrawingColor()), (int)p.X, (int)p.Y, (int)s.Width, (int)s.Height); }
        public void DrawFilledRectangle(Color col, Position p, Size s, uint borderRadius) { c.DrawRoundRect(new Pen(col.ToSystemDrawingColor()), (int)p.X, (int)p.Y, (int)s.Width, (int)s.Height, (int)borderRadius); }
        public void DrawFilledRectangle(Color col, Rectangle r) { c.DrawFilledRectangle(new Pen(col.ToSystemDrawingColor()), (int)r.Position.X, (int)r.Position.Y, (int)r.Size.Width, (int)r.Size.Height); }
        public void DrawFilledRectangle(Color col, Rectangle r, uint borderRadius) { c.DrawRoundRect(new Pen(col.ToSystemDrawingColor()), (int)r.Position.X, (int)r.Position.Y, (int)r.Size.Width, (int)r.Size.Height, (int)borderRadius); }
        public void DrawString(string Text,Color col, Position p) { c.DrawString(Text, font, new Pen(col.ToSystemDrawingColor()), (int)p.X, (int)p.Y); }
        public void DrawString(string Text, Font Font, Color col, Position p) { c.DrawString(Text, Font, new Pen(col.ToSystemDrawingColor()), (int)p.X, (int)p.Y); }
        public void DrawChar(char Char, Color col, Position p) { c.DrawChar(Char, PCScreenFont.Default, new Pen(col.ToSystemDrawingColor()), (int)p.X, (int)p.Y); }
        public void DrawChar(char Char, Font Font, Color col, Position p) { c.DrawChar(Char, Font, new Pen(col.ToSystemDrawingColor()), (int)p.X, (int)p.Y); }
        public void DrawTriangle(Color col, Position[] ps) { c.DrawTriangle(new Pen(col.ToSystemDrawingColor()), (int)ps[0].X, (int)ps[0].Y, (int)ps[1].X, (int)ps[1].Y, (int)ps[2].X, (int)ps[2].Y); }
        public void DrawCircle(Color col, Position p, uint radius) { c.DrawCircle(new Pen(col.ToSystemDrawingColor()), (int)p.X, (int)p.Y, (int)radius); }
        public void DrawFilledCircle(Color col, Position p, uint radius) { c.DrawFilledCircle(new Pen(col.ToSystemDrawingColor()), (int)p.X, (int)p.Y, (int)radius); }
        public void DrawEllipse(Color col, Position p, Size sr) { c.DrawEllipse(new Pen(col.ToSystemDrawingColor()), (int)p.X, (int)p.Y, (int)sr.Width, (int)sr.Height); }
        public void DrawFilledEllipse(Color col, Position p, Size sr) { c.DrawFilledEllipse(new Pen(col.ToSystemDrawingColor()), (int)p.X, (int)p.Y, (int)sr.Width, (int)sr.Height); }
        public void DrawBitmap(Bitmap bmp, Position p, bool alpha = false)
        {
            switch (alpha)
            {
                case false:
                    c.DrawImage(bmp, (int)p.X, (int)p.Y);
                    break;
                case true:
                    c.DrawImageAlpha(bmp, (int)p.X, (int)p.Y);
                    break;
            }
        }

        ////    GET FIELDS    ////
        
        public bool IsGuiEnabled() { return guiEnabled; }

        public uint GetWidth() { return (uint)m.Columns; }

        public uint GetHeight() { return (uint)m.Rows; }

        public int GetFPS() { return fps; }

        public Canvas GetCanvas() { return c; }
    }
}