using System.ComponentModel;
using System.Runtime.Serialization;

namespace MobileLoggerApp.pages
{
    [DataContract]
    public class ApplicationInfo : INotifyPropertyChanged
    {
        private string _appName;
        [DataMember]
        public string AppName
        {
            get { return _appName; }
            set
            {
                _appName = value;
                NotifyPropertyChanged("_appInfo");
            }
        }

        private string _currentVersion;
        [DataMember]
        public string CurrentVersion
        {
            get { return _currentVersion; }
            set
            {
                _currentVersion = value;
                NotifyPropertyChanged("_currentVersion");
            }
        }

        private string _contactName;
        [DataMember]
        public string ContactName
        {
            get { return _contactName; }
            set
            {
                _contactName = value;
                NotifyPropertyChanged("_contactName");
            }
        }

        private string _contactNumber;
        [DataMember]
        public string ContactNumber
        {
            get { return _contactNumber; }
            set
            {
                _contactNumber = value;
                NotifyPropertyChanged("_contactNumber");
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
