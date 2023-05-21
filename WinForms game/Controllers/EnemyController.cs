using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinForms_game.Entities;
using Timer = System.Windows.Forms.Timer;

namespace WinForms_game.Controllers
{
    internal static class EnemyController
    {
        public static List<Enemy> enemies;
        public static List<PointF> enemySpots;

        public static void InitEnemies()
        {
            enemies = new List<Enemy>();
            enemySpots = GameController.GetByNumber(5);

            for (var i = 0; i < enemySpots.Count; i++)
            {
                var enemy = new Enemy(enemySpots[i]);
                enemies.Add(enemy);
            }
        }

        public static void Update(GameScreen gameScreen, Timer timer)
        {
            for (var i = 0; i < enemies.Count; i++)
            {
                var enemy = enemies[i];

                if (enemy.hp <= 0)
                {
                    GameController.AddKill();
                    KillEnemy(enemy);
                    BuffController.SpawnBuffWithChance(25);
                }

                var bullets = BulletController.GetBullets();

                for (var j = 0; j < bullets.Count; j++)
                {
                    var bullet = bullets[j];

                    if (bullet.physics.transform.position.X < 0 || bullet.physics.transform.position.X > 1280)
                    {
                        BulletController.RemoveBullet(bullet);
                    }

                    if (bullet.physics.isCollided(enemy.physics.transform.position, enemy.physics.transform.size))
                    {
                        enemy.GetHit();
                        BulletController.RemoveBullet(bullet);
                    }
                }

                enemy.Move();
            }
        }

        public static void SpawnEnemy()
        {
            var random = new Random();
            var index = random.Next(enemySpots.Count);

            var enemy = new Enemy(enemySpots[index]);
            enemies.Add(enemy);
        }

        public static void KillEnemy(Enemy enemy)
        {
            enemies.Remove(enemy);
        }

        public static List<Enemy> GetEnemies()
        {
            return enemies;
        }
    }
}
