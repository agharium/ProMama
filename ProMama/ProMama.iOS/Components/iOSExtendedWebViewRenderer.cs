using ProMama.Components;
using ProMama.iOS.Components;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedWebView), typeof(iOSExtendedWebViewRenderer))]
namespace ProMama.iOS.Components
{
    public class iOSExtendedWebViewRenderer : WebViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            this.BackgroundColor = UIColor.Clear;
            Delegate = new ExtendedUIWebViewDelegate(this);
        }
    }
}