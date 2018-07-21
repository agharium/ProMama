using ProMama.Components.Carousel;
using ProMama.ViewModels.Inicio;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace ProMama.Views.Inicio
{
    public class IntroducaoView : ContentPage
    {
        public IntroducaoView()
        {
            // viewmodel
            BindingContext = new IntroducaoViewModel();
            
            // botão da página
            var btn = new Button();
            btn.SetBinding(Button.CommandProperty, new Binding("NavigationCommand"));
            btn.Text = "Vamos lá!";
            btn.Style = (Style)Application.Current.Resources["MainButton"];

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

            page1.Content1 = "Bem-vindo ao Aplicativo Pró-Mamá!";
            page1.Content2 = "O leite materno é o melhor alimento para o seu bebê, porém amamentar pode não ser tão simples assim! Com o apoio deste aplicativo tudo vai ficar mais fácil! ";
            page1.Image = "intro_baby1.png";

            page2.Content1 = "O que o aplicativo faz?";
            page2.Content2 = "Preencha seus dados corretamente e você receberá notícias semanais sobre o aleitamento materno, a alimentação e o desenvolvimento do seu bebê de acordo com a idade dele.";
            page2.Image = "intro_baby2.png";

            page3.Content1 = "Fotos e marcos de desenvolvimento!";
            page3.Content2 = "Você vai poder registrar os melhores momentos do seu bebê e guardar para sempre! Vai poder acompanhar o desenvolvimento do seu bebê através de fotos mensais, peso, altura e gravar as datas mais importantes para vocês, como por exemplo: o primeiro sorriso!";
            page3.Image = "intro_baby3.png";

            page4.Content1 = "Aproveite esses dois anos!";
            page4.Content2 = "Estaremos bem pertinho de vocês porque o aplicativo Pró-Mamá acompanha seu bebê de zero a 2 anos de idade. Todo seu conteúdo foi pensado por profissionais da saúde pensando no melhor para vocês!";
            page4.Image = "intro_baby4.png";

            page5.Content1 = "";
            page5.Content2 = "Uma parceria";
            page5.Image = "logos_parceria.png";

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