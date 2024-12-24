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

namespace KuaforRandevu.formlar
{
    public partial class GiderEkle : UserControl
    {
        public GiderEkle()
        {
            InitializeComponent();
        }

        private void GiderEkle_Load(object sender, EventArgs e)
        {
            LoadGiderTurleri();
            LoadGiderListesi();
        }

        private void LoadGiderTurleri()
        {
            comboBox1.Items.Clear(); // ComboBox'ı temizle
            using (var connection = VeriTabaniYardimcisi.GetConnection())
            {
                connection.Open();
                var command = new NpgsqlCommand("SELECT GiderTurleriID, GiderAdi FROM GiderTurleri", connection);
                using (var reader = command.ExecuteReader()) // using ifadesi ekledim
                {
                    while (reader.Read())
                    {
                        comboBox1.Items.Add(new { Text = reader.GetString(1), Value = reader.GetInt32(0) });
                    }
                }
            }
            comboBox1.DisplayMember = "Text"; // Görüntülenecek özelliği ayarla
            comboBox1.ValueMember = "Value"; // Değer özelliğini ayarla
        }

        private void LoadGiderListesi()
        {
            using (var connection = VeriTabaniYardimcisi.GetConnection())
            {
                connection.Open();
                var query = @"SELECT g.GiderID, gt.GiderAdi, g.Tutar, g.GiderTarihi
                                FROM Giderler g
                                JOIN GiderTurleri gt ON g.GiderTuruID = gt.GiderTurleriID";
                using (var adapter = new NpgsqlDataAdapter(query, connection)) // using ifadesi ekledim
                {
                    var dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string giderAdi = textBox2.Text.Trim();

            if (!string.IsNullOrEmpty(giderAdi))
            {
                using (var connection = VeriTabaniYardimcisi.GetConnection())
                {
                    connection.Open();

                    // Aynı isimde bir gider türü zaten var mı kontrol et
                    var checkCommand = new NpgsqlCommand("SELECT COUNT(*) FROM GiderTurleri WHERE GiderAdi = @GiderAdi", connection);
                    checkCommand.Parameters.AddWithValue("@GiderAdi", giderAdi);
                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                    if (count > 0)
                    {
                        MessageBox.Show("Bu gider türü zaten mevcut.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return; // Ekleme işlemini durdur
                    }
                    var command = new NpgsqlCommand("INSERT INTO GiderTurleri (GiderAdi) VALUES (@GiderAdi)", connection);
                    command.Parameters.AddWithValue("@GiderAdi", giderAdi);
                    command.ExecuteNonQuery();
                }

                LoadGiderTurleri();
                textBox2.Clear();
                MessageBox.Show("Gider türü başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lütfen geçerli bir gider adı girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Lütfen bir gider türü seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (decimal.TryParse(textBox1.Text, out decimal tutar) && comboBox1.SelectedItem != null)
            {
                using (var connection = VeriTabaniYardimcisi.GetConnection())
                {
                    connection.Open();
                    var selectedItem = (dynamic)comboBox1.SelectedItem; // Doğru cast
                    int giderTuruID = selectedItem.Value;

                    var command = new NpgsqlCommand("INSERT INTO Giderler (GiderTuruID, Tutar, GiderTarihi) VALUES (@GiderTuruID, @Tutar, NOW())", connection);
                    command.Parameters.AddWithValue("@GiderTuruID", giderTuruID);
                    command.Parameters.AddWithValue("@Tutar", tutar);
                    command.ExecuteNonQuery();

                    var islemCommand = new NpgsqlCommand("INSERT INTO MuhasebeIslemleri (GiderID) VALUES (currval('Giderler_GiderID_seq'))", connection);
                    islemCommand.ExecuteNonQuery();
                }
                LoadGiderListesi();
                textBox1.Clear();
                MessageBox.Show("Gider başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lütfen geçerli bir tutar girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
