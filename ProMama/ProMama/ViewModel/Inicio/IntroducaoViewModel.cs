using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModel.Inicio
{
    class IntroducaoViewModel : ViewModelBase
    {
        public ICommand NavigationCommand { get; set; }

        private readonly Services.INavigationService _navigationService;

        public IntroducaoViewModel()
        {
            this.NavigationCommand = new Command(this.NavigateToCadastroLogin);

            this._navigationService = DependencyService.Get<Services.INavigationService>();
        }

        private void NavigateToCadastroLogin()
        {
            this._navigationService.NavigateToCadastroLogin();
        }
    }
}
