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
    public partial class frmVerPuntuacion : Form
    {
        public frmVerPuntuacion()
        {
            InitializeComponent();
        }

        private void frmVerPuntuacion_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            string lista = "";
            string listaScore = "";
            List<Score> miLista = ScoreDAO.getScoreList();
            foreach(var fila in miLista)
            {
                lista += fila.name;
                lista += "\n\n";
                listaScore += fila.score;
                listaScore += "\n\n";
            }
           
            label2.Text = lista;
            label3.Text = listaScore;
        }
    }
}
