using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CsvHelper;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;
using LiveChartsCore.SkiaSharpView.Extensions;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using SkiaSharp;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Assignment3.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<GraphViewModel> Graphs { get; } = new();
    [ObservableProperty]
    private GraphViewModel selectedGraph;

    public MainWindowViewModel()
    {
        selectedGraph = new BarGraphViewModel();
    }

    [RelayCommand]
    private void AddGraph(GraphViewModel graph)
    {
        if (graph != null)
        {
            Graphs.Add(graph);
        }
    }

    [RelayCommand]
    private void UpdateGraph()
    {
        
    }
}
