using MarthaClient.Commands;
using MarthaClient.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarthaClient.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public DelegateCommand<string> ConnectCommand { get; }

        NavigationStore _navigationStore;

        public LoginViewModel(NavigationStore navigationStore) {
            ConnectCommand = new DelegateCommand<string>(Connect);
            _navigationStore = navigationStore;
        }

        private void Connect(string obj)
        {
            _navigationStore.CurrentViewModel = new QueryBuilderViewModel(_navigationStore);
        }
    }
}
