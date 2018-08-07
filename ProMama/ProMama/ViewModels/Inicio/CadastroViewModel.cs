using Acr.UserDialogs;
using Plugin.Connectivity;
using ProMama.Components;
using ProMama.Components.Cryptography;
using ProMama.Models;
using ProMama.ViewModels.Services;
using System;
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
        public ICommand AcessarTermosDeUsoCommand { get; set; }

        private readonly INavigationService NavigationService;
        private readonly IMessageService MessageService;
        private readonly IRestService RestService;

        public CadastroViewModel()
        {
            CadastroCommand = new Command(Cadastro);
            AcessarTermosDeUsoCommand = new Command(AcessarTermosDeUso);

            NavigationService = DependencyService.Get<INavigationService>();
            MessageService = DependencyService.Get<IMessageService>();
            RestService = DependencyService.Get<IRestService>();

            if (CrossConnectivity.Current.IsConnected)
            {
                var _bairros = App.BairroDatabase.GetAll();
                if (_bairros.Count == 0)
                {
                    BairrosRead();
                } else
                {
                    Bairros = _bairros;
                }
            }
        }

        private async void Cadastro()
        {
            IProgressDialog LoadingDialog = UserDialogs.Instance.Loading("Por favor, aguarde...", null, null, true, MaskType.Black);
            if (CrossConnectivity.Current.IsConnected)
            {
                if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Senha) || string.IsNullOrEmpty(SenhaConfirmacao))
                {
                    LoadingDialog.Hide();
                    await MessageService.AlertDialog("Nenhum campo pode estar vazio.");
                }
                else if (!Senha.Equals(SenhaConfirmacao))
                {
                    LoadingDialog.Hide();
                    await MessageService.AlertDialog("As senhas não são iguais.");
                }
                else if (BairroSelecionado == null)
                {
                    LoadingDialog.Hide();
                    await MessageService.AlertDialog("Você precisa selecionar um bairro.");
                }
                else if (Senha.Length < 6)
                {
                    LoadingDialog.Hide();
                    await MessageService.AlertDialog("A senha precisa ter no mínimo 6 caracteres.");
                }
                else if (!Ferramentas.ValidarEmailRegex(Email))
                {
                    LoadingDialog.Hide();
                    await MessageService.AlertDialog("E-mail inválido.");
                }
                else
                {
                    var u = new Usuario(Email, PasswordHash.CreateHash(Senha), BairroSelecionado.bairro_id);
                    var result = await RestService.UsuarioCreate(u);

                    if (!result.success)
                    {
                        LoadingDialog.Hide();
                        await MessageService.AlertDialog(result.message);
                    } else
                    {
                        u.id = result.id;
                        u.api_token = result.message;
                        u.notificacoes_oQuantoAntes = new List<int>();

                        app._usuario = u;
                        App.UsuarioDatabase.Save(app._usuario);

                        await Ferramentas.SincronizarBanco();

                        LoadingDialog.Hide();
                        NavigationService.NavigateAddCrianca();
                    }
                }
            } else
            {
                LoadingDialog.Hide();
                await MessageService.AlertDialog("Você precisa estar conectado à internet para poder realizar o cadastro no aplicativo.");
            }
        }

        private async void BairrosRead()
        {
            var syncAux = await RestService.SincronizacaoBairroRead();
            var syncBairro = syncAux.id;

            if (app._sync == null)
                app._sync = new Sincronizacao(1);

            if (app._sync.bairro != syncBairro)
            {
                App.BairroDatabase.WipeTable();
                App.BairroDatabase.SaveList(await RestService.BairrosRead());
                Bairros = App.BairroDatabase.GetAll();
            }

            app._sync.bairro = syncBairro;
        }

        private void AcessarTermosDeUso()
        {
            try
            {
                Device.OpenUri(new Uri("http://saude.osorio.rs.gov.br:7083/termos-de-uso"));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
    }
}
