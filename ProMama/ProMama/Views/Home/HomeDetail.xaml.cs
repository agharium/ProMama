using ProMama.ViewModels.Home;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProMama.Views.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeDetail : ContentPage
    {
        public HomeDetail()
        {
            InitializeComponent();

            BindingContext = new HomeDetailViewModel(this.Navigation);
        }
    }
}