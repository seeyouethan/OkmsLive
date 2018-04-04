using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using AForge.Controls;
using AForge.Video;
using AForge.Video.DirectShow;
using OkmsLive.Forms;
using OkmsLive.OkmsLiveTools;
using CheckBox = System.Windows.Controls.CheckBox;
using Orientation = System.Windows.Controls.Orientation;

namespace OkmsLive.Model
{
    public class VideoX: StackPanel
    {
        /// <summary>
        /// 名称
        /// </summary>
        private readonly TextBlock _textBlock = new TextBlock();
        /// <summary>
        /// 是否选中
        /// </summary>
        private readonly CheckBox _checkBox = new CheckBox();
        /// <summary>
        /// 序号
        /// </summary>
        private readonly int _index;
        /// <summary>
        /// 所属主窗体
        /// </summary>
        private readonly MultiScreen _model;
        /// <summary>
        /// 对应的转播台中的播放器的容器
        /// </summary>
        private ImageX _image;

        public VideoX()
        {

        }
        public VideoX(MultiScreen model, int index, string text)
        {
            _model = model;
            _index = index;
            this.Margin = new Thickness(2, 2, 0, 0);
            this.Orientation = Orientation.Horizontal;
            this.Width = 200;
            _textBlock.Text = text; _textBlock.Margin = new Thickness(2, 0, 0, 0);
            this.Children.Add(_checkBox);
            this.Children.Add(_textBlock);
            _checkBox.Checked += _checkBox_Checked;
            _checkBox.Unchecked += _checkBox_Unchecked;
        }
        /// <summary>
        /// 勾选框勾选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _checkBox_Checked(object sender, RoutedEventArgs e)
        {
            //勾选
            //如果是桌面，则特殊处理
            _image = new ImageX(_model, _index, _textBlock.Text, this);
            _model._imagexList.Add(_index, _image);
            _model.ImagePanel.Children.Add(_image);

            if (_index == 0)
            {
                List<SimpleModel> deskCapabilityList = CommonHelper.GetDeskCapability();
                _image.SetResolution(deskCapabilityList);
                //开始显示桌面
                Rectangle screenArea = Rectangle.Empty;
                screenArea = Rectangle.Union(screenArea, Screen.PrimaryScreen.Bounds);

                var streamVideo = new ScreenCaptureStream(screenArea);
                _image.SetVideoResource(streamVideo);
            }
            else
            {
                //index-1 是因为要去掉【桌面】这个序号
                _image.SetResolution(VideoHelper.GetCameraResolution(_index - 1));
                //开始显示摄像头
                FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                if (videoDevices.Count > 0)
                {
                    var camera = new VideoCaptureDevice(videoDevices[_index - 1].MonikerString);
                    camera.VideoResolution = camera.VideoCapabilities[_index - 1];
                    camera.NewFrame += _image.Camera_NewFrame;
                    _image.setFrameRate(camera.VideoCapabilities[_index - 1].AverageFrameRate);
                    _image.SetVideoResource(camera);
                }
            }
        }

        /// <summary>
        /// 勾选框取消勾选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            //取消勾选
            _model._imagexList.Remove(_index); ;
            //_capturer?.Stop();
            if (_image != null)
            {
                _image.StopVideoResource();
                _model.ImagePanel.Children.Remove(_image);
            }
        }

        public void DisableCheckBox()
        {
            _checkBox.IsEnabled = false;
        }

        public void EnableCheckBox()
        {
            _checkBox.IsEnabled = true;
        }
    }
}
