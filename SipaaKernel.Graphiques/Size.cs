using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernelV3.Graphics
{
    public struct Size
    {
        private uint width, height;

        public uint Width { get => width; set => width = value; }
        public uint Height { get => height; set => height = value; }

        public Size(uint width, uint height)
        {
            this.width = width;
            this.height = height;
        }
    }
}
