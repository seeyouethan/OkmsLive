using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using AForge.Video.DirectShow;
using OkmsLive.HelpersLib;
using OkmsLive.Models;

namespace OkmsLive.Forms
{
    /// <summary>
    /// SettingsWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private List<SimpleModel> cameraList = VideoHelper.GetAllCameras();
        public SettingsWindow()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Settings_Loaded);
        }
        private void Settings_Loaded(object sender, RoutedEventArgs e)
        {
            //tab1
            LoadTab0();
        }
        /// <summary>
        /// Tab页面切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabControlSetting_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                var index = TabControlSetting.SelectedIndex;
                if (index == 1)
                {
                    LoadTab0();
                }
                if (index == 2)
                {
                    LoadTab1();
                }
                if (index == 3)
                {
                    LoadTab2();
                }
                if (index == 5)
                {
                    LoadTab4();
                }
            }
            else
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// 直播参数设置Tab
        /// </summary>
        private void LoadTab0()
        {
            List<SimpleModel> deskCapabilityList = CommonHelper.GetDeskCapability();
            XDesktop.ItemsSource = deskCapabilityList;
            XDesktop.SelectedIndex = XmlHelper.GetIndexValue("DesktopResolution");
            FpsText.Text = string.IsNullOrEmpty(XmlHelper.GetValue("Fps")) ? "20" : XmlHelper.GetValue("Fps");
            //BoolDesktopVideo.SelectedIndex = XmlHelper.GetValue("DesktopAudio") == "0" ? 0 : 1;
        }
        /// <summary>
        /// 摄像头设置Tab
        /// </summary>
        private void LoadTab1()
        {
            cameraList = VideoHelper.GetAllCameras();
            if (cameraList.Count == 0)
            {
                return;
            }
            XCameraList.ItemsSource = cameraList;
            XCameraList.SelectedIndex = XmlHelper.GetIndexValue("Camera");
            
            List<SimpleModel> resolutionList = VideoHelper.GetCameraResolution(XCameraList.SelectedIndex);

            if (resolutionList == null || resolutionList.Count == 0)
            {
                MessageBox.Show("该摄像头没有支持的分辨率！", "系统提示");
                return;
            }
            XCamRes.ItemsSource = resolutionList;
            XCamRes.SelectedIndex = XmlHelper.GetIndexValue("CameraResolution");

            XCam.ItemsSource= VideoHelper.GetCameraSize(XCamRes.Text);
            XCam.SelectedIndex = XmlHelper.GetIndexValue("CameraSize");

            XCameraPosition.SelectedIndex = XmlHelper.GetIndexValue("CameraPosition");
            CameraSetting.IsEnabled = true;

        }
        /// <summary>
        /// 音频设置Tab
        /// </summary>
        private void LoadTab2()
        {
            GetMicrophoneDevice();
        }

        /// <summary>
        /// 录制设置
        /// </summary>
        private void LoadTab4()
        {
            var fileDir = XmlHelper.GetValue("RecordFileDir");
            recordFileDir.Text = fileDir;

            var index = XmlHelper.GetIndexValue("RecordFile");
            RecordBool.SelectedIndex = index;

        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {

        }

        #region tab1

        /// <summary>
        /// 选择直播桌面分辨率
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XDesktop_DropDownClosed(object sender, EventArgs e)
        {
            XmlHelper.SetValue("DesktopResolution", XDesktop.SelectedIndex, XDesktop.Text);
        }
        /// <summary>
        /// 保存帧率FPS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveBtn2_Click(object sender, RoutedEventArgs e)
        {
            //只能输入数字
            var fps = FpsText.Text;
            //var desktopAudio = BoolDesktopVideo.SelectedIndex;
            try
            {
                XmlHelper.SetValue("Fps", Convert.ToInt32(fps).ToString());
                //XmlHelper.SetValue("DesktopAudio", desktopAudio.ToString());
                MessageBox.Show("保存成功", "系统提示");
            }
            catch (Exception)
            {
                MessageBox.Show("Fps只能设置为整数！", "系统提示");
                FpsText.Text = string.IsNullOrEmpty(XmlHelper.GetValue("Fps")) ? "20" : XmlHelper.GetValue("Fps");
            }

        }
        #endregion

        #region tab2
        /// <summary>
        /// 刷新摄像头.重新读取摄像头列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshBtn_Click(object sender, RoutedEventArgs e)
        {
            cameraList = VideoHelper.GetAllCameras();
            if (cameraList.Count == 0)
            {
                XCameraList.ItemsSource = null;
                XCamRes.ItemsSource = null;
                XCam.ItemsSource = null;
                CameraSetting.IsEnabled = false;
                return;
            }
            XCameraList.ItemsSource = cameraList;
            XCameraList.SelectedIndex = 0;
            CameraSetting.IsEnabled = true;
            List<SimpleModel> resolutionList = VideoHelper.GetCameraResolution(0);

            if (resolutionList==null||resolutionList.Count == 0)
            {
                MessageBox.Show("读取摄像头出错，该摄像头没有支持的分辨率！", "系统提示");
                return;
            }
            XmlHelper.SetValue("Camera", XCameraList.SelectedIndex, XCameraList.Text);
            XCamRes.ItemsSource = resolutionList;
            XCamRes.SelectedIndex = 0;
            XmlHelper.SetValue("CameraResolution", XCamRes.SelectedIndex, XCamRes.Text);
            SetCamSize();
        }

        /// <summary>
        /// 选择摄像头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XCameraList_DropDownClosed(object sender, EventArgs e)
        {
            if (cameraList.Count == 0)
            {
                return;
            }
            XmlHelper.SetValue("Camera", XCameraList.SelectedIndex, XCameraList.Text); 

            List<SimpleModel> resolutionList = VideoHelper.GetCameraResolution(XCameraList.SelectedIndex);

            if (resolutionList == null || resolutionList.Count == 0)
            {
                MessageBox.Show("读取摄像头出错，该摄像头没有支持的分辨率！", "系统提示");
                return;
            }
            XCamRes.ItemsSource = resolutionList;
            XCamRes.SelectedIndex = 0;
            XmlHelper.SetValue("CameraResolution", XCamRes.SelectedIndex, XCamRes.Text);
            SetCamSize();
        }
        /// <summary>
        /// 选择摄像头分辨率
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XCamRes_DropDownClosed(object sender, EventArgs e)
        {
            if (cameraList.Count == 0)
            {
                return;
            }
            XmlHelper.SetValue("CameraResolution", XCamRes.SelectedIndex,XCamRes.Text);
            SetCamSize();
        }
        /// <summary>
        /// 同时直播桌面和摄像头的时候，摄像头的显示大小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XCam_DropDownClosed(object sender, EventArgs e)
        {
            if (cameraList.Count == 0)
            {
                return;
            }
            XmlHelper.SetValue("CameraSize", XCam.SelectedIndex, XCam.Text);
        }

        /// <summary>
        /// 设置摄像头大小
        /// </summary>
        private void SetCamSize()
        {
            try
            {
                var list = VideoHelper.GetCameraSize(XCamRes.Text);
                if (list.Count > 0)
                {
                    XCam.ItemsSource = list;
                    this.XCam.SelectedIndex = XmlHelper.GetIndexValue("CameraSize");
                    XmlHelper.SetValue("CameraSize", XCam.SelectedIndex, XCam.Text);
                }
                else
                {
                    MessageBox.Show("读取摄像头出错，无法设置摄像头大小!", "系统提示");
                    XCam.ItemsSource = null;
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误:没有找到视频设备！具体原因：" + ex.Message, "系统提示");
            }
        }
        /// <summary>
        /// 同时直播桌面和摄像头的时候，摄像头的位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XCameraPosition_DropDownClosed(object sender, EventArgs e)
        {
            XmlHelper.SetValue("CameraPosition", XCameraPosition.SelectedIndex, XCameraPosition.Text);
        }

        #endregion

        #region tab4
        /// <summary>
        /// 点击帮助文档打开网页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtUrl_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var strUrl = TxtUrl.Text;
            System.Diagnostics.Process.Start(strUrl);
        }
        #endregion

        #region tab3
        /// <summary>
        /// 获取本地的麦克风选项，并绑定到下拉列表中
        /// </summary>
        private void GetMicrophoneDevice()
        {/*
             * VCheckBox表示是否播放桌面音频
             */
            VCheckBox.IsChecked = XmlHelper.GetValue("DesktopAudio") == "1" ? true : false;
            List<SimpleModel> microphoneList = AudioHelper.GetMicrophoneDevices2();
            if (microphoneList.Count > 0)
            {
                MicrophonePanel.Children.Clear();

                var list = XmlHelper.GetValue("Microphone").Split(';');

                //在界面上显示出麦克风列表
                for (int i = 0; i < microphoneList.Count; i++)
                {
                    var microphone = list.Contains(i.ToString()) ? new Microphone(i, true) : new Microphone(i, false);
                    microphone._slider.Style = (Style)this.FindResource("SliderCustomStyle");
                    microphone._slider.Width = 180;
                    microphone._slider.Cursor = System.Windows.Input.Cursors.Hand;

                    microphone.SetText(microphoneList[i].Value);
                    MicrophonePanel.Children.Add(microphone);
                }
            }
            else
            {
                MessageBox.Show("未找到有效的麦克风设备", "系统提示");
            }
        }
        #endregion
        
        /// <summary>
        /// 设置摄像头参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CameraSetting_Click(object sender, RoutedEventArgs e)
        {
            FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            var cameraIndex = XCameraList.SelectedIndex;
            if (videoDevices.Count > 0)
            {
                var camera = new VideoCaptureDevice(videoDevices[cameraIndex].MonikerString);
                IntPtr hwnd = new WindowInteropHelper(this).Handle;
                camera.DisplayPropertyPage(hwnd);
            }
            else
            {
                MessageBox.Show("读取摄像头出错，未找到有效的摄像头！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnSaveFileDir_OnClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog { Description = "请选择要保存视频的目录" };
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string foldPath = dialog.SelectedPath;
                recordFileDir.Text = foldPath;
                //保存到xml中
                XmlHelper.SetValue("RecordFileDir",foldPath);
            }
        }

        private void RecordBool_DropDownClosed(object sender, EventArgs e)
        {
            XmlHelper.SetValue("RecordFile", RecordBool.SelectedIndex,RecordBool.Text);
        }

        /// <summary>
        /// 选中virtual-audio-capturer选项时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VCheckBox_OnChecked(object sender, RoutedEventArgs e)
        {
            XmlHelper.SetValue("DesktopAudio", "1");
        }

        /// <summary>
        /// 取消 virtual-audio-capturer 选项时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VCheckBox_OnUnchecked(object sender, RoutedEventArgs e)
        {
            XmlHelper.SetValue("DesktopAudio", "0");
        }
    }
}
