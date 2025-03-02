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
    int NoRows = 6;
    int NoColumns = 5;
    int[,] data = { {0, 0, 0, 0, 0},
                    {0, 13, 0, 13, 0},
                    {0, 0, 0, 0, 0},
                    {1, 0, 0, 0, 1},
                    {0, 1, 1, 1, 0},
                    {0, 0, 0, 0, 0}};

    public MainWindow()
    {
        InitializeComponent();
        this.Width = 600; 
        this.Height = 500;

        GenerateGrid();
    }

    private IBrush GetColorFromValue(int value)
    {
        return value switch
        {
            1 => new SolidColorBrush(Color.Parse("#000000")),
            2 => new SolidColorBrush(Color.Parse("#ff0000")),
            3 => new SolidColorBrush(Color.Parse("#00ff00")),
            4 => new SolidColorBrush(Color.Parse("#0000ff")),
            5 => new SolidColorBrush(Color.Parse("#ffff00")),
            6 => new SolidColorBrush(Color.Parse("#ff00ff")),
            7 => new SolidColorBrush(Color.Parse("#00ffff")),
            8 => new SolidColorBrush(Color.Parse("#0fffff")),
            9 => new SolidColorBrush(Color.Parse("#ff0fff")),
            10 => new SolidColorBrush(Color.Parse("#ffff0f")),
            11 => new SolidColorBrush(Color.Parse("#f0f0f0")),
            12 => new SolidColorBrush(Color.Parse("#0f0f0f")),
            13 => new SolidColorBrush(Color.Parse("#b0ffa0")),
            14 => new SolidColorBrush(Color.Parse("#06afab")),
            15 => new SolidColorBrush(Color.Parse("#60cd04")),
            _ => Brushes.White  // Default case
        };
    }

    private void GenerateGrid() {
        DynamicGrid.RowDefinitions.Clear();
        DynamicGrid.ColumnDefinitions.Clear();

        for (int i = 0; i < NoRows; i++) {
            DynamicGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        }

        for (int j = 0; j < NoColumns; j++) {
            DynamicGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        }

        for (int row=0; row<NoRows; row++) {
            for (int col=0; col<NoColumns; col++) {
                int r = row;  // Capture row index
                int c = col;  // Capture col index
                // Need to capture the incrementing values, otherwise an out of bounds causes a crash
                // This has to be some interpreted sorcery

                var button = new Button {
                    HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch,
                    VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch,
                    BorderThickness = new Thickness(0.3),
                    BorderBrush = Brushes.Black,
                    CornerRadius = new CornerRadius(0),
                    Width = 40,
                    Height = 30,
                };

                button.Background = GetColorFromValue(data[r, c]);

                button.Click += (sender, e) =>
                {
                    data[r, c] = (data[r, c] + 1) % 16;
                    button.Background = GetColorFromValue(data[r, c]);

                    // Console.WriteLine("value: "+ data[r, c]); // Debugging
                };

                Grid.SetRow(button, r);
                Grid.SetColumn(button, c);

                DynamicGrid.Children.Add(button);
            }
        }

    }
}