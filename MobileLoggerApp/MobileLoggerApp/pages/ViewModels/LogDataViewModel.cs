using System.ComponentModel;
using System.Runtime.Serialization;

namespace MobileLoggerApp.pages
{
    [DataContract]
    public class LogDataViewModel : INotifyPropertyChanged
    {
        private string _dataLineOne;
        [DataMember]
        public string DataLineOne
        {
            get { return _dataLineOne; }
            set
            {
                _dataLineOne = value;
                NotifyPropertyChanged("_dataLineOne");
            }
        }

        private string _dataLineTwo;
        [DataMember]
        public string DataLineTwo
        {
            get { return _dataLineTwo; }
            set
            {
                _dataLineTwo = value;
                NotifyPropertyChanged("_dataLineTwo");
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
