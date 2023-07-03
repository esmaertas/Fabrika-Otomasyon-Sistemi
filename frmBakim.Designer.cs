
namespace BakimTakipProgrami
{
    partial class frmBakim
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnKaydet = new System.Windows.Forms.Button();
            this.btnYeni = new System.Windows.Forms.Button();
            this.browser = new System.Windows.Forms.WebBrowser();
            this.label14 = new System.Windows.Forms.Label();
            this.txtYapilanlar = new System.Windows.Forms.TextBox();
            this.lstArizaKodlari = new System.Windows.Forms.CheckedListBox();
            this.lstPersonel = new System.Windows.Forms.CheckedListBox();
            this.chkCozuldu = new System.Windows.Forms.CheckBox();
            this.nmDurus = new System.Windows.Forms.NumericUpDown();
            this.lblMudahaleSuresi = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblTarih = new System.Windows.Forms.Label();
            this.dtBitisSaati = new System.Windows.Forms.DateTimePicker();
            this.dtBaslamaSaati = new System.Windows.Forms.DateTimePicker();
            this.cmbVardiya = new System.Windows.Forms.ComboBox();
            this.cmbMakine = new System.Windows.Forms.ComboBox();
            this.cmbBolum = new System.Windows.Forms.ComboBox();
            this.cmbArizaTipi = new System.Windows.Forms.ComboBox();
            this.cmbTuru = new System.Windows.Forms.ComboBox();
            this.lblArizaID = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmDurus)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1112, 368);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnKaydet);
            this.groupBox1.Controls.Add(this.btnYeni);
            this.groupBox1.Controls.Add(this.browser);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txtYapilanlar);
            this.groupBox1.Controls.Add(this.lstArizaKodlari);
            this.groupBox1.Controls.Add(this.lstPersonel);
            this.groupBox1.Controls.Add(this.chkCozuldu);
            this.groupBox1.Controls.Add(this.nmDurus);
            this.groupBox1.Controls.Add(this.lblMudahaleSuresi);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.lblTarih);
            this.groupBox1.Controls.Add(this.dtBitisSaati);
            this.groupBox1.Controls.Add(this.dtBaslamaSaati);
            this.groupBox1.Controls.Add(this.cmbVardiya);
            this.groupBox1.Controls.Add(this.cmbMakine);
            this.groupBox1.Controls.Add(this.cmbBolum);
            this.groupBox1.Controls.Add(this.cmbArizaTipi);
            this.groupBox1.Controls.Add(this.cmbTuru);
            this.groupBox1.Controls.Add(this.lblArizaID);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(11, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1090, 356);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnKaydet
            // 
            this.btnKaydet.AutoSize = true;
            this.btnKaydet.BackColor = System.Drawing.Color.Red;
            this.btnKaydet.Location = new System.Drawing.Point(769, 148);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(93, 87);
            this.btnKaydet.TabIndex = 25;
            this.btnKaydet.Text = "KAYDET";
            this.btnKaydet.UseVisualStyleBackColor = false;
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);
            // 
            // btnYeni
            // 
            this.btnYeni.AutoSize = true;
            this.btnYeni.BackColor = System.Drawing.Color.Yellow;
            this.btnYeni.Location = new System.Drawing.Point(769, 46);
            this.btnYeni.Name = "btnYeni";
            this.btnYeni.Size = new System.Drawing.Size(93, 87);
            this.btnYeni.TabIndex = 25;
            this.btnYeni.Text = "YENİ";
            this.btnYeni.UseVisualStyleBackColor = false;
            this.btnYeni.Click += new System.EventHandler(this.btnYeni_Click);
            // 
            // browser
            // 
            this.browser.Location = new System.Drawing.Point(878, 46);
            this.browser.MinimumSize = new System.Drawing.Size(20, 20);
            this.browser.Name = "browser";
            this.browser.Size = new System.Drawing.Size(204, 189);
            this.browser.TabIndex = 24;
            this.browser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.browser_DocumentCompleted);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(875, 18);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(75, 17);
            this.label14.TabIndex = 23;
            this.label14.Text = "DUYURU :";
            // 
            // txtYapilanlar
            // 
            this.txtYapilanlar.Location = new System.Drawing.Point(413, 219);
            this.txtYapilanlar.Multiline = true;
            this.txtYapilanlar.Name = "txtYapilanlar";
            this.txtYapilanlar.Size = new System.Drawing.Size(350, 115);
            this.txtYapilanlar.TabIndex = 22;
            this.txtYapilanlar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtYapilanlar_KeyPress);
            // 
            // lstArizaKodlari
            // 
            this.lstArizaKodlari.FormattingEnabled = true;
            this.lstArizaKodlari.Location = new System.Drawing.Point(613, 48);
            this.lstArizaKodlari.Name = "lstArizaKodlari";
            this.lstArizaKodlari.Size = new System.Drawing.Size(150, 140);
            this.lstArizaKodlari.TabIndex = 21;
            // 
            // lstPersonel
            // 
            this.lstPersonel.FormattingEnabled = true;
            this.lstPersonel.Location = new System.Drawing.Point(413, 46);
            this.lstPersonel.Name = "lstPersonel";
            this.lstPersonel.Size = new System.Drawing.Size(150, 140);
            this.lstPersonel.TabIndex = 21;
            // 
            // chkCozuldu
            // 
            this.chkCozuldu.AutoSize = true;
            this.chkCozuldu.Location = new System.Drawing.Point(269, 311);
            this.chkCozuldu.Name = "chkCozuldu";
            this.chkCozuldu.Size = new System.Drawing.Size(97, 21);
            this.chkCozuldu.TabIndex = 20;
            this.chkCozuldu.Text = "ÇÖZÜLDÜ";
            this.chkCozuldu.UseVisualStyleBackColor = true;
            // 
            // nmDurus
            // 
            this.nmDurus.Location = new System.Drawing.Point(169, 311);
            this.nmDurus.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nmDurus.Name = "nmDurus";
            this.nmDurus.Size = new System.Drawing.Size(85, 22);
            this.nmDurus.TabIndex = 19;
            // 
            // lblMudahaleSuresi
            // 
            this.lblMudahaleSuresi.AutoSize = true;
            this.lblMudahaleSuresi.Location = new System.Drawing.Point(311, 268);
            this.lblMudahaleSuresi.Name = "lblMudahaleSuresi";
            this.lblMudahaleSuresi.Size = new System.Drawing.Size(23, 17);
            this.lblMudahaleSuresi.TabIndex = 18;
            this.lblMudahaleSuresi.Text = "---";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(266, 238);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(146, 17);
            this.label13.TabIndex = 17;
            this.label13.Text = "MÜDAHALE SÜRESİ :";
            // 
            // lblTarih
            // 
            this.lblTarih.AutoSize = true;
            this.lblTarih.Location = new System.Drawing.Point(276, 194);
            this.lblTarih.Name = "lblTarih";
            this.lblTarih.Size = new System.Drawing.Size(23, 17);
            this.lblTarih.TabIndex = 16;
            this.lblTarih.Text = "---";
            // 
            // dtBitisSaati
            // 
            this.dtBitisSaati.CustomFormat = "HH:mm";
            this.dtBitisSaati.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtBitisSaati.Location = new System.Drawing.Point(140, 268);
            this.dtBitisSaati.Name = "dtBitisSaati";
            this.dtBitisSaati.ShowUpDown = true;
            this.dtBitisSaati.Size = new System.Drawing.Size(108, 22);
            this.dtBitisSaati.TabIndex = 15;
            this.dtBitisSaati.ValueChanged += new System.EventHandler(this.dtBitisSaati_ValueChanged);
            // 
            // dtBaslamaSaati
            // 
            this.dtBaslamaSaati.CustomFormat = "HH:mm";
            this.dtBaslamaSaati.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtBaslamaSaati.Location = new System.Drawing.Point(140, 233);
            this.dtBaslamaSaati.Name = "dtBaslamaSaati";
            this.dtBaslamaSaati.ShowUpDown = true;
            this.dtBaslamaSaati.Size = new System.Drawing.Size(108, 22);
            this.dtBaslamaSaati.TabIndex = 14;
            this.dtBaslamaSaati.ValueChanged += new System.EventHandler(this.dtBaslamaSaati_ValueChanged);
            // 
            // cmbVardiya
            // 
            this.cmbVardiya.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVardiya.FormattingEnabled = true;
            this.cmbVardiya.Location = new System.Drawing.Point(117, 194);
            this.cmbVardiya.Name = "cmbVardiya";
            this.cmbVardiya.Size = new System.Drawing.Size(137, 24);
            this.cmbVardiya.TabIndex = 13;
            // 
            // cmbMakine
            // 
            this.cmbMakine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMakine.FormattingEnabled = true;
            this.cmbMakine.Location = new System.Drawing.Point(117, 159);
            this.cmbMakine.Name = "cmbMakine";
            this.cmbMakine.Size = new System.Drawing.Size(256, 24);
            this.cmbMakine.TabIndex = 13;
            // 
            // cmbBolum
            // 
            this.cmbBolum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBolum.FormattingEnabled = true;
            this.cmbBolum.Location = new System.Drawing.Point(117, 122);
            this.cmbBolum.Name = "cmbBolum";
            this.cmbBolum.Size = new System.Drawing.Size(256, 24);
            this.cmbBolum.TabIndex = 13;
            this.cmbBolum.TextChanged += new System.EventHandler(this.cmbBolum_TextChanged);
            // 
            // cmbArizaTipi
            // 
            this.cmbArizaTipi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbArizaTipi.FormattingEnabled = true;
            this.cmbArizaTipi.Location = new System.Drawing.Point(117, 85);
            this.cmbArizaTipi.Name = "cmbArizaTipi";
            this.cmbArizaTipi.Size = new System.Drawing.Size(256, 24);
            this.cmbArizaTipi.TabIndex = 13;
            this.cmbArizaTipi.TextChanged += new System.EventHandler(this.cmbArizaTipi_TextChanged);
            // 
            // cmbTuru
            // 
            this.cmbTuru.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTuru.FormattingEnabled = true;
            this.cmbTuru.Location = new System.Drawing.Point(117, 48);
            this.cmbTuru.Name = "cmbTuru";
            this.cmbTuru.Size = new System.Drawing.Size(256, 24);
            this.cmbTuru.TabIndex = 13;
            // 
            // lblArizaID
            // 
            this.lblArizaID.AutoSize = true;
            this.lblArizaID.Location = new System.Drawing.Point(115, 18);
            this.lblArizaID.Name = "lblArizaID";
            this.lblArizaID.Size = new System.Drawing.Size(23, 17);
            this.lblArizaID.TabIndex = 12;
            this.lblArizaID.Text = "---";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(406, 194);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(100, 17);
            this.label12.TabIndex = 11;
            this.label12.Text = "YAPILANLAR :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(610, 18);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(120, 17);
            this.label11.TabIndex = 10;
            this.label11.Text = "ARIZA KODLARI :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(410, 21);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(118, 17);
            this.label10.TabIndex = 9;
            this.label10.Text = "PERSONELLER :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 317);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(145, 17);
            this.label9.TabIndex = 8;
            this.label9.Text = "MAKİNE DURUŞ (dk.)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 280);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(92, 17);
            this.label8.TabIndex = 7;
            this.label8.Text = "BİTİŞ SAATİ :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 243);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(123, 17);
            this.label7.TabIndex = 6;
            this.label7.Text = "BAŞLAMA SAATİ :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 206);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 17);
            this.label6.TabIndex = 5;
            this.label6.Text = "VARDİYA :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 169);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "MAKİNE :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 132);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "BÖLÜM :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "ARIZA TİPİ :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "TÜRÜ :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "ARIZA ID :";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 368);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1112, 370);
            this.panel2.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1112, 370);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEnter);
            this.dataGridView1.Sorted += new System.EventHandler(this.dataGridView1_Sorted);
            // 
            // frmBakim
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1112, 738);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "frmBakim";
            this.Text = "BAKIM KAYITLARI FORMU";
            this.Load += new System.EventHandler(this.frmBakim_Load);
            this.Shown += new System.EventHandler(this.frmBakim_Shown);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmDurus)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnKaydet;
        private System.Windows.Forms.Button btnYeni;
        private System.Windows.Forms.WebBrowser browser;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtYapilanlar;
        private System.Windows.Forms.CheckedListBox lstArizaKodlari;
        private System.Windows.Forms.CheckedListBox lstPersonel;
        private System.Windows.Forms.CheckBox chkCozuldu;
        private System.Windows.Forms.NumericUpDown nmDurus;
        private System.Windows.Forms.Label lblMudahaleSuresi;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblTarih;
        private System.Windows.Forms.DateTimePicker dtBitisSaati;
        private System.Windows.Forms.DateTimePicker dtBaslamaSaati;
        private System.Windows.Forms.ComboBox cmbVardiya;
        private System.Windows.Forms.ComboBox cmbMakine;
        private System.Windows.Forms.ComboBox cmbBolum;
        private System.Windows.Forms.ComboBox cmbArizaTipi;
        private System.Windows.Forms.ComboBox cmbTuru;
        private System.Windows.Forms.Label lblArizaID;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}