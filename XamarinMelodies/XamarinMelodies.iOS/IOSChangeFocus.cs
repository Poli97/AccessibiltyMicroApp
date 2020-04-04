using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinMelodies.iOS;
using XamarinMelodies.Model;

[assembly: Dependency(typeof(IOSChangeFocus))]
namespace XamarinMelodies.iOS
{
    public class IOSChangeFocus : IChangeAccessibilityFocus
    {
        public void ChangeFocus(View v)
        {
            UIAccessibility.PostNotification(UIAccessibilityPostNotification.ScreenChanged, v.GetRenderer().NativeView);
        }
    }
}
