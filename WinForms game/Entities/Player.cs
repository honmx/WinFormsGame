using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using WinForms_game.Controllers;
using WinForms_game.Helpers;
using WinForms_game.Interfaces;
using WinForms_game.Properties;
using Transform = WinForms_game.Helpers.Transform;

namespace WinForms_game.Entities
{
    internal class Player : IEntity
    {
        private int playerNumber;
        private int sizeX;
        private int sizeY;

        public int hp;

        public Keys keyUp;
        public Keys keyRight;
        public Keys keyDown;
        public Keys keyLeft;
        public Keys keyShoot;

        public Physics physics;
        private Image sprite;
        public Transform gunTransform;

        private int idleFrames;
        private int runFrames;
        private int hitFrames;
        private Animation currentAnimation;
        private int currentFrame;
        private int currentFramesLimit;

        public Direction currentDirection;
        public enum Animation
        {
            Idle,
            Run,
            Hit
        }
        public enum Direction
        {
            Left = -1,
            Right = 1
        }

        public Player(List<Keys> keys, PointF position, int number, int flipNumber)
        {
            sizeX = 105;
            sizeY = 105;
            hp = 100;
            keyUp = keys[0];
            keyRight = keys[1];
            keyDown = keys[2];
            keyLeft = keys[3];
            keyShoot = keys[4];
            playerNumber = number;
            physics = new Physics(position, new Size(sizeX, sizeY));
            sprite = playerNumber == 1 ? Resources.IdlePlayer1 : Resources.IdlePlayer2;
            idleFrames = 11;
            runFrames = 11;
            hitFrames = 7;
            currentAnimation = Animation.Idle;
            currentFrame = 0;
            currentFramesLimit = idleFrames;
            currentDirection = Direction.Right;
            gunTransform = new Transform(
                new PointF(
                    (int)physics.transform.position.X - (int)currentDirection * sizeX / 2 + (int)currentDirection * (physics.transform.size.Width / 2),
                    (int)physics.transform.position.Y + physics.transform.size.Height / 2 - currentFrame / 2 - 7
                ),
                new Size(
                    physics.transform.size.Width * (int)currentDirection / 3 * 2,
                    physics.transform.size.Height / 3 * 2
                )
            );
        }

        public void Move(int dx)
        {
            if (
                currentAnimation != Animation.Hit ||
                currentAnimation == Animation.Hit && currentFrame == currentFramesLimit &&
                !physics.isJumping
            )
            {
                currentAnimation = Animation.Run;
                currentFramesLimit = runFrames;
            }

            if (physics.IsCollidedByXWithLeftEdge(dx))
            {
                physics.transform.position.X = 300 - dx;
            }

            if (physics.IsCollidedByXWithRightEdge(dx))
            {                                             
                physics.transform.position.X = 1000 - physics.transform.size.Width - dx;
            }

            physics.Run(dx);
        }

        public void Stop()
        {
            currentAnimation = Animation.Idle;
            currentFramesLimit = idleFrames;
            physics.Stop();
        }

        public void Jump()
        {
            physics.Jump();
        }

        public void GetHit()
        {
            if (currentAnimation == Animation.Hit) return;
            currentAnimation = Animation.Hit;
            currentFramesLimit = hitFrames;
            hp -= 10;
        }

        public void Heal()
        {
            hp = 100;
        }

        public void DrawSprite(Graphics graphics)
        {
            currentFrame++;

            if (currentFrame >= currentFramesLimit) currentFrame = 0;

            sprite = FindAppropriateSprite();

            graphics.DrawImage(
                sprite,
                new Rectangle(
                    (int)physics.transform.position.X - (int)currentDirection * sizeX / 2,
                    (int)physics.transform.position.Y,
                    physics.transform.size.Width * (int)currentDirection,
                    physics.transform.size.Height
                ),
                new Rectangle(32 * currentFrame, 0, 32, 32),
                GraphicsUnit.Pixel
            );

            UpdateGunPosition();

            graphics.DrawImage(
                Resources.Pistol,
                new Rectangle(
                    (int)gunTransform.position.X, (int)gunTransform.position.Y,
                    gunTransform.size.Width, gunTransform.size.Height
                ),
                new Rectangle(150, 150, 212, 212),
                GraphicsUnit.Pixel
            );
        }

        private void UpdateGunPosition()
        {
            gunTransform.position.X = (int)physics.transform.position.X - (int)currentDirection * sizeX / 2 + (int)currentDirection * (physics.transform.size.Width / 2);
            gunTransform.position.Y = (int)physics.transform.position.Y + physics.transform.size.Height / 2 - currentFrame / 2 - 7;
            gunTransform.size.Width = physics.transform.size.Width * (int)currentDirection / 3 * 2;
        }

        private Image FindAppropriateSprite()
        {
            if (playerNumber == 1)
            {
                if (currentAnimation == Animation.Run)
                    return Resources.RunPlayer1;
                if (currentAnimation == Animation.Hit)
                    return Resources.HitPlayer1;

                return Resources.IdlePlayer1;
            }
            else
            {
                if (currentAnimation == Animation.Run)
                    return Resources.RunPlayer2;
                if (currentAnimation == Animation.Hit)
                    return Resources.HitPlayer2;    

                return Resources.IdlePlayer2;
            }
        }
    }
}
