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
    public partial class UrunlerFormu : Form
    {
        public UrunlerFormu()
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



        private void btnListele_Click(object sender, EventArgs e)
        {

            //ÜRÜN LİSTELEME İŞLEMLERİ
            SqlCommand komut1 = new SqlCommand("SELECT UrunID, urunAd, stok, alisFiyati, satisFiyati, Ad, kategori FROM tblUrunler INNER JOIN tblKategori ON tblUrunler.kategori=tblKategori.ID", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut1);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["Kategori"].Visible = false;
        }



        private void btnKaydet_Click(object sender, EventArgs e)
        {

            //ÜRÜN KAYDETME İŞLEMLERİ
            baglanti.Open();

            SqlCommand komut3 = new SqlCommand("INSERT INTO tblUrunler (urunAd, stok, alisFiyati, satisFiyati, kategori) VALUES (@P1, @P2, @P3, @P4, @P5)", baglanti);
            komut3.Parameters.AddWithValue("@P1", txtUrunAd.Text);
            komut3.Parameters.AddWithValue("@P2", nudStok.Value);
            komut3.Parameters.AddWithValue("@P3", txtAlisFiyat.Text);
            komut3.Parameters.AddWithValue("@P4", txtSatisFiyat.Text);
            komut3.Parameters.AddWithValue("@P5", comboBox1.SelectedValue);
            komut3.ExecuteNonQuery();

            baglanti.Close();

            MessageBox.Show("Ürün Kaydı yapılmıştır.", "ÜRÜN KAYIT İŞLEMİ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        }

        private void UrunlerFormu_Load(object sender, EventArgs e)
        {
            // COMBOX'a KATEGORİLERİ LİSTELEME İŞLEMİ

            SqlCommand komut2 = new SqlCommand("SELECT * FROM tblKategori", baglanti);
            SqlDataAdapter da2 = new SqlDataAdapter(komut2);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            comboBox1.DisplayMember = "Ad";
            comboBox1.ValueMember = "ID";
            comboBox1.DataSource = dt2;

        }

        private void btnSil_Click(object sender, EventArgs e)
        {

            // ÜRÜN SİLME İŞLEMLERİ
            baglanti.Open();

            SqlCommand komut4 = new SqlCommand("DELETE FROM tblUrunler WHERE urunID=@P1", baglanti);
            komut4.Parameters.AddWithValue("@P1", txtID.Text);
            komut4.ExecuteNonQuery();

            baglanti.Close();

            MessageBox.Show("Silme İşlemi yapılmıştır.", "ÜRÜN SİLME İŞLEMİ", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            // ÜRÜN GÜNCELLEME İŞLEMLERİ
            baglanti.Open();

            SqlCommand komut5 = new SqlCommand("UPDATE tblUrunler SET urunAd=@p1, stok=@p2, alisFiyati=@p3, satisFiyati=@p4, kategori=@p5 WHERE urunID=@p6", baglanti);
            komut5.Parameters.AddWithValue("@p1", txtUrunAd.Text);
            komut5.Parameters.AddWithValue("@p2", nudStok.Value);
            komut5.Parameters.AddWithValue("@p3", decimal.Parse(txtAlisFiyat.Text));
            komut5.Parameters.AddWithValue("@p4", decimal.Parse(txtSatisFiyat.Text));
            komut5.Parameters.AddWithValue("@p5", comboBox1.SelectedValue);
            komut5.Parameters.AddWithValue("@p6", txtID.Text);
            komut5.ExecuteNonQuery();

            baglanti.Close();

            MessageBox.Show("Güncelleme İşlemi yapılmıştır.", "ÜRÜN GÜNCELLEME İŞLEMİ", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // dataGridView SEÇİLEN KAYIT KISMINA OTOMATİK GELMESİ.

            txtID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtUrunAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            nudStok.Value = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
            txtAlisFiyat.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtSatisFiyat.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            comboBox1.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();

        }
    }
}
