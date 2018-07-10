using ProMama.Components;
using UIKit;

namespace ProMama.iOS.Components
{
    public class ExtendedUIWebViewDelegate : UIWebViewDelegate
    {
        iOSExtendedWebViewRenderer webViewRenderer;

        public ExtendedUIWebViewDelegate(iOSExtendedWebViewRenderer _webViewRenderer = null)
        {
            webViewRenderer = _webViewRenderer ?? new iOSExtendedWebViewRenderer();
        }

        public override async void LoadingFinished(UIWebView webView)
        {
            var wv = webViewRenderer.Element as ExtendedWebView;
            if (wv != null)
            {
                await System.Threading.Tasks.Task.Delay(100); // wait here till content is rendered
                wv.HeightRequest = (double)webView.ScrollView.ContentSize.Height;
            }
        }
    }
}