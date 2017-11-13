using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProMama.View.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeDetail : ContentPage
    {
        public HomeDetail()
        {
            InitializeComponent();

            BindingContext = new ViewModel.Home.HomeDetailViewModel();
        }
    }
}