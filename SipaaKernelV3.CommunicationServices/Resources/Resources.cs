using Cosmos.System.Graphics;
using IL2CPU.API.Attribs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernelV3
{
    public class Resources
    {
        // RAW FILES
        [ManifestResourceStream(ResourceName = "SipaaKernelV3.CommunicationServices.Resources.pointer.bmp")]
        public static byte[] rawCursor;
        [ManifestResourceStream(ResourceName = "SipaaKernelV3.CommunicationServices.Resources.wallpaper.bmp")]
        public static byte[] rawWallpaper;
        [ManifestResourceStream(ResourceName = "SipaaKernelV3.CommunicationServices.Resources.consolemode.bmp")]
        public static byte[] rawConsoleModeIcon;
        [ManifestResourceStream(ResourceName = "SipaaKernelV3.CommunicationServices.Resources.about.bmp")]
        public static byte[] rawAbout;

        // BITMAPS
        public static Bitmap cursor = new Bitmap(rawCursor);
        public static Bitmap wallpaper = new Bitmap(rawWallpaper);
        public static Bitmap consolemode = new Bitmap(rawConsoleModeIcon);
        public static Bitmap about = new Bitmap(rawAbout);
    }
}
