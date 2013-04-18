using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace MobileLoggerApp.pages
{
    [DataContract]
    public class SearchResultsViewModel : INotifyPropertyChanged
    {
        private string _searchLineOne;
        [DataMember]
        public string SearchLineOne
        {
            get { return _searchLineOne; }
            set
            {
                _searchLineOne = value;
                NotifyPropertyChanged("_searchLineOne");
            }
        }

        private string _searchLineTwo;
        [DataMember]
        public string SearchLineTwo
        {
            get { return _searchLineTwo; }
            set
            {
                _searchLineTwo = value;
                NotifyPropertyChanged("_searchLineTwo");
            }
        }

        private string _searchLineThree;
        [DataMember]
        public string SearchLineThree
        {
            get { return _searchLineThree; }
            set
            {
                _searchLineThree = value;
                NotifyPropertyChanged("_searchLineThree");
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (null != PropertyChanged)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
