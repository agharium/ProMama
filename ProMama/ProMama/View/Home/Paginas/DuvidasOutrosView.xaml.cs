using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProMama.View.Home.Paginas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DuvidasOutrosView : ContentPage
	{
		public DuvidasOutrosView()
		{
			InitializeComponent();

            BindingContext = new ViewModel.Home.Paginas.DuvidasOutrosViewModel(Navigation);
        }
    }
}