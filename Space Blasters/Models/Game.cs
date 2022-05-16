using Space_Blasters.Script;
using Space_Blasters.Static;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Space_Blasters.Models
{
    public class Game
    {
        ScriptMain _scriptMain;

        public void Load()
        {
            _scriptMain = new ScriptMain();
            _scriptMain.Load();
        }

        public void Unload()
        {
            //unload graphics and turn off game music
        }

        string DisplayText = "";

        private void ProcessMouse()
        {
            if ((MouseState.Buttons & MouseButtons.Left) == MouseButtons.Left)
            {
                if (!MouseState.LeftHeld)
                {
                    MouseState.LeftClicked = true;
                }
                MouseState.LeftHeld = true;
            }
            else
            {
                MouseState.LeftHeld = false;
            }

            if ((MouseState.Buttons & MouseButtons.Right) == MouseButtons.Right)
            {
                if (!MouseState.RightHeld)
                {
                    MouseState.RightClicked = true;
                }
                MouseState.RightHeld = true;
            }
            else
            {
                MouseState.RightHeld = false;
            }
        }

        private void FinalizeMouse()
        {
            MouseState.LastButtons = MouseState.Buttons;
            MouseState.Clicks = 0;
            MouseState.WheelTicks = 0;
            MouseState.LeftClicked = false;
            MouseState.RightClicked = false;
            MouseState.dX = 0;
            MouseState.dY = 0;
        }

        private void ProcessKeyboard()
        {
            if ((Keyboard.GetKeyStates(Key.Escape) & KeyStates.Down) > 0)
            {
                if (!KeyboardState.EscHeld)
                {
                    KeyboardState.EscPressed = true;
                }
                KeyboardState.EscHeld = true;
            }
            else if (KeyboardState.EscHeld)
            {
                KeyboardState.EscHeld = false;
            }
            if ((Keyboard.GetKeyStates(Key.Enter) & KeyStates.Down) > 0)
            {
                if (!KeyboardState.EnterHeld)
                {
                    KeyboardState.EnterPressed = true;
                }
                KeyboardState.EnterHeld = true;
            }
            else if (KeyboardState.EnterHeld)
            {
                KeyboardState.EnterHeld = false;
            }
            if ((Keyboard.GetKeyStates(Key.RightAlt) & KeyStates.Down) > 0)
            {
                KeyboardState.RightAltHeld = true;
            }
            else if (KeyboardState.RightAltHeld)
            {
                KeyboardState.RightAltHeld = false;
            }
            if ((Keyboard.GetKeyStates(Key.LeftShift) & KeyStates.Down) > 0)
            {
                KeyboardState.LeftShiftHeld = true;
            }
            else
            {
                KeyboardState.LeftShiftHeld = false;
            }
            if ((Keyboard.GetKeyStates(Key.RightShift) & KeyStates.Down) > 0)
            {
                KeyboardState.RightShiftHeld = true;
            }
            else
            {
                KeyboardState.RightShiftHeld = false;
            }
        }

        private void FinalizeKeyboard()
        {
            KeyboardState.EscPressed = false;
            KeyboardState.EnterPressed = false;
            //key.RightAltPressed = false;
        }

        public void Update(TimeSpan gameTime)
        {
            // get time elapsed in decimal of a second
            double gameTimeElapsed = gameTime.TotalMilliseconds / 1000f;
            DisplayText = string.Format("FPS: {0}", (int)(1f / gameTimeElapsed));

            ProcessMouse();
            ProcessKeyboard();

            _scriptMain.Update(gameTimeElapsed);

            FinalizeKeyboard();
            FinalizeMouse();
        }

        Font fontFPS = new Font("Consolas", 16);
        Font fontPause = new Font("Consolas", 92);

        public void Draw(Graphics g)
        {
            if (GameState.IsPaused)
            {
                g.FillRectangle(new SolidBrush(Color.CornflowerBlue), new Rectangle(0, 0, GameState.GameResolution.Width, GameState.GameResolution.Height));
                SizeF pauseSize = g.MeasureString("GAME PAUSED", fontPause);

                g.DrawString("GAME PAUSED", fontPause, Brushes.Red,
                    new PointF((GameState.GameResolution.Width - pauseSize.Width) / 2, (GameState.GameResolution.Height - pauseSize.Height) / 2));
            }
            else
            {
                g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(0, 0, GameState.GameResolution.Width, GameState.GameResolution.Height));
                
                // animate star field
                foreach (var star in GameState.StarField)
                {
                    star.Sprite.Draw(g);
                }

                // draw the player
                GameState.PlayerObject.Sprite.Draw(g);

                // draw the player's bullets
                foreach (var bullet in GameState.PlayerBullets)
                {
                    bullet.Sprite.Draw(g);
                }

                // draw the test toast a bunch of times, rotating it
                //for (int tX = 0; tX < 15; tX++)
                //{
                //    for (int tY = 0; tY < 8; tY++)
                //    {
                //        using (Bitmap temp = RotateImage(GameState.RotationImage, GameState.RotationAngle))
                //        {
                //            g.DrawImage(temp, 128 * tX, 128 * tY);
                //        }
                //    }
                //}


            }

            g.DrawString(DisplayText, fontFPS, Brushes.White, new PointF(5, 5));

        }

    }
}
