using ProMama.ViewModel.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModel.Inicio
{
    class CadastroViewModel : ViewModelBase
    {
        private string _email = "";
        public string Email {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                //this.Notify("Email");
            }
        }

        private string _password = "";
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                //this.Notify("Email");
            }
        }

        private string _passwordConfirmation = "";
        public string PasswordConfirmation
        {
            get
            {
                return _passwordConfirmation;
            }
            set
            {
                _passwordConfirmation = value;
                //this.Notify("Email");
            }
        }

        public ICommand CadastroCommand { get; set; }

        private readonly INavigationService _navigationService;
        private readonly IMessageService _messageService;

        public CadastroViewModel()
        {
            this.CadastroCommand = new Command(this.Cadastro);

            this._navigationService = DependencyService.Get<INavigationService>();
            this._messageService = DependencyService.Get<IMessageService>();
        }

        public async void Cadastro()
        {
            /*if (this.Email.Equals(string.Empty) && !this.Email.Contains("@")){
                await this._messageService.ShowAsync("E-mail inválido!");
            } else*/ if (!this.Password.Equals(PasswordConfirmation)){
                await this._messageService.ShowAsync("As senhas não coincidem!");
            } else
            {
                this._navigationService.NavigateToAddCrianca();
            }
        }
    }
}
