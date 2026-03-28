using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UrunTakipProgrami
{
    public partial class frmSatislar : Form
    {
        public frmSatislar()
        {
            InitializeComponent();
        }

        // SQL Bağalntı Adresim : 
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-QBP0E0A;Initial Catalog=dbUrun;Integrated Security=True");

        // DataSet bağlantı adresim 
        DataSetimTableAdapters.tblSatislarTableAdapter dataSetBaglantim = new DataSetimTableAdapters.tblSatislarTableAdapter();

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Sağ tıkla çıkış işlemi : 
            this.Close();
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            // // DataGridView Ürün Veritabanındaki Oluşturduğumuz Procedure getirilmesi. 
            //SqlCommand komut1 = new SqlCommand("EXECUTE satisListesi", baglanti);

            // // SQL View ile kullanımı 
            SqlCommand komut1 = new SqlCommand("SELECT * FROM View_1", baglanti);


            SqlDataAdapter da1 = new SqlDataAdapter(komut1);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);


            dataGridView1.Columns.Clear(); // Bunu View çağırdığımda soyadı sona attığı için yaptım. 
            dataGridView1.DataSource = dt1;

        }

        private void frmSatislar_Load(object sender, EventArgs e)
        {

            //ComboBox1 Ürün listesinin getirilmesi. 

            SqlCommand komut2 = new SqlCommand("SELECT * FROM tblUrunler", baglanti);
            SqlDataAdapter da2 = new SqlDataAdapter(komut2);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            comboBox1.ValueMember = "urunID";
            comboBox1.DisplayMember = "urunAd";
            comboBox1.DataSource = dt2;


            // DataSet üzerinden otomatik dataGridView Satişler listesinin gelmesi. 

            dataGridView1.DataSource = dataSetBaglantim.satisListesi();


        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            dataSetBaglantim.satisEkle(int.Parse(comboBox1.SelectedValue.ToString()),
            int.Parse(txtMusteri.Text), byte.Parse(txtAdet.Text), Decimal.Parse(txtFiyat.Text),
            decimal.Parse(txtToplam.Text), DateTime.Parse(mskTarih.Text));

            MessageBox.Show("Satış işlemi Gerçekleşmitir.", "BİLGİLENDİRME", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        }

        private void btnHesapla_Click(object sender, EventArgs e)
        {
            double adet, fiyat, toplam;
            adet = Convert.ToDouble(txtAdet.Text);
            fiyat = Convert.ToInt16(txtFiyat.Text);
            toplam = adet * fiyat;

            txtToplam.Text = toplam.ToString();

        }










        /*
        SQL'de Procedure Nedir : 
        SQL de uzunca yazılan kodları her dafasına tekrar tekrar yazmaktansa bir method mantığı gibi yazıp ihtiyaç duydukça kullanmaya yarar.
        Oluşturulan Procudure'ün Saklandığı yer Programmability Altındadır. 

        örnek Olarak aşağıdaki kodu her seferinde yazmaktansa; 
        SELECT ID, ad + ' '+soyad AS 'Müşteri Adı Soyadı', urunAd, adet, fiyat, toplam, tarih 
        FROM tblSatislar
        INNER JOIN tblUrunler
        ON tblSatislar.urun=tblUrunler.urunID
        INNER JOIN tblMusteri
        ON tblSatislar.ID=tblMusteri.musteriID



         * SQL'de Procedure Oluşturarak bir Procedure oluştururuz. (Create Procedure procedureAdı AS SQLKODLARI)
        ************************************************************************
        CREATE PROCEDURE satisListesi
        AS
        SELECT ID, ad + ' '+soyad AS 'Müşteri Adı Soyadı', urunAd, adet, fiyat, toplam, tarih 
        FROM tblSatislar
        INNER JOIN tblUrunler
        ON tblSatislar.urun=tblUrunler.urunID
        INNER JOIN tblMusteri
        ON tblSatislar.ID=tblMusteri.musteriID


        // Oluşturmuş olduğum Procedure çağırmak için ise aşağıdaki komutu kullanıyorum. 
        EXECUTE satisListesi


        // Procedure Parametre Ekleme : 

        CREATE PROCEDURE urunGetir(@ID int)
        AS
        SELECT * FROM tblUrunler WHERE urunID=@ID

        Çalıştırmak için : 
        Executude urunGetir 2 



        // Views : 
        SQL’de View, tabloya benzer şekilde sadece veri gösteren sanal bir yapıdır; Stored Procedure ise parametre alabilen, 
        mantık ve işlem barındırabilen, veri üzerinde değişiklik yapabilen programlanabilir bir yapıdır. 
        View sadece SELECT sorgularını temsil ederken, Procedure veri ekleme, silme, güncelleme gibi işlemleri de yapabilir.



        // Trigger nedir? 
        Tetikleyici olarak adlandırılmaktadır. 
        Herhangi bir veritabanı işlemi anında veya işlemden sonra bir başka işlem gerçekleştirilmesini sağlayan yapıdır. 
        Örneğin, bir tabloda ürün satışı yaptıktan sonra toplam stok sayısının satış adedi kadar azalması trigger ile yapılabilir. 
        
        Triger nasıl oluşturulu? 
        CREATE TRIGGER
        ON tblUrunler --Çalışılacak Tablo adı
        AFTER (--INSERT-DELETE) -- Tetiklenecek işlem
        AS
        UPDATE tblStok SET Adet=Adet+1 -- Adet bir bir arttır/azalt.

        Trigger Değişken kullanımı : 


        CREATE TRIGGER stokEkle
        AFTER DELETE
        AS
        DECLARE @stokSayiEkle int 
        SELECT @stokSayi=Stok FROM DELETED
        UPDATE tblstoklar SET Adet=Adet + @stokSayi


        CREATE TRIGGER stokDusur
        ON tblUrunler
        AFTER DELETE
        AS
        DECLARE @stokSayiDusur int 
        SELECT @stokSayi=Stok FROM DELETED
        UPDATE tblstoklar SET Adet=Adet - @stokSayi


        */

    }
}
