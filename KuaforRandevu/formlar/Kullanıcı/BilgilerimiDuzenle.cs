using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;
using static KuaforRandevu.Giris;

namespace KuaforRandevu.formlar
{
    public partial class BilgilerimiDuzenle : UserControl
    {
        public int KullaniciId { get; set; }

        public BilgilerimiDuzenle()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(yeniadsoyad.Text) ||
                    string.IsNullOrWhiteSpace(textBox2.Text) ||
                    string.IsNullOrWhiteSpace(yenisifre_txt.Text) ||
                    string.IsNullOrWhiteSpace(yenisifretekrari_txt.Text) ||
                    string.IsNullOrWhiteSpace(maskedTextBox1.Text))
                {
                    MessageBox.Show("Lütfen tüm alanları doldurduğunuzdan emin olun.");
                    return;
                }

                if (yenisifre_txt.Text.Trim() != yenisifretekrari_txt.Text.Trim())
                {
                    MessageBox.Show("Şifreler eşit değil. Lütfen kontrol edin.");
                    return;
                }

                // Mevcut şifreyi al
                string mevcutSifre = mevcutsifre_txt.Text.Trim();

                // Yeni şifrenin mevcut şifreyle aynı olup olmadığını kontrol et
                if (yenisifre_txt.Text.Trim() == mevcutSifre)
                {
                    MessageBox.Show("Yeni şifre mevcut şifre ile aynı olamaz. Lütfen farklı bir şifre girin.");
                    return; // Güncellemeyi durdur
                }

                using (var conn = VeriTabaniYardimcisi.GetConnection())
                {
                    conn.Open();

                    string sorgu = "UPDATE kullanicilar SET tamad = @p1, eposta = @p2, sifre = @p3, telefonnumarasi = @p4 WHERE kullaniciid = @id";

                    using (var guncelle = new NpgsqlCommand(sorgu, conn))
                    {
                        guncelle.Parameters.AddWithValue("@p1", yeniadsoyad.Text.Trim());
                        guncelle.Parameters.AddWithValue("@p2", textBox2.Text.Trim());
                        guncelle.Parameters.AddWithValue("@p3", yenisifre_txt.Text.Trim());
                        guncelle.Parameters.AddWithValue("@p4", maskedTextBox1.Text.Trim());
                        guncelle.Parameters.AddWithValue("@id", KullaniciBilgileri.KullaniciID);

                        int etkilenenSatir = guncelle.ExecuteNonQuery();

                        if (etkilenenSatir > 0)
                        {
                            MessageBox.Show("Bilgiler başarıyla güncellendi.");
                        }
                        else
                        {
                            MessageBox.Show("Güncelleme yapılamadı. Lütfen bilgileri kontrol edin.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (yenisifretekrari_txt.UseSystemPasswordChar || yenisifre_txt.UseSystemPasswordChar)
            {
                yenisifretekrari_txt.UseSystemPasswordChar = false;
                yenisifre_txt.UseSystemPasswordChar = false;
                button1.Text = "Gizle";
            }
            else
            {
                yenisifretekrari_txt.UseSystemPasswordChar = true;
                yenisifre_txt.UseSystemPasswordChar = true;
                button1.Text = "Göster";
            }
        }

        private void BilgilerimiDuzenle_Load1(object sender, EventArgs e)
        {
            try
            {
                using (var conn = VeriTabaniYardimcisi.GetConnection())
                {
                    conn.Open();
                    string sorgu = "SELECT TamAd, Eposta, TelefonNumarasi, Sifre FROM Kullanicilar WHERE KullaniciID = @id";

                    using (var cmd = new NpgsqlCommand(sorgu, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", KullaniciBilgileri.KullaniciID);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Bilgileri doldur
                                yeniadsoyad.Text = reader["TamAd"].ToString();
                                textBox2.Text = reader["Eposta"].ToString();
                                maskedTextBox1.Text = reader["TelefonNumarasi"].ToString();

                                // Mevcut şifreyi doldur ve disable yap
                                mevcutsifre_txt.Text = reader["Sifre"].ToString();
                                mevcutsifre_txt.Enabled = false; // Kullanıcı tarafından düzenlenemez
                                                                 // Şifre alanlarını gizle
                                yenisifre_txt.UseSystemPasswordChar = true;
                                yenisifretekrari_txt.UseSystemPasswordChar = true;

                                // Butonun başlangıç metnini ayarla
                                button1.Text = "Göster";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void mevcutsifre_txt_TextChanged(object sender, EventArgs e) { }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e) { }

        private void yenisifre_txt_TextChanged(object sender, EventArgs e) { }
    }
}
