using ProMama.ViewModel.Home.Paginas;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProMama.View.Home.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MarcosView : ContentPage
    {
        public MarcosView()
        {
            InitializeComponent();

            BindingContext = new MarcosViewModel(Navigation);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = new MarcosViewModel(Navigation);
        }
    }
}