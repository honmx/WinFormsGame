﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms_game
{
    public partial class PlayAgainScreen : Form
    {
        public PlayAgainScreen()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();

            var gameScreen = new GameScreen();
            gameScreen.Show();
        }
    }
}
