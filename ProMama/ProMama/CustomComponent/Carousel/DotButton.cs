using System;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace ProMama.CustomComponent.Carousel
{
    public class DotButton : CircleImage
    {
        public int index;
        public DotButtonsLayout layout;
        public event ClickHandler Clicked;
        public delegate void ClickHandler(DotButton sender);
        public DotButton()
        {
            var clickCheck = new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    Clicked?.Invoke(this);
                })
            };
            GestureRecognizers.Add(clickCheck);
        }
    }
}
