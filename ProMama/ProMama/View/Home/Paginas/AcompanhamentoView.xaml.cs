using ProMama.ViewModel.Home.Paginas;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProMama.View.Home.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AcompanhamentoView : ContentPage
    {
        public AcompanhamentoView()
        {
            InitializeComponent();

            BindingContext = new AcompanhamentoViewModel(Navigation);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = new AcompanhamentoViewModel(Navigation);
        }
    }
}