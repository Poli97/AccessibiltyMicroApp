using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinMelodies.Droid;
using XamarinMelodies.Model;

[assembly: Dependency(typeof(AndroidAccessibilitySpeak))]
namespace XamarinMelodies.Droid
{
    public class AndroidAccessibilitySpeak : IAccessibilitySpeak
    {
        public void AccSpeak(View v, string s)
        {
            v.GetRenderer().View.AnnounceForAccessibility(s);
        }
    }
}
