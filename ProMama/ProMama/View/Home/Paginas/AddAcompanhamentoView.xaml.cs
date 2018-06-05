using ProMama.ViewModel.Home.Paginas;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProMama.View.Home.Paginas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddAcompanhamentoView : ContentPage
	{
		public AddAcompanhamentoView ()
		{
			InitializeComponent ();

            BindingContext = new AddAcompanhamentoViewModel(Navigation);
		}
	}
}