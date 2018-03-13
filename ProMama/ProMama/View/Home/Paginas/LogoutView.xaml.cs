using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProMama.View.Home.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogoutView : ContentPage
    {
        public LogoutView()
        {
            InitializeComponent();

            BindingContext = new ViewModel.Home.Paginas.LogoutViewModel(Navigation);
        }
    }
}