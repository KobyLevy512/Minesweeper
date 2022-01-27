using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }
        private void CreateGame(int level)
        {
            Form1 f = new Form1();
            f.menuRef = this;
            f.Level = level;
            f.Show();
            f.Activate();
            f.InitGame();
            this.Hide();
        }
        //Easy
        private void button1_Click(object sender, EventArgs e)
        {
            CreateGame(0);
        }
        //Medium
        private void button2_Click(object sender, EventArgs e)
        {
            CreateGame(1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CreateGame(2);
        }
    }
}
