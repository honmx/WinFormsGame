using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinForms_game.Helpers;
using WinForms_game.Interfaces;

namespace WinForms_game.Entities
{
    internal class Buff : IEntity
    {
        public Physics physics;
        private Image sprite;
        public string type;
        private int sizeX;
        private int sizeY;

        public Buff(PointF position, string type, Image sprite)
        {
            sizeX = 30;
            sizeY = 30;
            physics = new Physics(position, new Size(sizeX, sizeY));
            this.sprite = sprite;
            this.type = type;
        }

        public void DrawSprite(Graphics graphics)
        {
            graphics.DrawImage(
                sprite,
                physics.transform.position.X,
                physics.transform.position.Y,
                sizeX,
                sizeY
            );
        }
    }
}
