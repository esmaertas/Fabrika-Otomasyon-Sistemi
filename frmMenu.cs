using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BakimTakipProgrami
{
    public partial class frmMenu : Form
    {
        public frmMenu()
        {
            InitializeComponent();
        }

        public static int yetki;

        private void cikisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void basamaklaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void yatayOlarakToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void dikeyOlarakToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void tumPencereleriKucultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form frm  in this.MdiChildren)
            {
                frm.WindowState = FormWindowState.Minimized;
            }
        }

        private void frmMenu_Load(object sender, EventArgs e)
        {
            this.Text = frmGiris.gonderilecekveri;
            yetki = Convert.ToInt32(frmGiris.gonderilecekyetki);
        }

        private void hakkindaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOpener<frmHakkinda>.Open(this);
        }

        private void personelEkleDegistirSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOpener<frmPersoneller>.Open(this);
        }

        private void arızaKodlariEkleDegistirSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOpener<frmArizaKodlari>.Open(this);
        }

        private void makinelerEkleDegistirSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOpener<frmMakineler>.Open(this);
        }

        private void bölümEkleDegistirSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOpener<frmBolumler>.Open(this);
        }

        private void sifreDegisikligiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOpener<frmSifreDegisikligi>.Open(this);
        }

        private void tarihKilitleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOpener<frmKilitTarih>.Open(this);
        }

        private void onerilerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOpener<frmOneriler>.Open(this);
        }

        private void duyurularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOpener<frmDuyurular>.Open(this);
        }

        private void isPlanlariToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOpener<frmIsPlanlari>.Open(this);
        }

        private void periyodikBakimToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOpener<frmPeriyodikBakim>.Open(this);
        }

        private void bakimlarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOpener<frmBakim>.Open(this);
        }

        private void personelArizaVeFaaliyetSureleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOpener<frmRapor01>.Open(this);
        }

        private void personelDetayliArizaVeFaaliyetSureleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOpener<frmRapor02>.Open(this);
        }

        private void vardiyaIleMudahaleSaatleriUymayanlarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOpener<frmRapor03>.Open(this);
        }

        private void bolumOlarakDurusSureleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOpener<frmRapor04>.Open(this);
        }

        private void arızaKodlarınaGoreDurusSureleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOpener<frmRapor05>.Open(this);
        }

        private void bolumMakinaArizaKodlarinaGoreDurusSuresiAdetleriRaporuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOpener<frmRapor06>.Open(this);
        }

        private void arizaKodlarinaGoreMudahaleSuresiAdetleriRaporuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOpener<frmRapor07>.Open(this);
        }

        private void bolumMakinaArizaKodlarinaMudahaleSuresiAdetleriRaporuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOpener<frmRapor08>.Open(this);
        }

        private void formPersonelGorevlendirmeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOpener<frmPersonelGorevlendirme>.Open(this);
        }

        private void arizaVeFaaliyetRaporlariToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOpener<frmRapor09>.Open(this);
        }

        private void periyodikBakimKayitFormuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOpener<frmRapor10>.Open(this);
        }

        private void mekanikElektrikArizaKayitFormuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOpener<frmRapor11>.Open(this);
        }

        private void personelMakine2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOpener<frmPersonelMakine2>.Open(this);
        }
    }

    public static class FormOpener<T> where T : Form
    {
        public static void Open(Form mdiContainer)
        {
            foreach (Form SelectedFrm in mdiContainer.MdiChildren)
            {
                if (SelectedFrm is T)
                {
                    SelectedFrm.Activate();
                    return;
                }
            }

            T frm = (T)Activator.CreateInstance(typeof(T));
            frm.MdiParent = mdiContainer;
            frm.Show();
        }
    }
}
