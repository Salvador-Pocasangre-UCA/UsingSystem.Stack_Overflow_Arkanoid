using proyectoFinal_POO.Controlador;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace proyectoFinal_POO
{
    public partial class FrmGame : Form
    {
        private CustomPB [,] cpb;
        private PictureBox ball;

        private Panel scoresPanel;
        private Label remainingLifes, score;

        private PictureBox heart;

        private int increment;
        private int remainingPB = 0;

        private delegate void BallActions();
        private readonly BallActions BallMovement;
        public Action FinishGame;

        private string Pplayer;


        public FrmGame(string player)
        {
            InitializeComponent();
            Pplayer = player;
            WindowState = FormWindowState.Maximized;
            Height = Screen.PrimaryScreen.Bounds.Height;
            Width = Screen.PrimaryScreen.Bounds.Width;

            increment = 10;

            BallMovement = BounceBall;
            BallMovement += MoveBall;
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

        private void FrmGame_Load(object sender, EventArgs e)
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

        private void FrmGame_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("¿Seguro que desea salir?",
                "ArkaNoid", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
                e.Cancel = false;
            }

        private void FrmGame_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void FrmGame_KeyPress(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys aux when aux == Keys.Left && !GameData.gameStarted:
                        if (pbPlayer.Left > 0)
                        {
                            pbPlayer.Left -= increment;
                            ball.Left = pbPlayer.Left + (pbPlayer.Width / 2) - (ball.Width / 2);
                        }
                        break;

                    case Keys aux when aux == Keys.Left && GameData.gameStarted:
                        if (pbPlayer.Left > 0)
                            pbPlayer.Left -= increment;                    
                        break;
                                            
                    case Keys aux when aux == Keys.Right && !GameData.gameStarted:
                        if (pbPlayer.Right < (Width - 20))
                        {
                            pbPlayer.Left += increment;
                            ball.Left = pbPlayer.Left + (pbPlayer.Width / 2) - (ball.Width / 2);
                        }
                        break;

                    case Keys aux when aux == Keys.Right && GameData.gameStarted:
                        if (pbPlayer.Right < (Width - 20))
                            pbPlayer.Left += increment;                        
                        break;

                    case Keys aux when aux == Keys.Space && !GameData.gameStarted:
                        GameData.gameStarted = true;
                        timer1.Start();
                        break;

                    default:
                        if(!GameData.gameStarted)
                            throw new WrongKeyPressedException("Presione Space para iniciar el juego");
                        break;
                }
            }
            catch (WrongKeyPressedException ex)
            {
                MessageBox.Show(ex.Message,"ArkaNoid",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void LoadTiles()
        {
            //Variables auxiliares para el calculo de tamaño de cada cpb
            int xAxis = 10, yAxis = 6;

            remainingPB = xAxis * yAxis;

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
                        cpb[i, j].Hits = 2;
                    else
                        cpb[i, j].Hits = 1;

                    //Seteando el tamaño
                    cpb[i, j].Height = pbHeight;
                    cpb[i, j].Width = pbWidth;

                    //Posicion de left y top
                    cpb[i, j].Left = j * pbWidth;
                    cpb[i, j].Top = i * pbHeight + scoresPanel.Height + 1;

                    //Agregando imagen de bloque segun fila
                    switch (i)
                    {
                        case 5:
                            cpb[i, j].BackgroundImage = Image.FromFile("../../../Sprites/Tile - green.png");
                            cpb[i, j].Tag = "titleTag";
                            break;
                        case 4:
                            cpb[i, j].BackgroundImage = Image.FromFile("../../../Sprites/Tile - pink.png");
                            cpb[i, j].Tag = "titleTag";
                            break;
                        case 3:
                            cpb[i, j].BackgroundImage = Image.FromFile("../../../Sprites/Tile - mint.png");
                            cpb[i, j].Tag = "titleTag";
                            break;
                        case 2:
                            cpb[i, j].BackgroundImage = Image.FromFile("../../../Sprites/Tile - yellow.png");
                            cpb[i, j].Tag = "titleTag";
                            break;
                        case 1:
                            cpb[i, j].BackgroundImage = Image.FromFile("../../../Sprites/Tile - red.png");
                            cpb[i, j].Tag = "titleTag";
                            break;
                        case 0:
                            cpb[i, j].BackgroundImage = Image.FromFile("../../../Sprites/Tile - blinded.png");
                            cpb[i, j].Tag = "blinded";
                            break;
                    }

                    cpb[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                    Controls.Add(cpb[i, j]);
                }
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (!GameData.gameStarted)
                return;

            GameData.ticksCount += 0.01;

            try
            {
                BallMovement?.Invoke();
            }
            catch(OutOfBoundsException ex)
            {
                try
                {
                    GameData.lives--;
                    GameData.gameStarted = false;
                    timer1.Stop();

                    RepositionElements();
                    UpdateElements();

                    if (GameData.lives == 0)
                        throw new NoRemainingLivesException("Has perdido!");
                }
                catch(NoRemainingLivesException ex2)
                {
                    timer1.Stop();
                    MessageBox.Show(ex2.Message,"ArkaNoid",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    this.Hide();
                    FinishGame?.Invoke();
                }               
            }  
        }
        //Rutina de rebote de pelota
        private void BounceBall()
        {
            //Rebote de pelota en la parte superior 
            if (ball.Top < scoresPanel.Height)
            {
                GameData.dirY = -GameData.dirY;
                return;
            }

            //Pelota se sale de los bounds
            if (ball.Bottom > Height)
                throw new OutOfBoundsException("");              
                
            //Rebote de pelota en lado derecho o izquierdo de la ventana
            if (ball.Left < 0 || ball.Right > Width)
            {
                GameData.dirX = -GameData.dirX;
                return;
            }

            //Rebote con el jugador
            if (ball.Bounds.IntersectsWith(pbPlayer.Bounds))
            {
                GameData.dirY = -GameData.dirY;
                return;
            }

            //Rutina de colisiones con cpb
            for(int i = 5; i >= 0; i--)
            {
                for(int j = 0; j < 10; j++)
                {
                    if (cpb[i,j] != null && ball.Bounds.IntersectsWith(cpb[i, j].Bounds))
                    {
                        GameData.score += (int) (cpb[i, j].Hits * GameData.ticksCount);
                        cpb[i, j].Hits--;

                        if(cpb[i, j].Hits == 0)
                        {
                            
                            Controls.Remove(cpb[i, j]);
                            cpb[i, j] = null;

                            remainingPB--;
                        }
                        else if(cpb[i, j].Tag.Equals("blinded"))
                            cpb[i, j].BackgroundImage = Image.FromFile("../../../Sprites/broken.png");

                        GameData.dirY = -GameData.dirY;

                        score.Text = GameData.score.ToString();

                        if(remainingPB == 0)
                        {
                            timer1.Stop();
                            ScoreController.CreateScore(Pplayer, GameData.score);
                            MessageBox.Show("Has ganado \n Tu puntaje final fue: " + GameData.score, "ArkaNoid", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Hide();
                            FinishGame?.Invoke();
                        }
                        return;
                    }                                 
                }
            }
        }

        private void MoveBall()
        {
            ball.Left += GameData.dirX;
            ball.Top += GameData.dirY;
        }

        //Inicializar todos los elementos del panel de puntajes
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

            remainingLifes.Text = "X" + GameData.lives.ToString();
            score.Text = GameData.score.ToString();

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
            remainingLifes.Text = "X" + GameData.lives.ToString();

        }
    }
}
