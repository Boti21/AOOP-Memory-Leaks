using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Assignment3.Views
{
    public partial class LineView : UserControl
    {
        public LineView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
