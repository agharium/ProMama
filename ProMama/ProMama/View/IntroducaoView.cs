using ProMama.CustomControl.Carousel;
using ProMama.ViewModel;
using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace ProMama.View
{
    public class IntroducaoView : ContentPage
    {
        public IntroducaoView()
        {
            // viewmodel
            BindingContext = new IntroducaoModel();
            
            // botão da página
            var NavigationButton = new Button();
            NavigationButton.SetBinding(Button.CommandParameterProperty, new Binding("NavigationCommand"));
            NavigationButton.Text = "Vamos lá!";

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
                            NavigationButton
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
            /*page1.BackgroundColor = Color.FromHex("e74c3c");
            page2.BackgroundColor = Color.FromHex("16a085");
            page3.BackgroundColor = Color.FromHex("2c3e50");*/
            page1.Header = "Uma informação aqui";
            page1.Content1 = "Um subtítulo...";
            page1.Content2 = "E uma imagem aqui.";
            page2.Header = "Uma informação aqui";
            page2.Content1 = "Um subtítulo...";
            page2.Content2 = "E uma imagem aqui.";
            page3.Header = "Uma informação aqui";
            page3.Content1 = "Um subtítulo...";
            page3.Content2 = "E uma imagem aqui.";
            var pages = new ObservableCollection<CarouselContent>
            {
                page1,
                page2,
                page3
            };
            var carouselView = new Carousel(pages);

            return carouselView;
        }
    }
}