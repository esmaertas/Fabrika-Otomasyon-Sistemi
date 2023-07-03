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
    public partial class frmDuyurular : Form
    {
        public frmDuyurular()
        {
            InitializeComponent();
        }

        private void frmDuyurular_Load(object sender, EventArgs e)
        {
            SqlConnection sqlConn = new SqlConnection();
            sqlConn.ConnectionString = frmGiris.Database;
            sqlConn.Open();

            SqlCommand sqCom = new SqlCommand();
            sqCom.Connection = sqlConn;
            sqCom.CommandText =
                "SELECT Duyuru, Yapilacak1,Yapilacak2,Yapilacak3,Yapilacak4,Yapilacak5,Yapilacak6,Yapilacak7,Yapilacak8,Yapilacak9,Yapilacak10 FROM bakDuyurular";
            sqCom.ExecuteNonQuery();

            DataTable dtProd = new DataTable();
            SqlDataAdapter sqDa = new SqlDataAdapter();
            sqDa.SelectCommand = sqCom;
            sqlConn.Close();
            sqDa.Fill(dtProd);
            txt0.Text = dtProd.Rows[0]["Duyuru"].ToString();
            txt1.Text = dtProd.Rows[0]["Yapilacak1"].ToString();
            txt2.Text = dtProd.Rows[0]["Yapilacak2"].ToString();
            txt3.Text = dtProd.Rows[0]["Yapilacak3"].ToString();
            txt4.Text = dtProd.Rows[0]["Yapilacak4"].ToString();
            txt5.Text = dtProd.Rows[0]["Yapilacak5"].ToString();
            txt6.Text = dtProd.Rows[0]["Yapilacak6"].ToString();
            txt7.Text = dtProd.Rows[0]["Yapilacak7"].ToString();
            txt8.Text = dtProd.Rows[0]["Yapilacak8"].ToString();
            txt9.Text = dtProd.Rows[0]["Yapilacak9"].ToString();
            txt10.Text = dtProd.Rows[0]["Yapilacak10"].ToString();

            txt0.Focus();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConn = new SqlConnection();
            sqlConn.ConnectionString = frmGiris.Database;
            sqlConn.Open();

            SqlCommand sqCom = new SqlCommand();
            sqCom.Connection = sqlConn;
            sqCom.CommandText =
                "UPDATE bakDuyurular SET Duyuru=@Duyuru, Yapilacak1=@Yapilacak1,Yapilacak2=@Yapilacak2,Yapilacak3=@Yapilacak3,Yapilacak4=@Yapilacak4,Yapilacak5=@Yapilacak5,Yapilacak6=@Yapilacak6,Yapilacak7=@Yapilacak7,Yapilacak8=@Yapilacak8,Yapilacak9=@Yapilacak9,Yapilacak10=@Yapilacak10 WHERE DuyuruID=1";

            sqCom.Parameters.Add("@Duyuru", SqlDbType.NVarChar);
            sqCom.Parameters["@Duyuru"].Value = txt0.Text;

            sqCom.Parameters.Add("@Yapilacak1", SqlDbType.NVarChar);
            sqCom.Parameters["@Yapilacak1"].Value = txt1.Text;

            sqCom.Parameters.Add("@Yapilacak2", SqlDbType.NVarChar);
            sqCom.Parameters["@Yapilacak2"].Value = txt2.Text;

            sqCom.Parameters.Add("@Yapilacak3", SqlDbType.NVarChar);
            sqCom.Parameters["@Yapilacak3"].Value = txt3.Text;

            sqCom.Parameters.Add("@Yapilacak4", SqlDbType.NVarChar);
            sqCom.Parameters["@Yapilacak4"].Value = txt4.Text;

            sqCom.Parameters.Add("@Yapilacak5", SqlDbType.NVarChar);
            sqCom.Parameters["@Yapilacak5"].Value = txt5.Text;

            sqCom.Parameters.Add("@Yapilacak6", SqlDbType.NVarChar);
            sqCom.Parameters["@Yapilacak6"].Value = txt6.Text;

            sqCom.Parameters.Add("@Yapilacak7", SqlDbType.NVarChar);
            sqCom.Parameters["@Yapilacak7"].Value = txt7.Text;

            sqCom.Parameters.Add("@Yapilacak8", SqlDbType.NVarChar);
            sqCom.Parameters["@Yapilacak8"].Value = txt8.Text;

            sqCom.Parameters.Add("@Yapilacak9", SqlDbType.NVarChar);
            sqCom.Parameters["@Yapilacak9"].Value = txt9.Text;

            sqCom.Parameters.Add("@Yapilacak10", SqlDbType.NVarChar);
            sqCom.Parameters["@Yapilacak10"].Value = txt10.Text;

            sqCom.ExecuteNonQuery();

            sqlConn.Close();

            MessageBox.Show("Duyuru Güncellendi ! ");
            this.Close();
        }
    }
}
