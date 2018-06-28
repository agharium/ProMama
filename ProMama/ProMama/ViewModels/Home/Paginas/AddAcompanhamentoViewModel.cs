using ProMama.Models;
using ProMama.ViewModels.Services;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModels.Home.Paginas
{
    class AddAcompanhamentoViewModel : ViewModelBase
    {
        private Aplicativo app = Aplicativo.Instance;

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

        public string Peso { get; set; }
        public string Altura { get; set; }
        public bool Alimentacao1 { get; set; }
        public bool Alimentacao2 { get; set; }
        public bool Alimentacao3 { get; set; }
        public bool Alimentacao4 { get; set; }
        public bool Alimentacao5 { get; set; }
        public bool Alimentacao6 { get; set; }
        public bool Alimentacao7 { get; set; }
        public bool Alimentacao8 { get; set; }

        private INavigation Navigation { get; set; }
        private readonly INavigationService NavigationService;
        private readonly IMessageService MessageService;

        public ICommand SalvarCommand { get; set; }

        public AddAcompanhamentoViewModel(INavigation _navigation)
        {
            Navigation = _navigation;
            NavigationService = DependencyService.Get<INavigationService>();
            MessageService = DependencyService.Get<IMessageService>();

            DataMinima = app._crianca.crianca_dataNascimento;
            DataMaxima = DateTime.Now;
            DataSelecionada = DateTime.Now;
            Alimentacao1 = false;
            Alimentacao2 = false;
            Alimentacao3 = false;
            Alimentacao4 = false;
            Alimentacao5 = false;
            Alimentacao6 = false;
            Alimentacao7 = false;
            Alimentacao8 = false;

            SalvarCommand = new Command(Salvar);
        }

        private async void Salvar()
        {
            if (!String.IsNullOrEmpty(Peso) &&
                !String.IsNullOrEmpty(Altura) && (
                Alimentacao1 || Alimentacao2 ||
                Alimentacao3 || Alimentacao4 ||
                Alimentacao5 || Alimentacao6 ||
                Alimentacao7 || Alimentacao8))
            {
                string alimentacoes = "";
                if (Alimentacao1)
                    alimentacoes += "leite materno, ";
                if (Alimentacao2)
                    alimentacoes += "fórmula infantil, ";
                if (Alimentacao3)
                    alimentacoes += "leite de vaca, ";
                if (Alimentacao4)
                    alimentacoes += "água, ";
                if (Alimentacao5)
                    alimentacoes += "chá, ";
                if (Alimentacao6)
                    alimentacoes += "suco, ";
                if (Alimentacao7)
                    alimentacoes += "frutas, ";
                if (Alimentacao8)
                    alimentacoes += "alimentação sólida, ";
                                
                alimentacoes = char.ToUpper(alimentacoes[0]) + alimentacoes.Substring(1, alimentacoes.Length - 3);

                var acompanhamento = new Acompanhamento(app._crianca.crianca_id, DataSelecionada, Peso + "kg", Altura + "cm", alimentacoes);
                App.AcompanhamentoDatabase.SaveIncrementing(acompanhamento);
                await Navigation.PopAsync();
            }
            else
            {
                await MessageService.AlertDialog("Nenhum campo pode estar vazio.");
            }
        }
    }
}
