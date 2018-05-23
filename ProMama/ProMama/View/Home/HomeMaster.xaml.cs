using ProMama.ViewModel.Home;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProMama.View.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeMaster : ContentPage
    {
        public ListView ListView;

        public HomeMaster()
        {
            InitializeComponent();

            BindingContext = new HomeMasterViewModel();
            ListView = MenuItemsListView;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = new HomeMasterViewModel();
        }
    }
}