using System.ComponentModel;
using System.Runtime.Serialization;

namespace MobileLoggerApp.pages
{
    [DataContract]
    public class HandlerSettings : INotifyPropertyChanged
    {
        private string _handlerName;
        [DataMember]
        public string HandlerName
        {
            get { return _handlerName; }
            set
            {
                _handlerName = value;
                NotifyPropertyChanged("_handlerName");
            }
        }

        private bool _handlerIsChecked;
        [DataMember]
        public bool HandlerIsChecked
        {
            get { return _handlerIsChecked; }
            set
            {
                _handlerIsChecked = value;
                NotifyPropertyChanged("_handlerIsChecked");
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
