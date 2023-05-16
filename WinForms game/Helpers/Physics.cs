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
        public float gravity;
        public float acceleration;
        public bool isMoving;
        public bool isJumping;
        public bool isGettingHit;

        public Physics(PointF position, Size size)
        {
            transform = new Transform(position, size);
            dx = 0;
            gravity = -30;
            acceleration = 2f;
            isMoving = false;
            isJumping = false;
        }

        public void Run(int newDx)
        {
            isMoving = true;
            dx = newDx;
            transform.position.X += dx;
        }

        public void Stop()
        {
            isMoving = false;
            dx = 0;
        }

        public void Jump()
        {
            gravity += acceleration;

            if (gravity == 30)
            {
                gravity = -30;
                isJumping = false;
                return;
            }

            transform.position.Y += gravity;
        }

        public bool IsCollidedByXWithLeftEdge(int dx)
        {
            return transform.position.X + dx < 300;
        }

        public bool IsCollidedByXWithRightEdge(int dx)
        {
            return transform.position.X + transform.size.Width + dx > 1000;
        }

        public bool isCollided(PointF objectPosition, Size objectSize)
        {
            return
                transform.position.X + transform.size.Width / 2 >= objectPosition.X &&
                transform.position.X + transform.size.Width / 2 <= objectPosition.X + objectSize.Width &&
                transform.position.Y + transform.size.Height / 2 >= objectPosition.Y &&
                transform.position.Y + transform.size.Height / 2 <= objectPosition.Y + objectSize.Height;
        }
    }
}
