using CoreGraphics;
using ProMama.iOS.Components;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Frame), typeof(iOSMaterialFrameRenderer))]
namespace ProMama.iOS.Components
{
    /// <summary>
    /// Renderer to update all frames with better shadows matching material design standards
    /// https://alexdunn.org/2017/05/01/xamarin-tips-making-your-ios-frame-shadows-more-material/
    /// </summary>

    public class iOSMaterialFrameRenderer : FrameRenderer
    {
        public override void Draw(CGRect rect)
        {
            base.Draw(rect);

            // Update shadow to match better material design standards of elevation
            Layer.ShadowRadius = 2.0f;
            Layer.ShadowColor = UIColor.Gray.CGColor;
            Layer.ShadowOffset = new CGSize(2, 2);
            Layer.ShadowOpacity = 0.80f;
            //Layer.ShadowPath = UIBezierPath.FromRect(Layer.Bounds).CGPath; --> tentativa pra resolver o problema da sombra voltar
            Layer.MasksToBounds = false;
        }
    }
}