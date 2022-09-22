using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sys = Cosmos.System;
using Cosmos.System.Graphics;
using Cosmos.HAL.Drivers.PCI.Video;

namespace SipaaKernelV3.Graphics
{
    public struct SVGAMode
    {
        public uint ScreenWidth;
        public uint ScreenHeight;
        public uint Depth;

        public SVGAMode(uint width, uint height, uint depth = 32)
        {
            ScreenWidth = width;
            ScreenHeight = height;
            Depth = depth;
        }
    }
    public class SipaVGA
    {
        public VMWareSVGAII driver;
        private SVGAMode mode = new SVGAMode(800, 600);
        private int delta, frames, fps;
        private bool isguienabled = false;
        private int GetPointOffset(int aX, int aY)
        {
            int stride = (int)mode.Depth / 8;
            int pitch = (int)mode.ScreenWidth * stride;

            return (aX * stride) + (aY * pitch);
        }

        /// Rectangles drawing
        public void DrawFilledRectangle(uint x, uint y, uint width, uint height, uint color)
        {
            for (uint i = 0; i < width * height; i++)
            {
                uint xx = i % width;
                uint yy = i / width;
                if (width > 0 && height > 0)
                {
                    driver.SetPixel(x + xx, y + yy, color);
                }
            }
        }
        public void DrawRectangle(uint x, uint y, uint width, uint height, uint bSize, uint color)
        {
            DrawFilledRectangle(x, y, width, bSize, color);
            DrawFilledRectangle(x, y + height - bSize, width, bSize, color);
            DrawFilledRectangle(x, y, bSize, height, color);
            DrawFilledRectangle(x + width - bSize, y, bSize, height, color);
        }

        // Line drawing
        public void DrawHorizontalLine(uint x, uint y, uint width, uint color)
        {
            for (uint i = 0; i < width; i++)
            {
                DrawPixel(x + i, y, color);
            }
        }
        public void DrawVerticalLine(uint x, uint y, uint height, uint color)
        {
            for (uint i = 0; i < height; i++)
            {
                DrawPixel(x, y + i, color);
            }
        }

        // Image / Bitmap drawing (broken?)
        public void DrawImage(Bitmap bmp, uint x, uint y)
        {
            Sys.Graphics.Pen _pen = new(System.Drawing.Color.White);

            for (uint _x = 0; _x < bmp.Width; _x++)
            {
                for (uint _y = 0; _y < bmp.Height; _y++)
                {
                    _pen.Color = System.Drawing.Color.FromArgb(bmp.rawData[_x + _y * bmp.Width]);
                    DrawPixel(x + _x, y + _y, (uint)_pen.Color.ToArgb());
                }
            }
        }

        public void DrawImageAlpha(Bitmap bmp, uint x, uint y, uint aColor)
        {
            Sys.Graphics.Pen _pen = new(System.Drawing.Color.White);

            for (uint _x = 0; _x < bmp.Width; _x++)
            {
                for (uint _y = 0; _y < bmp.Height; _y++)
                {
                    if (bmp.rawData[_x + _y * bmp.Width] != aColor)
                    {
                        _pen.Color = System.Drawing.Color.FromArgb(bmp.rawData[_x + _y * bmp.Width]);
                        DrawPixel(x + _x, y + _y, (uint)_pen.Color.ToArgb());
                    }
                }
            }
        }


        // Characters / String drawing
        public void DrawString(string str, Cosmos.System.Graphics.Fonts.Font aFont, uint color, uint x, uint y)
        {
            foreach (char i in str)
            {
                DrawChar(i, aFont, color, x, y);
                x += aFont.Width;
            }
        }
        public void DrawChar(char c, Cosmos.System.Graphics.Fonts.Font aFont, uint color, uint x, uint y)
        {
            uint p = aFont.Height * (uint)c;

            for (uint cy = 0; cy < aFont.Height; cy++)
            {
                for (byte cx = 0; cx < aFont.Width; cx++)
                {
                    if (aFont.ConvertByteToBitAddres(aFont.Data[p + cy], cx + 1))
                    {
                        driver.SetPixel(x + (aFont.Width - (uint)cx), y + cy, color);
                    }
                }
            }
        }

        // Circle / Ellipse Drawing (beta)
        public void DrawCircle(uint x_center, uint y_center, uint radius, uint color)
        {
            uint x = radius;
            uint y = 0;
            uint e = 0;

            while (x >= y)
            {
                DrawPixel(x_center + x, y_center + y, color);
                DrawPixel(x_center + y, y_center + x, color);
                DrawPixel(x_center - y, y_center + x, color);
                DrawPixel(x_center - x, y_center + y, color);
                DrawPixel(x_center - x, y_center - y, color);
                DrawPixel(x_center - y, y_center - x, color);
                DrawPixel(x_center + y, y_center - x, color);
                DrawPixel(x_center + x, y_center - y, color);

                y++;
                if (e <= 0)
                {
                    e += 2 * y + 1;
                }
                if (e > 0)
                {
                    x--;
                    e -= 2 * x + 1;
                }
            }
        }
        public void DrawFilledCircle(uint x0, uint y0, uint radius, uint color)
        {
            uint x = radius;
            uint y = 0;
            uint xChange = 1 - (radius << 1);
            uint yChange = 0;
            uint radiusError = 0;

            while (x >= y)
            {
                for (uint i = x0 - x; i <= x0 + x; i++)
                {

                    DrawPixel(i, y0 + y, color);
                    DrawPixel(i, y0 - y, color);
                }
                for (uint i = x0 - y; i <= x0 + y; i++)
                {
                    DrawPixel(i, y0 + x, color);
                    DrawPixel(i, y0 - x, color);
                }

                y++;
                radiusError += yChange;
                yChange += 2;
                if (((radiusError << 1) + xChange) > 0)
                {
                    x--;
                    radiusError += xChange;
                    xChange += 2;
                }
            }
        }

        // Pixel drawing
        public void DrawPixel(uint x, uint y, uint color)
        {
            driver.SetPixel(x, y, color);
        }

        // Utils / necessary functions

        public byte[] ScaleBitmap(Bitmap image, int newWidth, int newHeight)
        {
            int[] pixels = image.rawData;
            int w1 = (int)image.Width;
            int h1 = (int)image.Height;
            byte[] temp = new byte[newWidth * newHeight];
            int x_ratio = (int)((w1 << 16) / newWidth) + 1;
            int y_ratio = (int)((h1 << 16) / newHeight) + 1;
            int x2, y2;
            for (int i = 0; i < newHeight; i++)
            {
                for (int j = 0; j < newWidth; j++)
                {
                    x2 = ((j * x_ratio) >> 16);
                    y2 = ((i * y_ratio) >> 16);
                    temp[(i * newWidth) + j] = (byte)pixels[(y2 * w1) + x2];
                }
            }
            return temp;
        }
        public bool IsDriverLoaded()
        {
            return driver != null;
        }
        public int GetFPS() { return fps; }
        public SVGAMode GetResolution()
        {
            return mode;
        }
        public void UnloadDriver()
        {
            driver.Disable();
            driver = null;
        }
        public void Clear()
        {
            driver.Clear(0x000000);
        }
        public void Clear(uint color)
        {
            driver.Clear(color);
        }
        public void SetResolution(SVGAMode mode)
        {
            driver.SetMode(mode.ScreenWidth, mode.ScreenHeight, mode.Depth);
            this.mode = mode;
        }
        public void Update()
        {
            if (delta != Cosmos.HAL.RTC.Second)
            {
                delta = Cosmos.HAL.RTC.Second;
                fps = frames;
                frames = 0;
            }
            frames++;
            driver.DoubleBufferUpdate();
        }
        public void LoadDriver()
        {
            driver = new VMWareSVGAII();
            SetResolution(mode);
        }
    }
}
