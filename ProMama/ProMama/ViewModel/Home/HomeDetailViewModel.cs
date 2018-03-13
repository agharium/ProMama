using ProMama.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModel.Home
{
    class HomeDetailViewModel : ViewModelBase
    {
        private Aplicativo app = Aplicativo.Instance;

        // Criança
        public Crianca Crianca { get; set; }
        public string Nome { get; set; }

        private int _idadeAux;
        public int IdadeAux
        {
            get { return _idadeAux; }
            set
            {
                _idadeAux = value;
                IdadeExtenso = DefineIdadeExtenso();
                OrganizaInformacoes();
            }
        }

        private string _idadeExtenso;
        public string IdadeExtenso
        {
            get { return _idadeExtenso; }
            set
            {
                _idadeExtenso = value;
                this.Notify("IdadeExtenso");
            }
        }

        // Informações
        private ObservableCollection<Informacao> _informacoes;
        public ObservableCollection<Informacao> Informacoes
        {
            get { return _informacoes; }
            set { _informacoes = value; }

        }

        private List<Informacao> InformacoesAux = new List<Informacao>();

        // Picker
        private List<string> _idadesPickerLista;
        public List<string> IdadesPickerLista
        {
            get { return _idadesPickerLista; }
            set { _idadesPickerLista = value; }
        }

        // Commands
        public ICommand MenosIdadeCommand { get; set; }
        public ICommand MaisIdadeCommand { get; set; }
        public ICommand IdadePickerCommand { get; set; }
        public ICommand InfoPageCommand { get; set; }

        // Navigation
        private INavigation Navigation { get; set; }
        private readonly Services.INavigationService _navigationService;

        // Construtor
        public HomeDetailViewModel(INavigation Navigation)
        {
            Config cfg = new Config(app._usuario, app._crianca);
            App.ConfigDatabase.SaveConfig(cfg);

            // Informações
            Informacoes = new ObservableCollection<Informacao>();

            Informacao teste1 = new Informacao(
                0,
                8,
                "Informação nº 1",
                "Fusce sagittis non ante nec tristique. Nullam at turpis et augue interdum dictum non sit amet diam. Aenean sit amet mauris tortor. Sed dui est, luctus egestas elit vitae, sodales mollis ex. Aliquam a tortor ipsum. Praesent ac condimentum dolor, in porta tellus. Vivamus viverra ac dolor ac mattis. Vestibulum a hendrerit orci. Etiam tempus libero ut vestibulum interdum. Vestibulum viverra imperdiet consequat." + Environment.NewLine +
                    "Proin dignissim posuere tincidunt. Fusce a libero id enim fermentum aliquam. Donec ligula lacus, posuere sed tempor ut, rutrum at elit. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Praesent porttitor hendrerit lacinia. Aenean mattis dapibus viverra. Morbi mollis eget leo quis egestas .Nulla facilisi.Mauris elementum urna a lacus volutpat, in rutrum augue iaculis. Mauris quis dui sit amet ligula tincidunt rhoncus cursus in eros." + Environment.NewLine +
                    "Integer a dapibus dolor. Vivamus venenatis sem est, a feugiat erat volutpat id. Vestibulum sit amet purus lectus.Nullam velit elit, mollis sed cursus a, sagittis non ex. Proin accumsan augue nec est bibendum semper. Curabitur efficitur orci ut turpis tincidunt, at tempus diam vehicula.Sed placerat congue dolor ac aliquam.",
                "baby.jpeg");
            InformacoesAux.Add(teste1);

            Informacao teste2 = new Informacao(
                4,
                12,
                "Informação nº 2",
                teste1.Texto,
                "baby.jpeg");
            InformacoesAux.Add(teste2);

            Informacao teste3 = new Informacao(
                8,
                16,
                "Informação nº 3",
                teste1.Texto,
                "baby.jpeg");
            InformacoesAux.Add(teste3);

            Informacao teste4 = new Informacao(
                12,
                20,
                "Informação nº 4",
                teste1.Texto,
                "baby.jpeg");
            InformacoesAux.Add(teste4);

            Informacao teste5 = new Informacao(
                16,
                24,
                "Informação nº 5",
                teste1.Texto,
                "baby.jpeg");
            InformacoesAux.Add(teste5);

            // Criança
            Crianca = app._crianca;
            //Crianca = new Crianca("Zézinho", 24);
            Nome = Crianca.crianca_primeiro_nome;
            IdadeAux = (Crianca.IdadeMeses >= 24) ? 24 : Crianca.IdadeMeses;
            IdadeExtenso = DefineIdadeExtenso();

            // Commands
            this.MenosIdadeCommand = new Command(this.MenosIdade);
            this.MaisIdadeCommand = new Command(this.MaisIdade);
            this.IdadePickerCommand = new Command<Picker>(this.IdadePicker);
            this.InfoPageCommand = new Command<Informacao>(this.InfoPage);

            // Navigation
            this.Navigation = Navigation;
            this._navigationService = DependencyService.Get<Services.INavigationService>();

            // Idades picker
            IdadesPickerLista = new List<string>() { "2 semanas" };

            for (int i = 1; i <= Crianca.IdadeMeses; i++)
            {
                IdadesPickerLista.Add(i + " meses");
            }
        }

        // Botão da seta pra direita
        private void MaisIdade()
        {
            if (IdadeAux < 24 && IdadeAux < Crianca.IdadeMeses)
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

        // Define string da idade para mostrar na tela
        private string DefineIdadeExtenso()
        {
            if (IdadeAux == 0)
            {
                return "2 semanas";
            }
            else if (IdadeAux >= 12)
            {
                if (IdadeAux == 12)
                {
                    return "1 ano";
                }
                else if (IdadeAux >= 24)
                {
                    return "2 anos";
                }
                else
                {
                    if (IdadeAux - 12 == 1)
                    {
                        return "1 ano e 1 mês";
                    }
                    else
                    {
                        return "1 ano e " + (IdadeAux - 12) + " meses";
                    }
                }
            }
            else
            {
                if (IdadeAux == 1)
                {
                   return "1 mês";
                }
                else
                {
                    return IdadeAux + " meses";
                }
            }
        }

        // Organiza as informações mostradas na tela de acordo com a idade que o usuário escolhe ao interagir com as setas
        private void OrganizaInformacoes()
        {
            foreach (var informacao in InformacoesAux)
            {
                // se a idade da criança é compatível com a informação
                if (informacao.IdadeInicio <= IdadeAux && informacao.IdadeFim >= IdadeAux)
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
            await this._navigationService.NavigateToInfoPage(this.Navigation, info);
        }
    }
}
