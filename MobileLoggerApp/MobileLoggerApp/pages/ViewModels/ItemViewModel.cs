using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace MobileLoggerApp.pages
{
    public class ItemViewModel : INotifyPropertyChanged
    {
        public string LineOne
        {
            get;
            set;
        }

        public string LineTwo
        {
            get;
            set;
        }

        public string LineThree
        {
            get;
            set;
        }

        public JObject SearchResult
        {
            get;
            set;
        }

        public bool IsChecked
        {
            get;
            set;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
