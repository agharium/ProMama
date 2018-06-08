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

        private int _alimentacao;
        public int Alimentacao
        {
            get
            {
                return _alimentacao;
            }
            set
            {
                _alimentacao = value;
                Notify("Alimentacao");
            }
        }

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
            Alimentacao = -1;

            SalvarCommand = new Command(Salvar);
        }

        private async void Salvar()
        {
            if (!String.IsNullOrEmpty(Peso) && !String.IsNullOrEmpty(Altura) && Alimentacao != -1)
            {
                var acompanhamento = new Acompanhamento(app._crianca.crianca_id, DataSelecionada.ToString("dd/MM/yyyy"), Peso, Altura, Alimentacao);
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
