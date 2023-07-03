using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BakimTakipProgrami
{
    public partial class frmRapor01 : Form
    {
        public frmRapor01()
        {
            InitializeComponent();
        }

        public static int grupyetki;

        private void frmRapor01_Load(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;
            var FirsDateOfMonth = new DateTime(date.Year, date.Month, 1);
            var LastDayOfMonth = FirsDateOfMonth.AddMonths(1).AddDays(-1);
            dateTimePicker1.Value = FirsDateOfMonth;
            date = DateTime.Now.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            dateTimePicker2.Value = date;

            grupyetki = Convert.ToInt32(frmGiris.gonderilecekyetki);
            if (grupyetki != 0)
            {
                btnAktar.Visible = true;
            }
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
                dataGridView1.DataSource = null;
                SqlConnection sqlConn = new SqlConnection();
                sqlConn.ConnectionString = frmGiris.Database;
                sqlConn.Open();

                SqlCommand sqcom = new SqlCommand();
                sqcom.Connection = sqlConn;

                sqcom.CommandText = @"SELECT Tbl5.PersonelAdi
,CONCAT(((SUM(case when Tbl1.Islem = 1 then(Tbl1.ToplamSure) else 0 end) * 1) / 60), ' saat '
, ((SUM(case when Tbl1.Islem = 1 then(Tbl1.ToplamSure) else 0 end)) % 60), ' dk.') as [Ariza Saat, Dakika] 
,CONCAT(((SUM(case when Tbl1.Islem = 1 and FormVar = 0 
and Tbl1.Tarih>convert(datetime, '01.01.2019', 105) 
then (Tbl1.ToplamSure * -1) else 0 end) * 1) / 60), ' saat '
, ((SUM(case when Tbl1.Islem = 1  and FormVar = 0 
and Tbl1.Tarih>convert(datetime, '01.01.2019', 105) 
then(Tbl1.ToplamSure * -1) else 0 end)) % 60), ' dk.') as [Formsuz Arıza Saat, Dakika] 
,CONCAT(((SUM(case when Tbl1.Islem = 1 
then (Tbl1.ToplamSure) else 0 end) + SUM(case when Tbl1.Islem = 1 and FormVar = 0 
and Tbl1.Tarih>convert(datetime, '01.01.2019', 105) 
then (Tbl1.ToplamSure * -1) else 0 end)) * 1) / 60, ' saat ' 
, (SUM(case when Tbl1.Islem = 1 then (Tbl1.ToplamSure) 
else 0 end) + SUM(case when Tbl1.Islem = 1 and FormVar = 0 
and Tbl1.Tarih>convert(datetime, '01.01.2019', 105) 
then (Tbl1.ToplamSure * -1) else 0 end)) % 60, ' dk.') as [Geçerli Arıza Saat, Dakika] 
,CONCAT(((SUM(case when Tbl1.Islem = 2 then(Tbl1.ToplamSure) else 0 end) * 1) / 60), ' saat '
, ((SUM(case when Tbl1.Islem = 2 then(Tbl1.ToplamSure) 
else 0 end)) % 60), ' dk.') as [Faaliyet Saat, Dakika] 
,CONCAT(((SUM(case when Tbl1.Islem = 1 then(Tbl1.ToplamSure) 
else 0 end) + SUM(case when Tbl1.Islem = 1 and FormVar = 0 
and Tbl1.Tarih>convert(datetime, '01.01.2019', 105) 
then (Tbl1.ToplamSure * -1) else 0 end) + SUM(case when Tbl1.Islem = 2 
then (Tbl1.ToplamSure) else 0 end)) * 1) / 60, ' saat ' 
,(SUM(case when Tbl1.Islem = 1 then(Tbl1.ToplamSure) 
else 0 end) + SUM(case when Tbl1.Islem = 1 and FormVar = 0 
and Tbl1.Tarih>convert(datetime, '01.01.2019', 105) 
then (Tbl1.ToplamSure * -1) else 0 end) + SUM(case when Tbl1.Islem = 2 
then (Tbl1.ToplamSure) else 0 end)) % 60, ' dk.') as [TOPLAM Saat, Dakika]  
FROM bakArizalar Tbl1 LEFT JOIN bakTur Tbl3 ON Tbl1.Tur = Tbl3.TurID 
LEFT JOIN (SELECT ArizaID, LTRIM(RTRIM(m.n.value('.[1]', 'varchar(8000)'))) AS Personel 
FROM(SELECT ArizaID, CAST('<XMLRoot><RowData>' + REPLACE(Personel, '-', '</RowData><RowData>') + '</RowData></XMLRoot>' AS XML) AS x FROM  bakArizalar)t 
CROSS APPLY x.nodes('/XMLRoot/RowData')m(n)) Tbl4 ON Tbl1.ArizaID = Tbl4.ArizaID 
LEFT JOIN bakPersoneller Tbl5 ON Tbl5.PersonelID = Tbl4.Personel 
LEFT JOIN bakIslem Tbl6 ON Tbl1.Islem = Tbl6.IslemID 
WHERE Tbl1.Tarih BETWEEN convert(datetime, '" + dateTimePicker1.Value + "', 105) " +
                                    "AND convert(datetime, '" + dateTimePicker2.Value +
                                    "', 105) GROUP BY Tbl5.PersonelAdi " +
                                    "ORDER BY Tbl5.PersonelAdi ASC";
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
                    int widthColl = dataGridView1.Columns[i].Width;
                    dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    dataGridView1.Columns[i].Width = widthColl;
                    dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.Automatic;
            }
            catch
            {

            }
        }

        private void btnAktar_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = "Personel Arıza ve Faaliyet Süreleri.xls";
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
            stOutput += "\r\n";
            for (int i = 0; i < dGV.RowCount; i++)
            {
                string stLine = ";";
                for (int j = 0; j < dGV.Rows[i].Cells.Count; j++)
                    stLine = stLine.ToString() + Convert.ToString(dGV.Rows[i].Cells[i].Value) + "\t";
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
