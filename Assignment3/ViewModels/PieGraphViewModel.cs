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

public class PieGraphViewModel : GraphViewModel
{
    private ObservableCollection<ISeries> series;
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
    public override LabelVisual Title { get; set; }
    public override ObservableCollection<Axis> XAxes { get; set; }
    public override ObservableCollection<Axis> YAxes { get; set; }

    public PieGraphViewModel()
    {
        Series = new ObservableCollection<ISeries>
        {
            new ScatterSeries<double>
            {
                Values = new ObservableCollection<double>(),
                Name = string.Empty
            }
        };
        PieSeries = new ObservableCollection<ISeries>
            {
                new PieSeries<double>
                {
                    Values = new ObservableCollection<double>(),
                    Name = string.Empty
                }
            };
        Title = new LabelVisual
            {
                Text = string.Empty
            };
        XAxes = new ObservableCollection<Axis>
            {
                new Axis
                {
                    Name = string.Empty
                } 
            };
        YAxes = new ObservableCollection<Axis>
            {
                new Axis
                {
                    Name = string.Empty
                }
            };
    }
}