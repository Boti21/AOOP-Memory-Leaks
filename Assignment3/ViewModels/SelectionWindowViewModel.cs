using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Assignment3.ViewModels
{
    public partial class SelectionWindowViewModel : ViewModelBase
    {
        private bool CartesianVisible = true;
        private bool PieVisible = false;
        private TabItem selectedTab;
        public TabItem SelectedTab
        {
            get => selectedTab;
            set
            {
                if (selectedTab != value)
                {
                    selectedTab = value;
                    OnPropertyChanged(nameof(SelectedTab));
                    GraphType = selectedTab.Header.ToString();
                }
            }
        }
        [ObservableProperty]
        private ObservableCollection<string> headers = new();
        private string graphType = "Bar";
        public string GraphType
        {
            get => graphType;
            set
            {
                if (graphType != value)
                {
                    graphType = value;
                    OnPropertyChanged(nameof(GraphType));
                    CreateGraph();
                }
            }
        }
        [ObservableProperty]
        private GraphViewModel? selectedGraph;
        public SelectionWindowViewModel()
        {
            AddHeaders();
            CreateGraph();
            if (SelectedGraph is null)
            {
                SelectedGraph = new BarGraphViewModel();
            }
            else if (SelectedGraph != null)
            {
                OnPropertyChanged(nameof(SelectedGraph));
            }
        }

        [RelayCommand]
        private void CreateGraph()
        {
            Console.WriteLine($"Creating Graph: {GraphType}");
            SelectedGraph = GraphType switch
            {
                "Bar" => new BarGraphViewModel(),
                "Pie" => new PieGraphViewModel(),
                "Line" => new LineGraphViewModel(),
                "Scatter" => new ScatterGraphViewModel()
            };

            SelectedGraph.Series[0].Values = new ObservableCollection<double> { 1, 2, 3 };
            SelectedGraph.Series[0].Name = "NewTest";
            SelectedGraph.Title.Text = $"{GraphType} Chart";
            SelectedGraph.XAxes[0].Name = "X-Axis";
            SelectedGraph.YAxes[0].Name = "Y-Axis";

            if (GraphType == "Pie")
            {
                CartesianVisible = false;
                PieVisible = true;
            }
            else
            {
                CartesianVisible = true;
                PieVisible = false;
            }

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