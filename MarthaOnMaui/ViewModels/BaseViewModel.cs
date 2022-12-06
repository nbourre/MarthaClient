using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace MarthaOnMaui.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        [ObservableProperty]
        public bool _isBusy;

        [ObservableProperty]
        public string _title;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
