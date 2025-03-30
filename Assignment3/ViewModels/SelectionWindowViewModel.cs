using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Assignment3.ViewModels
{
    public class SelectionWindowViewModel : INotifyPropertyChanged
    {
        public SelectionWindowViewModel()
        {
            Headers = new ObservableCollection<string>
            {
                "Header 1",
                "Header 2",
                "Header 3"
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<string> _headers;
        public ObservableCollection<string> Headers
        {
            get => _headers;
            set
            {
                if (_headers != value)
                {
                    _headers = value;
                    OnPropertyChanged(nameof(Headers));
                }
            }
        }

        private string _xHeader;
        private string _yHeader;
        public string XHeader
        {
            get => _xHeader;
            set
            {
                if (_xHeader != value)
                {
                    _xHeader = value;
                    OnPropertyChanged(nameof(XHeader));
                }
            }
        }
        public string YHeader
        {
            get => _yHeader;
            set
            {
                if (_yHeader != value)
                {
                    _yHeader = value;
                    OnPropertyChanged(nameof(YHeader));
                }
            }
        }
    }
}