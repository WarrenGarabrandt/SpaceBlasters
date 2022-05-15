using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Space_Blasters.Static
{
    public static class MouseState
    {
        public enum MouseModes
        {
            /// <summary>
            /// Get mouse position in display coordinates
            /// </summary>
            AbsoluteCoords,
            /// <summary>
            /// Get mouse position on the game window
            /// </summary>
            RelativeCoords,
            /// <summary>
            /// Keep mouse in middle and report dX and dY movements.
            /// </summary>
            Delta,
        }

        private static MouseModes _mouseMode;
        public static MouseModes MouseMode
        {
            get
            {
                return _mouseMode;
            }
            set
            {
                _mouseMode = value;
                ChangeMouseMode = true;
            }
        }
        public static bool ChangeMouseMode { get; set; }
        public static bool MouseHidden { get; set; }
        public static bool MouseIsHidden { get; set; }
        public static int dX { get; set; }
        public static int dY { get; set; }
        public static Point Location { get; set; }
        public static MouseButtons Buttons { get; set; }
        public static MouseButtons LastButtons { get; set; }
        public static int Clicks { get; set; }
        public static int WheelTicks { get; set; }
        public static bool LeftHeld { get; set; }
        public static bool RightHeld { get; set; }
        public static bool LeftClicked { get; set; }
        public static bool RightClicked { get; set; }

    }
}
