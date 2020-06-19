using System;
using System.Windows.Forms;

namespace proyectoFinal_POO
{
    public partial class frmMenuPrincipal : Form
    {
        private ucTopJugadores A;
        private ucJugadorNombre B;

        public frmMenuPrincipal()
        {
            InitializeComponent();
            Height = ClientSize.Height;
            Width = ClientSize.Width;
            WindowState = FormWindowState.Maximized;

            A = new ucTopJugadores();
            B = new ucJugadorNombre();
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

        private void frmMenuPrincipal_Load(object sender, EventArgs e)
        {
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

        private void btnJugar_Click(object sender, EventArgs e)
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

        private void btnPuntajes_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Hide();
            A.ucTopJugadores_Load(sender, e);
            A.Show();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Seguro que desea salir?",
                "ArkaNoid", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
