using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WinForms_game.Helpers;
using WinForms_game.Interfaces;
using WinForms_game.Properties;

namespace WinForms_game.Entities
{
    internal class Wall : IEntity
    {
        private Image sprite;
        public Transform transform;
        private int sizeX;
        private int sizeY;

        public Wall(PointF position, Image image)
        {
            sprite = image;
            sizeX = 60;
            sizeY = 60;
            transform = new Transform(position, new Size(sizeX, sizeY));
        }

        public void DrawSprite(Graphics graphics)
        {
            graphics.DrawImage(
                sprite,
                transform.position.X,
                transform.position.Y,
                transform.size.Width,
                transform.size.Height
            );
        }
    }
}
