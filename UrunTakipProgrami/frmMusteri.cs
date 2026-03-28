using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UrunTakipProgrami.DataSetimTableAdapters;

namespace UrunTakipProgrami
{
    public partial class frmMusteri : Form
    {
        public frmMusteri()
        {
            InitializeComponent();
        }

        // Global Data Set BAğlantı adresim : 
        DataSetimTableAdapters.tblMusteriTableAdapter tb = new DataSetimTableAdapters.tblMusteriTableAdapter();

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rdbAd_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnListele_Click(object sender, EventArgs e)
        {

            dataGridView1.DataSource = tb.musteriListesi();

        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            tb.musteriEkle(txtAd.Text, txtSoyad.Text, txtSehir.Text, decimal.Parse(txtBakiye.Text));
            MessageBox.Show("Müşteri Eklendi", "BİLGİLENDİRME", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            tb.musteriSil(int.Parse(txtID.Text));
            MessageBox.Show("Müşteri Silindi", "BİLGİLENDİRME", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtSehir.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtBakiye.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            tb.musteriGuncelle(txtAd.Text, txtSoyad.Text, txtSehir.Text, decimal.Parse(txtBakiye.Text),int.Parse(txtID.Text));
            MessageBox.Show("Müşteri Güncellendi", "BİLGİLENDİRME", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            if (rdbAd.Checked == true)
            {
               dataGridView1.DataSource = tb.araAd(txtAranacak.Text);
            }

            if (rdbSoyad.Checked == true)
            {
                dataGridView1.DataSource = tb.araSoyad(txtAranacak.Text);
            }

            if (rdbSehir.Checked == true)
            {
                dataGridView1.DataSource = tb.araSehir(txtAranacak.Text);
            }

        }
    }
}
