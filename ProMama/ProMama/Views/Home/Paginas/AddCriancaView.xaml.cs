using ProMama.ViewModels.Home.Paginas;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProMama.Views.Home.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCriancaView : ContentPage
    {
        public AddCriancaView()
        {
            InitializeComponent();

            BindingContext = new AddCriancaViewModel();
        }
    }
}