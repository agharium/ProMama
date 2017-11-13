using Xamarin.Forms;

namespace ProMama.CustomComponent
{
    public class CardView : Frame
    {
        public CardView()
        {
            Padding = 10;
            Margin = new Thickness(10, 5, 10, 5);
            HeightRequest = 250;

            if (Device.RuntimePlatform == Device.iOS)
            {
                //HasShadow = false;
                OutlineColor = Color.FromHex("262626");
                BackgroundColor = Color.Transparent;
            }

        }

        public static CardView CriaCardView(string titulo, string texto, string imagem)
        {
            var cv = new CardView();
            var subContainer = new Grid();
            subContainer.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            subContainer.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            subContainer.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            subContainer.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            var tituloXml = new Label { Text = titulo, TextColor = Color.FromHex("212121"), FontSize = 20 };

            var textoXml = new Label { Text = texto, TextColor = Color.FromHex("757575") };

            var imagemXml = new Image { Source = imagem, Aspect = Aspect.AspectFill };

            subContainer.Children.Add(tituloXml, 0, 0);
            subContainer.Children.Add(textoXml, 0, 1);
            subContainer.Children.Add(imagemXml, 0, 2);

            cv.Content = subContainer;
            return cv;
        }
    }
}