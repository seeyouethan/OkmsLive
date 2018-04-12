using OkmsLive.Enums;
using OkmsLive.HelpersLib;
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
    /// ChooseCameraWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ChooseCameraWindow : Window
    {
        public ChooseCameraWindow()
        {
            InitializeComponent();

            //获取有效的摄像头
            var cameraList = VideoHelper.GetAllCameras();
            var maxwidth = 94;
            foreach (var item in cameraList)
            {
                StackPanel stackpanel = new StackPanel();
                stackpanel.Height = 30;
                stackpanel.HorizontalAlignment = HorizontalAlignment.Left;
                stackpanel.Orientation = Orientation.Horizontal;

                RadioButton radioButton = new RadioButton();
                radioButton.GroupName = "Cameras";
                radioButton.Margin = new Thickness(10, 0, 0, 0);
                radioButton.VerticalContentAlignment = VerticalAlignment.Center;
                var label = new Label();
                label.Content = item.Value;
                radioButton.Content = label;
                radioButton.Click += RadioButton_Click;

                var width = GetTextDisplayWidthHelper.GetTextDisplayWidth(label)+50;
                if (maxwidth<width)
                {
                    maxwidth = Convert.ToInt32(Math.Ceiling(width));
                }

                stackpanel.Children.Add(radioButton);
                CameraStackpanel.Children.Add(stackpanel);
            }
            this.Height = 34 + 30 * cameraList.Count;
            this.Width = maxwidth;
        } 

        public MainWindow mainWindow = new MainWindow();


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //获取配置文件中设置的摄像头
            var cameraName = VideoHelper.GetCameraName();
            if (string.IsNullOrEmpty(cameraName))
            {
                return;
            }

            foreach(var item in CameraStackpanel.Children)
            {
                var name=(((item as StackPanel).Children[0] as RadioButton).Content as Label).Content.ToString();
                if (name == cameraName)
                {
                    ((item as StackPanel).Children[0] as RadioButton).IsChecked = true;
                }
            }            
        }


        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            var raditobutton = sender as RadioButton;
            raditobutton.IsChecked = true;
            var label = raditobutton.Content as Label;
            var cameraName = label.Content.ToString();
            
            if (cameraName != "无")
            {
                //设置直播方式为直播摄像头
                mainWindow.liveType = LiveType.Camera;
            }
            mainWindow.SetCameraName(cameraName);
            mainWindow.CloseCameraWindow();
        }
    }
}
