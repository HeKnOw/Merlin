using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WpfApplication1
{
    class Ammo:MerlinObject
    {

        public bool available = true;
        //public bool reloaded = false;

        public double newAngle = 0.0;

        public double currentAngle
        {
            get
            {
                return _angle;
            }
            set
            {
                _angle = value;
            }
        }
        private static double _angle = 0.0;
        private double _X = 160;
        private double _Y = 190;

        public Image drawAmmo
        {
            get
            {
                Image ammoImg = new Image();
                ammoImg.Source = new BitmapImage(new Uri(@"/Resources/diamond.png", UriKind.Relative));
                Canvas.SetTop(ammoImg, 190);
                Canvas.SetLeft(ammoImg, 160);
                Canvas.SetZIndex(ammoImg, 0);
                ammoImg.Height = 50;
                ammoImg.Width = 50;
                newAngle = _angle;
                return ammoImg;
            }
        }

         public void updateAmmo()
        {
            translateTransform.X += 5 * Math.Cos((180-newAngle)* Math.PI/180);
            translateTransform.Y -= 5 * Math.Sin((180 - newAngle) * Math.PI / 180);
            _X += 5 * Math.Cos((180 - newAngle) * Math.PI / 180);
            _Y -= 5 * Math.Sin((180 - newAngle) * Math.PI / 180);
        }

         public int X
         {
             get
             {
                 return (int)_X;
             }
         }

         public int Y
         {
             get
             {
                 return (int)_Y;
             }
         }
    }
}
