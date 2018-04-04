using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OkmsLive.OkmsLiveTools
{
    public static class LiveHelper
    {
        #region 模拟控制台信号需要使用的api
        [DllImport("kernel32.dll")]
        static extern bool GenerateConsoleCtrlEvent(int dwCtrlEvent, int dwProcessGroupId);

        [DllImport("kernel32.dll")]
        static extern bool SetConsoleCtrlHandler(IntPtr handlerRoutine, bool add);

        [DllImport("kernel32.dll")]
        static extern bool AttachConsole(int dwProcessId);

        [DllImport("kernel32.dll")]
        static extern bool FreeConsole();
        #endregion


        // ffmpeg进程
        static Process p = new Process();

        // ffmpeg.exe实体文件路径
        static string ffmpegPath = FFmpegHelper.FFmpegPath;

        public static void Start(string audioDevice, string outFilePath)
        {
            if (File.Exists(outFilePath))
            {
                File.Delete(outFilePath);
            }

            /*转码，视频录制设备：gdigrab；录制对象：桌面；
             * 音频录制方式：dshow；
             * 视频编码格式：h.264；*/
            ProcessStartInfo startInfo = new ProcessStartInfo(ffmpegPath);
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            startInfo.Arguments = "-f gdigrab -framerate 15 -i desktop -f dshow -i audio=\"" + audioDevice + "\" -vcodec libx264 -preset:v ultrafast -tune:v zerolatency -acodec libmp3lame \"" + outFilePath + "\"";

            p.StartInfo = startInfo;

            p.Start();
        }

        /// <summary>
        /// 功能: 停止录制
        /// </summary>
        public static void Stop()
        {
            AttachConsole(p.Id);
            SetConsoleCtrlHandler(IntPtr.Zero, true);
            GenerateConsoleCtrlEvent(0, 0);
            FreeConsole();
        }

    }
}
