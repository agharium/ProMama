using Acr.UserDialogs;
using Plugin.Connectivity;
using ProMama.Models;
using ProMama.ViewModels.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModels.Home.Paginas
{
    class PerfilMaeEditViewModel : ViewModelBase
    {
        private Aplicativo app = Aplicativo.Instance;

        private string _nome;
        public string Nome
        {
            get
            {
                return _nome;
            }
            set
            {
                _nome = value;
                Notify("Nome");
            }
        }

        private DateTime _dataMaxima;
        public DateTime DataMaxima
        {
            get
            {
                return _dataMaxima;
            }
            set
            {
                _dataMaxima = value;
                Notify("DataMaxima");
            }
        }

        private DateTime _dataMinima;
        public DateTime DataMinima
        {
            get
            {
                return _dataMinima;
            }
            set
            {
                _dataMinima = value;
                Notify("DataMinima");
            }
        }

        private DateTime _dataSelecionada;
        public DateTime DataSelecionada
        {
            get
            {
                return _dataSelecionada;
            }
            set
            {
                _dataSelecionada = value;
                Notify("DataSelecionada");
            }
        }

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

        private int _bairroSelecionado;
        public int BairroSelecionado
        {
            get
            {
                return _bairroSelecionado;
            }
            set
            {
                _bairroSelecionado = value;
                Notify("BairroSelecionado");
            }
        }

        private List<Posto> _postos;
        public List<Posto> Postos
        {
            get
            {
                return _postos;
            }
            set
            {
                _postos = value;
                Notify("Postos");
            }
        }

        private int _postoSelecionado;
        public int PostoSelecionado
        {
            get
            {
                return _postoSelecionado;
            }
            set
            {
                _postoSelecionado = value;
                Notify("PostoSelecionado");
            }
        }

        private INavigation Navigation { get; set; }
        public ICommand SalvarCommand { get; set; }
        public ICommand TrocarSenhaCommand { get; set; }

        private readonly INavigationService NavigationService;
        private readonly IMessageService MessageService;
        private readonly IRestService RestService;

        public PerfilMaeEditViewModel(INavigation _navigation)
        {
            Navigation = _navigation;
            SalvarCommand = new Command(Salvar);
            TrocarSenhaCommand = new Command(TrocarSenha);

            Nome = app._usuario.name;

            DataMinima = DateTime.Now.AddYears(-100);
            DataMaxima = DateTime.Now.AddYears(-15);
            var aux = DateTime.Now.Year - app._usuario.data_nascimento.Year;
            DataSelecionada = (aux < 15 || aux > 100) ? DateTime.Now : app._usuario.data_nascimento;

            Bairros = App.BairroDatabase.GetAll();
            foreach (var b in Bairros)
            {
                if (b.bairro_id == app._usuario.bairro)
                {
                    BairroSelecionado = Bairros.IndexOf(b);
                    break;
                }
            }

            Postos = App.PostoDatabase.GetAll();
            if (app._usuario.posto_saude == -1)
            {
                PostoSelecionado = -1;
            } else
            {
                foreach (var p in Postos)
                {
                    if (p.id == app._usuario.posto_saude)
                    {
                        PostoSelecionado = Postos.IndexOf(p);
                        break;
                    }
                }
            }

            NavigationService = DependencyService.Get<INavigationService>();
            MessageService = DependencyService.Get<IMessageService>();
            RestService = DependencyService.Get<IRestService>();
        }

        private async void Salvar()
        {
            Debug.WriteLine("Bairro: " + Bairros[BairroSelecionado].bairro_id);
            Debug.WriteLine("Posto: " + Postos[PostoSelecionado].id);
            IProgressDialog LoadingDialog = UserDialogs.Instance.Loading("Por favor, aguarde...", null, null, true, MaskType.Black);

            if (CrossConnectivity.Current.IsConnected)
            {
                if (DataSelecionada.Year == DateTime.Now.Year || DataSelecionada.AddYears(15).Year > DateTime.Now.Year)
                {
                    LoadingDialog.Hide();
                    await MessageService.AlertDialog("Selecione uma data válida.");
                } else
                {
                    Usuario u = new Usuario();

                    u.id = app._usuario.id;
                    u.email = app._usuario.email;
                    u.password = app._usuario.password;
                    u.api_token = app._usuario.api_token;
                    u.criancas = app._usuario.criancas;

                    u.name = string.IsNullOrEmpty(Nome) ? "" : Nome;
                    u.data_nascimento = DataSelecionada;
                    u.bairro = Bairros[BairroSelecionado].bairro_id;
                    if (PostoSelecionado != -1)
                    {
                        u.posto_saude = Postos[PostoSelecionado].id;
                    } else
                    {
                        u.posto_saude = -1;
                    }

                    var result = await RestService.UsuarioUpdate(u);
                    if (result.success)
                    {
                        app._usuario = u;
                        App.UsuarioDatabase.Save(u);
                        app._master.Load();

                        LoadingDialog.Hide();
                        await Navigation.PopAsync();
                    } else
                    {
                        LoadingDialog.Hide();
                        await MessageService.AlertDialog("Ocorreu um erro. Tente novamente mais tarde.");
                    }
                }
            } else
            {
                LoadingDialog.Hide();
                await MessageService.AlertDialog("Você precisa estar conectado à internet para atualizar o perfil da mãe.");
            }
        }

        private async void TrocarSenha()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                await NavigationService.NavigateTrocarSenha(Navigation);
            }
            else
            {
                await MessageService.AlertDialog("Você precisa estar conectado à internet para trocar a senha.");
            }
        }
    }
}
