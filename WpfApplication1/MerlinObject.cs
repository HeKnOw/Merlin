using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Threading;

namespace WpfApplication1
{
    public abstract class MerlinObject
    {
         public FrameworkElement element
        {
            get { return _element; }
            set
            {
                _element = value;
                rotateTransform = new RotateTransform();
                rotateTransform.CenterX = _element.Width / 2.0;
                rotateTransform.CenterY = _element.Height / 2.0;
                scaleTransform = new ScaleTransform();
                scaleTransform.CenterX = _element.Width / 2.0;
                scaleTransform.CenterY = _element.Height / 2.0;
                translateTransform = new TranslateTransform();
                transformGroup = new TransformGroup();
                transformGroup.Children.Add(rotateTransform);
                transformGroup.Children.Add(scaleTransform);
                transformGroup.Children.Add(translateTransform);
                _element.RenderTransform = transformGroup;
                if(_canvas != null)
                {
                    _canvas.Children.Add(_element);
                }
                if(_effect != null)
                {
                    _canvas.Effect = _effect;
                }
            }
        }

        private FrameworkElement _element = null;

        public RotateTransform rotateTransform
        {
            get;
            set;
        }
        public ScaleTransform scaleTransform
        {
            get;
            set;
        }
        public TranslateTransform translateTransform
        {
            get;
            set;
        }
        public TransformGroup transformGroup
        {
            get;
            set;
        }

        public Effect effect
        {
            get
            {
                return _effect;
            }
            set
            {
                _effect = value;
                if(_element!=null)
                {
                    _element.Effect = _effect;
                }
            }
        }

        Effect _effect = null;

        public Canvas canvas
        {
            get
            {
                return _canvas;
            }
            set
            {
                _canvas = value;
                if (_element != null && _element.Parent == null)
                {
                    _canvas.Children.Add(_element);
                }
            }

        }

        public Canvas canvasRemove
        {
            //get
            //{
            //    return _canvas;
            //}
            set
            {
                _canvas = value;
                if (_element != null)
                {
                    _canvas.Children.Remove(_element);
                }
            }

        }
        private Canvas _canvas = null;

        //public DispatcherTimer timer
        //{
        //    get
        //    {
        //        if(_timer == null)
        //        {
        //            _timer = new DispatcherTimer();
        //            _timer.Tick += _timer_Tick;
        //        }
        //        return _timer;
        //    }
        //    set
        //    {
        //        _timer = value;
        //    }
        //}

        //void _timer_Tick(object sender, EventArgs e)
        //{
        //    update();
        //}

        virtual public void update()
        {
        }

       
      
        //private DispatcherTimer _timer = null;
    
    }
}
