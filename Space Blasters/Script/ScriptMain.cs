using Space_Blasters.Models;
using Space_Blasters.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Blasters.Script
{
    public class ScriptMain
    {

        SpaceBlasterEngine _engine;
        public void Load()
        {
            _engine = new SpaceBlasterEngine();
            _engine.Load();
        }

        public void Update(double gameTime)
        {
            if (GameState.IsPaused)
            {
                if (KeyboardState.EscPressed)
                {
                    GameState.IsPaused = false;
                    MouseState.MouseMode = MouseState.MouseModes.Delta;
                    MouseState.MouseHidden = true;
                }
                else
                {
                    // option to toggle full screen/windowed
                    if (KeyboardState.RightAltHeld && KeyboardState.EnterPressed)
                    {
                        // switch between full screen and windowed mode
                    }
                }
            }
            else
            {
                if (KeyboardState.EscPressed)
                {
                    GameState.IsPaused = true;
                    MouseState.MouseMode = MouseState.MouseModes.RelativeCoords;
                    MouseState.MouseHidden = false;
                }
            }

            if (!GameState.IsPaused)
            {
                _engine.Update(gameTime);
            }
        }
    }
}
