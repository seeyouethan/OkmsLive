using OkmsLive.HelpersLib;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace OkmsLive.Models
{
    public class Microphone : StackPanel
    {
        private readonly TextBlock _textBlock = new TextBlock();
        private readonly CheckBox _checkBox = new CheckBox();
        private readonly ProgressBar _progressBar = new ProgressBar();
        public readonly Slider _slider = new Slider();
        private double _ratio0;
        private WasapiCaptureViewModel _viewModel;
        private readonly int _index;
        private bool _isOpen;


        private Window _model;
        

        public Microphone(int index, bool isOpen)
        {
            _index = index;
            _isOpen = isOpen;
            this.Margin = new Thickness(10, 10, 0, 0);
            this.Orientation = Orientation.Horizontal;
            _checkBox.Margin = new Thickness(8, 2, 0, 0);
            _textBlock.MinWidth = 210;
            _textBlock.Margin = new Thickness(1, 8, 0, 0);
            this.Children.Add(_textBlock);
            this.Children.Add(_checkBox);
            _slider.Width = 200;
            _slider.Margin = new Thickness(8, 1, 0, 0);
            _slider.Maximum = 100;
            _slider.Value = XmlHelper.GetValue("Volume", _index.ToString()) * 100.0;
            this.Children.Add(_slider);
            _progressBar.Width = 200;
            _progressBar.Height = 12;
            _progressBar.Margin = new Thickness(20, 1, 0, 0);
            _progressBar.Maximum = 100;
            this.Children.Add(_progressBar);

            //_slider.Style = "{StaticResource SliderCustomStyle}";

            if (_isOpen)
            {
                _ratio0 = 100.0;
                _viewModel = new WasapiCaptureViewModel(_index);
                _progressBar.SetBinding(ProgressBar.ValueProperty, new Binding("Peak") { Source = _viewModel });
                _viewModel.RecordLevel = XmlHelper.GetValue("Volume", _index.ToString());
                _checkBox.IsChecked = true;
            }


            _checkBox.Checked += _checkBox_Checked;
            _checkBox.Unchecked += _checkBox_Unchecked;
            _slider.ValueChanged += _slider_ValueChanged;
        }

        public void SetModel(Window model)
        {
            _model = model;
        }

        private void _checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            //取消勾选
            _viewModel?.Dispose();
            _progressBar.Value = 0;
            var str = XmlHelper.GetValue("Microphone");
            var replace = str.Replace(_index.ToString(), "");
            XmlHelper.SetValue("Microphone", 0, replace);
            _isOpen = false;
            //_model?.MicrophoneList.Remove(this);
        }

        private void _slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _ratio0 = _slider.Value;
            if (_viewModel != null)
            {
                _viewModel.Ratio = _ratio0;
                _viewModel.RecordLevel = Convert.ToSingle(_ratio0 / 100);
                XmlHelper.SetValue("Volume", _index, Convert.ToSingle(_ratio0 / 100).ToString());
            }
        }

        private void _checkBox_Checked(object sender, RoutedEventArgs e)
        {
            //选中
            var volume = XmlHelper.GetValue("Volume", _index.ToString());
            _slider.Value = volume * 100;
            _ratio0 = 100.0;
            _viewModel = new WasapiCaptureViewModel(_index);
            _viewModel.RecordLevel = volume;
            _progressBar.SetBinding(ProgressBar.ValueProperty, new Binding("Peak") { Source = _viewModel });

            //修改xml
            var str = XmlHelper.GetValue("Microphone");
            if (string.IsNullOrEmpty(str))
            {
                XmlHelper.SetValue("Microphone", 0, _index.ToString());
            }
            else
            {
                XmlHelper.SetValue("Microphone", 0, str + ";" + _index);
            }
            _isOpen = true;

            //_model?.MicrophoneList.Add(this);
        }


        public void SetText(string text)
        {
            _textBlock.Text = text;
        }

        public string GetText()
        {
            return _textBlock.Text;
        }

        public void SetPanelMarginTop(int index)
        {
            this.Margin = new Thickness(10, index, 0, 0);
        }

    }
}
