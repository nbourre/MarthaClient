using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarthaOnMaui.Stores;
using MarthaOnMaui.ViewModels;

namespace MarthaOnMaui.Commands
{
    /// <summary>
    /// Commande permettant la navigation entre les ViewModels
    /// </summary>
    /// <typeparam name="TViewModel"></typeparam>
    public class NavigateCommand<TViewModel> : DelegateCommand<string> 
        where TViewModel : BaseViewModel
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;

        public NavigateCommand(NavigationStore navigationStore, Func<TViewModel> createViewModel) : base(null)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
            _execute = new Action<string>(Execute);
        }

        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = _createViewModel();
        }
    }
}
