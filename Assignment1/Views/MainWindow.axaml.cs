using System;
using System.IO;
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

namespace Assignment1.Views;

public partial class MainWindow : Window
{
    int NoRows;
    int NoColumns;
    /*int[,] data = { {0, 1, 0, 0, 0, 1, 0},
                    {0, 0, 0 ,0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {1, 0, 0, 0, 0, 0, 1},
                    {0, 1, 1, 1, 1, 1, 0}};*/
    int[,] data;

    public MainWindow()
    {
        InitializeComponent();
        this.Width = 600; 
        this.Height = 500;

        this.Background = Brushes.White;

        DisplaySizeInfo();

        //GenerateGrid();

        DefineSaveLoadButtons();

        DefineRotateButton();
    }

    private static IBrush GetColorFromValue(int value)
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

    private void DisplaySizeInfo() // Display the current grid size for the image
    {
        SizeTextBlock.Text = $"Size: {NoRows}x{NoColumns}";
        SizeTextBlock.Foreground = Brushes.Black;
        SizeTextBlock.FontFamily = "Comic Sans"; // Because Yes.
        SizeTextBlock.FontSize = 24;
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
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    BorderThickness = new Thickness(0.1),
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

    private void DefineSaveLoadButtons() // Create and adjust the Save Load buttons as desired
    {
        var saveButton = MakeButton("Save");

        var loadButton = MakeButton("Load");

        Grid.SetColumn(saveButton, 0);
        Grid.SetColumn(loadButton, 1);

        saveButton.Click += (sender, e) => // Add the save functionality when pressing the save button
        {
            var fileName = ImageFileTextBox.Text.ToString();

            SaveFile(fileName, NoRows, NoColumns, data);

            SaveImage(fileName, NoRows, NoColumns, data);
            //SerializeToFile(filePath); // Ask Boti
        };

        loadButton.Click += (sender, e) => // Add the load functionality when pressing the load button
        {
            var filePathElements = ImageFileTextBox.Text.Split('/');
            string fileName = filePathElements.Last().ToString();

            string filePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "ImageFiles", fileName);

            ReadFile(filePath, out NoRows, out NoColumns, out data);

            GenerateGrid();
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

            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    Console.Write(data[i, j] + " ");
                }
                Console.WriteLine();
            }

            data = rotated_data;
            var temp = NoRows;
            NoRows = NoColumns;
            NoColumns = temp;

            for (int i = 0; i < rotated_data.GetLength(0); i++)
            {
                for (int j = 0; j < rotated_data.GetLength(1); j++)
                {
                    Console.Write(rotated_data[i, j] + " ");
                }
                Console.WriteLine();
            }

            GenerateGrid();
        };
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

    static void ReadFile(string filePath, out int rows, out int columns, out int[,] pixelValues)
    {
        string[] lines = File.ReadAllLines(filePath);

        string[] dims = lines[0].Split(' '); // Split rows and columns number by empty space for now
        rows = Convert.ToInt32(dims[0]);
        columns = Convert.ToInt32(dims[1]);

        pixelValues = new int[rows, columns]; // Create array based on number of rows and columns to split pixel values correctly

        int index = 0;
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                pixelValues[r, c] = lines[1][index] - '0';
                index++;
            }
        }
    }

    static void SaveFile(string fileName, int rows, int columns, int[,] pixelValues)
    {
        var filePath = Path.Join("ImageFiles", fileName);
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine($"{rows} {columns}");

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    writer.Write(pixelValues[r, c]);
                }
            }
            writer.WriteLine();
        }
        Console.WriteLine(filePath);
    }

    static void SaveImage(string fileName, int rows, int columns, int[,] pixelValues)
    {
        PixelSize pixelSize = new PixelSize(columns, rows);
        Vector dpi = new Vector(96, 96);

        WriteableBitmap bitmap = new WriteableBitmap(pixelSize, dpi, PixelFormat.Bgra8888, AlphaFormat.Premul);
        
        using(var buffer = bitmap.Lock())
        {
            IntPtr pointer = buffer.Address;

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                
                {
                    int offset = (r * columns + c) * 4;  // Adjust for 4 bytes per pixel

                    var colorBrush = GetColorFromValue(pixelValues[r, c]);

                    byte alpha, red, green, blue;
                    
                    if (colorBrush is SolidColorBrush solidBrush)
                    {
                        var color = solidBrush.Color;

                        alpha = color.A;  // Alpha
                        red = color.R;    // Red
                        green = color.G;  // Green
                        blue = color.B;   // Blue
                    }
                    else
                    {
                        alpha = 255;
                        red = 255;
                        green = 255;
                        blue = 255;
                    }
                    Marshal.WriteByte(pointer + offset, blue);  // Blue
                    Marshal.WriteByte(pointer + offset + 1, green);  // Green
                    Marshal.WriteByte(pointer + offset + 2, red);  // Red
                    Marshal.WriteByte(pointer + offset + 3, alpha);  // Alpha
                }
            }
        }
        fileName = fileName.Split('.')[0];
        var filePath = Path.Join("Images", fileName + ".jpg");
        bitmap.Save(filePath);
    }
}