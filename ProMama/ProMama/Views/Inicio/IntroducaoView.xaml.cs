using ProMama.ViewModels.Inicio;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProMama.Views.Inicio
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class IntroducaoView : ContentPage
	{
		public IntroducaoView()
		{
			InitializeComponent ();

            BindingContext = new IntroducaoViewModel();
		}
	}
}