using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernelV3.Graphics
{
    public struct Color
    {
        ////    PREDEFINED COLORS    ////
        public static Color Black { get => new Color(0, 0, 0); }
        public static Color VeryDarkGray { get => new Color(32, 32, 32); }
        public static Color DarkGray { get => new Color(64, 64, 64); }
        public static Color Gray { get => new Color(116, 116, 116); }
        public static Color LightGray { get => new Color(180, 180, 180); }
        public static Color VeryLightGray { get => new Color(224, 224, 224); }
        public static Color White { get => new Color(255, 255, 255); }

        ////   COLOR STRUCT    ////
        private uint a = 0, r = 0, g = 0, b = 0;

        public uint A { get => a; set => a = value; }
        public uint R { get => r; set => r = value; }
        public uint G { get => g; set => g = value; }
        public uint B { get => b; set => b = value; }

        public Color(uint a, uint r, uint g, uint b)
        {
            if (a > 255 || r > 255 || g > 255 || b > 255)
            {
                throw new Exception("A, R, G or B can't be more than 255.");
            }
            A = a;
            R = r;
            G = g;
            B = b;
        }

        public Color(uint r, uint g, uint b)
        {
            if (r > 255 || g > 255 || b > 255)
            {
                throw new Exception("R, G or B can't be more than 255.");
            }
            A = 255;
            R = r;
            G = g;
            B = b;
        }

        public global::System.Drawing.Color ToSystemDrawingColor() { return global::System.Drawing.Color.FromArgb((int)a, (int)r, (int)g, (int)b); }
    }
}
