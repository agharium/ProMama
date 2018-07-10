using ProMama.Models;
using ProMama.ViewModels.Home.Paginas;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProMama.Views.Home.Paginas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DuvidasFrequentesView : ContentPage
	{
		public DuvidasFrequentesView ()
		{
			InitializeComponent ();

            BindingContext = new DuvidasFrequentesViewModel(Navigation);
        }

        protected override bool OnBackButtonPressed()
        {
            Aplicativo app = Aplicativo.Instance;
            app._home.Detail_Home();
            return true;
        }
    }
}