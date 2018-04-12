using System.ComponentModel;
namespace OkmsLive.Enums
{

    public enum LiveType
    {

        [Description("桌面")]
        Desktop = 1,
        [Description("窗口")]
        Window = 2,
        [Description("软件")]
        Software = 3,
        [Description("摄像头")]
        Camera = 4
    }
}
