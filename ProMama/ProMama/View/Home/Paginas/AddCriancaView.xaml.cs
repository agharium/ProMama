using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProMama.View.Home.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCriancaView : ContentPage
    {
        public AddCriancaView()
        {
            InitializeComponent();

            BindingContext = new ViewModel.Home.Paginas.AddCriancaViewModel();
        }
    }
}