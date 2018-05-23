using ImageCircle.Forms.Plugin.Abstractions;
using ProMama.Model;
using ProMama.ViewModel.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModel.Home
{
    class HomeDetailViewModel : ViewModelBase
    {
        private Aplicativo app = Aplicativo.Instance;

        // Criança
        public Crianca Crianca  { get; set; }
        public string Nome      { get; set; }

        // Foto da Criança
        private ImageSource _foto { get; set; }
        public  ImageSource Foto
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

        private int _idadeAux;
        public  int IdadeAux
        {
            get { return _idadeAux; }
            set
            {
                _idadeAux = value;
                IdadeExtenso = IdadesExtensoLista[value];
                OrganizaSetas();
                OrganizaInformacoes();
            }
        }

        private string _idadeExtenso;
        public  string IdadeExtenso
        {
            get { return _idadeExtenso; }
            set
            {
                _idadeExtenso = value;
                Notify("IdadeExtenso");
            }
        }

        // Variáveis auxiliares da interface
        private string _indicadorLoading;
        public  string IndicadorLoading
        {
            get
            {
                return _indicadorLoading;
            }
            set
            {
                _indicadorLoading = value;
                Notify("IndicadorLoading");
            }
        }

        private string _setaEsquerdaCor;
        public  string SetaEsquerdaCor
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
        public  string SetaDireitaCor
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
        public  ObservableCollection<Informacao> Informacoes { get; set; }

        private List<Informacao> InformacoesAux = new List<Informacao>();

        // Picker
        public  List<string> IdadesPickerLista { get; set; }

        // Commands
        public ICommand MenosIdadeCommand   { get; set; }
        public ICommand MaisIdadeCommand    { get; set; }
        public ICommand IdadePickerCommand  { get; set; }
        public ICommand InformacaoCommand   { get; set; }
        public ICommand FotoCommand         { get; set; }

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
            App.ConfigDatabase.SaveConfig(cfg);

            // Informações
            RestService = DependencyService.Get<IRestService>();
            Informacoes = new ObservableCollection<Informacao>();
            InformacaoRead();

            // Lista de idades por extenso
            IdadesExtensoLista = new List<string>() {
                // TO-DO: Crianca.crianca_sexo == 0 ? "recém-nascida"  : "recém-nascido",
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
            Crianca = app._crianca;
            Nome = Crianca.crianca_primeiro_nome;
            Foto = Crianca.Foto == null ? "avatar_default.png" : Crianca.Foto; 
            IdadeAux = IdadesExtensoLista.IndexOf(Crianca.IdadeExtenso);
            IdadeExtenso = IdadesExtensoLista[IdadeAux];

            // Display das setas
            OrganizaSetas();

            // Idades picker
            IdadesPickerLista = new List<string>();
            for (int i = 0; i <= IdadesExtensoLista.IndexOf(Crianca.IdadeExtenso); i++)
            {
                IdadesPickerLista.Add(IdadesExtensoLista[i]);
            }

            // Commands
            MenosIdadeCommand   = new Command(MenosIdade);
            MaisIdadeCommand    = new Command(MaisIdade);
            IdadePickerCommand  = new Command<Picker>(IdadePicker);
            InformacaoCommand   = new Command<Informacao>(NavigateInformacao);
            FotoCommand         = new Command<CircleImage>(NavigateFoto);

            // Navigation
            Navigation = _navigation;
            NavigationService = DependencyService.Get<INavigationService>();
        }

        // Botão da seta pra direita
        private void MaisIdade()
        {
            if (IdadeAux < 27 && IdadeAux < Crianca.IdadeMeses + 2 && IdadesExtensoLista.IndexOf(Crianca.IdadeExtenso) != 0)
            {
                IdadeAux++;
            }
        }

        // Botão da seta pra esquerda
        private void MenosIdade()
        {
            if (IdadeAux > 0 && IdadesExtensoLista.IndexOf(Crianca.IdadeExtenso) != 0)
            {
                IdadeAux--;
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
            if (IdadesExtensoLista.IndexOf(Crianca.IdadeExtenso) == 0)
            {
                SetaEsquerdaCor = "#FF8A80";
                SetaDireitaCor = "#FF8A80";
            } else
            {
                if (IdadeAux == 0)
                {
                    SetaEsquerdaCor = "#FF8A80";
                    SetaDireitaCor = "#EEEEEE";
                }
                else if (IdadeAux == 27 || IdadeAux == IdadesExtensoLista.IndexOf(Crianca.IdadeExtenso))
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
            foreach (var informacao in InformacoesAux)
            {
                // se a idade da criança é compatível com a informação
                if (informacao.informacao_idadeSemanasInicio <= IdadeAux && informacao.informacao_idadeSemanasFim >= IdadeAux)
                {
                    if (!Informacoes.Contains(informacao))
                    {
                        Informacoes.Add(informacao);
                    }
                }
                else
                {
                    if (Informacoes.Contains(informacao))
                    {
                        Informacoes.Remove(informacao);
                    }
                }
            }
        }
        
        // Abre pagina de informação
        private async void NavigateInformacao(Informacao informacao)
        {
            await NavigationService.NavigateInformacao(Navigation, informacao);
        }

        private async void NavigateFoto(CircleImage foto)
        {
            Imagem imagem = new Imagem(-1, "Visualização", foto.Source);
            await NavigationService.NavigateImagem(Navigation, imagem);
        }

        private void InformacaoRead()
        {
            IndicadorLoading = "True";

            var count = 0;
            var informacoes = App.InformacaoDatabase.GetAllInformacao();
            foreach (var i in informacoes)
            {
                i.informacao_imagem = count % 2 == 0 ? "baby.jpeg" : null;
                i.informacao_imagem_altura = i.informacao_imagem == null ? 0 : 150;
                i.informacao_resumo = Regex.Match(i.informacao_corpo, @"^(\w+\b.*?){20}").ToString() + "...";
                InformacoesAux.Add(i);
                count++;
            }
            OrganizaInformacoes();

            IndicadorLoading = "False";
        }
    }
}
