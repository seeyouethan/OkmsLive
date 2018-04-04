using OkmsLive.Models;
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
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ChooseCourseWindow : Window
    {
        public ChooseCourseWindow()
        {
            InitializeComponent();

            //模拟初始化
            var factory = new ControlFactory();

            var c1 = factory.CreateCourseControlNotStarted("1OKMS需求研讨", "（2018年4月5日 15:00-16:00）");
            var c2 = factory.CreateCourseControlNotStarted("2创业学苑镜像版需求研讨", "（2018年4月6日 15:00-16:00）");
            var c3 = factory.CreateCourseControlStarted("3OKMS登录界面www需求研讨", "（2018年4月4日 15:00-16:00）");

            c1.MouseLeftButtonDown += Border_MouseLeftButtonDown;
            c2.MouseLeftButtonDown += Border_MouseLeftButtonDown;
            c3.MouseLeftButtonDown += Border_MouseLeftButtonDown;

            StackPanelFather.Children.Add(c1);
            StackPanelFather.Children.Add(c2);
            StackPanelFather.Children.Add(c3);

        }


        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            foreach(var ele in StackPanelFather.Children)
            {
                if (ele is Border)
                {
                    //灰色
                    ((Border)ele).BorderBrush = new SolidColorBrush(Color.FromRgb(247, 247, 247));
                    ((Border)ele).Background = new SolidColorBrush(Color.FromRgb(247, 247, 247));
                    //箭头图标
                    var dockpanel = ((Border)ele).Child as DockPanel;
                    foreach(var item in dockpanel.Children)
                    {
                        if(item is Image)
                        {
                            ((Image)item).Source = new BitmapImage(new Uri("/Resources/arrowGray.png", UriKind.RelativeOrAbsolute));
                        }
                    }
                }
            }
            //设置自己为选中样式
            var border = (Border)sender;
            border.BorderBrush = new SolidColorBrush(Color.FromRgb(226, 242, 255));
            border.Background = new SolidColorBrush(Color.FromRgb(226, 242, 255));

            var dockpanel2 = border.Child as DockPanel;
            foreach (var item in dockpanel2.Children)
            {
                if (item is Image)
                {
                    ((Image)item).Source = new BitmapImage(new Uri("/Resources/selested.png", UriKind.RelativeOrAbsolute));
                }
            }
        }

    }
}
