using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernelV3.Graphics
{
    public struct Position
    {
        private uint x, y;

        public uint X { get => x; set => x = value; }
        public uint Y { get => y; set => y = value; }

        public Position(uint x, uint y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
