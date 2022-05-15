using Space_Blasters.Models;
using Space_Blasters.Static;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Space_Blasters
{
    public partial class FormMainScreen : Form
    {
        GameLoop _gameLoop = null;

        #region
        [StructLayout(LayoutKind.Sequential)]
        public struct NativeMessage
        {
            public IntPtr handle;
            public uint msg;
            public IntPtr wParam;
            public IntPtr lParam;
            public uint time;
            public Point p;
        }

        [SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool PeekMessage(out NativeMessage message, IntPtr hWnd, uint filterMin, uint filterMax, uint flags);
        #endregion
        public void OnApplicationIdle(object sender, EventArgs e)
        {
            if (_gameLoop != null)
            {
                while (AppStillIdle)
                {
                    if (_gameLoop.Update())
                    {
                        using (var g = this.CreateGraphics())
                        {
                            this.Invalidate();
                        }
                    }
                }
                
            }
        }

        private bool AppStillIdle
        {
            get
            {
                NativeMessage msg = new NativeMessage();
                return !PeekMessage(out msg, IntPtr.Zero, (uint)0, (uint)0, (uint)0);
            }
        }

        public FormMainScreen()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ResizeRedraw, true);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            return;
            //gameLoop.Draw(e.Graphics);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            _gameLoop.Draw(e.Graphics);
            return;
        }
        
        private void FormMainScreen_Resize(object sender, EventArgs e)
        {
            if (_gameLoop != null)
            {
                GameState.GameResolution = this.ClientRectangle;
                MouseState.ChangeMouseMode = true;
            }
        }

        private void FormMainScreen_Load(object sender, EventArgs e)
        {
            Rectangle resolution = this.ClientRectangle;
            GameState.GameResolution = resolution;

            Game myGame = new Game();
            _gameLoop = new GameLoop();
            _gameLoop.Load(myGame);
            _gameLoop.Start();
        }

        private void FormMainScreen_MouseMove(object sender, MouseEventArgs e)
        {
            if (MouseState.MouseHidden)
            {
                if (!MouseState.MouseIsHidden)
                {
                    MouseState.MouseIsHidden = true;
                    Cursor.Hide();
                }
            }
            else
            {
                if (MouseState.MouseIsHidden)
                {
                    MouseState.MouseIsHidden = false;
                    Cursor.Show();
                }
            }
            if (MouseState.ChangeMouseMode)
            {
                if (MouseState.MouseMode == MouseState.MouseModes.Delta)
                {
                    Point mouseCenter = new Point(this.ClientRectangle.Width / 2, this.ClientRectangle.Height / 2);
                    MouseState.Location = mouseCenter;
                    MouseState.dX = 0;
                    MouseState.dY = 0;
                    Cursor.Position = mouseCenter;
                }
                MouseState.ChangeMouseMode = false;
            }
            else
            {
                if (MouseState.MouseMode == MouseState.MouseModes.RelativeCoords)
                {
                    MouseState.Location = e.Location;
                }
                else if (MouseState.MouseMode == MouseState.MouseModes.Delta)
                {
                    MouseState.dX += e.X - MouseState.Location.X;
                    MouseState.dY += e.Y - MouseState.Location.Y;
                    if (MouseState.Location.X != e.X || MouseState.Location.Y != e.Y)
                    {
                        Cursor.Position = MouseState.Location;
                    }
                }
            }
            MouseState.Buttons = e.Button;
            MouseState.Clicks += e.Clicks;
            MouseState.WheelTicks += e.Delta;
        }
    }
}
