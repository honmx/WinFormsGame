using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinForms_game.Controllers;
using WinForms_game.Entities;
using WinForms_game.Helpers;
using WinForms_game.Interfaces;
using WinForms_game.Properties;
using Timer = System.Windows.Forms.Timer;

namespace WinForms_game
{
    public partial class GameScreen : Form
    {
        Timer timer;

        public GameScreen()
        {
            InitializeComponent();
            this.timer = new Timer();
            this.timer.Interval = 20;
            this.timer.Tick += new EventHandler(Update);

            this.BackgroundImage = Resources.Yellow;

            this.Paint += new PaintEventHandler(OnRepaint);

            this.KeyDown += new KeyEventHandler(OnKeyDown);
            this.KeyUp += new KeyEventHandler(OnKeyUp);

            Init();
        }

        public void Init()
        {
            GameController.InitGame();
            WallsController.InitWalls();
            PlayerController.InitPlayers();
            EnemyController.InitEnemies();
            BulletController.InitBullets();
            BuffController.InitBuffs();
            
            this.timer.Start();
        }

        private void Update(object sender, EventArgs e)
        {
            var enemies = EnemyController.GetEnemies();
            var buffs = BuffController.GetBuffs();

            foreach (var player in PlayerController.GetPlayers())
            {
                if (player.hp <= 0)
                {
                    var playAgainScreen = new PlayAgainScreen();
                    playAgainScreen.Show();

                    this.Close();
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

                for (var i = 0; i <buffs.Count; i++)
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

            if (enemies.Count < GameController.GetKills() / 20 + 2)
            {
                EnemyController.SpawnEnemy();
            }

            for (var i = 0; i < enemies.Count; i++)
            {
                var enemy = enemies[i];

                if (enemy.hp <= 0)
                {
                    GameController.AddKill();
                    EnemyController.KillEnemy(enemy);
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

            Invalidate();
        }

        private void OnRepaint(object sender, PaintEventArgs e)
        {
            var graphics = e.Graphics;

            var walls = WallsController.GetWalls();
            var players = PlayerController.GetPlayers();
            var enemies = EnemyController.GetEnemies();
            var bullets = BulletController.GetBullets();
            var buffs = BuffController.GetBuffs();

            DrawEntities(walls, graphics);
            DrawEntities(enemies, graphics);
            DrawEntities(players, graphics);
            DrawEntities(bullets, graphics);
            DrawEntities(buffs, graphics);

            for (var i = 0; i < players.Count; i++)
            {
                var player = players[i];

                graphics.DrawString(
                    "HP: " + player.hp,
                    new Font("Arial", 20),
                    new SolidBrush(Color.Black),
                    new PointF(10 + i * 100, 10)
                );

                graphics.DrawString(
                    "Damage: " + GameController.playerDamage,
                    new Font("Arial", 20),
                    new SolidBrush(Color.Black),
                    new PointF(10 + i * 100, 50)
                );

                graphics.DrawString(
                    "Kills: " + GameController.kills,
                    new Font("Arial", 20),
                    new SolidBrush(Color.Black),
                    new PointF(10 + i * 100, 90)
                );
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            foreach (var player in PlayerController.GetPlayers())
            {
                if (e.KeyCode == player.keyRight)
                {
                    player.physics.isMoving = true;
                    player.currentDirection = Player.Direction.Right;
                }
                if (e.KeyCode == player.keyLeft)
                {
                    player.physics.isMoving = true;
                    player.currentDirection = Player.Direction.Left;
                }
                if (e.KeyCode == player.keyUp)
                {
                    player.physics.isJumping = true;
                }
                if (e.KeyCode == player.keyShoot)
                {
                    BulletController.AddBullet(player.gunTransform.position, (int)player.currentDirection);
                }
            }
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            foreach (var player in PlayerController.GetPlayers())
            {
                if (
                    e.KeyCode == player.keyRight ||
                    e.KeyCode == player.keyLeft
                )
                {
                    player.Stop();
                }
            }
        }

        private static void DrawEntities<T>(List<T> entities, Graphics graphics) where T: IEntity
        {
            for (var i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];
                entity.DrawSprite(graphics);
            }
        }
    }
}
