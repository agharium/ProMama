using ProMama.ViewModels.Inicio;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProMama.Views.Inicio
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CadastroView : ContentPage
    {
        public CadastroView()
        {
            InitializeComponent();

            BindingContext = new CadastroViewModel();
        }

        private void ChangeToLogin()
        {
            var tabbedPage = this.Parent as LoginCadastroTabbedView;
            tabbedPage.ChangeToLogin();
        }
    }
}