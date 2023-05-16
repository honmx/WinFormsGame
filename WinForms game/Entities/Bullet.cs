using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using WinForms_game.Helpers;
using WinForms_game.Interfaces;
using WinForms_game.Properties;

namespace WinForms_game.Entities
{
    internal class Bullet : IEntity
    {
        public Physics physics;
        private int sizeX;
        private int sizeY;
        private Image sprite;
        private int direction;

        public Bullet(PointF position, int playerDirection)
        {
            sprite = Resources.Bullet;
            sizeX = 10;
            sizeY = 5;
            direction = playerDirection;
            physics = new Physics(
                new PointF(
                    position.X + playerDirection * 35,
                    position.Y + 25
                ),
                new Size(sizeX, sizeY)
            );
        }

        public void DrawSprite(Graphics graphics)
        {
            physics.transform.position.X += 25 * direction;

            graphics.DrawImage(
                sprite,
                physics.transform.position.X,
                physics.transform.position.Y,
                physics.transform.size.Width,
                physics.transform.size.Height
            );
        }
    }
}
