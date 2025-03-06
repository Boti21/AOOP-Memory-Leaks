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
// using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Avalonia.Markup.Xaml.Converters;
using Tmds.DBus.Protocol;
using Avalonia.Rendering;
using Assignment1.Views;
// using MainWindow.axaml.cs;

namespace Assignment1.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public void ReadFile(string filePath, out int rows, out int columns, out int[,] pixelValues)
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

    public void SaveFile(string fileName, int rows, int columns, int[,] pixelValues)
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

    public void SaveImage(string fileName, int rows, int columns, int[,] pixelValues)
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

                    // var colorBrush = window.GetColorFromValue(pixelValues[r, c]);
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
        Console.WriteLine(filePath);
        Directory.CreateDirectory("Images");
        bitmap.Save(filePath);
    }

    public IBrush GetColorFromValue(int value)
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

    // public MainWindowModel model;

    // public void Test()
    // {
    //     Greeting = this.model.otherGreeting;
    // }
}