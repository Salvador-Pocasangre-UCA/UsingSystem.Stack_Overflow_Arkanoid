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
        private CustomPB [,] cpb;
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

        private void frmJuego_Load(object sender, EventArgs e)
        {
            loasTiles();
        }

        private void loasTiles()
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
    }
}
