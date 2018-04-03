using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OkmsLive.Forms
{
    /// <summary>
    /// NoCourseWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NoCourseWindow : Window
    {
        public NoCourseWindow()
        {
            InitializeComponent();
        }

        private void Image_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            CloseBtn.Source = new BitmapImage(new Uri("/Resources/toClose_hover.png", UriKind.RelativeOrAbsolute));
        }

        private void Image_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            CloseBtn.Source = new BitmapImage(new Uri("/Resources/toClose2.png", UriKind.RelativeOrAbsolute));
        }

        private void CloseBtn_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
