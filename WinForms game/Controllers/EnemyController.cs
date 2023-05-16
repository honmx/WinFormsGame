using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinForms_game.Entities;

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
