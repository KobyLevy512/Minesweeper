using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace Minesweeper
{
    public enum State
    {
        Null,
        Open,
        Mine
    }
    public enum Value
    {
        Mine = 0,
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4
    }
    public class GameButton
    {
        public delegate void PlayerLost();
        public delegate void PlayerScore();
        public delegate void PlayerOpen(Point Index, Value state);
        public event PlayerLost Lost;
        public event PlayerOpen Open;
        public event PlayerScore Score;
        Button button = new Button();
        public State state = State.Null;
        public Value value;

        public SoundPlayer sound;
        public Point Index;
        public Button GetButton
        {
            get => button;
        }

        public GameButton(int x, int y, Value val, Point index)
        {
            this.Index = index;
            button.Location = new Point(x, y);
            button.Width = 40;
            button.Height = 40;
            button.BackColor = Color.FromArgb(200, 200, 200);
            button.ForeColor = Color.White;
            button.Font = new Font(FontFamily.GenericSansSerif, 16);
            button.BackgroundImageLayout = ImageLayout.Stretch;
            button.MouseDown += button_Click;
            Random rnd = new Random();
            value = val;
        }
        private void button_Click(object sender, MouseEventArgs e)
        {
            if(state != State.Open)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        if (value == Value.Mine) OpenMineButton();
                        else OpenButton();
                        break;
                    case MouseButtons.Right:
                        if(state == State.Mine)
                        {
                            state = State.Null;
                            button.BackgroundImage = null;
                        }
                        else
                        {
                            if(value == Value.Mine)Score?.Invoke();
                            state = State.Mine;
                            button.BackgroundImage = Properties.Resources.Flag;
                        }
                        break;
                }
            }
        }

        private void OpenMineButton()
        {
            state = State.Open;
            button.BackColor = Color.FromArgb(200, 0, 0);
            button.BackgroundImage = Properties.Resources.Bomb;
            sound = new SoundPlayer(Properties.Resources.RobotSound);
            sound.Play();
            MessageBox.Show("You Lost");
            Lost?.Invoke();
        }
        public void OpenButton()
        {
            state = State.Open;
            if (sound != null)sound.Stop();
            sound = new SoundPlayer(Properties.Resources.WinFx);
            sound.Play();
            button.BackColor = Color.FromArgb(50, 205, 75);
            button.BackgroundImage = null;
            button.Text = ((int)value).ToString();
            Open?.Invoke(Index, value);
            Score?.Invoke();

        }
    }
}
