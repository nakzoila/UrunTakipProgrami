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
    public partial class istatislikFormu : Form
    {
        public istatislikFormu()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-QBP0E0A;Initial Catalog=dbUrun;Integrated Security=True");

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmYonlendirme yonlendir = new frmYonlendirme();
            yonlendir.Show();
            this.Hide();
        }

        private void istatislikFormu_Load(object sender, EventArgs e)
        {

            // Toplam Kategori Sayısı : 
            baglanti.Open();

            SqlCommand komut1 = new SqlCommand("SELECT COUNT(*) FROM tblKategori", baglanti);
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                lblToplamKategori.Text = dr1[0].ToString();
            }

            baglanti.Close();



            // Toplam Ürün Sayısı : 
            baglanti.Open();

            SqlCommand komut2 = new SqlCommand(" SELECT COUNT(*) FROM tblUrunler", baglanti);
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                lblUrunSayisi.Text = dr2[0].ToString();
            }

            baglanti.Close();

            // Toplam Beyaz Eşya Sayısı : 
            baglanti.Open();

            SqlCommand komut3 = new SqlCommand("SELECT COUNT(*) FROM tblUrunler WHERE kategori=(SELECT ID FROM tblKategori WHERE Ad='Beyazeşya')", baglanti);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                lblToplamBeyazEsyaSayisi.Text = dr3[0].ToString();
            }

            baglanti.Close();


            // Toplam Küçük Ev Alterleri Sayısı : 

            baglanti.Open();

            SqlCommand komut4 = new SqlCommand(" SELECT COUNT(*) FROM tblUrunler WHERE kategori=(SELECT ID FROM tblKategori WHERE Ad='Küçük Ev Aletleri')", baglanti);
            SqlDataReader dr4 = komut4.ExecuteReader();
            while (dr4.Read())
            {
                lblKucukEvAlterleri.Text = dr4[0].ToString();
            }

            baglanti.Close();


            // En Yüksek Stoklu Ürün Sayısı : 

            baglanti.Open();

            SqlCommand komut5 = new SqlCommand("SELECT * FROM tblUrunler WHERE stok=(SELECT MAX(stok) FROM tblUrunler)", baglanti);
            SqlDataReader dr5 = komut5.ExecuteReader();
            while (dr5.Read())
            {
                lblEnYuksekStokluUrun.Text = dr5["urunAd"].ToString();
                lblEnYuksekStokluUrunAdet.Text = (dr5[2].ToString() + " Adet");
            }

            baglanti.Close();


            // En Düşük Stoklu Ürün Sayısı : 

            baglanti.Open();

            SqlCommand komut6 = new SqlCommand("SELECT * FROM tblUrunler WHERE stok=(SELECT MIN(stok) FROM tblUrunler)", baglanti);
            SqlDataReader dr6 = komut6.ExecuteReader();
            while (dr6.Read())
            {
                lblEnDusukStokluUrun.Text = dr6["urunAd"].ToString();
                lblEnDusukStokluUrunAdet.Text = (dr6[2].ToString() + " Adet");
            }

            baglanti.Close();


            // Laptop'tan Elde edilecek Kar
            baglanti.Open();

            SqlCommand komut7 = new SqlCommand("SELECT stok * (satisFiyati - alisFiyati) FROM tblUrunler WHERE urunAd='Laptop'", baglanti);
            SqlDataReader dt7 = komut7.ExecuteReader();
            while (dt7.Read())
            {
                lblLaptopKar.Text = dt7[0].ToString() + " ₺";
            }

            baglanti.Close();

            // Beyaz Eşya Toplam Kar Oranı

            baglanti.Open();

            SqlCommand komut8 = new SqlCommand("SELECT SUM(stok * (satisFiyati - alisFiyati)) AS [TOPLAM KAR] FROM tblUrunler WHERE kategori = (SELECT ID FROM tblKategori WHERE Ad = 'Beyazeşya')", baglanti);
            SqlDataReader dt8 = komut8.ExecuteReader();
            while (dt8.Read())
            {
                lblBeyazEsyaToplamKar.Text = dt8[0].ToString() + " ₺";
            }

            baglanti.Close();
        }
    }
}
