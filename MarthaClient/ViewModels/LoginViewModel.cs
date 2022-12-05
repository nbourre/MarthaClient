using MarthaClient.Commands;
using MarthaClient.Stores;
using MarthaService;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MarthaClient.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public DelegateCommand<string> ConnectCommand { get; }
        public DelegateCommand<string> TestCommand { get; set; }

        MarthaProcessor marthaProcessor;

        NavigationStore _navigationStore;

        public LoginViewModel(NavigationStore navigationStore) {
            ConnectCommand = new DelegateCommand<string>(Connect);
            TestCommand = new DelegateCommand<string>(Test);

            _navigationStore = navigationStore;

            marthaProcessor = MarthaProcessor.Instance;
            
        }

        private async void Test(string obj)
        {
            await marthaProcessor.ExecuteQuery("products_all");
        }

        private void Connect(string obj)
        {
            _navigationStore.CurrentViewModel = new QueryBuilderViewModel(_navigationStore);
        }
    }
}
