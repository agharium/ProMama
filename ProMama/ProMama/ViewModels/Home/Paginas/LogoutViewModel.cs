using ProMama.Components;
using ProMama.Models;
using ProMama.ViewModels.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProMama.ViewModels.Home.Paginas
{
    class LogoutViewModel : ViewModelBase
    {
        private Aplicativo app = Aplicativo.Instance;

        private INavigation Navigation;
        private readonly INavigationService _navigationService;
        private readonly IMessageService _messageService;

        public LogoutViewModel(INavigation Navigation)
        {
            this.Navigation = Navigation;
            _navigationService = DependencyService.Get<INavigationService>();
            _messageService = DependencyService.Get<IMessageService>();

            Logout();
        }

        private async void Logout()
        {
            if (await _messageService.ConfirmationDialog("Você tem certeza que deseja sair?", "Sair", "Voltar"))
            {
                Task.Run(async () =>
                {
                    await Ferramentas.CancelarNotificacoes(app._usuario.id);
                });

                App.UltimaCrianca = 0;
                App.UltimoUsuario = 0;
                app._usuario = null;
                app._crianca = null;
                _navigationService.NavigateCadastroLogin();
            }
            else
            {
                app._home.Detail_Home();
            }
        }
    }
}
