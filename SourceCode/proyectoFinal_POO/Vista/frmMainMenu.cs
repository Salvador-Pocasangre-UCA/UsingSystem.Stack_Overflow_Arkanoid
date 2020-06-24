using System;
using System.Windows.Forms;

namespace proyectoFinal_POO
{
    public partial class FrmMainMenu : Form
    {
        //UserControl del juego
        private UcTopPlayers A;
        private UcPlayerName B;

        public FrmMainMenu()
        {
            InitializeComponent();

            // Maximizar ventana en su creacion
            Height = ClientSize.Height;
            Width = ClientSize.Width;
            WindowState = FormWindowState.Maximized;

            // Instanciacion de UserControl
            A = new UcTopPlayers();
            B = new UcPlayerName();
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

        private void FrmMainMenu_Load(object sender, EventArgs e)
        {
            //Preparacion de UserControl
            A.Dock = DockStyle.Fill;
            A.Width = Width;
            A.Height = Height;

            B.Dock = DockStyle.Fill;
            B.Width = Width;
            B.Height = Height;

            Controls.Add(B);
            B.Hide();

            Controls.Add(A);
            A.Hide();

            A.OnClickButtonA += OnclickToUserControlA;

        }

        private void OnclickToUserControlA(object sender, EventArgs e)
        {
            A.Hide();
            tableLayoutPanel1.Show();
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Hide();
            B.Show();

            B.StartAction = () =>
            {
                this.Hide();
            };

            B.FinishAction = () =>
            {
                B.Hide();
                tableLayoutPanel1.Show();
                this.Show(); 
            };
        }

        private void BtnScores_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Hide();
            A.UcTopPlayers_Load(sender, e);
            A.Show();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Seguro que desea salir?",
                "ArkaNoid", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
