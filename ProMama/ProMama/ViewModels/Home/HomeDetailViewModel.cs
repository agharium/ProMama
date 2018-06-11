using Plugin.Connectivity;
using ProMama.Models;
using ProMama.ViewModels.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModels.Home
{
    class HomeDetailViewModel : ViewModelBase
    {
        private Aplicativo app = Aplicativo.Instance;

        // Criança
        public string Nome { get; set; }

        // Foto da Criança
        private ImageSource _foto { get; set; }
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

        // Variavéis auxiliares para controle da timeline
        private List<string> IdadesExtensoLista { get; set; }
        private List<double> IdadesAuxLista { get; set; }

        private int _idadeAuxIndex;
        public int IdadeAuxIndex
        {
            get
            {
                return _idadeAuxIndex;
            }
            set
            {
                _idadeAuxIndex = value;
                IdadeExtenso = IdadesExtensoLista[value];
                OrganizaSetas();
                OrganizaInformacoes();
                DefineFoto();
            }
        }

        private string _idadeExtenso;
        public string IdadeExtenso
        {
            get { return _idadeExtenso; }
            set
            {
                _idadeExtenso = value;
                Notify("IdadeExtenso");
            }
        }

        private bool _atualizandoInformacoes;
        public bool AtualizandoInformacoes
        {
            get
            {
                return _atualizandoInformacoes;
            }
            set
            {
                _atualizandoInformacoes = value;
                Notify("AtualizandoInformacoes");
            }
        }

        private string _setaEsquerdaCor;
        public string SetaEsquerdaCor
        {
            get
            {
                return _setaEsquerdaCor;
            }
            set
            {
                _setaEsquerdaCor = value;
                Notify("SetaEsquerdaCor");
            }
        }

        private string _setaDireitaCor;
        public string SetaDireitaCor
        {
            get
            {
                return _setaDireitaCor;
            }
            set
            {
                _setaDireitaCor = value;
                Notify("SetaDireitaCor");
            }
        }

        // Informações
        public ObservableCollection<Informacao> Informacoes { get; set; }

        private List<Informacao> InformacoesAux = new List<Informacao>();

        // Picker
        public List<string> IdadesPickerLista { get; set; }

        // Commands
        public ICommand MenosIdadeCommand { get; set; }
        public ICommand MaisIdadeCommand { get; set; }
        public ICommand IdadePickerCommand { get; set; }
        public ICommand AbrirInformacaoCommand { get; set; }
        public ICommand AtualizarInformacoesCommand { get; set; }
        public ICommand GaleriaCommand { get; set; }

        // Navigation
        private INavigation Navigation { get; set; }
        private readonly INavigationService NavigationService;

        // Rest
        private readonly IRestService RestService;

        // Construtor
        public HomeDetailViewModel(INavigation _navigation)
        {
            // Salva o login
            Config cfg = new Config(app._usuario, app._crianca);
            App.ConfigDatabase.Save(cfg);

            // Informações
            RestService = DependencyService.Get<IRestService>();
            Informacoes = new ObservableCollection<Informacao>();
            InformacoesRead();

            // Lista auxiliar de idades
            DefineListaIdadesAux();

            // Lista de idades por extenso
            IdadesExtensoLista = new List<string>() {
                "recém-nascido",
                "1 semana",
                "2 semanas",
                "3 semanas",
                "1 mês",
                "2 meses",
                "3 meses",
                "4 meses",
                "5 meses",
                "6 meses",
                "7 meses",
                "8 meses",
                "9 meses",
                "10 meses",
                "11 meses",
                "1 ano",
                "1 ano e 1 mês",
                "1 ano e 2 meses",
                "1 ano e 3 meses",
                "1 ano e 4 meses",
                "1 ano e 5 meses",
                "1 ano e 6 meses",
                "1 ano e 7 meses",
                "1 ano e 8 meses",
                "1 ano e 9 meses",
                "1 ano e 10 meses",
                "1 ano e 11 meses",
                "2 anos"
            };

            // Criança
            Nome = app._crianca.crianca_primeiro_nome;
            IdadeAuxIndex = IdadesExtensoLista.IndexOf(app._crianca.DefineIdadeExtenso());
            // bug-proof
            /*if (SetaDireitaCor.Equals("#EEEEEE"))
                IdadeAuxIndex--;*/

            // Idades picker
            IdadesPickerLista = new List<string>();
            for (int i = 0; i <= IdadesExtensoLista.IndexOf(app._crianca.IdadeExtenso); i++)
            {
                IdadesPickerLista.Add(IdadesExtensoLista[i]);
            }

            // Commands
            MenosIdadeCommand = new Command(MenosIdade);
            MaisIdadeCommand = new Command(MaisIdade);
            IdadePickerCommand = new Command<Picker>(IdadePicker);
            AbrirInformacaoCommand = new Command<Informacao>(AbrirInformacao);
            AtualizarInformacoesCommand = new Command(AtualizaInformacoes);
            GaleriaCommand = new Command(Galeria);

            // Navigation
            Navigation = _navigation;
            NavigationService = DependencyService.Get<INavigationService>();
        }

        // Atualiza informacoes com o banco de dados
        private async void AtualizaInformacoes()
        {
            AtualizandoInformacoes = true;
            if (CrossConnectivity.Current.IsConnected)
            {
                var syncAux = await RestService.SincronizacaoRead(app._usuario.api_token);

                if (app._sync == null)
                    app._sync = new Sincronizacao(1);

                if (app._sync.informacao != syncAux.informacao)
                {
                    App.InformacaoDatabase.WipeTable();
                    App.InformacaoDatabase.SaveList(await RestService.InformacoesRead(app._usuario.api_token));
                }

                app._sync = syncAux;
                App.SincronizacaoDatabase.Save(app._sync);

                InformacoesRead();
            }
            AtualizandoInformacoes = false;
        }

        // Botão da seta pra direita
        private void MaisIdade()
        {
            if (IdadeAuxIndex < 27 && IdadeAuxIndex < app._crianca.IdadeMeses + 2 && IdadesExtensoLista.IndexOf(app._crianca.IdadeExtenso) != 0)
            {
                IdadeAuxIndex++;
            }
        }

        // Botão da seta pra esquerda
        private void MenosIdade()
        {
            if (IdadeAuxIndex > 0 && IdadesExtensoLista.IndexOf(app._crianca.IdadeExtenso) != 0)
            {
                IdadeAuxIndex--;
            }
        }

        // Mostra picker de idades
        private void IdadePicker(Picker p)
        {
            p.Focus();
        }

        // Organiza o display as setas
        private void OrganizaSetas()
        {
            if (IdadesExtensoLista.IndexOf(app._crianca.IdadeExtenso) == 0)
            {
                SetaEsquerdaCor = "#FF8A80";
                SetaDireitaCor = "#FF8A80";
            } else
            {
                if (IdadeAuxIndex == 0)
                {
                    SetaEsquerdaCor = "#FF8A80";
                    SetaDireitaCor = "#EEEEEE";
                }
                else if (IdadeAuxIndex == 27 || IdadeAuxIndex == IdadesExtensoLista.IndexOf(app._crianca.IdadeExtenso))
                {
                    SetaEsquerdaCor = "#EEEEEE";
                    SetaDireitaCor = "#FF8A80";
                }
                else
                {
                    SetaEsquerdaCor = "#EEEEEE";
                    SetaDireitaCor = "#EEEEEE";
                }
            }
        }

        // Organiza as informações mostradas na tela de acordo com a idade que o usuário escolhe ao interagir com as setas
        private void OrganizaInformacoes()
        {
            foreach (var info in InformacoesAux)
            {
                var inicio = info.informacao_idadeSemanasInicio;
                var fim = info.informacao_idadeSemanasFim;

                if (((IdadesAuxLista[IdadeAuxIndex] >= inicio && IdadesAuxLista[IdadeAuxIndex] <= fim) ||
                    (IdadeAuxIndex == 0 && inicio < 1 && IdadesAuxLista[IdadeAuxIndex] <= fim)) &&
                    app._crianca.IdadeSemanas >= inicio)
                {
                    AdicionarInfo(info);
                } else
                {
                    RemoverInfo(info);
                }
            }
        }
        
        // Abre pagina de informação
        private async void AbrirInformacao(Informacao informacao)
        {
            await NavigationService.NavigateInformacao(Navigation, informacao);
        }

        private void Galeria()
        {
            app._home.Detail_Galeria();
        }

        private void InformacoesRead()
        {
            var count = 0;
            var informacoes = App.InformacaoDatabase.GetAll();
            foreach (var i in informacoes)
            {
                i.informacao_imagem_visivel = !String.IsNullOrEmpty(i.informacao_foto);
                i.informacao_resumo = String.Join(" ", i.informacao_corpo.Split().Take(20).ToArray());
                i.informacao_resumo.Remove(i.informacao_resumo.Length - 1, 1);
                i.informacao_resumo += "...";
                InformacoesAux.Add(i);
                count++;
            }
        }

        private void AdicionarInfo(Informacao info)
        {
            if (!Informacoes.Contains(info))
                Informacoes.Add(info);
        }

        private void RemoverInfo(Informacao info)
        {
            if (Informacoes.Contains(info))
                Informacoes.Remove(info);
        }

        private void DefineListaIdadesAux()
        {
            var semana = 0.1551871428571429;
            IdadesAuxLista = new List<double>()
            {
                0,
                semana * 7,
                semana * 14,
                semana * 21,
            };

            for (var i=28; i<=672; i+=28)
            {
                IdadesAuxLista.Add(semana * i);
            }
        }

        private int DefineIdadeAuxIndex()
        {
            if (app._crianca.IdadeExtenso.Equals("2 anos"))
                return IdadesAuxLista.Count() - 1;

            var idadeSemanas = app._crianca.IdadeSemanas;

            for (var i=0; i<IdadesAuxLista.Count()-1; i++)
            {
                if (IdadesAuxLista[i] <= idadeSemanas && IdadesAuxLista[i+1] > idadeSemanas)
                {
                    return i;
                }
            }

            return 0;
        }

        private void DefineFoto()
        {
            var list = App.FotoDatabase.FindByChildId(app._crianca.crianca_id);
            if (list.Count() == 0)
            {
                Foto = "avatar_default.png";
                return;
            }

            if (IdadeAuxIndex < 4)
            {
                foreach (var f in list)
                {
                    if (f.mes == 0)
                    {
                        Foto = f.caminho;
                        return;
                    }
                }
                Foto = "avatar_default.png";
            } else
            {
                foreach (var f in list)
                {
                    if (f.mes == IdadeAuxIndex - 3)
                    {
                        Foto = f.caminho;
                        return;
                    }
                }
                Foto = "avatar_default.png";
            }
        }
    }
}
