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
    public partial class frmPersonelMakine2 : Form
    {
        public frmPersonelMakine2()
        {
            InitializeComponent();
        }

        private void frmMakineler_Load(object sender, EventArgs e)
        {
            veriListele();
        }

        private void btnYeni_Click(object sender, EventArgs e)
        {


            lblMakineID.Text = "";
            // txtMakineAdi.Text = "";
            cmbPersonel.SelectedIndex = -1;
            // cmbMakina.SelectedIndex = -1;
            lblMakinalar.Text = "";
            //chkSecenek.Checked = false;

            btnKaydet.Enabled = true;
            btnKaydet.Text = "GÜNCELLE";
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {



            if (btnKaydet.Text == "GÜNCELLE")
            {
                if (btnKaydet.Text == "GÜNCELLE" && cmbPersonel.SelectedIndex != -1)
                {
                    if (!String.IsNullOrEmpty(lblMakineID.Text))
                    {
                        SqlConnection sqlConn = new SqlConnection();
                        sqlConn.ConnectionString = frmGiris.Database;
                        sqlConn.Open();

                        SqlCommand sqCom = new SqlCommand();
                        sqCom.Connection = sqlConn;
                        sqCom.CommandText =
                            "UPDATE bakMakineler SET   PersonelID=@PersonelID WHERE MakineID =" +
                            lblMakineID.Text;

                        //sqCom.Parameters.Add("@MakineAdi", SqlDbType.NVarChar);
                        //sqCom.Parameters["@MakineAdi"].Value = cmbMakina.SelectedValue;

                        sqCom.Parameters.Add("@PersonelID", SqlDbType.Int);
                        sqCom.Parameters["@PersonelID"].Value = Convert.ToInt32(cmbPersonel.SelectedValue);

                        sqCom.ExecuteNonQuery();
                        sqlConn.Close();

                        //----------------------------


                        MessageBox.Show("Kayıt Güncellendi ! ");
                    }
                    veriListele();

                    lblMakineID.Text = "";
                    // txtMakineAdi.Text = "";
                    cmbPersonel.SelectedIndex = -1;
                    // cmbMakina.SelectedIndex = -1;
                    lblMakinalar.Text = "";
                    //chkSecenek.Checked = false;

                    btnKaydet.Text = "GÜNCELLE";
                }
                else
                {
                    MessageBox.Show("Lütfen Bilgileri Tam Doldurunuz ! ");
                }
            }
            veriListele();
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
                "SELECT Tbl1.MakineID, Tbl1.MakineAdi, Tbl2.PersonelAdi FROM bakMakineler Tbl1 LEFT JOIN bakPersoneller Tbl2 ON Tbl1.PersonelID=Tbl2.PersonelID";
            sqCom.ExecuteNonQuery();

            DataTable dtProd = new DataTable();
            SqlDataAdapter sqDa = new SqlDataAdapter();
            sqDa.SelectCommand = sqCom;
            sqlConn.Close();
            sqDa.Fill(dtProd);

            dataGridView1.DataSource = dtProd;

            SqlConnection conn = new SqlConnection(frmGiris.Database);
            conn.Open();
            SqlCommand sc = new SqlCommand("SELECT * FROM bakMakineler", conn);
            SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("MakineID", typeof(string));
            dt.Columns.Add("MakineAdi", typeof(string));
            dt.Load(reader);

            //cmbMakina.ValueMember = "MakineID";
            //cmbMakina.DisplayMember = "MakineAdi";
            //cmbMakina.DataSource = dt;
            //cmbMakina.SelectedIndex = -1;

            lblMakinalar.DataBindings.Clear();
            lblMakinalar.DataBindings.Add("Text", dt, "MakineAdi");


            conn.Close();

            // PERSONEL LİSTELE

            SqlConnection conn2 = new SqlConnection(frmGiris.Database);
            conn2.Open();
            SqlCommand sc2 = new SqlCommand("SELECT * FROM bakPersoneller", conn2);
            SqlDataReader reader2;

            reader2 = sc2.ExecuteReader();
            DataTable dt2 = new DataTable();
            dt2.Columns.Add("PersonelID", typeof(string));
            dt2.Columns.Add("PersonelAdi", typeof(string));
            dt2.Load(reader2);

            cmbPersonel.ValueMember = "PersonelID";
            cmbPersonel.DisplayMember = "PersonelAdi";
            cmbPersonel.DataSource = dt2;
            cmbPersonel.SelectedIndex = -1;

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
                lblMakineID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();

                if (dataGridView1.Rows.Count > 0)
                {
                    SqlConnection sqlConn = new SqlConnection();
                    sqlConn.ConnectionString = frmGiris.Database;
                    sqlConn.Open();

                    SqlCommand sqCom = new SqlCommand();
                    sqCom.Connection = sqlConn;
                    // SqlDataAdapter da = new SqlDataAdapter();
                    sqCom.CommandText =
                        "SELECT Tbl1.MakineID, Tbl1.MakineAdi, Tbl2.PersonelAdi FROM bakMakineler Tbl1 LEFT JOIN bakPersoneller Tbl2 ON Tbl1.PersonelID=Tbl2.PersonelID WHERE Tbl1.MakineID =" +
                        lblMakineID.Text;
                    sqCom.ExecuteNonQuery();

                    DataTable dtProd = new DataTable();
                    SqlDataAdapter sqDa = new SqlDataAdapter();
                    sqDa.SelectCommand = sqCom;
                    sqlConn.Close();
                    sqDa.Fill(dtProd);

                    // txtMakineAdi.Text = dtProd.Rows[0]["MakineAdi"].ToString();
                    lblMakinalar.Text = dtProd.Rows[0]["MakineAdi"].ToString();
                    cmbPersonel.Text = dtProd.Rows[0]["PersonelAdi"].ToString();
                    // chkSecenek.Checked = Convert.ToBoolean(dtProd.Rows[0]["Secenek"]);
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
