using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using SipaaKernelV3.Commands.FileSystem;
using SipaaKernelV3.Commands.System;
using SipaaKernelV3.Core;
using SipaaKernelV3.Core.Keyboard;
using SipaaKernelV3.Graphics;
using SipaaKernelV3.Shard;
using SipaaKernelV3.UI;
using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace SipaaKernelV3
{
    public enum KernelErrorCode : uint
    {
        UserManuallyThrowedException = 0x01,
        BootFail = 0x02,
        KernelFail = 0x03,
        CPUException = 0x03
    }
    public enum KernelErrorType : uint
    {
        KernelException = 0x01,
        CPUException = 0x02
    }

    public class Kernel : Sys.Kernel
    {
        #region Fields
        public static Shell sh;
        public static CommandRegister cr;
        public static PermissionManager permManager;
        public static Desktop d;
        public static SipaVGA g;
        public static MenuBar mb;
        public static FileSystemDriver fsd;
        public static AppManager appManager;
        public static Bitmap wallpaperScaled;
        public static string Username = "Raphael";
        public static string Password = "0000";
        public static string CurrentDir = @"0:\";
        public static uint permToken;
        public static bool dev = true;
        public static bool requestRunFunctionExit = false;
        #endregion
        #region Crash Methods
        static void SKRaiseKernelHardError(uint errorCode)
        {
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.Clear();
            Console.SetCursorPosition(2, 2);
            Console.WriteLine(":(");
            Console.SetCursorPosition(2, 4);
            Console.WriteLine("SipaaKernel ran into a problem and needs to restart.");
            Console.SetCursorPosition(2, 6);
            Console.WriteLine("ErrorCode : " + errorCode);
            Console.SetCursorPosition(2, 8);
            Console.WriteLine("If this is the first time you see this screen, reboot, ");
            Console.SetCursorPosition(2, 9);
            Console.WriteLine("Else, report this in channel #errors of SipaaKernel Discord server.");
            Console.SetCursorPosition(2, 11);
            Console.WriteLine("Press any key to reboot...");
            Console.ReadKey();
            Sys.Power.Reboot();
        }
        static void SKRaiseCPUHardError(uint lastKnownAddress, string description)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.Clear();
            Console.SetCursorPosition(2, 2);
            Console.WriteLine(":(");
            Console.SetCursorPosition(2, 4);
            Console.WriteLine("Your device CPU ran into a problem and needs to restart.");
            Console.SetCursorPosition(2, 6);
            Console.WriteLine("Last Known Address : " + lastKnownAddress);
            Console.SetCursorPosition(2, 7);
            Console.WriteLine(description);
            Console.SetCursorPosition(2, 9);
            Console.WriteLine("If this is the first time you see this screen, reboot, ");
            Console.SetCursorPosition(2, 10);
            Console.WriteLine("Else, report this in channel #errors of SipaaKernel Discord server.");
            Console.SetCursorPosition(2, 12);
            Console.WriteLine("Reboot manually...");
        }
        public static bool SKRaiseHardError(uint errorCode, uint errorType, string description = "")
        {
            if (permManager.IsUserHavePermission(Permission.PermRoot) || permManager.IsUserHavePermission(Permission.PermZero))
            {
                switch (errorType)
                {
                    case (uint)KernelErrorType.KernelException:
                        SKRaiseKernelHardError(errorCode);
                        return true;
                    case (uint)KernelErrorType.CPUException:
                        SKRaiseCPUHardError(errorCode, description);
                        return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }
        #endregion
        #region Kernel
        public static void SKLoadGUI()
        {
            Sys.MouseManager.ScreenWidth = g.GetResolution().ScreenWidth;
            Sys.MouseManager.ScreenHeight = g.GetResolution().ScreenHeight;
            g.LoadDriver();
        }
        public static void SKChangeRes(uint width, uint height)
        {
            g.SetResolution(new SVGAMode(width, height));
            Sys.MouseManager.ScreenWidth = g.GetResolution().ScreenWidth;
            Sys.MouseManager.ScreenHeight = g.GetResolution().ScreenHeight;
            d = new Desktop(g);
        }
        public static void SKOpenApp(Application app, Bitmap appIcon)
        {
            appManager.OpenApp(app);
            d.tb.AddTaskbarButton(app, appIcon);
        }
        protected override void OnBoot()
        {
            base.OnBoot();
            Console.WriteLine("[INFO] Starting SipaaKernel PreInitialization...");
            Console.WriteLine("[INFO] Initializing file system...");
            fsd = new FileSystemDriver();
            fsd.Initialize();
            Console.WriteLine("[INFO] Initializing permission manager...");
            permManager = new PermissionManager();
            permToken = permManager.Initialize();
            Console.WriteLine("[INFO] Initializing App Manager...");
            appManager = new AppManager();
            Console.WriteLine("[OK] SipaaKernel PreInitialization finished!");
        }
        protected override void BeforeRun()
        {
            g = new SipaVGA();
            d = new Desktop(g);
            SKLoadGUI();
        }

        protected override void Run()
        {
            RunKernel();
        }
        static void RunKernel()
        {
            try
            {
                KBPS2.Update();
                if (requestRunFunctionExit) { requestRunFunctionExit = false; return; }
                g.Clear((uint)Color.MakeArgb(255, 12, 12, 12));
                d.Draw(g);
                g.DrawString("SipaVGA driver (FPS : " + g.GetFPS() + ")", PCScreenFont.Default, 0xFFFFFF, 6, 6);
                foreach (Application app in appManager.OpenedApps)
                {
                    app.OnDraw(g);
                    app.OnUpdate();
                    if (app.RequestingQuit)
                    {
                        appManager.CloseApp(app);
                        d.tb.RemoveTaskbarButton(app);
                    }
                }
                d.DrawStartMenu(g);
                d.Update();
                g.DrawImageAlpha(Resources.cursor, Sys.MouseManager.X, Sys.MouseManager.Y, (uint)Color.MakeArgb(0, 0, 0, 0));
                g.Update();
            }
            catch (Exception ex)
            {
                if (g.IsDriverLoaded())
                {
                    g.UnloadDriver();
                }
                Console.WriteLine("---- SIPAAKERNEL PANIC ----");
                Console.WriteLine("We are sorry than that happened for you. Here is some info :");
                Console.WriteLine();
                Console.WriteLine("Date of event : " + Cosmos.HAL.RTC.DayOfTheMonth + "/" + Cosmos.HAL.RTC.Month + "/" + Cosmos.HAL.RTC.Year);
                Console.WriteLine("Time of event : " + Cosmos.HAL.RTC.Hour + ":" + Cosmos.HAL.RTC.Minute + ":" + Cosmos.HAL.RTC.Second);
                Console.WriteLine("Exception Message : " + ex.Message);
            }
        }
        #endregion
    }
}
