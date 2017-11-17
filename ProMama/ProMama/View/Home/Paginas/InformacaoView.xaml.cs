using ProMama.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProMama.View.Home.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InformacaoView : ContentPage
    {
        public InformacaoView(Informacao info)
        {
            InitializeComponent();

            BindingContext = new ViewModel.Home.Paginas.InformacaoViewModel(info);
        }
    }
}