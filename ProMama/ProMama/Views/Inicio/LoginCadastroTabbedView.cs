using Xamarin.Forms;

namespace ProMama.Views.Inicio
{
    public class LoginCadastroTabbedView : TabbedPage
    {
        private readonly Page Login = new LoginView();
        private readonly Page Cadastro = new CadastroView();

        public LoginCadastroTabbedView()
        {
            Title = "Pró-Mamá";

            Children.Add(Login);
            Children.Add(Cadastro);
        }

        public void ChangeToCadastro()
        {
            this.CurrentPage = Cadastro;
        }

        public void ChangeToLogin()
        {
            this.CurrentPage = Login;
        }
    }
}