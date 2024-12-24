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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using System.Net.Mail;

namespace KuaforRandevu
{
    public partial class KayitForm : Form
    {
        private string dogrulamaKodu;
        public KayitForm()
        {
            InitializeComponent();
        }

        static bool SayiMi(string deger)
        {
            return long.TryParse(deger, out _);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void KayitForm_Load(object sender, EventArgs e)
        {
            YeniDogrulamaKoduOlustur();
            timer1.Interval = 1000;
            timer1.Start();

            sifre_txt.UseSystemPasswordChar = true; // Şifre başlangıçta gizli olacak
            sifretekrar_txt.UseSystemPasswordChar = true; // Şifre başlangıçta gizli olacak

            // Kenarları Yuvarlatma Kodu
            System.Drawing.Drawing2D.GraphicsPath formPath = new System.Drawing.Drawing2D.GraphicsPath();
            formPath.AddArc(0, 0, 20, 20, 180, 90);
            formPath.AddArc(this.Width - 20, 0, 20, 20, 270, 90);
            formPath.AddArc(this.Width - 20, this.Height - 20, 20, 20, 0, 90);
            formPath.AddArc(0, this.Height - 20, 20, 20, 90, 90);
            formPath.CloseAllFigures();
            this.Region = new Region(formPath);
        }

        private void YeniDogrulamaKoduOlustur()
        {
            Random rnd = new Random();
            const string karakter = "QWERTTYUIOPASDFGHJKLZXCVBNMÖqwertyuıopasdfghjklşzxcvbnmöç1234567890";
            dogrulamaKodu = "";
            for (int i = 0; i < 6; i++)
            {
                dogrulamaKodu += karakter[rnd.Next(karakter.Length)];
            }
            label8.Text = dogrulamaKodu;
        }

        int sayac = 60;

        private bool emailkontrol(string email)
        {
            try
            {
                MailAddress ma = new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string telefon = new string(telno_txt.Text.Where(char.IsDigit).ToArray());

            if (string.IsNullOrWhiteSpace(tamisim_txt.Text) ||
                string.IsNullOrWhiteSpace(eposta_txt.Text) ||
                string.IsNullOrWhiteSpace(sifre_txt.Text) ||
                string.IsNullOrWhiteSpace(sifretekrar_txt.Text) ||
                string.IsNullOrWhiteSpace(telefon) ||
                string.IsNullOrWhiteSpace(dogrulamakodu_txt.Text))
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.");
                return;
            }

            if (dogrulamakodu_txt.Text != label8.Text)
            {
                MessageBox.Show("Doğrulama kodu hatalı.");
                return;
            }

            if (!emailkontrol(eposta_txt.Text))
            {
                MessageBox.Show("Geçerli bir e-posta adresi giriniz.");
                return;
            }

            if (!SayiMi(telefon))
            {
                MessageBox.Show("Girdiğiniz telefon numarası geçersiz.");
                return;
            }

            if (sifre_txt.Text != sifretekrar_txt.Text)
            {
                MessageBox.Show("Şifreler aynı değil.", "Hata");
                return;
            }

            KullaniciEkle(tamisim_txt.Text, eposta_txt.Text, sifre_txt.Text, telefon);
        }

        private void KullaniciEkle(string tamAd, string eposta, string sifre, string telefon)
        {
            using (NpgsqlConnection conn = VeriTabaniYardimcisi.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Kullanicilar (TamAd, Eposta, Sifre, TelefonNumarasi, YetkiID) " +
                                   "VALUES (@TamAd, @Eposta, @Sifre, @TelefonNumarasi, 1)";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TamAd", tamAd);
                        cmd.Parameters.AddWithValue("@Eposta", eposta);
                        cmd.Parameters.AddWithValue("@Sifre", sifre);
                        cmd.Parameters.AddWithValue("@TelefonNumarasi", telefon);

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Kullanıcı başarıyla eklendi.");

                            this.Close();
                            Giris giris = new Giris();
                            giris.Show();
                        }
                        else
                        {
                            MessageBox.Show("Kullanıcı eklenirken bir hata oluştu.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Veritabanı hatası: {ex.Message}");
                }
            }
        }

        private void sifre_txt_TextChanged(object sender, EventArgs e)
        {
            int karaktersayisi = sifre_txt.Text.Length;

            if (karaktersayisi > 0 && karaktersayisi < 4)
            {
                panel1.BackColor = Color.Red;
                panel5.BackColor = Color.Red;
                label6.Text = "Zayıf";
            }
            else if (karaktersayisi >= 4 && karaktersayisi < 8)
            {
                panel1.BackColor = Color.Yellow;
                panel5.BackColor = Color.Yellow;
                label6.Text = "Orta";
            }
            else if (karaktersayisi >= 8)
            {
                panel1.BackColor = Color.Green;
                panel5.BackColor = Color.Green;
                label6.Text = "Güçlü";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sayac--;
            label9.Text = sayac.ToString();
            progressBar1.Value = sayac;
            if (sayac == 0)
            {
                timer1.Stop();
                MessageBox.Show("Süre doldu tekrar deneyin!");
                sayac = 60;
                YeniDogrulamaKoduOlustur();
                timer1.Start();
            }
        }

        private void sifretekrar_txt_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            if (sifretekrar_txt.UseSystemPasswordChar || sifre_txt.UseSystemPasswordChar)
            {
                sifretekrar_txt.UseSystemPasswordChar = false;
                sifre_txt.UseSystemPasswordChar = false;
            }
            else
            {
                sifretekrar_txt.UseSystemPasswordChar = true;
                sifre_txt.UseSystemPasswordChar = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
            Giris giris = new Giris();
            giris.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized; // Formu küçült
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }
}
