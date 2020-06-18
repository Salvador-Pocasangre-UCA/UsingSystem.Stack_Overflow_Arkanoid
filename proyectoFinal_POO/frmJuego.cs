using System;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace proyectoFinal_POO
{
    public partial class frmJuego : Form
    {
        private CustomPB [,] cpb;
        private PictureBox ball;

        private Panel scores;
        private Label vidasRestantes, puntaje;

        private PictureBox corazon;

        private int increment;

        private delegate void AccionesPelota();
        private readonly AccionesPelota MovimientoPelota;

        

        public frmJuego()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            Height = Screen.PrimaryScreen.Bounds.Height;
            Width = Screen.PrimaryScreen.Bounds.Width;

            increment = 10;

            MovimientoPelota = rebotarPelota;
            MovimientoPelota += MoverPelota;


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
                        timer1.Start();
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
            PanelPuntajes();

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
                    cpb[i, j].Top = i * pbHeight + scores.Height + 1; 
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

            DatosJuego.ticksRealizados += 0.01;
            MovimientoPelota?.Invoke();
        }

        private void rebotarPelota()
        {
            if (ball.Top < 0)
                DatosJuego.dirX = -DatosJuego.dirY;

            if (ball.Bottom > Height)
            {
                DatosJuego.vidas--;
                DatosJuego.juegoIniciado = false;
                timer1.Stop();
                reposicionarElementos();
                actualizarElementos();
                if(DatosJuego.vidas == 0)
                {
                    timer1.Stop();
                    Application.Exit();
                }
                                
            }               
                

            if (ball.Left < 0 || ball.Right > Width)
            {
                DatosJuego.dirX = -DatosJuego.dirX;
                return;
            }

            if (ball.Bounds.IntersectsWith(pictureBox1.Bounds) || ball.Top < 0)
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
                        DatosJuego.puntaje += (int) (cpb[i, j].golpes * DatosJuego.ticksRealizados);
                        cpb[i, j].golpes--;

                        if(cpb[i, j].golpes == 0)
                        {
                            
                            Controls.Remove(cpb[i, j]);
                            cpb[i, j] = null;

                        }

                        DatosJuego.dirY = -DatosJuego.dirY;
                        puntaje.Text = DatosJuego.puntaje.ToString();
                        return;
                    }                                 
                }
            }
        }

        private void MoverPelota()
        {
            ball.Left += DatosJuego.dirX;
            ball.Top += DatosJuego.dirY;
        }

        private void PanelPuntajes()
        {
            scores = new Panel();
            scores.Width = Width;
            scores.Height = (int)(Height * 0.06);
            scores.Top = scores.Left = 0;
            scores.BackColor = Color.Black;

            vidasRestantes = new Label();
            puntaje = new Label();
            vidasRestantes.ForeColor = puntaje.ForeColor = Color.White;
            vidasRestantes.Text = "X" + DatosJuego.vidas.ToString();
            puntaje.Text = DatosJuego.puntaje.ToString();

            vidasRestantes.Font = puntaje.Font = new Font("Zorque", 22F);
            vidasRestantes.TextAlign = puntaje.TextAlign = ContentAlignment.MiddleCenter;

            
            puntaje.Left = Width - 100;

            vidasRestantes.Height = puntaje.Height = scores.Height;
            

            corazon = new PictureBox();
            corazon.Height = corazon.Width = scores.Height;
            corazon.Top = 0;
            corazon.Left = 20;
            corazon.BackgroundImage = Image.FromFile("../../../Sprites/Heart.png");
            corazon.BackgroundImageLayout = ImageLayout.Stretch;

            vidasRestantes.Left = corazon.Right + 5;

            scores.Controls.Add(corazon);
            scores.Controls.Add(vidasRestantes);
            scores.Controls.Add(puntaje);
            Controls.Add(scores);
        }

        private void reposicionarElementos()
        {
            pictureBox1.Left = (Width / 2) - (pictureBox1.Width / 2);
            ball.Top = pictureBox1.Top - ball.Height;
            ball.Left = pictureBox1.Left + (pictureBox1.Width / 2) - (ball.Width / 2);
        }

        private void actualizarElementos()
        {
            vidasRestantes.Text = "X" + DatosJuego.vidas.ToString();

        }
    }
}
