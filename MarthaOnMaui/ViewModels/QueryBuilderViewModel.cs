using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarthaOnMaui.Commands;
using MarthaOnMaui.Stores;

namespace MarthaOnMaui.ViewModels
{
    public class QueryBuilderViewModel : BaseViewModel
    {
        public string Name => nameof(QueryBuilderViewModel);

        public DelegateCommand<string> NavigateLoginCommand { get; }
        private readonly NavigationStore _navigationStore;

        public QueryBuilderViewModel(NavigationStore navigationStore)
        {
            NavigateLoginCommand = new DelegateCommand<string>(NavigateLogin);
            _navigationStore = navigationStore;
        }

        private void NavigateLogin(string obj)
        {
            _navigationStore.CurrentViewModel = new LoginViewModel(_navigationStore);
        }
    }
}
