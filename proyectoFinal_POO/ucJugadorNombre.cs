using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace proyectoFinal_POO
{
    public partial class ucJugadorNombre : UserControl
    {
        public delegate void GameActions();
        public GameActions StartAction;
        public GameActions FinishAction;

        public ucJugadorNombre()
        {
            InitializeComponent();
        }

        private void btnJugar_Click(object sender, EventArgs e)
        {
            DatosJuego.InicializaJuego();

            if (txbUsuario.Text.Length > 0)
            {
                Boolean esta = false;
                string nombre = txbUsuario.Text;
                List<Jugador> lista = JugadorDAO.getLista();

                foreach (Jugador j in lista)
                {
                    if (j.usuario.Equals(nombre))
                    {
                        esta = true;
                    }
                }

                if (esta)
                {
                    MessageBox.Show($"Bienvenido nuevamente {nombre}", "ArkaNoid", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmJuego Game = new frmJuego(nombre);
                    Game.Show();
                    StartAction?.Invoke();

                    Game.FinishGame = () =>
                    {
                        FinishAction?.Invoke();
                        Game.Dispose();
                    };
                }
                else
                {
                    MessageBox.Show($"Gracias por registrate {nombre}", "ArkaNoid", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    JugadorDAO.crearNuevo(txbUsuario.Text);
                    frmJuego Game = new frmJuego(nombre);
                    Game.Show();
                    StartAction?.Invoke();

                    Game.FinishGame = () =>
                    {
                        FinishAction?.Invoke();
                        Game.Dispose();
                    };
                }
                txbUsuario.Clear();
            }
            else
            {
                MessageBox.Show("¡Debe ingresar un usuario para jugar!", "ArkaNoid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
