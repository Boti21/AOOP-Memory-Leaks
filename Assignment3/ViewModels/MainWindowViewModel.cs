using Assignment3.Views;
using Avalonia.Controls;
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
    [ObservableProperty]
    private UserControl currentView;
    [ObservableProperty]
    private bool selectionButtonVisibility = true;
    public SelectionWindow SelectionWindow { get; private set; }
    public SelectionWindowViewModel SelectionWindowViewModel { get; private set; }
    public ObservableCollection<GraphViewModel> Graphs { get; } = new();

    public MainWindowViewModel()
    {
        SelectionWindowViewModel = new SelectionWindowViewModel();
        SelectionWindow = new SelectionWindow { DataContext = SelectionWindowViewModel };
    }

    [RelayCommand]
    private void EnterSelection()
    {
        SelectionButtonVisibility = false;
        CurrentView = SelectionWindow;
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
