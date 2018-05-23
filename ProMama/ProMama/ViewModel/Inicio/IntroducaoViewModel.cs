using Plugin.Connectivity;
using ProMama.ViewModel.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModel.Inicio
{
    class IntroducaoViewModel : ViewModelBase
    {
        public ICommand NavigationCommand { get; set; }
        private readonly INavigationService NavigationService;
        private readonly IMessageService MessageService;

        public IntroducaoViewModel()
        {
            NavigationCommand = new Command(NavigateToCadastroLogin);
            NavigationService = DependencyService.Get<INavigationService>();
            MessageService = DependencyService.Get<IMessageService>();
        }

        private async void NavigateToCadastroLogin()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                NavigationService.NavigateCadastroLogin();
            } else
            {
                await MessageService.AlertDialog("Você precisa estar conectado à internet para poder realizar o primeiro acesso ao aplicativo.");
            }
        }
    }
}
