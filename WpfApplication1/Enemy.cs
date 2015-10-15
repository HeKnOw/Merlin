using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;

namespace WpfApplication1
{
    class Enemy:MerlinObject
    {
        private int _enemyOnTarget = 0;
        public int enemyOnTarget
        {
            get
            {
                return _enemyOnTarget;
            }
            set
            {
                _enemyOnTarget = value;
            }
        }
        public int speedX
        {
            set
            {
                _speedx = value;
            }
        }
        public int speedY
        {
            set
            {
                _speedy = value;
            }
        }
        static Random random = new Random();
        private int startX = 0;
        private int startY = 0;
        
        private static int _speedx = 0;
        private static int _speedy = 0;
        private int _X = 0;
        private int _Y = 0;
        public bool destroyed = false;
        //private static int _positionx = 0;

       

        public Image drawEnemy()
        {
            startX = random.Next(200)-100;
            startY = random.Next(200)-100;

            if(startX >=0)
            {
                startX = 450;
            }
            if(startY>=0)
            {
                startY = 550;
            }
            _X = startX;
            _Y = startY;
            Image enemyImg = new Image();
            enemyImg.Source = new BitmapImage(new Uri(@"/Resources/monster.png", UriKind.Relative));
            Canvas.SetTop(enemyImg, startY);
            Canvas.SetLeft(enemyImg, startX);
            //Canvas.SetZIndex(enemyImg, 1);
            enemyImg.Height = 20;
            enemyImg.Width = 20;
            return enemyImg;
        }

        public override void update()
        {
            if (translateTransform.X + startX <= 175)
            {
                //where did the object start in the x
                
                translateTransform.X += _speedx;
                _X += _speedx;
            }
            if (translateTransform.Y  + startY <= 195 )
            {
                translateTransform.Y += _speedy;
                _Y += _speedy;
            }
            if (translateTransform.X + startX >= 175)
            {
                //where did the object start in the x

                translateTransform.X -= _speedx;
                _X -= _speedx;
            }
            if (translateTransform.Y + startY >= 195)
            {

                translateTransform.Y -= _speedy;
                _Y -= _speedy;
            }
            if(translateTransform.X + startX > 170 && translateTransform.X + startX < 180)
            {
                if (translateTransform.Y + startY > 190 && translateTransform.Y + startY < 200)
                {
                    _enemyOnTarget = 15;
                }
            }
        }

        public int X
        {
            get
            {
                return _X;
            }
        }

        public int Y
        {
            get
            {
                return _Y;
            }
        }
    }
}
