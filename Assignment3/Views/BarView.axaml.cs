using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Assignment3.ViewModels;

namespace Assignment3.Views
{
    public partial class BarView : UserControl
    {
        public BarView()
        {
            SelectionWindowViewModel viewModel = new SelectionWindowViewModel();
            
            InitializeComponent();
            
            // SelectedData = new ObservableCollection<dynamic>()
            // {
            //     // 1.0m, 2.0m, 3.0m
            //     "bruh",
            //     "I woke up",
            //     "in a new Bugatti"
            // };
            
            SetSectionVisibility(viewModel.SelectedData);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        
        private void SetSectionVisibility(ObservableCollection<dynamic> data)
        {
            if (data != null)
            {
                if (data[0] is string)
                {
                    this.FindControl<StackPanel>("NumericSection").IsVisible = false;
                    this.FindControl<ListBox>("StringSection").IsVisible = true; 
                }
            }
        }
    }
}
