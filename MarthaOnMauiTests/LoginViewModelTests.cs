using MarthaOnMaui.Stores;
using MarthaOnMaui.ViewModels;

namespace MarthaOnMauiTests
{
    public class LoginViewModelTests
    {
        LoginViewModel _sut;

        public LoginViewModelTests()
        {
            NavigationStore navigationStore = new();

            _sut = new LoginViewModel(navigationStore);
            navigationStore.CurrentViewModel = _sut;            
        }

        [Fact]
        public async void TestAsync_Should_Put_Values_In_VM()
        {
            await _sut.TestAsync();

            Assert.NotNull(_sut.Products);
        }
    }
}