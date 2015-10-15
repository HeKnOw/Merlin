using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Canvas canvas = new Canvas();
        DispatcherTimer gameLoop;
        public Random random = new Random();
        TextBlock title = new TextBlock();
        TextBlock playOption = new TextBlock();
        TextBlock lbOption = new TextBlock();
        TextBlock display = new TextBlock();
        TextBlock scoreText = new TextBlock();
        Rectangle lives = new Rectangle();
        Rectangle magic = new Rectangle();
        Image logo = new Image();
        Image wizardImg = new Image();
        Image enemyImg = new Image();
        Wizard wizard = new Wizard();
        Stopwatch stopwatch = new Stopwatch();
        List<Enemy> enemies = new List<Enemy>();
        List<Ammo> ammunition = new List<Ammo>();
        string positionX = " ";
        bool restart = false;
        bool pgSafeCheck = false;
        static int ii = 0;
      

        public MainWindow()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
            MainGrid.Children.Add(canvas);
            DisplayMainMenu();
            
            //for(int i=0;i<100;i++)
            //{
            //    Ammo ammo = new Ammo();
            //    ammunition.Add(ammo);
            //}
            
            for(int j=0;j<20;j++)
            {
                Enemy enemy = new Enemy();
                enemy.speedX = random.Next(1, 5);
                enemy.speedY = random.Next(1, 3);
                enemy.canvas = canvas;
                enemy.element = enemy.drawEnemy();
                enemies.Add(enemy);
            }
        }

        private void DisplayMainMenu()
        {
            logo.Source = new BitmapImage(new Uri(@"/Resources/logo.png",UriKind.Relative));
            Canvas.SetTop(logo, 50);
            Canvas.SetLeft(logo, 145);
            logo.Height = 200;
            logo.Width = 100;
            canvas.Children.Add(logo);
            
            //-----------------------------------------------------------------
            //*****************************************************************
            //-----------------------------------------------------------------
            canvas.Background = new SolidColorBrush(Colors.Ivory);
            title.Text = "Merlin";
            title.FontSize = 40.0;
            title.Foreground = new SolidColorBrush(Colors.Black);
            title.Margin = new Thickness(130.0, 200.0, 200.0, 250.0);
            //------------------------------------------------------------------
            //------------------------------------------------------------------
            
            playOption.Text = "Play";
            playOption.FontSize = 25.0;
            playOption.Foreground = new SolidColorBrush(Colors.Black);
            playOption.Margin = new Thickness(160.0, 255.0, 200.0, 310.0);
            playOption.MouseDown += playOption_MouseDown;
            playOption.MouseEnter += playOption_MouseEnter;
            playOption.MouseLeave += playOption_MouseLeave;
            //-----------------------------------------------------------------
            //-----------------------------------------------------------------
            
            lbOption.Text = "Creators: Christian Hinostroza & Alexander Sanchez";
            lbOption.FontSize = 14.0;
            lbOption.Foreground = new SolidColorBrush(Colors.Black);
            lbOption.Margin = new Thickness(35.0, 290.0, 200.0, 340.0);
            //-----------------------------------------------------------------
            //*****************************************************************
            //-----------------------------------------------------------------
            
            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            canvas.Children.Add(title);
            canvas.Children.Add(playOption);
            canvas.Children.Add(lbOption);
            
        }

        //----------------------------------------------------------------------------
        //---------------- ANIMATION FOR THE TEXTBOX ---------------------------------
        //----------------------------------------------------------------------------
        void playOption_MouseLeave(object sender, MouseEventArgs e)
        {
            playOption.Foreground = new SolidColorBrush(Colors.Black);
        }

        void playOption_MouseEnter(object sender, MouseEventArgs e)
        {
            playOption.Foreground = new SolidColorBrush(Colors.Red);
        }
        void lbOption_MouseLeave(object sender, MouseEventArgs e)
        {
            lbOption.Foreground = new SolidColorBrush(Colors.Black);
        }

        void lbOption_MouseEnter(object sender, MouseEventArgs e)
        {
            lbOption.Foreground = new SolidColorBrush(Colors.Tomato);
        }

        //-----------------------------------------------------------------------------
        //-----------------------------------------------------------------------------

        void lbOption_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Leaderboard();
        }
        void playOption_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(!pgSafeCheck)
            {

                PlayGame();
                stopwatch.Start();
      
            } 
        }
        

        /// <summary>
        /// Runs main game
        /// </summary>
        private void PlayGame()
        {
            //**************************************************************************
            //************** REMOVE MENU ***********************************************
            //*************************************************************************
            canvas.Children.Remove(title);
            canvas.Children.Remove(playOption);
            canvas.Children.Remove(lbOption);
            canvas.Children.Remove(logo);
            canvas.Background = new SolidColorBrush(Colors.Black);
            //******************************************************************************
            //*****************************************************************************

            //-----------------------------------------------------------------------------
            //-------------------------------------------------------------------------------
            wizardImg.Source = new BitmapImage(new Uri(@"/Resources/untitled.png", UriKind.Relative));
            Canvas.SetTop(wizardImg, 190);
            Canvas.SetLeft(wizardImg, 160);
            Canvas.SetZIndex(wizardImg,1);
            wizardImg.Height = 40;
            wizardImg.Width = 50;

            wizard.element = wizardImg;
            if(!restart)
            {
                wizard.canvas = canvas;
            }

            title.Text = positionX;
            title.FontSize = 20.0;
            title.Foreground = new SolidColorBrush(Colors.White);
            title.Margin = new Thickness(30.0, 20.0, 40.0, 50.0);
            canvas.Children.Add(title);
            InitializeTimer();          
           pgSafeCheck = true;
           
        }

        private void Leaderboard()
        {
            //**************************************************************************
            //************** REMOVE MENU ***********************************************
            //*************************************************************************
            canvas.Children.Remove(title);
            canvas.Children.Remove(playOption);
            canvas.Children.Remove(lbOption);
            canvas.Children.Remove(logo);
            canvas.Background = new SolidColorBrush(Colors.Wheat);
            //******************************************************************************
            //*****************************************************************************
            title.FontSize = 50.0;
            title.Foreground = new SolidColorBrush(Colors.DarkSlateGray);
            title.Text = "Leaderboard";
            title.Margin = new Thickness(50,5,150,40);
            canvas.Children.Add(title);
        }
        //---------------------------------------------------------------
        //---------------------------------------------------------------
        private void InitializeTimer()
        {
            if (gameLoop == null)
            {
                gameLoop = new DispatcherTimer();
                gameLoop.Interval = new TimeSpan(0, 0, 0, 0, 30);
                gameLoop.Tick += gameLoop_Tick;
            }
            gameLoop.Start();
        }

        //===========================================================
        //==========================================================



        static int ammoCount = 0;
        static int j = 0;
        static double lifeUnit = 200.0;
        static double magicUnit = 200.0;
        bool overheat = false;
        static int score = 0;
        //**************************************************************
        //***************************************************************
        //**************8 MAIN LOOP 8***********************************
        //***************************************************************
        void gameLoop_Tick(object sender, EventArgs e)
        {
            double timeElapsedsec = stopwatch.ElapsedMilliseconds / 1000;

            canvas.Children.Remove(title);
            canvas.Children.Remove(display);
            canvas.Children.Remove(magic);
            canvas.Children.Remove(lives);
            canvas.Children.Remove(scoreText);
            //if(ammoCount == 100)
            //{
            //    ammoCount = 0;
            //}

            
            if(timeElapsedsec < 60 && !(timeElapsedsec > 60))
            {
                
                enemies[0].update();
                enemies[1].update();
                enemies[2].update();
                enemies[3].update();
                enemies[4].update();
                enemies[5].update();
                enemies[6].update();
                enemies[7].update();
                
                //enemies[i].setPosition = random(0, sizeofwindow);
                //timer has to check for collision between bullet and monster
                //fucntion collision needs to return the index of the enemy
            }
            else if(timeElapsedsec < 120 && !(timeElapsedsec >120))
            {
                enemies[0].update();
                enemies[1].update();
                enemies[2].update();
                enemies[3].update();
                enemies[4].update();
                enemies[5].update();
                enemies[6].update();
                enemies[7].update();
                enemies[8].update();
                enemies[9].update();
                enemies[10].update();
                enemies[11].update();

            }
            else
            {
                enemies[0].update();
                enemies[1].update();
                enemies[2].update();
                enemies[3].update();
                enemies[4].update();
                enemies[5].update();
                enemies[6].update();
                enemies[7].update();
                enemies[8].update();
                enemies[9].update();
                enemies[10].update();
                enemies[11].update();
                enemies[12].update();
                enemies[13].update();
                enemies[14].update();
                enemies[15].update();
                enemies[16].update();
                enemies[17].update();
                enemies[18].update();
                enemies[19].update();
            }
            for (int i = 0; i < 20; i++)
            {
                lifeUnit -= enemies[i].enemyOnTarget;
                if (enemies[i].enemyOnTarget == 15)
                {
                    enemies[i].canvasRemove = canvas;
                    enemies[i].element = enemies[i].drawEnemy();                 
                    
                    Stream std = Properties.Resources.Pain_SoundBible_com_1883168362;
                    SoundPlayer sn = new SoundPlayer(std);
                    sn.Play();
                    enemies[i].enemyOnTarget = 0;                            
                }
               
            }

            wizard.update();
            if(magicUnit < 10)
            {
                overheat = true;
            }
            if(!overheat)
            {
                if (Keyboard.IsKeyDown(Key.Space))
                {
                   // ammunition[ammoCount].currentAngle = wizard.currentAngle;
                    //ammunition[ammoCount].element = ammunition[ammoCount].drawAmmo;
                    Ammo ammo = new Ammo();
                    ammunition.Add(ammo);
                    if (ammunition[ammoCount].available)
                    {                                    
                        ammunition[ammoCount].element = ammunition[ammoCount].drawAmmo;
                        ammunition[ammoCount].canvas = canvas;
                        ammunition[ammoCount].available = false;
                        ammoCount++;
                      
                    }
                    
                    magicUnit -= 5.0;
                }
            }
            //+++++++++++++++++++++++++++++++++++++++++++++++++++++
            //++++UPDATE AMMO +++++++++++++++++++++++++++++++
            //++++++++++++++++++++++++++++++++++++++++++++++++
            for (int i = ii; i < ammunition.Count; i++)
            {
                if (!ammunition[i].available)
                {
                    ammunition[i].updateAmmo();
                }
                if(ammunition[i].X <= -150 || ammunition[i].X >=650)
                {
                    ammunition[i].canvasRemove = canvas;
                    ammunition[i].available = true;
                    //ammunition[i].reloaded = true;
                    ii++;
                }
                if(ammunition[i].Y <=-150 || ammunition[i].Y >= 750)
                {
                    ammunition[i].canvasRemove = canvas;
                    ammunition[i].available = true;
                    //ammunition[i].reloaded = true;
                    ii++;
                }
            }

            //+++++++++++++++++++++++++++++++++++++++++++++++++
            //++++CHECK ENEMIES++++++++++++++++++++++++++++
            //+++++++++++++++++++++++++++++++++++++++++++++++
            for (int i = 0; i < 20; i++)
            {
                if (enemies[i].destroyed)
                {
                    enemies[i].element = enemies[i].drawEnemy();
                    //enemies[i].canvas = canvas;
                    enemies[i].destroyed = false;
                    score += 10;
                }

            }

            //*************************************************************
            //***************DESTROY ENEMIES******************************
            //*********************************************************
            for (int i = ii; i < ammunition.Count;i++ )
            {
                if (!ammunition[i].available)
                {
                    for (int k = 0; k < 20;k++)
                    {
                        if((ammunition[i].X >= enemies[k].X - 30 && ammunition[i].X <= enemies[k].X + 30) &&
                            (ammunition[i].Y >= enemies[k].Y - 30 && ammunition[i].Y <= enemies[k].Y + 30))
                        {
                            enemies[k].canvasRemove = canvas;
                            enemies[k].destroyed = true;
                            ammunition[i].available = true;
                            ammunition[i].canvasRemove = canvas;
                            Stream str = Properties.Resources.Spacecraft_Hatch_Opening_SoundBible_com_1577619787;
                            SoundPlayer snd = new SoundPlayer(str);
                            snd.Play();
                        }
                    }
                        
                }
                
            }
            
            //**********************************************************
            //***************   TEXT CHECK*******************************
            //display.Text = Canvas.GetTop(enemies[0].element).ToString();
            display.Text = "Magic";
            display.FontSize = 20.0;
            display.Foreground = new SolidColorBrush(Colors.White);
            display.Margin = new Thickness(5, 5, 40, 20);
            canvas.Children.Add(display);
            //positionX = Mouse.GetPosition(canvas).ToString();
            //title.Text = positionX;
            //canvas.Children.Add(title);
            title.Text = "Life";
            title.Margin = new Thickness(5, 35, 70, 30);
            canvas.Children.Add(title);
            lives.Height = 5.0;
            lives.Width = lifeUnit;
            Canvas.SetTop(lives, 60);
            Canvas.SetLeft(lives, 5);
            lives.Fill = Brushes.Red;
            canvas.Children.Add(lives);
            magic.Height = 5.0;
            magic.Width = magicUnit;
            Canvas.SetTop(magic, 30);
            Canvas.SetLeft(magic, 5);
            magic.Fill = Brushes.Aqua;
            canvas.Children.Add(magic);
            scoreText.Text = "Score: " + score.ToString();
            scoreText.FontSize = 20.0;
            scoreText.Foreground = new SolidColorBrush(Colors.White);
            scoreText.Margin = new Thickness(270, 5, 310, 20);
            canvas.Children.Add(scoreText);

            //*************************************************************
            if (magicUnit < 200.0)
            {
                magicUnit++;
            }
            if(magicUnit == 200)
            {
                overheat = false;
            }
          if(lifeUnit <= 10)
            {
                Stream s = Properties.Resources.Scream_And_Die_Fx_SoundBible_com_299479967;
                SoundPlayer snt = new SoundPlayer(s);
                snt.Play();

                canvas.Children.Remove(title);
                canvas.Children.Remove(display);
                canvas.Children.Remove(magic);
                canvas.Children.Remove(lives);
                canvas.Children.Remove(scoreText);
                wizard.canvasRemove = canvas;
                stopwatch.Stop();
                gameLoop.Stop();
                for (int i = 0; i < 20; i++)
                {
                    enemies[i].canvasRemove = canvas;
                    enemies[i].element = enemies[i].drawEnemy();
                    enemies[i].enemyOnTarget = 0;
                }
                for (int i = ammunition.Count - 1; i > 0; i--)
                {
                    //ammunition[i].canvasRemove = canvas;
                    //ammunition[i].available = true;
                    ammunition.Remove(ammunition[i]);
                }
                restart = true;
                pgSafeCheck = false;
                lifeUnit = 200.0;
                magicUnit = 200.0;
                ii = 0;
                score = 0;
                ammoCount = 0;
                overheat = false;
                DisplayMainMenu();
            }
             
        }
    }
}
