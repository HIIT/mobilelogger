using System.ComponentModel;
using System.Runtime.Serialization;

namespace MobileLoggerApp.pages
{
    [DataContract]
    public class SettingsViewModel : INotifyPropertyChanged
    {
        private string _settingsLineOne;
        [DataMember]
        public string SettingsLineOne
        {
            get { return _settingsLineOne; }
            set
            {
                _settingsLineOne = value;
                NotifyPropertyChanged("_settingsLineOne");
            }
        }

        private bool _settingsIsChecked;
        [DataMember]
        public bool SettingsIsChecked
        {
            get { return _settingsIsChecked; }
            set
            {
                _settingsIsChecked = value;
                NotifyPropertyChanged("_settingsIsChecked");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (null != PropertyChanged)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
