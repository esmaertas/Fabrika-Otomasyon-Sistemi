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
    public partial class frmKilitTarih : Form
    {
        public frmKilitTarih()
        {
            InitializeComponent();
        }

        private void frmKilitTarih_Load(object sender, EventArgs e)
        {
            SqlConnection sqlConn = new SqlConnection();
            sqlConn.ConnectionString = frmGiris.Database;
            sqlConn.Open();

            SqlCommand sqCom = new SqlCommand();
            sqCom.Connection = sqlConn;
            SqlDataAdapter da = new SqlDataAdapter();

            sqCom.CommandText = "SELECT AyarID, KilitTarih FROM bakAyarlar WHERE AyarID=1";
            sqCom.ExecuteNonQuery();

            DataTable dtProd = new DataTable();
            SqlDataAdapter sqDa = new SqlDataAdapter();
            sqDa.SelectCommand = sqCom;
            sqlConn.Close();
            sqDa.Fill(dtProd);

            dateTimePicker1.Value = Convert.ToDateTime(dtProd.Rows[0]["KilitTarih"]);
        }

        private void btnKilit_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult =
                MessageBox.Show(
                    "Tarih Güncellenecek. Bu tarihten öncesinde değişiklik yapılmayacak ! \nEminseniz Evet butonuna basınız.",
                    "DİKKAT !", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult==DialogResult.Yes)
            {
                SqlConnection sqlConn = new SqlConnection();
                sqlConn.ConnectionString = frmGiris.Database;
                sqlConn.Open();

                SqlCommand sqCom = new SqlCommand();
                sqCom.Connection = sqlConn;
                sqCom.CommandText = "UPDATE bakAyarlar SET KilitTarih=@KilitTarih WHERE AyarID=1";

                sqCom.Parameters.Add("@KilitTarih", SqlDbType.Date);
                sqCom.Parameters["@KilitTarih"].Value = dateTimePicker1.Value;

                sqCom.ExecuteNonQuery();

                sqlConn.Close();
                MessageBox.Show("Kayıt Güncellendi ! ");
            }
            else if ( dialogResult == DialogResult.No)
            {
                
            }
        }
    }
}
