using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using ProMama.Models;
using ProMama.ViewModels.Home.Paginas;
using ProMama.ViewModels.Services;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProMama.Views.Home.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostosSaudeView : ContentPage
    {
        public PostosSaudeView()
        {
            InitializeComponent();

            BindingContext = new PostosSaudeViewModel();
        }

        protected override bool OnBackButtonPressed()
        {
            Aplicativo app = Aplicativo.Instance;
            app._home.Detail_Home();
            return true;
        }
    }
}