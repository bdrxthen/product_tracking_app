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
    public partial class Dukkan : Form
    {
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=UrunTakipSistemiVeriTabani.mdb");
        OleDbCommand komut;
        OleDbDataAdapter adtr;
        DataSet tablo = new DataSet();
        public Dukkan()
        {
            InitializeComponent();
        }

        private void dukkan_AnasayfaDonButon_Click(object sender, EventArgs e)
        {
            GirisEkrani GE = new GirisEkrani();
            GE.Show();
            this.Hide();
        }

        private void dukkan_AnasayfaDon2Buton_Click(object sender, EventArgs e)
        {
            GirisEkrani GE = new GirisEkrani();
            GE.Show();
            this.Hide();
        }

        private void dukkan_ListeleButon_Click(object sender, EventArgs e)
        {
            tablo.Clear();
            baglanti.Open();
            komut = new OleDbCommand("select * from Dukkan ", baglanti);
            adtr = new OleDbDataAdapter(komut);
            adtr.Fill(tablo, "Dukkan");
            dukkan_DukkanBilgileriData.DataSource = tablo.Tables["Dukkan"];
            baglanti.Close();
        }

        private void dukkan_Listele2Buton_Click(object sender, EventArgs e)
        {
            tablo.Clear();
            baglanti.Open();
            komut = new OleDbCommand("select * from Dukkan ", baglanti);
            adtr = new OleDbDataAdapter(komut);
            adtr.Fill(tablo, "Dukkan");
            dukkan_DukkanData.DataSource = tablo.Tables["Dukkan"];
            baglanti.Close();

            baglanti.Open();
            komut = new OleDbCommand("select * from Sepet ", baglanti);
            adtr = new OleDbDataAdapter(komut);
            adtr.Fill(tablo, "Sepet");
            dukkan_SepetData.DataSource = tablo.Tables["Sepet"];
            baglanti.Close();
        }

        private void dukkan_EkleButon_Click(object sender, EventArgs e)
        {
            /*
                dukkandaki urunu girilen miktar kadar sepet e ekliyor ve burda eger ki bu ürün sepette varsa adet attırıyor 
                yoksa girilen adet kadar ekliyor.
             */
            int i,varsa=0;
            for (i = 0; i < dukkan_SepetData.Rows.Count; i++)
            {
                if (Convert.ToInt32(dukkan_Barkod.Text) ==Convert.ToInt32(dukkan_SepetData.Rows[i].Cells[0].Value))
                {
                    varsa = 1;
                    break;
                }

            }

            int x;
            for (x = 0; x < dukkan_DukkanData.Rows.Count; x++)
            {
                if(Convert.ToInt32(dukkan_Barkod.Text) == Convert.ToInt32(dukkan_DukkanData.Rows[x].Cells[0].Value))
                {
                    break;
                }
            }

            if (varsa == 1)
            {
                string dukkanGuncelleme = "Update Dukkan Set Marka=@marka,Model=@model,Renk=@renk,Adet=@adet,Fiyat=@fiyat,Toplam=@toplam Where Barkod=@barkod";
                komut = new OleDbCommand(dukkanGuncelleme, baglanti);

                komut.Parameters.AddWithValue("@marka", dukkan_Marka.Text);
                komut.Parameters.AddWithValue("@model", dukkan_Model.Text);
                komut.Parameters.AddWithValue("@renk", dukkan_Renk.Text);
                komut.Parameters.AddWithValue("@adet", Convert.ToInt32(dukkan_DukkanData.Rows[x].Cells[4].Value.ToString()) - Convert.ToInt32(dukkan_Adet.Text));
                komut.Parameters.AddWithValue("@fiyat", Convert.ToDouble(dukkan_DukkanData.Rows[x].Cells[5].Value.ToString()));
                komut.Parameters.AddWithValue("@toplam", (Convert.ToInt32(dukkan_DukkanData.Rows[x].Cells[4].Value.ToString()) - Convert.ToInt32(dukkan_Adet.Text)) * (Convert.ToDouble(dukkan_DukkanData.Rows[x].Cells[5].Value.ToString())));
                komut.Parameters.AddWithValue("@barkod", Convert.ToInt32(dukkan_Barkod.Text));

                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();

                string sepetGuncelleme = "Update Sepet Set Marka=@marka,Model=@model,Renk=@renk,Adet=@adet,Fiyat=@fiyat,Toplam=@toplam Where Barkod=@barkod";
                komut = new OleDbCommand(sepetGuncelleme, baglanti);

                komut.Parameters.AddWithValue("@marka", dukkan_Marka.Text);
                komut.Parameters.AddWithValue("@model", dukkan_Model.Text);
                komut.Parameters.AddWithValue("@renk", dukkan_Renk.Text);
                komut.Parameters.AddWithValue("@adet", Convert.ToInt32(dukkan_SepetData.Rows[i].Cells[4].Value.ToString()) + Convert.ToInt32(dukkan_Adet.Text));
                komut.Parameters.AddWithValue("@fiyat", Convert.ToDouble(dukkan_SepetData.Rows[i].Cells[5].Value.ToString()));
                komut.Parameters.AddWithValue("@toplam", (Convert.ToInt32(dukkan_SepetData.Rows[i].Cells[4].Value.ToString()) + Convert.ToInt32(dukkan_Adet.Text)) * (Convert.ToDouble(dukkan_SepetData.Rows[i].Cells[5].Value.ToString())));
                komut.Parameters.AddWithValue("@barkod", Convert.ToInt32(dukkan_Barkod.Text));

                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();

                tablo.Clear();
                baglanti.Open();
                komut = new OleDbCommand("select * from Dukkan ", baglanti);
                adtr = new OleDbDataAdapter(komut);
                adtr.Fill(tablo, "Dukkan");
                dukkan_DukkanData.DataSource = tablo.Tables["Dukkan"];
                baglanti.Close();

                baglanti.Open();
                komut = new OleDbCommand("select * from Sepet ", baglanti);
                adtr = new OleDbDataAdapter(komut);
                adtr.Fill(tablo, "Sepet");
                dukkan_SepetData.DataSource = tablo.Tables["Sepet"];
                baglanti.Close();

            }
            else
            {
          
              

                baglanti.Open();
                komut = new OleDbCommand("Insert into Sepet (Marka,Model,Renk,Adet,Fiyat,Toplam,Barkod) values ('"+dukkan_Marka.Text+ "','" + dukkan_Model.Text + "','" + dukkan_Renk.Text + "','" +Convert.ToInt32( dukkan_Adet.Text) + "','" +Convert.ToDouble( dukkan_Fiyat.Text )+ "','" + Convert.ToInt32(dukkan_Adet.Text)* Convert.ToDouble(dukkan_Fiyat.Text) + "','" +Convert.ToInt32( dukkan_Barkod.Text) + "')",baglanti);
                komut.ExecuteNonQuery();
                baglanti.Close();

                string dukkanGuncelleme = "Update Dukkan Set Marka=@marka,Model=@model,Renk=@renk,Adet=@adet,Fiyat=@fiyat,Toplam=@toplam Where Barkod=@barkod";
                komut = new OleDbCommand(dukkanGuncelleme, baglanti);

                komut.Parameters.AddWithValue("@marka", dukkan_Marka.Text);
                komut.Parameters.AddWithValue("@model", dukkan_Model.Text);
                komut.Parameters.AddWithValue("@renk", dukkan_Renk.Text);
                komut.Parameters.AddWithValue("@adet", Convert.ToInt32(dukkan_DukkanData.Rows[x].Cells[4].Value.ToString()) - Convert.ToInt32(dukkan_Adet.Text));
                komut.Parameters.AddWithValue("@fiyat", Convert.ToDouble(dukkan_DukkanData.Rows[x].Cells[5].Value.ToString()));
                komut.Parameters.AddWithValue("@toplam", (Convert.ToInt32(dukkan_DukkanData.Rows[x].Cells[4].Value.ToString()) - Convert.ToInt32(dukkan_Adet.Text)) * (Convert.ToDouble(dukkan_DukkanData.Rows[x].Cells[5].Value.ToString())));
                komut.Parameters.AddWithValue("@barkod", Convert.ToInt32(dukkan_Barkod.Text));

                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();

                tablo.Clear();
                baglanti.Open();
                komut = new OleDbCommand("select * from Dukkan ", baglanti);
                adtr = new OleDbDataAdapter(komut);
                adtr.Fill(tablo, "Dukkan");
                dukkan_DukkanData.DataSource = tablo.Tables["Dukkan"];
                baglanti.Close();

                baglanti.Open();
                komut = new OleDbCommand("select * from Sepet ", baglanti);
                adtr = new OleDbDataAdapter(komut);
                adtr.Fill(tablo, "Sepet");
                dukkan_SepetData.DataSource = tablo.Tables["Sepet"];
                baglanti.Close();

            }
        }

        private void dukkan_DukkanData_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            dukkan_Barkod.Text = dukkan_DukkanData.CurrentRow.Cells[0].Value.ToString();
            dukkan_Marka.Text = dukkan_DukkanData.CurrentRow.Cells[1].Value.ToString();
            dukkan_Model.Text = dukkan_DukkanData.CurrentRow.Cells[2].Value.ToString();
            dukkan_Renk.Text = dukkan_DukkanData.CurrentRow.Cells[3].Value.ToString();
            dukkan_Adet.Text = dukkan_DukkanData.CurrentRow.Cells[4].Value.ToString();
            dukkan_Fiyat.Text = dukkan_DukkanData.CurrentRow.Cells[5].Value.ToString();
        }

        private void dukkan_CikarButon_Click(object sender, EventArgs e)
        {
            /*
             sepetteki urunu dükkana geri çıkartıyor girdiğımız adet kadar ve sepetteki miktarı azaltıyor bunu da barkod sorgulayarak yaptık
             */
            int i, varsa = 0;
            for (i = 0; i < dukkan_SepetData.Rows.Count; i++)
            {
                if (Convert.ToInt32(dukkan_Barkod.Text) == Convert.ToInt32(dukkan_SepetData.Rows[i].Cells[0].Value))
                {
                    varsa = 1;
                    break;
                }

            }

            int x;
            for (x = 0; x < dukkan_DukkanData.Rows.Count; x++)
            {
                if (Convert.ToInt32(dukkan_Barkod.Text) == Convert.ToInt32(dukkan_DukkanData.Rows[x].Cells[0].Value))
                {
                    break;
                }
            }

            if (varsa == 1)
            {
                string dukkanGuncelleme = "Update Dukkan Set Marka=@marka,Model=@model,Renk=@renk,Adet=@adet,Fiyat=@fiyat,Toplam=@toplam Where Barkod=@barkod";
                komut = new OleDbCommand(dukkanGuncelleme, baglanti);

                komut.Parameters.AddWithValue("@marka", dukkan_Marka.Text);
                komut.Parameters.AddWithValue("@model", dukkan_Model.Text);
                komut.Parameters.AddWithValue("@renk", dukkan_Renk.Text);
                komut.Parameters.AddWithValue("@adet", Convert.ToInt32(dukkan_DukkanData.Rows[x].Cells[4].Value.ToString()) + Convert.ToInt32(dukkan_Adet.Text));
                komut.Parameters.AddWithValue("@fiyat", Convert.ToDouble(dukkan_DukkanData.Rows[x].Cells[5].Value.ToString()));
                komut.Parameters.AddWithValue("@toplam", (Convert.ToInt32(dukkan_DukkanData.Rows[x].Cells[4].Value.ToString()) + Convert.ToInt32(dukkan_Adet.Text)) * (Convert.ToDouble(dukkan_DukkanData.Rows[x].Cells[5].Value.ToString())));
                komut.Parameters.AddWithValue("@barkod", Convert.ToInt32(dukkan_Barkod.Text));

                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();

                string sepetGuncelleme = "Update Sepet Set Marka=@marka,Model=@model,Renk=@renk,Adet=@adet,Fiyat=@fiyat,Toplam=@toplam Where Barkod=@barkod";
                komut = new OleDbCommand(sepetGuncelleme, baglanti);

                komut.Parameters.AddWithValue("@marka", dukkan_Marka.Text);
                komut.Parameters.AddWithValue("@model", dukkan_Model.Text);
                komut.Parameters.AddWithValue("@renk", dukkan_Renk.Text);
                komut.Parameters.AddWithValue("@adet", Convert.ToInt32(dukkan_SepetData.Rows[i].Cells[4].Value.ToString()) - Convert.ToInt32(dukkan_Adet.Text));
                komut.Parameters.AddWithValue("@fiyat", Convert.ToDouble(dukkan_SepetData.Rows[i].Cells[5].Value.ToString()));
                komut.Parameters.AddWithValue("@toplam", (Convert.ToInt32(dukkan_SepetData.Rows[i].Cells[4].Value.ToString()) - Convert.ToInt32(dukkan_Adet.Text)) * (Convert.ToDouble(dukkan_SepetData.Rows[i].Cells[5].Value.ToString())));
                komut.Parameters.AddWithValue("@barkod", Convert.ToInt32(dukkan_Barkod.Text));

                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();

                tablo.Clear();
                baglanti.Open();
                komut = new OleDbCommand("select * from Dukkan ", baglanti);
                adtr = new OleDbDataAdapter(komut);
                adtr.Fill(tablo, "Dukkan");
                dukkan_DukkanData.DataSource = tablo.Tables["Dukkan"];
                baglanti.Close();

                baglanti.Open();
                komut = new OleDbCommand("select * from Sepet ", baglanti);
                adtr = new OleDbDataAdapter(komut);
                adtr.Fill(tablo, "Sepet");
                dukkan_SepetData.DataSource = tablo.Tables["Sepet"];
                baglanti.Close();

            }
            else
            {
                baglanti.Open();
                komut = new OleDbCommand("Insert into Dukkan (Marka,Model,Renk,Adet,Fiyat,Toplam,Barkod) values ('" + dukkan_Marka.Text + "','" + dukkan_Model.Text + "','" + dukkan_Renk.Text + "','" + Convert.ToInt32(dukkan_Adet.Text) + "','" + Convert.ToDouble(dukkan_Fiyat.Text) + "','" + Convert.ToInt32(dukkan_Adet.Text) * Convert.ToDouble(dukkan_Fiyat.Text) + "','" + Convert.ToInt32(dukkan_Barkod.Text) + "')", baglanti);
                komut.ExecuteNonQuery();
                baglanti.Close();

                string SepetGuncelleme = "Update Sepet Set Marka=@marka,Model=@model,Renk=@renk,Adet=@adet,Fiyat=@fiyat,Toplam=@toplam Where Barkod=@barkod";
                komut = new OleDbCommand(SepetGuncelleme, baglanti);

                komut.Parameters.AddWithValue("@marka", dukkan_Marka.Text);
                komut.Parameters.AddWithValue("@model", dukkan_Model.Text);
                komut.Parameters.AddWithValue("@renk", dukkan_Renk.Text);
                komut.Parameters.AddWithValue("@adet", Convert.ToInt32(dukkan_DukkanData.Rows[x].Cells[4].Value.ToString()) - Convert.ToInt32(dukkan_Adet.Text));
                komut.Parameters.AddWithValue("@fiyat", Convert.ToDouble(dukkan_DukkanData.Rows[x].Cells[5].Value.ToString()));
                komut.Parameters.AddWithValue("@toplam", (Convert.ToInt32(dukkan_DukkanData.Rows[x].Cells[4].Value.ToString()) - Convert.ToInt32(dukkan_Adet.Text)) * (Convert.ToDouble(dukkan_DukkanData.Rows[x].Cells[5].Value.ToString())));
                komut.Parameters.AddWithValue("@barkod", Convert.ToInt32(dukkan_Barkod.Text));

                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();

                tablo.Clear();
                baglanti.Open();
                komut = new OleDbCommand("select * from Dukkan ", baglanti);
                adtr = new OleDbDataAdapter(komut);
                adtr.Fill(tablo, "Dukkan");
                dukkan_DukkanData.DataSource = tablo.Tables["Dukkan"];
                baglanti.Close();

                baglanti.Open();
                komut = new OleDbCommand("select * from Sepet ", baglanti);
                adtr = new OleDbDataAdapter(komut);
                adtr.Fill(tablo, "Sepet");
                dukkan_SepetData.DataSource = tablo.Tables["Sepet"];
                baglanti.Close();

            }
        }

        private void dukkan_SepetData_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            dukkan_Barkod.Text = dukkan_SepetData.CurrentRow.Cells[0].Value.ToString();
            dukkan_Marka.Text = dukkan_SepetData.CurrentRow.Cells[1].Value.ToString();
            dukkan_Model.Text = dukkan_SepetData.CurrentRow.Cells[2].Value.ToString();
            dukkan_Renk.Text = dukkan_SepetData.CurrentRow.Cells[3].Value.ToString();
            dukkan_Adet.Text = dukkan_SepetData.CurrentRow.Cells[4].Value.ToString();
            dukkan_Fiyat.Text = dukkan_SepetData.CurrentRow.Cells[5].Value.ToString();
        }

        public void listeleme()
        {
            tablo.Clear();
            baglanti.Open();
            komut = new OleDbCommand("select * from Dukkan ", baglanti);
            adtr = new OleDbDataAdapter(komut);
            adtr.Fill(tablo, "Dukkan");
            dukkan_DukkanData.DataSource = tablo.Tables["Dukkan"];
            baglanti.Close();

            baglanti.Open();
            komut = new OleDbCommand("select * from Sepet ", baglanti);
            adtr = new OleDbDataAdapter(komut);
            adtr.Fill(tablo, "Sepet");
            dukkan_SepetData.DataSource = tablo.Tables["Sepet"];
            baglanti.Close();
        }
        private void dukkan_SatisButon_Click(object sender, EventArgs e)
        {
            OleDbCommand gunSonuKomut = new OleDbCommand("SELECT * FROM gunSonu", baglanti);
            DataTable gunSonuTablo = new DataTable();
            adtr.SelectCommand = gunSonuKomut;
            adtr.Fill(gunSonuTablo);

            OleDbCommand sepetKomut = new OleDbCommand("SELECT * FROM Sepet", baglanti);
            DataTable sepetTablo = new DataTable();
            adtr.SelectCommand = sepetKomut;
            adtr.Fill(sepetTablo);

            int sepetUzunluk = sepetTablo.Rows.Count;
            int gunSonuUzunluk = gunSonuTablo.Rows.Count;

            int i = 0;
            for (i = 0; i < sepetUzunluk; i++)
            {
                int yoksa = 0;

                int x = 0;
                for (x = 0; x < gunSonuUzunluk; x++)
                {
                    if (Convert.ToInt32(sepetTablo.Rows[i][0].ToString()) == Convert.ToInt32(gunSonuTablo.Rows[x][0].ToString()))
                    {
                        baglanti.Open();
                        string gunSonuGuncelleme = "Update gunSonu Set Marka=@marka,Model=@model,Renk=@renk,Adet=@adet,Fiyat=@fiyat,Toplam=@toplam WHERE Barkod=@barkod";
                        komut = new OleDbCommand(gunSonuGuncelleme, baglanti);
                        komut.Parameters.AddWithValue("@marka", sepetTablo.Rows[i][1].ToString());
                        komut.Parameters.AddWithValue("@model", sepetTablo.Rows[i][2].ToString());
                        komut.Parameters.AddWithValue("@renk", sepetTablo.Rows[i][3].ToString());
                        komut.Parameters.AddWithValue("@adet", Convert.ToInt32(sepetTablo.Rows[i][4].ToString()) + Convert.ToInt32(gunSonuTablo.Rows[x][4]));
                        komut.Parameters.AddWithValue("@fiyat", Convert.ToInt32(sepetTablo.Rows[i][5].ToString()));
                        komut.Parameters.AddWithValue("@toplam", Convert.ToInt32(sepetTablo.Rows[i][5].ToString()) * (Convert.ToInt32(sepetTablo.Rows[i][4].ToString()) + Convert.ToInt32(gunSonuTablo.Rows[x][4])));
                        komut.Parameters.AddWithValue("@barkod", Convert.ToInt32(sepetTablo.Rows[i][0]));
                        komut.ExecuteNonQuery();
                        baglanti.Close();

                        yoksa = 1;
                    }
                }

                if (yoksa == 0)
                {
                    baglanti.Open();
                    string gunSonuKaydetme = "Insert into gunSonu  (Barkod,Marka,Model,Renk,Adet,Fiyat,Toplam) values (@barkod,@marka,@model,@renk,@adet,@fiyat,@toplam) ";
                    komut = new OleDbCommand(gunSonuKaydetme, baglanti);
                    komut.Parameters.AddWithValue("@barkod", Convert.ToInt32(sepetTablo.Rows[i][0].ToString()));
                    komut.Parameters.AddWithValue("@marka", sepetTablo.Rows[i][1].ToString());
                    komut.Parameters.AddWithValue("@model", sepetTablo.Rows[i][2].ToString());
                    komut.Parameters.AddWithValue("@renk", sepetTablo.Rows[i][3].ToString());
                    komut.Parameters.AddWithValue("@adet", Convert.ToInt32(sepetTablo.Rows[i][4].ToString()));
                    komut.Parameters.AddWithValue("@fiyat", Convert.ToInt32(sepetTablo.Rows[i][5].ToString()));
                    komut.Parameters.AddWithValue("@toplam", Convert.ToInt32(sepetTablo.Rows[i][6].ToString()));

                    komut.ExecuteNonQuery();
                    baglanti.Close();
                }
               
            }


            baglanti.Open();
            string sepetTemizleme = "DELETE * FROM sepet";
            komut = new OleDbCommand(sepetTemizleme, baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();

            listeleme();

            hesaplamaLabel.Text = "";
        }

        private void hesaplaButon_Click(object sender, EventArgs e)
        {
            int i = 0;
            int uzunluk = dukkan_SepetData.Rows.Count - 1;
            int toplam = 0;
            for (i = 0; i < uzunluk; i++)
            {
                toplam = toplam + Convert.ToInt32( dukkan_SepetData.Rows[i].Cells[6].Value.ToString());
            }

             hesaplamaLabel.Text =Convert.ToString( toplam);
        }

        private void dukkan_Silme_Click(object sender, EventArgs e)
        {
            /*
                sepettiki urunu komple çıkarmaya yarıyor burda seçili urunu dukkandaki barkod numarasına göre sorgulayıp aynı olana adet miktarını kaydediyor.
                ve sepetten urunu cıkarıyor.
             */
            int i = 0;
            int dukkanUzunluk = dukkan_DukkanData.Rows.Count - 1;
            for (i = 0; i < dukkanUzunluk; i++)
            {
                if (Convert.ToInt32(dukkan_Barkod.Text) == Convert.ToInt32(dukkan_DukkanData.Rows[i].Cells[0].Value.ToString()))
                {
                    baglanti.Open();
                    string ekleme = "UPDATE Dukkan Set Marka=@marka,Model=@model,Renk=@renk, Adet=@adet,Fiyat=@fiyat,Toplam=@toplam WHERE Barkod=@barkod";
                    OleDbCommand komutEkleme = new OleDbCommand(ekleme, baglanti);
                    komutEkleme.Parameters.AddWithValue("@marka",dukkan_DukkanData.Rows[i].Cells[1].Value.ToString());
                    komutEkleme.Parameters.AddWithValue("@model",dukkan_DukkanData.Rows[i].Cells[2].Value.ToString());
                    komutEkleme.Parameters.AddWithValue("@renk",dukkan_DukkanData.Rows[i].Cells[3].Value.ToString());
                    komutEkleme.Parameters.AddWithValue("@adet",Convert.ToInt32(dukkan_DukkanData.Rows[i].Cells[4].Value.ToString())+Convert.ToInt32(dukkan_Adet.Text));
                    komutEkleme.Parameters.AddWithValue("@fiyat",Convert.ToInt32(dukkan_DukkanData.Rows[i].Cells[5].Value.ToString()));
                    komutEkleme.Parameters.AddWithValue("@toplam", (Convert.ToInt32(dukkan_DukkanData.Rows[i].Cells[4].Value.ToString()) + Convert.ToInt32(dukkan_Adet.Text)) * Convert.ToInt32(dukkan_DukkanData.Rows[i].Cells[5].Value.ToString()));
                    komutEkleme.Parameters.AddWithValue("@barkod",Convert.ToInt32(dukkan_DukkanData.Rows[i].Cells[0].Value.ToString()));
                    komutEkleme.ExecuteNonQuery();
                    baglanti.Close();

                    break;

                }
            }

            baglanti.Open();
            string silmeSorgu = "Delete From Sepet Where Barkod="+Convert.ToInt32(dukkan_SepetData.CurrentRow.Cells[0].Value.ToString())+"";
            OleDbCommand komutSilme = new OleDbCommand(silmeSorgu, baglanti);
            komutSilme.ExecuteNonQuery();
            baglanti.Close();

            listeleme();
        }
    }
}
