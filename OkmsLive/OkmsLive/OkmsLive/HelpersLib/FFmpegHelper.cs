using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OkmsLive.HelpersLib
{
    public static class FFmpegHelper
    {

        public static readonly string FFmpegPath = Application.StartupPath + "\\ffmpeg.exe";


        public static readonly string VideoPath = AppDomain.CurrentDomain.BaseDirectory + "VideoTemp\\"; //在直播摄像头的时候暂存的视频文件的路径
    }
}
