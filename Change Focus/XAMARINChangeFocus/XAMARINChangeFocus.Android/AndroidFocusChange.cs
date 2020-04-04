using System;
using Android.Views.Accessibility;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XAMARINChangeFocus.Droid;

[assembly: Dependency(typeof(AndroidFocusChange))]
namespace XAMARINChangeFocus.Droid
{
    public class AndroidFocusChange : IChangeFocusService
    {

        public void ChangeFocus(View v)
        {
            v.GetRenderer().View.SendAccessibilityEvent(EventTypes.ViewHoverEnter);
        }
    }
}
