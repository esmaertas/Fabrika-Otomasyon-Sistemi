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
    public partial class frmPersonelGorevlendirme : Form
    {
        string connectionString = @"server=.;database=BakimTakip4;integrated security=SSPI";
        // Veritabanı bağlantı dizesini buraya ekleyin

        public frmPersonelGorevlendirme()
        {
            InitializeComponent();
        }

        private void cmbPersonel_TextChanged(object sender, EventArgs e)
        {

        }
        private void LoadComboBox1()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT DISTINCT Tbl2.PersonelAdi,Tbl2.PersonelID, Tbl1.MakineID, Tbl1.MakineAdi  FROM bakMakineler Tbl1 LEFT JOIN bakPersoneller Tbl2 ON Tbl1.PersonelID=Tbl2.PersonelID";
                    SqlCommand command = new SqlCommand(query, connection);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbPersonel.Items.Add(reader["PersonelAdi"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata1: " + ex.Message);
            }
        }

        private void cmbPersonel_SelectedIndexChanged(object sender, EventArgs e)
        {

            //string selectedPersonel = cmbPersonel.SelectedItem.ToString();
            //LoadComboBox2(selectedPersonel);

            string selectedPersonel = cmbPersonel.SelectedItem?.ToString();
            if (selectedPersonel != null)
            {
                LoadComboBox2(selectedPersonel);
            }



        }
        private void LoadComboBox2(string personelAdi)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT MakineAdi FROM bakMakineler WHERE PersonelAdi = @PersonelAdi";
                    SqlCommand command = new SqlCommand(query, connection);



                    command.Parameters.AddWithValue("@PersonelAdi", personelAdi);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        cmbMakine.Items.Clear();
                        while (reader.Read())
                        {
                            cmbMakine.Items.Add(reader["MakineAdi"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata2: " + ex.Message);
            }
        }

        private void frmPersonelGorevlendirme_Load(object sender, EventArgs e)
        {
            LoadComboBox1();
        }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            cmbMakine.SelectedIndex = -1;
            cmbPersonel.SelectedIndex = -1;
            chkCozuldu.Checked = false;
        }
    }
}


