using System;
using Foundation;
using UIKit;
using Xamarin.Forms;

namespace XAMARINAccessibilitySpeech.iOS
{
    public class IosAccessibilitySpeak : IAccessibilitySpeakService
    {
        public void speakText(View v, string s)
        {
            UIAccessibility.PostNotification(notification: UIAccessibilityPostNotification.ScreenChanged, argument: new NSString(s));
        }
    }
}
