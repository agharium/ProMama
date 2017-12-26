using ProMama.Model;
using ProMama.ViewModel.Services;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModel.Inicio
{
    class LoginViewModel : ViewModelBase
    {
        private Aplicativo app = Aplicativo.Instance;

        private string _email = "";
        public string Email
        {
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

        public ICommand LoginCommand { get; set; }

        private readonly INavigationService _navigationService;
        private readonly IMessageService _messageService;
        private readonly IRestService _restService;

        public LoginViewModel()
        {
            this.LoginCommand = new Command(this.Login);

            this._navigationService = DependencyService.Get<INavigationService>();
            this._messageService = DependencyService.Get<IMessageService>();
            this._restService = DependencyService.Get<IRestService>();
        }

        public async void Login()
        {
            if (this.Email.Equals(string.Empty) || this.Password.Equals(string.Empty))
            {
                await this._messageService.AlertDialog("Nenhum campo pode estar vazio.");
            }
            else
            {
                var u = new Usuario(this.Email, this.Password);
                var result = await _restService.UsuarioLogin(u);
                
                if (result.Equals("false"))
                {
                    await this._messageService.AlertDialog("Usuário ou senha incorretos.");
                } else
                {
                    u.Id = Int32.Parse(result);
                    app._usuario = u;

                    this._navigationService.NavigateToAddCrianca();
                }
            }
        }

    }
}
