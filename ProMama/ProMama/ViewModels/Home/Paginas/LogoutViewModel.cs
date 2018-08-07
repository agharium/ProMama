using Acr.UserDialogs;
using Plugin.Connectivity;
using ProMama.Components;
using ProMama.Models;
using ProMama.ViewModels.Services;
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
                IProgressDialog LoadingDialog = UserDialogs.Instance.Loading("Por favor, aguarde...", null, null, true, MaskType.Black);
                
                await Ferramentas.CancelarNotificacoes(app._usuario.id);
                
                if (CrossConnectivity.Current.IsConnected)
                    await Ferramentas.UploadInformacoes();

                App.AcompanhamentoDatabase.DeleteByUserId(app._usuario.id);
                App.FotoDatabase.DeleteByUserId(app._usuario.id);
                App.MarcoDatabase.DeleteByUserId(app._usuario.id);
                App.UltimaCrianca = 0;
                App.UltimoUsuario = 0;
                app._usuario = null;
                app._crianca = null;

                LoadingDialog.Hide();
                _navigationService.NavigateCadastroLogin();
            }
            else
            {
                app._home.Detail_Home();
            }
        }
    }
}
