using Acr.UserDialogs;
using Plugin.Connectivity;
using ProMama.Models;
using ProMama.ViewModels.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModels.Inicio
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
                                    App.UsuarioDatabase.Save(app._usuario);
                                    
                                    var syncAux = await RestService.SincronizacaoRead(app._usuario.api_token);

                                    if (app._sync == null)
                                        app._sync = new Sincronizacao(1);
                                    
                                    // Popula banco
                                    if (app._sync.bairro != syncAux.bairro)
                                    {
                                        App.BairroDatabase.WipeTable();
                                        App.BairroDatabase.SaveList(await RestService.BairrosRead());
                                    }
                                        
                                    if (app._sync.posto != syncAux.posto)
                                    {
                                        App.PostoDatabase.WipeTable();
                                        App.PostoDatabase.SaveList(await RestService.PostosRead(app._usuario.api_token));
                                    }
                                        
                                    if (app._sync.informacao != syncAux.informacao)
                                    {
                                        App.InformacaoDatabase.WipeTable();
                                        App.InformacaoDatabase.SaveList(await RestService.InformacoesRead(app._usuario.api_token));
                                    }

                                    if (app._sync.duvidas != syncAux.duvidas)
                                    {
                                        App.DuvidaDatabase.WipeTable();
                                        App.DuvidaDatabase.SaveList(await RestService.ConversasRead(app._usuario.api_token));
                                    }

                                    /*if (app._sync.notificacao != syncAux.notificacao)
                                    {
                                        App.NotificacaoDatabase.WipeTable();
                                        App.NotificacaoDatabase.SaveNotificacaoList(await RestService.NotificacoesRead(app._usuario.api_token));
                                    }*/

                                    app._sync = syncAux;
                                    App.SincronizacaoDatabase.Save(app._sync);
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
            } else {
                await MessageService.AlertDialog("Você precisa estar conectado à internet para fazer login no aplicativo.");
            }
            
        }

    }
}
