using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModel.Home.Paginas
{
    class PerfilCriancaCreateViewModel : ViewModelBase
    {
        public ICommand NavigationCommand { get; set; }

        private readonly Services.INavigationService _navigationService;

        public PerfilCriancaCreateViewModel()
        {
            this.NavigationCommand = new Command(this.NavigateToPerfilCrianca);

            this._navigationService = DependencyService.Get<Services.INavigationService>();
        }

        private async void NavigateToPerfilCrianca()
        {
            await this._navigationService.NavigateToPerfilCrianca();
        }
    }
}
