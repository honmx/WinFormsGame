using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinForms_game.Helpers;
using WinForms_game.Interfaces;

namespace WinForms_game.Entities
{
    internal class Box : IEntity
    {
        Image sprite;
        public Transform transform;
        public int sizeX;
        public int sizeY;
        public bool isTouchedByPlayer;

        public Box(PointF position, Image image)
        {
            sprite = image;
            sizeX = 60;
            sizeY = 60;
            transform = new Transform(position, new Size(sizeX, sizeY));
            isTouchedByPlayer = false;
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
