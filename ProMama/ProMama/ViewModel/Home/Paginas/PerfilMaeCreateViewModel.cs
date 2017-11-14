using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModel.Home.Paginas
{
    class PerfilMaeCreateViewModel : ViewModelBase
    {
        public ICommand NavigationCommand { get; set; }

        private readonly Services.INavigationService _navigationService;

        public PerfilMaeCreateViewModel()
        {
            this.NavigationCommand = new Command(this.NavigateToPerfilMae);

            this._navigationService = DependencyService.Get<Services.INavigationService>();
        }

        private async void NavigateToPerfilMae()
        {
            await this._navigationService.NavigateToPerfilMae();
        }
    }
}
