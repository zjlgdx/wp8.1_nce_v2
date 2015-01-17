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
        private ObservableCollection<AudioSetting> _audios;
        public ObservableCollection<AudioSetting> Audios
        {
            get
            {
                if (_audios == null)
                {
                    _audios = new ObservableCollection<AudioSetting>() { new AudioSetting { AudioName = "美音" }, new AudioSetting { AudioName = "英音" } };
                }
                return _audios;
            }
        }

        // The isolated storage key names of our settings
        const string DefaultAudioSettingKeyName = "DefaultAudioSetting";


        // The default value of our settings
        const string DefaultLearningTypeSettingValue = "英音";

        /// <summary>
        /// Constructor that gets the application settings.
        /// </summary>
        public AppSettingsViewModel()
        {
            var defaultAudio = GetValueOrDefault<string>(DefaultAudioSettingKeyName, DefaultLearningTypeSettingValue);
            if (AudioSetting != null)
            {
                AudioSetting.AudioName = defaultAudio;
            }
            else
            {
                AudioSetting = new DataModel.AudioSetting() { AudioName = defaultAudio };
            }
            
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


        private AudioSetting _audioSetting;
        public AudioSetting AudioSetting
        {
            get { return _audioSetting; }
            set
            {
                this.SetProperty(ref this._audioSetting, value);
                AddOrUpdateValue(DefaultAudioSettingKeyName, value.AudioName);
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

    public class AudioSetting : INotifyPropertyChanged
    {
        private string audioName;
        public string AudioName { get { return audioName; } set { this.SetProperty(ref this.audioName, value); } }

        public override string ToString()
        {
            return AudioName;
        }

        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                if (this.AudioName == obj.ToString())
                {
                    return true;
                }
            }
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return AudioName.GetHashCode();
        }

        public event PropertyChangedEventHandler PropertyChanged;

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
    }
}
