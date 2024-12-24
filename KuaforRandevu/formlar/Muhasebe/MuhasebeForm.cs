using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Npgsql;
using static KuaforRandevu.Giris;

namespace KuaforRandevu
{
    public partial class MuhasebeForm : Form
    {
        public string kullaniciAdi;
        public MuhasebeForm(string adi)
        {
            InitializeComponent();
            kullaniciAdi = adi;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            giderEkle1.Show();
            finansRaporlari1.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void AnaForm_Load(object sender, EventArgs e)
        {
            giderEkle1.Hide();
            finansRaporlari1.Hide();


            label1.Text = kullaniciAdi;

            // Kenarları Yuvarlatma Kodu
            System.Drawing.Drawing2D.GraphicsPath formPath = new System.Drawing.Drawing2D.GraphicsPath();
            formPath.AddArc(0, 0, 20, 20, 180, 90);
            formPath.AddArc(this.Width - 20, 0, 20, 20, 270, 90);
            formPath.AddArc(this.Width - 20, this.Height - 20, 20, 20, 0, 90);
            formPath.AddArc(0, this.Height - 20, 20, 20, 90, 90);
            formPath.CloseAllFigures();
            this.Region = new Region(formPath);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            giderEkle1.Hide();
            finansRaporlari1.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void cikisyap_Click(object sender, EventArgs e)
        {
            // Formu kapat
            this.Close();

            // Giriş formunu yeniden oluştur ve göster
            Giris girisForm = new Giris();
            girisForm.Show();

            // Kullanıcı çıkış yaptıktan sonra tüm kullanıcı bilgilerini sıfırlayabiliriz.
            KullaniciBilgileri.KullaniciID = 0;
            KullaniciBilgileri.eposta = string.Empty;
            KullaniciBilgileri.YetkiID = 0;

            MessageBox.Show("Başarıyla çıkış yapıldı.", "Çıkış", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Muhasebe Formundan Kullanıcı Formuna geçiş yapma butonu
            // Formu kapat
            this.Close();

            // KullaniciForm'u parametreyle oluşturun
            KullaniciForm kullaniciForm = new KullaniciForm(kullaniciAdi);  // Parametreyi geçiyoruz
            kullaniciForm.Show();  // Formu gösteriyoruz

            // Kullanıcıya bilgi mesajı göster
            MessageBox.Show("Muhasebe profilinden kullanıcı profiline geçiş yaptınız.", "Kullanıcı Girişi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void giderEkle1_Load(object sender, EventArgs e)
        {

        }
    }
}
