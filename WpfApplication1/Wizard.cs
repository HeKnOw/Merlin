using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApplication1
{
    class Wizard:MerlinObject
    {
        Ammo ammo = new Ammo();
        double rotate = 0;
        public double currentAngle
        {
            get
            {
                return _angle;
            }
        }
        private static double _angle = 0.0;

        public override void update()
        {
            if (Keyboard.IsKeyDown(Key.Right))
            {
                rotate += 10;
                //rotateTransform.Angle-= 10;
            }
            if(Keyboard.IsKeyDown(Key.Left))
            {
                rotate += -10;
               // rotateTransform.Angle += 10;
            }
           
            rotateTransform.Angle = rotate;
            ammo.currentAngle = rotateTransform.Angle;
        }        
        
       
    }
}
