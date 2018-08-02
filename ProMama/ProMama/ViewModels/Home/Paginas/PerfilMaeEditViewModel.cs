using Acr.UserDialogs;
using Plugin.Connectivity;
using ProMama.Components;
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

        private Usuario u = new Usuario();

        private string _fotoString { get; set; }
        private ImageSource _foto;
        public ImageSource Foto
        {
            get
            {
                return _foto;
            }
            set
            {
                _foto = value;
                Notify("Foto");
            }
        }

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

        private int _bairroSelecionadoIndex;
        public int BairroSelecionadoIndex
        {
            get
            {
                return _bairroSelecionadoIndex;
            }
            set
            {
                _bairroSelecionadoIndex = value;
                Notify("BairroSelecionadoIndex");
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

        private int _postoSelecionadoIndex;
        public int PostoSelecionadoIndex
        {
            get
            {
                return _postoSelecionadoIndex;
            }
            set
            {
                _postoSelecionadoIndex = value;
                Notify("PostoSelecionadoIndex");
            }
        }

        private INavigation Navigation { get; set; }
        public ICommand SalvarCommand { get; set; }
        public ICommand TrocarFotoCommand { get; set; }
        public ICommand TrocarEmailCommand { get; set; }
        public ICommand TrocarSenhaCommand { get; set; }

        private readonly INavigationService NavigationService;
        private readonly IMessageService MessageService;
        private readonly IRestService RestService;

        public PerfilMaeEditViewModel(INavigation _navigation)
        {
            Navigation = _navigation;
            SalvarCommand = new Command(Salvar);
            TrocarFotoCommand = new Command(TrocarFoto);
            TrocarEmailCommand = new Command(TrocarEmail);
            TrocarSenhaCommand = new Command(TrocarSenha);
            
            _fotoString = string.IsNullOrEmpty(app._usuario.foto_caminho) ? "mother_default.jpeg" : app._usuario.foto_caminho;
            Foto = string.IsNullOrEmpty(app._usuario.foto_caminho) ? "mother_default.jpeg" : app._usuario.foto_caminho;
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
                    BairroSelecionadoIndex = Bairros.IndexOf(b);
                    break;
                }
            }

            Postos = App.PostoDatabase.GetAll();
            if (app._usuario.posto_saude == -1)
            {
                PostoSelecionadoIndex = -1;
            } else
            {
                foreach (var p in Postos)
                {
                    if (p.id == app._usuario.posto_saude)
                    {
                        PostoSelecionadoIndex = Postos.IndexOf(p);
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
            Debug.WriteLine("Bairro: " + Bairros[BairroSelecionadoIndex].bairro_id);
            Debug.WriteLine("Posto: " + Postos[PostoSelecionadoIndex].id);
            IProgressDialog LoadingDialog = UserDialogs.Instance.Loading("Por favor, aguarde...", null, null, true, MaskType.Black);
            
            if (DataSelecionada.Year == DateTime.Now.Year || DataSelecionada.AddYears(15).Year > DateTime.Now.Year)
            {
                LoadingDialog.Hide();
                await MessageService.AlertDialog("Selecione uma data válida.");
            } else if (!string.IsNullOrEmpty(Nome) && Nome.Length < 2)
            {
                LoadingDialog.Hide();
                await MessageService.AlertDialog("O nome deve ter no mínimo 2 caracteres.");
            } else
            {
                u.id = app._usuario.id;
                u.email = app._usuario.email;
                u.password = app._usuario.password;
                u.api_token = app._usuario.api_token;
                u.criancas = app._usuario.criancas;
                u.uploaded = false;

                u.name = string.IsNullOrEmpty(Nome) ? "" : Nome;
                u.data_nascimento = DataSelecionada;
                u.bairro = Bairros[BairroSelecionadoIndex].bairro_id;
                if (PostoSelecionadoIndex != -1)
                {
                    u.posto_saude = Postos[PostoSelecionadoIndex].id;
                } else
                {
                    u.posto_saude = -1;
                }
                
                if (app._usuario.foto_caminho != u.foto_caminho)
                {
                    u.foto_uploaded = false;
                }
                
                app._usuario = u;
                App.UsuarioDatabase.Save(u);
                app._master.Load();

                Ferramentas.UploadThread();

                LoadingDialog.Hide();
                await Navigation.PopAsync();
            }
        }

        private async void TrocarFoto()
        {
            int escolha = await Ferramentas.FotoActionSheet(_fotoString.Equals("mother_default.jpeg") ? 1 : 2);
            if (escolha == 1 || escolha == 2)
            {
                var foto = await Ferramentas.SelecionarFoto(new Foto(), escolha);
                if (foto != null)
                {
                    _fotoString = foto.caminho;
                    Foto = foto.source;
                    u.foto_caminho = foto.caminho;
                    Notify("Foto");
                }
            }
            else if (escolha == 3)
            {
                if (await MessageService.ConfirmationDialog("Você tem certeza que deseja excluir esta foto?", "Sim", "Não"))
                {
                    _fotoString = "mother_default.jpeg";
                    Foto = "mother_default.jpeg";
                    u.foto_caminho = null;
                    u.foto_uploaded = true;
                    u.foto_url = null;
                }
            }
        }

        private async void TrocarEmail()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                await NavigationService.NavigateTrocarEmail(Navigation);
            }
            else
            {
                await MessageService.AlertDialog("Você precisa estar conectado à internet para trocar o e-mail.");
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
