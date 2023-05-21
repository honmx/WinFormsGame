using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinForms_game.Entities;
using Timer = System.Windows.Forms.Timer;

namespace WinForms_game.Controllers
{
    internal static class PlayerController
    {
        static List<Player> players;
        private static List<List<Keys>> playersKeys;

        public static void InitPlayers()
        {
            playersKeys = new List<List<Keys>>(
                new List<Keys>[1]
                {
                    new List<Keys>( new Keys[5] { Keys.W, Keys.D, Keys.S, Keys.A, Keys.Space })
                }
            );

            players = new List<Player>();
            var playersOnMap = GameController.GetByNumber(9);

            for (var i = 0; i < playersOnMap.Count; i++)
            {
                players.Add(new Player(
                    playersKeys[i],
                    playersOnMap[i],
                    i + 1,
                    playersOnMap[i].X < 1280 / 2 ? 1 : -1
                ));
            }
        }

        public static void Update(GameScreen gameScreen, Timer timer)
        {
            var enemies = EnemyController.GetEnemies();
            var buffs = BuffController.GetBuffs();

            foreach (var player in players)
            {
                if (player.GetHp() <= 0)
                {
                    var playAgainScreen = new PlayAgainScreen();
                    playAgainScreen.Show();

                    gameScreen.Close();
                    timer.Stop();
                }

                if (player.physics.isMoving)
                {
                    player.Move((int)player.currentDirection * 15);
                }

                if (player.physics.isJumping)
                {
                    player.Jump();
                }

                for (var i = 0; i < enemies.Count; i++)
                {
                    var enemy = enemies[i];

                    if (player.physics.isCollided(enemy.physics.transform.position, enemy.physics.transform.size))
                    {
                        player.GetHit();
                    }
                }

                for (var i = 0; i < buffs.Count; i++)
                {
                    var buff = buffs[i];

                    if (buff.physics.isCollided(player.physics.transform.position, player.physics.transform.size))
                    {
                        if (buff.type == "health")
                        {
                            player.Heal();
                        }

                        if (buff.type == "strength")
                        {
                            GameController.playerDamage = Math.Floor(GameController.playerDamage * 1.1);
                        }

                        BuffController.RemoveBuff(buff);
                    }
                }
            }
        }

        public static List<Player> GetPlayers()
        {
            return players;
        }
    }
}
