using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernelV3.Core
{
    public class AppManager
    {
        public List<Application> appList;
        public List<Application> OpenedApps { get => appList;  set =>appList = value; }

        public AppManager()
        {
            appList = new List<Application>();
        }

        public void OpenApp(Application app)
        {
            appList.Add(app);
            app.AppStart();
        }

        public void CloseApp(Application app)
        {
            appList.Remove(app);
        }
    }
}
