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

namespace KuaforRandevu.formlar.Kullanıcı
{
    public partial class Randevularim : UserControl
    {
        private NpgsqlDataAdapter da;
        private CurrencyManager cm;
        private DataTable dt;

        public void RefreshRandevular()
        {
            LoadData();
        }
        public Randevularim()
        {
            InitializeComponent();
        }

        private void Randevularim_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
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
                    m.KullaniciID = @MusteriID;";

            try
            {
                using (var conn = VeriTabaniYardimcisi.GetConnection())
                {
                    conn.Open();
                    da = new NpgsqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@MusteriID", KullaniciBilgileri.KullaniciID);
                    dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;

                    // Musteri ve RandevuID kolonlarını gizle
                    dataGridView1.Columns["Musteri"].Visible = false;
                    dataGridView1.Columns["RandevuID"].Visible = false;

                    // İsterseniz kolon başlıklarını Türkçeleştirebiliriz
                    dataGridView1.Columns["Personel"].HeaderText = "Personel";
                    dataGridView1.Columns["RandevuTarihi"].HeaderText = "Tarih";
                    dataGridView1.Columns["RandevuTarihi"].DefaultCellStyle.Format = "dd.MM.yyyy"; // Sadece tarih gösterir

                    dataGridView1.Columns["RandevuSaati"].HeaderText = "Saat";
                    dataGridView1.Columns["Durum"].HeaderText = "Durum";
                    dataGridView1.Columns["ToplamTutar"].HeaderText = "Toplam Tutar";
                    dataGridView1.Columns["Notlar"].HeaderText = "Notlar";
                    

                    cm = (CurrencyManager)BindingContext[dt];
                    UpdateNavigationLabel();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri yüklenirken bir hata oluştu: " + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cm.Position = 0;
            UpdateNavigationLabel();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cm.Position--;
            UpdateNavigationLabel();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cm.Position++;
            UpdateNavigationLabel();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cm.Position = cm.Count - 1;
            UpdateNavigationLabel();
        }

        private void UpdateNavigationLabel()
        {
            if (cm != null && cm.Count > 0)
            {
                label1.Text = (cm.Position + 1) + " / " + cm.Count;
            }
            else
            {
                label1.Text = "0 / 0";
            }
        }

        private void iptal_btn_Click(object sender, EventArgs e)
        {
            // DataGridView'de seçili satırı al
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Seçili satırdaki RandevuID değerini al
                int selectedRandevuId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["RandevuID"].Value);

                // Güncelleme sorgusu
                string updateQuery = "UPDATE Randevular SET Durum = @Durum WHERE RandevuID = @RandevuID";

                try
                {
                    using (var baglanti = VeriTabaniYardimcisi.GetConnection())
                    {
                        baglanti.Open();

                        using (var command = new NpgsqlCommand(updateQuery, baglanti))
                        {
                            // Parametreleri ekle
                            command.Parameters.AddWithValue("@Durum", "İptal Edildi");
                            command.Parameters.AddWithValue("@RandevuID", selectedRandevuId);

                            // Sorguyu çalıştır
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Randevunuz İptal Edildi.");
                                // Listeyi güncelle
                                RefreshDataGridView();
                            }
                            else
                            {
                                MessageBox.Show("Randevunuz İptal Edildi.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir randevu seçin.");
            }
        }

        private void RefreshDataGridView()
        {
            try
            {
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tablo yenilenirken bir hata oluştu: " + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void yenile_btn_Click(object sender, EventArgs e)
        {
            try
            {
                // DataGridView'i yenile
                RefreshDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tablo yenilenirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}