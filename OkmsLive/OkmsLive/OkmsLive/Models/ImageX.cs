using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using System.Windows.Media;
using AForge.Controls;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Video.FFMPEG;
using OkmsLive.Forms;
using OkmsLive.OkmsLiveTools;
using Color = System.Windows.Media.Color;

namespace OkmsLive.Model
{
    /// <summary>
    /// 导播台中显示图像的Model
    /// </summary>
    public class ImageX: StackPanel
    {
        private readonly Border _border = new Border();
        private readonly TextBlock _textBlock = new TextBlock();
        private readonly CheckBox _checkBox = new CheckBox();
        private readonly ComboBox _comboBox = new ComboBox();
        private readonly MultiScreen _model;
        private VideoX _videoX;
        private string _resolution;

        private readonly int _index;

        private readonly WindowsFormsHost _windowsFormsHost=new WindowsFormsHost();
        private readonly WindowsFormsHost _comboxHost = new WindowsFormsHost();
        private readonly ElementHost _elementComboxHost = new ElementHost();
        private readonly WindowsFormsHost _textNameHost = new WindowsFormsHost();
        private readonly ElementHost _elementTextNameHost = new ElementHost();

        private readonly VideoSourcePlayer _videoSourcePlayer=new VideoSourcePlayer();
        



        public ImageX()
        {

        }
        public ImageX(MultiScreen model, int index, string text, VideoX videoX)
        {
            _model = model;
            _index = index;
            _videoX = videoX;

            _border.BorderThickness = new Thickness(1);
            _border.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            _border.Width = 522;
            _border.Height = 210;
            _border.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            _border.Margin = new Thickness(0, -210, 0, 0);

            _textBlock.Text = text;
            _textBlock.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            _textBlock.Margin = new Thickness(6, -208, 0, 0);

            _checkBox.Content = "选中";
            _checkBox.Margin = new Thickness(6, -20, 0, 0);
            _checkBox.Checked += _checkBox_Checked;
            _checkBox.Unchecked += _checkBox_Unchecked;

            _comboBox.Margin = new Thickness(0, -30, 1, 2);
            _comboBox.Width = 110;
            _comboBox.HorizontalAlignment = HorizontalAlignment.Right;
            _comboBox.DisplayMemberPath = "Value";
            _comboBox.DropDownClosed += _comboBox_DropDownClosed;
            _comboBox.VerticalContentAlignment=VerticalAlignment.Center;

            _windowsFormsHost.Child = _videoSourcePlayer;
            this.Children.Add(_windowsFormsHost);

            _comboxHost.Width = 100;
            _comboxHost.Height = 21;
            _comboxHost.Margin=new Thickness(420,-21,0,0);
            _comboxHost.Child = _elementComboxHost;
            _elementComboxHost.Child = _comboBox;
            this.Children.Add(_comboxHost);

            //_textNameHost.Margin = new Thickness(5, -395, 0, 0);
            //_textNameHost.Height = 21;
            //_textNameHost.Child = _elementTextNameHost;
            //_elementTextNameHost.Child = _textBlock;
            ////_textNameHost.Background=new SolidColorBrush(Color.FromArgb(0,0,0,0));
            ////_textNameHost.Opacity = 0;
            //_elementTextNameHost.BackColor = System.Drawing.Color.FromArgb(0, 0, 0, 0);
            //this.Children.Add(_textNameHost);


            this.Children.Add(_border);
            this.Children.Add(_textBlock);
            this.Children.Add(_checkBox);
            //this.Children.Add(_comboBox);
        }
        public void SetVideoResource(IVideoSource videoSource)
        {
            if (videoSource is ScreenCaptureStream)
            {
                var source = videoSource as ScreenCaptureStream;
                _windowsFormsHost.Width = CommonHelper.GetWidth(source.Region.Width, source.Region.Height,210); ;
                _windowsFormsHost.Height = 210;
                _videoSourcePlayer.VideoSource = videoSource;
                _videoSourcePlayer.Start();
            }else if (videoSource is VideoCaptureDevice)
            {
                var source = videoSource as VideoCaptureDevice;
                source.VideoResolution = source.VideoCapabilities[0];
                _windowsFormsHost.Width = CommonHelper.GetWidth(source.VideoResolution.FrameSize.Width, source.VideoResolution.FrameSize.Height, 210); 
                _windowsFormsHost.Height = 210;
                _videoSourcePlayer.VideoSource = videoSource;
                _videoSourcePlayer.Start();
            }
        }

        public void StartVideoResource()
        {
            _videoSourcePlayer.Start();
        }

        public void StopVideoResource()
        {
            if (_videoSourcePlayer.IsRunning)
            {
                _videoSourcePlayer.SignalToStop();
                _videoSourcePlayer.WaitForStop();
            }
        }
        /// <summary>
        /// 选中直播时的分辨率
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _comboBox_DropDownClosed(object sender, EventArgs e)
        {
            this._resolution = _comboBox.Text;
            var width = CommonHelper.GetWidth(_resolution);
            var height = CommonHelper.GetHeight(_resolution);
            _windowsFormsHost.Width = CommonHelper.GetWidth(width, height, 210);
        }
        

        private void _checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            //取消勾选
            if (_model.isLive)
            {
                if (Equals(_model.CurrentImageX, this))
                {
                    _checkBox.IsChecked = true;
                    return;
                }
            }
            _model.CurrentImageX = null;
        }

        /// <summary>
        /// 勾选视频上的CheckBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _checkBox_Checked(object sender, RoutedEventArgs e)
        {
            /*
             * 若当前正在直播，则点击切换的时候，若点击的自己，则无法进行取消，若点击其他的则切换直播进程
             * 
             * 若当前并没有在直播，则点击切换的时候，只是和其他CheckBox进行互斥
             * */
            if (_model.isLive)
            {
                if (Equals(_model.CurrentImageX, this))
                {
                    return;
                }
                else
                {
                    //修改名称
                    _model.EndLive();
                    _model.CurrentImageX._textBlock.Text=_model.CurrentImageX._textBlock.Text.Replace("   直播中...", "");
                    //ParameterizedThreadStart threadStart = CaptureStartX;
                    //Thread thread = new Thread(threadStart);
                    //thread.Start(_model.CurrentImageX);
                    _model.CurrentImageX.StopRecord();
                    
                    _model.CurrentImageX = this;
                    var list = _model._imagexList;
                    foreach (var imageX in list)
                    {
                        if (imageX.Key != _index)
                        {
                            imageX.Value._checkBox.IsChecked = false;
                        }
                    }
                    _model.CurrentImageX = this;
                    _model.StartLive();
                }
            }
            else
            {
                var list = _model._imagexList;
                foreach (var imageX in list)
                {
                    if (imageX.Key != _index)
                    {
                        imageX.Value._checkBox.IsChecked = false;
                    }
                }
                _model.CurrentImageX = this;
            }
        }
        
        /// <summary>
        /// 取消直播  转换为预览
        /// </summary>
        /// <param name="model"></param>
        private void CaptureStartX(object model)
        {
            //Thread.Sleep(1000);
            this.Dispatcher.Invoke(new Action(delegate
            {
                var modelx = model as ImageX;
                if (modelx != null)
                {
                    modelx.SetText(modelx.GetText().Replace("   直播中...", ""));
                    //modelx.StartVideoResource();
                }
            }));
        }


        public void SetText(string text)
        {
            _textBlock.Text = text;
        }
        public string GetText()
        {
            return _textBlock.Text;
        }

        public string GetResolution()
        {
            return this._resolution;
        }

        public void SetResolution(List<SimpleModel> resolutionList)
        {
            //this._resolutionList = resolutionList;
            this._comboBox.ItemsSource = resolutionList;
            _comboBox.SelectedIndex = 0;
            this._resolution = _comboBox.Text;
        }

        private bool _stopREC = true;
        private bool _createNewFile = true;
        private VideoFileWriter _videoWriter = null;
        private string _videoFileName = string.Empty;  //视频文件名
        private string _videoFileFullPath = string.Empty;  //视频文件全路径
        public int _frameRate = 30;

        public void StartRecord()
        {
            _stopREC = false;
        }

        public void StopRecord()
        {
            _stopREC = true;
        }

        /// <summary>
        /// 录制视频的文件是否已经创建完成
        /// </summary>
        /// <returns></returns>
        public bool IsHaveCreateNewFile()
        {
            return _createNewFile;
        }
        /// <summary>
        /// 录制视频
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        public void Camera_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap img = (Bitmap)eventArgs.Frame.Clone();
            if (_stopREC)
            {
                _stopREC = true;
                _createNewFile = true;  //这里要设置为true表示要创建新文件
                if (_videoWriter != null)
                {
                    _videoWriter.Close();
                }
            }
            else
            {
                //开始录像
                if (_createNewFile)
                {
                    if (_videoWriter != null)
                    {
                        _videoWriter.Close();
                        _videoWriter.Dispose();
                    }
                    _videoWriter = new VideoFileWriter();
                    //这里必须是全路径，否则会默认保存到程序运行根据录下了
                    _videoWriter.Open(_videoFileFullPath, img.Width, img.Height, _frameRate, VideoCodec.FLV1);
                    _createNewFile = false;
                }
                _videoWriter.WriteVideoFrame(img);
            }
        }


        public void setFrameRate(int averageFrameRate)
        {
            _frameRate = averageFrameRate;
        }

        public void SetVideoPath(string videoFileFullPath)
        {
            _videoFileFullPath = videoFileFullPath;
        }
    }
}
