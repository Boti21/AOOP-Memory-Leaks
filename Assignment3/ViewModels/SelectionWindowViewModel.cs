using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Assignment3.ViewModels
{
    public partial class SelectionWindowViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ObservableCollection<string> headers = new();
        [ObservableProperty]
        private string xHeader = string.Empty;
        [ObservableProperty]
        private string yHeader = string.Empty;
        [ObservableProperty]
        public ObservableCollection<dynamic> selectedData = new();
        [ObservableProperty]
        private string graphType = "Bar";
        [ObservableProperty]
        private GraphViewModel? selectedGraph;
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
                ForceStepToMin = true, 
                MinStep = 1 
            }
        ];
        public SelectionWindowViewModel()
        {
            AddHeaders();
            CreateGraph();
        }

        [RelayCommand]
        private void CreateGraph()
        {
            SelectedGraph = GraphType switch
            {
                "Bar" => new BarGraphViewModel(),
                "Pie" => new PieGraphViewModel(),
                "Line" => new LineGraphViewModel(),
                "Scatter" => new ScatterGraphViewModel()
            };
            OnPropertyChanged(nameof(SelectedGraph));
        }
        private void AddHeaders()
        {
            Headers = new ObservableCollection<string>
            {
                "Country", "Year", "Food Category", "Total Waste (Tons)", "Economic Loss (Million $)",
                "Avg Waste per Capita (Kg)", "Population (Million)", "Household Waste (%)"
            };
        }
    }
}