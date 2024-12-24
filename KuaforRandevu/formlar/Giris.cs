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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace KuaforRandevu
{
    public partial class Giris : Form
    {
        public Giris()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Kullanıcı adı ve şifreyi al
            string eposta = eposta_txt.Text;
            string sifre = sifre_txt.Text;


            using (NpgsqlConnection con = VeriTabaniYardimcisi.GetConnection())
            {
                try
                {
                    con.Open();

                    // SQL sorgusu (Kullanıcı adı, şifre ve yetki bilgilerini al)
                    string query = "SELECT KullaniciID, YetkiID, TamAd FROM Kullanicilar WHERE Eposta=@eposta AND Sifre=@sifre";

                    using (NpgsqlCommand com = new NpgsqlCommand(query, con))
                    {
                        // Parametreler ekleniyor
                        com.Parameters.AddWithValue("@eposta", eposta);
                        com.Parameters.AddWithValue("@sifre", sifre);

                        // Sorgu çalıştırılıyor
                        using (var reader = com.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Kullanıcının ID, yetki ve ad bilgilerini al
                                int kullaniciID = reader.GetInt32(0);
                                int yetkiID = reader.GetInt32(1);
                                string kullaniciAdi = reader.GetString(2);

                                // Kullanıcı bilgilerini statik sınıfa kaydet
                                KullaniciBilgileri.KullaniciID = kullaniciID;
                                KullaniciBilgileri.eposta = eposta;
                                KullaniciBilgileri.YetkiID = yetkiID;

                                // Yetkiye göre yönlendirme
                                if (yetkiID == 1) // Müşteri Yetkisi
                                {
                                    MessageBox.Show("Müşteri olarak giriş yaptınız.");
                                    KullaniciForm kullaniciForm = new KullaniciForm(kullaniciAdi); // Parametreleri ilet
                                    kullaniciForm.Show();
                                }
                                else if (yetkiID == 2) // Personel Olarak Giriş Yaptınız
                                {
                                    MessageBox.Show("Personel olarak giriş yaptınız.");
                                    PersonelForm personelForm = new PersonelForm(kullaniciAdi); // Parametreleri İlet
                                    personelForm.Show();
                                }
                                else if (yetkiID == 3) // Muhasebe Olarak Giriş Yaptınız
                                {
                                    MessageBox.Show("Muhasebe olarak giriş yaptınız.");
                                    MuhasebeForm muhasebeForm = new MuhasebeForm(kullaniciAdi);
                                    muhasebeForm.Show();
                                }
                                else if (yetkiID == 4) // Admin Olarak giriş yaptınız
                                {
                                    MessageBox.Show("Admin olarak giriş yaptınız.");
                                    AdminForm adminForm = new AdminForm(kullaniciAdi);
                                    adminForm.Show();
                                }

                                this.Hide(); // Giriş formunu gizle
                            }
                            else
                            {
                                MessageBox.Show("Hatalı kullanıcı adı veya şifre");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Bir hata oluştu: {ex.Message}");
                }
            }
        }

        private void Giris_Load(object sender, EventArgs e)
        {
            sifre_txt.UseSystemPasswordChar = true; // Şifre başlangıçta gizli olacak
            

            System.Drawing.Drawing2D.GraphicsPath formPath = new System.Drawing.Drawing2D.GraphicsPath();
            formPath.AddArc(0, 0, 20, 20, 180, 90);
            formPath.AddArc(this.Width - 20, 0, 20, 20, 270, 90);
            formPath.AddArc(this.Width - 20, this.Height - 20, 20, 20, 0, 90);
            formPath.AddArc(0, this.Height - 20, 20, 20, 90, 90);
            formPath.CloseAllFigures();
            this.Region = new Region(formPath);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Uygulamayı kapat
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized; // Formu küçült
        }

        private void sifre_txt_TextChanged(object sender, EventArgs e)
        {

        }

        public static class KullaniciBilgileri
        {
            public static int KullaniciID { get; set; }
            public static string eposta { get; set; }
            public static int YetkiID { get; set; }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            KayitForm kayitform = new KayitForm();
            kayitform.Show();

        }

        private void eposta_txt_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Şifre kutusunun UseSystemPasswordChar özelliğini tersine çeviriyoruz
            if (sifre_txt.UseSystemPasswordChar)
            {
                sifre_txt.UseSystemPasswordChar = false; // Şifreyi göster
               
            }
            else
            {
                sifre_txt.UseSystemPasswordChar = true; // Şifreyi gizle
              
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (sifre_txt.UseSystemPasswordChar)
            {
                sifre_txt.UseSystemPasswordChar = false; // Şifreyi göster

            }
            else
            {
                sifre_txt.UseSystemPasswordChar = true; // Şifreyi gizle

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}