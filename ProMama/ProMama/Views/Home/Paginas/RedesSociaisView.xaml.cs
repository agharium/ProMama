using ProMama.Models;
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
                Device.OpenUri(new Uri("fb://profile/100018379265466/"));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                try
                {
                    Device.OpenUri(new Uri("https://www.facebook.com/promama.osorio.3"));
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                }
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

        protected override bool OnBackButtonPressed()
        {
            Aplicativo app = Aplicativo.Instance;
            app._home.Detail_Home();
            return true;
        }
    }
}