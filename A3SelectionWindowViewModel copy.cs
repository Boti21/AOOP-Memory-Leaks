using System.Collections.ObjectModel;
using System.ComponentModel;
using SkiaSharp;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using Assignment3.Views;

namespace Assignment3.ViewModels
{
    public class SelectionWindowViewModel : ViewModelBase
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


using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CsvHelper;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;
using LiveChartsCore.SkiaSharpView.Extensions;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Assignment3.ViewModels;

public partial class SelectionWindowViewModel : ViewModelBase
{
    private ObservableCollection<string> Sources { get; } = new();
    [ObservableProperty]
    private string graphType = string.Empty;
    [ObservableProperty]
    private GraphViewModel selectedGraph;

    public SelectionWindowViewModel()
    {
        GraphType = "Bar";
        AddSources();
        CreateGraph();

        if (SelectedGraph != null)
        {
            // SelectedGraph.Sources = Sources;
        }
    }

    [RelayCommand]
    private void CreateGraph()
    {
        SelectedGraph = GraphType switch
        {
            "Bar" => new BarGraphViewModel(),
            // "Pie" => new PieGraphViewModel(),
            // "Line" => new LineGraphViewModel(),
            // "Scatter" => new ScatterGraphViewModel()
        };
    }

    private void AddSources()
    {
        List<string> sources = new List<string>
        {
            "Country", "Year", "Food Category", "Total Waste (Tons)", "Economic Loss (Million $)",
            "Avg Waste per Capita (Kg)", "Population (Million)", "Household Waste (%)"
        };

        foreach (string source in sources)
        {
            Sources.Add(source);
        }