using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static KuaforRandevu.Giris;

namespace KuaforRandevu
{
    public partial class RandevuOlustur : UserControl
    {
        public RandevuOlustur()
        {
            InitializeComponent();
            PersonelListele(); // Sayfa yüklendiğinde personelleri listele


        }


        private void UserControl2_Load(object sender, EventArgs e)
        {
            PersonelListele();
            HizmetListele();
        }

        // Personelleri ComboBox'ta listeleme
        private void PersonelListele()
        {
            comboBox1.Items.Clear();
            try
            {
                using (var baglanti = VeriTabaniYardimcisi.GetConnection())
                {
                    baglanti.Open();

                    string sql = "SELECT tamad FROM kullanicilar WHERE yetkiid = 2";
                    using (NpgsqlCommand komut = new NpgsqlCommand(sql, baglanti))
                    {
                        using (NpgsqlDataReader reader = komut.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                comboBox1.Items.Add(reader.GetString(0)); // Personelin kullanıcı adını ekle
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Personel listeleme hatası: {ex.Message}");
            }
        }

        // Seçilen personelin doluluk durumunu DataGridView'de gösterme
        private void PersonelRandevuDurumuGoster(string personelAdi)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("RandevuTarihi", "Randevu Tarihi");
            dataGridView1.Columns.Add("RandevuSaati", "Randevu Saati");

            dataGridView1.Rows.Clear();
            try
            {
                using (var baglanti = VeriTabaniYardimcisi.GetConnection())
                {
                    baglanti.Open();

                    string sql = @"
                        SELECT RandevuTarihi, RandevuSaati
                        FROM randevular
                        INNER JOIN kullanicilar ON randevular.PersonelID = kullanicilar.kullaniciid
                        WHERE kullanicilar.tamad = @personelAdi
                        AND RandevuTarihi >= CURRENT_DATE";

                    using (NpgsqlCommand komut = new NpgsqlCommand(sql, baglanti))
                    {
                        komut.Parameters.AddWithValue("@personelAdi", personelAdi);
                        using (NpgsqlDataReader reader = komut.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DateTime randevuTarihi = reader.GetDateTime(0);
                                TimeSpan randevuSaati = reader.GetTimeSpan(1);

                                dataGridView1.Rows.Add(randevuTarihi.ToString("yyyy-MM-dd"), randevuSaati.ToString(@"hh\:mm"));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Randevu durumu gösterme hatası: {ex.Message}");
            }
        }

        private void kaydet_button_Click(object sender, EventArgs e)
        {
            try
            {
                using (var baglanti = VeriTabaniYardimcisi.GetConnection())
                {
                    baglanti.Open();

                    if (string.IsNullOrEmpty(comboBox1.Text))
                    {
                        MessageBox.Show("Lütfen bir personel seçin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (comboBoxHizmetler.SelectedIndex == -1)
                    {
                        MessageBox.Show("Lütfen bir hizmet seçin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string saat = comboBoxSaat.SelectedItem?.ToString();
                    if (string.IsNullOrEmpty(saat))
                    {
                        MessageBox.Show("Lütfen bir saat seçin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Seçilen hizmetin fiyatını al
                    decimal toplamTutar = 0;
                    string fiyatSql = "SELECT Fiyat FROM Hizmetler WHERE HizmetAdi = @HizmetAdi";
                    using (NpgsqlCommand fiyatKomut = new NpgsqlCommand(fiyatSql, baglanti))
                    {
                        fiyatKomut.Parameters.AddWithValue("@HizmetAdi", comboBoxHizmetler.SelectedItem.ToString());
                        var fiyatResult = fiyatKomut.ExecuteScalar();
                        if (fiyatResult != null)
                        {
                            toplamTutar = Convert.ToDecimal(fiyatResult);
                        }
                    }

                    TimeSpan randevuSaati = TimeSpan.Parse(saat);
                    DateTime randevuTarihi = dateTimePicker1.Value;
                    string notlar = notlar_txt.Text;

                    // Veritabanında aynı tarih ve saatte randevu olup olmadığını kontrol et
                    string kontrolSql = @"
                SELECT 1
                FROM randevular
                WHERE PersonelID = (SELECT kullaniciid FROM kullanicilar WHERE tamad = @PersonelAdi)
                  AND RandevuTarihi = @RandevuTarihi
                  AND RandevuSaati = @RandevuSaati";

                    using (NpgsqlCommand kontrolKomut = new NpgsqlCommand(kontrolSql, baglanti))
                    {
                        kontrolKomut.Parameters.AddWithValue("@PersonelAdi", comboBox1.Text);
                        kontrolKomut.Parameters.AddWithValue("@RandevuTarihi", randevuTarihi);
                        kontrolKomut.Parameters.AddWithValue("@RandevuSaati", randevuSaati);

                        var kontrolSonuc = kontrolKomut.ExecuteScalar();

                        if (kontrolSonuc != null)
                        {
                            MessageBox.Show("Bu saatte zaten bir randevu var.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string sql = @"
                INSERT INTO randevular (MusteriID, PersonelID, RandevuTarihi, RandevuSaati, Notlar, toplamtutar)
                VALUES (@MusteriID,
                       (SELECT kullaniciid FROM kullanicilar WHERE tamad = @PersonelAdi),
                       @RandevuTarihi, @RandevuSaati, @Notlar, @ToplamTutar)";

                    using (NpgsqlCommand komut = new NpgsqlCommand(sql, baglanti))
                    {
                        komut.Parameters.AddWithValue("@MusteriID", KullaniciBilgileri.KullaniciID);
                        komut.Parameters.AddWithValue("@PersonelAdi", comboBox1.Text);
                        komut.Parameters.AddWithValue("@RandevuTarihi", randevuTarihi);
                        komut.Parameters.AddWithValue("@RandevuSaati", randevuSaati);
                        komut.Parameters.AddWithValue("@Notlar", notlar);
                        komut.Parameters.AddWithValue("@ToplamTutar", toplamTutar);

                        komut.ExecuteNonQuery();
                        MessageBox.Show("Randevu başarıyla oluşturuldu.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        PersonelRandevuDurumuGoster(comboBox1.Text);
                        dateTimePicker1_ValueChanged(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Randevu kaydetme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        // ComboBox'tan personel seçildiğinde doluluk durumunu göster
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string personelAdi = comboBox1.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(personelAdi))
            {
                PersonelRandevuDurumuGoster(personelAdi);
            }
        }

        private void notlar_txt_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
{
    dateTimePicker1.MinDate = DateTime.Now;
    comboBoxSaat.Items.Clear();

    // Önce tüm saatleri ekle
    List<string> tumSaatler = new List<string>
    {
        "09:00", "10:00", "11:00", "12:00", "14:00", "15:00", "16:00", "17:00", "18:00"
    };

    foreach (string saat in tumSaatler)
    {
        comboBoxSaat.Items.Add(saat);
    }

    if (string.IsNullOrEmpty(comboBox1.Text)) return;

    try
    {
        using (var baglanti = VeriTabaniYardimcisi.GetConnection())
        {
            baglanti.Open();
            string selectedDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            
            // DataGridView'daki randevuları kontrol et
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["RandevuTarihi"].Value != null && row.Cells["RandevuSaati"].Value != null)
                {
                    string tarih = Convert.ToDateTime(row.Cells["RandevuTarihi"].Value).ToString("yyyy-MM-dd");
                    string saat = row.Cells["RandevuSaati"].Value.ToString();

                    // Eğer tarih seçilen tarihe eşitse ve saat comboBox'ta varsa, o saati kaldır
                    if (tarih == selectedDate && comboBoxSaat.Items.Contains(saat))
                    {
                        comboBoxSaat.Items.Remove(saat);
                    }
                }
            }
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Saat seçme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}


        private void comboBoxSaat_SelectedIndexChanged(object sender, EventArgs e)
        {
            {
                // Saat seçimiyle ilgili işlemleri burada gerçekleştirebilirsiniz
                string selectedSaat = comboBoxSaat.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(selectedSaat))
                {
                    // Seçilen saati işleme veya kontrol etme
                }
            }
        }

        // Hizmetleri ComboBox'ta listeleme
        private void HizmetListele()
        {
            comboBoxHizmetler.Items.Clear();
            try
            {
                using (var baglanti = VeriTabaniYardimcisi.GetConnection())
                {
                    baglanti.Open();
                    string sql = "SELECT HizmetAdi FROM Hizmetler";
                    using (NpgsqlCommand komut = new NpgsqlCommand(sql, baglanti))
                    {
                        using (NpgsqlDataReader reader = komut.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                comboBoxHizmetler.Items.Add(reader.GetString(0));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hizmet listeleme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void comboBoxHizmetler_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Seçilen hizmetin adını al
            string hizmetAdi = comboBoxHizmetler.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(hizmetAdi))
            {
                try
                {
                    using (var baglanti = VeriTabaniYardimcisi.GetConnection())
                    {
                        baglanti.Open();

                        // Hizmetin fiyatını veri tabanından sorgulama
                        string sql = "SELECT Fiyat FROM Hizmetler WHERE HizmetAdi = @HizmetAdi";

                        using (NpgsqlCommand komut = new NpgsqlCommand(sql, baglanti))
                        {
                            komut.Parameters.AddWithValue("@HizmetAdi", hizmetAdi);
                            var result = komut.ExecuteScalar();

                            // Eğer fiyat varsa, işlemleri yap
                            if (result != null)
                            {
                                decimal fiyat = Convert.ToDecimal(result);
                                hizmetfiyati.Text = fiyat.ToString("C"); // Fiyatı 'Currency' formatında göster
                            }
                            else
                            {
                                hizmetfiyati.Text = "Fiyat bulunamadı";
                                return;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hizmet fiyatı sorgulama hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void hizmetfiyati_Click(object sender, EventArgs e)
        {

        }
    }
}