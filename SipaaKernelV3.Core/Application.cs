using SipaaKernelV3.Graphics;

namespace SipaaKernelV3.Core
{
    public abstract class Application : IDisposable
    {
        private string AppName;
        private double AppVersion;
        public bool RequestingQuit = false;
        public Application()
        {
            AppStart();
        }
        public abstract void AppStart();
        public abstract void OnDraw(SGraphics g);
        public abstract void OnUpdate();
        public void RequestQuit()
        {
            RequestingQuit = true;
        }

        public void Dispose()
        {
        }
    }
}