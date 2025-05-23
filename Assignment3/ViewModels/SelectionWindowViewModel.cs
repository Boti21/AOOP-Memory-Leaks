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
using System.Linq;
using Assignment3.Models;

namespace Assignment3.ViewModels
{
    public partial class SelectionWindowViewModel : ViewModelBase
    {
        private MainWindowViewModel mainWindowViewModel;
        public bool CartesianVisible = true;
        public bool PieVisible = false;
        
        public Dataset Dataset;
        
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
                    if (selectedTab is TabItem tabItem && tabItem.Tag is string tag)
                    {
                        GraphType = tag;
                    }
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
        public SelectionWindowViewModel(MainWindowViewModel mainWindowViewModel)
        {
            Dataset = new Dataset();
            
            this.mainWindowViewModel = mainWindowViewModel;
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
            SelectedGraph = GraphType switch
            {
                "Bar" => new BarGraphViewModel(),
                "Pie" => new PieGraphViewModel(),
                "Line" => new LineGraphViewModel(),
                "Scatter" => new ScatterGraphViewModel()
            };

            //SelectedGraph.Series[0].Values = new ObservableCollection<double> { 1, 2, 3 };
            //SelectedGraph.Series[0].Values = new ObservableCollection<double>(Dataset.GetTotalWasteInYear(Dataset.Years[2]).YAxis);
            SelectedGraph.Series[0].Values = new ObservableCollection<double>(Dataset.GetAvgEconomicLoss().YAxis);
            SelectedGraph.Series[0].Name = "NewTest";
            SelectedGraph.Title.Text = $"{GraphType} Chart";
            SelectedGraph.XAxes[0].Name = "X-Axis";
            selectedGraph.XAxes[0].Labels = Headers;
            SelectedGraph.YAxes[0].Name = "Y-Axis";
            
            SelectedGraph.PieSeries.Clear();
            
            SelectedGraph.PieSeries.Add(new PieSeries<double> { Values = new ObservableCollection<double> { 10 }, Name = "This" });
            SelectedGraph.PieSeries.Add(new PieSeries<double> { Values = new ObservableCollection<double> { 7 }, Name = "Was" });
            SelectedGraph.PieSeries.Add(new PieSeries<double> { Values = new ObservableCollection<double> { 14 }, Name = "Annoying" });

            if (SelectedGraph is PieGraphViewModel)
            {
                SelectedGraph.IsPie = true;
                SelectedGraph.IsCart = false;
            }
            else
            {
                SelectedGraph.IsPie = false;
                SelectedGraph.IsCart = true;
            }
            
            // if (GraphType == "Pie")
            // {
            //     SelectedGraph.PieVisible = true;
            //     SelectedGraph.CartesianVisible = false;
            // }
            // else
            // {
            //     SelectedGraph.PieVisible = false;
            //     SelectedGraph.CartesianVisible = true;
            // }

            OnPropertyChanged(nameof(SelectedGraph));
        }
        private void AddHeaders()
        {
            /*
            Headers = new ObservableCollection<string>
            {
                "Country", "Year", "Food Category", "Total Waste (Tons)", "Economic Loss (Million $)",
                "Avg Waste per Capita (Kg)", "Population (Million)", "Household Waste (%)"
            };
            */
            //Headers = new ObservableCollection<string>(Dataset.GetTotalWasteInYear(Dataset.Years[2]).XAxis);
            Headers = new ObservableCollection<string>(Dataset.GetAvgEconomicLoss().XAxis);
            //Headers = new ObservableCollection<string>(Dataset.Header);
            //Headers = new ObservableCollection<string>(Dataset.GetTotalWasteInYear(Dataset.Years[2]).XAxis);
        }
        [RelayCommand]
        private void AddGraph()
        {
            if (SelectedGraph != null)
            {
                mainWindowViewModel.Graphs.Add(SelectedGraph);
                mainWindowViewModel.CurrentView = mainWindowViewModel.mainView;
                mainWindowViewModel.MainWindowVisibility = true;
            }
        }
        [RelayCommand]
        private void SwitchToCanvas()
        {
            mainWindowViewModel.CurrentView = mainWindowViewModel.mainView;
            mainWindowViewModel.MainWindowVisibility = true;
        }
    }
}