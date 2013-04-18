using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace MobileLoggerApp.pages
{
    [DataContract]
    public class SearchResults : INotifyPropertyChanged
    {
        private string _searchResultTitle;
        [DataMember]
        public string SearchResultTitle
        {
            get { return _searchResultTitle; }
            set
            {
                _searchResultTitle = value;
                NotifyPropertyChanged("_searchResult");
            }
        }

        private string _searchResultSnippet;
        [DataMember]
        public string SearchResultSnippet
        {
            get { return _searchResultSnippet; }
            set
            {
                _searchResultSnippet = value;
                NotifyPropertyChanged("_searchResultSnippet");
            }
        }

        private string _searchResultLink;
        [DataMember]
        public string SearchResultLink
        {
            get { return _searchResultLink; }
            set
            {
                _searchResultLink = value;
                NotifyPropertyChanged("_searchResultLink");
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
