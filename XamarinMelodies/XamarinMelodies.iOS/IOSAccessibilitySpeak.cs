using System;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinMelodies.iOS;
using XamarinMelodies.Model;

[assembly: Dependency(typeof(IOSAccessibilitySpeak))]
namespace XamarinMelodies.iOS
{
    public class IOSAccessibilitySpeak : IAccessibilitySpeak
    {
        public void AccSpeak(View v, string s)
        {
            UIAccessibility.PostNotification(UIAccessibilityPostNotification.Announcement, new NSString(s));
        }
    }
}
