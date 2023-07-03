using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BakimTakipProgrami
{
    public partial class _1frmPersonelMakine : Form
    {
        public _1frmPersonelMakine()
        {
            InitializeComponent();
        }

        private void _1frmPersonelMakine_Load(object sender, EventArgs e)
        {

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
                "SELECT Tbl1.PersonelID, Tbl1.PersonelAdi, Tbl2.Bolum, Tbl1.Secenek FROM bakMakineler Tbl1 LEFT JOIN bakBolumler Tbl2 ON Tbl1.BolumID=Tbl2.BolumID";
            sqCom.ExecuteNonQuery();

            DataTable dtProd = new DataTable();
            SqlDataAdapter sqDa = new SqlDataAdapter();
            sqDa.SelectCommand = sqCom;
            sqlConn.Close();
            sqDa.Fill(dtProd);

            dataGridView1.DataSource = dtProd;

            SqlConnection conn = new SqlConnection(frmGiris.Database);
            conn.Open();
            SqlCommand sc = new SqlCommand("SELECT * FROM bakBolumler", conn);
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
    }
}
