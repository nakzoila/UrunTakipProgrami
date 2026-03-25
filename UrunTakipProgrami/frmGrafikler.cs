using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace UrunTakipProgrami
{
    public partial class frmGrafikler : Form
    {
        public frmGrafikler()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-QBP0E0A;Initial Catalog=dbUrun;Integrated Security=True");

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmYonlendirme yonlendir = new frmYonlendirme();
            yonlendir.Show();
            this.Close();
        }

        private void frmGrafikler_Load(object sender, EventArgs e)
        {
            baglanti.Open();

            SqlCommand komut = new SqlCommand("SELECT Ad, COUNT(*) FROM tblUrunler INNER JOIN tblKategori ON tblUrunler.kategori=tblKategori.ID GROUP BY Ad", baglanti);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                chart1.Series["Kategori"].Points.AddXY(dr[0], dr[1]);
            }
            baglanti.Close();

        }
    }
}
