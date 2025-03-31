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

public abstract partial class GraphViewModel : ViewModelBase
{
    [ObservableProperty]
    private string selectedXSource = string.Empty;
    [ObservableProperty]
    private string selectedYSource = string.Empty;
    public abstract string GraphType { get; }
    public abstract ObservableCollection<ISeries> Series { get; set; } //General Data
    public abstract LabelVisual Title { get; set; } //General Title
    public abstract ObservableCollection<Axis> XAxes { get; set; } //General X-axis
    public abstract ObservableCollection<Axis> YAxes { get; set; } //General Y-axis
}