using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace proyectoFinal_POO
{
    public partial class UcTopPlayers : UserControl
    {
        public delegate void EventUserControlA(object sender, EventArgs e);
        public EventUserControlA OnClickButtonA;
        public UcTopPlayers()
        {
            InitializeComponent();
        }

        public void UcTopPlayers_Load(object sender, EventArgs e)
        {
            string lista = "";
            string listaScore = "";
            List<Score> miLista = ScoreController.GetScoreList();
            foreach (var fila in miLista)
            {
                lista += fila.Name;
                lista += "\n\n";
                listaScore += fila.Scores;
                listaScore += "\n\n";
            }

            lblUsername.Text = lista;
            lblScore.Text = listaScore;
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            OnClickButtonA?.Invoke(this, e);
        }
    }
}
