using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using SipaaKernelV3.Commands.FileSystem;
using SipaaKernelV3.Commands.System;
using SipaaKernelV3.Core;
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
        public static SGraphics g;
        public static Dock d;
        public static MenuBar mb;
        public static FileSystemDriver fsd;
        public static AppManager appManager;
        public static string CurrentDir = @"0:\";
        public static uint permToken;
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
        #region Shard
        static void InitShell()
        {
            cr = new CommandRegister();
            cr.AppendCommand(new GUICommand());
            cr.AppendCommand(new AboutCommand());
            cr.AppendCommand(new ShutdownCommand());
            cr.AppendCommand(new RebootCommand());
            cr.AppendCommand(new PermissionManagerCommand());
            cr.AppendCommand(new ResetAppManagerCommand());
            cr.AppendCommand(new ChangeResCommand());
            cr.AppendCommand(new EchoCommand());
            cr.AppendCommand(new ListDirsFilesCommand());
            sh = new Shell(cr);
            sh.StartLine = "user@sipaakernel:> ";
            Console.Clear();
            Console.WriteLine("SipaaKernel V3");
            Console.WriteLine("Type help to get all commands, gui to enter GUI mode.");
            Console.WriteLine();
        }
        static void UpdateStartLine()
        {
            string startline = "";
            if (permManager.IsUserHavePermission(Permission.PermRoot))
            {
                startline += "root@";
            }
            else if (permManager.IsUserHavePermission(Permission.PermGuest))
            {
                startline += "guest@";
            }
            else
            {
                startline += "user@";
            }

            startline += CurrentDir + ":>";
            sh.StartLine = startline;
        }
        #endregion
        #region Kernel
        public static void SKLoadGUI()
        {
            Sys.MouseManager.ScreenWidth = g.GetWidth();
            Sys.MouseManager.ScreenHeight = g.GetHeight();
            g.Init();
        }
        public static void SKChangeRes(uint width, uint height)
        {
            Sys.MouseManager.ScreenWidth = g.GetWidth();
            Sys.MouseManager.ScreenHeight = g.GetHeight();
            g.SetResolution(width, height);
            d = new Dock(g);
            mb = new MenuBar();
        }
        public static void SKLoadConsole()
        {
            g.Disable();
        }

        protected override void OnBoot()
        {
            base.OnBoot();
            Console.WriteLine("Initializing file system...");
            fsd = new FileSystemDriver();
            fsd.Initialize();
            Console.WriteLine("Initializing permission manager...");
            permManager = new PermissionManager();
            permToken = permManager.Initialize();
            Console.WriteLine("Initializing App Manager...");
            appManager = new AppManager();
            Console.WriteLine("Initializing Shard Shell...");
            InitShell();
        }
        protected override void BeforeRun()
        {
            g = new SGraphics();
            d = new Dock(g);
            mb = new MenuBar();
        }

        protected override void Run()
        {
            if (g.IsGuiEnabled())
            {
                // Draw wallpaper
                g.DrawBitmap(Resources.wallpaper, new Position(0, 0));
                // Draw FPS
                g.DrawString("Running at " + g.GetFPS() + " FPS", Color.White, new Position(10, 10));
                // Draw apps
                foreach (Application app in appManager.OpenedApps)
                {
                    app.OnDraw(g);
                    app.OnUpdate();
                    if (app.RequestingQuit) { appManager.CloseApp(app); }
                }
                // Draw dock
                mb.Draw(g);
                mb.Update();
                d.Draw(g);
                d.Update(appManager);
                if (d.returnToConsole.ButtonState == ButtonState.Clicked) { return; }
                // Draw cursor and apply graphics to screen
                g.DrawBitmap(Resources.cursor, new Position(Sys.MouseManager.X, Sys.MouseManager.Y), true);
                g.Update();
            }
            else
            {
                UpdateStartLine();
                sh.GetAndRunCommand();
            }
        }
        #endregion
    }
}
