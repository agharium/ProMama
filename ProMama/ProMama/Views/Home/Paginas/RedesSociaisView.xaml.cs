using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProMama.Views.Home.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RedesSociaisView : ContentPage
    {
        public RedesSociaisView()
        {
            InitializeComponent();
        }

        void OnFacebookTapped(object sender, EventArgs args)
        {
            try
            {
                Device.OpenUri(new Uri("https://www.facebook.com/promama.osorio.3/"));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        void OnYoutubeTapped(object sender, EventArgs args)
        {
            try
            {
                Device.OpenUri(new Uri("https://www.youtube.com/channel/UCvEaKW37iqyEUWOswirD2Qg/featured"));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
    }
}