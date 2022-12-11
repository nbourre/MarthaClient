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
using Microsoft.Maui.Controls.Platform.Compatibility;

namespace MarthaOnMaui.ViewModels
{
    /// <summary>
    /// Src : https://www.youtube.com/watch?v=z4_EQ2wM6No
    /// </summary>
    public class LoginViewModel : BaseViewModel
    {
        private string username;

        public string Username
        {
            get { return username; }
            set {
                username = value;
                OnPropertyChanged();
            }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { 
                password = value;
                OnPropertyChanged();
            }
        }

        public DelegateCommand<string> LoginCommand { get; }
        public DelegateCommand<string> TestCommand { get; set; }

        MarthaProcessor marthaProcessor;

        NavigationStore _navigationStore;

        public LoginViewModel(NavigationStore navigationStore) {
            LoginCommand = new DelegateCommand<string>(Login);
            TestCommand = new DelegateCommand<string>(TestAsync);

            _navigationStore = navigationStore;

            marthaProcessor = MarthaProcessor.Instance;

            TestCommand.Execute("");
            
        }

        private async void TestAsync(string obj)
        {
            var param = "{\"prodName\":\"chef\"}";
            var response = await marthaProcessor.ExecuteQueryAsync("products_byName", param);

            var list = await MarthaResponseConverter<Product>.ConvertAsync(response);

            foreach (var o in list)
            {
                Console.WriteLine(o.ToString());
            }
        }

        private async void Login(string obj)
        {
            _navigationStore.CurrentViewModel = new QueryBuilderViewModel(_navigationStore);
        }
    }
}
