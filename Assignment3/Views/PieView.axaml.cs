using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Assignment3.Views
{
    public partial class PieView : UserControl
    {
        public PieView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
