using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BakimTakipProgrami
{
    public partial class frmRapor04 : Form
    {
        public frmRapor04()
        {
            InitializeComponent();
        }

        public static int grupyetki;

        private void frmRapor04_Load(object sender, EventArgs e)
        {
            grupyetki = Convert.ToInt32(frmGiris.gonderilecekyetki);
            if (grupyetki!=0)
            {
                btnAktar.Visible = true;
            }

            DateTime date = DateTime.Now;
            var FirstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var LastDayOfMonth = FirstDayOfMonth.AddMonths(1).AddDays(-1);
            dateTimePicker1.Value = FirstDayOfMonth;
            date = DateTime.Now.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            dateTimePicker2.Value = date;
        }

        private void btnRaporla_Click(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value <= dateTimePicker2.Value)
            {
                veriListele();
            }
            else
            {
                MessageBox.Show("Bitiş tarihi başlangıç tarihinden küçük olamaz. Lütfen düzeltiniz!");
            }
        }

        public void veriListele()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                dataGridView1.DataSource = null;
                SqlConnection sqlConn = new SqlConnection();
                sqlConn.ConnectionString = frmGiris.Database;
                sqlConn.Open();

                SqlCommand sqcom = new SqlCommand();
                sqcom.Connection = sqlConn;
                SqlDataAdapter da = new SqlDataAdapter();

                sqcom.CommandText = @"SELECT Tbl2.Bolum AS [Bölüm]
, SUM(Tbl1.Durus) AS[Duruş(dk.)]
, CONCAT(((SUM(Tbl1.Durus)) * 1) / 60, ' saat '
, (SUM(Tbl1.Durus)) % 60, ' dk.') as [Duruş Saat, Dakika]
FROM bakArizalar Tbl1
LEFT JOIN bakBolumler Tbl2 ON Tbl1.Bolum = Tbl2.BolumID 
WHERE Tbl1.Tarih BETWEEN convert(datetime, '" + dateTimePicker1.Value + "', 105) AND convert(datetime, '" + dateTimePicker2.Value + "', 105) GROUP BY Tbl2.Bolum ORDER BY Tbl2.Bolum";
                sqcom.ExecuteNonQuery();

                DataTable dtProd = new DataTable();
                SqlDataAdapter sqDa = new SqlDataAdapter();
                sqDa.SelectCommand = sqcom;
                sqlConn.Close();
                sqDa.Fill(dtProd);

                dataGridView1.DataSource = dtProd;

                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    int widthCol = dataGridView1.Columns[i].Width;
                    dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    dataGridView1.Columns[i].Width = widthCol;
                }

                dataGridView1.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
                Cursor.Current = Cursors.Default;
            }
            catch
            {

            }
        }

        private void btnAktar_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = "Bölüm Olarak Duruş Süreleri.xls";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                ToCsV(dataGridView1, sfd.FileName);
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Excel dosyası oluşturuldu.");
            }
        }
        private void ToCsV(DataGridView dGV, string filename)
        {
            string stOutput = "";
            string sHeaders = "";

            for (int j = 0; j < dGV.Columns.Count; j++)
                sHeaders = sHeaders.ToString() + Convert.ToString(dGV.Columns[j].HeaderText) + "\t";
            stOutput += sHeaders + "\r\n";

            for (int i = 0; i < dGV.RowCount; i++)
            {
                string stLine = "";
                for (int j = 0; j < dGV.Rows[i].Cells.Count; j++)
                    stLine = stLine.ToString() + Convert.ToString(dGV.Rows[i].Cells[j].Value) + "\t";
                stOutput += stLine + "\r\n";
            }
            Encoding utf16 = Encoding.GetEncoding(1254);
            byte[] output = utf16.GetBytes(stOutput);
            FileStream fs = new FileStream(filename, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(output, 0, output.Length);
            bw.Flush();
            bw.Close();
            fs.Close();
        }
    }
}
