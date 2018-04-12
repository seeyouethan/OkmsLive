using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System;
using System.Windows.Data;
using System.Threading;
using System.ComponentModel;

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
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="imgUrl"></param>
        /// <param name="userName"></param>
        /// <param name="time"></param>
        /// <param name="message"></param>
        /// <param name="MessageGrid"></param>
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
                    <StackPanel Width="190" Height="40" Orientation="Horizontal">
                        <Grid>
                            <Image Width="32" Height="32" Stretch="Fill"  Source="/Resources/headphoto.jpg">
                                <Image.OpacityMask>
                                    <VisualBrush Visual="{Binding ElementName=OnlineImgBorder}" />
                                </Image.OpacityMask>
                            </Image>
                        </Grid>                        
                        <Label VerticalAlignment="Center" FontSize="12px" Foreground="#666666" Width="50">黎明</Label>
                        <Label VerticalAlignment="Center" FontSize="12px" Foreground="#888888" Width="60">连接中...</Label>
                        <Image Source="/Resources/speak.png" Width="24" Height="24"></Image>
                        <Image Source="/Resources/hand.png" Width="24" Height="24"></Image>
                        <Image Margin="10,0,10,0" Source="/Resources/yes.png" Width="16" Height="16" Stretch="UniformToFill" Cursor="Hand"></Image>
                        <Label Margin="0,0,10,0" VerticalAlignment="Center" Height="16" Background="#999999" Width="1"></Label>
                        <Image Source="/Resources/no.png" Width="16" Height="16" Stretch="UniformToFill" Cursor="Hand"></Image>
                        <!--有时候margin会为6,0,0,0-->
                        <Button Style="{StaticResource ButtonStyle2}" Margin="30,0,0,0" FontSize="12" Width="70" Height="30" HorizontalAlignment="Right" VerticalAlignment="Center">邀请发言</Button>          
                    </StackPanel>
                </StackPanel>
             */
            var stackpanel = new StackPanel();
            stackpanel.Margin = new Thickness(0, 6, 0, 6);
            stackpanel.Cursor = Cursors.Hand;
            stackpanel.Background = ColorModels.OnlineUser;

            var stackpanel2 = new StackPanel();
            stackpanel2.Width = 190;
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

            //正在发言小图标
            var img0 = new Image();
            img0.Source = new BitmapImage(new Uri("/Resources/speak.png", UriKind.RelativeOrAbsolute));
            img0.Height = 24;
            img0.Width = 24;
            img0.Visibility = Visibility.Collapsed;

            //举手小图标
            var img1 = new Image();
            img1.Source = new BitmapImage(new Uri("/Resources/hand.png", UriKind.RelativeOrAbsolute));
            img1.Height = 24;
            img1.Width = 24;
            img1.Visibility = Visibility.Collapsed;

            //对号小图标
            var img2 = new Image();
            img2.Source = new BitmapImage(new Uri("/Resources/yes.png", UriKind.RelativeOrAbsolute));
            img2.Height = 16;
            img2.Width = 16;
            img2.Cursor = Cursors.Hand;
            img2.Margin = new Thickness(10,0,10,0);
            img2.Stretch = Stretch.UniformToFill;
            img2.Visibility = Visibility.Collapsed;
            img2.MouseLeftButtonDown += AllowImg_MouseLeftButtonDown;

            //叉号小图标
            var img3 = new Image();
            img3.Source = new BitmapImage(new Uri("/Resources/no.png", UriKind.RelativeOrAbsolute));
            img3.Height = 16;
            img3.Width = 16;
            img3.Cursor = Cursors.Hand;
            img3.Stretch = Stretch.UniformToFill;
            img3.Visibility = Visibility.Collapsed;
            img3.MouseLeftButtonDown += RejectImg_MouseLeftButtonDown;


            //邀请与拒绝中间的 竖杠
            var labelS = new Label();
            labelS.VerticalAlignment = VerticalAlignment.Center;
            labelS.Height = 16;
            labelS.Background = new SolidColorBrush(Color.FromRgb(153, 153, 153));
            labelS.Width = 1;
            labelS.Margin = new Thickness(0, 0, 10, 0);
            labelS.Visibility = Visibility.Collapsed;


            //状态栏小图标
            var button = new Button();
            button.Margin = new Thickness(30, 0, 0, 0);
            button.FontSize = 12;
            button.Width = 70;
            button.Height = 30;
            button.HorizontalAlignment = HorizontalAlignment.Right;
            button.VerticalAlignment = VerticalAlignment.Center;
            button.Content = "邀请发言";
            //设置样式
            button.SetValue(Button.StyleProperty, Application.Current.Resources["ButtonStyle2"]);
            button.Click += Button_Click;
            button.Visibility = Visibility.Collapsed;

            //连接中... label
            var labelStatus = new Label();
            labelStatus.Content = "连接中...";
            labelStatus.VerticalAlignment = VerticalAlignment.Center;
            labelStatus.Width = 60;
            labelStatus.FontSize = 12;
            labelStatus.Foreground = new SolidColorBrush(Color.FromRgb(136, 136, 136));
            labelStatus.Visibility = Visibility.Collapsed;
            

            grid.Children.Add(img);
            stackpanel2.Children.Add(grid);
            stackpanel2.Children.Add(label);
            stackpanel2.Children.Add(labelStatus);
            stackpanel2.Children.Add(img0);
            stackpanel2.Children.Add(img1);
            stackpanel2.Children.Add(img2);
            stackpanel2.Children.Add(labelS);
            stackpanel2.Children.Add(img3);
            stackpanel2.Children.Add(button);
            stackpanel.Children.Add(stackpanel2);

            stackpanel.MouseEnter += Stackpanel_MouseEnter;
            stackpanel.MouseLeave += Stackpanel_MouseLeave;
            
            return stackpanel;
        }

        /// <summary>
        /// 鼠标进入控件 显示邀请发言按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Stackpanel_MouseEnter(object sender, MouseEventArgs e)
        {
            var ele = sender as StackPanel;
            var button = GetButton(ele);
            button.Visibility = Visibility.Visible;
            ele.Background = ColorModels.OnlineUserHover;
        }

        /// <summary>
        /// 鼠标离开控件 隐藏邀请发言按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Stackpanel_MouseLeave(object sender, MouseEventArgs e)
        {
            var ele = sender as StackPanel;
            var button = GetButton(ele);
            button.Visibility = Visibility.Collapsed;
            ele.Background = ColorModels.OnlineUser;
        }

        /// <summary>
        /// 【对号】图标点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AllowImg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var img = sender as Image;
            var stackpanel = VisualTreeHelper.GetParent(VisualTreeHelper.GetParent(img) as StackPanel) as StackPanel;
            /*
             * 允许发言
             * 1.隐藏image1 image2 image3 labels
             * 2.显示 连接中... 状态
             * todo:3.发送允许发言通信
             */
            var image1 = GetImage1(stackpanel);
            var image3 = GetImage3(stackpanel);
            var labelS = GetLabelS(stackpanel);
            var labelStatus = GetLabelStatus(stackpanel);
            img.Visibility = Visibility.Collapsed;
            image1.Visibility = Visibility.Collapsed;
            image3.Visibility = Visibility.Collapsed;
            labelS.Visibility = Visibility.Collapsed;
            labelStatus.Visibility = Visibility.Visible;
            AllowTalk(stackpanel);
        }
        /// <summary>
        /// 【叉号】图标点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RejectImg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var img = sender as Image;
            var stackpanel = VisualTreeHelper.GetParent(VisualTreeHelper.GetParent(img) as StackPanel) as StackPanel;
            /*
             * 拒绝发言
             * 1.隐藏image1 image2 image3 labels
             * todo:2.发送拒绝发言通信
             */
            var image1 = GetImage1(stackpanel);
            var image2 = GetImage2(stackpanel);
            var labelS = GetLabelS(stackpanel);
            img.Visibility = Visibility.Collapsed;
            image1.Visibility = Visibility.Collapsed;
            image2.Visibility = Visibility.Collapsed;
            labelS.Visibility = Visibility.Collapsed;
            RejectTalk(stackpanel);
        }


        /// <summary>
        /// 点击邀请发言按钮 点击结束发言
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var stackpanel = VisualTreeHelper.GetParent(VisualTreeHelper.GetParent(btn) as StackPanel) as StackPanel;
            var labelStatus = GetLabelStatus(stackpanel);
            var button = GetButton(stackpanel);
            var img = GetImage0(stackpanel);
            if (btn.Content.ToString() == "邀请发言")
            {
                /*
                 * 1.界面修改 显示状态栏 连接中...
                 * 2.隐藏发言按钮
                 * 3.移除stackpanel的mouseenter事件
                 * todo:4.发送邀请发言请求 建立后台任务 发送邀请发言
                 */
                labelStatus.Visibility = Visibility.Visible;
                button.Visibility = Visibility.Collapsed;
                stackpanel.MouseEnter -= Stackpanel_MouseEnter;
                SendTalkInvitation(stackpanel);
                return;
            }            
            if (btn.Content.ToString() == "停止发言")
            {
                /*
                 * 1.按钮的Content设置为“邀请发言” margin设置为（30,0,0,0）
                 * 2.隐藏发言图标 隐藏button按钮
                 * 3.移除stackpanel的mouseenter事件
                 * todo:4.发送停止发言请求 建立后台任务 发送停止发言
                 */
                button.Content = "邀请发言";
                button.Margin = new Thickness(30,0,0,0);
                button.Visibility = Visibility.Collapsed;
                img.Visibility = Visibility.Collapsed;
                stackpanel.MouseEnter -= Stackpanel_MouseEnter;
                EndTalkInvitation(stackpanel);
                return;
            }
        }


        /// <summary>
        /// 发送发言邀请
        /// </summary>
        /// <param name="stackPanel">父Panel 根据这个Panel得到其子控件 修改子控件的Visiblility、触发事件</param>
        private void SendTalkInvitation(StackPanel stackPanel)
        {
            BackgroundWorker worker = new BackgroundWorker();

            worker.DoWork += (o, ea) =>
            {
                // 耗时操作 发送邀请发言
                Thread.Sleep(3000); 
            };

            worker.RunWorkerCompleted += (o, ea) =>
            {
                /*
                 * 如果同意发言则将 发言的图标显示出来 添加一个用户发言的视频窗口
                 * 如果不同意发言则 弹出提示对话框
                 */
                if (true)
                {
                    /*
                     * 1.隐藏 “连接中...”
                     * 2.显示发言图标
                     * 3.修改“邀请发言”按钮的margin为(6,0,0,0);
                     * 4.鼠标移动上去后 显示为 “停止发言”
                     * 5.恢复Stackpanel的mouseEnter事件
                     * todo:5.添加一个用户发言的视频窗口
                     */
                    var labelStatus = GetLabelStatus(stackPanel); 
                    var button = GetButton(stackPanel);
                    var image = GetImage0(stackPanel);
                    labelStatus.Visibility = Visibility.Collapsed;
                    image.Visibility = Visibility.Visible;
                    button.Margin = new Thickness(6, 0, 0, 0);
                    button.Content = "停止发言";
                    stackPanel.Background = ColorModels.OnlineUser;
                    stackPanel.MouseEnter += Stackpanel_MouseEnter;
                }
                else
                {
                    MessageBox.Show("xxx拒绝了发言","提示");
                }

            };
            worker.RunWorkerAsync();
        }

        /// <summary>
        /// 结束发言
        /// </summary>
        /// <param name="stackPanel"></param>
        private void EndTalkInvitation(StackPanel stackPanel)
        {
            BackgroundWorker worker = new BackgroundWorker();

            worker.DoWork += (o, ea) =>
            {
                // 耗时操作 发送停止发言
                Thread.Sleep(3000);
            };

            worker.RunWorkerCompleted += (o, ea) =>
            {
                if (true)
                {
                    /*
                     * 1.恢复stackpanel的mouseenter事件
                     * todo:2.删除对应的用户发言的视频窗口
                     */
                    stackPanel.MouseEnter += Stackpanel_MouseEnter;
                }
                else
                {
                    MessageBox.Show("停止xxx发言失败", "提示");
                }

            };
            worker.RunWorkerAsync();
        }



        /// <summary>
        /// 收到请求发言
        /// </summary>
        /// <param name="stackPanel"></param>
        public void RequestTalk(StackPanel stackPanel)
        {
            /*
             * 1.显示举手图标
             * 2.显示对号、竖杠、叉号
             * 3.移除stackpanel的mouseenter事件
             * 
             */
            var image1 = GetImage1(stackPanel);
            var image2 = GetImage2(stackPanel);
            var image3 = GetImage3(stackPanel);
            var labels = GetLabelS(stackPanel);
            image1.Visibility = Visibility.Visible;
            image2.Visibility = Visibility.Visible;
            image3.Visibility = Visibility.Visible;
            labels.Visibility = Visibility.Visible;

            stackPanel.MouseEnter -= Stackpanel_MouseEnter;

        }

        /// <summary>
        /// 请求发言被允许
        /// </summary>
        /// <param name="stackPanel"></param>
        public void AllowTalk(StackPanel stackPanel)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (o, ea) =>
            {
                // 耗时操作 允许发言
                Thread.Sleep(3000);
            };

            worker.RunWorkerCompleted += (o, ea) =>
            {
                if (true)
                {
                    /*
                     * 1.隐藏 “连接中...”
                     * 2.显示发言图标
                     * 3.修改“邀请发言”按钮的margin为(6,0,0,0);
                     * 4.鼠标移动上去后 显示为 “停止发言”
                     * 5.恢复Stackpanel的mouseEnter事件
                     * todo:5.添加一个用户发言的视频窗口
                     */
                    var labelStatus = GetLabelStatus(stackPanel);
                    var button = GetButton(stackPanel);
                    var image = GetImage0(stackPanel);
                    labelStatus.Visibility = Visibility.Collapsed;
                    image.Visibility = Visibility.Visible;
                    button.Margin = new Thickness(6, 0, 0, 0);
                    button.Content = "停止发言";
                    stackPanel.Background = ColorModels.OnlineUser;
                    stackPanel.MouseEnter += Stackpanel_MouseEnter;
                }
                else
                {
                    MessageBox.Show("xxx拒绝了发言", "提示");
                }

            };
            worker.RunWorkerAsync();
        }



        /// <summary>
        /// 请求发言被驳回
        /// </summary>
        /// <param name="stackPanel"></param>
        public void RejectTalk(StackPanel stackPanel)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (o, ea) =>
            {
                // 耗时操作 发送通信 请求发言被驳回
                Thread.Sleep(2000);
            };

            worker.RunWorkerCompleted += (o, ea) =>
            {
                if (true)
                {
                    /*
                     * 1.隐藏 “连接中...”
                     * 2.显示发言图标
                     * 3.修改“邀请发言”按钮的margin为(30,0,0,0);
                     * 4.鼠标移动上去后 显示为 “停止发言”
                     * 5.恢复Stackpanel的mouseEnter事件
                     * todo:5.添加一个用户发言的视频窗口
                     */
                    stackPanel.MouseEnter += Stackpanel_MouseEnter;
                }
                else
                {
                    MessageBox.Show("xxx拒绝了发言", "提示");
                }

            };
            worker.RunWorkerAsync();
        }



        #region 根据模板获取子控件


            /*
             * 创建当前在线人员  头像 姓名 
                <StackPanel Margin="0,6,0,6">
                    <StackPanel Width="190" Height="40" Orientation="Horizontal">
                        <Grid>
                            <Image Width="32" Height="32" Stretch="Fill"  Source="/Resources/headphoto.jpg">
                                <Image.OpacityMask>
                                    <VisualBrush Visual="{Binding ElementName=OnlineImgBorder}" />
                                </Image.OpacityMask>
                            </Image>
                        </Grid>                        
                        <Label VerticalAlignment="Center" FontSize="12px" Foreground="#666666" Width="50">黎明</Label>
                        <Label VerticalAlignment="Center" FontSize="12px" Foreground="#888888" Width="60">连接中...</Label>
                        <Image Source="/Resources/speak.png" Width="24" Height="24"></Image>
                        <Image Source="/Resources/hand.png" Width="24" Height="24"></Image>
                        <Image Margin="10,0,10,0" Source="/Resources/yes.png" Width="16" Height="16" Stretch="UniformToFill" Cursor="Hand"></Image>
                        <Label Margin="0,0,10,0" VerticalAlignment="Center" Height="16" Background="#999999" Width="1"></Label>
                        <Image Source="/Resources/no.png" Width="16" Height="16" Stretch="UniformToFill" Cursor="Hand"></Image>
                        <!--有时候margin会为6,0,0,0-->
                        <Button Style="{StaticResource ButtonStyle2}" Margin="30,0,0,0" FontSize="12" Width="70" Height="30" HorizontalAlignment="Right" VerticalAlignment="Center">邀请发言</Button>          
                    </StackPanel>
                </StackPanel>
             */

        /// <summary>
        /// 获取状态Label
        /// </summary>
        /// <param name="stackPanel"></param>
        /// <returns></returns>
        public Label GetLabelStatus(StackPanel stackPanel)
        {
            var secondStackpanel = stackPanel.Children[0] as StackPanel;
            return VisualTreeHelper.GetChild(secondStackpanel, 2) as Label;
        }

        /// <summary>
        /// 获取【举手】图标
        /// </summary>
        /// <param name="stackPanel"></param>
        /// <returns></returns>
        public Image GetImage0(StackPanel stackPanel)
        {
            var secondStackpanel = stackPanel.Children[0] as StackPanel;
            return VisualTreeHelper.GetChild(secondStackpanel, 3) as Image;
        }
        /// <summary>
        /// 获取【举手】图标
        /// </summary>
        /// <param name="stackPanel"></param>
        /// <returns></returns>
        public Image GetImage1(StackPanel stackPanel)
        {
            var secondStackpanel = stackPanel.Children[0] as StackPanel;
            return VisualTreeHelper.GetChild(secondStackpanel, 4) as Image;
        }
        /// <summary>
        /// 获取【对号】图标
        /// </summary>
        /// <param name="stackPanel"></param>
        /// <returns></returns>
        public Image GetImage2(StackPanel stackPanel)
        {
            var secondStackpanel = stackPanel.Children[0] as StackPanel;
            return VisualTreeHelper.GetChild(secondStackpanel, 5) as Image;
        }
        /// <summary>
        /// 获取【叉号】图标
        /// </summary>
        /// <param name="stackPanel"></param>
        /// <returns></returns>
        public Image GetImage3(StackPanel stackPanel)
        {
            var secondStackpanel = stackPanel.Children[0] as StackPanel;
            return VisualTreeHelper.GetChild(secondStackpanel, 7) as Image;
        }
        /// <summary>
        /// 获取【竖杠】Label
        /// </summary>
        /// <param name="stackPanel"></param>
        /// <returns></returns>
        public Label GetLabelS(StackPanel stackPanel)
        {
            var secondStackpanel = stackPanel.Children[0] as StackPanel;
            return VisualTreeHelper.GetChild(secondStackpanel, 6) as Label;
        }
        /// <summary>
        /// 获取【邀请发言】按钮
        /// </summary>
        /// <param name="stackPanel"></param>
        /// <returns></returns>
        public Button GetButton(StackPanel stackPanel)
        {
            var secondStackpanel = stackPanel.Children[0] as StackPanel;
            return VisualTreeHelper.GetChild(secondStackpanel, 8) as Button;
        }

        #endregion
    }
}
