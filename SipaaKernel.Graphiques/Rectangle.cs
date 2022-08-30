using Cosmos.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernelV3.Graphics
{
    public struct Rectangle
    {
        public Size size;
        public Position pos;

        public Rectangle(Size s, Position p)
        {
            size = s;
            pos = p;
        }

        public Rectangle(uint width, uint height, uint x, uint y)
        {
            this.size = new Size(width, height);
            this.pos = new Position(x, y);
        }

        public Size Size { get => size; set => size = value; }
        public Position Position { get => pos; set => pos = value; }

        public bool IsMouseHover()
        {
            if (MouseManager.X > pos.X && MouseManager.X < pos.X + Size.Width && MouseManager.Y > pos.Y && MouseManager.Y < pos.Y + Size.Height)
            {
                return true;
            }
            return false;
        }
    }
}
