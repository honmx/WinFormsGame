using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinForms_game.Entities;

namespace WinForms_game.Controllers
{
    internal static class BulletController
    {
        public static List<Bullet> bullets;

        public static void InitBullets()
        {
            bullets = new List<Bullet>();
        }

        public static List<Bullet> GetBullets()
        {
            return bullets;
        }

        public static void AddBullet(PointF position, int playerDirection)
        {
            var bullet = new Bullet(position, playerDirection);
            bullets.Add(bullet);
        }

        public static void RemoveBullet(Bullet bullet)
        {
            bullets.Remove(bullet);
        }
    }
}
