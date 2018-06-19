using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace ProMama.Components.Carousel
{
    public class Carousel : AbsoluteLayout
    {
        private DotButtonsLayout dotLayout;
        private CarouselView carousel;

        public Carousel(ObservableCollection<CarouselContent> pages)
        {
            //Set the Layout to fill and expand to occupy its whole space.
            HorizontalOptions = LayoutOptions.FillAndExpand;
            VerticalOptions = LayoutOptions.FillAndExpand;
            //Create the CarouselView itself.
            carousel = new CarouselView();
            //And make it expand to the whole Layout.
            carousel.HorizontalOptions = LayoutOptions.FillAndExpand;
            carousel.VerticalOptions = LayoutOptions.FillAndExpand;
            //Create that new DataTemplate

            var template = new DataTemplate(() => {
                //We chose an AbsoluteLayout for this because it is the 
                //most flexble layout in my opinion.
                var page1 = new AbsoluteLayout();
                //page1.BackgroundColor = Color.FromHex("2C2E31");
                page1.HorizontalOptions = LayoutOptions.FillAndExpand;
                page1.VerticalOptions = LayoutOptions.FillAndExpand;
                // We make the background color bindable so the ItemSource
                //Can set the color.
                page1.SetBinding(AbsoluteLayout.BackgroundColorProperty, "BackgroundColor");
                // Create the second label 
                var lab = new Label()
                {
                     FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                     FontAttributes = FontAttributes.Bold,
                     HorizontalOptions = LayoutOptions.Center,
                     VerticalOptions = LayoutOptions.Center
                };
                //lab.TextColor = Color.White;
                //Bind its conteent to the Content1-attribute
                lab.SetBinding(Label.TextProperty, "Content1");
                var lab2 = new Label()
                {
                    FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalTextAlignment = TextAlignment.Center,
                    Margin = new Thickness(15, 0, 15, 0)
                };
                //lab2.TextColor = Color.White;
                //And finally bind the last label.
                lab2.SetBinding(Label.TextProperty, "Content2");

                // IMG
                var img = new FFImageLoading.Forms.CachedImage()
                {
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    WidthRequest = 500,
                    HeightRequest = 500,
                    //CacheDuration = System.TimeSpan.FromDays(30),
                    DownsampleToViewSize = true,
                    //LoadingPlaceholder = "loading.png",
                    //ErrorPlaceholder = "error.png",
                };
                img.SetBinding(FFImageLoading.Forms.CachedImage.SourceProperty, "Image");

                // Add everything to our Layout.
                page1.Children.Add(lab);
                page1.Children.Add(lab2);
                page1.Children.Add(img);
                // And position the content
                AbsoluteLayout.SetLayoutBounds(lab, new Rectangle(0, 0, 1, 0.2));
                AbsoluteLayout.SetLayoutFlags(lab, AbsoluteLayoutFlags.All);
                AbsoluteLayout.SetLayoutBounds(lab2, new Rectangle(0, 0.1, 1, 0.2));
                AbsoluteLayout.SetLayoutFlags(lab2, AbsoluteLayoutFlags.All);
                AbsoluteLayout.SetLayoutBounds(img, new Rectangle(0, 0.6, 1, 0.5));
                AbsoluteLayout.SetLayoutFlags(img, AbsoluteLayoutFlags.All);
                return page1;
            });

            //Assign the passeg pages to the ItemsSource
            carousel.ItemsSource = pages;
            //Assign the freshly created template
            carousel.ItemTemplate = template;
            //The ItemSelected event is raised when the user swipes trough the 
            //carousel view. We subscribe to it to update the page indicators in
            //Placeholder 3. Make sure to unsubscribe somewhere
            carousel.PositionSelected += pageChanged;
            //Add the carousel to the abolsute layout and set its boundaries to fill
            //the entire layout
            Children.Add(carousel);
            AbsoluteLayout.SetLayoutBounds(carousel, new Rectangle(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(carousel, AbsoluteLayoutFlags.All);
            //Create the button layout with as many buttons as there are pages
            dotLayout = new DotButtonsLayout(pages.Count, Color.FromHex("F44336"), 20);
            //Subscribe to the click events of the dot buttons to switch to the desired 
            //page
            foreach (DotButton dot in dotLayout.dots)
                dot.Clicked += dotClicked;
            Children.Add(dotLayout);
            AbsoluteLayout.SetLayoutBounds(dotLayout, new Rectangle(0, 0.92, 1, .05));
            AbsoluteLayout.SetLayoutFlags(dotLayout, AbsoluteLayoutFlags.All);
        }

        //The function that is called when the user swipes trough pages
        private void pageChanged(object sender, SelectedPositionChangedEventArgs e)
        {
            //Get the selected page
            var position = (int)(e.SelectedPosition);
            //Set all buttons opacity to 0.5 but the selected one, which we set to 1
            for (int i = 0; i < dotLayout.dots.Length; i++)
                if (position == i)
                {
                    dotLayout.dots[i].Opacity = 1;
                    dotLayout.dots[i].FillColor = Color.FromHex("F44336");
                }
                else
                {
                    dotLayout.dots[i].Opacity = 0.5;
                    dotLayout.dots[i].FillColor = Color.Transparent;
                }
        }
        //The function called by the buttons clicked event
        private void dotClicked(object sender)
        {
            var button = (DotButton)sender;
            //Get the selected buttons index
            int index = button.index;
            //Set the corresponding page as position of the carousel view
            carousel.Position = index;
        }
    }
}
