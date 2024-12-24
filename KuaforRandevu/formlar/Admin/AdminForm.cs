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
using static KuaforRandevu.Giris;

namespace KuaforRandevu
{
    public partial class AdminForm : Form
    {
        public string kullaniciAdi;
        public AdminForm(string adi)
        {
            InitializeComponent();
            kullaniciAdi = adi;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            KullaniciListele.Show();
            finansRaporlari1.Hide();
            kurumYonetimi1.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Uygulamayı kapat
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void AnaForm_Load(object sender, EventArgs e)
        {
            finansRaporlari1.Hide();
            KullaniciListele.Hide();
            kurumYonetimi1.Hide();
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
            finansRaporlari1.Show();
            KullaniciListele.Hide();
            kurumYonetimi1.Hide();
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
            // Admin Formundan Kullanıcı Formuna geçiş yapma butonu
            // Formu kapat
            this.Close();

            // KullaniciForm'u parametreyle oluşturun
            KullaniciForm kullaniciForm = new KullaniciForm(kullaniciAdi);  // Parametreyi geçiyoruz
            kullaniciForm.Show();  // Formu gösteriyoruz

            // Kullanıcıya bilgi mesajı göster
            MessageBox.Show("Admin profilinden kullanıcı profiline geçiş yaptınız.", "Kullanıcı Girişi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Admin Formundan Personel Formuna geçiş yapma butonu
            // Formu kapat
            this.Close();

            // PersonelForm'u parametreyle oluşturun
            PersonelForm personelForm = new PersonelForm(kullaniciAdi);  // Parametreyi geçiyoruz
            personelForm.Show();  // Formu gösteriyoruz

            // Kullanıcıya bilgi mesajı göster
            MessageBox.Show("Admin profilinden personel profiline geçiş yaptınız. Tekrar admin girişi yapmak için personel profilinden çıkış yaptıktan sonra tekrar admin hesabınızla giriş yapmanız gerekmektedir.", "Personel Girişi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Admin Formundan Personel Formuna geçiş yapma butonu
            // Formu kapat
            this.Close();

            // PersonelForm'u parametreyle oluşturun
            MuhasebeForm muhasebeForm = new MuhasebeForm(kullaniciAdi);  // Parametreyi geçiyoruz
            muhasebeForm.Show();  // Formu gösteriyoruz

            // Kullanıcıya bilgi mesajı göster
            MessageBox.Show("Admin profilinden muhasebe profiline geçiş yaptınız. Tekrar admin girişi yapmak için muhasebe profilinden çıkış yaptıktan sonra tekrar admin hesabınızla giriş yapmanız gerekmektedir.", "Muhasebe Girişi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            kurumYonetimi1.Show();
            finansRaporlari1.Hide();
            KullaniciListele.Hide();
        }
    }
}
