using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModel.Inicio
{
    class LoginViewModel : ViewModelBase
    {
        public ICommand LoginCommand { get; set; }

        private readonly Services.INavigationService _navigationService;

        public LoginViewModel()
        {
            this.LoginCommand = new Command(this.Login);

            this._navigationService = DependencyService.Get<Services.INavigationService>();
        }

        public void Login()
        {
            
        }

    }
}
