using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace proyectoFinal_POO
{
    public partial class ucJugadorNombre : UserControl
    {

        public ucJugadorNombre()
        {
            InitializeComponent();
        }

        private void btnJugar_Click(object sender, EventArgs e)
        {
            if (txbUsuario.Text.Length > 0)
            {
                Boolean esta = false;
                string nombre = txbUsuario.Text;
                List<Jugador> lista = JugadorDAO.getLista();

                foreach (Jugador j in lista)
                {
                    if (j.usuario.Equals(nombre, StringComparison.InvariantCultureIgnoreCase))
                    {
                        esta = true;
                    }
                }

                if (esta)
                {
                    frmJuego juego = new frmJuego();
                    juego.Show();
                    this.Hide(); 
                }
                else
                {
                    JugadorDAO.crearNuevo(txbUsuario.Text);
                    frmJuego juego = new frmJuego();
                    juego.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("¡Debe ingresar un usuario para jugar!", "ArkaNoid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
