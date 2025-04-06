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
using LiveChartsCore.Kernel.Sketches;

namespace Assignment3.ViewModels;

public abstract partial class GraphViewModel : ViewModelBase
{
    public virtual ObservableCollection<ISeries> Series { get; set; } //General Data

    private ObservableCollection<ISeries> pieSeries;
    public virtual ObservableCollection<ISeries> PieSeries
    {
        get => pieSeries;
        set
        {
            if (pieSeries != value)
            {
                pieSeries = value;
                OnPropertyChanged(nameof(PieSeries));
            }
        }
    }
    public virtual LabelVisual Title { get; set; } //General Title
    public virtual ObservableCollection<Axis> XAxes { get; set; } //General X-axis
    public virtual ObservableCollection<Axis> YAxes { get; set; } //General Y-axis

    public bool IsPie { get; set; }
    public bool IsCart { get; set; }
    // public bool CartesianVisible = true;
    // public bool PieVisible = false;
}