using ProMama.ViewModel.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModel.Extra
{
    public class AddCriancaViewModel : ContentPage
    {
        public ICommand NavigationCommand { get; set; }

        private readonly INavigationService _navigationService;

        public AddCriancaViewModel()
        {
            this.NavigationCommand = new Command(this.NavigateToHome);

            this._navigationService = DependencyService.Get<INavigationService>();
        }

        private void NavigateToHome()
        {
            this._navigationService.NavigateToHome();
        }
    }
}