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
    public partial class frmRapor11 : Form
    {
        public frmRapor11()
        {
            InitializeComponent();
        }
        public static int grupyetki;

        private void frmRapor11_Load(object sender, EventArgs e)
        {
            grupyetki = Convert.ToInt32(frmGiris.gonderilecekyetki);
            if (grupyetki != 0)
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

        private void veriListele()
        {
            try
            {
                dataGridView1.DataSource = null;

                SqlConnection sqlConn = new SqlConnection();
                sqlConn.ConnectionString = frmGiris.Database;

                sqlConn.Open();

                SqlCommand sqcom = new SqlCommand();
                sqcom.Connection = sqlConn;

                sqcom.CommandText = @"SELECT bakMakineler.MakineAdi as [Makine Adı]
, CONVERT(VARCHAR(10),Tbl1.Tarih,105) AS Tarih, bakTur.Turu as [Bakım Türü]
, bakVardiyalar.Vardiya as [Vardiyası], TblN.Isimler as [Bakımı Yapan Personel Adı/Soyadı]
, CONVERT(VARCHAR(5),Tbl1.BaslamaSaati,108) AS [Başlama Saati]
, CONVERT(VARCHAR(5),Tbl1.BitisSaati,108) AS [Bitiş Saati]
, Tbl1.Durus as [Toplam Duruş (dk)], bakIslem.IslemTuru as [Arıza Türü]
, Tbl1.Yapilanlar as [Yapılan İşlemler] FROM bakArizalar Tbl1
INNER JOIN bakMakineler ON Tbl1.Makine = bakMakineler.MakineID
INNER JOIN bakTur ON Tbl1.Tur = bakTur.TurID
INNER JOIN bakVardiyalar ON Tbl1.Vardiya = bakVardiyalar.VardiyaID
INNER JOIN bakIslem ON Tbl1.Islem = bakIslem.IslemID
LEFT JOIN(SELECT DISTINCT o.ArizaID
, Isimler = STUFF((SELECT ', ' + b.PersonelAdi
FROM(SELECT ArizaID, LTRIM(RTRIM(m.n.value('.[1]', 'varchar(8000)'))) AS Personel
FROM(SELECT ArizaID, CAST('<XMLRoot><RowData>' + REPLACE(Personel
, '-', '</RowData><RowData>') + '</RowData></XMLRoot>' AS XML) AS x
FROM bakArizalar)t CROSS APPLY x.nodes('/XMLRoot/RowData')m(n)) AS a
INNER JOIN[dbo].[bakPersoneller] AS b ON a.Personel = b.PersonelID
WHERE a.ArizaID = o.ArizaID FOR XML PATH, TYPE).value(N'.[1]', N'varchar(max)'), 1, 2, '')
FROM (SELECT ArizaID, LTRIM(RTRIM(m.n.value('.[1]', 'varchar(8000)'))) AS Personel
FROM (SELECT ArizaID, CAST('<XMLRoot><RowData>' + REPLACE(Personel
, '-', '</RowData><RowData>') + '</RowData></XMLRoot>' AS XML) AS x
FROM bakArizalar)t CROSS APPLY x.nodes('/XMLRoot/RowData')m(n)) AS o) AS TblN
ON Tbl1.ArizaID = TblN.ArizaID
WHERE Tbl1.Islem = 1 AND Tbl1.Tarih BETWEEN convert(datetime, '" + dateTimePicker1.Value + "', 105) AND convert(datetime, '" + dateTimePicker2.Value + "', 105) ORDER BY Tbl1.ArizaID ASC";
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
                    int widthcol = dataGridView1.Columns[i].Width;
                    dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    dataGridView1.Columns[i].Width = widthcol;
                }
            }
            catch
            {

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

        private void btnAktar_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = "Mekanik-Elektrik Arıza Kayıt Formu.xls";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                ToCsV(dataGridView1, sfd.FileName);
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Excel dosyası oluşturuldu.");
            }
        }
    }
}
