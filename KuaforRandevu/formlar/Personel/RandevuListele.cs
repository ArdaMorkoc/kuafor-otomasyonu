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
using static KuaforRandevu.Giris;

namespace KuaforRandevu
{
    public partial class RandevuListele : UserControl
    {
        private NpgsqlDataAdapter da;
        private CurrencyManager cm;
        private DataTable dt;

        public RandevuListele()
        {
            InitializeComponent();
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            string query;

            // Admin (YetkiID = 4) için tüm randevuları göster
            if (KullaniciBilgileri.YetkiID == 4)
            {
                query = @"
                SELECT 
                    r.RandevuID,
                    r.RandevuTarihi,
                    r.RandevuSaati,
                    r.Notlar,
                    r.Durum,
                    r.ToplamTutar,
                    p.TamAd AS Personel,
                    m.TamAd AS Musteri
                FROM 
                    Randevular r
                INNER JOIN 
                    Kullanicilar p ON r.PersonelID = p.KullaniciID
                INNER JOIN 
                    Kullanicilar m ON r.MusteriID = m.KullaniciID";
            }
            else
            {
                query = @"
                SELECT 
                    r.RandevuID,
                    r.RandevuTarihi,
                    r.RandevuSaati,
                    r.Notlar,
                    r.Durum,
                    r.ToplamTutar,
                    p.TamAd AS Personel,
                    m.TamAd AS Musteri
                FROM 
                    Randevular r
                INNER JOIN 
                    Kullanicilar p ON r.PersonelID = p.KullaniciID
                INNER JOIN 
                    Kullanicilar m ON r.MusteriID = m.KullaniciID
                WHERE 
                    r.PersonelID = @PersonelID";
            }

            try
            {
                using (var conn = VeriTabaniYardimcisi.GetConnection())
                {
                    conn.Open();
                    da = new NpgsqlDataAdapter(query, conn);

                    if (KullaniciBilgileri.YetkiID != 4)
                    {
                        da.SelectCommand.Parameters.AddWithValue("@PersonelID", KullaniciBilgileri.KullaniciID);
                    }

                    dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;

                    dataGridView1.Columns["RandevuID"].Visible = false;

                    dataGridView1.Columns["Personel"].HeaderText = "Personel";
                    dataGridView1.Columns["Musteri"].HeaderText = "Müşteri";
                    dataGridView1.Columns["RandevuTarihi"].HeaderText = "Tarih";
                    dataGridView1.Columns["RandevuSaati"].HeaderText = "Saat";
                    dataGridView1.Columns["Durum"].HeaderText = "Durum";
                    dataGridView1.Columns["ToplamTutar"].HeaderText = "Toplam Tutar";
                    dataGridView1.Columns["Notlar"].HeaderText = "Notlar";

                    cm = (CurrencyManager)BindingContext[dt];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri yüklenirken bir hata oluştu: " + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string secilenTarihText = maskedTextBox2.Text;

            if (!string.IsNullOrEmpty(secilenTarihText))
            {
                if (DateTime.TryParse(secilenTarihText, out DateTime secilenTarih))
                {
                    string query;

                    if (KullaniciBilgileri.YetkiID == 4)
                    {
                        query = @"
                        SELECT 
                            r.RandevuID,
                            r.RandevuTarihi,
                            r.RandevuSaati,
                            r.Notlar,
                            r.Durum,
                            r.ToplamTutar,
                            p.TamAd AS Personel,
                            m.TamAd AS Musteri
                        FROM 
                            Randevular r
                        INNER JOIN 
                            Kullanicilar p ON r.PersonelID = p.KullaniciID
                        INNER JOIN 
                            Kullanicilar m ON r.MusteriID = m.KullaniciID
                        WHERE 
                            r.RandevuTarihi::date = @secilenTarih";
                    }
                    else
                    {
                        query = @"
                        SELECT 
                            r.RandevuID,
                            r.RandevuTarihi,
                            r.RandevuSaati,
                            r.Notlar,
                            r.Durum,
                            r.ToplamTutar,
                            p.TamAd AS Personel,
                            m.TamAd AS Musteri
                        FROM 
                            Randevular r
                        INNER JOIN 
                            Kullanicilar p ON r.PersonelID = p.KullaniciID
                        INNER JOIN 
                            Kullanicilar m ON r.MusteriID = m.KullaniciID
                        WHERE 
                            r.PersonelID = @PersonelID AND
                            r.RandevuTarihi::date = @secilenTarih";
                    }

                    try
                    {
                        using (var conn = VeriTabaniYardimcisi.GetConnection())
                        {
                            conn.Open();
                            da = new NpgsqlDataAdapter(query, conn);
                            if (KullaniciBilgileri.YetkiID != 4)
                            {
                                da.SelectCommand.Parameters.AddWithValue("@PersonelID", KullaniciBilgileri.KullaniciID);
                            }
                            da.SelectCommand.Parameters.AddWithValue("@secilenTarih", secilenTarih.Date);
                            dt = new DataTable();
                            da.Fill(dt);
                            dataGridView1.DataSource = dt;
                            cm = (CurrencyManager)BindingContext[dt];
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Geçersiz tarih formatı!");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string musteriAdi = textBox1.Text;

            if (string.IsNullOrEmpty(musteriAdi))
            {
                MessageBox.Show("Lütfen bir müşteri adı girin.");
                return;
            }

            string query;

            if (KullaniciBilgileri.YetkiID == 4)
            {
                query = @"
                SELECT 
                    r.RandevuID,
                    r.RandevuTarihi,
                    r.RandevuSaati,
                    r.Notlar,
                    r.Durum,
                    r.ToplamTutar,
                    p.TamAd AS Personel,
                    m.TamAd AS Musteri
                FROM 
                    Randevular r
                INNER JOIN 
                    Kullanicilar p ON r.PersonelID = p.KullaniciID
                INNER JOIN 
                    Kullanicilar m ON r.MusteriID = m.KullaniciID
                WHERE 
                    m.TamAd ILIKE @MusteriAdi";
            }
            else
            {
                query = @"
                SELECT 
                    r.RandevuID,
                    r.RandevuTarihi,
                    r.RandevuSaati,
                    r.Notlar,
                    r.Durum,
                    r.ToplamTutar,
                    p.TamAd AS Personel,
                    m.TamAd AS Musteri
                FROM 
                    Randevular r
                INNER JOIN 
                    Kullanicilar p ON r.PersonelID = p.KullaniciID
                INNER JOIN 
                    Kullanicilar m ON r.MusteriID = m.KullaniciID
                WHERE 
                    r.PersonelID = @PersonelID AND
                    m.TamAd ILIKE @MusteriAdi";
            }

            try
            {
                using (var conn = VeriTabaniYardimcisi.GetConnection())
                {
                    conn.Open();
                    da = new NpgsqlDataAdapter(query, conn);
                    if (KullaniciBilgileri.YetkiID != 4)
                    {
                        da.SelectCommand.Parameters.AddWithValue("@PersonelID", KullaniciBilgileri.KullaniciID);
                    }
                    da.SelectCommand.Parameters.AddWithValue("@MusteriAdi", "%" + musteriAdi + "%");
                    dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    cm = (CurrencyManager)BindingContext[dt];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            LoadData();
        }

        private void onay_btn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedRandevuId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["RandevuID"].Value);

                using (var conn = VeriTabaniYardimcisi.GetConnection())
                {
                    try
                    {
                        conn.Open();
                        using (var cmd = new NpgsqlCommand("UPDATE Randevular SET Durum = @Durum WHERE RandevuID = @RandevuID", conn))
                        {
                            cmd.Parameters.AddWithValue("@Durum", "Onaylandı");
                            cmd.Parameters.AddWithValue("@RandevuID", selectedRandevuId);

                            int affected = cmd.ExecuteNonQuery();
                            if (affected > 0)
                            {
                                MessageBox.Show("Randevu başarıyla onaylandı.");
                                LoadData();
                            }
                            else
                            {
                                MessageBox.Show("Randevu onaylanamadı.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir randevu seçin.");
            }
        }

        private void tamamla_btn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedRandevuId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["RandevuID"].Value);

                using (var conn = VeriTabaniYardimcisi.GetConnection())
                {
                    try
                    {
                        conn.Open();
                        using (var cmd = new NpgsqlCommand(@"
                    UPDATE Randevular 
                    SET Durum = @Durum,
                        odemetarihi = @OdemeTarihi 
                    WHERE RandevuID = @RandevuID", conn))
                        {
                            cmd.Parameters.AddWithValue("@Durum", "Tamamlandı");
                            cmd.Parameters.AddWithValue("@OdemeTarihi", DateTime.Now);
                            cmd.Parameters.AddWithValue("@RandevuID", selectedRandevuId);

                            int affected = cmd.ExecuteNonQuery();
                            if (affected > 0)
                            {
                                MessageBox.Show("Randevu başarıyla tamamlandı.");
                                LoadData();
                            }
                            else
                            {
                                MessageBox.Show("Randevu tamamlanamadı.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir randevu seçin.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen silmek istediğiniz randevuyu seçin.");
                return;
            }

            if (MessageBox.Show("Bu randevuyu silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int selectedRandevuId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["RandevuID"].Value);

                using (var conn = VeriTabaniYardimcisi.GetConnection())
                {
                    try
                    {
                        conn.Open();
                        using (var cmd = new NpgsqlCommand("DELETE FROM Randevular WHERE RandevuID = @RandevuID", conn))
                        {
                            cmd.Parameters.AddWithValue("@RandevuID", selectedRandevuId);

                            int affected = cmd.ExecuteNonQuery();
                            if (affected > 0)
                            {
                                MessageBox.Show("Randevu başarıyla silindi.");
                                LoadData();
                            }
                            else
                            {
                                MessageBox.Show("Randevu silinemedi.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata: " + ex.Message);
                    }
                }
            }
        }

        private void RandevuListele_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void label4_Click(object sender, EventArgs e) { }
        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}