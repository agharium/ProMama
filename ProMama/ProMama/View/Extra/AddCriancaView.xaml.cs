using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProMama.View.Extra
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCriancaView : ContentPage
    {
        public AddCriancaView()
        {
            InitializeComponent();

            BindingContext = new ViewModel.Extra.AddCriancaViewModel();
        }
    }
}