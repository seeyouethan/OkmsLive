using OkmsLive.Enums;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace OkmsLive.Forms
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        

        private bool IsMax = false;//界面是否是最大化状态
        private bool IsLive = false;//是否正在直播
        private LiveTypeWindow liveTypeWindow;

        public LiveType liveType = LiveType.Desktop;

        #region 界面交互

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var dlg = new CloseWindow();
            if (dlg.ShowDialog() == true)
            {
                Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void TitleStackPanedl_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        
        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            var item = (StackPanel)sender;
            item.Background = new SolidColorBrush(Color.FromRgb(116, 191, 250));
        }
        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            var item = (StackPanel)sender;
            item.Background = new SolidColorBrush(Color.FromRgb(66, 160, 234));
        }

        private void CloseBtn_MouseLeftButtonDown(object sender,MouseButtonEventArgs e)
        {
            var dlg = new CloseWindow();
            if (dlg.ShowDialog() == true)
            {
                Environment.Exit(0);
            }
        }
        private void SizeBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var ele = (StackPanel)sender;
            var img = (Image)ele.Children[0];

            //获取当前显示器屏幕
            Rect rc = SystemParameters.WorkArea;//获取当前工作区大小
            if (IsMax)
            {
                Left = (rc.Width - 1280) / 2;
                Top = (rc.Height - 720) / 2;
                Width = 1280;
                Height = 720;
                IsMax = false;
                SizeBtn.ToolTip = "最大化";
                img.Source = new BitmapImage(new Uri("/Resources/toBig.png", UriKind.RelativeOrAbsolute));
                UserOnlinePanel.Height = Height - 85;
                DiscussPanel.Height = Height - 147;

            }
            else
            {
                Left = 0;
                Top = 0;
                Width = rc.Width;
                Height = rc.Height;
                IsMax = true;
                SizeBtn.ToolTip = "还原";
                img.Source = new BitmapImage(new Uri("/Resources/toNormal.png", UriKind.RelativeOrAbsolute));
                UserOnlinePanel.Height = Height - 85;
                DiscussPanel.Height = Height - 147;
            }
        }
        private void MinBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var window = this;
            window.Topmost = false;
            window.WindowState = WindowState.Minimized;
        }
        #endregion




        public MainWindow()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 点击直播按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LiveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (IsLive)
            {
                IsLive = false;
            }
            else
            {
                IsLive = true;
            }
        }

        private void SettingPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //打开设置对话框

        }

        private void CameraPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //打开摄像头选择选项

        }

        private void LiveTypePanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //打开直播类型选项
            if (liveTypeWindow == null)
            {
                liveTypeWindow = new LiveTypeWindow();
                liveTypeWindow.WindowStartupLocation = WindowStartupLocation.Manual;
                liveTypeWindow.Left = this.Left + 308;
                liveTypeWindow.Top = this.Top + this.Height - 110;

                liveTypeWindow.mainWindow = this;
                liveTypeWindow.ShowDialog();
            }
            else
            {
                liveTypeWindow.Close();
                liveTypeWindow = null;
            }
            
        }

        public void SetLiveTypeImgAndText(LiveType type )
        {
            liveTypeWindow.Close();
            liveTypeWindow = null;
            liveType = type;
            if (type == LiveType.Desktop)
            {
                img01.Source = new BitmapImage(new Uri("/Resources/computor.png", UriKind.RelativeOrAbsolute));
                label01.Content = "桌面演示";                
            }
            else if (type == LiveType.Software)
            {
                img01.Source = new BitmapImage(new Uri("/Resources/software.png", UriKind.RelativeOrAbsolute));
                label01.Content = "软件演示";
            }
            else if (type == LiveType.Window)
            {
                img01.Source = new BitmapImage(new Uri("/Resources/area.png", UriKind.RelativeOrAbsolute));
                label01.Content = "区域演示";
            }
        }
    }
}
