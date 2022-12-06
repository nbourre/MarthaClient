using MarthaOnMaui.Commands;
using MarthaOnMaui.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MarthaService;
using Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace MarthaOnMaui.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string password;


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
            var response = await marthaProcessor.ExecuteQuery("products_all");

            foreach (object o in response.Data)
            {
                var stringContent = o.ToString();

                var tempProduct = JsonSerializer.Deserialize<Product>(stringContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                Console.WriteLine(tempProduct);
            }
        }

        private void Connect(string obj)
        {
            _navigationStore.CurrentViewModel = new QueryBuilderViewModel(_navigationStore);
        }
    }
}
