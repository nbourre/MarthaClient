using MarthaOnMaui.Commands;
using MarthaOnMaui.Stores;
using MarthaService;
using Models;

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
            set => SetProperty(ref username, value);
        }

        private string password;

        public string Password
        {
            get { return password; }
            set => SetProperty(ref password, value);
        }

        public DelegateCommand LoginCommand { get; }
        public DelegateCommand TestCommand { get; set; }

        MarthaProcessor marthaProcessor;

        NavigationStore _navigationStore;

        public LoginViewModel(NavigationStore navigationStore) {
            LoginCommand = new DelegateCommand(LoginAsync);
            TestCommand = new DelegateCommand(TestAsync);

            _navigationStore = navigationStore;

            marthaProcessor = MarthaProcessor.Instance;

            TestCommand.Execute("");
            
        }

        private async void TestAsync()
        {
            var param = "{\"prodName\":\"chef\"}";
            var response = await marthaProcessor.ExecuteQueryAsync("products_byName", param);

            var list = await MarthaResponseConverter<Product>.ConvertAsync(response);

            foreach (var o in list)
            {
                Console.WriteLine(o.ToString());
            }
        }

        private async void LoginAsync()
        {
            _navigationStore.CurrentViewModel = await Task.Run(() => new QueryBuilderViewModel(_navigationStore));
        }
    }
}
