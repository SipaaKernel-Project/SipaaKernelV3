using SipaaKernelV3.Graphics;

namespace SipaaKernelV3.Core
{
    public enum Message
    {
        CHANGEWINDOWVISIBILITY
    }
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
        public abstract void OnDraw(SipaVGA g);
        public abstract void OnUpdate();
        public virtual void OnMessageReceived(Message m)
        {
        }
        public void SendMessage(Message m)
        {
            OnMessageReceived(m);
        }
        public void RequestQuit()
        {
            RequestingQuit = true;
        }

        public void Dispose()
        {
        }
    }
}