using ProMama.ViewModels.Home.Paginas;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProMama.Views.Home.Paginas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FaleConoscoOutrosView : ContentPage
	{
		public FaleConoscoOutrosView()
		{
			InitializeComponent();

            BindingContext = new FaleConoscoOutrosViewModel(Navigation);
        }
    }
}