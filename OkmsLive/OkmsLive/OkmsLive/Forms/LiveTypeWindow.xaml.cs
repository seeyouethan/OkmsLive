﻿using OkmsLive.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OkmsLive.Forms
{
    /// <summary>
    /// LiveTypeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LiveTypeWindow : Window
    {
        public LiveTypeWindow()
        {
            InitializeComponent();
        }

        public MainWindow mainWindow = new MainWindow();

        private void LiveTypePanel1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mainWindow.SetLiveTypeImgAndText(LiveType.Window);
            this.Close();
        }
        private void LiveTypePanel2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mainWindow.SetLiveTypeImgAndText(LiveType.Software);
            this.Close();
        }
        private void LiveTypePanel3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mainWindow.SetLiveTypeImgAndText(LiveType.Desktop);
            this.Close();
        }

        private void StackPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            var item = (StackPanel)sender;
            item.Background = new SolidColorBrush(Color.FromRgb(219, 234, 245));
        }

        private void StackPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            var item = (StackPanel)sender;
            item.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }
    }
}