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

        private int _idadeAux;
        public int IdadeAux
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
        private ObservableCollection<Informacao> _informacoes;
        public  ObservableCollection<Informacao> Informacoes
        {
            get { return _informacoes; }
            set { _informacoes = value; }

        }

        private List<Informacao> InformacoesAux = new List<Informacao>();

        // Picker
        private List<string> _idadesPickerLista;
        public  List<string> IdadesPickerLista
        {
            get { return _idadesPickerLista; }
            set { _idadesPickerLista = value; }
        }

        // Commands
        public ICommand MenosIdadeCommand   { get; set; }
        public ICommand MaisIdadeCommand    { get; set; }
        public ICommand IdadePickerCommand  { get; set; }
        public ICommand InfoPageCommand     { get; set; }
        public ICommand FotoPageCommand     { get; set; }

        // Navigation
        private INavigation Navigation { get; set; }
        private readonly INavigationService _navigationService;

        // Rest
        private readonly IRestService _restService;

        // Construtor
        public HomeDetailViewModel(INavigation Navigation)
        {
            Config cfg = new Config(app._usuario, app._crianca);
            App.ConfigDatabase.SaveConfig(cfg);

            /*app._crianca.IdadeSemanas = (DateTime.Now - app._crianca.crianca_dataNascimento).Days / 7;
            app._crianca.IdadeMeses = app._crianca.IdadeSemanas / 4;*/

            // Informações
            _restService = DependencyService.Get<IRestService>();
            Informacoes = new ObservableCollection<Informacao>();

            IndicadorLoading = "True";
            InformacaoGet();
            IndicadorLoading = "False";

            /*Informacao teste1 = new Informacao(
                0,
                8,
                "Informação nº 1",
                "Fusce sagittis non ante nec tristique. Nullam at turpis et augue interdum dictum non sit amet diam. Aenean sit amet mauris tortor. Sed dui est, luctus egestas elit vitae, sodales mollis ex. Aliquam a tortor ipsum. Praesent ac condimentum dolor, in porta tellus. Vivamus viverra ac dolor ac mattis. Vestibulum a hendrerit orci. Etiam tempus libero ut vestibulum interdum. Vestibulum viverra imperdiet consequat." + Environment.NewLine +
                    "Proin dignissim posuere tincidunt. Fusce a libero id enim fermentum aliquam. Donec ligula lacus, posuere sed tempor ut, rutrum at elit. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Praesent porttitor hendrerit lacinia. Aenean mattis dapibus viverra. Morbi mollis eget leo quis egestas .Nulla facilisi.Mauris elementum urna a lacus volutpat, in rutrum augue iaculis. Mauris quis dui sit amet ligula tincidunt rhoncus cursus in eros." + Environment.NewLine +
                    "Integer a dapibus dolor. Vivamus venenatis sem est, a feugiat erat volutpat id. Vestibulum sit amet purus lectus.Nullam velit elit, mollis sed cursus a, sagittis non ex. Proin accumsan augue nec est bibendum semper. Curabitur efficitur orci ut turpis tincidunt, at tempus diam vehicula.Sed placerat congue dolor ac aliquam.",
                "baby.jpeg");
            InformacoesAux.Add(teste1);*/

            // Lista de idades por extenso
            IdadesExtensoLista = new List<string>() { "recém-nascido", "1 semana", "2 semanas", "3 semanas", "1 mês" };
            for (int i = 2; i <= 11; i++) { IdadesExtensoLista.Add(i + " meses"); }
            IdadesExtensoLista.AddRange(new string[] { "1 ano", "1 ano e 1 mês" });
            for (int i = 2; i <= 11; i++) { IdadesExtensoLista.Add("1 ano e " + i + " meses"); }
            IdadesExtensoLista.Add("2 anos");

            // Criança
            Crianca = app._crianca;
            Nome = Crianca.crianca_primeiro_nome;
            Foto = Crianca.Foto == null ? "avatar_default.jpg" : Crianca.Foto; 
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
            InfoPageCommand     = new Command<Informacao>(InfoPage);
            FotoPageCommand     = new Command<CircleImage>(FotoPage);

            // Navigation
            this.Navigation = Navigation;
            _navigationService = DependencyService.Get<INavigationService>();
        }

        // Botão da seta pra direita
        private void MaisIdade()
        {
            if (IdadeAux < 27 && IdadeAux < Crianca.IdadeMeses + 2)
            {
                IdadeAux++;
            }
        }

        // Botão da seta pra esquerda
        private void MenosIdade()
        {
            if (IdadeAux > 0)
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
        private async void InfoPage(Informacao info)
        {
            await _navigationService.NavigateToInfoPage(Navigation, info);
        }

        private async void FotoPage(CircleImage foto)
        {
            Debug.WriteLine("CHEGOU");
            await _navigationService.NavigateToFotoPage(Navigation, foto.Source);
        }

        private async void InformacaoGet()
        {
            var count = 0;
            var infos = await _restService.InformacaoGet("token2");
            foreach (var i in infos)
            {
                i.informacao_imagem = count % 2 == 0 ? "baby.jpeg" : null;
                i.informacao_imagem_altura = i.informacao_imagem == null ? 0 : 150;
                i.informacao_resumo = Regex.Match(i.informacao_corpo, @"^(\w+\b.*?){20}").ToString() + "...";
                InformacoesAux.Add(i);
                count++;
            }
            OrganizaInformacoes();
        }
    }
}
