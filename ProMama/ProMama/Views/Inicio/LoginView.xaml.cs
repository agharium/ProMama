using ProMama.ViewModels.Inicio;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProMama.Views.Inicio
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();

            BindingContext = new LoginViewModel();
        }

        private void ChangeToCadastro()
        {
            var tabbedPage = Parent as LoginCadastroTabbedView;
            tabbedPage.ChangeToCadastro();
        }
    }
}