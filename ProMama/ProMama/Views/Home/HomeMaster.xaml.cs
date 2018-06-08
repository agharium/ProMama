using ProMama.ViewModels.Home;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProMama.Views.Home
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
    }
}