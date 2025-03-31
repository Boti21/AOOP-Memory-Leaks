// using CommunityToolkit.Mvvm.ComponentModel;
// using CommunityToolkit.Mvvm.Input;
// using CsvHelper;
// using LiveChartsCore;
// using LiveChartsCore.Defaults;
// using LiveChartsCore.SkiaSharpView;
// using LiveChartsCore.SkiaSharpView.Drawing.Geometries;
// using LiveChartsCore.SkiaSharpView.Extensions;
// using LiveChartsCore.SkiaSharpView.Painting;
// using LiveChartsCore.SkiaSharpView.VisualElements;
// using SkiaSharp;
// using System.Collections.Generic;
// using System.Collections.ObjectModel;

// namespace Assignment3.ViewModels;

// public class LineGraphViewModel : GraphViewModel
// {
//     public override string GraphType => "Line";
//     public override ObservableCollection<ISeries> Series { get; set; } = { new LineSeries<double> { Values = new double[] { 0, 0, 0 } } };
//     public override LabelVisual Title { get; set; } = new() { Text = "Bar Chart" };
//     public override ObservableCollection<Axis> XAxes { get; set; } = { new Axis { Name = "X-Axis" } };
//     public override ObservableCollection<Axis> YAxes { get; set; } = { new Axis { Name = "Y-Axis" } };
// }