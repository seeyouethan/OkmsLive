using OkmsLive.Forms;
using OkmsLive.HelpersLib;
using System;
using System.Windows;
using System.Windows.Threading;

namespace OkmsLive
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            log4net.Config.XmlConfigurator.Configure();
            this.Startup += new StartupEventHandler(App_Startup);
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var dlg = new ChooseCourseWindow();
            dlg.ShowDialog();

            //if (e.Args.Length != 0)
            //{
            //    var str = e.Args[0];
            //    LogHelper.AddEventLog("从Web页面启动应用程序，传入的参数如下：" + str);
            //    str = str.Replace("znyktlive://", "").Replace("ZnyktLive://", "");
            //    var index1 = str.IndexOf("&");
            //    var userName = str.Substring(0, index1);
            //    var pwd = str.Substring(index1 + 1, str.Length - index1 - 2);//除去一个‘&’和最后的一个‘/’
            //    LogHelper.AddEventLog("UserName:" + userName);
            //    LogHelper.AddEventLog("Password" + pwd);
            //    Login diaLogin = new Login(userName, pwd);
            //    diaLogin.ShowDialog();
            //}
            //else
            //{
            //    Login diaLogin = new Login();
            //    diaLogin.ShowDialog();
            //}
        }


        private void App_Startup(object sender, StartupEventArgs e)
        {


            Application app = Current;
            app.ShutdownMode = ShutdownMode.OnLastWindowClose;
            //捕获未处理的异常  UI线程中的异常未处理\非UI线程抛出的未处理异常
            Current.DispatcherUnhandledException += App_OnDispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            //一次只允许打开一个窗口
            new System.Threading.Mutex(true, "OkmsLive", out bool ret);

            if (!ret)
            {
                MessageBox.Show("已经有一个直播助手在运行！", "系统提示");
                Environment.Exit(0);
            }
        }
        /// <summary>
        /// UI线程抛出全局异常事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            try
            {
                LogHelper.AddErrorLog(e.Exception.ToString());
                MessageBox.Show("很抱歉，当前应用程序遇到一些问题，该操作已经终止，请进行重试，如果问题继续存在，请联系开发人员.", "意外的操作", MessageBoxButton.OK, MessageBoxImage.Information);
                Environment.Exit(0);
                //e.Handled = true;
            }
            catch (Exception ex)
            {
                LogHelper.AddErrorLog(ex.ToString());
                MessageBox.Show("应用程序发生不可恢复的异常，将要退出！", "意外的操作", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// 非UI线程抛出全局异常事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                var exception = e.ExceptionObject as Exception;
                if (exception != null)
                {
                    LogHelper.AddErrorLog(exception.ToString());
                }
            }
            catch (Exception ex)
            {
                LogHelper.AddErrorLog(ex.ToString());
                MessageBox.Show("应用程序发生不可恢复的异常，将要退出！", "意外的操作", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }

}
