using ProMama.Model;
using ProMama.ViewModel.Services;
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
            LoginCommand = new Command(Login);

            _navigationService = DependencyService.Get<INavigationService>();
            _messageService = DependencyService.Get<IMessageService>();
            _restService = DependencyService.Get<IRestService>();
        }

        public async void Login()
        {
            if (Email.Equals(string.Empty) || Password.Equals(string.Empty))
            {
                await _messageService.AlertDialog("Nenhum campo pode estar vazio.");
            }
            else
            {
                var u = new Usuario(Email, Password);
                var result = await _restService.UsuarioLogin(u);

                if (!result.success)
                {
                    await _messageService.AlertDialog(result.message);
                } else
                {
                    var resultAux = await _restService.UsuarioGet(result);
                    if (!resultAux.success)
                    {
                        await _messageService.AlertDialog(result.message);
                    } else
                    {
                        app._usuario = resultAux.user;
                        App.UsuarioDatabase.SaveUsuario(app._usuario);

                        if (app._usuario.usuario_criancas.Count == 0)
                        {
                            _navigationService.NavigateToAddCrianca();
                        }
                        else
                        {
                            app._crianca = app._usuario.usuario_criancas[app._usuario.usuario_criancas.Count - 1];
                            _navigationService.NavigateToHome();
                        }
                    }
                }
            }
        }

    }
}
