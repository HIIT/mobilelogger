using System.ComponentModel;
using System.Runtime.Serialization;

namespace MobileLoggerApp.pages
{
    [DataContract]
    public class LogData : INotifyPropertyChanged
    {
        private string _logDataHeading;
        [DataMember]
        public string LogDataHeading
        {
            get { return _logDataHeading; }
            set
            {
                _logDataHeading = value;
                NotifyPropertyChanged("_logDataHeading");
            }
        }

        private string _logDataContent;
        [DataMember]
        public string LogDataContent
        {
            get { return _logDataContent; }
            set
            {
                _logDataContent = value;
                NotifyPropertyChanged("_logDataContent");
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
