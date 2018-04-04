using System.Collections.Generic;
using System.Linq;
using AForge.Video.DirectShow;
using NAudio.CoreAudioApi;
using OkmsLive.Models;

namespace OkmsLive.HelpersLib
{
    public static class AudioHelper
    {
        /// <summary>
        /// 获取所有麦克风设备(音频输入设备)
        /// </summary>
        /// <returns></returns>
        public static List<SimpleModel> GetMicrophoneDevices()
        {
            var enumerator = new MMDeviceEnumerator();
            var captureDevices = enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active).ToArray();

            var microphoneList = new List<SimpleModel>();
            if (captureDevices.Length > 0)
            {
                for (int i = 0; i < captureDevices.Length; i++)
                {
                    SimpleModel microphone = new SimpleModel
                    {
                        Id = i + 1,
                        Value = captureDevices[i].FriendlyName
                    };
                    microphoneList.Add(microphone);
                }
            }
            return microphoneList;
        }


        public static List<SimpleModel> GetMicrophoneDevices2()
        {
            FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.AudioInputDevice);

            //var enumerator = new MMDeviceEnumerator();
            //var captureDevices = enumerator.EnumerateAudioEndPoints(DataFlow.Capture, NAudio.CoreAudioApi.DeviceState.Active).ToArray();



            var microphoneList = new List<SimpleModel>();
            if (videoDevices.Count > 0)
            {
                for (int i = 0; i < videoDevices.Count; i++)
                {

                    if (videoDevices[i].Name != "virtual-audio-capturer")
                    {
                        SimpleModel microphone = new SimpleModel
                        {
                            Id = i + 1,
                            Value = videoDevices[i].Name
                        };
                        microphoneList.Add(microphone);
                    }
                }
            }
            return microphoneList;
        }
    }
}
