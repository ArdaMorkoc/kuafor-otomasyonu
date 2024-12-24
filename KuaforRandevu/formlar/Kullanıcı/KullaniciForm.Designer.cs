namespace KuaforRandevu
{
    partial class KullaniciForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KullaniciForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.musteriadi_lbl = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.muhasebedon_btn = new System.Windows.Forms.Button();
            this.personeldon_btn = new System.Windows.Forms.Button();
            this.admindon_btn = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.anasayfa = new System.Windows.Forms.Button();
            this.cikisyap = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.hakkimizda1 = new KuaforRandevu.formlar.Kullanıcı.Hakkimizda();
            this.karsilama1 = new KuaforRandevu.formlar.karsilama();
            this.randevularim1 = new KuaforRandevu.formlar.Kullanıcı.Randevularim();
            this.bilgilerimiDuzenle = new KuaforRandevu.formlar.BilgilerimiDuzenle();
            this.RandevuOlustur = new KuaforRandevu.RandevuOlustur();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SlateBlue;
            this.panel1.Controls.Add(this.musteriadi_lbl);
            this.panel1.Controls.Add(this.button6);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.anasayfa);
            this.panel1.Controls.Add(this.cikisyap);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(256, 565);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // musteriadi_lbl
            // 
            this.musteriadi_lbl.AutoSize = true;
            this.musteriadi_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.musteriadi_lbl.Location = new System.Drawing.Point(83, 126);
            this.musteriadi_lbl.Name = "musteriadi_lbl";
            this.musteriadi_lbl.Size = new System.Drawing.Size(167, 32);
            this.musteriadi_lbl.TabIndex = 36;
            this.musteriadi_lbl.Text = "Müşteri Adı";
            // 
            // button6
            // 
            this.button6.FlatAppearance.BorderSize = 0;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button6.Image = global::KuaforRandevu.Properties.Resources.hakkimizda;
            this.button6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button6.Location = new System.Drawing.Point(0, 401);
            this.button6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(256, 46);
            this.button6.TabIndex = 35;
            this.button6.Text = "    Hakkımızda";
            this.button6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click_1);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.admindon_btn);
            this.groupBox1.Controls.Add(this.muhasebedon_btn);
            this.groupBox1.Controls.Add(this.personeldon_btn);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox1.Location = new System.Drawing.Point(47, 470);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(164, 66);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Eski Profile Dönme";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // muhasebedon_btn
            // 
            this.muhasebedon_btn.FlatAppearance.BorderSize = 0;
            this.muhasebedon_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.muhasebedon_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.muhasebedon_btn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.muhasebedon_btn.Image = global::KuaforRandevu.Properties.Resources.icons8_calculator_24__1_;
            this.muhasebedon_btn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.muhasebedon_btn.Location = new System.Drawing.Point(55, 18);
            this.muhasebedon_btn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.muhasebedon_btn.Name = "muhasebedon_btn";
            this.muhasebedon_btn.Size = new System.Drawing.Size(47, 41);
            this.muhasebedon_btn.TabIndex = 28;
            this.muhasebedon_btn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.muhasebedon_btn.UseVisualStyleBackColor = false;
            this.muhasebedon_btn.Click += new System.EventHandler(this.muhasebedon_btn_Click);
            // 
            // personeldon_btn
            // 
            this.personeldon_btn.FlatAppearance.BorderSize = 0;
            this.personeldon_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.personeldon_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.personeldon_btn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.personeldon_btn.Image = global::KuaforRandevu.Properties.Resources.personel;
            this.personeldon_btn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.personeldon_btn.Location = new System.Drawing.Point(55, 18);
            this.personeldon_btn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.personeldon_btn.Name = "personeldon_btn";
            this.personeldon_btn.Size = new System.Drawing.Size(47, 41);
            this.personeldon_btn.TabIndex = 29;
            this.personeldon_btn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.personeldon_btn.UseVisualStyleBackColor = false;
            this.personeldon_btn.Click += new System.EventHandler(this.personeldon_btn_Click);
            // 
            // admindon_btn
            // 
            this.admindon_btn.FlatAppearance.BorderSize = 0;
            this.admindon_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.admindon_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.admindon_btn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.admindon_btn.Image = global::KuaforRandevu.Properties.Resources.admin;
            this.admindon_btn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.admindon_btn.Location = new System.Drawing.Point(55, 18);
            this.admindon_btn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.admindon_btn.Name = "admindon_btn";
            this.admindon_btn.Size = new System.Drawing.Size(47, 41);
            this.admindon_btn.TabIndex = 30;
            this.admindon_btn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.admindon_btn.UseVisualStyleBackColor = false;
            this.admindon_btn.Click += new System.EventHandler(this.admindon_btn_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Location = new System.Drawing.Point(33, 234);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 1);
            this.panel2.TabIndex = 30;
            // 
            // button5
            // 
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button5.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button5.Image = global::KuaforRandevu.Properties.Resources.icons8_business_30__1_;
            this.button5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button5.Location = new System.Drawing.Point(0, 298);
            this.button5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(256, 46);
            this.button5.TabIndex = 28;
            this.button5.Text = "    Randevularım";
            this.button5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.SlateBlue;
            this.label1.Location = new System.Drawing.Point(13, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 16);
            this.label1.TabIndex = 25;
            this.label1.Text = "label1";
            this.label1.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // anasayfa
            // 
            this.anasayfa.FlatAppearance.BorderSize = 0;
            this.anasayfa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.anasayfa.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.anasayfa.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.anasayfa.Image = global::KuaforRandevu.Properties.Resources.icons8_home_page_50;
            this.anasayfa.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.anasayfa.Location = new System.Drawing.Point(51, 176);
            this.anasayfa.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.anasayfa.Name = "anasayfa";
            this.anasayfa.Size = new System.Drawing.Size(51, 41);
            this.anasayfa.TabIndex = 27;
            this.anasayfa.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.anasayfa.UseVisualStyleBackColor = false;
            this.anasayfa.Click += new System.EventHandler(this.anasayfa_Click);
            // 
            // cikisyap
            // 
            this.cikisyap.FlatAppearance.BorderSize = 0;
            this.cikisyap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cikisyap.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cikisyap.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cikisyap.Image = global::KuaforRandevu.Properties.Resources.icons8_exit_button_50;
            this.cikisyap.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cikisyap.Location = new System.Drawing.Point(152, 176);
            this.cikisyap.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cikisyap.Name = "cikisyap";
            this.cikisyap.Size = new System.Drawing.Size(51, 41);
            this.cikisyap.TabIndex = 26;
            this.cikisyap.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cikisyap.UseVisualStyleBackColor = false;
            this.cikisyap.Click += new System.EventHandler(this.cikisyap_Click);
            // 
            // button4
            // 
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button4.Image = global::KuaforRandevu.Properties.Resources.icons8_contact_info_30;
            this.button4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button4.Location = new System.Drawing.Point(0, 350);
            this.button4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(259, 41);
            this.button4.TabIndex = 24;
            this.button4.Text = " Bilgilerimi düzenle";
            this.button4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::KuaforRandevu.Properties.Resources.u902759511611;
            this.pictureBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBox1.Location = new System.Drawing.Point(80, 14);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(96, 96);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 22;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Image = global::KuaforRandevu.Properties.Resources.icons8_calendar_30;
            this.button1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button1.Location = new System.Drawing.Point(0, 246);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(256, 41);
            this.button1.TabIndex = 23;
            this.button1.Text = " Randevu Oluştur";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Image = global::KuaforRandevu.Properties.Resources.minimizer;
            this.button3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button3.Location = new System.Drawing.Point(1013, 12);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(41, 28);
            this.button3.TabIndex = 21;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Image = global::KuaforRandevu.Properties.Resources.cikis;
            this.button2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button2.Location = new System.Drawing.Point(1061, 12);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(41, 28);
            this.button2.TabIndex = 20;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // hakkimizda1
            // 
            this.hakkimizda1.Location = new System.Drawing.Point(353, 25);
            this.hakkimizda1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.hakkimizda1.Name = "hakkimizda1";
            this.hakkimizda1.Size = new System.Drawing.Size(643, 492);
            this.hakkimizda1.TabIndex = 26;
            // 
            // karsilama1
            // 
            this.karsilama1.BackColor = System.Drawing.Color.Transparent;
            this.karsilama1.Location = new System.Drawing.Point(275, 12);
            this.karsilama1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.karsilama1.Name = "karsilama1";
            this.karsilama1.Size = new System.Drawing.Size(753, 542);
            this.karsilama1.TabIndex = 24;
            this.karsilama1.Load += new System.EventHandler(this.karsilama1_Load);
            // 
            // randevularim1
            // 
            this.randevularim1.Location = new System.Drawing.Point(275, 58);
            this.randevularim1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.randevularim1.Name = "randevularim1";
            this.randevularim1.Size = new System.Drawing.Size(813, 473);
            this.randevularim1.TabIndex = 25;
            // 
            // bilgilerimiDuzenle
            // 
            this.bilgilerimiDuzenle.KullaniciId = 0;
            this.bilgilerimiDuzenle.Location = new System.Drawing.Point(321, 70);
            this.bilgilerimiDuzenle.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bilgilerimiDuzenle.Name = "bilgilerimiDuzenle";
            this.bilgilerimiDuzenle.Size = new System.Drawing.Size(639, 441);
            this.bilgilerimiDuzenle.TabIndex = 23;
            this.bilgilerimiDuzenle.Load += new System.EventHandler(this.bilgilerimiDuzenle_Load);
            // 
            // RandevuOlustur
            // 
            this.RandevuOlustur.Location = new System.Drawing.Point(275, 60);
            this.RandevuOlustur.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RandevuOlustur.Name = "RandevuOlustur";
            this.RandevuOlustur.Size = new System.Drawing.Size(805, 471);
            this.RandevuOlustur.TabIndex = 22;
            this.RandevuOlustur.Load += new System.EventHandler(this.RandevuOlustur_Load);
            // 
            // KullaniciForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1115, 554);
            this.Controls.Add(this.hakkimizda1);
            this.Controls.Add(this.karsilama1);
            this.Controls.Add(this.randevularim1);
            this.Controls.Add(this.bilgilerimiDuzenle);
            this.Controls.Add(this.RandevuOlustur);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "KullaniciForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KullaniciForm";
            this.Load += new System.EventHandler(this.KullaniciForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private RandevuOlustur RandevuOlustur;
        private formlar.BilgilerimiDuzenle bilgilerimiDuzenle;
        private formlar.karsilama karsilama1;
        private System.Windows.Forms.Button cikisyap;
        private System.Windows.Forms.Button anasayfa;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button5;
        private formlar.Kullanıcı.Randevularim randevularim1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button muhasebedon_btn;
        private System.Windows.Forms.Button personeldon_btn;
        private System.Windows.Forms.Button admindon_btn;
        private System.Windows.Forms.Button button6;
        private formlar.Kullanıcı.Hakkimizda hakkimizda1;
        private System.Windows.Forms.Label musteriadi_lbl;
    }
}