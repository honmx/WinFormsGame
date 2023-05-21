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
            GameController.Update(this, timer);
            PlayerController.Update(this, timer);
            EnemyController.Update(this, timer);

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
                    "HP: " + player.GetHp(),
                    new Font("Arial", 20),
                    new SolidBrush(Color.Black),
                    new PointF(10 + i * 100, 10)
                );

                graphics.DrawString(
                    "Damage: " + GameController.GetPlayerDamage(),
                    new Font("Arial", 20),
                    new SolidBrush(Color.Black),
                    new PointF(10 + i * 100, 50)
                );

                graphics.DrawString(
                    "Kills: " + GameController.GetKills(),
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
