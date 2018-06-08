using ProMama.ViewModels.Home.Paginas;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProMama.Views.Home.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PerfilMaeView : ContentPage
    {
        public PerfilMaeView()
        {
            InitializeComponent();

            BindingContext = new PerfilMaeViewModel(Navigation);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = new PerfilMaeViewModel(Navigation);
        }
    }
}