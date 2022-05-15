using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Blasters.Static
{
    public static class KeyboardState
    {
        public static bool EscHeld { get; set; }
        public static bool EscPressed { get; set; }

        public static bool EnterHeld { get; set; }
        public static bool EnterPressed { get; set; }

        public static bool RightAltHeld { get; set; }
        //public bool RightAltPressed { get; set; }

        public static bool LeftShiftHeld { get; set; }
        public static bool RightShiftHeld { get; set; }
    }
}
