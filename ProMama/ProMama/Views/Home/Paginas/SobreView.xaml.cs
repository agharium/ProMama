using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProMama.Views.Home.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SobreView : ContentPage
    {
        public SobreView()
        {
            InitializeComponent();
        }

        void OnDecretoTapped(object sender, EventArgs args)
        {
            try
            {
                Device.OpenUri(new Uri("https://leismunicipais.com.br/a/rs/o/osorio/decreto/2017/20/193/decreto-n-193-2017-dispoe-sobre-o-programa-municipal-de-aleitamento-materno-pro-mama-atraves-do-protocolo-municipal-de-aleitamento-materno-de-osorio?q=193%2F2017"));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
    }
}