using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class Form1 : Form
    {
        const int WIDHT = 617;
        const int HEIGHT = 480;
        public MainMenu menuRef;
        public int Level;
        public bool GoToMenu = false;
        public bool FirstInit = true;
        private int Score = 0;
        GameButton[,] gameButtons = new GameButton[HEIGHT / 40 - 1, WIDHT / 40];
        public Form1()
        {
            InitializeComponent();
            this.Width = WIDHT;
            this.Height = HEIGHT;
        }
        public void InitGame()
        {
            if (!FirstInit) MessageBox.Show("Score:" + Score);
            else FirstInit = false;
            Score = 0;
            Random rnd = new Random();
            this.Controls.Clear();
            int fixLevel = Level == 0?3: Level == 1?4:6;
            for(int y = 0; y< HEIGHT / 40 - 1; y++)
               for (int x = 0; x < WIDHT / 40; x++)
               {
                    int tmp = 0;
                    int rand = rnd.Next(0, 1000);
                    if (rand > 950 - Level * 50 && rand < 990) tmp = 0;
                    else tmp = rnd.Next(1, fixLevel);
                    rand -= 1;
                    gameButtons[y,x] = new GameButton(x * 40, y * 40, (Value)tmp, new Point(x,y));
                    this.Controls.Add(gameButtons[y, x].GetButton);
                    gameButtons[y, x].Lost += InitGame;
                    gameButtons[y, x].Open += OpenButton;
                    gameButtons[y, x].Score += PlayerScore;
                    gameButtons[y, x].GetButton.KeyDown += Form1_KeyDown;
               }
        }

        private void OpenButton(Point index, Value value)
        {
            int i = 1;
            if (index.X + i < gameButtons.GetLength(1) && value == gameButtons[index.Y, index.X + i].value && gameButtons[index.Y, index.X + i].state != State.Open)
            {
                gameButtons[index.Y, index.X + i].OpenButton();
            }
            if (index.X - i >= 0 && value == gameButtons[index.Y, index.X - i].value && gameButtons[index.Y, index.X - i].state != State.Open)
            {
                gameButtons[index.Y, index.X - i].OpenButton();
            }

            if (index.Y + i < gameButtons.GetLength(0) && value == gameButtons[index.Y + i, index.X].value && gameButtons[index.Y + i, index.X].state != State.Open)
            {
                gameButtons[index.Y + i, index.X].OpenButton();
            }
            if (index.Y - i >= 0 && value == gameButtons[index.Y - i, index.X].value && gameButtons[index.Y - i, index.X].state != State.Open)
            {
                gameButtons[index.Y - i, index.X].OpenButton();
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(!GoToMenu) menuRef.Close();
        }
        private void PlayerScore()
        {
            Score++;
            if (Score >= 165)
            {
                MessageBox.Show("You WIN !!");
                menuRef.Show();
                GoToMenu = true;
                this.Close();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                DialogResult dr = MessageBox.Show("Back to Main Menu are you sure?", "Exit", MessageBoxButtons.OKCancel);
                if(dr == DialogResult.OK)
                {
                    menuRef.Show();
                    GoToMenu = true;
                    this.Close();
                }
            }
        }
    }
}
