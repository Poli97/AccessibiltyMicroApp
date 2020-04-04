using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XAMARINAccessibilitySpeech.Droid;

[assembly: Dependency(typeof(AndroidAccessibilitySpeak))]
namespace XAMARINAccessibilitySpeech.Droid
{
    public class AndroidAccessibilitySpeak : IAccessibilitySpeakService
    {

        public void speakText(View v, string s)
        {
            v.GetRenderer().View.AnnounceForAccessibility(s);
        }
    }
}
