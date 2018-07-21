using Acr.UserDialogs;
using Plugin.Connectivity;
using ProMama.Components;
using ProMama.Components.Cryptography;
using ProMama.Models;
using ProMama.ViewModels.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModels.Home.Paginas
{
    class PerfilMaeEditEmailViewModel
    {
        private Aplicativo app = Aplicativo.Instance;

        public string NovoEmail { get; set; }

        private INavigation Navigation { get; set; }
        public ICommand SalvarCommand { get; set; }

        private readonly IMessageService MessageService;
        private readonly IRestService RestService;

        public PerfilMaeEditEmailViewModel(INavigation _navigation)
        {
            Navigation = _navigation;
            SalvarCommand = new Command(Salvar);
            MessageService = DependencyService.Get<IMessageService>();
            RestService = DependencyService.Get<IRestService>();
        }

        private async void Salvar()
        {
            IProgressDialog LoadingDialog = UserDialogs.Instance.Loading("Por favor, aguarde...", null, null, true, MaskType.Black);
            if (!CrossConnectivity.Current.IsConnected)
            {
                LoadingDialog.Hide();
                await MessageService.AlertDialog("Você precisa estar conectado à internet para trocar seu e-mail.");
            }
            else if (string.IsNullOrEmpty(NovoEmail))
            {
                LoadingDialog.Hide();
                await MessageService.AlertDialog("Nenhum campo pode estar vazio.");
            }
            else if (!Ferramentas.ValidarEmailRegex(NovoEmail))
            {
                LoadingDialog.Hide();
                await MessageService.AlertDialog("E-mail inválido.");
            }
            else if (NovoEmail.Equals(app._usuario.email))
            {
                LoadingDialog.Hide();
                await MessageService.AlertDialog("O novo e-mail inserido é igual ao e-mail atual da conta.");
            }
            else
            {
                LoadingDialog.Hide();
                var prompt = await UserDialogs.Instance.PromptAsync(new PromptConfig()
                                .SetTitle("Confirme a ação com sua senha")
                                .SetPlaceholder("Senha")
                                .SetInputMode(InputType.Password)
                                .SetCancelText("Cancelar")
                                .SetOkText("Confirmar"));
                LoadingDialog.Show();
                if (prompt.Ok)
                {
                    var senha = prompt.Text;
                    if (!PasswordHash.ValidatePassword(senha, app._usuario.password))
                    {
                        LoadingDialog.Hide();
                        await MessageService.AlertDialog("Senha incorreta.");
                    } else
                    {
                        var emailAntigo = app._usuario.email;
                        app._usuario.email = NovoEmail;

                        var result = await RestService.UsuarioUpdate(app._usuario);
                        if (result.success)
                        {
                            App.UsuarioDatabase.Save(app._usuario);
                            LoadingDialog.Hide();
                            await MessageService.AlertDialog("O e-mail foi atualizado com sucesso.");
                            await Navigation.PopAsync();
                        }
                        else
                        {
                            app._usuario.email = emailAntigo;
                            LoadingDialog.Hide();
                            await MessageService.AlertDialog("Algo de errado aconteceu. O seu e-mail não foi atualizado. Tente novamente mais tarde.");
                        }
                    }
                }
                LoadingDialog.Hide();
            }
        }
    }
}
