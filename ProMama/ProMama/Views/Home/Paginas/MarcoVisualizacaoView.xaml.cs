using ProMama.Models;
using ProMama.ViewModels.Home.Paginas;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProMama.Views.Home.Paginas
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