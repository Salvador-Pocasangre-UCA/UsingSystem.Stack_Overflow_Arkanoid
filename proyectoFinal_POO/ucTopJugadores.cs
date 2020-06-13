using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace proyectoFinal_POO
{
    public partial class ucTopJugadores : UserControl
    {
        public delegate void EventUserControlA(object sender, EventArgs e);
        public EventUserControlA OnClickButtonA;
        public ucTopJugadores()
        {
            InitializeComponent();
        }

        private void ucTopJugadores_Load(object sender, EventArgs e)
        {
            string lista = "";
            string listaScore = "";
            List<Score> miLista = ScoreDAO.getScoreList();
            foreach (var fila in miLista)
            {
                lista += fila.name;
                lista += "\n\n";
                listaScore += fila.score;
                listaScore += "\n\n";
            }

            label2.Text = lista;
            label3.Text = listaScore;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            OnClickButtonA?.Invoke(this, e);
        }
    }
}
