using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;// ACCESS VERİ TABANINI KULLANMAK İÇİN GEREKLİ KÜTÜPHANE.


namespace UrunTakipSistemi
{
    public partial class UrunBilgisi : Form
    {
        #region ACCESS BAGLANTISI VE ACCESS KULLANIMI ICIN GEREKLI OLAN KODLAR

        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=UrunTakipSistemiVeriTabani.mdb"); //ACCESS VERİ TABANI İLE BAĞLANTI KURMAK İÇİN KULLANILIR.
        OleDbCommand komut; //KLASİK VERİ TABANI İŞLEMLERİ(EKLEME,GÜNCELLEME,VERİ ALMA,SİLME GİBİ) KOMUTLARI ÇALIŞTIRIR.
        OleDbDataAdapter adtr; //SİSTEM BELLEĞİ ÜZERİNDE YER ALAN BAĞLANTISIZ KATMAN NESENLERİ ARASINDAKİ VERİ ALIŞVERİŞİNİ SAĞLAR. DATA SET VE VERİLER ARASINDAKİ KÖPRÜDÜR.
        DataSet tablo = new DataSet(); //VERİ TABANINDA BAĞLANTISIZ OLAN BİR NESENEDİR. BİR KEZ VERİ TABANINA BAĞLANDIKTAN SONRA VERİYİ ALIR VE BAĞLANTIYI KESER.

        #endregion

        public UrunBilgisi()
        {
            InitializeComponent(); //FORMUN(UrunBilgisi) İÇİNDEKİ KAYNAK KODLARI ÇAĞIRIR.
        }
       
        private void UrunKayitKaydetButon_Click(object sender, EventArgs e)
        {
            #region -ÜRÜN KAYDETME-
            urunBilgisi_UrunKayitListeleme.Show();//Program listeleme olmadan hata veriyordu bizde butonu çağırdık ve mantık hatası ortadan kalktı.
            
            /*
             Egerki girilen parametrelerden birtanesi eksik olursa uyarı vermesini egerki tam ise
             bu sefer aynı urunu tekrar kaydetmesini engelledik ve uunu kaydettirdik
             */
            if (urunKayitMarka.Text=="" || urunKayitModel.Text =="" || urunKayitRenk.Text == "" || urunKayitAdet.Text == "" || urunKayitFiyat.Text == "") 
            {
                MessageBox.Show("Eksik Bilgi Girdiniz Tekrar Deneyiniz!");
            }
            else
            {   
                int varsa = 0;
                int depoUzunluk = urunKayitData.Rows.Count - 1;
                for (int b = 0; b <depoUzunluk ; b++)
                {   
                    if ((urunKayitMarka.Text == urunKayitData.Rows[b].Cells[1].Value.ToString()) && (urunKayitModel.Text == urunKayitData.Rows[b].Cells[2].Value.ToString()) && (urunKayitRenk.Text == urunKayitData.Rows[b].Cells[3].Value.ToString()))
                    {
                       varsa = 1;
                       break;
                       // Eğer datanın içndeki b.satırdaki 1 2 ve 3. sütundaki değerler aynıysa uyarı ver.
                    }
                }
                if (varsa == 1)
                {
                    MessageBox.Show("Aynı ürünü iki kez girdiniz!!");
                }
                else//değilse kaydet
                {
                    tablo.Clear();
                    baglanti.Open();
                    komut = new OleDbCommand("Insert Into Depo (Marka,Model,Renk,Adet,Fiyat,Toplam) values ('" + urunKayitMarka.Text + "','" + urunKayitModel.Text + "','" + urunKayitRenk.Text + "','" + Convert.ToInt32(urunKayitAdet.Text) + "','" + Convert.ToDouble(urunKayitFiyat.Text) + "','" + Convert.ToInt32(urunKayitAdet.Text) * Convert.ToDouble(urunKayitFiyat.Text) + "')", baglanti);                                      
                    // Depo tablosuna değerleri sırayla kaydet 
                    komut.ExecuteNonQuery(); // Komudu çalıştır
                    baglanti.Close();

                    // Listeleme
                    baglanti.Open();
                    komut = new OleDbCommand("select * from Depo ", baglanti); // Depo tablosunu seç ve bağlantı kur.
                    adtr = new OleDbDataAdapter(komut); // komut ile tablo arasında veri alışverişini oluşturur.
                    adtr.Fill(tablo, "Depo"); // Depo tablosunu girilen verilerle doldur.
                    urunKayitData.DataSource = tablo.Tables["Depo"]; //urunKayitData ya hangi tablonun geleceğini belirtir. 
                    baglanti.Close();
                }
            }
            #endregion
        }
        private void urunBilgisi_UrunKayitListeleme_Click(object sender, EventArgs e)
        {
            #region -ÜRÜN KAYDETME LİSTELEME-
            /*tabloyu ekrana yazdırır*/
            tablo.Clear();
            baglanti.Open();
            komut = new OleDbCommand("select * from Depo ", baglanti);
            adtr = new OleDbDataAdapter(komut);
            adtr.Fill(tablo, "Depo");
            urunKayitData.DataSource = tablo.Tables["Depo"];
            baglanti.Close();
            #endregion
        }
        private void urunGuncellemeAnasayfaDonButon_Click(object sender, EventArgs e)
        {
            GirisEkrani GE = new GirisEkrani();
            GE.Show();
            this.Hide();
            
        }
        private void urunSilmeAnasayfaDonButon_Click(object sender, EventArgs e)
        {
            GirisEkrani GE = new GirisEkrani();
            GE.Show();
            this.Hide();
        }
        private void urunKayıtAnasayfaDonButon_Click(object sender, EventArgs e)
        {
            GirisEkrani gs = new GirisEkrani();
            gs.Show();
            this.Hide();
        }


        private void urunGuncelleme_Click(object sender, EventArgs e)
        {
            #region Güncelleme Bölümü
            // Eğer texboxlardan herhangi birine değer girilmezse uyarı ver.
            if (urunGuncellemeMarka.Text == "" || urunGuncellemeModel.Text == "" || urunGuncellemeRenk.Text == "" || urunGuncellemeAdet.Text == "" || urunGuncellemeFiyat.Text == "")
            {
                MessageBox.Show("Eksik Bilgi Girdiniz ve/veya Listeleme Yapmadınız. Tekrar Deneyiniz!");
            }
            else //Değer girilirse Gücelle ve Depoya kaydet.
            {
                String depoGuncelleme = "UPDATE Depo SET Marka=@marka,Model=@model,Renk=@renk,Adet=@adet,Fiyat=@fiyat,Toplam=@toplam Where Barkod=@barkod";
                komut = new OleDbCommand(depoGuncelleme, baglanti);

                komut.Parameters.AddWithValue("@marka", urunGuncellemeMarka.Text);
                komut.Parameters.AddWithValue("@model", urunGuncellemeModel.Text);
                komut.Parameters.AddWithValue("@renk", urunGuncellemeRenk.Text);
                komut.Parameters.AddWithValue("@adet", Convert.ToInt32(urunGuncellemeAdet.Text));
                komut.Parameters.AddWithValue("@fiyat", Convert.ToDouble(urunGuncellemeFiyat.Text));
                komut.Parameters.AddWithValue("@toplam", Convert.ToInt32(urunGuncellemeAdet.Text) * Convert.ToDouble(urunGuncellemeFiyat.Text));
                komut.Parameters.AddWithValue("@barkod", Convert.ToInt32(urunGuncellemeBarkod.Text));

                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();

                tablo.Clear();
                baglanti.Open();
                komut = new OleDbCommand("select * from Depo ", baglanti);
                adtr = new OleDbDataAdapter(komut);
                adtr.Fill(tablo, "Depo");
                urunGuncellemeData.DataSource = tablo.Tables["Depo"];
                baglanti.Close();
            }
            #endregion
        }
        private void urunBilgisi_ListelemeButon_Click(object sender, EventArgs e)
        {
            #region -ÜRÜN GÜNCELLEME LİSTELE-
            tablo.Clear();
            baglanti.Open();
            komut = new OleDbCommand("select * from Depo ", baglanti); // Depoyu seç ve bağlantı kur.
            adtr = new OleDbDataAdapter(komut);  // komut ile tablo arasında veri alışverişini oluşturur.
            adtr.Fill(tablo, "Depo"); // Depo tablosunu girilen verilerle doldur.
            urunGuncellemeData.DataSource = tablo.Tables["Depo"]; // urunGuncellemeData ya hangi tablonun geleceğini belirtir.
            baglanti.Close();
            #endregion
        }
        private void urunGuncellemeData_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            #region urunGuncellemeData Seçme
            urunGuncellemeBarkod.Text = urunGuncellemeData.CurrentRow.Cells[0].Value.ToString();
            urunGuncellemeMarka.Text = urunGuncellemeData.CurrentRow.Cells[1].Value.ToString();
            urunGuncellemeModel.Text = urunGuncellemeData.CurrentRow.Cells[2].Value.ToString();
            urunGuncellemeRenk.Text = urunGuncellemeData.CurrentRow.Cells[3].Value.ToString();
            urunGuncellemeAdet.Text = urunGuncellemeData.CurrentRow.Cells[4].Value.ToString();
            urunGuncellemeFiyat.Text = urunGuncellemeData.CurrentRow.Cells[5].Value.ToString();
            #endregion
        }

        private void urunSilmeButon_Click(object sender, EventArgs e)
        {
            #region -ÜRÜN SİLME-
            //Eğer textboxlara boş değer girilirse uyarı ver.
            if (urunSilmeMarka.Text == "" || urunSilmeModel.Text == "" || urunSilmeRenk.Text == "" || urunSilmeAdet.Text == "" || urunSilmeFiyat.Text == "")
            {
                MessageBox.Show("Eksik Bilgi Girdiniz veya Listelemediniz. Tekrar Deneyiniz!");
            }
            else // Boş değer girilmezse verileri sil
            {   
                // Veri kaybını önlemek için de silmek istediğinizden emin misiniz diye sor. Evet ise sil.
                DialogResult mesaj;
                mesaj = MessageBox.Show("Silmek istediğinizden emin misiniz?", "Uyarı!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (mesaj == DialogResult.Yes)
                {
                    baglanti.Open();
                    komut.Connection = baglanti;
                    komut.CommandText = "Delete from Depo where Barkod=" + urunSilmeBarkod.Text + "";
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    tablo.Clear();
                    //listeleme
                    tablo.Clear();
                    baglanti.Open();
                    komut = new OleDbCommand("select * from Depo ", baglanti);
                    adtr = new OleDbDataAdapter(komut);
                    adtr.Fill(tablo, "Depo");
                    urunSilmeData.DataSource = tablo.Tables["Depo"];
                    baglanti.Close();
                }
            }
            #endregion
        }
        private void urunBilgisi_Listele2Buton_Click(object sender, EventArgs e)
        {
            #region -ÜRÜN SİLME LİSTELE-
            tablo.Clear();
            baglanti.Open();
            komut = new OleDbCommand("select * from Depo ", baglanti);
            adtr = new OleDbDataAdapter(komut);
            adtr.Fill(tablo, "Depo");
            urunSilmeData.DataSource = tablo.Tables["Depo"];
            baglanti.Close();
            #endregion
        }
        private void urunSilmeData_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            #region-urunSilmeData SEÇME-
            urunSilmeBarkod.Text = urunSilmeData.CurrentRow.Cells[0].Value.ToString();
            urunSilmeMarka.Text = urunSilmeData.CurrentRow.Cells[1].Value.ToString();
            urunSilmeModel.Text = urunSilmeData.CurrentRow.Cells[2].Value.ToString();
            urunSilmeRenk.Text = urunSilmeData.CurrentRow.Cells[3].Value.ToString();
            urunSilmeAdet.Text = urunSilmeData.CurrentRow.Cells[4].Value.ToString();
            urunSilmeFiyat.Text = urunSilmeData.CurrentRow.Cells[5].Value.ToString();
            #endregion
        }
    }
}
