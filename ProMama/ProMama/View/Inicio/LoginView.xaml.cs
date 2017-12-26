using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProMama.View.Inicio
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();

            BindingContext = new ViewModel.Inicio.LoginViewModel();
        }

        private void ChangeToCadastro()
        {
            var tabbedPage = this.Parent as LoginCadastroTabbedView;
            tabbedPage.ChangeToCadastro();
        }
    }
}