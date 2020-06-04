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
    public partial class frmMenuPrincipal : Form
    {
        public frmMenuPrincipal()
        {
            InitializeComponent();
        }

        private void btnPuntajes_Click(object sender, EventArgs e)
        {
            frmVerPuntuacion puntuacion = new frmVerPuntuacion();
            puntuacion.Show();
        }
    }
}
