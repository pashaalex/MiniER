using System;

namespace MiniER
{
    public class SettingsDTO : ICloneable
    {
        protected static SettingsDTO _settings = new SettingsDTO();

        public static SettingsDTO GetSettings()
        {
            return _settings;
        }

        public static void SetSettings(SettingsDTO settings)
        {
            _settings = settings;
        }

        public bool ShowSchema { get; set; }
        public bool ShowDataType { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
