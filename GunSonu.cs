using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace UrunTakipSistemi
{
    public partial class GunSonu : Form
    {
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=UrunTakipSistemiVeriTabani.mdb");
        OleDbCommand komut;
        OleDbDataAdapter adtr;
        DataSet tablo = new DataSet();

        public GunSonu()
        {
            InitializeComponent();
        }

        private void gunsonu_GunuBitirButon_Click(object sender, EventArgs e)
        {

            /*
                burda urunlerin hepsini temizlemek için kullanılıyor tum verileri siliyor
             */
            baglanti.Open();
            string gunuTemizleme = "DELETE * FROM gunSonu";
            komut = new OleDbCommand(gunuTemizleme, baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();

            tablo.Clear();
            baglanti.Open();
            komut = new OleDbCommand("select * from gunSonu ", baglanti);
            adtr = new OleDbDataAdapter(komut);
            adtr.Fill(tablo, "gunSonu");
            gunSonuData.DataSource = tablo.Tables["gunSonu"];
            baglanti.Close();

        }


        private void gunsonu_AnasayfaDonButon_Click(object sender, EventArgs e)
        {
            GirisEkrani GE = new GirisEkrani();
            GE.Show();
            this.Hide();
        }

        private void gunsonu_ListeleButon_Click(object sender, EventArgs e)
        {
            tablo.Clear();
            baglanti.Open();
            komut = new OleDbCommand("select * from gunSonu ", baglanti);
            adtr = new OleDbDataAdapter(komut);
            adtr.Fill(tablo, "gunSonu");
            gunSonuData.DataSource = tablo.Tables["gunSonu"];
            baglanti.Close();
        }

        private void gunsonu_HesaplaButon_Click(object sender, EventArgs e)
        {
          
            OleDbCommand depoKomut = new OleDbCommand("SELECT * FROM Depo", baglanti);
            DataTable depoTablo = new DataTable();
            adtr.SelectCommand = depoKomut;
            adtr.Fill(depoTablo);


            /*
              burda gun sonunda ne kadar toplam para kazandıgını utun toplamlarını toplayarak yapıyor

           */
            int tumTopla = 0;
            int dataIcinSayim = gunSonuData.Rows.Count - 1;
            int saydir;
            for (saydir = 0; saydir < dataIcinSayim; saydir++)
            {
                tumTopla = tumTopla + Convert.ToInt32(gunSonuData.Rows[saydir].Cells[6].Value.ToString());
            }


            /*
                burda depodaki alış fiyatından sepetteki satış fiyatını cıkarıp urunu adeti kadar carpıp karını hesaplıyor 
                ve barkod a göre sorguluyorum.
             */
            int i ;
            int karToplam = 0;
            int depoIcinSayim = depoTablo.Rows.Count ;
            for (i = 0; i < dataIcinSayim; i++)
            {
                
                int x= 0;
                for (x = 0; x < depoIcinSayim; x++)
                {
                    if (Convert.ToInt32(gunSonuData.Rows[i].Cells[0].Value.ToString()) == Convert.ToInt32(depoTablo.Rows[x][0].ToString()))
                    {
                        
                        
                        karToplam = karToplam + ((Convert.ToInt32(gunSonuData.Rows[i].Cells[5].Value.ToString()) - Convert.ToInt32(depoTablo.Rows[x][5].ToString())) * Convert.ToInt32(gunSonuData.Rows[i].Cells[4].Value.ToString()));
                    }
                }
                

            }

            gunlukSatisLabel.Text = Convert.ToString(tumTopla);
            gunlukKarLabel.Text = Convert.ToString(karToplam);
        }
    }
}
