using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proyectoFinal_POO
{
    public partial class frmJuego : Form
    {
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
            switch (e.KeyCode)
            {
                case Keys.Left:
                    pictureBox1.Left -= increment;
                    break;
                case Keys.Right:
                    pictureBox1.Left += increment;
                    break;
            }
        }
    }
}
