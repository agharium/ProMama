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

            BindingContext = new ViewModel.Home.Paginas.AcompanhamentoViewModel(Navigation);
        }
    }
}