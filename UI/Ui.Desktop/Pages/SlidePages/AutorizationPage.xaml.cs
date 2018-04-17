using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace FitnessClubMWWM.Ui.Desktop.Pages.SlidePages.AutorizationPage
{
    /// <summary>
    /// Логика взаимодействия для AutorizationPage.xaml
    /// </summary>
    public partial class AutorizationPage : Page
    {
        //private const int MnCntOfPages = 4;
        //private int m_nPagesCounter;
        //private readonly List<string> m_strPath;
        //private readonly DispatcherTimer m_timer;
        public AutorizationPage()
        {
            //m_nPagesCounter = 0;
            //m_strPath = new List<string>();
            //InitializeImagesPath(ref m_strPath, MnCntOfPages);

            //m_timer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 10) };
            //m_timer.Tick += TimerTick;
            
            InitializeComponent();
        }

        //private void TimerTick(object sender, EventArgs e)
        //{
        //    m_nPagesCounter++;
        //    if (m_nPagesCounter >= MnCntOfPages)
        //        m_nPagesCounter = 0;
        //    PlaySlideShow(m_nPagesCounter);
        //}

        //private void InitializeImagesPath(ref List<string> strPath, int nCnt)
        //{
        //    for (int i = 0; i < nCnt; i++)
        //    {
        //        int nCount = i + 1;
        //        string filename = Path.GetFullPath(
        //            $@"\Projects\FitnessClub\FitnesClubMWWM\UI\Ui.Desktop\Images\AutorizationPictureImages\fitness-club{
        //            nCount
        //            }.jpg");

        //        strPath.Add(filename);
        //    }
        //}

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    m_timer.Start();
        //    PlaySlideShow(m_nPagesCounter);
        //}

        //private void PlaySlideShow(int nPagesCounter)
        //{
        //   BitmapImage image = new BitmapImage();
        //   image.BeginInit();
        //   image.UriSource = new Uri(m_strPath[m_nPagesCounter], UriKind.Relative);
        //   image.EndInit();
        //   MyImage.ImageSource = image;

        //   DoubleAnimation animation = new DoubleAnimation
        //   {
        //       From = 0.2,
        //       To = 0.3,
        //       Duration = TimeSpan.FromSeconds(5)

        //   };
        //   AnimatedGrid.BeginAnimation(OpacityProperty, animation);
        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        //    MessageBox.Show(AutorizationTextBox.InputString);
        }
    }
}
