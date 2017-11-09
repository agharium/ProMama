using ProMama.CustomComponent.Carousel;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace ProMama.View.Inicio
{
    public class IntroducaoView : ContentPage
    {
        public IntroducaoView()
        {
            // viewmodel
            BindingContext = new ViewModel.Inicio.IntroducaoViewModel();
            
            // botão da página
            var btn = new Button();
            btn.SetBinding(Button.CommandProperty, new Binding("NavigationCommand"));
            btn.Text = "Vamos lá!";
            btn.Style = (Style)Application.Current.Resources["buttonMain"];

            // carousel da página
            var Carousel = CreateCarousel();

            Content = new StackLayout
            {
                Children = {
                    new StackLayout
                    {
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        Children =
                        {
                            Carousel
                        }
                    },
                    new StackLayout
                    {
                        Padding = 20,
                        VerticalOptions = LayoutOptions.Fill,
                        Children =
                        {
                            btn
                        }
                    }
                }
            };
        }

        private Carousel CreateCarousel()
        {
            var page1 = new CarouselContent();
            var page2 = new CarouselContent();
            var page3 = new CarouselContent();
            var page4 = new CarouselContent();
            var page5 = new CarouselContent();

            /*page1.BackgroundColor = Color.FromHex("e74c3c");
            page2.BackgroundColor = Color.FromHex("16a085");
            page3.BackgroundColor = Color.FromHex("2c3e50");*/

            page1.Content1 = "Título";
            page1.Content2 = "Pequena informação introdutória sobre o aplicativo";
            page1.Image = "intro_bebe1.png";

            page2.Content1 = "Título";
            page2.Content2 = "Pequena informação introdutória sobre o aplicativo";
            page2.Image = "intro_bebe2.png";

            page3.Content1 = "Título";
            page3.Content2 = "Pequena informação introdutória sobre o aplicativo";
            page3.Image = "intro_bebe3.png";

            page4.Content1 = "Título";
            page4.Content2 = "Pequena informação introdutória sobre o aplicativo";
            page4.Image = "intro_bebe4.png";

            page5.Content1 = "";
            page5.Content2 = "Desenvolvido em parceria com";
            page5.Image = "ifrs_logo.png";

            var pages = new ObservableCollection<CarouselContent>
            {
                page1,
                page2,
                page3,
                page4,
                page5
            };
            var carouselView = new Carousel(pages);

            return carouselView;
        }
    }
}