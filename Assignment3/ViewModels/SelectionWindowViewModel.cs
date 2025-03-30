using System.Collections.ObjectModel;
using System.ComponentModel;
using SkiaSharp;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using Assignment3.Views;

namespace Assignment3.ViewModels
{
    public class SelectionWindowViewModel : INotifyPropertyChanged
    {
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
        
        public ObservableCollection<dynamic> SelectedData { get; set; }
        public SelectionWindowViewModel()
        {
            Headers = new ObservableCollection<string>
            {
                "Header 1",
                "Header 2",
                "Header 3"
            };
            SelectedData = new ObservableCollection<dynamic>
            {
                // 1.0m, 2.0m, 3.0m
                "bruh",
                "I woke up",
                "in a new Bugatti"
            };
        }
        public ISeries[] Series { get; set; } =
        [
            new ColumnSeries<double>
            {
                Name = "Mary",
                Values = [2, 5, 4]
            },
            new ColumnSeries<double>
            {
                Name = "Ana",
                Values = [3, 1, 6]
            }
        ];

        public Axis[] XAxes { get; set; } =
        [
            new Axis
            {
                Labels = ["Category 1", "Category 2", "Category 3"],
                LabelsRotation = 0,
                SeparatorsPaint = new SolidColorPaint(new SKColor(200, 200, 200)),
                SeparatorsAtCenter = false,
                TicksPaint = new SolidColorPaint(new SKColor(35, 35, 35)),
                TicksAtCenter = true,
                // By default the axis tries to optimize the number of 
                // labels to fit the available space, 
                // when you need to force the axis to show all the labels then you must: 
                ForceStepToMin = true, 
                MinStep = 1 
            }
        ];
    }
}