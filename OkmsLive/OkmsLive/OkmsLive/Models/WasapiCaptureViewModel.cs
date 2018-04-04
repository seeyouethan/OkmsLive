using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace OkmsLive.Models
{
    internal class WasapiCaptureViewModel : ViewModelBase, IDisposable
    {
        private WasapiCapture _capture;
        private float _peak;
        private readonly SynchronizationContext _synchronizationContext;

        private float _recordLevel;

        private readonly MMDevice _selectedDevice;

        public double Ratio;
        public WasapiCaptureViewModel(int index)
        {
            Ratio = 100;
            _synchronizationContext = SynchronizationContext.Current;
            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            _selectedDevice = enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active).ToList()[index];
            //_selectedDevice.AudioEndpointVolume.MasterVolumeLevelScalar = Convert.ToSingle(Ratio/100.00);
            Record();
        }

        private void Stop()
        {
            if (_capture != null)
            {
                _capture.StopRecording();
            }
        }

        private void Record()
        {
            try
            {
                _capture = new WasapiCapture(_selectedDevice);
                _capture.ShareMode = AudioClientShareMode.Shared;
                _capture.WaveFormat = WaveFormat.CreateIeeeFloatWaveFormat(48000, 2);
                int sampleRate;
                int channelCount;
                using (var c = new WasapiCapture(_selectedDevice))
                {
                    sampleRate = c.WaveFormat.SampleRate;
                    channelCount = c.WaveFormat.Channels;
                }
                _capture.WaveFormat = WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channelCount);
                RecordLevel = _selectedDevice.AudioEndpointVolume.MasterVolumeLevelScalar;
                _capture.StartRecording();
                _capture.DataAvailable += CaptureOnDataAvailable;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }




        private void CaptureOnDataAvailable(object sender, WaveInEventArgs waveInEventArgs)
        {
            UpdatePeakMeter();
        }

        void UpdatePeakMeter()
        {
            // can't access this on a different thread from the one it was created on, so get back to GUI thread
            _synchronizationContext.Post(s => Peak = Convert.ToSingle(_selectedDevice.AudioMeterInformation
                .MasterPeakValue * Ratio), null);
        }


        public float Peak
        {
            get { return _peak; }
            set
            {
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                if (_peak != value)
                {
                    _peak = value;
                    OnPropertyChanged("Peak");
                }
            }
        }




        public void Dispose()
        {
            Stop();
        }

        public float RecordLevel
        {
            get { return _recordLevel; }
            set
            {
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                if (_recordLevel != value)
                {
                    _recordLevel = value;
                    if (_capture != null)
                    {
                        _selectedDevice.AudioEndpointVolume.MasterVolumeLevelScalar = value;
                    }
                    OnPropertyChanged("RecordLevel");
                }
            }
        }
    }
}
