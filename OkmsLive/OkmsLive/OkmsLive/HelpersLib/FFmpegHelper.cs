using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OkmsLive.OkmsLiveTools
{
    public static class FFmpegHelper
    {

        public static readonly string FFmpegPath = Environment.Is64BitOperatingSystem ?
            Application.StartupPath + "\\ffmpeg64\\ffmpeg.exe"
            : Application.StartupPath + "\\ffmpeg32\\ffmpeg.exe";


        public static readonly string VideoPath = AppDomain.CurrentDomain.BaseDirectory + "VideoTemp\\"; //在直播摄像头的时候暂存的视频文件的路径
    }
}
