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
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Assignment3.Models;

namespace Assignment3.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public UserControl mainView { get; set; } 
    [ObservableProperty]
    private UserControl currentView;
    [ObservableProperty]
    private bool mainWindowVisibility = true;
    public SelectionWindow SelectionWindow { get; private set; }
    public SelectionWindowViewModel SelectionWindowViewModel { get; private set; }
    public ObservableCollection<GraphViewModel> Graphs { get; } = new();

    public MainWindowViewModel()
    {
        SelectionWindowViewModel = new SelectionWindowViewModel(this);
        SelectionWindow = new SelectionWindow { DataContext = SelectionWindowViewModel };

        // Testing
        /*
        var dataset = new Dataset();
        dataset.GetRowOfCountryInCatInYear(dataset.Countries[0], dataset.Categories[0], dataset.Years[0]);
        dataset.GetCountryTrendInCat(dataset.Countries[0], dataset.Header[3]);
        dataset.GetTotalWasteInYear(dataset.Years[0]);
        dataset.GetSumOfTotalEconomicLossTrend();
        dataset.GetPopulationTrendOfCountry(dataset.Countries[0]);
        dataset.GetHouseHoldWasteInYear(dataset.Years[0]);
        dataset.GetAvgEconomicLoss();
        */
        // Until here
    }

    [RelayCommand]
    private void EnterSelection()
    {
        MainWindowVisibility = false;
        mainView = CurrentView;
        CurrentView = SelectionWindow;
    }
}
