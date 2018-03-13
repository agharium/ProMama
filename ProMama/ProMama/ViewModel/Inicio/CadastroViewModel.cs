using ProMama.Model;
using ProMama.ViewModel.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModel.Inicio
{
    class CadastroViewModel : ViewModelBase
    {
        private Aplicativo app = Aplicativo.Instance;

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
        private readonly IRestService _restService;

        public CadastroViewModel()
        {
            CadastroCommand = new Command(Cadastro);

            _navigationService = DependencyService.Get<INavigationService>();
            _messageService = DependencyService.Get<IMessageService>();
            _restService = DependencyService.Get<IRestService>();
        }

        public async void Cadastro()
        {
            if (!Password.Equals(PasswordConfirmation)){
                await _messageService.AlertDialog("As senhas não são iguais.");
            }
            else if (Email.Equals(string.Empty) || Password.Equals(string.Empty) || PasswordConfirmation.Equals(string.Empty))
            {
                await _messageService.AlertDialog("Nenhum campo pode estar vazio.");
            }
            else
            {
                var u = new Usuario(Email, Password);
                var result = await _restService.UsuarioCreate(u);

                if (!result.success)
                {
                    await _messageService.AlertDialog(result.message);
                }
                else
                {
                    u.usuario_id = result.id;
                    App.UsuarioDatabase.SaveUsuario(u);
                    app._usuario = u;

                    _navigationService.NavigateToAddCrianca();
                }
            }
        }
    }
}
