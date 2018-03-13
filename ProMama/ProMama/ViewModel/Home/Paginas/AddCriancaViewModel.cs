using ProMama.Model;
using ProMama.ViewModel.Services;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModel.Home.Paginas
{
    class AddCriancaViewModel : ViewModelBase
    {
        private Aplicativo app = Aplicativo.Instance;

        private string _primeiroNome = "";
        public string PrimeiroNome
        {
            get
            {
                return _primeiroNome;
            }
            set
            {
                _primeiroNome = value;
            }
        }

        private DateTime _minimumDate;
        public DateTime MinimumDate
        {
            get
            {
                return _minimumDate;
            }
            set
            {
                _minimumDate = value;
                this.Notify("MinimumDate");
            }
        }

        private DateTime _maximumDate;
        public DateTime MaximumDate
        {
            get
            {
                return _maximumDate;
            }
            set
            {
                _maximumDate = value;
                this.Notify("MaximumDate");
            }
        }

        private DateTime _currentDate;
        public DateTime CurrentDate
        {
            get
            {
                return _currentDate;
            }
            set
            {
                _currentDate = value;
                this.Notify("CurrentDate");
            }
        }

        public ICommand AddCriancaCommand { get; set; }

        private readonly INavigationService _navigationService;
        private readonly IMessageService _messageService;
        private readonly IRestService _restService;

        public AddCriancaViewModel()
        {
            this.AddCriancaCommand = new Command(this.AddCrianca);

            this._navigationService = DependencyService.Get<INavigationService>();
            this._messageService = DependencyService.Get<IMessageService>();
            this._restService = DependencyService.Get<IRestService>();

            this.MinimumDate = DateTime.Now.AddYears(-2);
            this.MaximumDate = DateTime.Now;
            this.CurrentDate = DateTime.Now;
        }

        private async void AddCrianca()
        {
            if (PrimeiroNome.Equals(string.Empty))
            {
                await this._messageService.AlertDialog("Nenhum campo pode estar vazio.");
            }
            else
            {
                if (await _messageService.ConfirmationDialog("Você tem certeza que esta é a data de nascimento da criança? Você não poderá alterar esta informação posteriormente.", "Continuar", "Voltar")){
                    var c = new Crianca(app._usuario, PrimeiroNome, CurrentDate);
                    var result = await _restService.CriancaCreate(c);

                    if (result.success)
                    {
                        c.crianca_id = result.id;
                        App.CriancaDatabase.SaveCrianca(c);
                        app._crianca = c;

                        _navigationService.NavigateToHome();
                    }
                    else
                    {
                        await _messageService.AlertDialog("Ocorreu um erro. Tente novamente mais tarde.");
                    }
                }
            }
        }
    }
}