using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AutoClicker
{
    public class KeyPresser
    {
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        const int VK_UP = 0x26; //up key
        const int VK_DOWN = 0x28;  //down key
        const int VK_LEFT = 0x25;
        const int VK_RIGHT = 0x27;
        const uint KEYEVENTF_KEYUP = 0x0002;
        const uint KEYEVENTF_EXTENDEDKEY = 0x0001;
        public const uint ONE = 0x31;
        public const uint TWO = 0x32;
        public const uint THREE = 0x33;
        public const uint FOUR = 0x34;
        public const uint FIVE = 0x35;
        public const uint SIX = 0x36;

        public static void PressKey(uint key)
        {
            //Press the key
            keybd_event((byte)key, 0, KEYEVENTF_EXTENDEDKEY | 0, 0);
            keybd_event((byte)key, 0, KEYEVENTF_KEYUP | 0, 0);
        }
    }
}
