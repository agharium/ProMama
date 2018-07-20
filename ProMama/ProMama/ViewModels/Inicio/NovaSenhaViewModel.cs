using Acr.UserDialogs;
using Plugin.Connectivity;
using ProMama.Components.Cryptography;
using ProMama.Models;
using ProMama.ViewModels.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModels.Inicio
{
    class NovaSenhaViewModel
    {
        private Aplicativo app = Aplicativo.Instance;

        public string NovaSenha { get; set; }
        public string NovaSenhaConfirmacao { get; set; }

        public ICommand AtualizarSenhaCommand { get; set; }

        private readonly IMessageService MessageService;
        private readonly INavigationService NavigationService;
        private readonly IRestService RestService;

        public NovaSenhaViewModel()
        {
            AtualizarSenhaCommand = new Command(AtualizarSenha);

            MessageService = DependencyService.Get<IMessageService>();
            NavigationService = DependencyService.Get<INavigationService>();
            RestService = DependencyService.Get<IRestService>();
        }

        private async void AtualizarSenha()
        {
            IProgressDialog LoadingDialog = UserDialogs.Instance.Loading("Por favor, aguarde...", null, null, true, MaskType.Black);
            if (!CrossConnectivity.Current.IsConnected)
            {
                LoadingDialog.Hide();
                await MessageService.AlertDialog("Você precisa estar conectado à internet para trocar a senha.");
            }
            else if (string.IsNullOrEmpty(NovaSenha) || string.IsNullOrEmpty(NovaSenhaConfirmacao))
            {
                LoadingDialog.Hide();
                await MessageService.AlertDialog("Nenhum campo pode estar vazio.");
            }
            else if (NovaSenha.Length < 6)
            {
                LoadingDialog.Hide();
                await MessageService.AlertDialog("A senha precisa ter no mínimo 6 caracteres.");
            }
            else if (!NovaSenha.Equals(NovaSenhaConfirmacao))
            {
                LoadingDialog.Hide();
                await MessageService.AlertDialog("As senhas não são iguais.");
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

                    LoadingDialog.Hide();
                    NavigationService.NavigateHome();
                }
                else
                {
                    app._usuario.password = senhaAntiga;
                    LoadingDialog.Hide();
                    await MessageService.AlertDialog("Algo de errado aconteceu. A senha não foi atualizada.");
                }
            }
        }
    }
}
