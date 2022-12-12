using MarthaOnMaui.Stores;
using MarthaOnMaui.ViewModels;

namespace MarthaOnMaui;



public partial class App : Application
{

	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}

    protected override void OnStart()
    {
        NavigationStore navigationStore = new();

        navigationStore.CurrentViewModel = new LoginViewModel(navigationStore);
        MainPage.BindingContext = navigationStore.CurrentViewModel;
        base.OnStart();
    }
}

