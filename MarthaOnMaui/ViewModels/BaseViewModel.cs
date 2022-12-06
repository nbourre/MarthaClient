using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace MarthaOnMaui.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {

        private bool isBusy;

        public bool IsBusy
        {
            get => isBusy;
            set { 
                isBusy= value;
                OnPropertyChanged();
            }
        }

        private string title;

        public string Title
        {
            get { return title; }
            set {
                title = value;
                OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
