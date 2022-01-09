namespace UrunTakipSistemi
{
    partial class GirisEkrani
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.urunBilgisiGirisButton = new System.Windows.Forms.Button();
            this.depoGirisButon = new System.Windows.Forms.Button();
            this.dukkanGirisButon = new System.Windows.Forms.Button();
            this.gunSonuGirisButon = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // urunBilgisiGirisButton
            // 
            this.urunBilgisiGirisButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.urunBilgisiGirisButton.Location = new System.Drawing.Point(0, 0);
            this.urunBilgisiGirisButton.Name = "urunBilgisiGirisButton";
            this.urunBilgisiGirisButton.Size = new System.Drawing.Size(130, 124);
            this.urunBilgisiGirisButton.TabIndex = 0;
            this.urunBilgisiGirisButton.Text = "Ürün Bilgisi";
            this.urunBilgisiGirisButton.UseVisualStyleBackColor = true;
            this.urunBilgisiGirisButton.Click += new System.EventHandler(this.UrunBilgisibutton_Click);
            // 
            // depoGirisButon
            // 
            this.depoGirisButon.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.depoGirisButon.Location = new System.Drawing.Point(136, 0);
            this.depoGirisButon.Name = "depoGirisButon";
            this.depoGirisButon.Size = new System.Drawing.Size(130, 124);
            this.depoGirisButon.TabIndex = 1;
            this.depoGirisButon.Text = "Depoooo";
            this.depoGirisButon.UseVisualStyleBackColor = true;
            this.depoGirisButon.Click += new System.EventHandler(this.depoGirisButon_Click);
            // 
            // dukkanGirisButon
            // 
            this.dukkanGirisButon.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dukkanGirisButon.Location = new System.Drawing.Point(0, 130);
            this.dukkanGirisButon.Name = "dukkanGirisButon";
            this.dukkanGirisButon.Size = new System.Drawing.Size(130, 124);
            this.dukkanGirisButon.TabIndex = 2;
            this.dukkanGirisButon.Text = "Dükkan";
            this.dukkanGirisButon.UseVisualStyleBackColor = true;
            this.dukkanGirisButon.Click += new System.EventHandler(this.dukkanGirisButon_Click);
            // 
            // gunSonuGirisButon
            // 
            this.gunSonuGirisButon.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.gunSonuGirisButon.Location = new System.Drawing.Point(136, 130);
            this.gunSonuGirisButon.Name = "gunSonuGirisButon";
            this.gunSonuGirisButon.Size = new System.Drawing.Size(130, 124);
            this.gunSonuGirisButon.TabIndex = 3;
            this.gunSonuGirisButon.Text = "Gün Sonu";
            this.gunSonuGirisButon.UseVisualStyleBackColor = true;
            this.gunSonuGirisButon.Click += new System.EventHandler(this.gunSonuGirisButon_Click);
            // 
            // GirisEkrani
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(268, 255);
            this.Controls.Add(this.gunSonuGirisButon);
            this.Controls.Add(this.dukkanGirisButon);
            this.Controls.Add(this.depoGirisButon);
            this.Controls.Add(this.urunBilgisiGirisButton);
            this.Name = "GirisEkrani";
            this.Text = "Giriş Ekranı";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button urunBilgisiGirisButton;
        private System.Windows.Forms.Button depoGirisButon;
        private System.Windows.Forms.Button dukkanGirisButon;
        private System.Windows.Forms.Button gunSonuGirisButon;
    }
}

