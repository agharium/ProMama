using ProMama.ViewModels.Inicio;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProMama.Views.Inicio
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NovaSenhaView : ContentPage
    {
        public NovaSenhaView()
        {
            InitializeComponent();

            BindingContext = new NovaSenhaViewModel();
        }
    }
}