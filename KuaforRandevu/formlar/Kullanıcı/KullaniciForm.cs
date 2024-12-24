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
using Npgsql;
using System.Runtime.InteropServices.ComTypes;

namespace KuaforRandevu
{
    public partial class KullaniciForm : Form
    {
        // Kullanıcı adı ve yetki bilgilerini tutan değişkenler
        private string kullaniciAdi;
        public int kullaniciid;
        // Kullanıcı bilgilerini almak için bir kurucu metod ekleyelim
        public KullaniciForm(string adi)
        {

            InitializeComponent();
            kullaniciAdi = adi;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            karsilama1.Hide();
            bilgilerimiDuzenle.Hide();
            randevularim1.Hide();
            RandevuOlustur.Show();
            hakkimizda1.Hide();
            RandevuListele randevuListesi = new RandevuListele();

            panel2.Width = button1.Width; // Genişliği butonun genişliğiyle aynı yap
            panel2.Height = 1; // Yüksekliği 1 piksel yap (ince çizgi görünümü için)
            panel2.Top = button1.Bottom; // Panelin üst konumunu butonun altına ayarla
            panel2.Left = button1.Left; // Paneli yatayda butonla hizala
            panel2.Visible = true; // Paneli görünür yap
        }

        private void KullaniciForm_Load(object sender, EventArgs e)
        {
            // Form elemanlarının başlangıç görünürlük ayarları
            RandevuOlustur.Hide();
            bilgilerimiDuzenle.Hide();
            randevularim1.Hide();
            panel2.Hide();
            hakkimizda1.Hide();
            groupBox1.Hide();
            muhasebedon_btn.Hide();
            personeldon_btn.Hide();
            admindon_btn.Hide();

            // Label ayarları
            musteriadi_lbl.AutoSize = false;
            musteriadi_lbl.Width = 150; // Label'ın sabit genişliği
            musteriadi_lbl.Text = kullaniciAdi;

            // Font boyutunu otomatik ayarlama
            float fontSize = 9.0f; // Başlangıç font boyutu
            Font originalFont = musteriadi_lbl.Font;

            // Metin sığana kadar font boyutunu küçült
            while (TextRenderer.MeasureText(kullaniciAdi, new Font(originalFont.FontFamily, fontSize)).Width > musteriadi_lbl.Width && fontSize > 6.0f)
            {
                fontSize -= 0.5f;
            }

            // Yeni font boyutunu uygula
            musteriadi_lbl.Font = new Font(originalFont.FontFamily, fontSize, originalFont.Style);

            // Form kenarlarını yuvarlama
            System.Drawing.Drawing2D.GraphicsPath formPath = new System.Drawing.Drawing2D.GraphicsPath();
            formPath.AddArc(0, 0, 20, 20, 180, 90);
            formPath.AddArc(this.Width - 20, 0, 20, 20, 270, 90);
            formPath.AddArc(this.Width - 20, this.Height - 20, 20, 20, 0, 90);
            formPath.AddArc(0, this.Height - 20, 20, 20, 90, 90);
            formPath.CloseAllFigures();
            this.Region = new Region(formPath);

            // Kullanıcı yetki kontrolü
            using (NpgsqlConnection baglanti = VeriTabaniYardimcisi.GetConnection())
            {
                baglanti.Open();
                string sorguKontrol = "SELECT kullaniciid, yetkiid FROM kullanicilar WHERE tamad = @ad";
                using (NpgsqlCommand kontrol = new NpgsqlCommand(sorguKontrol, baglanti))
                {
                    kontrol.Parameters.AddWithValue("@ad", kullaniciAdi);
                    using (NpgsqlDataReader reader = kontrol.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            kullaniciid = reader.GetInt32(0);
                            int yetkiid = reader.GetInt32(1);

                            // Yetki ID'sine göre groupBox görünürlüğü
                            if (yetkiid == 2 || yetkiid == 3 || yetkiid == 4)
                            {
                                groupBox1.Show();
                            }

                            // Yetki ID'sine göre buton görünürlükleri
                            switch (yetkiid)
                            {
                                case 2:
                                    personeldon_btn.Show();
                                    break;
                                case 3:
                                    muhasebedon_btn.Show();
                                    break;
                                case 4:
                                    admindon_btn.Show();
                                    break;
                            }
                        }
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            karsilama1.Hide();
            RandevuOlustur.Hide();
            bilgilerimiDuzenle.Show();
            randevularim1.Hide();
            hakkimizda1.Hide();

            panel2.Width = button4.Width; // Genişliği butonun genişliğiyle aynı yap
            panel2.Height = 1; // Yüksekliği 1 piksel yap (ince çizgi görünümü için)
            panel2.Top = button4.Bottom; // Panelin üst konumunu butonun altına ayarla
            panel2.Left = button4.Left; // Paneli yatayda butonla hizala
            panel2.Visible = true; // Paneli görünür yap

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

        private void anasayfa_Click(object sender, EventArgs e)
        {
            karsilama1.Show();
            RandevuOlustur.Hide();
            bilgilerimiDuzenle.Hide();
            randevularim1.Hide();
            hakkimizda1.Hide();

            panel2.Hide();
        }

        private void RandevuOlustur_Load(object sender, EventArgs e)
        {

        }

        private void karsilama1_Load(object sender, EventArgs e)
        {

        }

        private void bilgilerimiDuzenle_Load(object sender, EventArgs e)
        {
            using (NpgsqlConnection baglanti = VeriTabaniYardimcisi.GetConnection())
            {
                baglanti.Open();
                string sorguKontrol = "SELECT kullaniciid FROM kullanicilar WHERE tamad = @ad";
                using (NpgsqlCommand kontrol = new NpgsqlCommand(sorguKontrol, baglanti))
                {
                    kontrol.Parameters.AddWithValue("@ad", kullaniciAdi);
                    using (NpgsqlDataReader reader = kontrol.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            kullaniciid = reader.GetInt32(0);
                        }
                    }
                }
            }
            label1.Text = kullaniciid.ToString();
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            randevularim1.Show();
            karsilama1.Hide();
            RandevuOlustur.Hide();
            bilgilerimiDuzenle.Hide();
            hakkimizda1.Hide();

            panel2.Width = button5.Width; // Genişliği butonun genişliğiyle aynı yap
            panel2.Height = 1; // Yüksekliği 1 piksel yap (ince çizgi görünümü için)
            panel2.Top = button5.Bottom; // Panelin üst konumunu butonun altına ayarla
            panel2.Left = button5.Left; // Paneli yatayda butonla hizala
            panel2.Visible = true; // Paneli görünür yap
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void admindon_btn_Click(object sender, EventArgs e)
        {
            // Kullanıcı formundan Admin Formuna geçiş yapma butonu
            // Formu kapat
            this.Close();

            // AdminForm'u parametreyle oluşturun
            AdminForm adminForm = new AdminForm(kullaniciAdi);  // Parametreyi geçiyoruz
            adminForm.Show();  // Formu gösteriyoruz

            // Kullanıcıya bilgi mesajı göster
            MessageBox.Show("Kullanıcı profilinden Admin profiline dönüş yaptınız. ", "Admin Profiline Dönüş", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void personeldon_btn_Click(object sender, EventArgs e)
        {
            // Kullanıcı formundan Personel Formuna geçiş yapma butonu
            // Formu kapat
            this.Close();

            // PersonelForm'u parametreyle oluşturun
            PersonelForm personelForm = new PersonelForm(kullaniciAdi);  // Parametreyi geçiyoruz
            personelForm.Show();  // Formu gösteriyoruz

            // Kullanıcıya bilgi mesajı göster
            MessageBox.Show("Kullanıcı profilinden Personel profiline dönüş yaptınız. ", "Personel Profiline Dönüş", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void muhasebedon_btn_Click(object sender, EventArgs e)
        {
            // Kullanıcı formundan Muhasebe Formuna geçiş yapma butonu
            // Formu kapat
            this.Close();

            // MuhasebeForm'u parametreyle oluşturun
            MuhasebeForm muhasebeForm = new MuhasebeForm(kullaniciAdi);  // Parametreyi geçiyoruz
            muhasebeForm.Show();  // Formu gösteriyoruz

            // Kullanıcıya bilgi mesajı göster
            MessageBox.Show("Kullanıcı profilinden Personel profiline dönüş yaptınız. ", "Personel Profiline Dönüş", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            hakkimizda1.Show();
            karsilama1.Hide();
            RandevuOlustur.Hide();
            bilgilerimiDuzenle.Hide();
            randevularim1.Hide();

            panel2.Width = button6.Width; // Genişliği butonun genişliğiyle aynı yap
            panel2.Height = 1; // Yüksekliği 1 piksel yap (ince çizgi görünümü için)
            panel2.Top = button6.Bottom; // Panelin üst konumunu butonun altına ayarla
            panel2.Left = button6.Left; // Paneli yatayda butonla hizala
            panel2.Visible = true; // Paneli görünür yap
        }

        private void musteriadi_lbl_Click(object sender, EventArgs e)
        {

        }
    }
}
