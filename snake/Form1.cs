using System;
using System.Media;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace snake
{
    public partial class Form1 : Form
    {
        private int rI, rJ;
        private PictureBox fruit;
        private PictureBox[] snake = new PictureBox[400];
        private int dirX, dirY;
        private int w = 900;
        private int h = 800;
        private int size = 40;
        private int s = 0;
        public Form1()
        {
            InitializeComponent();
            this.Width = w;
            this.Height = h;
            this.BackgroundImage = pictureBox1.Image;
            dirX = 1;
            dirY = 0;
            fruit = new PictureBox ();
            fruit.Image = pictureBox2.Image;
            pictureBox2.Visible = false;
            pictureBox1.Visible = false;
            pictureBox3.Visible = false;
            fruit.Size = new Size(size, size);
            snake[0] = new PictureBox();
            snake[0].Image= pictureBox3.Image;
            snake[0].Location = new Point(201, 201);
            snake[0].Size = new Size(size - 1, size - 1);
            this.Controls.Add(snake[0]);
            generateFruit();
            timer.Tick += new EventHandler(game);
            timer.Interval = 200;
            timer.Start();
            this.KeyDown += new KeyEventHandler(dv);
        }
        private void checkBorders()
        {
            if (snake[0].Location.X < 0)
            {
                for (int _i = 1; _i <= s; _i++)
                {
                    this.Controls.Remove(snake[_i]);
                }
                s = 0;
                label1.Text = "Счёт: " + s;
                dirX = 1;
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                System.IO.Stream resourcestream = assembly.GetManifestResourceStream(@"snake.o.wav");
                SoundPlayer player = new SoundPlayer(resourcestream);
                player.Play();
                player.PlaySync();
            }
            if (snake[0].Location.X > h)
            {
                for (int _i = 1; _i <= s; _i++)
                {
                    this.Controls.Remove(snake[_i]);
                }
                s = 0;
                label1.Text = "Счёт: " + s;
                dirX = -1;
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                System.IO.Stream resourcestream = assembly.GetManifestResourceStream(@"snake.o.wav");
                SoundPlayer player = new SoundPlayer(resourcestream);
                player.Play();
                player.PlaySync();
            }
            if (snake[0].Location.Y < 0)
            {
                for (int _i = 1; _i <= s; _i++)
                {
                    this.Controls.Remove(snake[_i]);
                }
                s = 0;
                label1.Text = "Счёт: " + s;
                dirY = 1;
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                System.IO.Stream resourcestream = assembly.GetManifestResourceStream(@"snake.o.wav");
                SoundPlayer player = new SoundPlayer(resourcestream);
                player.Play();
                player.PlaySync();
            }
            if (snake[0].Location.Y > h)
            {
                for (int _i = 1; _i <= s; _i++)
                {
                    this.Controls.Remove(snake[_i]);
                }
                s = 0;
                label1.Text = "Счёт: " + s;
                dirY = -1;
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                System.IO.Stream resourcestream = assembly.GetManifestResourceStream(@"snake.o.wav");
                SoundPlayer player = new SoundPlayer(resourcestream);
                player.Play();
                player.PlaySync();
            }
        }
        private void Snake()
        {
            for (int i = s; i >= 1; i--)
            {
                snake[i].Location = snake[i - 1].Location;
            }
            snake[0].Location = new Point(snake[0].Location.X + dirX * (size), snake[0].Location.Y + dirY * (size));
            eatItself();
        }
        private void eatItself()
        {
            for (int _i = 1; _i < s; _i++)
            {
                if (snake[0].Location == snake[_i].Location)
                {
                    for (int _j = _i; _j <= s; _j++)
                        this.Controls.Remove(snake[_j]);
                    s = s - (s - _i + 1);
                    label1.Text = "Счёт: " + s;
                    System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                    System.IO.Stream resourcestream = assembly.GetManifestResourceStream(@"snake.o.wav");
                    SoundPlayer player = new SoundPlayer(resourcestream);
                    player.Play();
                    player.PlaySync();
                }
            }
        }
        private void eatFruit()
        {
            if (snake[0].Location.X == rI && snake[0].Location.Y == rJ)
            {
                label1.Text = "Счёт: " + ++s;
                snake[s] = new PictureBox();
                snake[s].Image = pictureBox3.Image;
                snake[s].Location = new Point(snake[s - 1].Location.X + 40 * dirX, snake[s - 1].Location.Y - 40 * dirY);
                snake[s].Size = new Size(size - 1, size - 1);
                this.Controls.Add(snake[s]);
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                System.IO.Stream resourcestream = assembly.GetManifestResourceStream(@"snake.nyam.wav");
                SoundPlayer player = new SoundPlayer(resourcestream);
                player.Play();
                player.PlaySync();
                generateFruit();
            }
        }

        private void generateFruit()
        {
            Random r = new Random();
            rI = r.Next(0, h - size);
            int tempI = rI % size;
            rI -= tempI;
            rJ = r.Next(0, h - size);
            int tempJ = rJ % size;
            rJ -= tempJ;
            rI++;
            rJ++;
            fruit.Location = new Point(rI, rJ);
            this.Controls.Add(fruit);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void game(Object myObject, EventArgs eventsArgs)
        {
            Snake();
            eatFruit();
            checkBorders();
        }
        private void dv(object sender, KeyEventArgs e)
        { 

            switch (e.KeyCode.ToString())
            {
                case "Right":
                    dirX = 1;
                    dirY = 0;
                    break;
                case "Left":
                    dirX = -1;
                    dirY = 0;
                    break;
                case "Up":
                    dirY = -1;
                    dirX = 0;
                    break;
                case "Down":
                    dirY = 1;
                    dirX = 0;
                    break;
            }
        }

    }
}

