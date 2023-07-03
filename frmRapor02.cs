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
    public partial class frmRapor02 : Form
    {
        public frmRapor02()
        {
            InitializeComponent();
        }

        public static int grupyetki;

        private void frmRapor02_Load(object sender, EventArgs e)
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
            date= DateTime.Now.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
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

                sqcom.CommandText = @"SELECT Tbl5.PersonelAdi AS [Personel Adı], DMG1.GUN AS [Gün Adedi]
, SUM(case when Tbl1.Islem = 1 then Tbl1.ToplamSure else 0 end) as [ARIZA(dk.)]
, SUM(case when Tbl1.Islem = 2 then Tbl1.ToplamSure else 0 end) as [FAALIYET(dk.)]
, (SUM(case when Tbl1.Islem = 1 then Tbl1.ToplamSure else 0 end) + SUM(case when Tbl1.Islem = 2
then Tbl1.ToplamSure else 0 end)) as [TOPLAM(dk.)] ,CONCAT((((SUM(case when Tbl1.Islem = 1
then(Tbl1.ToplamSure) else 0 end) + SUM(case when Tbl1.Islem = 2
then(Tbl1.ToplamSure) else 0 end)) * 1) / 60), ' saat ' , (((SUM(case when Tbl1.Islem = 1
then(Tbl1.ToplamSure) else 0 end) + SUM(case when Tbl1.Islem = 2
then(Tbl1.ToplamSure) else 0 end))) % 60), ' dk.') as [TOPLAM Saat, Dakika]
, (DMG1.GUN * 7.5) AS[MaxSaat] , ROUND(CAST(((((SUM(case when Tbl1.Islem = 1
then Tbl1.ToplamSure else 0 end) + SUM(case when Tbl1.Islem = 2
then Tbl1.ToplamSure else 0 end) )) * 100) / (DMG1.GUN * 450)) AS FLOAT), 2) AS[Yüzde Oran]
FROM bakArizalar Tbl1 LEFT JOIN bakTur Tbl3 ON Tbl1.Tur = Tbl3.TurID
LEFT JOIN(SELECT ArizaID, LTRIM(RTRIM(m.n.value('.[1]', 'varchar(8000)'))) AS Personel
FROM(SELECT ArizaID, CAST('<XMLRoot><RowData>' + REPLACE(Personel, '-', '</RowData><RowData>') + '</RowData></XMLRoot>' AS XML) AS x
FROM  bakArizalar)t CROSS APPLY x.nodes('/XMLRoot/RowData')m(n)) Tbl4 ON Tbl1.ArizaID = Tbl4.ArizaID
LEFT JOIN bakPersoneller Tbl5 ON Tbl5.PersonelID = Tbl4.Personel
LEFT JOIN bakIslem Tbl6 ON Tbl1.Islem = Tbl6.IslemID
LEFT JOIN(SELECT DMG.Personel, COUNT(DMG.Personel) AS GUN
FROM(SELECT DISTINCT Tbl4.Personel, DAY(Tbl1.Tarih) AS GUN
, MONTH(Tbl1.Tarih) AS AY, YEAR(Tbl1.Tarih) AS YIL
FROM[dbo].[bakArizalar] Tbl1
LEFT JOIN(SELECT ArizaID, LTRIM(RTRIM(m.n.value('.[1]', 'varchar(8000)'))) AS Personel
FROM(SELECT ArizaID, CAST('<XMLRoot><RowData>' + REPLACE(Personel
, '-', '</RowData><RowData>') + '</RowData></XMLRoot>' AS XML) AS x
FROM  bakArizalar)t CROSS APPLY x.nodes('/XMLRoot/RowData')m(n)) Tbl4 ON Tbl1.ArizaID = Tbl4.ArizaID
LEFT JOIN bakPersoneller Tbl5 ON Tbl5.PersonelID = Tbl4.Personel
WHERE Tbl1.Tarih BETWEEN convert(datetime, '" + dateTimePicker1.Value + "', 105) AND convert(datetime, '" +
                                    dateTimePicker2.Value +
                                    "', 105)GROUP BY Tbl4.Personel, Tbl5.PersonelAdi, DAY(Tbl1.Tarih), MONTH(Tbl1.Tarih), YEAR(Tbl1.Tarih)) AS DMG GROUP BY DMG.Personel) AS DMG1 ON DMG1.Personel = Tbl5.PersonelID WHERE Tbl1.Tarih BETWEEN convert(datetime, '" +
                                    dateTimePicker1.Value + "', 105) AND convert(datetime, '" + dateTimePicker2.Value +
                                    "', 105) GROUP BY Tbl5.PersonelAdi, DMG1.GUN ORDER BY Tbl5.PersonelAdi ASC";
                sqcom.ExecuteNonQuery();
                DataTable dtProd = new DataTable();
                SqlDataAdapter sqDa = new SqlDataAdapter();
                sqDa.SelectCommand = sqcom;
                sqlConn.Close();
                sqDa.Fill(dtProd);
                dataGridView1.DataSource = dtProd;
                btnResize();
                RowsColor();
                dataGridView1.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            catch
            {

            }

        }

        private void btnResize()
        {
            dataGridView1.AutoResizeColumns();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        public void RowsColor()
        {
            for (int i=0; i < dataGridView1.Rows.Count; i++)
            {
                double val = Convert.ToByte(dataGridView1.Rows[i].Cells[7].Value);
                if (val < 107)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }
            }
        }

        private void btnAktar_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = "Personel Detaylı    Arıza ve Faaliyet Süreleri.xls";
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
        private void btnOnceki_Click(object sender, EventArgs e)
        {
            DateTime date = dateTimePicker1.Value;
            DateTime date2 = dateTimePicker2.Value;
            date = dateTimePicker1.Value.AddDays(-1).AddHours(00).AddMinutes(00).AddSeconds(00);
            dateTimePicker1.Value = date;
            date = date.AddHours(23).AddMinutes(59).AddSeconds(59);
            dateTimePicker2.Value = date2;

            veriListele();
        }

        private void btnSonraki_Click(object sender, EventArgs e)
        {
            DateTime date = dateTimePicker1.Value;
            DateTime date2 = dateTimePicker2.Value;
            date = dateTimePicker1.Value.AddDays(1).AddHours(00).AddMinutes(00).AddSeconds(00);
            dateTimePicker1.Value = date;
            date = date.AddHours(23).AddMinutes(59).AddSeconds(59);
            dateTimePicker2.Value = date2;
            veriListele();
        }
    }
}
