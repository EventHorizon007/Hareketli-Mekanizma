using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Configuration;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hareketli_Mekanizma
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //1.CizimAlani tanımlanıyor mavi dikdörtgenler haric hersey buraya cizdirilecek.
        Graphics CizimAlani;
        Pen Kalem1 = new Pen(System.Drawing.Color.Yellow, 1);
        Pen Kalem2 = new Pen(System.Drawing.Color.Red, 1);
        //Mavi dikdörtgenleri buraya cizdiricez.Çünkü bu alan döndürülerek mavi çubuğun dönmesi sağlanacak.
        Graphics CizimAlani2;
        private void Form1_Load(object sender, EventArgs e)
        {
            //Cizim alanlarının picturebox üzerinde görüntülenmesi ve timer value nun textbox a atanması.
            CizimAlani = pictureBox1.CreateGraphics();
            CizimAlani2 = pictureBox1.CreateGraphics();
            textBox1.Text = timer1.Interval.ToString();
        }
        int x, y;
        private void button1_Click(object sender, EventArgs e)
        {
            //butona tıklandığında timer çalışacak.
            timer1.Start();
            
        }

        //Döndürme islemi bu class ile yapılıyor genislik ve yükseklik 2 ye bölünürse ortadan döner.
        //Su an dikdörtgenin ucundan dönüyör.
        public void RotateRectangle(Graphics g, Rectangle r, float angle)
        {
            using (Matrix m = new Matrix())
            {
                m.RotateAt(angle, new PointF(r.X + (r.Width), r.Y + (r.Height)));
                CizimAlani2.Transform = m;
                CizimAlani2.DrawRectangle(Pens.Blue, r);
                CizimAlani2.ResetTransform();
            }
        }
        //sayaclar ve daire dilimin baslangıc acısı.Dısarda tanımlanmalarının nedeni global olmaları gerektiginden.
        int sayac = -1;
        int sayac1 =0;
        float startAngle = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            //bu sayac sartı degerleri sistemdeki parcaların kat etmesi gereken 
            //acılar ve mesafelerin ebob una göre hesaplandı. 
            if (sayac < 15)
            { 
                sayac++; 
            }

            if (sayac1 < 160)
            {
                sayac1+=10;
            }

            x = 75;
            y = 350;
            
            pictureBox1.Refresh();
            //Sabitleme aparatı 1
            CizimAlani.DrawRectangle(Kalem1, x, y, 25, 20);
            CizimAlani.DrawRectangle(Kalem1, x, y + 20, 25, 40);
            CizimAlani.DrawRectangle(Kalem1, x, y + 60, 25, 20);
            CizimAlani.DrawEllipse(Kalem1, (float)(x + 8.25), y + 6, 8, 8);
            CizimAlani.DrawEllipse(Kalem1, (float)(x + 8.25), y + 66, 8, 8);

            //Sabitleme aparatı 2
            CizimAlani.DrawRectangle(Kalem1, x + 470, y, 25, 20);
            CizimAlani.DrawRectangle(Kalem1, x + 470, y + 20, 25, 40);
            CizimAlani.DrawRectangle(Kalem1, x + 470, y + 60, 25, 20);
            CizimAlani.DrawEllipse(Kalem1, (float)(x + 478.25), y + 6, 8, 8);
            CizimAlani.DrawEllipse(Kalem1, (float)(x + 478.25), y + 66, 8, 8);

            //Ray çubugu
            if (sayac <= 5)
            {
                CizimAlani.DrawRectangle(Kalem2, -5 - (sayac * 15), y + 25, 655, 30);
            }
            else if (6 <= sayac && sayac <= 15)
            { 
                CizimAlani.DrawRectangle(Kalem2, -5 + ((sayac - 10) * 15), y + 25, 655, 30); 
            }
            //Sarkac
            CizimAlani.DrawEllipse(Kalem1, (float)307.5, (float)247.5, 35, 35);
            CizimAlani.DrawEllipse(Kalem1, (float)297.5, (float)237.5, 55, 55);
            Rectangle rect1 = new Rectangle(225, 152, 200, 200);
            Rectangle rect2 = new Rectangle(250, 177, 150, 150);
            
            if (sayac <= 5)
            {
                startAngle = 30 + (sayac * 8);
            }
            else if (6 <= sayac)
            {
                startAngle = 30 - ((sayac-10) * 8);
            }
            
            float sweepAngle = 120;
            CizimAlani.DrawPie(Kalem1, rect1, startAngle, sweepAngle);
            CizimAlani.DrawPie(Kalem1, rect2, startAngle, sweepAngle);

            //Ray cubugu dislileri
            int x1 = 470, y1 = 375, artım = 0,artım2=0;
            if (sayac <= 5)
            {
                for (int i = 0; i < 10; i++)
                {
                    CizimAlani.DrawLine(Kalem1, x1 - artım - (sayac * 15), y1, x1 - 5 - artım - (sayac * 15), y1 - 20);
                    CizimAlani.DrawLine(Kalem1, x1 - 5 - artım - (sayac * 15), y1 - 20, x1 - 15 - artım - (sayac * 15), y1 - 20);
                    CizimAlani.DrawLine(Kalem1, x1 - 15 - artım - (sayac * 15), y1 - 20, x1 - 20 - artım - (sayac * 15), y1);
                    artım += 30;
                }
            }

            else if (6<=sayac && sayac <= 15)
            {
                for (int i = 0; i < 10; i++)
                {
                    CizimAlani.DrawLine(Kalem1, x1 - artım2 + ((sayac - 10) * 15), y1, x1 - 5 - artım2 + ((sayac - 10) * 15), y1 - 20);
                    CizimAlani.DrawLine(Kalem1, x1 - 5 - artım2 + ((sayac - 10) * 15), y1 - 20, x1 - 15 - artım2 + ((sayac - 10) * 15), y1 - 20);
                    CizimAlani.DrawLine(Kalem1, x1 - 15 - artım2 + ((sayac - 10) * 15), y1 - 20, x1 - 20 - artım2 + ((sayac - 10) * 15), y1);
                    artım2 += 30;
                }
            }

            //Cark
            CizimAlani.DrawEllipse(Kalem1, 310, 120, 30, 30);
            CizimAlani.DrawEllipse(Kalem1, 300, 110, 50, 50);
            CizimAlani.DrawEllipse(Kalem1, 225, 35, 200, 200);
            double a1 = 325;
            int b1 = 135;
            int cizgiboyu = 75;
            CizimAlani2.SmoothingMode = SmoothingMode.HighQuality;
            Rectangle r1 = new Rectangle(310, 10, 30, 275);
            Rectangle r2 = new Rectangle(305, 5, 40, 285);
            if (sayac <= 5)
            {
                double alfaradyan = ((sayac * 25) + 90) * 2 * Math.PI / 360;//Dereceden Radyana
                double x2 = a1 - cizgiboyu * Math.Cos(alfaradyan);
                double y2 = b1 - cizgiboyu * Math.Sin(alfaradyan);
                CizimAlani.DrawEllipse(Kalem1, (float)x2 - 15, (float)y2 - 15, 30, 30);

                //bu dondurme acıları deneme yanılmayla bulundu.Mavi dikdörtgenlerin kodu.
                if (sayac1 == 40)
                {
                    RotateRectangle(CizimAlani2, r1, (float)((sayac1 - 10) * 0.785));
                    RotateRectangle(CizimAlani2, r2, (float)((sayac1 - 10) * 0.785));
                }
                else if (sayac1 == 50)
                {
                    RotateRectangle(CizimAlani2, r1, (float)((sayac1 - 10) * 0.715));
                    RotateRectangle(CizimAlani2, r2, (float)((sayac1 - 10) * 0.715));

                }
                else if (sayac1 <= 30)
                {

                    RotateRectangle(CizimAlani2, r1, (float)((sayac1 - 10) * 0.780));
                    RotateRectangle(CizimAlani2, r2, (float)((sayac1 - 10) * 0.780));
                }
                else if (sayac1 == 60)
                {
                    RotateRectangle(CizimAlani2, r1, (float)((sayac1 - 10) * 0.625));
                    RotateRectangle(CizimAlani2, r2, (float)((sayac1 - 10) * 0.625));
                }
            }
            else if (6 <= sayac && sayac <= 15)
            {
                double alfaradyan = -(((sayac-10) * 25) + 90) * 2 * Math.PI / 360;//Dereceden Radyana
                double x2 = a1 + cizgiboyu * Math.Cos(alfaradyan);
                double y2 = b1 + cizgiboyu * Math.Sin(alfaradyan);
                CizimAlani.DrawEllipse(Kalem1, (float)x2 - 15, (float)y2 - 15, 30, 30);

                //bu dondurme acıları deneme yanılmayla bulundu.Mavi dikdörtgenlerin kodu.
                if (sayac1 == 100)
                {
                    RotateRectangle(CizimAlani2, r1, (float)((sayac1 - 110) * -0.785));
                    RotateRectangle(CizimAlani2, r2, (float)((sayac1 - 110) * -0.785));
                }
                else if (sayac1 == 110 || sayac1 == 150)
                {
                    RotateRectangle(CizimAlani2, r1, (float)((sayac1 - 110) * -0.715));
                    RotateRectangle(CizimAlani2, r2, (float)((sayac1 - 110) * -0.715));

                }

                else if (sayac1 <= 90 || (sayac1 >= 120 && sayac1 <= 140))
                {

                    RotateRectangle(CizimAlani2, r1, (float)((sayac1 - 110) * -0.780));
                    RotateRectangle(CizimAlani2, r2, (float)((sayac1 - 110) * -0.780));
                }
                

                else if (sayac1 == 160)
                {

                    RotateRectangle(CizimAlani2, r1, (float)((sayac1 - 110) * -0.625));
                    RotateRectangle(CizimAlani2, r2, (float)((sayac1 - 110) * -0.625));
                }

            }

            if (sayac == 15)
            {
                sayac = -1;
                sayac1 = 0;
            }

        }
        //trackbar ayarı.
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            timer1.Interval = trackBar1.Value;
            textBox1.Text = timer1.Interval.ToString();
        }
    }
}
