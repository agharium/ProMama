using ProMama.Model;
using ProMama.ViewModel.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModel.Inicio
{
    class IntroducaoViewModel : ViewModelBase
    {
        public ICommand NavigationCommand { get; set; }
        private readonly INavigationService _navigationService;

        public IntroducaoViewModel()
        {
            NavigationCommand = new Command(NavigateToCadastroLogin);
            _navigationService = DependencyService.Get<INavigationService>();
        }

        private void NavigateToCadastroLogin()
        {
            _navigationService.NavigateToCadastroLogin();
        }
    }
}
