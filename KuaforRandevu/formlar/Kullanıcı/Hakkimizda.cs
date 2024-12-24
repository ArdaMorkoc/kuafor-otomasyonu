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

namespace KuaforRandevu.formlar.Kullanıcı
{
    public partial class Hakkimizda : UserControl
    {
        public Hakkimizda()
        {
            InitializeComponent();
        }

        private void Hakkimizda_Load(object sender, EventArgs e)
        {
            try
            {
                // Hizmetleri yükleme kodu
                using (var baglanti = VeriTabaniYardimcisi.GetConnection())
                {
                    baglanti.Open();
                    string sql = "SELECT HizmetAdi FROM Hizmetler";

                    using (NpgsqlCommand komut = new NpgsqlCommand(sql, baglanti))
                    {
                        using (NpgsqlDataReader dr = komut.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                comboBoxHizmetler.Items.Add(dr["HizmetAdi"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veri yüklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void comboBoxHizmetler_SelectedIndexChanged(object sender, EventArgs e)
        {
            string hizmetAdi = comboBoxHizmetler.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(hizmetAdi))
            {
                try
                {
                    using (var baglanti = VeriTabaniYardimcisi.GetConnection())
                    {
                        baglanti.Open();

                        // Hizmet bilgilerini sorgulayan SQL
                        string sql = "SELECT HizmetAdi, Aciklama, Fiyat FROM Hizmetler WHERE HizmetAdi = @HizmetAdi";

                        using (NpgsqlCommand komut = new NpgsqlCommand(sql, baglanti))
                        {
                            komut.Parameters.AddWithValue("@HizmetAdi", hizmetAdi);
                            using (NpgsqlDataReader reader = komut.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    // Hizmet bilgilerini ilgili kontrollere yansıt
                                    label6.Text = reader["HizmetAdi"].ToString(); // Hizmet Adı
                                    richTextBox1.Text = reader["Aciklama"].ToString(); // Açıklama
                                    button3.Text = Convert.ToDecimal(reader["Fiyat"]).ToString("C"); // Fiyat (Currency format)
                                }
                                else
                                {
                                    // Veri bulunamazsa
                                    label6.Text = "-";
                                    richTextBox1.Text = "Bilgi bulunamadı";
                                    button3.Text = "Fiyat yok";
                                }
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
    }
}
