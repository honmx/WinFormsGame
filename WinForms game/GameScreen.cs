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
            timer = new Timer();
            timer.Interval = 20;
            timer.Tick += new EventHandler(Update);

            this.Paint += new PaintEventHandler(OnRepaint);

            this.KeyDown += new KeyEventHandler(OnKeyDown);
            this.KeyUp += new KeyEventHandler(OnKeyUp);

            Init();
        }

        public void Init()
        {
            GameController.InitMap();
            WallsController.InitWalls();
            BoxController.InitBoxes();
            PlayerController.InitPlayers();
            
            timer.Start();
        }

        private void Update(object sender, EventArgs e)
        {
            foreach (var player in PlayerController.GetPlayers())
            {
                if (player.isMoving) player.Move((int)player.currentDirection * 15);
            }

            Invalidate();
        }

        private void OnRepaint(object sender, PaintEventArgs e)
        {
            var graphics = e.Graphics;
            var walls = WallsController.GetWalls();
            var boxes = BoxController.GetBoxes();
            var players = PlayerController.GetPlayers();

            DrawEntities(walls, graphics);
            DrawEntities(boxes, graphics);
            DrawEntities(players, graphics);
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            foreach (var player in PlayerController.GetPlayers())
            {
                if (e.KeyCode == player.keyRight)
                {
                    player.currentDirection = Player.Direction.Right;
                    player.isMoving = true;
                    player.flip = 1;
                }
                if (e.KeyCode == player.keyLeft)
                {
                    player.currentDirection = Player.Direction.Left;
                    player.isMoving = true;
                    player.flip = -1;
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
                var wall = entities[i];
                wall.DrawSprite(graphics);
            }
        }
    }
}
