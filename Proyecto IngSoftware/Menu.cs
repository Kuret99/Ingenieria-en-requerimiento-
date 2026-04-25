using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_IngSoftware
{
    public partial class Menu : Form
    {
        private GestionUs Gu;
        public Menu()
        {
            InitializeComponent();
        }

        private void gestionUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gu = new GestionUs();

            Gu.MdiParent = this;

            Gu.Show();
        }
    }
}
