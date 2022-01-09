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
    public partial class Depo : Form
    {
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=UrunTakipSistemiVeriTabani.mdb"); //ACCESS VERİ TABANI İLE BAĞLANTI KURMAK İÇİN KULLANILIR.
        OleDbCommand komut; //KLASİK VERİ TABANI İŞLEMLERİ(EKLEME,GÜNCELLEME,VERİ ALMA,SİLME GİBİ) KOMUTLARI ÇALIŞTIRIR.
        OleDbDataAdapter adtr; //SİSTEM BELLEĞİ ÜZERİNDE YER ALAN BAĞLANTISIZ KATMAN NESENLERİ ARASINDAKİ VERİ ALIŞVERİŞİNİ SAĞLAR. DATA SET VE VERİLER ARASINDAKİ KÖPRÜDÜR.
        DataSet tablo = new DataSet(); //VERİ TABANINDA BAĞLANTISIZ OLAN BİR NESENEDİR. BİR KEZ VERİ TABANINA BAĞLANDIKTAN SONRA VERİYİ ALIR VE BAĞLANTIYI KESER.
        public Depo()
        {
            InitializeComponent(); // FORMUN(Depo) İÇİNDEKİ KAYNAK KODLARI ÇAĞIRIR.
        }

        private void depo_AnasayfaDonButon_Click(object sender, EventArgs e)
        {
            GirisEkrani GE = new GirisEkrani();
            GE.Show();
            this.Hide();
        }

        private void depo_AnaSayfaDon_Click(object sender, EventArgs e)
        {
            GirisEkrani GE = new GirisEkrani();
            GE.Show();
            this.Hide();
        }

        private void depo_AnasayfayaDon2_Click(object sender, EventArgs e)
        {
            GirisEkrani GE = new GirisEkrani();
            GE.Show();
            this.Hide();
        }

        private void depo_ListeleButon_Click(object sender, EventArgs e)
        {
            #region Depo Bilgisi Listeleme
            tablo.Clear();
            baglanti.Open();
            komut = new OleDbCommand("select * from Depo ", baglanti); //Depoyu seç ve bağlantı kur.
            adtr = new OleDbDataAdapter(komut); //Komut ile tablo arasında veri alışverişini oluşturur.
            adtr.Fill(tablo, "Depo"); //Depo tablosunu girilen verilerle doldur.
            depo_DepoBilgisiData.DataSource = tablo.Tables["Depo"]; // depo_DepoBilgisiData ya hangi tablonun geleceğini belirtir.
            baglanti.Close();
            #endregion
        }

        private void depo_Listele2Buton_Click(object sender, EventArgs e)
        {
            #region depo_DepoData Listeleme
            tablo.Clear(); 
            baglanti.Open();
            komut = new OleDbCommand("select * from Depo ", baglanti); 
            adtr = new OleDbDataAdapter(komut);
            adtr.Fill(tablo, "Depo");
            depo_depoData.DataSource = tablo.Tables["Depo"];
            baglanti.Close();
            #endregion
            #region depo_dukkanData Listeleme
            baglanti.Open();
            komut = new OleDbCommand("select * from Dukkan ", baglanti);
            adtr = new OleDbDataAdapter(komut);
            adtr.Fill(tablo, "Dukkan");
            depo_dukkanData.DataSource = tablo.Tables["Dukkan"];
            baglanti.Close();
            #endregion
        }

        public void listemeSayfa1()
        {
            #region Depo Listeleme
            tablo.Clear();
            baglanti.Open();
            komut = new OleDbCommand("select * from Depo ", baglanti);
            adtr = new OleDbDataAdapter(komut);
            adtr.Fill(tablo, "Depo");
            depo_depoData.DataSource = tablo.Tables["Depo"];
            baglanti.Close();
            #endregion

            #region Dükkan Listeleme
            baglanti.Open();
            komut = new OleDbCommand("select * from Dukkan ", baglanti);
            adtr = new OleDbDataAdapter(komut);
            adtr.Fill(tablo, "Dukkan");
            depo_dukkanData.DataSource = tablo.Tables["Dukkan"];
            baglanti.Close();
            #endregion
        }
        private void depo_EkleButon_Click(object sender, EventArgs e)
        {
            /*
             burda depodaki urunu dükkanımıza cıkarıyoruz ve kac adet ve ne kadara satılıcagını belirliyoruz 
             ilk önce urun dukkanda varmı varsa update ediyor
             yok sa urunu kaydediyor
             */
            if (depo_Adet.Text == "" || depo_Fiyat.Text == "" || Convert.ToInt32(depo_Adet.Text) <= 0 || Convert.ToInt32(depo_Fiyat.Text) <= 0)
            {
                MessageBox.Show("Hatalı Girdiniz!");

            }
            else
            {
                int i = 0, yoksa = 0;
                int depoUzunluk = depo_dukkanData.Rows.Count - 1;
                for (i = 0; i < depoUzunluk; i++)
                {
                    if (Convert.ToInt32(depo_Barkod.Text) == Convert.ToInt32(depo_dukkanData.Rows[i].Cells[0].Value.ToString()))
                    {
                        baglanti.Open();
                        string depoGuncelleme = "UPDATE Depo SET Marka=@marka,Model=@model,Renk=@renk,Adet=@adet,Fiyat=@fiyat,Toplam=@toplam WHERE Barkod=@barkod";
                        OleDbCommand komutDepo = new OleDbCommand(depoGuncelleme, baglanti);
                        komutDepo.Parameters.AddWithValue("@marka", depo_depoData.CurrentRow.Cells[1].Value.ToString());
                        komutDepo.Parameters.AddWithValue("@model", depo_depoData.CurrentRow.Cells[2].Value.ToString());
                        komutDepo.Parameters.AddWithValue("@renk", depo_depoData.CurrentRow.Cells[3].Value.ToString());
                        komutDepo.Parameters.AddWithValue("@adet", Convert.ToInt32(depo_depoData.CurrentRow.Cells[4].Value.ToString()) - Convert.ToInt32(depo_Adet.Text));
                        komutDepo.Parameters.AddWithValue("@fiyat", Convert.ToInt32(depo_depoData.CurrentRow.Cells[5].Value.ToString()));
                        komutDepo.Parameters.AddWithValue("@toplam", (Convert.ToInt32(depo_depoData.CurrentRow.Cells[4].Value.ToString()) - Convert.ToInt32(depo_Adet.Text)) * Convert.ToInt32(depo_depoData.CurrentRow.Cells[5].Value.ToString()));
                        komutDepo.Parameters.AddWithValue("@barkod", Convert.ToInt32(depo_depoData.CurrentRow.Cells[0].Value.ToString()));
                        komutDepo.ExecuteNonQuery();
                        baglanti.Close();

                        baglanti.Open();
                        string dukkanGuncelleme = "UPDATE Dukkan SET Marka=@marka,Model=@model,Renk=@renk,Adet=@adet,Fiyat=@fiyat,Toplam=@toplam WHERE Barkod=@barkod";
                        OleDbCommand komutDukkan = new OleDbCommand(dukkanGuncelleme, baglanti);
                        komutDukkan.Parameters.AddWithValue("@marka", depo_dukkanData.Rows[i].Cells[1].Value.ToString());
                        komutDukkan.Parameters.AddWithValue("@model", depo_dukkanData.Rows[i].Cells[2].Value.ToString());
                        komutDukkan.Parameters.AddWithValue("@renk", depo_dukkanData.Rows[i].Cells[3].Value.ToString());
                        komutDukkan.Parameters.AddWithValue("@adet", Convert.ToInt32(depo_dukkanData.Rows[i].Cells[4].Value.ToString()) + Convert.ToInt32(depo_Adet.Text));
                        komutDukkan.Parameters.AddWithValue("@fiyat", Convert.ToInt32(depo_dukkanData.Rows[i].Cells[5].Value.ToString()));
                        komutDukkan.Parameters.AddWithValue("@toplam", (Convert.ToInt32(depo_dukkanData.Rows[i].Cells[4].Value.ToString()) + Convert.ToInt32(depo_Adet.Text)) * Convert.ToInt32(depo_dukkanData.Rows[i].Cells[5].Value.ToString()));
                        komutDukkan.Parameters.AddWithValue("@barkod", depo_dukkanData.Rows[i].Cells[0].Value.ToString());
                        komutDukkan.ExecuteNonQuery();
                        baglanti.Close();

                        listemeSayfa1();
                        yoksa = 1;
                        break;
                    }
                }
                if (yoksa == 0)
                {
                    baglanti.Open();
                    string dukkanKaydet = "INSERT INTO Dukkan (Barkod,Marka,Model,Renk,Adet,Fiyat,Toplam) values (@barkod,@marka,@model,@renk,@adet,@fiyat,@toplam)";
                    OleDbCommand komutKaydet = new OleDbCommand(dukkanKaydet, baglanti);

                    komutKaydet.Parameters.AddWithValue("@barkod", Convert.ToInt32(depo_Barkod.Text));
                    komutKaydet.Parameters.AddWithValue("@marka", depo_Marka.Text);
                    komutKaydet.Parameters.AddWithValue("@model", depo_Model.Text);
                    komutKaydet.Parameters.AddWithValue("@renk", depo_Renk.Text);
                    komutKaydet.Parameters.AddWithValue("@adet", Convert.ToInt32(depo_Adet.Text));
                    komutKaydet.Parameters.AddWithValue("@fiyat", Convert.ToInt32(depo_Fiyat.Text));
                    komutKaydet.Parameters.AddWithValue("@toplam", Convert.ToInt32(depo_Adet.Text) * Convert.ToInt32(depo_Fiyat.Text));

                    komutKaydet.ExecuteNonQuery();
                    baglanti.Close();

                    baglanti.Open();
                    string depoGuncelle = "UPDATE Depo SET Marka=@marka,Model=@model,Renk=@renk,Adet=@adet,Fiyat=@fiyat,Toplam=@toplam WHERE Barkod=@barkod";
                    OleDbCommand komutGuncelle = new OleDbCommand(depoGuncelle, baglanti);

                    komutGuncelle.Parameters.AddWithValue("@marka", depo_depoData.CurrentRow.Cells[1].Value.ToString()); //seçili satırdaki 1.sütundaki değeri al. 
                    komutGuncelle.Parameters.AddWithValue("@model", depo_depoData.CurrentRow.Cells[2].Value.ToString());
                    komutGuncelle.Parameters.AddWithValue("@renk", depo_depoData.CurrentRow.Cells[3].Value.ToString());
                    komutGuncelle.Parameters.AddWithValue("@adet", Convert.ToInt32(depo_depoData.CurrentRow.Cells[4].Value.ToString()) - Convert.ToInt32(depo_Adet.Text));
                    komutGuncelle.Parameters.AddWithValue("@fiyat", Convert.ToInt32(depo_depoData.CurrentRow.Cells[5].Value.ToString()));
                    komutGuncelle.Parameters.AddWithValue("@toplam", (Convert.ToInt32(depo_depoData.CurrentRow.Cells[4].Value.ToString()) - Convert.ToInt32(depo_Adet.Text)) * Convert.ToInt32(depo_depoData.CurrentRow.Cells[5].Value.ToString()));
                    komutGuncelle.Parameters.AddWithValue("@barkod", Convert.ToInt32(depo_depoData.CurrentRow.Cells[0].Value.ToString()));
                    komutGuncelle.ExecuteNonQuery();
                    baglanti.Close();

                    listemeSayfa1();
                }
            }
        }

        private void depo_depoData_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            #region depo_depoData Seçme
            depo_Barkod.Text = depo_depoData.CurrentRow.Cells[0].Value.ToString();
            depo_Marka.Text = depo_depoData.CurrentRow.Cells[1].Value.ToString();
            depo_Model.Text = depo_depoData.CurrentRow.Cells[2].Value.ToString();
            depo_Renk.Text = depo_depoData.CurrentRow.Cells[3].Value.ToString();
            depo_Adet.Text = depo_depoData.CurrentRow.Cells[4].Value.ToString();
            depo_Fiyat.Text = depo_depoData.CurrentRow.Cells[5].Value.ToString();
            #endregion
        }

        private void depo_Listele3Buton_Click(object sender, EventArgs e)
        {
            #region Depo Listeleme
            tablo.Clear();
            baglanti.Open();
            komut = new OleDbCommand("select * from Depo ", baglanti);
            adtr = new OleDbDataAdapter(komut);
            adtr.Fill(tablo, "Depo");
            depo_depoData2.DataSource = tablo.Tables["Depo"];
            baglanti.Close();
            #endregion

            #region Dükkan Listeleme
            baglanti.Open();
            komut = new OleDbCommand("select * from Dukkan ", baglanti);
            adtr = new OleDbDataAdapter(komut);
            adtr.Fill(tablo, "Dukkan");
            depo_dukkanData2.DataSource = tablo.Tables["Dukkan"];
            baglanti.Close();
            #endregion
        }

        public void listemeSayfa2()
        {
            #region Depo Listeleme
            tablo.Clear();
            baglanti.Open();
            komut = new OleDbCommand("select * from Depo ", baglanti);
            adtr = new OleDbDataAdapter(komut);
            adtr.Fill(tablo, "Depo");
            depo_depoData2.DataSource = tablo.Tables["Depo"];
            baglanti.Close();
            #endregion

            #region Dükkan Listeleme
            baglanti.Open();
            komut = new OleDbCommand("select * from Dukkan ", baglanti);
            adtr = new OleDbDataAdapter(komut);
            adtr.Fill(tablo, "Dukkan");
            depo_dukkanData2.DataSource = tablo.Tables["Dukkan"];
            baglanti.Close();
            #endregion
        }
        private void depo_Ekle2Buton_Click(object sender, EventArgs e)
        {
            /*
             dükkandaki urunu depoya aktarıyor ve burada urun depoda varsa adet miktarını ustune ekliyoruz
             egerki urun depodan silindiyse urunu kaydediyor bu seferalış fiyadı ile
             */
            if (depo_Adet2.Text == "" || depo_Fiyat2.Text == "" || Convert.ToInt32(depo_Adet2.Text) <= 0 || Convert.ToInt32(depo_Fiyat2.Text) <= 0)
            {
                MessageBox.Show("HATALI GİRİŞ YAPTINIZ");
            }
            else
            {
                int i = 0, yoksa = 0;
                int depoUzunluk = depo_depoData2.Rows.Count - 1;
                for (i = 0; i < depoUzunluk; i++)
                {


                    if (Convert.ToInt32(depo_Barkod2.Text) == Convert.ToInt32(depo_depoData2.Rows[i].Cells[0].Value.ToString()))
                    { 
                        baglanti.Open();
                        string depoGuncelleme = "UPDATE Depo SET Marka=@marka,Model=@model,Renk=@renk,Adet=@adet,Fiyat=@fiyat,Toplam=@toplam WHERE Barkod=@barkod";
                        OleDbCommand komutDepo = new OleDbCommand(depoGuncelleme, baglanti);
                        komutDepo.Parameters.AddWithValue("@marka", depo_depoData2.CurrentRow.Cells[1].Value.ToString());
                        komutDepo.Parameters.AddWithValue("@model", depo_depoData2.CurrentRow.Cells[2].Value.ToString());
                        komutDepo.Parameters.AddWithValue("@renk", depo_depoData2.CurrentRow.Cells[3].Value.ToString());
                        komutDepo.Parameters.AddWithValue("@adet", Convert.ToInt32(depo_depoData2.CurrentRow.Cells[4].Value.ToString()) + Convert.ToInt32(depo_Adet2.Text));
                        komutDepo.Parameters.AddWithValue("@fiyat", Convert.ToInt32(depo_depoData2.CurrentRow.Cells[5].Value.ToString()));
                        komutDepo.Parameters.AddWithValue("@toplam", (Convert.ToInt32(depo_depoData2.CurrentRow.Cells[4].Value.ToString()) + Convert.ToInt32(depo_Adet2.Text)) * Convert.ToInt32(depo_depoData2.CurrentRow.Cells[5].Value.ToString()));
                        komutDepo.Parameters.AddWithValue("@barkod", Convert.ToInt32(depo_depoData2.CurrentRow.Cells[0].Value.ToString()));
                        komutDepo.ExecuteNonQuery();
                        baglanti.Close();

                        baglanti.Open();
                        string dukkanGuncelleme = "UPDATE Dukkan SET Marka=@marka,Model=@model,Renk=@renk,Adet=@adet,Fiyat=@fiyat,Toplam=@toplam WHERE Barkod=@barkod";
                        OleDbCommand komutDukkan = new OleDbCommand(dukkanGuncelleme, baglanti);
                        komutDukkan.Parameters.AddWithValue("@marka", depo_dukkanData2.Rows[i].Cells[1].Value.ToString());
                        komutDukkan.Parameters.AddWithValue("@model", depo_dukkanData2.Rows[i].Cells[2].Value.ToString());
                        komutDukkan.Parameters.AddWithValue("@renk", depo_dukkanData2.Rows[i].Cells[3].Value.ToString());
                        komutDukkan.Parameters.AddWithValue("@adet", Convert.ToInt32(depo_dukkanData2.Rows[i].Cells[4].Value.ToString()) - Convert.ToInt32(depo_Adet2.Text));
                        komutDukkan.Parameters.AddWithValue("@fiyat", Convert.ToInt32(depo_dukkanData2.Rows[i].Cells[5].Value.ToString()));
                        komutDukkan.Parameters.AddWithValue("@toplam", (Convert.ToInt32(depo_dukkanData2.Rows[i].Cells[4].Value.ToString()) - Convert.ToInt32(depo_Adet2.Text)) * Convert.ToInt32(depo_dukkanData2.Rows[i].Cells[5].Value.ToString()));
                        komutDukkan.Parameters.AddWithValue("@barkod", depo_dukkanData2.Rows[i].Cells[0].Value.ToString());
                        komutDukkan.ExecuteNonQuery();
                        baglanti.Close();

                        listemeSayfa2();
                        yoksa = 1;
                        break;
                    }


                }

                if (yoksa == 0)
                {
                    MessageBox.Show("uurun yok");
                    baglanti.Open();
                    string depoKaydet = "INSERT INTO Depo (Barkod,Marka,Model,Renk,Adet,Fiyat,Toplam) values (@barkod,@marka,@model,@renk,@adet,@fiyat,@toplam)";
                    OleDbCommand komutKaydet = new OleDbCommand(depoKaydet, baglanti);

                    komutKaydet.Parameters.AddWithValue("@barkod", Convert.ToInt32(depo_Barkod2.Text));
                    komutKaydet.Parameters.AddWithValue("@marka", depo_Marka2.Text);
                    komutKaydet.Parameters.AddWithValue("@model", depo_Model2.Text);
                    komutKaydet.Parameters.AddWithValue("@renk", depo_Renk2.Text);
                    komutKaydet.Parameters.AddWithValue("@adet", Convert.ToInt32(depo_Adet2.Text));
                    komutKaydet.Parameters.AddWithValue("@fiyat", Convert.ToInt32(depo_Fiyat2.Text));
                    komutKaydet.Parameters.AddWithValue("@toplam", Convert.ToInt32(depo_Adet2.Text) * Convert.ToInt32(depo_Fiyat2.Text));

                    komutKaydet.ExecuteNonQuery();
                    baglanti.Close();

                    baglanti.Open();
                    string dukkanGuncelle = "UPDATE Dukkan SET Marka=@marka,Model=@model,Renk=@renk,Adet=@adet,Fiyat=@fiyat,Toplam=@toplam WHERE Barkod=@barkod";
                    OleDbCommand komutGuncelle = new OleDbCommand(dukkanGuncelle, baglanti);

                    komutGuncelle.Parameters.AddWithValue("@marka", depo_dukkanData2.CurrentRow.Cells[1].Value.ToString()); //seçili satırdaki 1.sütundaki değeri al. 
                    komutGuncelle.Parameters.AddWithValue("@model", depo_dukkanData2.CurrentRow.Cells[2].Value.ToString());
                    komutGuncelle.Parameters.AddWithValue("@renk", depo_dukkanData2.CurrentRow.Cells[3].Value.ToString());
                    komutGuncelle.Parameters.AddWithValue("@adet", Convert.ToInt32(depo_dukkanData2.CurrentRow.Cells[4].Value.ToString()) - Convert.ToInt32(depo_Adet2.Text));
                    komutGuncelle.Parameters.AddWithValue("@fiyat", Convert.ToInt32(depo_dukkanData2.CurrentRow.Cells[5].Value.ToString()));
                    komutGuncelle.Parameters.AddWithValue("@toplam", (Convert.ToInt32(depo_dukkanData2.CurrentRow.Cells[4].Value.ToString()) - Convert.ToInt32(depo_Adet2.Text)) * Convert.ToInt32(depo_dukkanData2.CurrentRow.Cells[5].Value.ToString()));
                    komutGuncelle.Parameters.AddWithValue("@barkod", Convert.ToInt32(depo_dukkanData2.CurrentRow.Cells[0].Value.ToString()));
                    komutGuncelle.ExecuteNonQuery();
                    baglanti.Close();

                    listemeSayfa2();

                }
            }

        }
        private void depo_dukkanData2_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            #region depo_dukkanData2 Seçme
            depo_Barkod2.Text = depo_dukkanData2.CurrentRow.Cells[0].Value.ToString();
            depo_Marka2.Text = depo_dukkanData2.CurrentRow.Cells[1].Value.ToString();
            depo_Model2.Text = depo_dukkanData2.CurrentRow.Cells[2].Value.ToString();
            depo_Renk2.Text = depo_dukkanData2.CurrentRow.Cells[3].Value.ToString();
            depo_Adet2.Text = depo_dukkanData2.CurrentRow.Cells[4].Value.ToString();
            depo_Fiyat2.Text = depo_dukkanData2.CurrentRow.Cells[5].Value.ToString();
            #endregion
        }
    }
}
