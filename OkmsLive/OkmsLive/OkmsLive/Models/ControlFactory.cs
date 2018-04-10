using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System;
using System.Windows.Data;

namespace OkmsLive.Models
{
    /// <summary>
    /// 控件工厂
    /// </summary>
    public class ControlFactory
    {
        /// <summary>
        /// 创建未开始的直播的Control
        /// </summary>
        /// <param name="name">直播名称</param>
        /// <param name="time">直播时间</param>
        /// <returns></returns>
        public UIElement CreateCourseControlNotStarted(string name,string time)
        {
            /*
             * 创建如下样式的control 未开始的课程
              <Border Background="#F7F7F7" Height="50" Margin="0,4,0,4" Width="660" BorderThickness="1" CornerRadius="1" BorderBrush="#FFF7F7F7" Cursor="Hand">
                    <DockPanel>
                        <Image Width="24" Height="24" Stretch="Fill"  Source="/Resources/arrowGray.png"/>
                        <Label VerticalContentAlignment="Center" Foreground="#FF333333" FontSize="14">OKMS需求研讨</Label>
                        <Label VerticalContentAlignment="Center" Foreground="#FF666666" FontSize="14">（15:00-16:00）</Label>
                        <StackPanel DockPanel.Dock="Right" Orientation="Horizontal" FlowDirection="RightToLeft">
                            <Label VerticalContentAlignment="Center" Foreground="#FF666666" FontSize="14">即将开始</Label>
                        </StackPanel>
                    </DockPanel>
                </Border>
             */
            var border = new Border();
            border.Background = new SolidColorBrush(Color.FromRgb(247, 247, 247));
            border.Height = 50;
            border.Width = 660;
            border.Margin = new Thickness(0, 4, 0, 4);
            border.BorderThickness = new Thickness(1);
            border.CornerRadius = new CornerRadius(1);
            border.BorderBrush= new SolidColorBrush(Color.FromRgb(247, 247, 247));
            border.Cursor = Cursors.Hand;

            var dockpanel = new DockPanel();

            var img = new Image();
            img.Source= new BitmapImage(new Uri("/Resources/arrowGray.png", UriKind.RelativeOrAbsolute));
            img.Width = 24;
            img.Height = 24;
            img.Stretch = Stretch.Fill;
            var nameLabel = new Label();
            nameLabel.Content = name;
            nameLabel.FontSize = 14;
            nameLabel.Foreground = new SolidColorBrush(Color.FromRgb(51,51,51));
            nameLabel.VerticalContentAlignment = VerticalAlignment.Center;

            var timeLabel = new Label();
            timeLabel.Content = time;
            timeLabel.FontSize = 14;
            timeLabel.Foreground = new SolidColorBrush(Color.FromRgb(102, 102, 102));
            timeLabel.VerticalContentAlignment = VerticalAlignment.Center;

            var stackpanel2 = new StackPanel();
            stackpanel2.Orientation = Orientation.Horizontal;
            stackpanel2.FlowDirection = FlowDirection.RightToLeft;

            var label = new Label();
            label.Content = "即将开始";
            label.Foreground = new SolidColorBrush(Color.FromRgb(102, 102, 102));
            label.VerticalContentAlignment = VerticalAlignment.Center;
            label.FontSize = 14;

            stackpanel2.Children.Add(label);

            dockpanel.Children.Add(img);
            dockpanel.Children.Add(nameLabel);
            dockpanel.Children.Add(timeLabel);
            dockpanel.Children.Add(stackpanel2);
            DockPanel.SetDock(stackpanel2, Dock.Right);

            border.Child = dockpanel;


            return border;
        }

        

        /// <summary>
        /// 创建正在进行中的直播Control
        /// </summary>
        /// <param name="name">直播名称</param>
        /// <param name="time">直播时间</param>
        /// <returns></returns>
        public UIElement CreateCourseControlStarted(string name, string time)
        {
            /*
             * 创建如下样式的control  已经开始的课程                
                <Border Background="#FFE2F2FF" Height="50" Margin="0,4,0,4" Width="660" BorderThickness="1" CornerRadius="1" BorderBrush="#FFE2F2FF">
                    <DockPanel>
                        <Image Width="24" Height="24" Stretch="Fill"  Source="/Resources/selested.png"/>
                        <Label VerticalContentAlignment="Center" Foreground="#FF333333" FontSize="14">OKMS需求研讨</Label>
                        <Label VerticalContentAlignment="Center" Foreground="#FF666666" FontSize="14">（15:00-16:00）</Label>
                        <StackPanel  DockPanel.Dock="Right" Orientation="Horizontal" FlowDirection="RightToLeft" HorizontalAlignment="Stretch">
                            <Label VerticalContentAlignment="Center" Foreground="#FF41A0E9" FontSize="14">...直播中</Label>
                        </StackPanel>
                    </DockPanel>
                </Border>
             */
            var border = new Border();
            border.Background = new SolidColorBrush(Color.FromRgb(226, 242, 255));
            border.Height = 50;
            border.Width = 660;
            border.Margin = new Thickness(0, 4, 0, 4);
            border.BorderThickness = new Thickness(1);
            border.CornerRadius = new CornerRadius(1);
            border.BorderBrush = new SolidColorBrush(Color.FromRgb(226, 242, 255));
            border.Cursor = Cursors.Hand;

            var dockpanel = new DockPanel();
            
            var img = new Image();
            img.Source = new BitmapImage(new Uri("/Resources/selested.png", UriKind.RelativeOrAbsolute));
            img.Width = 24;
            img.Height = 24;
            img.Stretch = Stretch.Fill;
            var nameLabel = new Label();
            nameLabel.Content = name;
            nameLabel.FontSize = 14;
            nameLabel.Foreground = new SolidColorBrush(Color.FromRgb(51, 51, 51));
            nameLabel.VerticalContentAlignment = VerticalAlignment.Center;

            var timeLabel = new Label();
            timeLabel.Content = time;
            timeLabel.FontSize = 14;
            timeLabel.Foreground = new SolidColorBrush(Color.FromRgb(102, 102, 102));
            timeLabel.VerticalContentAlignment = VerticalAlignment.Center;

            var stackpanel2 = new StackPanel();            
            stackpanel2.Orientation = Orientation.Horizontal;
            stackpanel2.FlowDirection = FlowDirection.RightToLeft;

            var label = new Label();
            label.Content = "...直播中";
            label.VerticalContentAlignment = VerticalAlignment.Center;
            label.FontSize = 14;
            label.Foreground = new SolidColorBrush(Color.FromRgb(102, 102, 102));

            stackpanel2.Children.Add(label);

            dockpanel.Children.Add(img);
            dockpanel.Children.Add(nameLabel);
            dockpanel.Children.Add(timeLabel);
            dockpanel.Children.Add(stackpanel2);
            DockPanel.SetDock(stackpanel2, Dock.Right);

            border.Child = dockpanel;

            return border;
        }

        public void AddMessage(string imgUrl,string userName,string time,string message,Grid MessageGrid)
        {
            /*
             * 创建聊天界面的 头像 xxx yyyy-mm-dd
             * 
                <StackPanel  Grid.Row="0" Grid.Column="0" Orientation="Horizontal" VerticalAlignment="Center">
                    <Grid Margin="8,0,0,0">
                        <Image Width="32" Height="32" Stretch="Fill"  Source="/Resources/headphoto.jpg">
                            <Image.OpacityMask>
                                <VisualBrush Visual="{Binding ElementName=OnlineImgBorder}" />
                            </Image.OpacityMask>
                        </Image>
                    </Grid>
                    <Label VerticalAlignment="Center" FontSize="12px" Foreground="#333333" Width="50">刘德华</Label>
                </StackPanel>
                <TextBlock FontSize="10" Foreground="#888888" Grid.Row="0" Grid.Column="1" HorizontalAlignment="Right" Margin="0,0,6,0" VerticalAlignment="Center">2018-11-16 11:28:23</TextBlock>
                <StackPanel Grid.Row="1" Grid.Column="0" Grid.ColumnSpan="2" Margin="20,0,0,0" Width="150">
                    <TextBlock TextWrapping="Wrap">这里是信息内容</TextBlock>
                </StackPanel>
             */


            RowDefinition row0 = new RowDefinition();   //  头像 xxx yyyy-mm-dd
            row0.Height = new GridLength(40);
            RowDefinition row1 = new RowDefinition();   //说的内容

            var stackpanel = new StackPanel();
            stackpanel.Orientation = Orientation.Horizontal;
            stackpanel.VerticalAlignment = VerticalAlignment.Center;

            var grid = new Grid();
            grid.Margin = new Thickness(8, 0, 0, 0);
            //头像
            var img = new Image();
            img.Source=new BitmapImage(new Uri(imgUrl, UriKind.RelativeOrAbsolute));
            img.Width = 32;
            img.Height = 32;
            img.Stretch = Stretch.Fill;
            var bind = new Binding();
            bind.ElementName = "OnlineImgBorder";//界面上已经写了OnlineImgBorder
            var vbsh = new VisualBrush();
            BindingOperations.SetBinding(vbsh, VisualBrush.VisualProperty, bind);
            img.OpacityMask = vbsh;
            //用户名
            var label = new Label();
            label.Content = userName;
            label.VerticalAlignment = VerticalAlignment.Center;
            label.FontSize = 12;
            label.Foreground= new SolidColorBrush(Color.FromRgb(51, 51, 51));
            label.Width = 50;
            //时间
            var textblock = new TextBlock();
            textblock.FontSize = 10;
            textblock.Margin = new Thickness(0, 0, 6, 0);
            textblock.VerticalAlignment = VerticalAlignment.Center;
            textblock.HorizontalAlignment = HorizontalAlignment.Right;
            textblock.Foreground = new SolidColorBrush(Color.FromRgb(136, 136, 136));
            textblock.Text = time;
            //消息
            var messagestackpanel = new StackPanel();
            var messagetextblock = new TextBlock();

            messagestackpanel.Margin= new Thickness(20, 0, 0, 0);
            messagestackpanel.Width = 150;
            messagetextblock.TextWrapping = TextWrapping.Wrap;
            messagetextblock.Text = message;
            messagestackpanel.Children.Add(messagetextblock);

            MessageGrid.RowDefinitions.Add(row0);
            MessageGrid.RowDefinitions.Add(row1);

            grid.Children.Add(img);
            stackpanel.Children.Add(grid);
            stackpanel.Children.Add(label);

            MessageGrid.Children.Add(stackpanel);
            MessageGrid.Children.Add(textblock);
            MessageGrid.Children.Add(messagestackpanel);

            Grid.SetColumn(stackpanel, 0);
            Grid.SetColumn(textblock, 1);
            Grid.SetColumn(messagestackpanel, 0);

            Grid.SetRow(stackpanel, MessageGrid.RowDefinitions.Count - 2);
            Grid.SetRow(textblock, MessageGrid.RowDefinitions.Count - 2);
            Grid.SetRow(messagestackpanel, MessageGrid.RowDefinitions.Count - 1);

            Grid.SetColumnSpan(messagestackpanel, 2);
        }

        public UIElement GreateOnlineUser(string imgUrl,string userName)
        {
            /*
             * 创建当前在线人员  头像 姓名 
                <StackPanel Margin="0,6,0,6">
                    <StackPanel Width="270" Height="40" Orientation="Horizontal">
                        <Grid>
                            <Image Width="32" Height="32" Stretch="Fill"  Source="/Resources/headphoto.jpg">
                                <Image.OpacityMask>
                                    <VisualBrush Visual="{Binding ElementName=OnlineImgBorder}" />
                                </Image.OpacityMask>
                            </Image>
                        </Grid>
                        <Label VerticalAlignment="Center" FontSize="12px" Foreground="#666666" Width="50">黎明</Label>
                        <Button Style="{StaticResource ButtonStyle2}" Margin="110,0,0,0" FontSize="12" Width="70" Height="30" HorizontalAlignment="Right" VerticalAlignment="Center">邀请发言</Button>          
                    </StackPanel>
                </StackPanel>
             */
            var stackpanel = new StackPanel();
            stackpanel.Margin = new Thickness(0, 6, 0, 6);
            stackpanel.Cursor = Cursors.Hand;

            var stackpanel2 = new StackPanel();
            stackpanel2.Width = 270;
            stackpanel2.Height = 40;
            stackpanel2.Orientation = Orientation.Horizontal;

            var grid = new Grid();

            var img = new Image();
            img.Source = new BitmapImage(new Uri(imgUrl, UriKind.RelativeOrAbsolute));
            img.Width = 32;
            img.Height = 32;
            img.Stretch = Stretch.Fill;
            var bind = new Binding();
            bind.ElementName = "OnlineImgBorder";//界面上已经写了OnlineImgBorder
            var vbsh = new VisualBrush();
            BindingOperations.SetBinding(vbsh, VisualBrush.VisualProperty, bind);
            img.OpacityMask = vbsh;

            var label = new Label();
            label.VerticalAlignment = VerticalAlignment.Center;
            label.FontSize = 12;
            label.Foreground= new SolidColorBrush(Color.FromRgb(102, 102, 102));
            label.Width = 50;
            label.Content = userName;

            var button = new Button();

            grid.Children.Add(img);
            stackpanel2.Children.Add(grid);
            stackpanel2.Children.Add(label);
            stackpanel.Children.Add(stackpanel2);

            return stackpanel;
        }

    }
}
