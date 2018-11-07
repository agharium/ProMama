using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProMama.Views.Home.Paginas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DesenvolvedoresView : ContentPage
	{
		public DesenvolvedoresView ()
		{
			InitializeComponent();
        }

        private void OpenUrl(string url)
        {
            try
            {
                Device.OpenUri(new Uri(url));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        private void Jose_Github(object sender, EventArgs e)
        {
            OpenUrl("https://github.com/agharium");
        }

        private void Jose_LinkedIn(object sender, EventArgs e)
        {
            OpenUrl("https://linkedin.com/in/jose-paulo-oliveira-filho");
        }

        private void Lucas_Github(object sender, EventArgs e)
        {
            OpenUrl("https://github.com/lucasjardi/");
        }

        private void Lucas_LinkedIn(object sender, EventArgs e)
        {
            OpenUrl("https://linkedin.com/in/lucas-jardim-7642abb7");
        }

        private void Max_Github(object sender, EventArgs e)
        {
            OpenUrl("https://github.com/maxcarlesso");
        }

        private void Max_LinkedIn(object sender, EventArgs e)
        {
            OpenUrl("https://linkedin.com/in/maxcsantos");
        }

        private void Voltar_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}