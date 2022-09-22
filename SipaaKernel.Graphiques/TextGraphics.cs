using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernelV3.Graphics
{
    public class TextGraphics
    {
        public static void DrawLineH(int x, int y, int width, ConsoleColor c)
        {
            for (int i = 0; i < width; i++)
            {
                DrawChar(' ', x + i, y, ConsoleColor.White, c);
            }
        }

        public static void DrawLineV(int x, int y, int height, ConsoleColor c)
        {
            for (int i = 0; i < height; i++)
            {
                DrawChar(' ', x, y + i, ConsoleColor.White, c);
            }
        }

        public static void DrawString(string s, int x, int y, ConsoleColor fg, ConsoleColor bg)
        {
            var oldfg = Console.ForegroundColor;
            var oldbg = Console.BackgroundColor;
            var cx = Console.GetCursorPosition().Left;
            var cy = Console.GetCursorPosition().Top;

            Console.ForegroundColor = fg;
            Console.BackgroundColor = bg;

            Console.SetCursorPosition(x, y);

            Console.Write(s);

            Console.ForegroundColor = oldfg;
            Console.BackgroundColor = oldbg;

            Console.SetCursorPosition(cx, cy);
        }

        public static void DrawChar(char c, int x, int y, ConsoleColor fg, ConsoleColor bg)
        {
            var oldfg = Console.ForegroundColor;
            var oldbg = Console.BackgroundColor;
            var cx = Console.GetCursorPosition().Left;
            var cy = Console.GetCursorPosition().Top;

            Console.ForegroundColor = fg;
            Console.BackgroundColor = bg;

            Console.SetCursorPosition(x, y);

            Console.Write(c);

            Console.ForegroundColor = oldfg;
            Console.BackgroundColor = oldbg;

            Console.SetCursorPosition(cx, cy);
        }
    }
}
