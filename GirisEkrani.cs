using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UrunTakipSistemi
{
    public partial class GirisEkrani : Form
    {
        public GirisEkrani()
        {
            InitializeComponent();
        }

        private void UrunBilgisibutton_Click(object sender, EventArgs e)
        {
            UrunBilgisi UB = new UrunBilgisi();
            UB.Show();
            this.Hide();
        }

        private void depoGirisButon_Click(object sender, EventArgs e)
        {
            Depo DP = new Depo();
            DP.Show();
            this.Hide();
        }

        private void dukkanGirisButon_Click(object sender, EventArgs e)
        {
            Dukkan DK = new Dukkan();
            DK.Show();
            this.Hide();
        }

        private void gunSonuGirisButon_Click(object sender, EventArgs e)
        {
            GunSonu GS = new GunSonu();
            GS.Show();
            this.Hide();
        }
    }
}
