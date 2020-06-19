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

        private Panel scoresPanel;
        private Label remainingLifes, score;

        private PictureBox heart;

        private int increment;
        private int remainingPB = 0;

        private delegate void AccionesPelota();
        private readonly AccionesPelota MovimientoPelota;
        public Action FinishGame;

        private string Pplayer;


        public frmJuego(string player)
        {
            InitializeComponent();
            Pplayer = player;
            WindowState = FormWindowState.Maximized;
            Height = Screen.PrimaryScreen.Bounds.Height;
            Width = Screen.PrimaryScreen.Bounds.Width;

            increment = 10;

            MovimientoPelota = BounceBall;
            MovimientoPelota += MoveBall;
        }

        //Metodo que arregla flickering issue
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED       
                return handleParam;
            }
        }

        private void frmJuego_Load(object sender, EventArgs e)
        {

            BackgroundImage = Image.FromFile("../../../Recursos_proyectoPOO/fondo.jpg");
            BackgroundImageLayout = ImageLayout.Stretch;
            ScoresPanel();

            // Insertando y configurando imagen de plataforma de  jugador
            pbPlayer.BackgroundImage = Image.FromFile("../../../Recursos_proyectoPOO/Player.png");
            pbPlayer.BackgroundImageLayout = ImageLayout.Stretch;

            pbPlayer.Top = Height - pbPlayer.Height - 80;
            pbPlayer.Left = (Width / 2) - (pbPlayer.Width / 2);

            // Insertando y configurando imagen de pelota
            ball = new PictureBox();
            ball.Width = ball.Height = 20;
            ball.BackgroundImage = Image.FromFile("../../../Sprites/Ball.png");
            ball.BackgroundImageLayout = ImageLayout.Stretch;

            ball.Top = pbPlayer.Top - ball.Height;
            ball.Left = pbPlayer.Left + (pbPlayer.Width / 2) - (ball.Width / 2);

            Controls.Add(ball);

            LoadTiles();
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
                        if(pbPlayer.Left > 0)
                        {
                            pbPlayer.Left -= increment;
                            ball.Left = pbPlayer.Left + (pbPlayer.Width / 2) - (ball.Width / 2);
                        }                        
                        break;
                        
                    case Keys.Right:
                        if(pbPlayer.Right < (Width - 20))
                        {
                            pbPlayer.Left += increment;
                            ball.Left = pbPlayer.Left + (pbPlayer.Width / 2) - (ball.Width / 2);
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
                        if (pbPlayer.Left > 0)
                        {
                            pbPlayer.Left -= increment;;
                        }
                        break;

                    case Keys.Right:
                        if (pbPlayer.Right < (Width - 20))
                        {
                            pbPlayer.Left += increment;               
                        }
                        break;
                }
            }
        }

        private void LoadTiles()
        {
            //Variables auxiliares para el calculo de tamaño de cada cpb
            int xAxis = 10, yAxis = 6;

            remainingPB = xAxis *yAxis;


            int pbHeight = (int)(Height * 0.3) / yAxis;
            int pbWidth = (int) (Width - (xAxis-5)) / xAxis;

            cpb = new CustomPB[yAxis, xAxis];

            //Rutina de instanciacion y agregacion al form
            for (int i = 0; i < yAxis ; i++)
            {
                for (int j = 0; j < xAxis; j++)
                {
                    cpb[i, j] = new CustomPB();

                    if (i == 0)
                        cpb[i, j].golpes = 2;
                    else
                        cpb[i, j].golpes = 1;

                    //Seteando el tamaño
                    cpb[i, j].Height = pbHeight;
                    cpb[i, j].Width = pbWidth;

                    //Posicion de left y top
                    cpb[i, j].Left = j * pbWidth;
                    cpb[i, j].Top = i * pbHeight + scoresPanel.Height + 1;

                    //Agregando imagen de bloque segun fila
                    if (i == 5)
                    {
                        cpb[i, j].BackgroundImage = Image.FromFile("../../../Sprites/Tile - green.png");
                        cpb[i, j].Tag = "titleTag";
                    }
                    if (i == 4) 
                    { 
                        cpb[i, j].BackgroundImage = Image.FromFile("../../../Sprites/Tile - pink.png");
                        cpb[i, j].Tag = "titleTag";
                    }
                    if (i == 3)
                    {
                        cpb[i, j].BackgroundImage = Image.FromFile("../../../Sprites/Tile - mint.png");
                        cpb[i, j].Tag = "titleTag";
                    }
                    if (i == 2)
                    {
                        cpb[i, j].BackgroundImage = Image.FromFile("../../../Sprites/Tile - yellow.png");
                        cpb[i, j].Tag = "titleTag";
                    }
                    if (i == 1)
                    {
                        cpb[i, j].BackgroundImage = Image.FromFile("../../../Sprites/Tile - red.png");
                        cpb[i, j].Tag = "titleTag";
                    }
                    if (i == 0)
                    {
                        cpb[i, j].BackgroundImage = Image.FromFile("../../../Sprites/Tile - blinded.png");
                        cpb[i, j].Tag = "blinded";
                    }

                    cpb[i, j].BackgroundImageLayout = ImageLayout.Stretch;
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

        private void BounceBall()
        {
            //Rebote de pelota en la parte superior 
            if (ball.Top < scoresPanel.Height)
            {
                DatosJuego.dirY = -DatosJuego.dirY;
                return;
            }

            //Pelota se sale de los bounds
            if (ball.Bottom > Height)
            {
                DatosJuego.vidas--;
                DatosJuego.juegoIniciado = false;
                timer1.Stop();

                RepositionElements();
                UpdateElements();

                if(DatosJuego.vidas == 0)
                {
                    timer1.Stop();
                    MessageBox.Show("Has perdido!", "ArkaNoid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Hide();
                    FinishGame?.Invoke();

                }
                                
            }               
                
            //Rebote de pelota en lado derecho o izquierdo de la ventana
            if (ball.Left < 0 || ball.Right > Width)
            {
                DatosJuego.dirX = -DatosJuego.dirX;
                return;
            }

            if (ball.Bounds.IntersectsWith(pbPlayer.Bounds))
            {
                DatosJuego.dirY = -DatosJuego.dirY;
                return;
            }

            //Rutina de colisiones con cpb
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

                            remainingPB--;
                        }
                        else if(cpb[i, j].Tag.Equals("blinded"))
                            cpb[i, j].BackgroundImage = Image.FromFile("../../../Sprites/broken.png");

                        DatosJuego.dirY = -DatosJuego.dirY;

                        score.Text = DatosJuego.puntaje.ToString();

                        if(remainingPB == 0)
                        {
                            timer1.Stop();
                            ScoreDAO.createScore(Pplayer, DatosJuego.puntaje);
                            MessageBox.Show("Has ganado \n Tu puntaje final fue: " + DatosJuego.puntaje);
                            this.Hide();
                            FinishGame.Invoke();
                        }

                        return;
                    }                                 
                }
            }
        }

        private void MoveBall()
        {
            ball.Left += DatosJuego.dirX;
            ball.Top += DatosJuego.dirY;
        }

        private void ScoresPanel()
        {
            //Instanciar panel de puntajes
            scoresPanel = new Panel();

            //Seteando elementos del panel puntajes
            scoresPanel.Width = Width;
            scoresPanel.Height = (int)(Height * 0.06);

            scoresPanel.Top = scoresPanel.Left = 0;

            scoresPanel.BackColor = Color.Black;

            //Instanciar label de vidas restantes
            remainingLifes = new Label();
            score = new Label();

            //Seteando elementos de label vidas restantes
            remainingLifes.ForeColor = score.ForeColor = Color.White;

            remainingLifes.Text = "X" + DatosJuego.vidas.ToString();
            score.Text = DatosJuego.puntaje.ToString();

            remainingLifes.Font = score.Font = new Font("Zorque", 25F);
            remainingLifes.TextAlign = score.TextAlign = ContentAlignment.MiddleCenter;

            
            score.Left = Width - 100;

            remainingLifes.Height = score.Height = scoresPanel.Height;
            
            //Instanciar picture box de vidas
            heart = new PictureBox();

            heart.Height = heart.Width = scoresPanel.Height;

            heart.Top = 0;
            heart.Left = 20;

            heart.BackgroundImage = Image.FromFile("../../../Sprites/Heart.png");
            heart.BackgroundImageLayout = ImageLayout.Stretch;

            remainingLifes.Left = heart.Right-10;

            scoresPanel.Controls.Add(heart);
            scoresPanel.Controls.Add(remainingLifes);
            scoresPanel.Controls.Add(score);
            Controls.Add(scoresPanel);
        }

        //Reposicionamiento de elementos luego de perder una vida
        private void RepositionElements()
        {
            pbPlayer.Left = (Width / 2) - (pbPlayer.Width / 2);
            ball.Top = pbPlayer.Top - ball.Height;
            ball.Left = pbPlayer.Left + (pbPlayer.Width / 2) - (ball.Width / 2);
        }

        //Actualizacion de elementos luego de perder una vida
        private void UpdateElements()
        {
            remainingLifes.Text = "X" + DatosJuego.vidas.ToString();

        }
    }
}
