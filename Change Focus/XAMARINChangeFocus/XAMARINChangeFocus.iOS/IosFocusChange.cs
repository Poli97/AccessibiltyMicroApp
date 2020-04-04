using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XAMARINChangeFocus.iOS;

[assembly: Dependency(typeof(IosFocusChange))]
namespace XAMARINChangeFocus.iOS
{
    public class IosFocusChange : IChangeFocusService
    {
        public void ChangeFocus(View v)
        {
            UIAccessibility.PostNotification(notification: UIAccessibilityPostNotification.ScreenChanged, argument: v.GetRenderer().NativeView);

        }
    }
}
