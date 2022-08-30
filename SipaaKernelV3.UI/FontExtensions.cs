using Cosmos.System.Graphics.Fonts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernelV3.UI
{
    public static class FontExtensions
    {
        public static uint GetStringWidth(this Font f, string s)
        {
            return ((uint)s.Length * f.Width);
        }
    }
}
