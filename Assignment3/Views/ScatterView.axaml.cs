using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Assignment3.Views
{
    public partial class ScatterView : UserControl
    {
        public ScatterView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
