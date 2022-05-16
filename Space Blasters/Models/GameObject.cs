using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Blasters.Models
{
    public class GameObject
    {
        // stores graphical information and rendering routine
        public GameSprite Sprite { get; set; }
        // Stores info about the direction of movement for the object
        public KinematicObject Kinematic { get; set; }
        // This is used to despawn bullets
        public double Lifetime { get; set; }

        
    }
}
