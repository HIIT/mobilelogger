using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace MobileLoggerApp.pages
{
    [DataContract]
    public class ItemViewModel : INotifyPropertyChanged
    {
        private string _lineOne;
        [DataMember]
        public string LineOne
        {
            get { return _lineOne; }
            set
            {
                _lineOne = value;
                NotifyPropertyChanged("_lineOne");
            }
        }

        private string _lineTwo;
        [DataMember]
        public string LineTwo
        {
            get { return _lineTwo; }
            set
            {
                _lineTwo = value;
                NotifyPropertyChanged("_lineTwo");
            }
        }

        private string _lineThree;
        [DataMember]
        public string LineThree
        {
            get { return _lineThree; }
            set
            {
                _lineThree = value;
                NotifyPropertyChanged("_lineThree");
            }
        }

        private JObject _searchResult;
        [DataMember]
        public JObject SearchResult
        {
            get { return _searchResult; }
            set
            {
                _searchResult = value;
                NotifyPropertyChanged("_searchResult");
            }
        }

        private bool _isChecked;
        [DataMember]
        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                NotifyPropertyChanged("_isChecked");
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
