using Space_Blasters.Static;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Space_Blasters.Models
{
    public class SpaceBlasterEngine
    {
        public void Load()
        {
            // Start the game paused for now.
            GameState.IsPaused = true;
            // Create the player object
            GameState.PlayerObject = new GameObject();
            GameState.PlayerObject.Sprite = new GameSprite();
            GameState.PlayerObject.Sprite.SpriteArray = new Bitmap[16];
            GameState.PlayerObject.Sprite.SpriteArray[0] = Properties.Resources.ship1_0;
            GameState.PlayerObject.Sprite.SpriteArray[1] = Properties.Resources.ship1_1;
            GameState.PlayerObject.Sprite.SpriteArray[2] = Properties.Resources.ship1_2;
            GameState.PlayerObject.Sprite.SpriteArray[3] = Properties.Resources.ship1_3;
            GameState.PlayerObject.Sprite.SpriteArray[4] = Properties.Resources.ship1_4;
            GameState.PlayerObject.Sprite.SpriteArray[5] = Properties.Resources.ship1_5;
            GameState.PlayerObject.Sprite.SpriteArray[6] = Properties.Resources.ship1_6;
            GameState.PlayerObject.Sprite.SpriteArray[7] = Properties.Resources.ship1_7;
            GameState.PlayerObject.Sprite.SpriteArray[8] = Properties.Resources.ship1_8;
            GameState.PlayerObject.Sprite.SpriteArray[9] = Properties.Resources.ship1_9;
            GameState.PlayerObject.Sprite.SpriteArray[10] = Properties.Resources.ship1_10;
            GameState.PlayerObject.Sprite.SpriteArray[11] = Properties.Resources.ship1_11;
            GameState.PlayerObject.Sprite.SpriteArray[12] = Properties.Resources.ship1_12;
            GameState.PlayerObject.Sprite.SpriteArray[13] = Properties.Resources.ship1_13;
            GameState.PlayerObject.Sprite.SpriteArray[14] = Properties.Resources.ship1_14;
            GameState.PlayerObject.Sprite.SpriteArray[15] = Properties.Resources.ship1_15;
            GameState.PlayerObject.Sprite.SpriteImage = GameState.PlayerObject.Sprite.SpriteArray[4];

            // load the player bullet images
            GameState.PlayerBulletSprites = new Dictionary<int, GameSprite>();
            GameState.PlayerBulletSprites[0] = new GameSprite(Properties.Resources.PlayerBullet_0, 55, 28);
            GameState.PlayerBulletSprites[1] = new GameSprite(Properties.Resources.PlayerBullet_1, 54, 16);
            GameState.PlayerBulletSprites[2] = new GameSprite(Properties.Resources.PlayerBullet_2, 48, 5);
            GameState.PlayerBulletSprites[3] = new GameSprite(Properties.Resources.PlayerBullet_3, 41, -2);
            GameState.PlayerBulletSprites[4] = new GameSprite(Properties.Resources.PlayerBullet_4, 28, -3);
            GameState.PlayerBulletSprites[5] = new GameSprite(Properties.Resources.PlayerBullet_5, 16, -2);
            GameState.PlayerBulletSprites[6] = new GameSprite(Properties.Resources.PlayerBullet_6, 4, 6);
            GameState.PlayerBulletSprites[7] = new GameSprite(Properties.Resources.PlayerBullet_7, -2, 17);
            GameState.PlayerBulletSprites[8] = new GameSprite(Properties.Resources.PlayerBullet_8, -4, 28);
            GameState.PlayerBulletSprites[9] = new GameSprite(Properties.Resources.PlayerBullet_9, -1, 40);
            GameState.PlayerBulletSprites[10] = new GameSprite(Properties.Resources.PlayerBullet_10, 4, 46);
            GameState.PlayerBulletSprites[11] = new GameSprite(Properties.Resources.PlayerBullet_11, 17, 52);
            GameState.PlayerBulletSprites[12] = new GameSprite(Properties.Resources.PlayerBullet_12, 29, 54);
            GameState.PlayerBulletSprites[13] = new GameSprite(Properties.Resources.PlayerBullet_13, 39, 52);
            GameState.PlayerBulletSprites[14] = new GameSprite(Properties.Resources.PlayerBullet_14, 48, 47);
            GameState.PlayerBulletSprites[15] = new GameSprite(Properties.Resources.PlayerBullet_15, 53, 40);

            GameState.PlayerObject.Sprite.Width = GameState.PlayerObject.Sprite.SpriteImage.Width;
            GameState.PlayerObject.Sprite.Height = GameState.PlayerObject.Sprite.SpriteImage.Height;
            GameState.PlayerObject.Sprite.X = (GameState.GameResolution.Width - GameState.PlayerObject.Sprite.Width) / 2;
            GameState.PlayerObject.Sprite.Y = (GameState.GameResolution.Height - GameState.PlayerObject.Sprite.Height) / 2;
            GameState.PlayerObject.Kinematic = new KinematicObject();
            GameState.PlayerObject.Kinematic.dX = 0;
            GameState.PlayerObject.Kinematic.dY = 0;
            GameState.PlayerBullets = new HashSet<GameObject>();

            GameState.PlayerShootingEnabled = false;
            GameState.EnemySpawnQueue = new HashSet<GameObject>();
            GameState.Enemies = new HashSet<GameObject>();
            GameState.EnemyBullets = new HashSet<GameObject>();
            GameState.Level = 0;
            GameState.PlayerLevelBulletsShot = 0;
            GameState.PlayerLevelBulletsHit = 0;
            GameState.PlayerTotalBulletsShot = 0;
            GameState.PlayerTotalBulletsHit = 0;

            GameState.StarField = new HashSet<GameObject>();
            GameState.StarFieldBoundary = new Rectangle(-10, -500, GameState.GameResolution.Width + 20, GameState.GameResolution.Height + 1000);
            GameState.StarImages = new Bitmap[]
            {
                Properties.Resources.star_blue,
                Properties.Resources.star_orange,
                Properties.Resources.star_redgiant,
                Properties.Resources.star_white,
                Properties.Resources.star_yellow
            };

            Random rnd = new Random();
            for (int i = 0; i < 100; i++)
            {
                GameObject newStar = new GameObject();
                float speed = ((float)rnd.Next() / (float)int.MaxValue) * 1.5f + 1.5f;
                int x = rnd.Next(GameState.StarFieldBoundary.Left, GameState.StarFieldBoundary.Right);
                int y = rnd.Next(GameState.StarFieldBoundary.Top, GameState.StarFieldBoundary.Bottom);
                int idx = rnd.Next(0, GameState.StarImages.Length);
                newStar.Sprite = new GameSprite(GameState.StarImages[idx], x, y);
                newStar.Kinematic = new KinematicObject();
                newStar.Kinematic.dX = 0;
                newStar.Kinematic.dY = speed;
                GameState.StarField.Add(newStar);
            }

            GameState.RotationAngle = 0f;
            GameState.RotationImage = Properties.Resources.Toast;

        }

        public void SetupLevel(int level)
        {
            GameState.BulletFireCooldown = 0;
            GameState.PlayerShootingEnabled = true;
            GameState.Enemies.Clear();
            GameState.PlayerLevelBulletsShot = 0;
            GameState.PlayerLevelBulletsHit = 0;
            GameState.LevelTimer = 0;
            GameState.EnemySpawnQueue = new HashSet<GameObject>();


        }

        public void Update(double gameTime)
        {
            GameState.RotationAngle += (float)gameTime * 100f;
            GameState.LevelTimer += gameTime;
            Random rnd = new Random();
            // animate star field
            foreach (var star in GameState.StarField)
            {
                star.Sprite.Y += star.Kinematic.dY;
                if (star.Sprite.Y > GameState.StarFieldBoundary.Bottom)
                {
                    star.Sprite.Y = GameState.StarFieldBoundary.Top;
                    star.Sprite.X = rnd.Next(GameState.StarFieldBoundary.Left, GameState.StarFieldBoundary.Right);
                    star.Kinematic.dY = ((float)rnd.Next() / (float)int.MaxValue) * 1.5f + 1.5f;
                    int idx = rnd.Next(0, GameState.StarImages.Length);
                    star.Sprite.SpriteImage = GameState.StarImages[idx];
                }
            }

            // update position of all player bullets
            foreach (var pbullet in GameState.PlayerBullets)
            {
                pbullet.Sprite.X += pbullet.Kinematic.dX;
                pbullet.Sprite.Y += pbullet.Kinematic.dY;
                pbullet.Lifetime -= gameTime;
            }

            GameState.PlayerBullets.RemoveWhere(x => x.Lifetime < 0.0);


            // movement speed force for mouse delta and keyboard arrow keys
            float force = (float)(GameState.PlayerAcceleration * gameTime);

            //mouse movement input
            GameState.PlayerObject.Kinematic.dX += MouseState.dX * force * 0.3f;
            GameState.PlayerObject.Kinematic.dY += MouseState.dY * force * 0.3f;
            // cap player movement. This is a quick hack because it's 1 AM.
            // I'll change this to calculate the angle the player is moving at, and 
            // properly limit them to the actual speed.
            if (GameState.PlayerObject.Kinematic.dX > GameState.PlayerMaxSpeed)
            {
                GameState.PlayerObject.Kinematic.dX = GameState.PlayerMaxSpeed;
            }
            if (GameState.PlayerObject.Kinematic.dY > GameState.PlayerMaxSpeed)
            {
                GameState.PlayerObject.Kinematic.dY = GameState.PlayerMaxSpeed;
            }
            if (GameState.PlayerObject.Kinematic.dX < -1 * GameState.PlayerMaxSpeed)
            {
                GameState.PlayerObject.Kinematic.dX = -1 * GameState.PlayerMaxSpeed;
            }
            if (GameState.PlayerObject.Kinematic.dY < -1 * GameState.PlayerMaxSpeed)
            {
                GameState.PlayerObject.Kinematic.dY = -1 * GameState.PlayerMaxSpeed;
            }

            //keyboard movement input
            if ((Keyboard.GetKeyStates(Key.Right) & KeyStates.Down) > 0)
            {
                GameState.PlayerObject.Kinematic.dX += force;
                if (GameState.PlayerObject.Kinematic.dX > GameState.PlayerMaxSpeed)
                {
                    GameState.PlayerObject.Kinematic.dX = GameState.PlayerMaxSpeed;
                }
            }
            if ((Keyboard.GetKeyStates(Key.Left) & KeyStates.Down) > 0)
            {
                GameState.PlayerObject.Kinematic.dX -= force;
                if (GameState.PlayerObject.Kinematic.dX < -1 * GameState.PlayerMaxSpeed)
                {
                    GameState.PlayerObject.Kinematic.dX = -1 * GameState.PlayerMaxSpeed;
                }
            }
            if ((Keyboard.GetKeyStates(Key.Up) & KeyStates.Down) > 0)
            {
                GameState.PlayerObject.Kinematic.dY -= force;
                if (GameState.PlayerObject.Kinematic.dY < -1 * GameState.PlayerMaxSpeed)
                {
                    GameState.PlayerObject.Kinematic.dY = -1 * GameState.PlayerMaxSpeed;
                }
            }
            if ((Keyboard.GetKeyStates(Key.Down) & KeyStates.Down) > 0)
            {
                GameState.PlayerObject.Kinematic.dY += force;
                if (GameState.PlayerObject.Kinematic.dY > GameState.PlayerMaxSpeed)
                {
                    GameState.PlayerObject.Kinematic.dY = GameState.PlayerMaxSpeed;
                }
            }

            GameState.PlayerObject.Sprite.X += GameState.PlayerObject.Kinematic.dX;
            GameState.PlayerObject.Sprite.Y += GameState.PlayerObject.Kinematic.dY;

            // Keep ship in screen bounds
            if (GameState.PlayerObject.Sprite.X + GameState.PlayerObject.Sprite.Width >
                GameState.GameResolution.Width)
            {
                GameState.PlayerObject.Sprite.X = GameState.GameResolution.Width - GameState.PlayerObject.Sprite.Width;
                GameState.PlayerObject.Kinematic.dX = 0;
            }
            if (GameState.PlayerObject.Sprite.X < 0)
            {
                GameState.PlayerObject.Sprite.X = 0;
                GameState.PlayerObject.Kinematic.dX = 0;
            }
            if (GameState.PlayerObject.Sprite.Y + GameState.PlayerObject.Sprite.Height >
                GameState.GameResolution.Height)
            {
                GameState.PlayerObject.Sprite.Y = GameState.GameResolution.Height - GameState.PlayerObject.Sprite.Height;
                GameState.PlayerObject.Kinematic.dY = 0;
            }
            if (GameState.PlayerObject.Sprite.Y < 0)
            {
                GameState.PlayerObject.Sprite.Y = 0;
                GameState.PlayerObject.Kinematic.dY = 0;
            }

            // calculate the direction the ship should face
            double theta = Math.Atan2(GameState.PlayerObject.Kinematic.dY * -1, GameState.PlayerObject.Kinematic.dX) * (180 / Math.PI);
            if (theta < 0)
            {
                theta += 360;
            }
            int ShipRotationIndex = ThetaToShipIndex(theta);
            // now override that actual angle if the ship is against a wall. We want to point the ship into the field, and the speed
            // determines how far it leans in the direction of travel.

            // Ship pinned on left side
            if (GameState.PlayerObject.Sprite.X < 10)
            {
                if (GameState.PlayerObject.Kinematic.dY >= -1.5 &&
                    GameState.PlayerObject.Kinematic.dY <= 1.5)
                {
                    ShipRotationIndex = 0;
                }
                else if (GameState.PlayerObject.Kinematic.dY < -5)
                {
                    ShipRotationIndex = 4;
                }
                else if (GameState.PlayerObject.Kinematic.dY > 5)
                {
                    ShipRotationIndex = 12;
                }
                else if (GameState.PlayerObject.Kinematic.dY < -4)
                {
                    ShipRotationIndex = 3;
                }
                else if (GameState.PlayerObject.Kinematic.dY > 4)
                {
                    ShipRotationIndex = 13;
                }
                else if (GameState.PlayerObject.Kinematic.dY < -3)
                {
                    ShipRotationIndex = 2;
                }
                else if (GameState.PlayerObject.Kinematic.dY > 3)
                {
                    ShipRotationIndex = 14;
                }
                else if (GameState.PlayerObject.Kinematic.dY < -1.5)
                {
                    ShipRotationIndex = 1;
                }
                else if (GameState.PlayerObject.Kinematic.dY > 1.5)
                {
                    ShipRotationIndex = 15;
                }
            }

            // Ship pinned to the right
            if (GameState.GameResolution.Width - GameState.PlayerObject.Sprite.X - GameState.PlayerObject.Sprite.Width < 10)
            {
                if (GameState.PlayerObject.Kinematic.dY >= -1.5 &&
                    GameState.PlayerObject.Kinematic.dY <= 1.5)
                {
                    ShipRotationIndex = 8;
                }
                else if (GameState.PlayerObject.Kinematic.dY < -5)
                {
                    ShipRotationIndex = 4;
                }
                else if (GameState.PlayerObject.Kinematic.dY > 5)
                {
                    ShipRotationIndex = 12;
                }
                else if (GameState.PlayerObject.Kinematic.dY < -4)
                {
                    ShipRotationIndex = 5;
                }
                else if (GameState.PlayerObject.Kinematic.dY > 4)
                {
                    ShipRotationIndex = 11;
                }
                else if (GameState.PlayerObject.Kinematic.dY < -3)
                {
                    ShipRotationIndex = 6;
                }
                else if (GameState.PlayerObject.Kinematic.dY > 3)
                {
                    ShipRotationIndex = 10;
                }
                else if (GameState.PlayerObject.Kinematic.dY < -1.5)
                {
                    ShipRotationIndex = 7;
                }
                else if (GameState.PlayerObject.Kinematic.dY > 1.5)
                {
                    ShipRotationIndex = 9;
                }
            }

            // Ship pinned to the top
            if (GameState.PlayerObject.Sprite.Y < 10)
            {
                if (GameState.PlayerObject.Kinematic.dX >= -1.5 &&
                    GameState.PlayerObject.Kinematic.dX <= 1.5)
                {
                    ShipRotationIndex = 12;
                }
                else if (GameState.PlayerObject.Kinematic.dX < -5)
                {
                    ShipRotationIndex = 8;
                }
                else if (GameState.PlayerObject.Kinematic.dX > 5)
                {
                    ShipRotationIndex = 0;
                }
                else if (GameState.PlayerObject.Kinematic.dX < -4)
                {
                    ShipRotationIndex = 9;
                }
                else if (GameState.PlayerObject.Kinematic.dX > 4)
                {
                    ShipRotationIndex = 15;
                }
                else if (GameState.PlayerObject.Kinematic.dX < -3)
                {
                    ShipRotationIndex = 10;
                }
                else if (GameState.PlayerObject.Kinematic.dX > 3)
                {
                    ShipRotationIndex = 14;
                }
                else if (GameState.PlayerObject.Kinematic.dX < -1.5)
                {
                    ShipRotationIndex = 11;
                }
                else if (GameState.PlayerObject.Kinematic.dX > 1.5)
                {
                    ShipRotationIndex = 13;
                }
            }

            // Ship pinned to the bottom
            if (GameState.GameResolution.Height - GameState.PlayerObject.Sprite.Y - GameState.PlayerObject.Sprite.Height < 10)
            {
                if (GameState.PlayerObject.Kinematic.dX >= -1.5 &&
                    GameState.PlayerObject.Kinematic.dX <= 1.5)
                {
                    ShipRotationIndex = 4;
                }
                else if (GameState.PlayerObject.Kinematic.dX < -5)
                {
                    ShipRotationIndex = 8;
                }
                else if (GameState.PlayerObject.Kinematic.dX > 5)
                {
                    ShipRotationIndex = 0;
                }
                else if (GameState.PlayerObject.Kinematic.dX < -4)
                {
                    ShipRotationIndex = 7;
                }
                else if (GameState.PlayerObject.Kinematic.dX > 4)
                {
                    ShipRotationIndex = 1;
                }
                else if (GameState.PlayerObject.Kinematic.dX < -3)
                {
                    ShipRotationIndex = 6;
                }
                else if (GameState.PlayerObject.Kinematic.dX > 3)
                {
                    ShipRotationIndex = 2;
                }
                else if (GameState.PlayerObject.Kinematic.dX < -1.5)
                {
                    ShipRotationIndex = 5;
                }
                else if (GameState.PlayerObject.Kinematic.dX > 1.5)
                {
                    ShipRotationIndex = 3;
                }
            }

            //Apply the ship angle override
            GameState.PlayerObject.Sprite.SpriteImage = GameState.PlayerObject.Sprite.SpriteArray[ShipRotationIndex];

            // check to see if they want to shoot
            if (GameState.BulletFireCooldown > 0)
            {
                GameState.BulletFireCooldown -= gameTime;
            }
            else
            {
                if (KeyboardState.RightShiftHeld || KeyboardState.LeftShiftHeld || MouseState.LeftHeld)
                {
                    GameState.BulletFireCooldown = 60.0 / GameState.PlayerBulletsPerMinute;
                    System.Diagnostics.Debug.WriteLine("Time: {0}, Bullet Time: {1}", gameTime, GameState.BulletFireCooldown);
                    // spawn a bullet.
                    GameObject bullet = new GameObject();
                    bullet.Sprite = new GameSprite();
                    GameSprite bulletSprite = GameState.PlayerBulletSprites[ShipRotationIndex];
                    bullet.Sprite.SpriteImage = bulletSprite.SpriteImage;
                    bullet.Sprite.SpriteArray = new Bitmap[0];
                    bullet.Sprite.Width = bulletSprite.Width;
                    bullet.Sprite.Height = bulletSprite.Height;
                    // test values
                    bullet.Sprite.X = GameState.PlayerObject.Sprite.X + bulletSprite.X;
                    bullet.Sprite.Y = GameState.PlayerObject.Sprite.Y + bulletSprite.Y;
                    bullet.Kinematic = new KinematicObject();
                    double bulletTheta = (360 - ShipIndexToTheta(ShipRotationIndex)) * (Math.PI / 180);
                    if (bulletTheta > 180)
                    {
                        bulletTheta -= 360;
                    }
                    bullet.Kinematic.dX = (float)(GameState.BulletSpeed * Math.Cos(bulletTheta)) + GameState.PlayerObject.Kinematic.dX;
                    bullet.Kinematic.dY = (float)(GameState.BulletSpeed * Math.Sin(bulletTheta)) + GameState.PlayerObject.Kinematic.dY;
                    bullet.Lifetime = 4.0;
                    GameState.PlayerBullets.Add(bullet);
                }
            }
        }


        public static int ThetaToShipIndex(double theta)
        {
            if (theta >= 348.75 || theta < 11.25)
            {
                return 0;
            }
            else if (theta < 33.75)
            {
                return 1;
            }
            else if (theta < 56.25)
            {
                return 2;
            }
            else if (theta < 78.75)
            {
                return 3;
            }
            else if (theta < 101.25)
            {
                return 4;
            }
            else if (theta < 123.75)
            {
                return 5;
            }
            else if (theta < 146.25)
            {
                return 6;
            }
            else if (theta < 168.75)
            {
                return 7;
            }
            else if (theta < 191.25)
            {
                return 8;
            }
            else if (theta < 213.75)
            {
                return 9;
            }
            else if (theta < 236.25)
            {
                return 10;
            }
            else if (theta < 258.75)
            {
                return 11;
            }
            else if (theta < 281.25)
            {
                return 12;
            }
            else if (theta < 303.75)
            {
                return 13;
            }
            else if (theta < 326.25)
            {
                return 14;
            }
            else if (theta < 348.75)
            {
                return 15;
            }
            else
            {
                return 4;
            }
        }

        public static double ShipIndexToTheta(int index)
        {
            switch (index)
            {
                case 0:
                    return 0.0;
                case 1:
                    return 22.5;
                case 2:
                    return 45.0;
                case 3:
                    return 67.5;
                case 4:
                    return 90.0;
                case 5:
                    return 112.5;
                case 6:
                    return 135.0;
                case 7:
                    return 157.5;
                case 8:
                    return 180.0;
                case 9:
                    return 202.5;
                case 10:
                    return 225.0;
                case 11:
                    return 247.5;
                case 12:
                    return 270.0;
                case 13:
                    return 292.5;
                case 14:
                    return 315.0;
                case 15:
                    return 337.5;
                default:
                    return 90.0;
            }
        }

    }
}
