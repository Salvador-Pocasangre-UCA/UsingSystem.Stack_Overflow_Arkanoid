using proyectoFinal_POO.Controlador;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace proyectoFinal_POO
{
    public partial class UcPlayerName : UserControl
    {
        public delegate void GameActions();
        public GameActions StartAction;
        public GameActions FinishAction;

        public UcPlayerName()
        {
            InitializeComponent();
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            GameData.InitializeGame();

            try
            {
                switch (txtUsername.Text)
                {
                    case string aux when aux.Length > 15:
                        throw new ExceededMaxCharacterException("El nombre de usuario no puede ser mayor " +
                                                                   "de 15 caracteres");
                    case string aux when aux.Trim().Length == 0:
                        throw new EmptyNicknameException("¡Debe ingresar un usuario para jugar!");

                    default:
                        Boolean exist = false;
                        string name = txtUsername.Text;
                        List<Player> list = PlayerController.GetList();

                        foreach (Player j in list)
                        {
                            if (j.Username.Equals(name))
                            {
                                exist = true;
                            }
                        }

                        if (exist)
                        {
                            MessageBox.Show($"Bienvenido nuevamente {name}", "ArkaNoid", MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);

                            FrmGame Game = new FrmGame(name);
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
                            MessageBox.Show($"Gracias por registrate {name}", "ArkaNoid", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            PlayerController.CreatePlayer(txtUsername.Text);
                            FrmGame Game = new FrmGame(name);
                            Game.Show();
                            StartAction?.Invoke();

                            Game.FinishGame = () =>
                            {
                                FinishAction?.Invoke();
                                Game.Dispose();
                            };
                        }
                        txtUsername.Clear();
                        break;
                }
            }
            catch(EmptyNicknameException ex)
            {
                MessageBox.Show(ex.Message,"ArkaNoid",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            catch (ExceededMaxCharacterException ex)
            {
                MessageBox.Show(ex.Message,"ArkaNoid",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }            
        }
    }
}
