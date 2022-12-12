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

        List<Product> products;

        public DelegateCommand LoginCommand { get; }
        public AsyncDelegateCommand TestCommand { get; set; }
        public List<Product> Products { get => products; set => products = value; }

        MarthaProcessor marthaProcessor;

        NavigationStore _navigationStore;

        public LoginViewModel(NavigationStore navigationStore) {
            LoginCommand = new DelegateCommand(LoginAsync);
            TestCommand = new AsyncDelegateCommand(TestAsync);

            _navigationStore = navigationStore;

            marthaProcessor = MarthaProcessor.Instance;

            TestCommand.Execute("");
            
        }

        public async Task TestAsync()
        {
            var param = "{\"prodName\":\"chef\"}";
            var response = await marthaProcessor.ExecuteQueryAsync("products_byName", param);

            Products = await MarthaResponseConverter<Product>.ConvertAsync(response);

            foreach (var o in Products)
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
