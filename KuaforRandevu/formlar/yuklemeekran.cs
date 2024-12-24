using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KuaforRandevu
{
    public partial class yuklemeekran : Form
    {
        public yuklemeekran()
        {
            InitializeComponent();
        }
    
        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            progressBar1.Increment(2); // Her tick'te %2 artır
            if (progressBar1.Value == 100)
            {
                timer1.Enabled = false; // Timer'ı durdur
                Giris giris = new Giris();
                giris.Show();
                this.Hide();
            }
        }

        private void yuklemeekran_Load(object sender, EventArgs e)
        {
            // Timer ayarları
            timer1.Interval = 40; // 50ms = 0.05 saniye (5 saniyede tam dolum için)
            timer1.Start(); // Timer'ı başlat
        }
    }
}
