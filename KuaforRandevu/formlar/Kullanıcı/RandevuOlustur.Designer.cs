namespace KuaforRandevu
{
    partial class RandevuOlustur
    {
        /// <summary> 
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Bileşen Tasarımcısı üretimi kod

        /// <summary> 
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.kaydet_button = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.notlar_txt = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.comboBoxSaat = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxHizmetler = new System.Windows.Forms.ComboBox();
            this.hizmetfiyati = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // kaydet_button
            // 
            this.kaydet_button.BackColor = System.Drawing.Color.SlateBlue;
            this.kaydet_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.kaydet_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kaydet_button.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.kaydet_button.Location = new System.Drawing.Point(381, 92);
            this.kaydet_button.Name = "kaydet_button";
            this.kaydet_button.Size = new System.Drawing.Size(75, 33);
            this.kaydet_button.TabIndex = 23;
            this.kaydet_button.Text = "Kaydet";
            this.kaydet_button.UseVisualStyleBackColor = false;
            this.kaydet_button.Click += new System.EventHandler(this.kaydet_button_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(406, 12);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(184, 20);
            this.dateTimePicker1.TabIndex = 20;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // notlar_txt
            // 
            this.notlar_txt.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.notlar_txt.Location = new System.Drawing.Point(93, 54);
            this.notlar_txt.Name = "notlar_txt";
            this.notlar_txt.Size = new System.Drawing.Size(183, 27);
            this.notlar_txt.TabIndex = 19;
            this.notlar_txt.TextChanged += new System.EventHandler(this.notlar_txt_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(294, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 18);
            this.label5.TabIndex = 27;
            this.label5.Text = "Randevu saati:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(294, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 18);
            this.label4.TabIndex = 26;
            this.label4.Text = "Randevu tarihi:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(10, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 18);
            this.label2.TabIndex = 24;
            this.label2.Text = "Notlar:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(10, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 18);
            this.label1.TabIndex = 28;
            this.label1.Text = "Personel";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(92, 12);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(156, 21);
            this.comboBox1.TabIndex = 30;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(13, 140);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(577, 230);
            this.dataGridView1.TabIndex = 31;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // comboBoxSaat
            // 
            this.comboBoxSaat.FormattingEnabled = true;
            this.comboBoxSaat.Location = new System.Drawing.Point(406, 51);
            this.comboBoxSaat.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBoxSaat.Name = "comboBoxSaat";
            this.comboBoxSaat.Size = new System.Drawing.Size(92, 21);
            this.comboBoxSaat.TabIndex = 32;
            this.comboBoxSaat.SelectedIndexChanged += new System.EventHandler(this.comboBoxSaat_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 92);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 33;
            this.label3.Text = "Hizmet Seç";
            // 
            // comboBoxHizmetler
            // 
            this.comboBoxHizmetler.FormattingEnabled = true;
            this.comboBoxHizmetler.Location = new System.Drawing.Point(13, 107);
            this.comboBoxHizmetler.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBoxHizmetler.Name = "comboBoxHizmetler";
            this.comboBoxHizmetler.Size = new System.Drawing.Size(92, 21);
            this.comboBoxHizmetler.TabIndex = 34;
            this.comboBoxHizmetler.SelectedIndexChanged += new System.EventHandler(this.comboBoxHizmetler_SelectedIndexChanged);
            // 
            // hizmetfiyati
            // 
            this.hizmetfiyati.AutoSize = true;
            this.hizmetfiyati.Location = new System.Drawing.Point(116, 110);
            this.hizmetfiyati.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.hizmetfiyati.Name = "hizmetfiyati";
            this.hizmetfiyati.Size = new System.Drawing.Size(0, 13);
            this.hizmetfiyati.TabIndex = 36;
            // 
            // RandevuOlustur
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.hizmetfiyati);
            this.Controls.Add(this.comboBoxHizmetler);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxSaat);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.kaydet_button);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.notlar_txt);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "RandevuOlustur";
            this.Size = new System.Drawing.Size(610, 384);
            this.Load += new System.EventHandler(this.UserControl2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button kaydet_button;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.TextBox notlar_txt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox comboBoxSaat;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxHizmetler;
        private System.Windows.Forms.Label hizmetfiyati;
    }
}
