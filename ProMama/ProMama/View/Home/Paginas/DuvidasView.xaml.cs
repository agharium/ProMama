using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProMama.View.Home.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DuvidasView : ContentPage
    {
        public DuvidasView()
        {
            InitializeComponent();

            BindingContext = new ViewModel.Home.Paginas.DuvidasViewModel();
        }
    }
}