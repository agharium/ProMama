
using Plugin.Connectivity;
using ProMama.Components.Cryptography;
using ProMama.Models;
using ProMama.ViewModels.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModels.Home.Paginas
{
    class PerfilMaeEditSenhaViewModel : ViewModelBase
    {
        private Aplicativo app = Aplicativo.Instance;

        public string SenhaAtual { get; set; }
        public string NovaSenha { get; set; }
        public string NovaSenhaConfirmacao { get; set; }

        private INavigation Navigation { get; set; }
        public ICommand SalvarCommand { get; set; }

        private readonly IMessageService MessageService;
        private readonly IRestService RestService;

        public PerfilMaeEditSenhaViewModel(INavigation _navigation)
        {
            Navigation = _navigation;
            SalvarCommand = new Command(Salvar);
            MessageService = DependencyService.Get<IMessageService>();
            RestService = DependencyService.Get<IRestService>();
        }

        private async void Salvar()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                await MessageService.AlertDialog("Você precisa estar conectado à internet para trocar a senha.");
            }
            else if (SenhaAtual.Equals(string.Empty) || NovaSenha.Equals(string.Empty) || NovaSenhaConfirmacao.Equals(string.Empty))
            {
                await MessageService.AlertDialog("Nenhum campo pode estar vazio.");
            }
            else if (NovaSenha.Length < 8)
            {
                await MessageService.AlertDialog("A senha precisa ter no mínimo 8 caracteres.");
            }
            else if (!NovaSenha.Equals(NovaSenhaConfirmacao))
            {
                await MessageService.AlertDialog("As senhas não são iguais.");
            }
            else if (!PasswordHash.ValidatePassword(SenhaAtual, app._usuario.password))
            {
                await MessageService.AlertDialog("A senha atual inserida está incorreta.");
            }
            else
            {
                var senhaAntiga = app._usuario.password;
                app._usuario.password = PasswordHash.CreateHash(NovaSenha);

                var result = await RestService.UsuarioUpdate(app._usuario);
                if (result.success)
                {
                    App.UsuarioDatabase.Save(app._usuario);
                    await MessageService.AlertDialog("A senha foi atualizada com sucesso.");
                    await Navigation.PopAsync();
                } else
                {
                    app._usuario.password = senhaAntiga;
                    await MessageService.AlertDialog("Algo de errado aconteceu. A senha não foi trocada.");
                }
            }
        }
    }
}
