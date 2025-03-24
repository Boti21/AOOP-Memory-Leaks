using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Assignment2.ViewModels;

public class ViewModelBase : ObservableObject
{
    public event PropertyChangedEventHandler PropertyChanged;
 
    protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
