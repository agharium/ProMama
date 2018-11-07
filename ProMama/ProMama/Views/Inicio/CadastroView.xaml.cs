using ProMama.ViewModels.Inicio;
using System;
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

        private void ChangeToLogin(object sender, EventArgs e)
        {
            var tabbedPage = Parent as LoginCadastroTabbedView;
            tabbedPage.ChangeToLogin();
        }

        protected override bool OnBackButtonPressed()
        {
            ChangeToLogin(null, null);
            return true;
        }
    }
}