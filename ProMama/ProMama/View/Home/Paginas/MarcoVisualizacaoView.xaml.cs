using ProMama.Model;
using ProMama.ViewModel.Home.Paginas;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProMama.View.Home.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MarcoVisualizacaoView : ContentPage
    {
        public MarcoVisualizacaoView(Marco marco)
        {
            InitializeComponent();

            BindingContext = new MarcoVisualizacaoViewModel(Navigation, marco);
        }
    }
}