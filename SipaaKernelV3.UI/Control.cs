using SipaaKernelV3.Graphics;

namespace SipaaKernelV3.UI
{
    public class Control
    {
        private uint y;
        private uint x;

        public uint X { get { return x; } set { x = value; } }
        public uint Y { get { return y; } set { y = value; } }

        public virtual void Draw(SipaVGA c) { }

        public virtual void Update() { }
    }
}