using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinForms_game.Controllers;
using WinForms_game.Helpers;
using WinForms_game.Interfaces;
using WinForms_game.Properties;
using static WinForms_game.Entities.Player;

namespace WinForms_game.Entities
{
    internal class Enemy : IEntity
    {
        public Physics physics;
        private int sizeX;
        private int sizeY;

        public double hp;

        private Image sprite;

        private int idleFrames;
        private int runFrames;
        private int hitFrames;
        private Animation currentAnimation;
        private int currentFrame;
        private int currentFramesLimit;

        public enum Direction
        {
            Left = -1,
            Right = 1
        }
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

        public Direction currentDirection;

        public Enemy(PointF position)
        {
            sizeX = 105;
            sizeY = 105;
            hp = 100;
            physics = new Physics(position, new Size(sizeX, sizeY));
            idleFrames = 11;
            runFrames = 11;
            hitFrames = 7;
            currentAnimation = Animation.Run;
            currentFrame = 0;
            currentFramesLimit = idleFrames;
            currentDirection = physics.transform.position.X < 1280 / 2 
                ? Direction.Right
                : Direction.Left;
        }

        public void Move()
        {
            if (
                currentAnimation != Animation.Hit ||
                currentAnimation == Animation.Hit && currentFrame == currentFramesLimit
            )
            {
                currentAnimation = Animation.Run;
                currentFramesLimit = runFrames;
            }

            var players = PlayerController.GetPlayers();

            foreach (var player in players)
            {
                if (physics.isCollided(player.physics.transform.position, player.physics.transform.size))
                {
                    Stop();
                    return;
                }

                currentDirection = player.physics.transform.position.X > physics.transform.position.X + sizeX / 2 
                    ? Direction.Right
                    : Direction.Left;
            }

            physics.Run((int)currentDirection * 5);
        }

        public void GetHit()
        {
            hp -= GameController.playerDamage;
            currentAnimation = Animation.Hit;
            currentFrame = 0;
            currentFramesLimit = hitFrames;
        }

        public void Stop()
        {
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
                    (int)physics.transform.position.X - (int)currentDirection * sizeX / 2,
                    (int)physics.transform.position.Y,
                    physics.transform.size.Width * (int)currentDirection,
                    physics.transform.size.Height
                ),
                new Rectangle(32 * currentFrame, 0, 32, 32),
                GraphicsUnit.Pixel
            );

            graphics.DrawRectangle(
                new Pen(Color.Red, 3),
                new Rectangle(
                    new Point(
                        (int)physics.transform.position.X - sizeX / 2,
                        (int)physics.transform.position.Y
                    ),
                    new Size(physics.transform.size.Width * (int)hp / 100, 3)
                )
            );
        }

        private Image FindAppropriateSprite()
        {
            if (currentAnimation == Animation.Idle)
                return Resources.IdlePlayer2;
            if (currentAnimation == Animation.Run)
                return Resources.RunPlayer2;
            return Resources.HitPlayer2;
        }
    }
}
