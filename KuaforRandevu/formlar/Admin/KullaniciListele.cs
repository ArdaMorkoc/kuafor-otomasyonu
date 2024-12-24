using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace KuaforRandevu.formlar
{
    public partial class KullaniciListele : UserControl
    {
        public KullaniciListele()
        {
            InitializeComponent();
        }

        NpgsqlDataAdapter da;
        CurrencyManager cm;

        private void UserControl1_Load(object sender, EventArgs e)
        {

            // Veritabanından Yetki bilgilerini çekip comboBox1'e yükleyelim.
            using (var baglan = VeriTabaniYardimcisi.GetConnection())
            {
                baglan.Open();

                // İlk sorgu
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT YetkiID, YetkiAdi FROM Yetkiler", baglan);
                NpgsqlDataReader dr = cmd.ExecuteReader();

                Dictionary<int, string> yetkiler = new Dictionary<int, string>();
                while (dr.Read())
                {
                    yetkiler.Add(dr.GetInt32(0), dr.GetString(1));
                }

                comboBox1.DataSource = new BindingSource(yetkiler, null);
                comboBox1.DisplayMember = "Value";
                comboBox1.ValueMember = "Key";

                // İkinci sorguyu aynı bağlantı ile yapalım
                dr.Close(); // İlk DataReader'ı kapatmalıyız
                NpgsqlCommand cmd2 = new NpgsqlCommand("SELECT YetkiID, YetkiAdi FROM Yetkiler", baglan);
                NpgsqlDataReader dr2 = cmd2.ExecuteReader();

                Dictionary<int, string> yetkiler2 = new Dictionary<int, string>();
                while (dr2.Read())
                {
                    yetkiler2.Add(dr2.GetInt32(0), dr2.GetString(1));
                }

                // ComboBox2'yi doldur
                comboBox2.DataSource = new BindingSource(yetkiler2, null);
                comboBox2.DisplayMember = "Value";
                comboBox2.ValueMember = "Key";


            }
            KullaniciListeleHepsi();
        }


        // Kullanıcıları başlangıçta listeleyen metod
        private void KullaniciListeleHepsi()
{
    using (var baglan = VeriTabaniYardimcisi.GetConnection())
    {
        baglan.Open();
        string query = @"
            SELECT 
                k.KullaniciID, 
                k.TamAd, 
                k.Eposta, 
                k.Sifre,
                k.TelefonNumarasi, 
                y.YetkiAdi,
                k.KayitTarihi
            FROM 
                Kullanicilar k
            JOIN 
                Yetkiler y ON k.YetkiID = y.YetkiID";

        da = new NpgsqlDataAdapter(query, baglan);
        DataSet ds = new DataSet();
        da.Fill(ds, "KullaniciTablosu");
        dataGridView1.DataSource = ds.Tables["KullaniciTablosu"];
        cm = (CurrencyManager)BindingContext[ds.Tables["KullaniciTablosu"]];
    }

    UpdatePositionLabel();
}


        private void UpdatePositionLabel()
        {
            label1.Text = $"{cm.Position + 1} / {cm.Count}";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var baglan = VeriTabaniYardimcisi.GetConnection())
            {
                baglan.Open();
                string query;

                if (comboBox1.SelectedIndex == 0)
                    query = "SELECT * FROM Kullanicilar WHERE YetkiID = 1"; // Müşteri
                else if (comboBox1.SelectedIndex == 1)
                    query = "SELECT * FROM Kullanicilar WHERE YetkiID = 2"; // Personel
                else
                    query = "SELECT * FROM Kullanicilar"; // Tüm Kayıtlar

                da = new NpgsqlDataAdapter(query, baglan);
                DataSet ds = new DataSet();
                da.Fill(ds, "KullaniciTablosu");
                dataGridView1.DataSource = ds.Tables["KullaniciTablosu"];
                cm = (CurrencyManager)BindingContext[ds.Tables["KullaniciTablosu"]];
            }
        }

        // TABLODA HAREKET ETMEK İÇİN YÖN TUŞLARI BÖLÜMÜ
        private void button3_Click_1(object sender, EventArgs e)
        {
            cm.Position++;
            label1.Text = (cm.Position + 1) + " / " + cm.Count;
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            cm.Position = cm.Count;
            label1.Text = (cm.Position + 1) + " / " + cm.Count;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            cm.Position--;
            label1.Text = (cm.Position + 1) + " / " + cm.Count;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            cm.Position = 0;
            label1.Text = (cm.Position + 1) + " / " + cm.Count;
        }
        // ---------------------------------------------------------
        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            using (var baglan = VeriTabaniYardimcisi.GetConnection())
            {
                baglan.Open();

                // ComboBox'tan seçilen YetkiID değerini alıyoruz.
                int selectedYetkiID = ((KeyValuePair<int, string>)comboBox1.SelectedItem).Key;

                string query;

                if (selectedYetkiID == 0)
                    query = "SELECT * FROM Kullanicilar"; // Tüm Kayıtlar
                else
                    query = $"SELECT * FROM Kullanicilar WHERE YetkiID = {selectedYetkiID}";

                da = new NpgsqlDataAdapter(query, baglan);
                DataSet ds = new DataSet();
                da.Fill(ds, "KullaniciTablosu");
                dataGridView1.DataSource = ds.Tables["KullaniciTablosu"];

                cm = (CurrencyManager)BindingContext[ds.Tables["KullaniciTablosu"]];
                UpdatePositionLabel();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            string email = textBox1.Text;
            int yetkiID = ((KeyValuePair<int, string>)comboBox2.SelectedItem).Key;

            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Lütfen bir e-posta adresi girin!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var baglan = VeriTabaniYardimcisi.GetConnection())
            {
                try
                {
                    baglan.Open();

                    // Önce kullanıcının mevcut yetkisini kontrol et
                    string checkQuery = "SELECT YetkiID FROM Kullanicilar WHERE Eposta = @Eposta";
                    using (NpgsqlCommand checkCmd = new NpgsqlCommand(checkQuery, baglan))
                    {
                        checkCmd.Parameters.AddWithValue("@Eposta", email);
                        object result = checkCmd.ExecuteScalar();

                        if (result != null && (int)result == 4)
                        {
                            MessageBox.Show("Bu kullanıcının yetkisi güncellenemez!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Yetki güncellemesini gerçekleştir
                    string updateQuery = "UPDATE Kullanicilar SET YetkiID = @YetkiID WHERE Eposta = @Eposta";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(updateQuery, baglan))
                    {
                        cmd.Parameters.AddWithValue("@YetkiID", yetkiID);
                        cmd.Parameters.AddWithValue("@Eposta", email);

                        int affectedRows = cmd.ExecuteNonQuery();
                        if (affectedRows > 0)
                        {
                            MessageBox.Show("Yetki başarıyla güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("E-posta adresine sahip kullanıcı bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    KullaniciListeleHepsi();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string nameFilter = textBox2.Text;
            FilterUsersByName(nameFilter);
        }

        private void FilterUsersByName(string name)
        {
            using (var baglan = VeriTabaniYardimcisi.GetConnection())
            {
                try
                {
                    baglan.Open();
                    string query = $"SELECT * FROM Kullanicilar WHERE TamAd ILIKE '%{name}%'";
                    da = new NpgsqlDataAdapter(query, baglan);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "KullaniciTablosu");
                    dataGridView1.DataSource = ds.Tables["KullaniciTablosu"];
                    cm = (CurrencyManager)BindingContext[ds.Tables["KullaniciTablosu"]];
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            UpdatePositionLabel();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string emailFilter = textBox3.Text;
            FilterUsersByEmail(emailFilter);
        }

        private void FilterUsersByEmail(string email)
        {
            using (var baglan = VeriTabaniYardimcisi.GetConnection())
            {
                try
                {
                    baglan.Open();
                    string query = $"SELECT * FROM Kullanicilar WHERE Eposta ILIKE '%{email}%'";
                    da = new NpgsqlDataAdapter(query, baglan);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "KullaniciTablosu");
                    dataGridView1.DataSource = ds.Tables["KullaniciTablosu"];
                    cm = (CurrencyManager)BindingContext[ds.Tables["KullaniciTablosu"]];
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            UpdatePositionLabel();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}