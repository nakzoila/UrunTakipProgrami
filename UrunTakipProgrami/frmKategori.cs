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
    public partial class frmKategori : Form
    {
        public frmKategori()
        {
            InitializeComponent();
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmYonlendirme yonlendirme = new frmYonlendirme();
            yonlendirme.Show();
            this.Hide();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-QBP0E0A;Initial Catalog=dbUrun;Integrated Security=True");

        private void btnListele_Click(object sender, EventArgs e)
        {

            SqlCommand komut1 = new SqlCommand("SELECT * FROM tblKategori", baglanti);
            SqlDataAdapter dataAdapTersNesnesi = new SqlDataAdapter(komut1);
            DataTable dataTableNesnesi = new DataTable();
            dataAdapTersNesnesi.Fill(dataTableNesnesi);
            dataGridView1.DataSource = dataTableNesnesi;


        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("INSERT INTO tblKategori (Ad) VALUES (@p1)", baglanti);
            komut2.Parameters.AddWithValue("@p1", txtKategoriAd.Text);
            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt gerçekleştirdi.","KAYIT İŞLEMİ", MessageBoxButtons.OK,MessageBoxIcon.Information);

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut3 = new SqlCommand("DELETE FROM tblKategori WHERE ID=@p1", baglanti);
            komut3.Parameters.AddWithValue("@p1", txtID.Text);
            komut3.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Silme gerçekleştirdi.", "SİLME İŞLEMİ", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut4 = new SqlCommand("UPDATE tblKategori SET Ad=@p1 WHERE ID=@p2", baglanti);
            komut4.Parameters.AddWithValue("@p1",  txtKategoriAd.Text);
            komut4.Parameters.AddWithValue("@p2", txtID.Text);
            komut4.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Güncelleme gerçekleştirdi.", "GÜNCELLEME İŞLEMİ", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            SqlCommand komut5 = new SqlCommand("SELECT * FROM tblKategori WHERE Ad=@p1", baglanti);
            komut5.Parameters.AddWithValue("@p1", txtKategoriAd.Text);
            SqlDataAdapter dataAdapTersNesnesi = new SqlDataAdapter(komut5);
            DataTable dataTableNesnesi = new DataTable();
            dataAdapTersNesnesi.Fill(dataTableNesnesi);
            dataGridView1.DataSource = dataTableNesnesi;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

//Data Source=DESKTOP-QBP0E0A;Initial Catalog=dbUrun;Integrated Security=True
//Data Source=DESKTOP-QBP0E0A;Initial Catalog=dbUrun;Integrated Security=True;Trust Server Certificate=True

/*
 

SqlConnetion    : Bağlantı Sınıfı
SqlCommand      : Komut Sınıfı (New Query) Komutları 
SqlDataAdapter  : Köprü Sınıfı
DataTable       : Veri Tablosu

*/