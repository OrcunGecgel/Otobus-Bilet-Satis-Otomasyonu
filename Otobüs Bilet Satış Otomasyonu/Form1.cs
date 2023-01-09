using Otobüs_Bilet_Satış_Otomasyonu.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Otobüs_Bilet_Satış_Otomasyonu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void cmbOtobus_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(cmbOtobus.Text)
            {
                case "Travego": KoltukDoldur(8, false);
                    break;
                case "Setra":KoltukDoldur(12, true);
                    break;
                case "Neoplan":KoltukDoldur(10, false);
                    break;
            }
            void KoltukDoldur(int sira,bool arkaBesliMİ)
            {
                yavaslat:
                foreach (Control ctrl in this.Controls)
                {
                    if(ctrl is Button)
                    {
                        Button btn = ctrl as Button;
                        if(btn.Text == "Kaydet")
                        {
                            continue;
                        }
                        else
                        {
                            this.Controls.Remove(ctrl);
                            goto yavaslat;
                        }
                    }
                }
                int koltukNo = 1;
                for (int i = 0; i < sira; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if(arkaBesliMİ==true)
                        {
                            if(i!=sira-1&&j==2)
                            {
                                continue;
                            }
                        }
                        else
                        {
                            if (j == 2)
                                continue;
                        }
                        
                        Button koltuk = new Button();
                        koltuk.Height = koltuk.Width = 40;
                        koltuk.Top = 30 + (i * 45);
                        koltuk.Left = 5 + (j * 45);
                        koltuk.Text = koltukNo.ToString();
                        koltukNo++;
                        koltuk.ContextMenuStrip = contextMenuStrip1;
                        koltuk.MouseDown += koltuk_MouseDown;
                        this.Controls.Add(koltuk);
                    }
                }
            }
        }
        Button tiklanan;
        private void koltuk_MouseDown(object sender, MouseEventArgs e)
        {
            tiklanan = sender as Button;
        }

        private void rezerveEtToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if(cmbOtobus.SelectedIndex == -1 || cmbNereden.SelectedIndex== -1 || cmbNereye.SelectedIndex==-1)
            {
                MessageBox.Show("Lütfen Önce Gerekli Alanları Doldurun");
                    return;
            }
            kayıtFormu kf = new kayıtFormu();
            DialogResult sonuc = kf.ShowDialog();
            if(sonuc == DialogResult.OK)
            {
                ListViewItem lvi=new ListViewItem();
                lvi.Text = String.Format("{0} {1}", kf.txtİsim.Text, kf.txtSoyisim.Text);
                lvi.SubItems.Add(kf.mskdTelefon.Text);
                if(kf.rdbBay.Checked)
                {
                    lvi.SubItems.Add("BAY");
                    tiklanan.BackColor = Color.Blue;
                }
                if(kf.rdbBayan.Checked)
                {
                    lvi.SubItems.Add("BAYAN");
                    tiklanan.BackColor = Color.Red;
                }
                lvi.SubItems.Add(cmbNereden.Text);
                lvi.SubItems.Add(cmbNereye.Text);
                lvi.SubItems.Add(tiklanan.Text);
                lvi.SubItems.Add(dtpTarih.Text);
                lvi.SubItems.Add(nudFiyat.Value.ToString());
                listView1.Items.Add(lvi);

            }
        }
    }
}
