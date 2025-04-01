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

public class BarGraphViewModel : GraphViewModel
{
    public override string GraphType => "Bar";
    private ObservableCollection<ISeries> series = 
        new ObservableCollection<ISeries> { new ColumnSeries<double> { Values = new ObservableCollection<double> { 1, 2, 3 } } };
    public override ObservableCollection<ISeries> Series
    {
        get => series;
        set
        {
            if (series != value)
            {
                series = value;
                OnPropertyChanged();
            }
        }
    }
    public override LabelVisual Title { get; set; } = new() { Text = "Bar Chart" };
    public override ObservableCollection<Axis> XAxes { get; set; } = new ObservableCollection<Axis> { new Axis { Name = "X-Axis" } };
    public override ObservableCollection<Axis> YAxes { get; set; } = new ObservableCollection<Axis> { new Axis { Name = "Y-Axis" } };
}