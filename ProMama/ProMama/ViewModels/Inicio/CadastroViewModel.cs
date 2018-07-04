using Acr.UserDialogs;
using Plugin.Connectivity;
using ProMama.Components;
using ProMama.Components.Cryptography;
using ProMama.Models;
using ProMama.ViewModels.Services;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
                    else if (Senha.Length < 8)
                    {
                        await MessageService.AlertDialog("A senha precisa ter no mínimo 8 caracteres.");
                        CadastroClicado = false;
                    }
                    else if (!Ferramentas.VerificarEmailRegex(Email))
                    {
                        await MessageService.AlertDialog("E-mail inválido.");
                        CadastroClicado = false;
                    }
                    else
                    {
                        var u = new Usuario(Email, PasswordHash.CreateHash(Senha), BairroSelecionado.bairro_id);
                        var result = await RestService.UsuarioCreate(u);

                        if (!result.success)
                        {
                            await MessageService.AlertDialog(result.message);
                            CadastroClicado = false;
                        } else
                        {
                            using (UserDialogs.Instance.Loading("Por favor, aguarde...", null, null, true, MaskType.Black))
                            {
                                u.id = result.id;
                                u.api_token = result.message;
                                u.posto_saude = -1;
                                u.criancas = new List<Crianca>();

                                app._usuario = u;
                                App.UsuarioDatabase.Save(app._usuario);

                                await Ferramentas.SincronizarBanco();
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
