using System;
using System.Drawing;
using System.Windows.Forms;

namespace proyectoFinal_POO
{
    public partial class frmJuego : Form
    {
        private CustomPB [,] cpb;
        private PictureBox ball;

        private int increment;

        public frmJuego()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            Height = Screen.PrimaryScreen.Bounds.Height;
            Width = Screen.PrimaryScreen.Bounds.Width;

            increment = 10;
        }

        private void frmJuego_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("¿Seguro que desea salir?",
                "ArkaNoid", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }
        }

        private void frmJuego_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void frmJuego_KeyPress(object sender, KeyEventArgs e)
        {
            if (!DatosJuego.juegoIniciado)
            {
                switch (e.KeyCode)
                {
                    case Keys.Left:
                        if(pictureBox1.Left > 0)
                        {
                            pictureBox1.Left -= increment;
                            ball.Left = pictureBox1.Left + (pictureBox1.Width / 2) - (ball.Width / 2);
                        }                        
                        break;
                        
                    case Keys.Right:
                        if(pictureBox1.Right < (Width - 20))
                        {
                            pictureBox1.Left += increment;
                            ball.Left = pictureBox1.Left + (pictureBox1.Width / 2) - (ball.Width / 2);
                        }                            
                        break;
                    case Keys.Space:
                        DatosJuego.juegoIniciado = true;
                        break;
                }
            }
            else
            {
                switch (e.KeyCode)
                {
                    case Keys.Left:
                        if (pictureBox1.Left > 0)
                        {
                            pictureBox1.Left -= increment;;
                        }
                        break;

                    case Keys.Right:
                        if (pictureBox1.Right < (Width - 20))
                        {
                            pictureBox1.Left += increment;               
                        }
                        break;
                }
            }
        }

        private void frmJuego_Load(object sender, EventArgs e)
        {
            // Insertando y configurando imagen de plataforma
            pictureBox1.BackgroundImage = Image.FromFile("../../../Sprites/Player.png");
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;

            pictureBox1.Top = Height - pictureBox1.Height - 80;
            pictureBox1.Left = (Width / 2) - (pictureBox1.Width / 2);

            // Insertando y configurando imagen de pelota
            ball = new PictureBox();
            ball.Width = ball.Height = 20;
            ball.BackgroundImage = Image.FromFile("../../../Sprites/Ball.png");
            ball.BackgroundImageLayout = ImageLayout.Stretch;

            ball.Top = pictureBox1.Top - ball.Height;
            ball.Left = pictureBox1.Left + (pictureBox1.Width / 2) - (ball.Width / 2);

            Controls.Add(ball);

            LoadTiles();
            timer1.Start();
        }

        private void LoadTiles()
        {
            int xAxis = 10, yAxis = 6;

            int pbHeight = (int)(Height * 0.3) / yAxis;
            int pbWidth = (int) (Width - (xAxis-5)) / xAxis;

            cpb = new CustomPB[yAxis, xAxis];


            for (int i = 0; i < yAxis ; i++)
            {
                for (int j = 0; j < xAxis; j++)
                {
                    cpb[i, j] = new CustomPB();

                    if (i == 0)
                        cpb[i, j].golpes = 2;
                    else
                        cpb[i, j].golpes = 1;

                    cpb[i, j].Height = pbHeight;
                    cpb[i, j].Width = pbWidth;
                    cpb[i, j].Left = j * pbWidth;
                    cpb[i, j].Top = i * pbHeight; 
                    if (i == 5)
                        cpb[i, j].BackgroundImage = Image.FromFile("../../../Sprites/Tile - green.png");
                    if (i == 4)
                        cpb[i, j].BackgroundImage = Image.FromFile("../../../Sprites/Tile - pink.png");
                    if (i == 3)
                        cpb[i, j].BackgroundImage = Image.FromFile("../../../Sprites/Tile - mint.png");
                    if (i == 2)
                        cpb[i, j].BackgroundImage = Image.FromFile("../../../Sprites/Tile - yellow.png");
                    if (i == 1)
                        cpb[i, j].BackgroundImage = Image.FromFile("../../../Sprites/Tile - red.png");
                    if (i == 0)
                        cpb[i, j].BackgroundImage = Image.FromFile("../../../Sprites/Tile - blinded.png");

                    cpb[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                    cpb[i, j].Tag = "titleTag";
                    Controls.Add(cpb[i, j]);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!DatosJuego.juegoIniciado)
                return;

            ball.Left += DatosJuego.dirX;
            ball.Top += DatosJuego.dirY;

            rebotarPelota();
        }

        private void rebotarPelota()
        {
            if (ball.Bottom > Height)
                Application.Exit();

            if (ball.Left < 0 || ball.Right > Width)
            {
                DatosJuego.dirX = -DatosJuego.dirX;
                return;
            }

            if (ball.Bounds.IntersectsWith(pictureBox1.Bounds))
            {
                DatosJuego.dirY = -DatosJuego.dirY;
                return;
            }

            for(int i = 5; i >= 0; i--)
            {
                for(int j = 0; j < 10; j++)
                {
                    if (cpb[i,j] != null && ball.Bounds.IntersectsWith(cpb[i, j].Bounds))
                    {
                        cpb[i, j].golpes--;

                        if(cpb[i, j].golpes == 0)
                        {
                            Controls.Remove(cpb[i, j]);
                            cpb[i, j] = null;
                        }

                        DatosJuego.dirY = -DatosJuego.dirY;
                        return;
                    }                                 
                }
            }
        }
    }
}
