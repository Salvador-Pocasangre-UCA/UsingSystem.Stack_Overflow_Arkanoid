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

        private void frmMenuPrincipal_Load(object sender, EventArgs e)
        {
            A.Dock = DockStyle.Fill;
            A.Width = Width;
            A.Height = Height;

            B.Dock = DockStyle.Fill;
            B.Width = Width;
            B.Height = Height;

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
            Controls.Add(B);
        }

        private void btnPuntajes_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Hide();
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
