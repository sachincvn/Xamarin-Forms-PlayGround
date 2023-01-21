using FormsPlay.UI;
using Foundation;
using MediaManager;
using MvvmCross.Forms.Platforms.Ios.Core;
using UIKit;

namespace FormsPlay.iOS
{
    [Register(nameof(AppDelegate))]
    public partial class AppDelegate : MvxFormsApplicationDelegate<Setup, Core.App, UI.App>
    {
        public override void FinishedLaunching(UIApplication application)
        {
            Rg.Plugins.Popup.Popup.Init();
            CrossMediaManager.Current.Init();
            base.FinishedLaunching(application);
            App.ScreenHeight = (int)UIScreen.MainScreen.Bounds.Height;
            App.ScreenWidth = (int)UIScreen.MainScreen.Bounds.Width;
        }
    }
}
