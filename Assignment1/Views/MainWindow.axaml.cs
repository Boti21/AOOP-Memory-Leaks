using System;
using System.Runtime.InteropServices;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media.Imaging;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Styling;
using Avalonia.Data;
using System.IO.Enumeration;
using Assignment1.Models;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Avalonia.Markup.Xaml.Converters;
using Tmds.DBus.Protocol;
using Assignment1.ViewModels;

namespace Assignment1.Views;

public partial class MainWindow : Window
{
    int NoRows;
    int NoColumns;
    int[,] data; //Store the pixel values

    MainWindowViewModel viewmodel = new MainWindowViewModel();

    public MainWindow()
    {
        InitializeComponent();
        this.Width = 600; 
        this.Height = 500;

        this.Background = Brushes.White;

        DefineSaveLoadButtons();

        DefineRotateButton();

        DefineFlipButtons();
    }

    private IBrush GetColorFromValue(int value)
    {
        return value switch // 16 color support
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

    private void DisplaySizeInfo() // Display the current grid size for the image
    {
        SizeTextBlock.Text = $"Size: {NoRows}x{NoColumns}";
        SizeTextBlock.Foreground = Brushes.Black;
        SizeTextBlock.FontFamily = "Comic Sans"; // Because Yes.
        SizeTextBlock.FontSize = 24;
    }

    public void GenerateGrid() {
        DynamicGrid.RowDefinitions.Clear();
        DynamicGrid.ColumnDefinitions.Clear();

        for (int i = 0; i < NoRows; i++) {
            DynamicGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // Add rows
        }

        for (int j = 0; j < NoColumns; j++) {
            DynamicGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto }); // Add columns
        }

        for (int row=0; row<NoRows; row++) {
            for (int col=0; col<NoColumns; col++) {
                int r = row;  // Capture row index
                int c = col;  // Capture col index
                // Need to capture the incrementing values, otherwise an out of bounds causes a crash
                // This has to be some interpreted sorcery

                var button = new Button { // Specific button for the image pixels to be clickable
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    BorderThickness = new Thickness(0.1),
                    BorderBrush = Brushes.Black,
                    CornerRadius = new CornerRadius(0),
                    Width = 40,
                    Height = 30,
                };

                button.Background = viewmodel.GetColorFromValue(data[r, c]);

                button.Click += (sender, e) =>
                {
                    data[r, c] = (data[r, c] + 1) % 16;
                    button.Background = viewmodel.GetColorFromValue(data[r, c]);

                    // Console.WriteLine("value: "+ data[r, c]); // Debugging
                };

                Grid.SetRow(button, r);
                Grid.SetColumn(button, c);

                DynamicGrid.Children.Add(button);
            }
        }
    }

    public void DefineSaveLoadButtons() // Create and adjust the Save Load buttons as desired
    {
        var saveButton = MakeButton("Save");

        var loadButton = MakeButton("Load");

        Grid.SetColumn(saveButton, 0);
        Grid.SetColumn(loadButton, 1);

        saveButton.Click += (sender, e) => // Add the save functionality when pressing the save button
        {
            var fileName = ImageFileTextBox.Text.ToString();

            viewmodel.SaveFile(fileName, NoRows, NoColumns, data);

            viewmodel.SaveImage(fileName, NoRows, NoColumns, data);
            //SerializeToFile(filePath); // Ask Boti
        };

        loadButton.Click += (sender, e) => // Add the load functionality when pressing the load button
        {
            var filePathElements = ImageFileTextBox.Text.Split('/');
            string fileName = filePathElements.Last().ToString();

            string filePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "ImageFiles", fileName);

            viewmodel.ReadFile(filePath, out NoRows, out NoColumns, out data);

            GenerateGrid();

            DisplaySizeInfo();
        };

        SaveLoadButtons.Children.Add(saveButton);
        SaveLoadButtons.Children.Add(loadButton);
    }

    public void DefineRotateButton()
    {
        var rotateButton = MakeButton("Rotate");

        rotateButton.Margin = new Thickness(0, 25, 0, 0);

        ParentStackPanel.Children.Add(rotateButton);

        rotateButton.Click += (sender, e) =>
        {
            int[,] rotated_data = new int[NoColumns, NoRows];

            for (int r = 0; r < NoRows; r++)
            {
                for (int c = 0; c < NoColumns; c++)
                {
                    rotated_data[c, NoRows - 1 - r] = data[r, c];
                }
            }

            data = rotated_data;
            var temp = NoRows;
            NoRows = NoColumns;
            NoColumns = temp;

            GenerateGrid();

            DisplaySizeInfo();
        };
    }

    private void DefineFlipButtons()
    {
        var verticalflipButton = MakeButton("V-Flip");

        var horizontalflipButton = MakeButton("H-Flip");

        Grid.SetColumn(verticalflipButton, 0);
        Grid.SetColumn(horizontalflipButton, 1);

        verticalflipButton.Click += (sender, e) =>
        {
            int[,] flipped_data = new int[NoRows, NoColumns];

            for (int r = 0; r < NoRows; r++)
            {
                for (int c = 0; c < NoColumns; c++)
                {
                    flipped_data[NoRows - 1 - r, c] = data[r, c];
                }
            }

            data = flipped_data;

            GenerateGrid();

            DisplaySizeInfo();
        };

        horizontalflipButton.Click += (sender, e) =>
        {
            int[,] flipped_data = new int[NoRows, NoColumns];

            for (int r = 0; r < NoRows; r++)
            {
                for (int c = 0; c < NoColumns; c++)
                {
                    flipped_data[r, NoColumns - 1 - c] = data[r, c];
                }
            }

            data = flipped_data;

            GenerateGrid();

            DisplaySizeInfo();
        };

        FlipButtons.Children.Add(verticalflipButton);
        FlipButtons.Children.Add(horizontalflipButton);
    }

    static Button MakeButton(string content)
    {
        var button = new Button
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Stretch,
            BorderThickness = new Thickness(0.1),
            BorderBrush = Brushes.Black,
            CornerRadius = new CornerRadius(0),
            Width = 100,
            Height = 30,
            Background = Brushes.Gray,
            Content = content,
            HorizontalContentAlignment = HorizontalAlignment.Center
        };

        return button;
    }
}