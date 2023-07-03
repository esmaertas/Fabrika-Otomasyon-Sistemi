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
    public partial class frmPersoneller : Form
    {
        public frmPersoneller()
        {
            InitializeComponent();
        }

        public string kullanicibilgi;

        private void frmPersoneller_Load(object sender, EventArgs e)
        {
            kullanicibilgi = frmGiris.kullanici_bilgi;
            veriListele();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (btnKaydet.Text=="KAYDET" && txtPersonelAdi.Text != "" && cmbBolum.SelectedIndex != -1)
            {
                SqlConnection sqlConn = new SqlConnection();
                sqlConn.ConnectionString = frmGiris.Database;
                sqlConn.Open();
                SqlCommand sqCom = new SqlCommand();
                sqCom.Connection = sqlConn;
                sqCom.CommandText =
                    "INSERT INTO bakPersoneller (PersonelAdi, Tur, Pasif, Kullanici) VALUES (@PersonelAdi, @Tur, @Pasif, @Kullanici)";

                sqCom.Parameters.Add("@PersonelAdi", SqlDbType.NVarChar);
                sqCom.Parameters["@PersonelAdi"].Value = txtPersonelAdi.Text;

                sqCom.Parameters.Add("@Tur", SqlDbType.Int);
                sqCom.Parameters["@Tur"].Value = Convert.ToInt32(cmbBolum.SelectedValue);

                sqCom.Parameters.Add("@Pasif", SqlDbType.Bit);
                sqCom.Parameters["@Pasif"].Value = chkPasifMi.Checked;

                sqCom.Parameters.Add("@Kullanici", SqlDbType.NVarChar);
                sqCom.Parameters["@Kullanici"].Value = kullanicibilgi;

                sqCom.ExecuteNonQuery();
                sqlConn.Close();

                veriListele();

                Personelid.Text = "";
                txtPersonelAdi.Text = "";
                cmbBolum.SelectedIndex = -1;
                chkPasifMi.Checked = false;
                MessageBox.Show("Yeni Kayıt Eklendi.");
            }
            else if (btnKaydet.Text=="GÜNCELLE")
            {
                if (btnKaydet.Text=="GÜNCELLE" && txtPersonelAdi.Text !="" && cmbBolum.SelectedIndex != -1)
                {
                    if (!String.IsNullOrEmpty(Personelid.Text))
                    {
                        SqlConnection sqlConn = new SqlConnection();
                        sqlConn.ConnectionString = frmGiris.Database;
                        sqlConn.Open();
                        SqlCommand sqCom = new SqlCommand();
                        sqCom.Connection = sqlConn;

                        sqCom.CommandText =
                            "UPDATE bakPersoneller SET PersonelAdi=@PersonelAdi, Tur=@Tur, Pasif=@Pasif, Kullanici=@Kullanici WHERE PersonelID=" +
                            Personelid.Text;

                        sqCom.Parameters.Add("@PersonelAdi", SqlDbType.NVarChar);
                        sqCom.Parameters["@PersonelAdi"].Value = txtPersonelAdi.Text;

                        sqCom.Parameters.Add("@Tur", SqlDbType.Int);
                        sqCom.Parameters["@Tur"].Value = Convert.ToInt32(cmbBolum.SelectedValue);

                        sqCom.Parameters.Add("@Pasif", SqlDbType.Bit);
                        sqCom.Parameters["@Pasif"].Value = chkPasifMi.Checked;

                        sqCom.Parameters.Add("@Kullanici", SqlDbType.NVarChar);
                        sqCom.Parameters["@Kullanici"].Value = kullanicibilgi;

                        sqCom.ExecuteNonQuery();
                        sqlConn.Close();

                        MessageBox.Show("Kayıt Güncellendi");
                    }
                    veriListele();

                    Personelid.Text = "";
                    txtPersonelAdi.Text = "";
                    cmbBolum.SelectedIndex = -1;
                    chkPasifMi.Checked = false;

                    btnKaydet.Text = "KAYDET";
                }
                else
                {
                    MessageBox.Show("Lütfen Bilgileri Tam Doldurunuz!");
                }
            }
        }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            veriListele();

            Personelid.Text = "";
            txtPersonelAdi.Text = "";
            cmbBolum.SelectedIndex = -1;
            chkPasifMi.Checked = false;
            btnKaydet.Enabled = true;
            btnKaydet.Text = "KAYDET";
        }

        private void veriListele()
        {
            SqlConnection sqlConn = new SqlConnection();
            sqlConn.ConnectionString = frmGiris.Database;
            sqlConn.Open();

            SqlCommand sqCom = new SqlCommand();
            sqCom.Connection = sqlConn;
            //SqlDataAdapter da = new SqlDataAdapter();

            sqCom.CommandText =
                "SELECT Tbl1.PersonelID, Tbl1.PersonelAdi, Tbl2.Turu, Tbl1.Pasif FROM bakPersoneller Tbl1 LEFT JOIN bakTur Tbl2 ON Tbl1.Tur=Tbl2.TurID ORDER BY Tbl1.Pasif, Tbl2.Turu, Tbl1.PersonelAdi";
            sqCom.ExecuteNonQuery();

            DataTable dtProd = new DataTable();
            SqlDataAdapter sqDa = new SqlDataAdapter();
            sqDa.SelectCommand = sqCom;
            sqlConn.Close();
            sqDa.Fill(dtProd);

            dataGridView1.DataSource = dtProd;
            SqlConnection conn = new SqlConnection(frmGiris.Database);
            conn.Open();
            SqlCommand sc = new SqlCommand("SELECT * FROM bakTur WHERE TurID<>3", conn);
            SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("TurID", typeof(string));
            dt.Columns.Add("Turu", typeof(string));
            dt.Load(reader);

            cmbBolum.ValueMember = "TurID";
            cmbBolum.DisplayMember = "Turu";
            cmbBolum.DataSource = dt;
            cmbBolum.SelectedIndex = -1;

            conn.Close();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            // Change the background color of cells
            dataGridView1.DefaultCellStyle.BackColor = Color.LightBlue;

            // Change the foreground (text) color of cells
            dataGridView1.DefaultCellStyle.ForeColor = Color.DarkBlue;

            // Change the background color of selected cells
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.Yellow;

            // Change the foreground color of selected cells
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
            try
            {
                Personelid.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();

                if (dataGridView1.Rows.Count > 0)
                {
                    SqlConnection sqlConn = new SqlConnection();
                    sqlConn.ConnectionString = frmGiris.Database;
                    sqlConn.Open();
                    SqlCommand sqCom = new SqlCommand();
                    sqCom.Connection = sqlConn;
                   // SqlDataAdapter da = new SqlDataAdapter();
                    sqCom.CommandText =
                        "SELECT Tbl1.PersonelID, Tbl1.PersonelAdi, Tbl2.Turu, Tbl1.Pasif FROM bakPersoneller Tbl1 LEFT JOIN  bakTur Tbl2 ON Tbl1.Tur = Tbl2.TurID WHERE PersonelID=" +
                        Personelid.Text;
                    sqCom.ExecuteNonQuery();

                    DataTable dtProd = new DataTable();
                    SqlDataAdapter sqDa = new SqlDataAdapter();
                    sqDa.SelectCommand = sqCom;
                    sqlConn.Close();
                    sqDa.Fill(dtProd);
                    txtPersonelAdi.Text = dtProd.Rows[0]["PersonelAdi"].ToString();
                    cmbBolum.Text = dtProd.Rows[0]["Turu"].ToString();
                    chkPasifMi.Checked = Convert.ToBoolean(dtProd.Rows[0]["Pasif"]);
                    btnKaydet.Enabled = true;
                    btnKaydet.Text = "GÜNCELLE";
                }
            }
            catch 
            {
                
            }
        }
    }
}
