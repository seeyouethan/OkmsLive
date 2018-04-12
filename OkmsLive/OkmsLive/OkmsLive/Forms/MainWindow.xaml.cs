using AForge.Video;
using AForge.Video.DirectShow;
using OkmsLive.Enums;
using OkmsLive.HelpersLib;
using OkmsLive.Models;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
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
        private ChooseLiveTypeWindow liveTypeWindow;//直播方式选择窗口
        private ChooseCameraWindow cameraWindow;//直播方式选择窗口
        private ShareWindow shareWindow;//分享窗口
        private ControlFactory factory = new ControlFactory();

        public LiveType liveType = LiveType.Desktop;

        private string cameraName = "";//摄像头名称
        private string cameraResolution = "";//摄像头分辨率
        private int cameraResolutionIndex = 0;//摄像头分辨率index

        private static Process p = new Process();//FFmpeg进程

        public MainWindow()
        {
            InitializeComponent();

            //模拟对话
            factory.AddMessage("/Resources/headphoto.jpg", "张三", "2018-11-16 11:28:23", "大家好", MessageGrid);
            factory.AddMessage("/Resources/headphoto.jpg", "李四", "2018-11-16 11:28:23", "你好", MessageGrid);
            factory.AddMessage("/Resources/headphoto.jpg", "张三", "2018-11-16 11:28:23", "现在开始讲课，有要发言的同学吗？", MessageGrid);
            factory.AddMessage("/Resources/headphoto.jpg", "刘静杰", "2018-11-16 11:28:23", "老师你好我要发言，我的问题的这个测试长度看看是否是会自动增长的", MessageGrid);
            DiscussPanel.ScrollToBottom();



            var el1 = factory.GreateOnlineUser("/Resources/headphoto.jpg", "Lucy");
            var el2 = factory.GreateOnlineUser("/Resources/headphoto.jpg", "Dav");
            var el3 = factory.GreateOnlineUser("/Resources/headphoto.jpg", "Jerax");
            OnlieUsers.Children.Add(el1);
            OnlieUsers.Children.Add(el2);
            OnlieUsers.Children.Add(el3);


        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cameraName = VideoHelper.GetCameraName();
            cameraResolution = VideoHelper.GetCameraResolution();
            cameraResolutionIndex = VideoHelper.GetCameraResolutionIndex();
            if (cameraName == string.Empty || cameraResolution == string.Empty || cameraName == "无")
            {
                //设置默认的摄像头 设置默认的分辨率 修改appconfig.config文件
                VideoHelper.SetDefaultCamera();
                cameraName = VideoHelper.GetCameraName();
                cameraResolution = VideoHelper.GetCameraResolution();
                if (cameraName == "无")
                {
                    //如果没有摄像头 则默认直播方式是直播桌面
                    liveType = LiveType.Desktop;
                    PreviewDesktop();
                }
                else
                {
                    liveType = LiveType.Camera;
                    PreviewCamera();
                }
            }
            else
            {
                liveType = LiveType.Camera;
                PreviewCamera();
            }
        }



        #region 界面交互

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var dlg = new CloseWindow();
            if (dlg.ShowDialog() == true)
            {
                if (PreviewPlayer.IsRunning)
                {
                    PreviewPlayer.SignalToStop();
                    PreviewPlayer.WaitForStop();
                }
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
            item.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(116, 191, 250));
        }
        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            var item = (StackPanel)sender;
            item.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(66, 160, 234));
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
            var img = (System.Windows.Controls.Image)ele.Children[0];

            //获取当前显示器屏幕
            Rect rc = SystemParameters.WorkArea;//获取当前工作区大小
            if (IsMax)
            {
                Left = (rc.Width - 1280) / 2;
                Top = (rc.Height - 650) / 2;
                Width = 1280;
                Height = 650;
                IsMax = false;
                SizeBtn.ToolTip = "最大化";
                img.Source = new BitmapImage(new Uri("/Resources/toBig.png", UriKind.RelativeOrAbsolute));
                UserOnlinePanel.Height = Height - 85;
                DiscussPanel.Height = Height - 147;
                //重新设置预览区大小
                CameraStackpanel.Width = 876;
                CameraStackpanel.Height = 547;
                if (cameraName != "无")
                {
                    PreviewCamera();
                }
                else
                {
                    PreviewDesktop();
                }
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
                //重新设置预览区大小
                CameraStackpanel.Width = 876 + (rc.Width - 1280);
                CameraStackpanel.Height = 547 + (rc.Height - 650) ;
                if (cameraName != "无")
                {
                    PreviewCamera();
                }
                else
                {
                    PreviewDesktop();
                }

            }
        }
        private void MinBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var window = this;
            window.Topmost = false;
            window.WindowState = WindowState.Minimized;
        }

        private void LiveBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            if (IsLive)
            {
                LiveBtn.Source = new BitmapImage(new Uri("/Resources/stopOn.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                LiveBtn.Source = new BitmapImage(new Uri("/Resources/startOn.png", UriKind.RelativeOrAbsolute));
            }
        }

        private void LiveBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            if (IsLive)
            {
                LiveBtn.Source = new BitmapImage(new Uri("/Resources/stop.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                LiveBtn.Source = new BitmapImage(new Uri("/Resources/start.png", UriKind.RelativeOrAbsolute));
            }
        }

        #endregion




        

        /// <summary>
        /// 点击直播按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LiveBtn_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            if (IsLive)
            {
                IsLive = false;
                LiveBtn.Source= new BitmapImage(new Uri("/Resources/stop.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                IsLive = true;
                LiveBtn.Source = new BitmapImage(new Uri("/Resources/start.png", UriKind.RelativeOrAbsolute));
            }
        }

        private void SettingPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //打开设置对话框
            var dlg = new SettingsWindow();
            dlg.ShowDialog();

        }

        private void CameraPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //打开摄像头选择选项
            if (cameraWindow == null)
            {
                cameraWindow = new ChooseCameraWindow();
                cameraWindow.WindowStartupLocation = WindowStartupLocation.Manual;
                cameraWindow.Left = this.Left + 208 + 82;
                cameraWindow.Top = this.Top + this.Height - 110;

                cameraWindow.mainWindow = this;
                cameraWindow.ShowDialog();
            }
            else
            {
                cameraWindow.Close();
                cameraWindow = null;
            }
        }
        /// <summary>
        /// 直播方式选择按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LiveTypePanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //打开直播类型选项
            if (liveTypeWindow == null)
            {
                liveTypeWindow = new ChooseLiveTypeWindow();
                liveTypeWindow.WindowStartupLocation = WindowStartupLocation.Manual;
                liveTypeWindow.Left = this.Left + 208;
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

        /// <summary>
        /// 设置直播方式
        /// </summary>
        /// <param name="type"></param>
        public void SetLiveTypeImgAndText(LiveType type)
        {
            liveTypeWindow.Close();
            liveTypeWindow = null;
            liveType = type;
            if (type == LiveType.Desktop)
            {
                img01.Source = new BitmapImage(new Uri("/Resources/computor.png", UriKind.RelativeOrAbsolute));
                label01.Content = "桌面演示";
                SetCameraName("无");
                PreviewDesktop();
            }
            else if (type == LiveType.Software)
            {
                img01.Source = new BitmapImage(new Uri("/Resources/software.png", UriKind.RelativeOrAbsolute));
                label01.Content = "软件演示";
                //todo:演示软件
            }
            else if (type == LiveType.Window)
            {
                img01.Source = new BitmapImage(new Uri("/Resources/area.png", UriKind.RelativeOrAbsolute));
                label01.Content = "区域演示";
                //todo:演示区域
            }
        }

        /// <summary>
        /// 分享按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShareBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (shareWindow == null)
            {
                shareWindow = new ShareWindow();
                shareWindow.mainWindow = this;
                var p = ShareBtn.PointToScreen(new System.Windows.Point(0, 0));

                shareWindow.WindowStartupLocation = WindowStartupLocation.Manual;
                shareWindow.Left = p.X - 380;
                shareWindow.Top = p.Y+40;

                shareWindow.ShowDialog();
            }
            else
            {
                shareWindow.Close();
                shareWindow = null;
            }
        }

        /// <summary>
        /// 分享窗口关闭
        /// </summary>
        public void CloseShareWindow()
        {
            shareWindow.Close();
            shareWindow = null;
        }
        /// <summary>
        /// 摄像头选择窗口关闭
        /// </summary>
        public void CloseCameraWindow()
        {
            cameraWindow.Close();
            cameraWindow = null;            
        }

        public void SetCameraName(string str)
        {
            cameraName = VideoHelper.GetCameraName();
            if (cameraName == str)
            {
                return;
            }
            else
            {
                if (str != "无")
                {
                    cameraName = str;
                    XmlHelper.SetValue("Camera", str);
                    XmlHelper.SetValue("CameraResolution", 0, VideoHelper.GetCamereFirstResolution(cameraName));
                    cameraName = VideoHelper.GetCameraName();
                    PreviewCamera();
                }
                else
                {
                    XmlHelper.SetValue("Camera", str);
                    cameraName = VideoHelper.GetCameraName();
                    PreviewDesktop();
                }
            }
        }


        /// <summary>
        /// FFmpeg的输出内容
        /// </summary>
        /// <param name="sendProcess"></param>
        /// <param name="output"></param>
        private void FFmpegOutput(object sendProcess, DataReceivedEventArgs output)
        {
            this.Dispatcher.Invoke(new Action(delegate
            {
                if (!String.IsNullOrEmpty(output.Data))
                {
                    LogHelper.AddFFmpegLog(output.Data);
                    //SetStatus(output.Data);
                }
            }));
        }

        /// <summary>
        /// 清除状态栏
        /// </summary>
        /// <param name="strStatus"></param>
        private void ClearStatus(string strStatus)
        {
            FpsLabel.Content = "0帧/秒";
            SpeedLabel.Content = "0k/s";
        }

        /// <summary>
        /// 初始状态
        /// </summary>
        private void Status0()
        {
            ClearStatus("");
        }


        //private void StartLive()
        //{
        //    ParameterizedThreadStart threadStart = new ParameterizedThreadStart(Live);
        //    Thread thread = new Thread(threadStart);

        //    //todo:这里要写上从服务器获取的流媒体服务器地址
        //    var streamUrl = XmlHelper.GetValue("StreamUrl");
        //    //var streamUrl = RtmpServerUrl;
        //    if (string.IsNullOrEmpty(streamUrl))
        //    {
        //        MessageBox.Show("请先设置流媒体服务器URL!", "系统提示");
        //        return;
        //    }
        //    //todo:这里写上要直播课程的串流码
        //    //string strStream = ((List<Course>)CourseList.ItemsSource)[CourseList.SelectedIndex].StreamCode;
        //    var strStream = "okmstest";
        //    LogHelper.AddEventLog("开始直播，串流码为：" + streamUrl + strStream);
        //    if (string.IsNullOrEmpty(strStream))
        //    {
        //        MessageBox.Show("未获取到直播课程的串流码！", "系统提示");
        //        return;
        //    }
        //    if (strStream.Contains("/"))
        //    {
        //        MessageBox.Show("串流码名称中不能够包含特殊字符！", "系统提示");
        //        return;
        //    }

        //    string ffmpegCmd = "";
        //    //Status2();//显示正在连接流媒体服务器

        //    //以下这个list中包含了用户所勾选的音频输入设备的下标（可能存在空字符串的）
        //    var listMicrophonestr = XmlHelper.GetValue("Microphone");
        //    var listMicphone = listMicrophonestr.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
        //    //所有的音频输入设备
        //    var list = AudioHelper.GetMicrophoneDevices2();
        //    var strCmd1 = "";
        //    var strCmd2 = "";
        //    var count = 0;
        //    for (int i = 0; i < listMicphone.Length; i++)
        //    {
        //        if (Convert.ToInt32(listMicphone[i]) < list.Count)
        //        {
        //            strCmd1 += " -f dshow -rtbufsize 100M -i audio=\"" + list[Convert.ToInt32(listMicphone[i])].Value + "\"";
        //            strCmd2 += "[" + (count + 1) + ":a]";
        //            count++;
        //        }
        //    }
        //    if (XmlHelper.GetValue("DesktopAudio") == "1")
        //    {
        //        strCmd1 += " -f dshow -rtbufsize 100M -i audio=\"virtual-audio-capturer\"";

        //        strCmd2 += "[" + (count + 1) + ":a]";
        //        count++;
        //    }
        //    if (liveType == LiveType.Camera)
        //    {
        //        if (LoadCamera())
        //        {
        //            //LogHelper.AddEventLog("开始直播摄像头...");
        //            LiveForm.Visibility = Visibility.Visible;
        //            //直播摄像头
        //            _stopRec = false;
        //            _videoFileName = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".flv";
        //            _videoFileFullPath = FFmpegHelper.VideoPath + _videoFileName;
        //            Thread threadView = new Thread(PreviewMainCamera);
        //            threadView.IsBackground = true;
        //            threadView.Name = "PreviewCameraWhileLiving";
        //            threadView.Start();
        //            if (count == 0)
        //            {
        //                //如果没有选择音频设备，则发送的时候，并不发送音频
        //                //ffmpegCmd = " -re -i \"" + _videoFileFullPath + "\" -vcodec copy -b:v 1M -f flv " + streamUrl + "/" + strStream;
        //                ffmpegCmd = " -re -i \"" + _videoFileFullPath + "\" -vcodec h264 -f flv " + streamUrl + "/" + strStream;
        //                /*
        //                //如果没有选择音频设备，则发送的时候，并不发送音频
        //                ffmpegCmd = " -f dshow -rtbufsize 1700M -i video=\"" + cameraName + "\" -vcodec h264 -r " +
        //                            _frameRate + " -s " + cameraResolution + " -f flv " + streamUrl + "/" + strStream;
        //                ParameterizedThreadStart threadStart = new ParameterizedThreadStart(Live);
        //                Thread thread = new Thread(threadStart);
        //                */
        //            }
        //            else if (count == 1)
        //            {
        //                ffmpegCmd = " -re -i \"" + _videoFileFullPath + "\"" + strCmd1 + " -r " + _frameRate + " -vcodec h264 -acodec aac -ac 2 -ar 44100 -ab 128k  -pix_fmt yuv420p -s " + cameraResolution + " -f flv " + streamUrl + "/" + strStream;
        //                //ffmpegCmd = " -f dshow -rtbufsize 1700M -i video=\"" + cameraName + "\"" + strCmd1 +" -vcodec h264  -r " + _frameRate +" -acodec aac -ac 2 -ar 44100 -ab 128k -b:v 1M -b:v 1M -pix_fmt yuv420p -s " +cameraResolution + " -f flv " + streamUrl + "/" + strStream;
        //            }
        //            else if (count > 1)
        //            {
        //                ffmpegCmd = " -re -i \"" + _videoFileFullPath + "\"" + strCmd1 + " -filter_complex \"" + strCmd2 + "amerge = inputs =" + (count) + "[aout]\"  -map \"[aout]\" -map 0  -vcodec h264 -r " + _frameRate + " -vcodec h264 -acodec aac -ac 2 -ar 44100 -ab 128k -pix_fmt yuv420p  -s " + cameraResolution + " -f flv " + streamUrl + "/" + strStream;
        //                //ffmpegCmd = " -f dshow -rtbufsize 1700M -i video=\"" + cameraName + "\"" + strCmd1 +" -filter_complex \"" + strCmd2 + "amerge = inputs =" + (count) +"[aout]\"  -map \"[aout]\" -map 0  -vcodec h264 -r " + _frameRate + " -acodec aac -ac 2 -ar 44100 -ab 128k -b:v 1M -pix_fmt yuv420p  -s "+cameraResolution + " -f flv " + streamUrl + "/" + strStream;
        //            }
        //            if (IsRecord())
        //            {
        //                ffmpegCmd += " " + _recordFileDir;
        //            }
        //            thread.Start(ffmpegCmd);
        //        }
        //        else
        //        {
        //            return;
        //        }
        //    }
        //    if (_liveType == 1)
        //    {
        //        //直播桌面
        //        LiveForm.Visibility = Visibility.Visible;
        //        Rectangle screenArea = Rectangle.Empty;
        //        screenArea = Rectangle.Union(screenArea, Screen.PrimaryScreen.Bounds);
        //        var streamVideo = new ScreenCaptureStream(screenArea);
        //        //CameraPlayer.Width = 822;
        //        //CameraPlayer.Height = CommonHelper.GetHeight(screenArea.Width, screenArea.Height, 822);
        //        var index1 =
        //                       Math.Round(
        //                           (screenArea.Width * 1.0 / screenArea.Height * 1.0),
        //                           2);
        //        var index2 = Math.Round(
        //                (828 * 1.0 / 592 * 1.0),
        //                2);

        //        if (index1 > index2)
        //        {
        //            CameraPlayer.Width = 822;
        //            CameraPlayer.Height = CommonHelper.GetHeight(screenArea.Width, screenArea.Height, 822);
        //        }
        //        else
        //        {
        //            CameraPlayer.Width = CommonHelper.GetWidth(screenArea.Width, screenArea.Height, 592);
        //            CameraPlayer.Height = 592;
        //        }
        //        CameraPlayer.VideoSource = streamVideo;
        //        CameraPlayer.Start();
        //        if (LoadDesktop())
        //        {
        //            var screenWidth = CommonHelper.GetPrimaryScreenWidth();
        //            var screenHeight = CommonHelper.GetPrimaryScreenHeight();
        //            threadStart = new ParameterizedThreadStart(LiveDesktop);
        //            thread = new Thread(threadStart);
        //            if (count == 0)
        //            {
        //                //todo 发送桌面视频的时候，控制帧率
        //                //如果没有选择音频设备，则发送的时候，并不发送音频
        //                ffmpegCmd = "-f gdigrab -r " + _frameRate + " -video_size " + screenWidth + "x" + screenHeight + " -i desktop -vf scale=" + _desktopResolution + " -vcodec h264 -pix_fmt yuv420p -r " + _frameRate + " -f flv " + streamUrl + "/" + strStream;
        //                //ffmpegCmd = "-f gdigrab -rtbufsize 1700M -i desktop -vf scale=" + desktopResolution + " -vcodec h264 -r " + _frameRate + " -pix_fmt yuv420p -f flv " + streamUrl + "/" + strStream;
        //            }
        //            else if (count == 1)
        //            {
        //                ffmpegCmd = "-f gdigrab -r " + _frameRate + " -video_size " + screenWidth + "x" + screenHeight + " -i desktop " + strCmd1 + " -vf scale=" + _desktopResolution + " -vcodec h264 -r " + _frameRate + " -acodec aac -ac 2 -ar 44100 -ab 128k -b:v 1M -pix_fmt yuv420p -f flv " + streamUrl + "/" + strStream;
        //            }
        //            else if (count > 1)
        //            {
        //                ffmpegCmd = "-f gdigrab -r " + _frameRate + " -video_size " + screenWidth + "x" + screenHeight + " -i desktop " + strCmd1 + " -filter_complex \"" + strCmd2 + "amerge = inputs =" + (count) + "[aout]\"  -map \"[aout]\" -map 0 -vf scale=" + _desktopResolution + " -vcodec h264  -r " + _frameRate + " -acodec aac -ac 2 -ar 44100 -ab 128k -b:v 1M -pix_fmt yuv420p -f flv " + streamUrl + "/" + strStream;
        //            }
        //            if (IsRecord())
        //            {
        //                ffmpegCmd += " " + _recordFileDir;
        //            }
        //            thread.Start(ffmpegCmd);
        //        }
        //        else
        //        {
        //            return;
        //        }
        //    }
        //    if (_liveType == 2)
        //    {
        //        LiveForm.Visibility = Visibility.Visible;
        //        //直播桌面+摄像头
        //        //todo 直播桌面和摄像头的时候，帧率比较低，画面比较卡
        //        var screenWidth = CommonHelper.GetPrimaryScreenWidth();
        //        var screenHeight = CommonHelper.GetPrimaryScreenHeight();
        //        CameraPlayerHost.Visibility = Visibility.Visible;
        //        _stopRec = false;
        //        _videoFileName = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".flv";
        //        _videoFileFullPath = FFmpegHelper.VideoPath + _videoFileName;
        //        Thread threadView = new Thread(PreviewSecondCamera);
        //        threadView.IsBackground = true;
        //        threadView.Name = "PreviewCameraWhileLiving";
        //        threadView.Start();

        //        Rectangle screenArea = Rectangle.Empty;
        //        screenArea = Rectangle.Union(screenArea, Screen.PrimaryScreen.Bounds);
        //        var streamVideo = new ScreenCaptureStream(screenArea);
        //        //CameraPlayer.Width = 822;
        //        //CameraPlayer.Height = CommonHelper.GetHeight(screenArea.Width, screenArea.Height, 822);
        //        var index1 =
        //                       Math.Round(
        //                           (screenArea.Width * 1.0 / screenArea.Height * 1.0),
        //                           2);
        //        var index2 = Math.Round(
        //                (828 * 1.0 / 592 * 1.0),
        //                2);

        //        if (index1 > index2)
        //        {
        //            CameraPlayer.Width = 822;
        //            CameraPlayer.Height = CommonHelper.GetHeight(screenArea.Width, screenArea.Height, 822);
        //        }
        //        else
        //        {
        //            CameraPlayer.Width = CommonHelper.GetWidth(screenArea.Width, screenArea.Height, 592);
        //            CameraPlayer.Height = 592;
        //        }
        //        CameraPlayer.VideoSource = streamVideo;
        //        CameraPlayer.Start();
        //        if (LoadCamera() && LoadDesktop())
        //        {
        //            var cameraWidth = CommonHelper.GetWidth(cameraSize);
        //            var cameraHeight = CommonHelper.GetHeight(cameraSize);

        //            GetCamPosition();
        //            if (count == 0)
        //            {
        //                ffmpegCmd = "-f gdigrab -video_size " + screenWidth + "x" + screenHeight + " -i desktop " + strCmd1 + " -re -i \"" + _videoFileFullPath + "\" -filter_complex \"[1:v]scale=w=" + cameraWidth * 2 + ":h=" + cameraHeight * 2 + ":force_original_aspect_ratio=decrease[ckout];[0:v][ckout]overlay=x=" + _overWidth + ":y=" + _overHeight + "[out]\" -map \"[out]\" -vcodec h264  -r " + _frameRate + " -s " + _desktopResolution + " -f flv " + streamUrl + "/" + strStream;
        //            }
        //            else if (count == 1)
        //            {
        //                ffmpegCmd = "-f gdigrab -video_size " + screenWidth + "x" + screenHeight + " -i desktop " + strCmd1 + " -re -i \"" + _videoFileFullPath + "\" -filter_complex \"[2:v]scale=w=" + cameraWidth * 2 + ":h=" + cameraHeight * 2 + ":force_original_aspect_ratio=decrease[ckout];[0:v][ckout]overlay=x=" + _overWidth + ":y=" + _overHeight + "[out]\" -map \"[out]\" -map 1 -vcodec h264 -r " + _frameRate + " -movflags faststart -acodec aac -ar 44100 -ac 2 -ab 128k -b:v 1M  -s " + _desktopResolution + " -f flv " + streamUrl + "/" + strStream;
        //            }
        //            else if (count > 1)
        //            {
        //                ffmpegCmd = "-f gdigrab -video_size " + screenWidth + "x" + screenHeight + " -i desktop " + strCmd1 + " -re -i \"" + _videoFileFullPath + "\" -filter_complex \"[" + (count + 1) + ":v]scale=w=" + cameraWidth * 2 + ":h=" + cameraHeight * 2 + ":force_original_aspect_ratio=decrease[ckout];[0:v][ckout]overlay=x=" + _overWidth + ":y=" + _overHeight + "[out]\"" + " -filter_complex \"" + strCmd2 + "amerge = inputs =" + (count) + "[aout]\"" + " -map \"[out]\"   -map \"[aout]\" -vcodec h264 -r " + _frameRate + " -acodec aac -ac 2 -ar 44100 -ab 128k -b:v 1M -s " + _desktopResolution + " -f flv " + streamUrl + "/" + strStream;
        //            }
        //            if (this.IsRecord())
        //            {
        //                ffmpegCmd += " " + _recordFileDir;
        //            }
        //            thread.Start(ffmpegCmd);
        //        }
        //        else
        //        {
        //            return;
        //        }
        //    }
        //    LogHelper.AddFFmpegLog("ffmpeg命令行如下");
        //    LogHelper.AddFFmpegLog("ffmpeg " + ffmpegCmd);
        //    PreviewBtn.IsEnabled = false;
        //    PreviewBtn.Style = (Style)this.FindResource("EndButton");
        //    LiveBtn.IsEnabled = false;
        //    LiveBtn.Style = (Style)this.FindResource("EndButton");
        //    EndLiveBtn.IsEnabled = true;
        //    EndLiveBtn.Style = (Style)this.FindResource("EnterButton");
        //}


        /// <summary>
        /// 开启一个线程进行FFmpeg推流
        /// </summary>
        /// <param name="parameters"></param>
        private void Live(object parameters)
        {
            p = new Process();
            p.StartInfo.FileName = FFmpegHelper.FFmpegPath;
            p.StartInfo.Arguments = (string)parameters;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.ErrorDataReceived += new DataReceivedEventHandler(FFmpegOutput);
            p.Start();
            p.BeginErrorReadLine();
        }

        /// <summary>
        /// 预览桌面
        /// </summary>
        private void PreviewDesktop()
        {
            if (PreviewPlayer.IsRunning)
            {
                PreviewPlayer.SignalToStop();
                PreviewPlayer.WaitForStop();
            }
            Rectangle screenArea = Rectangle.Empty;
            screenArea = Rectangle.Union(screenArea, System.Windows.Forms.Screen.PrimaryScreen.Bounds);
            var streamVideo = new ScreenCaptureStream(screenArea);
            //当前屏幕的宽高比
            var index1 =Math.Round((screenArea.Width * 1.0 / screenArea.Height * 1.0),2);
            var width = Convert.ToInt32(Math.Round(CameraStackpanel.Width));
            var height = Convert.ToInt32(Math.Round(CameraStackpanel.Height));
            //预览播放器的宽高比
            var index2 = Math.Round( (width * 1.0 / height * 1.0), 2);

            if (index1 > index2)
            {
                PreviewPlayer.Width = width;
                PreviewPlayer.Height = CommonHelper.GetHeight(screenArea.Width, screenArea.Height, width);
            }
            else
            {
                PreviewPlayer.Width = CommonHelper.GetWidth(screenArea.Width, screenArea.Height, height);
                PreviewPlayer.Height = height;
            }
            PreviewPlayer.VideoSource = streamVideo;
            PreviewPlayer.Start();
        }

        /// <summary>
        /// 预览摄像头
        /// </summary>
        private void PreviewCamera()
        {
            if (PreviewPlayer.IsRunning)
            {
                PreviewPlayer.SignalToStop();
                PreviewPlayer.WaitForStop();
            }
            var camera = VideoHelper.GetCameraInfo(cameraName);
            if (camera!=null)
            {
                cameraResolutionIndex = VideoHelper.GetCameraResolutionIndex();
                camera.VideoResolution = camera.VideoCapabilities[cameraResolutionIndex];
                //当前摄像头分辨率的宽高比
                var index1 = Math.Round((camera.VideoResolution.FrameSize.Width * 1.0 / camera.VideoResolution.FrameSize.Height * 1.0), 2);
                var width = Convert.ToInt32(Math.Round(CameraStackpanel.Width));
                var height = Convert.ToInt32(Math.Round(CameraStackpanel.Height));
                //预览播放器的宽高比
                var index2 = Math.Round((width * 1.0 / height * 1.0), 2);

                if (index1 > index2)
                {
                    PreviewPlayer.Width = width;
                    PreviewPlayer.Height = CommonHelper.GetHeight(camera.VideoResolution.FrameSize.Width,
                        camera.VideoResolution.FrameSize.Height, width);
                }
                else
                {
                    PreviewPlayer.Width = CommonHelper.GetWidth(camera.VideoResolution.FrameSize.Width, camera.VideoResolution.FrameSize.Height, height);
                    PreviewPlayer.Height = height;
                }
                PreviewPlayer.VideoSource = camera;
                PreviewPlayer.Start();
            }
            else
            {
                MessageBox.Show("错误的摄像头设置","提示");
            }
        }

    }
}
