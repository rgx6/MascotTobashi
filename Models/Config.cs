using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace MascotTobashi.Models
{
    [Serializable]
    public class Config
    {
        public static readonly double WidthMin = 300;
        public static readonly double HeightMin = 300;

        public static readonly double IntervalRangeMin = 0.1;
        public static readonly double IntervalRangeMax = 300;
        public static readonly double IntervalStep = 0.1;
        public static readonly double DurationRangeMin = 0.1;
        public static readonly double DurationRangeMax = 10.0;
        public static readonly double DurationStep = 0.1;
        public static readonly double ScaleRangeMin = 0.1;
        public static readonly double ScaleRangeMax = 3.0;
        public static readonly double ScaleStep = 0.1;

        private static readonly double InitialLeft = 0;
        private static readonly double InitialTop = 0;
        private static readonly double InitialWidth = 300;
        private static readonly double InitialHeight = 300;
        private static readonly bool InitialTopmost = true;
        private static readonly double InitialInterval = 5;
        private static readonly double InitialDurationMin = 0.5;
        private static readonly double InitialDurationMax = 3.0;
        private static readonly double InitialScale = 0.5;

        private static readonly string filePath = Path.Combine(
            Path.GetDirectoryName(Assembly.GetEntryAssembly().Location),
            "Config.xml");

        public static Config Current { get; set; }

        public double Left { get; set; }
        public double Top { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public bool Topmost { get; set; }
        public double LaunchIntervalSeconds { get; set; }
        public double LaunchDurationSecondsMin { get; set; }
        public double LaunchDurationSecondsMax { get; set; }
        public double Scale { get; set; }

        public static void Load()
        {
            try
            {
                if (filePath == null || !File.Exists(filePath))
                {
                    throw new FileNotFoundException(filePath);
                }

                var serializer = new XmlSerializer(typeof(Config));

                using (var sr = new StreamReader(filePath, new UTF8Encoding(false)))
                {
                    Current = (Config)serializer.Deserialize(sr);
                    Current.Check();
                }
            }
            catch (Exception e)
            {
                Current = GetInitialSettings();
                Debug.WriteLine(e);
            }
        }

        public static Config GetInitialSettings()
        {
            return new Config
            {
                Left = InitialLeft,
                Top = InitialTop,
                Width = InitialWidth,
                Height = InitialHeight,
                Topmost = InitialTopmost,
                LaunchIntervalSeconds = InitialInterval,
                LaunchDurationSecondsMin = InitialDurationMin,
                LaunchDurationSecondsMax = InitialDurationMax,
                Scale = InitialScale,
            };
        }

        public void Save()
        {
            var serializer = new XmlSerializer(typeof(Config));

            using (var sw = new StreamWriter(filePath, false, new UTF8Encoding(false)))
            {
                try
                {
                    serializer.Serialize(sw, this);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
        }

        private void Check()
        {
            if (Current.Width < WidthMin)
            {
                Current.Width = InitialWidth;
            }

            if (Current.Height < HeightMin)
            {
                Current.Height = InitialHeight;
            }

            if (Current.LaunchIntervalSeconds < IntervalRangeMin || IntervalRangeMax < Current.LaunchIntervalSeconds)
            {
                Current.LaunchIntervalSeconds = InitialInterval;
            }

            if (Current.LaunchDurationSecondsMin < DurationRangeMin || DurationRangeMax < Current.LaunchDurationSecondsMin)
            {
                Current.LaunchDurationSecondsMin = InitialDurationMin;
            }

            if (Current.LaunchDurationSecondsMax < DurationRangeMin || DurationRangeMax < Current.LaunchDurationSecondsMax)
            {
                Current.LaunchDurationSecondsMax = InitialDurationMax;
            }

            if (Current.LaunchDurationSecondsMax < Current.LaunchDurationSecondsMin)
            {
                Current.LaunchDurationSecondsMin = InitialDurationMin;
                Current.LaunchDurationSecondsMax = InitialDurationMax;
            }

            if (Current.Scale < ScaleRangeMin || ScaleRangeMax < Current.Scale)
            {
                Current.Scale = InitialScale;
            }
        }
    }
}
