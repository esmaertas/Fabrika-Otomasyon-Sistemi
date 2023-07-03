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

namespace BakimTakipProgrami
{
    public partial class frmPeriyodikBakim : Form
    {
        public frmPeriyodikBakim()
        {
            InitializeComponent();
        }

        public DataTable dtTablom;
        public DataTable dtMakinelerTablom;
        public DataTable dtTurTablom;
        public DataTable dtPerTablom;
        public DataTable dtVardiyalarTablom;
        public DataTable dtDonemTablom;
        public DataTable dtDonemTablom2;
        public bool personelsecim = false;
        public string sb1;
        public string sb2;
        public string kullaniciadisoyadi;
        public bool combacik = false;
        public int sec;
        public string sec2;
        public DataTable dtYilTablom;
        public bool ilk = false;
        public bool ilk2 = false;

        private void frmPeriyodikBakim_Load(object sender, EventArgs e)
        {
            dtYilTablom = YilYukle();
            kullaniciadisoyadi = frmGiris.kullanici_bilgi;

            ilk = true;
            ilk2 = true;

            if (ilk==true)
            {
                dtPerTablom = null;
                dtPerTablom = PersonelYukle();
                ilk = false;
            }

            if (ilk2== true)
            {
                dtTurTablom = null;
                dtTurTablom = TurYukle();
                dtVardiyalarTablom = null;
                dtVardiyalarTablom = VardiyalarYukle();
                dtMakinelerTablom = null;
                dtMakinelerTablom = MakineYukle();
                dtDonemTablom = null;
                dtDonemTablom = DonemYukle();
                dtDonemTablom2 = null;
                dtDonemTablom2 = DonemYukle();

                comboboxYukle();
                ilk2 = false;
            }

            combacik = true;
            veriListeleHepsi();
            this.ActiveControl = dataGridView1;
        }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            veriListeleHepsi();

            lblPeriyodilID.Text = null;
            cmbBakimTuru.SelectedIndex = -1;
            cmbDonem.SelectedIndex = -1;
            cmbVardiya.SelectedIndex = -1;
            cmbMakine.SelectedIndex = -1;

            dateTimePicker2.Value = Convert.ToDateTime("00:00");
            dateTimePicker3.Value = Convert.ToDateTime("00:00");

            chkYapildi.Checked = false;

            btnKaydet.Enabled = true;
            btnKaydet.Text = "KAYDET";

            personelseciliolmasin();
            lstPersonel.Visible = false;
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            personelsecili();

            if (btnKaydet.Text=="KAYDET" && cmbBakimTuru.SelectedIndex != -1 && cmbMakine.SelectedIndex != -1 && cmbVardiya.SelectedIndex != -1 && personelsecim != false)
            {
                SqlConnection sqlConn = new SqlConnection();
                sqlConn.ConnectionString = frmGiris.Database;
                sqlConn.Open();

                SqlCommand sqCom = new SqlCommand();
                sqCom.Connection = sqlConn;
                sqCom.CommandText =
                    "INSERT INTO bakPeriyodikBakim (Makine, Tarih, BakimTuru, Personel, Vardiya, BaslamaSaati, BitisSaati, Yapildi, Kullanici, Donem, Yapilanlar) VALUES (@Makine, @Tarih, @BakimTuru, @Personel, @Vardiya, @BaslamaSaati, @BitisSaati, @Yapildi, @Kullanici, @Donem, @Yapilanlar); SELECT SCOPE_IDENTITY()";

                sqCom.Parameters.Add("@Makine", SqlDbType.Int);
                sqCom.Parameters["@Makine"].Value = Convert.ToInt32(cmbMakine.SelectedValue);

                sqCom.Parameters.Add("@Tarih", SqlDbType.Date);
                sqCom.Parameters["@Tarih"].Value = dateTimePicker1.Value;

                sqCom.Parameters.Add("@BakimTuru", SqlDbType.Int);
                sqCom.Parameters["@BakimTuru"].Value = Convert.ToInt32(cmbBakimTuru.SelectedValue);

                sqCom.Parameters.Add("@Vardiya", SqlDbType.Int);
                sqCom.Parameters["@Vardiya"].Value = Convert.ToInt32(cmbVardiya.SelectedValue);

                string time = dateTimePicker2.Value.ToString();
                var result = Convert.ToDateTime(time);
                string test = result.ToString("HH:mm", System.Globalization.CultureInfo.InvariantCulture);

                sqCom.Parameters.Add("@BaslamaSaati", SqlDbType.Time);
                sqCom.Parameters["@BaslamaSaati"].Value = test.ToString();

                string time2 = dateTimePicker3.Value.ToString();
                var result2 = Convert.ToDateTime(time2);
                string test2 = result2.ToString("HH:mm", System.Globalization.CultureInfo.InvariantCulture);

                sqCom.Parameters.Add("@BitisSaati", SqlDbType.Time);
                sqCom.Parameters["@BitisSaati"].Value = test2.ToString();

                sqCom.Parameters.Add("@Yapildi", SqlDbType.Bit);
                sqCom.Parameters["@Yapildi"].Value = chkYapildi.Checked;

                sqCom.Parameters.Add("@Kullanici", SqlDbType.NVarChar);
                sqCom.Parameters["@Kullanici"].Value = kullaniciadisoyadi;

                sqCom.Parameters.Add("@Donem", SqlDbType.Int);
                sqCom.Parameters["@Donem"].Value = Convert.ToInt32(cmbDonem.SelectedValue);

                sqCom.Parameters.Add("@Yapilanlar", SqlDbType.NVarChar);
                sqCom.Parameters["@Yapilanlar"].Value = txtYapilanlar.Text;

                PersonelIDGuncelle();
                sqCom.Parameters.Add("@Personel", SqlDbType.NVarChar);
                sqCom.Parameters["@Personel"].Value = sb2.ToString();

                var order_id = sqCom.ExecuteScalar();

                lblPeriyodilID.Text = order_id.ToString();
                sqlConn.Close();

                veriListeleHepsi();

                cmbBakimTuru.SelectedIndex = -1;
                cmbVardiya.SelectedIndex = -1;
                cmbMakine.SelectedIndex = -1;

                txtYapilanlar.Text = null;
                chkYapildi.Checked = false;

                MessageBox.Show(order_id.ToString() + " nolu PeriyodikID ile \nYeni Kayıt Eklendi !");

                lblPeriyodilID.Text = null;
                personelseciliolmasin();

                veriListeleHepsi();
            }
            else if (btnKaydet.Text=="GÜNCELLE")
            {
                personelsecili();
                if (btnKaydet.Text == "GÜNCELLE" && cmbBakimTuru.SelectedIndex != -1 && cmbMakine.SelectedIndex != -1 && cmbVardiya.SelectedIndex != -1 && personelsecim != false)
                {
                    DialogResult dialogResult =
                        MessageBox.Show("Seçili olan veri güncellenecek. \nEminseniz Evet butonuna basınız.",
                            "DİKKAT !", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        if (!String.IsNullOrEmpty(lblPeriyodilID.Text))
                        {
                            SqlConnection sqlConn = new SqlConnection();
                            sqlConn.ConnectionString = frmGiris.Database;
                            sqlConn.Open();

                            SqlCommand sqCom = new SqlCommand();
                            sqCom.Connection = sqlConn;
                            sqCom.CommandText =
                                "UPDATE bakPeriyodikBakim SET Makine=@Makine, Tarih=@Tarih, BakimTuru=@BakimTuru, Vardiya=@Vardiya, BaslamaSaati=@BaslamaSaati, BitisSaati=@BitisSaati, Yapildi=@Yapildi, Kullanici=@Kullanici, Personel=@Personel, Donem=@Donem, Yapilanlar=@Yapilanlar WHERE PeriyodikID=" +
                                lblPeriyodilID.Text;

                            sqCom.Parameters.Add("@Makine", SqlDbType.Int);
                            sqCom.Parameters["@Makine"].Value = Convert.ToInt32(cmbMakine.SelectedValue);

                            sqCom.Parameters.Add("@Tarih", SqlDbType.Date);
                            sqCom.Parameters["@Tarih"].Value = dateTimePicker1.Value;

                            sqCom.Parameters.Add("@BakimTuru", SqlDbType.Int);
                            sqCom.Parameters["@BakimTuru"].Value = Convert.ToInt32(cmbBakimTuru.SelectedValue);

                            sqCom.Parameters.Add("@Vardiya", SqlDbType.Int);
                            sqCom.Parameters["@Vardiya"].Value = Convert.ToInt32(cmbVardiya.SelectedValue);

                            string time = dateTimePicker2.Value.ToString();
                            var result = Convert.ToDateTime(time);
                            string test = result.ToString("HH:mm", System.Globalization.CultureInfo.InvariantCulture);

                            sqCom.Parameters.Add("@BaslamaSaati", SqlDbType.Time);
                            sqCom.Parameters["@BaslamaSaati"].Value = test.ToString();

                            string time2 = dateTimePicker3.Value.ToString();
                            var result2 = Convert.ToDateTime(time2);
                            string test2 = result2.ToString("HH:mm", System.Globalization.CultureInfo.InvariantCulture);

                            sqCom.Parameters.Add("@BitisSaati", SqlDbType.Time);
                            sqCom.Parameters["@BitisSaati"].Value = test2.ToString();

                            sqCom.Parameters.Add("@Yapildi", SqlDbType.Bit);
                            sqCom.Parameters["@Yapildi"].Value = chkYapildi.Checked;

                            sqCom.Parameters.Add("@Kullanici", SqlDbType.NVarChar);
                            sqCom.Parameters["@Kullanici"].Value = kullaniciadisoyadi;

                            sqCom.Parameters.Add("@Donem", SqlDbType.Int);
                            sqCom.Parameters["@Donem"].Value = Convert.ToInt32(cmbDonem.SelectedValue);

                            sqCom.Parameters.Add("@Yapilanlar", SqlDbType.NVarChar);
                            sqCom.Parameters["@Yapilanlar"].Value = txtYapilanlar.Text;
                            
                            PersonelIDGuncelle();

                            sqCom.Parameters.Add("@Personel", SqlDbType.NVarChar);
                            sqCom.Parameters["@Personel"].Value = sb2.ToString();

                            sqCom.ExecuteNonQuery();

                            sqlConn.Close();

                            MessageBox.Show(lblPeriyodilID.Text + " PeriyodikID nolu  \nKayıt Güncellendi ! ");
                            cmbBakimTuru.SelectedIndex = -1;
                            cmbVardiya.SelectedIndex = -1;
                            cmbMakine.SelectedIndex = -1;

                            txtYapilanlar.Text = null;
                            lblPeriyodilID.Text = null;
                            chkYapildi.Checked = false;
                        }
                        veriListeleHepsi();
                        btnKaydet.Text = "KAYDET";
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        
                    }

                    this.ActiveControl = dataGridView1;
                }
                else
                {
                    MessageBox.Show("Lütfen Bilgileri tam Doldurunuz !");
                }
            }
            else
            {
                MessageBox.Show("Lütfen Bilgileri tam Doldurunuz !");
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult =
                MessageBox.Show("Seçili olan veri silinecek. \nEminseniz Evet butonuna basınız.", "DİKKAT !",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            if (dialogResult==DialogResult.Yes)
            {
                SqlConnection sqlConn = new SqlConnection();
                sqlConn.ConnectionString = frmGiris.Database;
                sqlConn.Open();

                SqlCommand sqCom = new SqlCommand();
                sqCom.Connection = sqlConn;

                sqCom.CommandText = "DELETE FROM bakPeriyodikBakim WHERE PeriyodikID=" + lblPeriyodilID.Text;
                sqCom.ExecuteNonQuery();

                sqlConn.Close();

                MessageBox.Show("Veri Silindi !");
                veriListeleHepsi();
            }
            else if (dialogResult== DialogResult.No)
            {
                
            }
        }

        private void veriListele(string sorgu)
        {
            SqlConnection sqlConn = new SqlConnection();
            sqlConn.ConnectionString = frmGiris.Database;

            if (sqlConn.State == ConnectionState.Open)
            {
                sqlConn.Close();
                sqlConn.Open();
            }
            else
            {
                sqlConn.Open();
            }

            SqlCommand sqCom = new SqlCommand();
            sqCom.Connection = sqlConn;
            sqCom.CommandText = sorgu;

            SqlDataAdapter da = new SqlDataAdapter(sqCom);
            sqlConn.Close();
            DataTable dtProd = new DataTable();
            da.Fill(dtProd);
            dtTablom = null;
            dtTablom = dtProd;

            dataGridView1.DataSource = dtProd;
        }

        private void veriListeleHepsi()
        {
            veriListele(@"SELECT Tbl1.PeriyodikID, Tbl2.MakineAdi, Tbl1.Tarih, Tbl3.Turu, Tbl1.Personel, Tbl4.Vardiya
, CONVERT(varchar(5),Tbl1.BaslamaSaati,108) AS BaslamaSaati
, CONVERT(varchar(5),Tbl1.BitisSaati,108) AS BitisSaati
, (SELECT CONVERT(varchar(50), (DATEADD(MINUTE,(SELECT DATEDIFF(MINUTE,Tbl1.BaslamaSaati,Tbl1.BitisSaati))
, (SELECT DATEDIFF(HOUR, Tbl1.BaslamaSaati, Tbl1.BitisSaati)))), 108)) AS ToplamSure, Tbl1.Yapildi, Tbl1.Yapilanlar
, CONCAT(Tbl6.Yil, '-', Tbl7.Ay) AS Donem
FROM bakPeriyodikBakim Tbl1
LEFT JOIN bakMakineler Tbl2 ON Tbl1.Makine=Tbl2.MakineID
LEFT JOIN bakTur Tbl3 ON Tbl1.BakimTuru=Tbl3.TurID
LEFT JOIN bakVardiyalar Tbl4 ON Tbl1.Vardiya=Tbl4.VardiyaID
LEFT JOIN bakDonem Tbl5 ON Tbl1.Donem=Tbl5.DonemID
LEFT JOIN bakYillar Tbl6 ON Tbl5.Yil=Tbl6.YilID
LEFT JOIN bakAylar Tbl7 ON Tbl5.Ay=Tbl7.AyID");
            reSize();
        }

        private void reSize()
        {
            dataGridView1.AutoResizeColumns();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        public DataTable tableListele(int PeriyodikNo)
        {
            DataTable dtProd = new DataTable();
            dtProd = dtTablom.Select("PeriyodikID=" + PeriyodikNo).CopyToDataTable();

            return dtProd;
        }

        public DataTable MakineYukle()
        {
            SqlConnection conn4 = new SqlConnection(frmGiris.Database);
            conn4.Open();
            SqlCommand sc4 =
                new SqlCommand("SELECT MakineID, MakineAdi FROM bakMakineler WHERE Secenek=1 ORDER BY MakineID", conn4);
            SqlDataReader reader4;

            reader4 = sc4.ExecuteReader();
            DataTable dt4 = new DataTable();
            dt4.Columns.Add("MakineID", typeof(string));
            dt4.Columns.Add("MakineAdi", typeof(string));
            dt4.Load(reader4);

            conn4.Close();

            return dt4;
        }

        public DataTable TurYukle()
        {
            SqlConnection conn2 = new SqlConnection(frmGiris.Database);
            conn2.Open();
            SqlCommand sc2 = new SqlCommand("SELECT * FROM bakTur WHERE TurID !=3", conn2);
            SqlDataReader reader2;

            reader2 = sc2.ExecuteReader();
            DataTable dt2 = new DataTable();
            dt2.Columns.Add("TurID", typeof(string));
            dt2.Columns.Add("Turu", typeof(string));
            dt2.Load(reader2);

            conn2.Close();
            return dt2;
        }

        public DataTable PersonelYukle()
        {
            SqlConnection conn = new SqlConnection(frmGiris.Database);
            conn.Open();

            SqlCommand sc = new SqlCommand("SELECT * FROM bakPersoneller WHERE Pasif=0 ORDER BY PersonelAdi ASC", conn);
            SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();

            dt.Columns.Add("PersonelID", typeof(string));
            dt.Columns.Add("PersonelAdi", typeof(string));

            dt.Load(reader);
            conn.Close();
            return dt;
        }

        public DataTable VardiyalarYukle()
        {
            SqlConnection conn3 = new SqlConnection(frmGiris.Database);
            conn3.Open();
            SqlCommand sc3 = new SqlCommand("SELECT * FROM bakVardiyalar", conn3);
            SqlDataReader reader3;

            reader3 = sc3.ExecuteReader();
            DataTable dt3 = new DataTable();
            dt3.Columns.Add("VardiyaID", typeof(string));
            dt3.Columns.Add("Vardiya", typeof(string));
            dt3.Load(reader3);
            conn3.Close();

            return dt3;
        }

        public DataTable DonemYukle()
        {
            SqlConnection conn4 = new SqlConnection(frmGiris.Database);
            conn4.Open();
            SqlCommand sc4 = new SqlCommand(@"SELECT DonemID, CONCAT(Tbl6.Yil, '-',Tbl7.Ay) AS Donem
FROM bakDonem Tbl5
LEFT JOIN bakYillar Tbl6 ON Tbl5.Yil=Tbl6.YilID
LEFT JOIN bakAylar Tbl7 ON Tbl5.Ay=Tbl7.AyID", conn4);
            SqlDataReader reader4;

            reader4 = sc4.ExecuteReader();
            DataTable dt4 = new DataTable();

            dt4.Columns.Add("DonemID", typeof(string));
            dt4.Columns.Add("Donem", typeof(string));
            dt4.Load(reader4);

            conn4.Close();

            return dt4;
        }

        public void PersonelSec()
        {
            try
            {
                char[] ayrac = { '-' };
                string[] kelimeler = sb1.Split(ayrac);

                foreach (var item in kelimeler)
                {
                    DataTable dt2 = new DataTable();
                    dt2 = dtPerTablom.Select("PersonelID=" + item.ToString()).CopyToDataTable();

                    int index = lstPersonel.FindString(dt2.Rows[0][1].ToString());

                    if (index < 0)
                    {
                        MessageBox.Show("Bulunamadı !");
                    }
                    else
                    {
                        lstPersonel.SetItemChecked(index, true);
                    }
                }
            }
            catch
            {

            }
        }

        public void personelsecili()
        {
            if (lstPersonel.CheckedItems.Count < 1)
            {
                personelsecim = false;
            }
            else
            {
                personelsecim = true;
            }
        }

        public void personelseciliolmasin()
        {
            foreach (int indexChecked in lstPersonel.CheckedIndices)
            {
                lstPersonel.SetItemChecked(indexChecked, false);
            }
            lstPersonel.ClearSelected();
        }

        public void PersonelIDGuncelle()
        {
            string aa = lblPeriyodilID.Text;
            sb2 = null;
            foreach (object element in lstPersonel.CheckedItems)
            {
                DataRowView row = (DataRowView)element;

                sb2 += row[0].ToString();
                sb2 += "-";
            }

            sb2 = sb2.Substring(0, sb2.Length - 1);
        }

        public DataTable YilYukle()
        {
            using (SqlConnection conn = new SqlConnection(frmGiris.Database))
            {
                conn.Open();
                using (SqlCommand sc = new SqlCommand("SELECT YilID, Yil FROM bakYillar ORDER BY Yil DESC",conn))
                {
                    SqlDataReader reader;

                    reader = sc.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Columns.Add("YilID", typeof(string));
                    dt.Columns.Add("Yil", typeof(string));
                    dt.Load(reader);
                    conn.Close();
                    return dt;
                }
            }
        }

        private void comboboxYukle()
        {
            cmbBakimTuru.ValueMember = "TurID";
            cmbBakimTuru.DisplayMember = "Turu";
            cmbBakimTuru.DataSource = dtTurTablom;
            cmbBakimTuru.SelectedIndex = -1;

            cmbVardiya.ValueMember = "VardiyaID";
            cmbVardiya.DisplayMember = "Vardiya";
            cmbVardiya.DataSource = dtVardiyalarTablom;
            cmbVardiya.SelectedIndex = -1;

            cmbDonem.ValueMember = "DonemID";
            cmbDonem.DisplayMember = "Donem";
            cmbDonem.DataSource = dtDonemTablom;
            cmbDonem.SelectedIndex = -1;

            cmbMakine.ValueMember = "MakineID";
            cmbMakine.DisplayMember = "MakineAdi";
            cmbMakine.DataSource = dtMakinelerTablom;
            cmbMakine.SelectedIndex = -1;
        }

        private void LoadData(string kisim)
        {
            lstPersonel.Visible = true;
            DataTable dtProd = new DataTable();
            dtProd = dtPerTablom.Select("Tur=" + kisim).CopyToDataTable();

            DataTable dtP = new DataTable();
            dtP = dtProd;
            lstPersonel.DataSource = dtP;
            lstPersonel.ValueMember = "PersonelID";
            lstPersonel.DisplayMember = "PersonelAdi";
            lstPersonel.ClearSelected();
        }

        private void cmbBakimTuru_TextChanged(object sender, EventArgs e)
        {
            switch (cmbBakimTuru.Text)
            {
                case "ELEKTRİK":
                    LoadData("1");
                    break;
                case "MEKANİK":
                    LoadData("2");
                    break;

                default:
                    break;
            }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int a = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                lblPeriyodilID.Text = a.ToString();
                DataTable dtProd = new DataTable();
                dtProd = tableListele(Convert.ToInt32(a.ToString()));

                if (dataGridView1.Rows.Count > 0 && lblPeriyodilID.Text != "")
                {
                    sb1 = null;

                    dateTimePicker1.Value = Convert.ToDateTime(dtProd.Rows[0]["Tarih"]);
                    cmbBakimTuru.Text = dtProd.Rows[0]["Turu"].ToString();
                    cmbVardiya.Text = dtProd.Rows[0]["Vardiya"].ToString();
                    dateTimePicker2.Text = Convert.ToString(dtProd.Rows[0]["BaslamaSaati"]);
                    dateTimePicker3.Text = Convert.ToString(dtProd.Rows[0]["BitisSaati"]);
                    lblSure.Text = Convert.ToString(dtProd.Rows[0]["ToplamSure"]);
                    sb1 = dtProd.Rows[0]["Personel"].ToString();
                    chkYapildi.Checked = Convert.ToBoolean(dtProd.Rows[0]["Yapildi"]);
                    cmbDonem.Text = dtProd.Rows[0]["Donem"].ToString();
                    cmbMakine.Text = dtProd.Rows[0]["MakineAdi"].ToString();
                    txtYapilanlar.Text = dtProd.Rows[0]["Yapilanlar"].ToString();

                    personelseciliolmasin();
                    PersonelSec();

                    if (btnKaydet.Enabled == false)
                    {
                        btnKaydet.Enabled = true;
                    }

                    if (btnKaydet.Text =="KAYDET")
                    {
                        btnKaydet.Text = "GÜNCELLE";
                    }
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void txtYapilanlar_TextChanged(object sender, EventArgs e)
        {
            if (txtYapilanlar.Text.Length > 249)
            {
                MessageBox.Show("Uzun veri yazamazsınız !");
                txtYapilanlar.Text = txtYapilanlar.Text.Substring(0, 249);
            }
        }
    }
}
