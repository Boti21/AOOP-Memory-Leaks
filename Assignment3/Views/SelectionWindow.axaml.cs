using Assignment3.ViewModels;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Assignment3.Views;

public partial class SelectionWindow : UserControl
{
    public SelectionWindow()
    {
        InitializeComponent();
        DataContext = new SelectionWindowViewModel();
    }
}