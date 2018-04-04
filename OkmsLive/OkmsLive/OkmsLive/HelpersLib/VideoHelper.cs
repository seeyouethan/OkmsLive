using System;
using System.Collections.Generic;
using AForge.Video.DirectShow;
using OkmsLive.Models;

namespace OkmsLive.HelpersLib
{
    public static class VideoHelper
    {

        public static List<SimpleModel> GetAllCameras()
        {
            List<SimpleModel> cameraList = new List<SimpleModel>();
            FilterInfoCollection cameras = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            for (var i = 0; i < cameras.Count; i++)
            {
                if (cameras[i].Name != "screen-capture-recorder")
                {
                    var camera = new SimpleModel
                    {
                        Id = i,
                        Value = cameras[i].Name
                    };
                    cameraList.Add(camera);
                }
            }
            return cameraList;
        }

        public static List<SimpleModel> GetCameraResolution(int index)
        {
            if (index < 0)
            {
                return null;
            }
            List<SimpleModel> resolutionList = new List<SimpleModel>();
            FilterInfoCollection cameras = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (cameras.Count <= 0)
            {
                return null;
            }
            var camera = new VideoCaptureDevice(cameras[index].MonikerString);

            for (int j = 0; j < camera.VideoCapabilities.Length; j++)
            {
                var height = camera.VideoCapabilities[j].FrameSize.Height;
                var width = camera.VideoCapabilities[j].FrameSize.Width;

                var model = new SimpleModel
                {
                    Id = j,
                    Value = width + "x" + height
                };
                resolutionList.Add(model);
            }
            return resolutionList;
        }


        /// <summary>
        /// 根据分辨率 生成 摄像头的大小 宽度固定为320 240 160 120三种
        /// </summary>
        /// <param name="index"></param>
        /// <param name="widthandheight"></param>
        /// <returns></returns>
        public static List<SimpleModel> GetCameraSize(string widthandheight)
        {
            if (string.IsNullOrEmpty(widthandheight))
                return null;
            List<SimpleModel> cameraSizeList = new List<SimpleModel>();
            //320 240 160 120
                double x = Math.Round((double)CommonHelper.GetWidth(widthandheight)/ (double)CommonHelper.GetHeight(widthandheight), 4);
                cameraSizeList.Add(new SimpleModel() { Id = 0, Value = 320 + "x" + Convert.ToInt32(320 / x) });
                cameraSizeList.Add(new SimpleModel() { Id = 1, Value = 240 + "x" + Convert.ToInt32(240 / x) });
                cameraSizeList.Add(new SimpleModel() { Id = 2, Value = 160 + "x" + Convert.ToInt32(160 / x) });
                cameraSizeList.Add(new SimpleModel() { Id = 3, Value = 120 + "x" + Convert.ToInt32(120 / x) });
            return cameraSizeList;
        }

        /// <summary>
        /// 获取选中的摄像头的名称
        /// </summary>
        /// <returns></returns>
        public static string GetCameraName()
        {
            return XmlHelper.GetValue("Camera");
        }

        public static int GetCameraIndex()
        {
            return XmlHelper.GetIndexValue("Camera");
        }

        /// <summary>
        /// 获取选中的摄像头的分辨率
        /// </summary>
        /// <returns></returns>
        public static string GetCameraResolution()
        {
            return XmlHelper.GetValue("CameraResolution");
        }
        /// <summary>
        /// 获取设置的摄像头大小
        /// </summary>
        /// <returns></returns>
        internal static string GetCameraSize()
        {
            return XmlHelper.GetValue("CameraSize"); 
        }
        /// <summary>
        /// 获取摄像头的相对位置
        /// </summary>
        /// <returns></returns>
        public static string GetCameraPosition()
        {
            return XmlHelper.GetValue("CameraPosition");
        }
        /// <summary>
        /// 获取分辨率大小的Index
        /// </summary>
        /// <returns></returns>
        public static int GetCameraResolutionIndex()
        {
            return XmlHelper.GetIndexValue("CameraResolution");
        }

        public static string GetFps()
        {
            return XmlHelper.GetValue("Fps");
        }
        /// <summary>
        /// 获取所有摄像头设备（包括桌面录屏在内的）
        /// </summary>
        /// <returns></returns>
        public static List<SimpleModel> GetDeskAndAllVideoDevices()
        {
            List<SimpleModel> cameraList = new List<SimpleModel>();
            cameraList.Add(
                new SimpleModel
                {
                    Id = 0,
                    Value = "桌面"
                });

            FilterInfoCollection cameras = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            for (int i = 0; i < cameras.Count; i++)
            {
                var camera = new SimpleModel
                {
                    Id = i + 1,
                    Value = cameras[i].Name
                };
                cameraList.Add(camera);
            }
            return cameraList;
        }
        /// <summary>
        /// 设置默认的摄像头
        /// </summary>
        public static void SetDefaultCamera()
        {
            var cameraList = VideoHelper.GetAllCameras();
            if (cameraList.Count != 0)
            {
                XmlHelper.SetValue("Camera", 0, cameraList[0].Value);
                List<SimpleModel> resolutionList = VideoHelper.GetCameraResolution(0);
                if (resolutionList != null && resolutionList.Count != 0)
                {
                    XmlHelper.SetValue("CameraResolution", 0, resolutionList[0].Value);
                    var listCameraSize = VideoHelper.GetCameraSize(resolutionList[0].Value);
                    if (listCameraSize != null && listCameraSize.Count != 0)
                    {
                        XmlHelper.SetValue("CameraSize", 0, listCameraSize[0].Value);
                    }
                }
            }
        }
    }
}
