using Space_Blasters.Static;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Space_Blasters.Models
{
    public class GameLoop
    {
        private Game _myGame;

        public bool Running { get; private set; }

        public void Load(Game gameObj)
        {
            _myGame = gameObj;
        }

        public void Start()
        {
            if (_myGame == null)
            {
                throw new ArgumentException("Game not loaded!");
            }

            _myGame.Load();

            Running = true;
            _previousGameTime = DateTime.Now;
        }

        DateTime _previousGameTime;

        /// <summary>
        /// Returns true if we should repaint
        /// </summary>
        /// <returns></returns>
        public bool Update()
        {
            if (Running)
            {
                TimeSpan GameTime = DateTime.Now - _previousGameTime;
                if (GameTime.TotalMilliseconds > 10)
                {
                    _previousGameTime += GameTime;
                    _myGame.Update(GameTime);
                    return true;
                }
            }
            return false;
        }

        public void Stop()
        {
            Running = false;
            _myGame?.Unload();
        }

        public void Draw(Graphics g)
        {
            _myGame.Draw(g);
        }
    }

}
