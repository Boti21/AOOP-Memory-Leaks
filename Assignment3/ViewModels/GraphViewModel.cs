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
    public virtual ObservableCollection<ISeries> Series { get; set; } //General Data
    public virtual LabelVisual Title { get; set; } //General Title
    public virtual ObservableCollection<Axis> XAxes { get; set; } //General X-axis
    public virtual ObservableCollection<Axis> YAxes { get; set; } //General Y-axis
}