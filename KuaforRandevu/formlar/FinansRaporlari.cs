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
    public partial class FinansRaporlari : UserControl
    {
        public FinansRaporlari()
        {
            InitializeComponent();
        }

        private void FinansRaporlari_Load(object sender, EventArgs e)
        {
            // Form yüklendiğinde verileri ilk kez yükle
            YenileVerileri();
        }

        private void YenileVerileri()
        {
            LoadGelirTablosu();
            LoadGiderTablosu();
            HesaplaCiroVeKar();
        }

        private void LoadGelirTablosu()
        {
            using (var connection = VeriTabaniYardimcisi.GetConnection())
            {
                connection.Open();
                var query = @"SELECT r.RandevuID, r.ToplamTutar AS Tutar, r.OdemeTarihi, 
                            k.TamAd AS MusteriAdi, p.TamAd AS PersonelAdi
                     FROM Randevular r
                     JOIN Kullanicilar k ON r.MusteriID = k.KullaniciID
                     JOIN Kullanicilar p ON r.PersonelID = p.KullaniciID
                     WHERE r.durum = 'Tamamlandı'";  // Sadece onaylanmış randevuları getir

                using (var adapter = new NpgsqlDataAdapter(query, connection))
                using (var dataTable = new DataTable())
                {
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
            }
        }

        private void LoadGiderTablosu()
        {
            using (var connection = VeriTabaniYardimcisi.GetConnection())
            {
                connection.Open();
                var query = @"SELECT g.GiderID, gt.GiderAdi, g.Tutar, g.GiderTarihi
                              FROM Giderler g
                              JOIN GiderTurleri gt ON g.GiderTuruID = gt.GiderTurleriID";
                using (var adapter = new NpgsqlDataAdapter(query, connection)) // using ifadesi eklendi
                using (var dataTable = new DataTable()) // using ifadesi eklendi
                {
                    adapter.Fill(dataTable);
                    dataGridView2.DataSource = dataTable;
                }
            }
        }

        private void HesaplaCiroVeKar()
        {
            using (var connection = VeriTabaniYardimcisi.GetConnection())
            {
                connection.Open();
                var gelirQuery = @"SELECT SUM(ToplamTutar) FROM Randevular WHERE durum = 'Tamamlandı'";
                using (var gelirCommand = new NpgsqlCommand(gelirQuery, connection))
                {
                    var totalGelir = gelirCommand.ExecuteScalar();
                    var giderQuery = @"SELECT SUM(Tutar) FROM Giderler";
                    using (var giderCommand = new NpgsqlCommand(giderQuery, connection))
                    {
                        var totalGider = giderCommand.ExecuteScalar();
                        decimal totalCiro = totalGelir != DBNull.Value ? Convert.ToDecimal(totalGelir) : 0;
                        decimal totalKar = totalCiro - (totalGider != DBNull.Value ? Convert.ToDecimal(totalGider) : 0);

                        DataTable dataTable = new DataTable();
                        dataTable.Columns.Add("Ciro");
                        dataTable.Columns.Add("Kar");
                        var row = dataTable.NewRow();
                        row["Ciro"] = totalCiro;
                        row["Kar"] = totalKar;
                        dataTable.Rows.Add(row);
                        dataGridView3.DataSource = dataTable;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            YenileVerileri();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
