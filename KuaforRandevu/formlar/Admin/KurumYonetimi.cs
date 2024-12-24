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

namespace KuaforRandevu.formlar
{
    public partial class KurumYonetimi : UserControl
    {
        public KurumYonetimi()
        {
            InitializeComponent();
        }

        NpgsqlConnection baglan = new NpgsqlConnection("Host=localhost;Port=5432;Database=kuafor2;Username=postgres;Password=12345");
        NpgsqlDataAdapter da;
        CurrencyManager cm;

        private NpgsqlConnection baglanti = new NpgsqlConnection("Host=localhost;Port=5432;Database=kuafor2;Username=postgres;Password=12345");
        private string connectionString = "Host=localhost;Port=5432;Database=kuafor2;Username=postgres;Password=12345";
        private void HizmetListele()
        {
            comboBoxHizmetSec.Items.Clear();
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT HizmetAdi FROM Hizmetler";
                    using (var command = new NpgsqlCommand(sql, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comboBoxHizmetSec.Items.Add(reader.GetString(0));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hizmet listeleme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBoxHizmetSec_SelectedIndexChanged(object sender, EventArgs e)
        {
            string hizmetAdi = comboBoxHizmetSec.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(hizmetAdi))
                return;

            using (var baglanti = new NpgsqlConnection(connectionString))
            {
                try
                {
                    baglanti.Open();
                    string sql = @"
                SELECT h.KategoriID, k.KategoriAdi, h.Aciklama, h.Fiyat, h.SureDakika 
                FROM Hizmetler h
                JOIN Kategoriler k ON h.KategoriID = k.KategoriID
                WHERE h.HizmetAdi = @HizmetAdi";

                    using (var komut = new NpgsqlCommand(sql, baglanti))
                    {
                        komut.Parameters.AddWithValue("@HizmetAdi", hizmetAdi);

                        using (var reader = komut.ExecuteReader())
                        {
                            if (reader.HasRows) // Veri var mı kontrol et
                            {
                                if (reader.Read())
                                {
                                    comboBoxKategoriGuncelle.Text = reader.IsDBNull(1) ? "" : reader.GetString(1);
                                    textBox7.Text = reader.IsDBNull(2) ? "" : reader.GetString(2);
                                    guncelfiyat_txt.Text = reader.IsDBNull(3) ? "0" : reader.GetDecimal(3).ToString();
                                    textBox5.Text = reader.IsDBNull(4) ? "0" : reader.GetInt32(4).ToString();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Seçilen hizmet için bilgi bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                // Alanları temizle
                                comboBoxKategoriGuncelle.Text = "";
                                textBox7.Text = "";
                                guncelfiyat_txt.Text = "";
                                textBox5.Text = "";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hizmet bilgisi sorgulama hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void KurumYonetimi_Load(object sender, EventArgs e)
        {
            HizmetListele();
            KategoriListele();

            baglan.Open();
            da = new NpgsqlDataAdapter("SELECT * FROM Hizmetler", baglan);
            DataSet ds = new DataSet();
            da.Fill(ds, "HizmetlerTablosu");
            dataGridView1.DataSource = ds.Tables["HizmetlerTablosu"];
            cm = (CurrencyManager)BindingContext[ds.Tables["HizmetlerTablosu"]];
            baglan.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guncelle_btn_Click(object sender, EventArgs e)
        {
            string hizmetAdi = comboBoxHizmetSec.SelectedItem?.ToString();
            string yeniFiyatText = guncelfiyat_txt.Text;
            string yeniKategori = comboBoxKategoriGuncelle.Text;
            string yeniAciklama = textBox7.Text;
            string yeniSureText = textBox5.Text;

            if (string.IsNullOrEmpty(hizmetAdi) || string.IsNullOrEmpty(yeniFiyatText) || string.IsNullOrEmpty(yeniKategori) || string.IsNullOrEmpty(yeniSureText))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(yeniFiyatText, out decimal yeniFiyat) || !int.TryParse(yeniSureText, out int yeniSure))
            {
                MessageBox.Show("Geçerli fiyat ve süre giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (baglanti.State != ConnectionState.Open)
                    baglanti.Open();

                // KategoriAdi'yi KategoriID'ye dönüştürmek için kategori ID'sini alıyoruz
                string kategoriIDQuery = "SELECT KategoriID FROM Kategoriler WHERE KategoriAdi = @KategoriAdi";
                int kategoriID = 0;

                using (NpgsqlCommand kategoriKomut = new NpgsqlCommand(kategoriIDQuery, baglanti))
                {
                    kategoriKomut.Parameters.AddWithValue("@KategoriAdi", yeniKategori);

                    object result = kategoriKomut.ExecuteScalar();
                    if (result != null)
                    {
                        kategoriID = Convert.ToInt32(result);
                    }
                    else
                    {
                        MessageBox.Show("Geçerli bir kategori seçin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                string sql = @"
            UPDATE Hizmetler 
            SET Fiyat = @YeniFiyat, KategoriID = @YeniKategoriID, Aciklama = @YeniAciklama, SureDakika = @YeniSure
            WHERE HizmetAdi = @HizmetAdi";

                using (NpgsqlCommand komut = new NpgsqlCommand(sql, baglanti))
                {
                    komut.Parameters.AddWithValue("@YeniFiyat", yeniFiyat);
                    komut.Parameters.AddWithValue("@YeniKategoriID", kategoriID);  // KategoriID
                    komut.Parameters.AddWithValue("@YeniAciklama", yeniAciklama);
                    komut.Parameters.AddWithValue("@YeniSure", yeniSure);
                    komut.Parameters.AddWithValue("@HizmetAdi", hizmetAdi);

                    int rowsAffected = komut.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Hizmet bilgileri başarıyla güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        HizmetListele(); // Listeyi yenile
                        KurumYonetimi_Load(null, null); // DataGridView'i yenile
                    }
                    else
                    {
                        MessageBox.Show("Güncelleme sırasında bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Güncelleme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (baglanti.State != ConnectionState.Closed)
                    baglanti.Close();
            }
        }

        private void KategoriListele()
        {
            comboBoxKategoriGuncelle.Items.Clear();
            comboBox1.Items.Clear(); // comboBox1'i de temizle
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT KategoriAdi FROM Kategoriler";
                    using (var command = new NpgsqlCommand(sql, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string kategoriAdi = reader.GetString(0);
                            comboBoxKategoriGuncelle.Items.Add(kategoriAdi); // KategoriAdi'ni comboBoxKategoriGuncelle'ye ekle
                            comboBox1.Items.Add(kategoriAdi); // KategoriAdi'ni comboBox1'e ekle
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kategori listeleme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void comboBoxKategoriGuncelle_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Seçilen kategoriyi al
            string kategoriGuncelle = comboBoxKategoriGuncelle.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(kategoriGuncelle))
            {
                try
                {
                    if (baglanti.State != ConnectionState.Open)
                        baglanti.Open();

                    // KategoriAdi'yi KategoriID'ye dönüştürmek için Kategoriler tablosunda sorgulama yapıyoruz
                    string kategoriIDQuery = "SELECT KategoriID FROM Kategoriler WHERE KategoriAdi = @KategoriAdi";
                    int kategoriID = 0;

                    using (NpgsqlCommand kategoriKomut = new NpgsqlCommand(kategoriIDQuery, baglanti))
                    {
                        kategoriKomut.Parameters.AddWithValue("@KategoriAdi", kategoriGuncelle);

                        object result = kategoriKomut.ExecuteScalar();
                        if (result != null)
                        {
                            kategoriID = Convert.ToInt32(result);
                        }
                        else
                        {
                            // Eğer kategori bulunamazsa bir hata mesajı gösterebiliriz, ancak fiyat gösterilmeyecek.
                            MessageBox.Show("Geçerli bir kategori seçin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Seçilen kategoriye ait hizmetlerin fiyatlarını sorgulama
                    string sql = "SELECT Fiyat FROM Hizmetler WHERE KategoriID = @KategoriID";

                    using (NpgsqlCommand komut = new NpgsqlCommand(sql, baglanti))
                    {
                        komut.Parameters.AddWithValue("@KategoriID", kategoriID);
                        var result = komut.ExecuteScalar();

                        if (result != null)
                        {
                            decimal fiyat = Convert.ToDecimal(result);
                            // Burada fiyatı kullanmak için ek bir işlem yapabilirsiniz, örneğin başka bir alanda kullanma
                            // Fiyatı bir değişkende saklayabilir veya başka bir işlem yapabilirsiniz, ancak MessageBox göstermiyoruz.
                        }
                        else
                        {
                            // Fiyat bulunamadığında, yine de kullanıcıya bilgi vermek isterseniz burada hata mesajı verebilirsiniz.
                            // Fakat MessageBox yerine başka bir işlem yapılabilir.
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Hata mesajı yine gösterilebilir.
                    MessageBox.Show($"Hizmet fiyatı sorgulama hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (baglanti.State != ConnectionState.Closed)
                        baglanti.Close();
                }
            }
        }



        private void yenikategoriekle_btn_Click(object sender, EventArgs e)
        {
            // Yeni kategori adını al
            string yeniKategoriAdi = yenikategoriadi_txt.Text.Trim();

            // Kategori adının boş olmadığını kontrol et
            if (string.IsNullOrEmpty(yeniKategoriAdi))
            {
                MessageBox.Show("Lütfen yeni kategori adını girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kategorinin veritabanında zaten var olup olmadığını kontrol et
            try
            {
                if (baglanti.State != ConnectionState.Open)
                    baglanti.Open();

                string kontrolSql = "SELECT COUNT(*) FROM Kategoriler WHERE KategoriAdi = @KategoriAdi";

                using (NpgsqlCommand kontrolKomut = new NpgsqlCommand(kontrolSql, baglanti))
                {
                    kontrolKomut.Parameters.AddWithValue("@KategoriAdi", yeniKategoriAdi);
                    int kategoriVarMi = Convert.ToInt32(kontrolKomut.ExecuteScalar());

                    if (kategoriVarMi > 0)
                    {
                        MessageBox.Show("Bu kategori zaten mevcut.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kategori kontrol hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                if (baglanti.State != ConnectionState.Closed)
                    baglanti.Close();
            }

            // Yeni kategoriyi ekleme
            try
            {
                if (baglanti.State != ConnectionState.Open)
                    baglanti.Open();

                string sql = "INSERT INTO Kategoriler (KategoriAdi) VALUES (@KategoriAdi)";

                using (NpgsqlCommand komut = new NpgsqlCommand(sql, baglanti))
                {
                    komut.Parameters.AddWithValue("@KategoriAdi", yeniKategoriAdi);

                    int rowsAffected = komut.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Yeni kategori başarıyla eklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        yenikategoriadi_txt.Clear(); // TextBox'ı temizle
                        KategoriListele(); // Kategori listesini yenile
                    }
                    else
                    {
                        MessageBox.Show("Kategori ekleme sırasında bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kategori ekleme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (baglanti.State != ConnectionState.Closed)
                    baglanti.Close();
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {
            // Form yüklenirken kategorileri doldur
            KategoriComboBoxDoldur();
        }

        private void KategoriComboBoxDoldur()
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT KategoriAdi FROM Kategoriler ORDER BY KategoriAdi";

                    using (var command = new NpgsqlCommand(sql, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        comboBox1.Items.Clear();
                        while (reader.Read())
                        {
                            comboBox1.Items.Add(reader.GetString(0));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kategori listesi yükleme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Kullanıcıdan alınan bilgileri değişkenlere atayın
            string hizmetAdi = textBox1.Text.Trim();
            string aciklama = textBox2.Text.Trim();
            decimal fiyat;
            int sureDakika;

            // Giriş kontrolleri
            if (string.IsNullOrEmpty(hizmetAdi))
            {
                MessageBox.Show("Lütfen hizmet adını girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kategori seçim kontrolü
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Lütfen bir kategori seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int kategoriID;
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT KategoriID FROM Kategoriler WHERE KategoriAdi = @kategoriAdi", conn))
                {
                    cmd.Parameters.AddWithValue("@kategoriAdi", comboBox1.Text);
                    var result = cmd.ExecuteScalar();
                    if (result == null)
                    {
                        MessageBox.Show("Seçili kategori bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    kategoriID = Convert.ToInt32(result);
                }
            }

            if (!decimal.TryParse(textBox3.Text, out fiyat))
            {
                MessageBox.Show("Geçerli bir fiyat girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(textBox4.Text, out sureDakika))
            {
                MessageBox.Show("Geçerli bir süre girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var baglanti = new NpgsqlConnection(connectionString))
                {
                    baglanti.Open();

                    // Önce hizmet adının benzersiz olup olmadığını kontrol et
                    string kontrolSql = "SELECT COUNT(*) FROM Hizmetler WHERE HizmetAdi = @HizmetAdi";
                    using (var kontrolKomut = new NpgsqlCommand(kontrolSql, baglanti))
                    {
                        kontrolKomut.Parameters.AddWithValue("@HizmetAdi", hizmetAdi);
                        int varOlanHizmet = Convert.ToInt32(kontrolKomut.ExecuteScalar());
                        if (varOlanHizmet > 0)
                        {
                            MessageBox.Show("Bu hizmet adı zaten kullanılıyor.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Hizmeti veritabanına ekle
                    string sql = @"INSERT INTO Hizmetler 
                         (HizmetAdi, KategoriID, Aciklama, Fiyat, SureDakika) 
                         VALUES (@HizmetAdi, @KategoriID, @Aciklama, @Fiyat, @SureDakika)";

                    using (var komut = new NpgsqlCommand(sql, baglanti))
                    {
                        komut.Parameters.AddWithValue("@HizmetAdi", hizmetAdi);
                        komut.Parameters.AddWithValue("@KategoriID", kategoriID);
                        komut.Parameters.AddWithValue("@Aciklama", string.IsNullOrEmpty(aciklama) ? DBNull.Value : (object)aciklama);
                        komut.Parameters.AddWithValue("@Fiyat", fiyat);
                        komut.Parameters.AddWithValue("@SureDakika", sureDakika);

                        int rowsAffected = komut.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Yeni hizmet başarıyla eklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Form elemanlarını temizle
                            textBox1.Clear();
                            textBox2.Clear();
                            textBox3.Clear();
                            textBox4.Clear();
                            comboBox1.SelectedIndex = -1;

                            // Varsa hizmet listesini güncelle
                            HizmetListele();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hizmet ekleme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
