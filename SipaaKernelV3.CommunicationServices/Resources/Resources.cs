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
        [ManifestResourceStream(ResourceName = "SipaaKernelV3.CommunicationServices.Resources.paint.bmp")]
        public static byte[] rawPaint;
        [ManifestResourceStream(ResourceName = "SipaaKernelV3.CommunicationServices.Resources.consolemode.bmp")]
        public static byte[] rawConsoleModeIcon;
        [ManifestResourceStream(ResourceName = "SipaaKernelV3.CommunicationServices.Resources.sklogo.bmp")]
        public static byte[] rawSipaaKernelLogo32;
        [ManifestResourceStream(ResourceName = "SipaaKernelV3.CommunicationServices.Resources.sipad.bmp")]
        public static byte[] rawSipad;
        /**[ManifestResourceStream(ResourceName = "SipaaKernelV3.CommunicationServices.Resources.wallpaper.bmp")]
        public static byte[] rawWallpaper;**/
        [ManifestResourceStream(ResourceName = "SipaaKernelV3.CommunicationServices.Resources.about.bmp")]
        public static byte[] rawAbout;

        // BITMAPS
        public static Bitmap cursor = new Bitmap(rawCursor);
        public static Bitmap paint = new Bitmap(rawPaint);
        public static Bitmap consolemode = new Bitmap(rawConsoleModeIcon);
        public static Bitmap SipaaKernelLogo32 = new Bitmap(rawSipaaKernelLogo32);
        public static Bitmap Sipad = new Bitmap(rawSipad);
        //public static Bitmap wallpaper = new Bitmap(rawWallpaper);
        public static Bitmap about = new Bitmap(rawAbout);
    }
}
