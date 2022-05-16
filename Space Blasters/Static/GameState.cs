using Space_Blasters.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Blasters.Static
{
    public static class GameState
    {

        public static Rectangle GameResolution { get; set; }
        public static bool IsPaused { get; set; }

        public static GameObject PlayerObject { get; set; }
        public static float PlayerMaxSpeed = 8f;
        public static float PlayerAcceleration = 2f;
        public static double BulletSpeed = 12.0;
        public static double PlayerBulletsPerMinute = 600.0;
        public static double BulletFireCooldown = 0.0;

        public static long PlayerLevelBulletsShot { get; set; }
        public static long PlayerLevelBulletsHit { get; set; }
        public static long PlayerTotalBulletsShot { get; set; }
        public static long PlayerTotalBulletsHit { get; set; }
        public static bool PlayerShootingEnabled { get; set; }

        public static Dictionary<int, GameSprite> PlayerBulletSprites { get; set; }

        public static HashSet<GameObject> PlayerBullets { get; set; }

        public static HashSet<GameObject> Enemies { get; set; }

        public static HashSet<GameObject> EnemyBullets { get; set; } 

        public static HashSet<GameObject> EnemySpawnQueue { get; set; }
        public static int Level { get; set; }
        public static double LevelTimer { get; set; }

        public static HashSet<GameObject> StarField { get; set; }
        public static Rectangle StarFieldBoundary { get; set; }
        public static Bitmap[] StarImages { get; set; }

        public static Bitmap RotationImage { get; set; }
        public static float RotationAngle { get; set; }
    }
}
