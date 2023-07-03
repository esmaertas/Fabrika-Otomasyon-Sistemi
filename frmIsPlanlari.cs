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
    public partial class frmIsPlanlari : Form
    {
        public frmIsPlanlari()
        {
            InitializeComponent();
        }

        private void frmIsPlanlari_Load(object sender, EventArgs e)
        {
            BolumYukle();
            IslemYukle();

            txtisDurumu.ScrollBars = ScrollBars.Vertical;
            txtisTanim.ScrollBars = ScrollBars.Vertical;
            this.ActiveControl = dataGridView1;
            kullaniciadisoyadi = frmGiris.kullanici_bilgi;
            veriListele();
        }

        public string kullaniciadisoyadi;

        private void btnYeni_Click(object sender, EventArgs e)
        {
            lblisID.Text = null;
            cmbBolum.SelectedIndex = -1;
            cmbislemTuru.SelectedIndex = -1;
            formNo.Value = 0;
            txtTakip.Text = null;
            txtisTanim.Text = null;
            txtisDurumu.Text = null;
            chkYapildi.Checked = false;
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker1.Value = DateTime.Today;

            btnKaydet.Enabled = true;
            btnKaydet.Text = "KAYDET";
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (btnKaydet.Text=="KAYDET" && cmbBolum.SelectedIndex != -1 && cmbislemTuru.SelectedIndex != -1 && txtisDurumu.Text!="" && txtisTanim.Text!="" && txtTakip.Text!="")
            {
                SqlConnection sqlConn = new SqlConnection();
                sqlConn.ConnectionString = frmGiris.Database;
                sqlConn.Open();

                SqlCommand sqCom = new SqlCommand();
                sqCom.Connection = sqlConn;
                sqCom.CommandText =
                    "INSERT INTO bakIsPlani(IsTanimi, Durumu, Turu, Bolum, FormSiraNo, TalepTarihi, IstenenTarih, TamamlanmaTarihi, TakipEdenler, Yapildi, Kullanici, DegisiklikTuru, IslemTarihi) VALUES (@IsTanimi, @Durumu, @Turu, @Bolum, @FormSiraNo, @TalepTarihi, @IstenenTarih, @TamamlanmaTarihi, @TakipEdenler, @Yapildi, @Kullanici, @DegisiklikTuru, @IslemTarihi); SELECT SCOPE_IDENTITY()";

                sqCom.Parameters.Add("@IsTanimi", SqlDbType.NVarChar);
                sqCom.Parameters["@IsTanimi"].Value = txtisTanim.Text;

                sqCom.Parameters.Add("@Durumu", SqlDbType.NVarChar);
                sqCom.Parameters["@Durumu"].Value = txtisDurumu.Text;

                sqCom.Parameters.Add("@Turu", SqlDbType.Int);
                sqCom.Parameters["@Turu"].Value = Convert.ToInt32(cmbislemTuru.SelectedValue);

                sqCom.Parameters.Add("@Bolum", SqlDbType.Int);
                sqCom.Parameters["@Bolum"].Value = Convert.ToInt32(cmbBolum.SelectedValue);

                sqCom.Parameters.Add("@FormSiraNo", SqlDbType.Int);
                sqCom.Parameters["@FormSiraNo"].Value = formNo.Value;

                sqCom.Parameters.Add("@TalepTarihi", SqlDbType.Date);
                sqCom.Parameters["@TalepTarihi"].Value = dateTimePicker1.Value;

                sqCom.Parameters.Add("@IstenenTarih", SqlDbType.Date);
                sqCom.Parameters["@IstenenTarih"].Value = dateTimePicker2.Value;

                sqCom.Parameters.Add("@Kullanici", SqlDbType.NVarChar);
                sqCom.Parameters["@Kullanici"].Value = kullaniciadisoyadi;

                sqCom.Parameters.Add("@DegisiklikTuru", SqlDbType.NVarChar);
                sqCom.Parameters["@DegisiklikTuru"].Value = "Kayıt";

                sqCom.Parameters.Add("@IslemTarihi", SqlDbType.SmallDateTime);
                sqCom.Parameters["@IslemTarihi"].Value = DateTime.Now.ToString();

                sqCom.Parameters.Add("@Yapildi", SqlDbType.Bit);
                sqCom.Parameters["@Yapildi"].Value = chkYapildi.Checked;

                if (chkYapildi.Checked==true)
                {
                    sqCom.Parameters.Add("@TamamlanmaTarihi", SqlDbType.Date);
                    sqCom.Parameters["@TamamlanmaTarihi"].Value = DateTime.Now.ToLongDateString();
                }
                else
                {
                    sqCom.Parameters.Add("@TamamlanmaTarihi", SqlDbType.Date);
                    sqCom.Parameters["@TamamlanmaTarihi"].Value = DBNull.Value;
                }

                sqCom.Parameters.Add("@TakipEdenler", SqlDbType.NVarChar);
                sqCom.Parameters["@TakipEdenler"].Value = txtTakip.Text;

                var order_id = sqCom.ExecuteNonQuery();

                lblisID.Text = order_id.ToString();
                sqlConn.Close();

                cmbBolum.SelectedIndex = -1;
                cmbislemTuru.SelectedIndex = -1;
                formNo.Value = 0;
                txtisDurumu.Text = null;
                txtisTanim.Text = null;
                chkYapildi.Checked = false;

                MessageBox.Show(lblisID.Text + " nolu IsID ile \nYeni kayıt eklendi !");
                lblisID.Text = null;

                veriListele();
            }
            else if (btnKaydet.Text == "GÜNCELLE")
            {
                if (btnKaydet.Text == "GÜNCELLE" && cmbBolum.SelectedIndex != -1 && cmbislemTuru.SelectedIndex != -1 && txtisDurumu.Text != "" && txtisTanim.Text != "" && txtTakip.Text != "")
                {
                    DialogResult dialogResult =
                        MessageBox.Show("Seçili olan veri güncellenecek. \nEminseniz Evet butonuna basınız.", "DİKKAT!",
                            MessageBoxButtons.YesNo);
                    if (dialogResult==DialogResult.Yes)
                    {
                        if (!String.IsNullOrEmpty(lblisID.Text))
                        {
                            SqlConnection sqlConn = new SqlConnection();
                            sqlConn.ConnectionString = frmGiris.Database;
                            sqlConn.Open();

                            SqlCommand sqCom = new SqlCommand();
                            sqCom.Connection = sqlConn;
                            sqCom.CommandText =
                                "UPDATE bakIsPlani SET IsTanimi=@IsTanimi, Durumu=@Durumu, Turu=@Turu, Bolum=@Bolum, FormSiraNo=@FormSiraNo, TalepTarihi=@TalepTarihi, IstenenTarih=@IstenenTarih, TamamlanmaTarihi=@TamamlanmaTarihi, TakipEdenler=@TakipEdenler, Yapildi=@Yapildi, Kullanici=@Kullanici, DegisiklikTuru=@DegisiklikTuru, IslemTarihi=@IslemTarihi WHERE IsID =" +
                                lblisID.Text;

                            sqCom.Parameters.Add("@IsTanimi", SqlDbType.NVarChar);
                            sqCom.Parameters["@IsTanimi"].Value = txtisTanim.Text;

                            sqCom.Parameters.Add("@Durumu", SqlDbType.NVarChar);
                            sqCom.Parameters["@Durumu"].Value = txtisDurumu.Text;

                            sqCom.Parameters.Add("@Turu", SqlDbType.Int);
                            sqCom.Parameters["@Turu"].Value = Convert.ToInt32(cmbislemTuru.SelectedValue);

                            sqCom.Parameters.Add("@Bolum", SqlDbType.Int);
                            sqCom.Parameters["@Bolum"].Value = Convert.ToInt32(cmbBolum.SelectedValue);

                            sqCom.Parameters.Add("@FormSiraNo", SqlDbType.Int);
                            sqCom.Parameters["@FormSiraNo"].Value = formNo.Value;

                            sqCom.Parameters.Add("@TalepTarihi", SqlDbType.Date);
                            sqCom.Parameters["@TalepTarihi"].Value = dateTimePicker1.Value;

                            sqCom.Parameters.Add("@IstenenTarih", SqlDbType.Date);
                            sqCom.Parameters["@IstenenTarih"].Value = dateTimePicker2.Value;

                            sqCom.Parameters.Add("@Kullanici", SqlDbType.NVarChar);
                            sqCom.Parameters["@Kullanici"].Value = kullaniciadisoyadi;

                            sqCom.Parameters.Add("@DegisiklikTuru", SqlDbType.NVarChar);
                            sqCom.Parameters["@DegisiklikTuru"].Value = "Düzeltme";

                            sqCom.Parameters.Add("@IslemTarihi", SqlDbType.SmallDateTime);
                            sqCom.Parameters["@IslemTarihi"].Value = DateTime.Now.ToString();

                            sqCom.Parameters.Add("@Yapildi", SqlDbType.Bit);
                            sqCom.Parameters["@Yapildi"].Value = chkYapildi.Checked;

                            if (chkYapildi.Checked==true)
                            {
                                sqCom.Parameters.Add("@TamamlanmaTarihi", SqlDbType.Date);
                                sqCom.Parameters["@TamamlanmaTarihi"].Value = DateTime.Now.ToLongDateString();
                            }
                            else
                            {
                                sqCom.Parameters.Add("@TamamlanmaTarihi", SqlDbType.Date);
                                sqCom.Parameters["@TamamlanmaTarihi"].Value = DBNull.Value;
                            }

                            sqCom.Parameters.Add("@TakipEdenler", SqlDbType.NVarChar);
                            sqCom.Parameters["@TakipEdenler"].Value = txtTakip.Text;
                            sqCom.ExecuteNonQuery();
                            sqlConn.Close();

                            MessageBox.Show(lblisID.Text + " IsID nolu \nKayıt güncellendi !");
                            veriListele();

                            cmbBolum.SelectedIndex = -1;
                            cmbBolum.SelectedIndex = -1;
                            formNo.Value = 0;
                            txtisDurumu.Text = null;
                            txtisTanim.Text = null;
                            lblisID.Text = null;
                            chkYapildi.Checked = false;
                        }

                        btnKaydet.Text = "KAYDET";
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        
                    }

                    this.ActiveControl = dataGridView1;
                }
                else
                {
                    MessageBox.Show("Lütfen Bilgileri Tam Doldurunuz ! ");
                }
            }
            else
            {
                MessageBox.Show("Lütfen Bilgileri Tam Doldurunuz ! ");
            }

        }

        public void BolumYukle()
        {
            SqlConnection conn = new SqlConnection(frmGiris.Database);
            conn.Open();
            SqlCommand sc = new SqlCommand("SELECT * FROM bakBolumler",conn);
            SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("BolumID", typeof(string));
            dt.Columns.Add("Bolum", typeof(string));
            dt.Load(reader);

            cmbBolum.ValueMember = "BolumID";
            cmbBolum.DisplayMember = "Bolum";
            cmbBolum.DataSource = dt;
            cmbBolum.SelectedIndex = -1;

            conn.Close();
        }

        public void IslemYukle()
        {
            SqlConnection conn = new SqlConnection(frmGiris.Database);
            conn.Open();
            SqlCommand sc = new SqlCommand("SELECT * FROM bakIslem WHERE IslemID=2 OR IslemID=3", conn);
            SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("IslemID", typeof(string));
            dt.Columns.Add("IslemTuru", typeof(string));
            dt.Load(reader);

            cmbislemTuru.ValueMember = "IslemID";
            cmbislemTuru.DisplayMember = "IslemTuru";
            cmbislemTuru.DataSource = dt;
            cmbislemTuru.SelectedIndex = -1;

            conn.Close();
        }

        private void veriListele()
        {
            SqlConnection sqlConn = new SqlConnection();
            sqlConn.ConnectionString = frmGiris.Database;

            if (sqlConn.State==ConnectionState.Open)
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
            sqCom.CommandText =
                @"SELECT Tbl1.IsID, Tbl1.IsTanimi, Tbl1.Durumu, Tbl3.IslemTuru, Tbl2.Bolum, Tbl1.FormSiraNo, Tbl1.TalepTarihi, Tbl1.IstenenTarih, Tbl1.TamamlanmaTarihi, Tbl1.TakipEdenler, Tbl1.Yapildi
FROM bakIsPlani Tbl1
LEFT JOIN bakBolumler Tbl2 ON Tbl1.Bolum=Tbl2.BolumID
LEFT JOIN bakIslem Tbl3 ON Tbl1.Turu=Tbl3.IslemID
WHERE Tbl1.Yapildi=0
ORDER BY Tbl1.IstenenTarih, Tbl1.IsID";
            SqlDataAdapter da = new SqlDataAdapter(sqCom);
            DataTable dtProd = new DataTable();
            da.Fill(dtProd);
            dataGridView1.DataSource = dtProd;
            sqlConn.Close();
            this.ActiveControl = dataGridView1;
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                lblisID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                if (dataGridView1.Rows.Count > 0)
                {
                    SqlConnection sqlConn = new SqlConnection();
                    sqlConn.ConnectionString = frmGiris.Database;
                    sqlConn.Open();

                    SqlCommand sqCom = new SqlCommand();
                    sqCom.Connection = sqlConn;

                    sqCom.CommandText =
                        @"SELECT Tbl1.IsID, Tbl1.IsTanimi, Tbl1.Durumu, Tbl3.IslemTuru, Tbl2.Bolum, Tbl1.FormSiraNo, Tbl1.TalepTarihi, Tbl1.IstenenTarih, Tbl1.TamamlanmaTarihi, Tbl1.TakipEdenler, Tbl1.Yapildi
FROM bakIsPlani Tbl1
LEFT JOIN bakBolumler Tbl2 ON Tbl1.Bolum=Tbl2.BolumID
LEFT JOIN bakIslem Tbl3 ON Tbl1.Turu=Tbl3.IslemID
WHERE Tbl1.IsID =" + lblisID.Text;
                    sqCom.ExecuteNonQuery();

                    DataTable dtProd = new DataTable();
                    SqlDataAdapter sqDa = new SqlDataAdapter();
                    sqDa.SelectCommand = sqCom;
                    sqlConn.Close();
                    sqDa.Fill(dtProd);

                    cmbislemTuru.Text = dtProd.Rows[0]["IslemTuru"].ToString();
                    cmbBolum.Text = dtProd.Rows[0]["Bolum"].ToString();
                    formNo.Value = Convert.ToInt32(dtProd.Rows[0]["FormSiraNo"]);
                    dateTimePicker1.Value = Convert.ToDateTime(dtProd.Rows[0]["TalepTarihi"]);
                    dateTimePicker2.Value = Convert.ToDateTime(dtProd.Rows[0]["IstenenTarih"]);
                    txtTakip.Text = dtProd.Rows[0]["TakipEdenler"].ToString();
                    string tamamlanmatarih = dtProd.Rows[0]["TamamlanmaTarihi"].ToString();

                    if (!String.IsNullOrEmpty(tamamlanmatarih))
                    {
                        lblTamamlanmaTarihi.Text = tamamlanmatarih.Substring(0, 10);
                    }

                    txtisTanim.Text = dtProd.Rows[0]["IsTanimi"].ToString();
                    txtisDurumu.Text = dtProd.Rows[0]["Durumu"].ToString();
                    chkYapildi.Checked = Convert.ToBoolean(dtProd.Rows[0]["Yapildi"]);
                    txtTakip.Text = dtProd.Rows[0]["TakipEdenler"].ToString();

                    btnKaydet.Text = "GÜNCELLE";
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
