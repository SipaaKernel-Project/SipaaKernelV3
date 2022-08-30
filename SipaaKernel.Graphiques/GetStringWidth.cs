using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernelV3.Graphics
{
    public static class GetStrWidth
    {
        public static uint GetStringWidth(this Cosmos.System.Graphics.Fonts.Font f, string String)
        {
            return (uint)String.Length * f.Width;
        }
    }
}
