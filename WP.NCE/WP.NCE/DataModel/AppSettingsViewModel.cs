using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WP.NCE.DataModel
{
    public class AppSettingsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<string> _audios;
        public ObservableCollection<string> Audios
        {
            get
            {
                if (_audios == null)
                {
                    _audios = new ObservableCollection<string>() { "美音", "英音" };
                }
                return _audios;
            }
        }

        // The isolated storage key names of our settings
        const string DefaultAudioSettingKeyName = "DefaultAudioSetting";


        // The default value of our settings
        const string DefaultLearningTypeSettingValue = "美音";

        /// <summary>
        /// Constructor that gets the application settings.
        /// </summary>
        public AppSettingsViewModel()
        {
            var defaultAudio = GetValueOrDefault<string>(DefaultAudioSettingKeyName, DefaultLearningTypeSettingValue);
            AudioSetting = defaultAudio;
        }

        /// <summary>
        /// Update a setting value for our application. If the setting does not
        /// exist, then add the setting.
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool AddOrUpdateValue(string Key, Object value)
        {
            bool valueChanged = false;
            var settings = Windows.Storage.ApplicationData.Current.LocalSettings;
            // If the key exists
            if (settings.Values.ContainsKey(Key))
            {
                // If the value has changed
                if (settings.Values[Key] != value)
                {
                    // Store the new value
                    settings.Values[Key] = value;
                    valueChanged = true;
                }
            }
            // Otherwise create the key.
            else
            {
                settings.Values.Add(Key, value);
                valueChanged = true;
            }

            return valueChanged;
        }

        public void Save(string value)
        {
            AddOrUpdateValue(DefaultAudioSettingKeyName, value);
        }

        /// <summary>
        /// Get the current value of the setting, or if it is not found, set the 
        /// setting to the default setting.
        /// </summary>
        /// <typeparam name="valueType"></typeparam>
        /// <param name="Key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public valueType GetValueOrDefault<valueType>(string Key, valueType defaultValue)
        {
            valueType value;
            var settings = Windows.Storage.ApplicationData.Current.LocalSettings;
            // If the key exists, retrieve the value.
            if (settings.Values.ContainsKey(Key))
            {
                value = (valueType)settings.Values[Key];
            }
            // Otherwise, use the default value.
            else
            {
                value = defaultValue;
            }

            return value;
        }


        private string _audioSetting;
        public string AudioSetting
        {
            get { return _audioSetting; }
            set
            {
                this.SetProperty(ref this._audioSetting, value);
            }
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] String propertyName = null)
        {
            if (object.Equals(storage, value))
            {
                return false;
            }

            storage = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
    
}
