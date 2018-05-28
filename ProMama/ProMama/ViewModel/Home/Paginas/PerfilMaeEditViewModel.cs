using Plugin.Connectivity;
using ProMama.Model;
using ProMama.ViewModel.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModel.Home.Paginas
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

        private readonly IMessageService MessageService;
        private readonly IRestService RestService;

        public PerfilMaeEditViewModel(INavigation _navigation)
        {
            Navigation = _navigation;
            SalvarCommand = new Command(Salvar);

            Nome = app._usuario.name;

            DataMaxima = DateTime.Now;
            var aux = DateTime.Now.Year - app._usuario.data_nascimento.Year;
            DataSelecionada = (aux < 10 || aux > 100) ? DateTime.Now : app._usuario.data_nascimento;

            Bairros = App.BairroDatabase.GetAllBairro();
            foreach (var b in Bairros)
            {
                if (b.bairro_id == app._usuario.bairro)
                    BairroSelecionado = Bairros.IndexOf(b);
            }

            Postos = App.PostoDatabase.GetAllPosto();
            if (app._usuario.posto_saude == -1)
            {
                PostoSelecionado = -1;
            } else
            {
                foreach (var p in Postos)
                {
                    if (p.posto_id == app._usuario.posto_saude)
                        PostoSelecionado = Postos.IndexOf(p);
                }
            }

            MessageService = DependencyService.Get<IMessageService>();
            RestService = DependencyService.Get<IRestService>();
        }

        private async void Salvar()
        {
            Debug.WriteLine("Bairro: " + Bairros[BairroSelecionado].bairro_id);
            Debug.WriteLine("Posto: " + Postos[PostoSelecionado].posto_id);
            /*if (CrossConnectivity.Current.IsConnected)
            {
                if (DataSelecionada.Year == DateTime.Now.Year)
                {
                    await MessageService.AlertDialog("Selecione uma data válida.");
                } else
                {
                    Usuario u = new Usuario();

                    u.id = app._usuario.id;
                    u.email = app._usuario.email;
                    u.password = app._usuario.password;
                    u.api_token = app._usuario.api_token;
                    u.criancas = app._usuario.criancas;

                    u.name = Nome;
                    u.data_nascimento = DataSelecionada;
                    u.bairro = Bairros[BairroSelecionado].bairro_id;
                    if (PostoSelecionado != -1)
                    {
                        u.posto_saude = Postos[PostoSelecionado].posto_id;
                    } else
                    {
                        u.posto_saude = -1;
                    }

                    var result = await RestService.UsuarioUpdate(u);
                    if (result.success)
                    {
                        app._usuario = u;
                        App.UsuarioDatabase.SaveUsuario(u);
                        app._master.Load();
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await MessageService.AlertDialog("Ocorreu um erro. Tente novamente mais tarde.");
                    }
                }
            } else
            {
                await MessageService.AlertDialog("Você precisa estar conectado à internet para atualizar o perfil da mãe.");
            }*/
        }
    }
}
