using Acr.UserDialogs;
using Plugin.Connectivity;
using ProMama.Models;
using ProMama.ViewModels.Services;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModels.Inicio
{
    class CadastroViewModel : ViewModelBase
    {
        private Aplicativo app = Aplicativo.Instance;

        public string Email { get; set; }

        public string Senha { get; set; }

        public string SenhaConfirmacao { get; set; }

        private List<Bairro> _bairros;
        public List<Bairro> Bairros
        {
            get
            {
                return _bairros;
            }
            set
            {
                _bairros = value;
                Notify("Bairros");
            }
        }

        public Bairro BairroSelecionado { get; set; }

        public ICommand CadastroCommand { get; set; }

        private readonly INavigationService NavigationService;
        private readonly IMessageService MessageService;
        private readonly IRestService RestService;

        private bool CadastroClicado = false;

        public CadastroViewModel()
        {
            CadastroCommand = new Command(Cadastro);

            NavigationService = DependencyService.Get<INavigationService>();
            MessageService = DependencyService.Get<IMessageService>();
            RestService = DependencyService.Get<IRestService>();

            if (CrossConnectivity.Current.IsConnected)
            {
                BairrosRead();
            }
        }

        private async void Cadastro()
        {   
            if (CrossConnectivity.Current.IsConnected)
            {
                if (!CadastroClicado)
                {
                    CadastroClicado = true;

                    if (!Senha.Equals(SenhaConfirmacao))
                    {
                        await MessageService.AlertDialog("As senhas não são iguais.");
                        CadastroClicado = false;
                    }
                    else if (Email.Equals(string.Empty) || Senha.Equals(string.Empty) || SenhaConfirmacao.Equals(string.Empty))
                    {
                        await MessageService.AlertDialog("Nenhum campo pode estar vazio.");
                        CadastroClicado = false;
                    }
                    else if (BairroSelecionado == null)
                    {
                        await MessageService.AlertDialog("Você precisa selecionar um bairro.");
                        CadastroClicado = false;
                    }
                    else
                    {
                        Debug.WriteLine(BairroSelecionado.bairro_id);
                        var u = new Usuario(Email, Senha, BairroSelecionado.bairro_id);
                        var result = await RestService.UsuarioCreate(u);

                        if (!result.success)
                        {
                            await MessageService.AlertDialog(result.message);
                            CadastroClicado = false;
                        }
                        else
                        {
                            using (UserDialogs.Instance.Loading("Por favor, aguarde...", null, null, true, MaskType.Black))
                            {
                                u.id = result.id;
                                u.api_token = result.message;
                                u.posto_saude = -1;

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

                            NavigationService.NavigateAddCrianca();
                        }
                    }
                }
            } else
            {
                await MessageService.AlertDialog("Você precisa estar conectado à internet para poder realizar o cadastro no aplicativo.");
            }
        }

        private async void BairrosRead()
        {
            Bairros = await RestService.BairrosRead();
        }
    }
}
