using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using WinForms_game.Entities;
using WinForms_game.Interfaces;

namespace WinForms_game.Helpers
{
    internal class Physics
    {
        public Transform transform;
        public float dx;
        double gravity;
        double acceleration;

        public Physics(PointF position, Size size)
        {
            transform = new Transform(position, size);
            gravity = 0;
            acceleration = 0.4;
            dx = 0;
        }

        public void Run(int newDx)
        {
            dx = newDx;
            transform.position.X += dx;
        }

        public void Stop()
        {
            dx = 0;
        }

        public void Jump()
        {

        }

        public bool IsCollidedByX(PointF objectPosition, int dx)
        {
            if (transform.position.X + dx < 25)
            {
                transform.position.X = 25;
                return true;
            }

            if (transform.position.X + transform.size.Width + dx > 1280 + 31)
            {
                transform.position.X = 1280 - transform.size.Width + 31;
                return true;
            }

            if (
                transform.position.X + 60 - 27 + dx > objectPosition.X && transform.position.X < objectPosition.X &&
                transform.position.Y + 60 > objectPosition.Y && transform.position.Y < objectPosition.Y
            )
            {
                transform.position.X = objectPosition.X - 60 + 27;
                return true;
            }

            if (
                transform.position.X + dx < objectPosition.X + 60 + 31 && transform.position.X > objectPosition.X &&
                transform.position.Y + 60 > objectPosition.Y && transform.position.Y < objectPosition.Y
            )
            {
                transform.position.X = objectPosition.X + 60 + 31;
                return true;
            }

            return false;
        }
    }
}
