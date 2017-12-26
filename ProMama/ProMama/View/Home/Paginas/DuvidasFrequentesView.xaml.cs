using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProMama.View.Home.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DuvidasFrequentesView : ContentPage
    {
        public DuvidasFrequentesView()
        {
            InitializeComponent();

            BindingContext = new ViewModel.Home.Paginas.DuvidasFrequentesViewModel();
        }
    }
}