using SipaaKernelV3.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernelV3.UI
{
    public class Desktop
    {
        public Taskbar tb;
        public StartMenu sm;

        public Desktop(SipaVGA g)
        {
            tb = new Taskbar(g);
            sm = new StartMenu(g, tb);
        }

        public void Draw(SipaVGA g)
        {
            tb.Draw(g);
        }

        public void DrawStartMenu(SipaVGA g)
        {
            sm.Draw(g);
        }

        public void Update()
        {
            tb.Update(sm);
            sm.Update();
        }
    }
}
