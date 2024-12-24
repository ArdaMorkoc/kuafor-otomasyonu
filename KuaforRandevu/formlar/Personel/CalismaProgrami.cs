using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static KuaforRandevu.Giris;

namespace KuaforRandevu.formlar
{
    public partial class CalismaProgrami : UserControl
    {
        private NpgsqlDataAdapter da;
        private DataTable dt;
        public CalismaProgrami()
        {
            InitializeComponent();
        }

        private void CalismaProgrami_Load(object sender, EventArgs e)
        {
            // Takvimdeki ilk tarihi yükleyelim
            LoadRandevular(DateTime.Now); // Burada DateTime.Now kullanarak bugünün tarihini alıyoruz
        }
        private void LoadRandevular(DateTime selectedDate)
        {
            // Personelin kendisine ait onaylı randevuları filtreleyen SQL sorgusu
            string query = @"
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
            r.PersonelID = @PersonelID
        AND
            r.Durum = 'Onaylandı'
        AND
            r.RandevuTarihi::date = @SelectedDate";  // Seçilen tarihe göre filtrele

            try
            {
                // Veritabanı bağlantısını aç ve sorguyu çalıştır
                using (var conn = VeriTabaniYardimcisi.GetConnection())
                {
                    conn.Open();
                    da = new NpgsqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@PersonelID", KullaniciBilgileri.KullaniciID); // Kullanıcı ID'sini alıp sorguya ekliyoruz
                    da.SelectCommand.Parameters.AddWithValue("@SelectedDate", selectedDate.Date); // Seçilen tarihi sorguya ekliyoruz
                    dt = new DataTable();
                    da.Fill(dt);

                    // DataGridView'e veriyi yükleyelim
                    dataGridView1.DataSource = dt;

                    // Kolon başlıklarını düzenleyelim
                    dataGridView1.Columns["RandevuID"].Visible = false;  // RandevuID'yi gizle
                    dataGridView1.Columns["Musteri"].HeaderText = "Müşteri";
                    dataGridView1.Columns["RandevuTarihi"].HeaderText = "Tarih";
                    dataGridView1.Columns["RandevuTarihi"].DefaultCellStyle.Format = "dd.MM.yyyy"; // Yalnızca tarihi göster
                    dataGridView1.Columns["RandevuSaati"].HeaderText = "Saat";
                    dataGridView1.Columns["Durum"].HeaderText = "Durum";
                    dataGridView1.Columns["ToplamTutar"].HeaderText = "Toplam Tutar";
                    dataGridView1.Columns["Notlar"].HeaderText = "Notlar";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri yüklenirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            // Takvimden seçilen tarihi alalım
            DateTime selectedDate = e.Start;

            // Seçilen tarihe ait randevuları yükleyelim
            LoadRandevular(selectedDate);
        }
    }
}