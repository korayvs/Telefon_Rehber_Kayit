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

namespace Telefon_Rehber_Kayıt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-F1A12T8\KORAY;Initial Catalog=Rehber;Integrated Security=True");

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from KISILER", baglanti);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["RESIM"].Visible = false;
        }

        void temizle()
        {
            TxtID.Text = "";
            TxtAd.Text = "";
            TxtSoyad.Text = "";
            TxtMail.Text = "";
            MskTel.Text = "";
            pictureBox1.Refresh();
            TxtAd.Focus();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Insert Into KISILER (AD, SOYAD, TELEFON, MAIL, RESIM) Values (@p1, @p2, @p3, @p4, @p5)", baglanti);
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", MskTel.Text);
            komut.Parameters.AddWithValue("@p4", TxtMail.Text);
            komut.Parameters.AddWithValue("@p5", TxtResim.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kişi Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            TxtID.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            MskTel.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            TxtMail.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            TxtResim.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(TxtAd.Text + " " + TxtSoyad.Text + " rehberden silinsin mi?", "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("Delete From KISILER Where ID =" + TxtID.Text, baglanti);
                komut.ExecuteNonQuery();
                baglanti.Close();
                listele();
                temizle();
            }
            else
            {
                listele();
            }
            baglanti.Close();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Kişi bilgileri güncellensin mi?", "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("Update KISILER Set AD=@p1, SOYAD=@p2, TELEFON=@p3, MAIL=@p4, RESIM=@p5 Where ID=@p6", baglanti);
                komut.Parameters.AddWithValue("@p1", TxtAd.Text);
                komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
                komut.Parameters.AddWithValue("@p3", MskTel.Text);
                komut.Parameters.AddWithValue("@p4", TxtMail.Text);
                komut.Parameters.AddWithValue("@p5", TxtResim.Text);
                komut.Parameters.AddWithValue("@p6", TxtID.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                listele();
                temizle();
            }
            else
            {
                listele();
            }
            baglanti.Close();
        }

        private void BtnResim_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            pictureBox1.ImageLocation = openFileDialog1.FileName;
            TxtResim.Text = openFileDialog1.FileName.ToString();
        }

        private void TxtResim_TextChanged(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = TxtResim.Text;
        }
    }
}
