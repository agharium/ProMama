using ProMama.Models;
using ProMama.Views.Home.Paginas;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProMama.Views.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : MasterDetailPage
    {
        private Aplicativo app = Aplicativo.Instance;

        public Home()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;

            app._home = this;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (!(e.SelectedItem is HomeMenuItem item))
                return;

            var page = (Page)Activator.CreateInstance(item.Pagina);
            //page.Title = item.Titulo;

            Detail = new NavigationPage(page);
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }

        public void Detail_Galeria()
        {
            var page = (Page)Activator.CreateInstance(typeof(GaleriaView));
            page.Title = "Galeria";

            Detail = new NavigationPage(page);
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }

        public void Detail_Home()
        {
            var page = (Page)Activator.CreateInstance(typeof(HomeDetail));
            page.Title = "Início";

            Detail = new NavigationPage(page);
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }
    }
}