using System;
using System.ComponentModel;
using MascotTobashi.Models;

namespace MascotTobashi.ViewModels
{
    public class ConfigViewModel : INotifyPropertyChanged
    {
        private double _launchIntervalSeconds;
        public double LaunchIntervalSeconds
        {
            get { return _launchIntervalSeconds; }
            set
            {
                if (_launchIntervalSeconds != value)
                {
                    _launchIntervalSeconds = value;
                    NotifyPropertyChanged("LaunchIntervalSeconds");
                }
            }
        }

        public double IntervalRangeMin
        {
            get { return Config.IntervalRangeMin; }
        }

        public double IntervalRangeMax
        {
            get { return Config.IntervalRangeMax; }
        }

        public double IntervalStep
        {
            get { return Config.IntervalStep; }
        }

        private double _launchDurationSecondsMin;
        public double LaunchDurationSecondsMin
        {
            get { return _launchDurationSecondsMin; }
            set
            {
                if (_launchDurationSecondsMin != value)
                {
                    _launchDurationSecondsMin = value;
                    NotifyPropertyChanged("LaunchDurationSecondsMin");
                    NotifyPropertyChanged("LaunchDurationSecondsMax");

                    if (this.LaunchDurationSecondsMax < this.LaunchDurationSecondsMin)
                    {
                        this.LaunchDurationSecondsMax = this.LaunchDurationSecondsMin;
                    }
                }
            }
        }

        private double _launchDurationSecondsMax;
        public double LaunchDurationSecondsMax
        {
            get { return _launchDurationSecondsMax; }
            set
            {
                if (_launchDurationSecondsMax != value)
                {
                    _launchDurationSecondsMax = value;
                    NotifyPropertyChanged("LaunchDurationSecondsMin");
                    NotifyPropertyChanged("LaunchDurationSecondsMax");

                    if (this.LaunchDurationSecondsMax < this.LaunchDurationSecondsMin)
                    {
                        this.LaunchDurationSecondsMin = this.LaunchDurationSecondsMax;
                    }
                }
            }
        }

        public double DurationRangeMin
        {
            get { return Config.DurationRangeMin; }
        }

        public double DurationRangeMax
        {
            get { return Config.DurationRangeMax; }
        }

        public double DurationStep
        {
            get { return Config.DurationStep; }
        }

        private double _scale;
        public double Scale
        {
            get { return _scale; }
            set
            {
                if (_scale != value)
                {
                    _scale = value;
                    NotifyPropertyChanged("Scale");
                }
            }
        }

        public double ScaleRangeMin
        {
            get { return Config.ScaleRangeMin; }
        }

        public double ScaleRangeMax
        {
            get { return Config.ScaleRangeMax; }
        }

        public double ScaleStep
        {
            get { return Config.ScaleStep; }
        }

        public ConfigViewModel()
        {
            this.LaunchIntervalSeconds = Config.Current.LaunchIntervalSeconds;
            this.LaunchDurationSecondsMin = Config.Current.LaunchDurationSecondsMin;
            this.LaunchDurationSecondsMax = Config.Current.LaunchDurationSecondsMax;
            this.Scale = Config.Current.Scale;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
