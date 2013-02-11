using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;


namespace MobileLoggerApp.pages
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this.Items = new ObservableCollection<ItemViewModel>();
            this.Settings = new ObservableCollection<ItemViewModel>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<ItemViewModel> Items { get; private set; }
        public ObservableCollection<ItemViewModel> Settings { get; private set; }

        private string _sampleProperty = "Sample Runtime Property Value";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding
        /// </summary>
        /// <returns></returns>
        public string SampleProperty
        {
            get
            {
                return _sampleProperty;
            }
            set
            {
                if (value != _sampleProperty)
                {
                    _sampleProperty = value;
                    NotifyPropertyChanged("SampleProperty");
                }
            }
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            // Sample data; replace with real data
            this.Settings.Add(new ItemViewModel() { LineOne = "setting one", LineTwo = "Yo Dawg", LineThree = "Some relevant setting" });
            this.Settings.Add(new ItemViewModel() { LineOne = "setting two", LineTwo = "I heard", LineThree = "Some relevant setting" });
            this.Settings.Add(new ItemViewModel() { LineOne = "setting three", LineTwo = "You Like", LineThree = "Some relevant setting" });
            this.Settings.Add(new ItemViewModel() { LineOne = "setting four", LineTwo = "Settings", LineThree = "Some relevant setting" });
            this.Settings.Add(new ItemViewModel() { LineOne = "setting five", LineTwo = "So We", LineThree = "Some relevant setting" });
            this.Settings.Add(new ItemViewModel() { LineOne = "setting six", LineTwo = "Put Settings", LineThree = "Some relevant setting" });
            this.Settings.Add(new ItemViewModel() { LineOne = "setting seven", LineTwo = "In Your", LineThree = "Some relevant setting" });
            this.Settings.Add(new ItemViewModel() { LineOne = "setting eight", LineTwo = "Settings", LineThree = "Some relevant setting" });
            this.Settings.Add(new ItemViewModel() { LineOne = "setting nine", LineTwo = "So You", LineThree = "Some relevant setting" });
            this.Settings.Add(new ItemViewModel() { LineOne = "setting ten", LineTwo = "Can Set", LineThree = "Some relevant setting" });
            this.Settings.Add(new ItemViewModel() { LineOne = "setting eleven", LineTwo = "Settings", LineThree = "Some relevant setting" });
            this.Settings.Add(new ItemViewModel() { LineOne = "setting twelve", LineTwo = "While You", LineThree = "Some relevant setting" });
            this.Settings.Add(new ItemViewModel() { LineOne = "setting thirteen", LineTwo = "Set Settings", LineThree = "Some relevant setting" });
            this.Settings.Add(new ItemViewModel() { LineOne = "setting fourteen", LineTwo = "Gooby", LineThree = "Some relevant setting" });
            this.Settings.Add(new ItemViewModel() { LineOne = "setting fifteen", LineTwo = "Plz", LineThree = "Some relevant setting" });
            this.Settings.Add(new ItemViewModel() { LineOne = "setting sixteen", LineTwo = "Dolan", LineThree = "Some relevant setting" });

            this.IsDataLoaded = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}