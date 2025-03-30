using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Assignment3.Views
{
    public partial class DesignView : UserControl
    {
        public DesignView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}