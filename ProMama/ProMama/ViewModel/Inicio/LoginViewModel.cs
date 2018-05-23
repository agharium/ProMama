using Acr.UserDialogs;
using Plugin.Connectivity;
using ProMama.Model;
using ProMama.ViewModel.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModel.Inicio
{
    class LoginViewModel : ViewModelBase
    {
        private Aplicativo app = Aplicativo.Instance;

        public string Email { get; set; }

        public string Senha { get; set; }

        public ICommand LoginCommand { get; set; }

        private readonly INavigationService NavigationService;
        private readonly IMessageService MessageService;
        private readonly IRestService RestService;

        private bool LoginClicado = false;

        public LoginViewModel()
        {
            LoginCommand = new Command(Login);

            NavigationService = DependencyService.Get<INavigationService>();
            MessageService = DependencyService.Get<IMessageService>();
            RestService = DependencyService.Get<IRestService>();
        }

        public async void Login()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                if (!LoginClicado)
                {
                    LoginClicado = true;

                    if (Email.Equals(string.Empty) || Senha.Equals(string.Empty))
                    {
                        await MessageService.AlertDialog("Nenhum campo pode estar vazio.");
                        LoginClicado = false;
                    }
                    else
                    {
                        var u = new Usuario(Email, Senha);
                        var result = await RestService.UsuarioLogin(u);

                        if (!result.success)
                        {
                            await MessageService.AlertDialog(result.message);
                            LoginClicado = false;
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(u));

                            u = await RestService.UsuarioRead(result);
                            if (u == null)
                            {
                                await MessageService.AlertDialog("Algo de errado não está certo.");
                                LoginClicado = false;
                            }
                            else
                            {
                                using (UserDialogs.Instance.Loading("Por favor, aguarde...", null, null, true, MaskType.Black))
                                {
                                    app._usuario = u;
                                    App.UsuarioDatabase.SaveUsuario(app._usuario);

                                    // Popula banco
                                    App.BairroDatabase.SaveBairroList(await RestService.BairrosRead());
                                    App.PostoDatabase.SavePostoList(await RestService.PostosRead());
                                    App.InformacaoDatabase.SaveInformacaoList(await RestService.InformacoesRead(app._usuario.api_token));
                                    App.DuvidaDatabase.SaveDuvidaList(await RestService.DuvidasRead(app._usuario.api_token));
                                }

                                if (app._usuario.criancas.Count == 0)
                                {
                                    NavigationService.NavigateAddCrianca();
                                }
                                else
                                {
                                    app._crianca = app._usuario.criancas[app._usuario.criancas.Count - 1];
                                    NavigationService.NavigateHome();
                                }
                            }
                        }
                    }
                }
            } else{
                await MessageService.AlertDialog("Você precisa estar conectado à internet para fazer login no aplicativo.");
            }
            
        }

    }
}
