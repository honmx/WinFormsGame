using System.Drawing;
using WinForms_game.Entities;

namespace WinForms_game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.startGameButton = new System.Windows.Forms.Button();
            this.title = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // startGameButton
            // 
            this.startGameButton.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.startGameButton.ForeColor = System.Drawing.SystemColors.GrayText;
            this.startGameButton.Location = new System.Drawing.Point(508, 316);
            this.startGameButton.Name = "startGameButton";
            this.startGameButton.Size = new System.Drawing.Size(291, 74);
            this.startGameButton.TabIndex = 0;
            this.startGameButton.Text = "Start Game";
            this.startGameButton.UseVisualStyleBackColor = true;
            this.startGameButton.Click += new System.EventHandler(this.StartGameButton_Click_1);
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.BackColor = System.Drawing.Color.Transparent;
            this.title.Font = new System.Drawing.Font("Segoe UI", 35F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.title.ForeColor = System.Drawing.SystemColors.GrayText;
            this.title.Location = new System.Drawing.Point(423, 201);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(464, 78);
            this.title.TabIndex = 1;
            this.title.Text = "WinForms Game";
            // 
            // Form1
            // 
            this.BackgroundImage = global::WinForms_game.Properties.Resources.Yellow;
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this.title);
            this.Controls.Add(this.startGameButton);
            this.Name = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void StartGameButton_Click_1(object sender, EventArgs e)
        {
            var gameScreen = new GameScreen();
            gameScreen.Show();

            this.Hide();
        }
    }
}