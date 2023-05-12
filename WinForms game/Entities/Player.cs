using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using WinForms_game.Controllers;
using WinForms_game.Helpers;
using WinForms_game.Interfaces;
using WinForms_game.Properties;

namespace WinForms_game.Entities
{
    internal class Player : IEntity
    {
        private int playerNumber;
        public int sizeX;
        public int sizeY;

        public Keys keyUp;
        public Keys keyRight;
        public Keys keyDown;
        public Keys keyLeft;

        private Physics physics;
        private Image sprite;

        private int idleFrames;
        private Animation currentAnimation;
        private int currentFrame;
        private int currentFramesLimit;

        public Direction currentDirection;
        public enum Animation
        {
            Idle,
            Run,
            Jump,
            DoubleJump,
            WallJump,
            Fall,
            Hit
        }
        public enum Direction
        {
            Left = -1,
            Right = 1
        }

        public int flip;
        public bool isMoving;

        public Player(List<Keys> keys, PointF position, int number, int flipNumber)
        {
            sizeX = 75;
            sizeY = 75;
            keyUp = keys[0];
            keyRight = keys[1];
            keyDown = keys[2];
            keyLeft = keys[3];
            playerNumber = number;
            physics = new Physics(position, new Size(sizeX, sizeY));
            sprite = playerNumber == 1 ? Resources.IdlePlayer1 : Resources.Box;
            idleFrames = 11;
            currentAnimation = Animation.Idle;
            currentFrame = 0;
            currentFramesLimit = idleFrames;
            currentDirection = Direction.Right;
            flip = flipNumber;
            isMoving = false;
        }

        public void Move(int dx)
        {
            currentAnimation = Animation.Run;
            var obstacles = GameController.GetAllObstacles();

            for (int i = 0; i < obstacles.Count; i++)
            {
                if (physics.IsCollidedByX(obstacles[i], dx))
                {
                    physics.Stop();
                    return;
                }
            }

            physics.Run(dx);
        }

        public void Stop()
        {
            isMoving = false;
            currentAnimation = Animation.Idle;
            physics.Stop();
        }

        public void DrawSprite(Graphics graphics)
        {
            currentFrame++;

            if (currentFrame == currentFramesLimit) currentFrame = 0;

            sprite = FindAppropriateSprite();

            graphics.DrawImage(
                sprite,
                new Rectangle(
                    (int)physics.transform.position.X - flip * sizeX / 2,
                    (int)physics.transform.position.Y,
                    physics.transform.size.Width * flip,
                    physics.transform.size.Height
                ),
                new Rectangle(32 * currentFrame, 0, 32, 32),
                GraphicsUnit.Pixel
            );
        }

        private Image FindAppropriateSprite()
        {
            if (playerNumber == 1)
            {
                if (currentAnimation == Animation.Idle)
                    return Resources.IdlePlayer1;
                if (currentAnimation == Animation.Run)
                    return Resources.RunPlayer1;
                if (currentAnimation == Animation.Jump)
                    return Resources.JumpPlayer1;
                if (currentAnimation == Animation.DoubleJump)
                    return Resources.DoubleJumpPlayer1;
                if (currentAnimation == Animation.WallJump)
                    return Resources.WallJumpPlayer1;
                if (currentAnimation == Animation.Fall)
                    return Resources.FallPlayer1;
                return Resources.HitPlayer1;
            }
            else
            {
                if (currentAnimation == Animation.Idle)
                    return Resources.IdlePlayer2;
                if (currentAnimation == Animation.Run)
                    return Resources.RunPlayer2;
                if (currentAnimation == Animation.Jump)
                    return Resources.JumpPlayer2;
                if (currentAnimation == Animation.DoubleJump)
                    return Resources.DoubleJumpPlayer2;
                if (currentAnimation == Animation.WallJump)
                    return Resources.WallJumpPlayer2;
                if (currentAnimation == Animation.Fall)
                    return Resources.FallPlayer2;
                return Resources.HitPlayer2;
            }
        }
    }
}
