using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProMama.View.Inicio
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CadastroView : ContentPage
    {
        public CadastroView()
        {
            InitializeComponent();

            BindingContext = new ViewModel.Inicio.CadastroViewModel();
        }

        private void ChangeToLogin()
        {
            var tabbedPage = this.Parent as LoginCadastroTabbedView;
            tabbedPage.ChangeToLogin();
        }
    }
}