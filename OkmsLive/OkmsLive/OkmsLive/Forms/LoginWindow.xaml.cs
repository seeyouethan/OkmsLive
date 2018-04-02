using OkmsLive.HelpersLib;
using System.Windows;

namespace OkmsLive.Forms
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {

        private bool isMd5Pwd = false;
        public LoginWindow()
        {
            InitializeComponent();
            UserName.Focus();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            //登录按钮

            var username = UserName.Text.Trim();
            var password = PasswordText.Password;

            if (string.IsNullOrEmpty(username) || username == "账  号")
            {
                MessageBox.Show("请输入用户名!", "登陆失败", MessageBoxButton.OK, MessageBoxImage.Information);
                UserName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("请输入密码!", "登陆失败", MessageBoxButton.OK, MessageBoxImage.Information);
                PasswordText.Focus();
                return;
            }
            this.UserLoading.Visibility = Visibility.Visible;
            LoginBtn.IsEnabled = false;
            UserName.IsEnabled = false;
            PasswordText.IsEnabled = false;
            var enPassword = password;
            if (!isMd5Pwd)
            {
                enPassword = SecureHelper.MD5(password);
            }
            string resp;
            var dlg = new MainWindow();
            dlg.Show();
            this.Close();


            /*
            //发送请求 发送一个异步请求
            ThreadPool.QueueUserWorkItem((object state) =>
            {
                //模拟一下等待
                Thread.Sleep(100);
                try
                {
                    //从配置文件中取web地址
                    var serverUrl = XmlHelper.GetValue("ServerUrl");
                    if (string.IsNullOrEmpty(serverUrl))
                    {
                        //默认为内网测试地址
                        serverUrl = "http://192.168.105.80:8082";
                    }
                    //接口的地址为 web地址+"/api/tealivelogin"
                    serverUrl = serverUrl + "/api/tealivelogin";

                    LogHelper.AddEventLog("登录时发送的Http请求为:" + serverUrl);
                    resp = HttpWebHelper.HttpPost(serverUrl,
                        "u=" + username + "&p=" + enPassword);
                    LogHelper.AddEventLog("登录时发送的Http请求返回为：" + resp);
                    Dispatcher.Invoke(new Action(delegate
                    {
                        this.UserLoading.Visibility = Visibility.Collapsed;
                        LoginBtn.IsEnabled = true;
                        UserName.IsEnabled = true;
                        PasswordText.IsEnabled = true;
                    }));

                }
                catch (Exception ex)
                {
                    LogHelper.AddErrorLog(ex.ToString());
                    MessageBox.Show("访问服务器失败，请检查网络连接!", "登陆失败", MessageBoxButton.OK, MessageBoxImage.Information);

                    Dispatcher.Invoke(new Action(delegate
                    {
                        this.UserLoading.Visibility = Visibility.Collapsed;
                        LoginBtn.IsEnabled = true;
                        UserName.IsEnabled = true;
                        PasswordText.IsEnabled = true;
                    }));
                    return;
                }
                var list = JsonHandleHelper.DataRowFromJSON(resp);
                var dataJson = JsonHandleHelper.ObjectToJson(list["Data"]);
                var courses = JsonHandleHelper.JSONToObject<List<Course>>(dataJson);
                //判断是否登录成功
                if (list["R"].ToString() == "1")
                {
                    var rtmpserverUrl = list["FMS_Url"];
                    var trueName = list["TrueName"];
                    var headPhoto = list["HeadPhoto"].ToString();
                    //登录成功

                    Dispatcher.Invoke(new Action(delegate
                    {
                        if (RememberCheckBox.IsChecked == true && !isMd5Pwd)
                        {
                            //记录用户名和密码
                            var enPwd = Base64Helper.Base64Encode(password);
                            XmlHelper.StoreUsers(username, enPwd);
                        }
                    }));

                    //登陆成功后删除上传的直播的课程的VideoTemp
                    ThreadPool.QueueUserWorkItem(o =>
                    {
                        FileHelper.ClearFolder(FFmpegHelper.VideoPath);
                    });
                    //打开直播界面
                    Dispatcher.Invoke(new Action(delegate
                    {
                        var dialog = new SecondWindow();
                        if (courses != null && courses.Count > 0)
                        {
                            dialog.SetCourseList(courses);
                            dialog.SetRtmpServerUrl(rtmpserverUrl.ToString());
                            dialog.SetTrueName(string.IsNullOrEmpty(trueName?.ToString())
                                ? username
                                : trueName.ToString());
                            dialog.SetUserName(username);
                            dialog.SetUserImg(headPhoto);
                        }
                        dialog.Show();
                        this.Close();
                    }));
                }
                else
                {
                    //登录失败
                    if (string.IsNullOrEmpty(list["M"].ToString()))
                    {
                        MessageBox.Show("用户名和密码错误，请重新输入!", "登陆失败", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show(list["M"].ToString(), "登陆失败", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }, null);
            */
        }

       
    }
}
