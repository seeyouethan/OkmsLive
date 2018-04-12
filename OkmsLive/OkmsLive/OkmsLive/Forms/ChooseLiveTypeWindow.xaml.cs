using OkmsLive.Enums;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace OkmsLive.Forms
{
    /// <summary>
    /// ChooseLiveTypeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ChooseLiveTypeWindow : Window
    {
        public ChooseLiveTypeWindow()
        {
            InitializeComponent();
        }

        public MainWindow mainWindow = new MainWindow();

        private void LiveTypePanel1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mainWindow.SetLiveTypeImgAndText(LiveType.Window);
            this.Close();
        }
        private void LiveTypePanel2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mainWindow.SetLiveTypeImgAndText(LiveType.Software);
            this.Close();
        }
        private void LiveTypePanel3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mainWindow.SetLiveTypeImgAndText(LiveType.Desktop);
            this.Close();
        }

        private void StackPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            var item = (StackPanel)sender;
            item.Background = new SolidColorBrush(Color.FromRgb(219, 234, 245));
        }

        private void StackPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            var item = (StackPanel)sender;
            item.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }
    }
}
