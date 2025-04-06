using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Avalonia;
using System.Collections.ObjectModel;
using System;
using Assignment3.ViewModels;

namespace Assignment3.Views
{
    public partial class DesignView : UserControl
    {
        // private Grid? Chart;
        // private MainWindowViewModel viewModel;
        // private SelectionWindowViewModel selVModel;
        private GraphViewModel graphViewModel;
        public DesignView()
        {
            InitializeComponent();
            // viewModel = new MainWindowViewModel();
            // selVModel = viewModel.SelectionWindowViewModel;
            
            this.DataContextChanged += (_, _) =>
            {
                // selVModel = DataContext as SelectionWindowViewModel;
                graphViewModel = DataContext as GraphViewModel;
                GenerateChart();
            };
            
            // GenerateChart();
        }


        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            Chart = this.FindControl<Grid>("Chart");
        }

        public void GenerateChart()
        {
            Chart.Children.Clear();
            if (graphViewModel is PieGraphViewModel) GeneratePieChart();
            else GenerateCartesianChart();
        }

        public void GeneratePieChart()
        {
            if (Chart == null) 
            {
                Console.WriteLine("I woke up");
                return;
            }
            Console.WriteLine("in a new Bugatti");
    
            var pieChart = new PieChart
            {
                Series = graphViewModel.PieSeries,
                LegendPosition = LiveChartsCore.Measure.LegendPosition.Right,
            };
            
            Chart.Children.Clear();
            Chart.Children.Add(pieChart);
        }

        public void GenerateCartesianChart()
        {
            if (Chart == null) 
            {
                Console.WriteLine("I woke up");
                return;
            }
            Console.WriteLine("in a new Bugatti");

            var carChart = new CartesianChart();
            carChart.Series = graphViewModel.Series;
            carChart.XAxes = graphViewModel.XAxes;
            carChart.YAxes = graphViewModel.YAxes;
            carChart.Title = graphViewModel.Title;
            
            Chart.Children.Clear();
            Chart.Children.Add(carChart);
        }
    }

    
}