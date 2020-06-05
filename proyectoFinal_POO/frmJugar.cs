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
    public partial class frmJugar : Form
    {
        public frmJugar()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            Height = Screen.PrimaryScreen.Bounds.Height;
            Width = Screen.PrimaryScreen.Bounds.Width;
        }

        private void frmJugar_FormClosing(object sender, FormClosingEventArgs e)
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

        private void frmJugar_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnJugar_Click(object sender, EventArgs e)
        {
            if (txbUsuario.Text.Length > 0)
            {
                Boolean esta = false;
                string nombre = txbUsuario.Text;
                List<Jugador> lista = JugadorDAO.getLista();

                foreach(Jugador j in lista)
                {
                    if (j.usuario.Equals(nombre, StringComparison.InvariantCultureIgnoreCase)){
                        esta = true;
                    }
                }

                if (esta)
                {
                    Console.WriteLine("Ya existe");
                }
                else
                {
                    JugadorDAO.crearNuevo(txbUsuario.Text);
                    MessageBox.Show("¡Bienvenido " + nombre + "!", "ArkaNoid", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
