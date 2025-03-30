using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Assignment3.Views
{
    public partial class BarView : UserControl
    {
        public BarView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
