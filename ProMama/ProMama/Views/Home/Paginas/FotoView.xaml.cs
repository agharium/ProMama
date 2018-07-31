using ProMama.Models;
using ProMama.ViewModels.Home.Paginas;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProMama.Views.Home.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FotoView : ContentPage
    {
        public FotoView(Foto foto)
        {
            InitializeComponent();

            BindingContext = new FotoViewModel(foto, Navigation);
        }
    }
}