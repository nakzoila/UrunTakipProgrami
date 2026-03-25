using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UrunTakipProgrami
{
    public partial class frmYonlendirme : Form
    {
        public frmYonlendirme()
        {
            InitializeComponent();
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        private void pnlKategori_Click(object sender, EventArgs e)
        {
            frmKategori kategori = new frmKategori();
            kategori.Show();
            this.Hide();
        }

        private void pnlUrunler_Click(object sender, EventArgs e)
        {
            UrunlerFormu urunler = new UrunlerFormu();
            urunler.Show();
            this.Hide();
        }

        private void pnlIstatislik_Click(object sender, EventArgs e)
        {
            istatislikFormu istatistlik = new istatislikFormu();
            istatistlik.Show();
            this.Hide();
        }

        private void pnlLogin_Click(object sender, EventArgs e)
        {
            frmAdmin admin = new frmAdmin();
            admin.Show();
            this.Close();
        }

        private void pnlGrafik_Click(object sender, EventArgs e)
        {
            frmGrafikler grafik = new frmGrafikler();
            grafik.Show();
            this.Close();
        }
    }
}
