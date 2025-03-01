using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Media;
using Avalonia.Platform;

namespace Assignment1.Views;

public partial class MainWindow : Window
{
    int NoRows = 12;
    int NoColumns = 9;

    public MainWindow()
    {
        InitializeComponent();
        this.Width = 600; 
        this.Height = 500;

        GenerateGrid(NoRows, NoColumns);
    }

    private void GenerateGrid(int rows, int columns) {
        DynamicGrid.RowDefinitions.Clear();
        DynamicGrid.ColumnDefinitions.Clear();

        for (int i = 0; i < rows; i++) {
            DynamicGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        }

        for (int j = 0; j < columns; j++) {
            DynamicGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        }

        for (int row=0; row<rows; row++) {
            for (int col=0; col<columns; col++) {
                var button = new Button {
                    HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch,
                    VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch,
                    BorderThickness = new Thickness(0),
                    Width = 40,
                    Height = 30,
                    Background = Brushes.White
                };

                button.Click += (sender, e) =>
                {
                    var btn = (Button)sender;
                    btn.Background = btn.Background == Brushes.White ? Brushes.Black : Brushes.White;
                };

                // Set position in grid
                Grid.SetRow(button, row);
                Grid.SetColumn(button, col);

                // Add to the grid
                DynamicGrid.Children.Add(button);
            }
        }

    }
}