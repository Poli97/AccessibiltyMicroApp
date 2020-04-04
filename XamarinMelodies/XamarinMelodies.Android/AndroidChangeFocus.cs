using System;
using Android.Views;
using Android.Views.Accessibility;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinMelodies.Droid;
using XamarinMelodies.Model;

[assembly: Dependency(typeof(AndroidChangeFocus))]
namespace XamarinMelodies.Droid
{
    public class AndroidChangeFocus : IChangeAccessibilityFocus
    {
        public void ChangeFocus(Xamarin.Forms.View v)
        {
            (v.GetRenderer().View).SendAccessibilityEvent(EventTypes.ViewHoverEnter);
        }
    }
}
