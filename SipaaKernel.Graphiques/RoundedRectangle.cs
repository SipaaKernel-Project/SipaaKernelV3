////    ROUNDRECT PROJECT FROM @atmo#2158    ////
using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace SipaaKernelV3.Graphics
{
    internal static class RoundedRectangle
    {
        public static double Offset = 0;

        public static double Sharpness = 0.8;

        private static List<string> CacheKeys = new List<string>();
        private static List<System.Drawing.Color[]> CacheValues = new List<System.Drawing.Color[]>();

        public enum Corner
        {
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight
        }

        private static double Distance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }

        public static void DrawRoundCorner(this Canvas canvas, Pen pen, int cx, int cy, int size, Corner corner)
        {
            System.Drawing.Color originalColor = pen.Color;
            string key = originalColor.R.ToString() + originalColor.G.ToString() + originalColor.B.ToString() + size.ToString() + ((int)corner).ToString();
            int index = -1;
            for (int i = 0; i < CacheKeys.Count; i++)
            {
                if (CacheKeys[i] == key)
                {
                    index = i;
                }
            }
            System.Drawing.Color[] values;
            if (index == -1)
            {
                CacheKeys.Add(key);
                values = new System.Drawing.Color[(size * size)];
                double lookX = (corner == Corner.TopLeft || corner == Corner.BottomLeft) ? size - 1 : 0;
                double lookY = (corner == Corner.TopLeft || corner == Corner.TopRight) ? size - 1 : 0;
                for (int x = 0; x < size; x++)
                {
                    for (int y = 0; y < size; y++)
                    {
                        double distance = (Distance(x, y, lookX, lookY) + Offset) / size;
                        byte alpha = (byte)Math.Clamp((1 - distance) * (size * Sharpness) * 255, 0, 255);
                        values[(y * size) + x] = System.Drawing.Color.FromArgb(alpha, originalColor);
                    }
                }
                CacheValues.Add(values);
            }
            else
            {
                values = CacheValues[index];
            }
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    pen.Color = values[(y * size) + x];
                    canvas.DrawPoint(pen, cx + x, cy + y);
                }
            }
            pen.Color = originalColor;
        }

        public static void DrawRoundRect(this Canvas canvas, Pen pen, int x, int y, int width, int height, int cornerSize)
        {
            if (cornerSize == 0)
            {
                canvas.DrawFilledRectangle(pen, x, y, width, height);
            }

            int longest = Math.Max(width, height);
            if (cornerSize > longest / 2) cornerSize = longest / 2;

            canvas.DrawFilledRectangle(pen, x, y + cornerSize, width, height - (cornerSize * 2));
            canvas.DrawFilledRectangle(pen, x + cornerSize, y, width - (cornerSize * 2), height);
            DrawRoundCorner(canvas, pen, x, y, cornerSize, Corner.TopLeft);
            DrawRoundCorner(canvas, pen, (x + width) - cornerSize, y, cornerSize, Corner.TopRight);
            DrawRoundCorner(canvas, pen, x, (y + height) - cornerSize, cornerSize, Corner.BottomLeft);
            DrawRoundCorner(canvas, pen, (x + width) - cornerSize, (y + height) - cornerSize, cornerSize, Corner.BottomRight);
        }
    }
}
