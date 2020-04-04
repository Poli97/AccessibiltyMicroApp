using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinMelodies.iOS;
using XamarinMelodies.Model;

[assembly: Dependency(typeof(IOSAccessibleName))]
namespace XamarinMelodies.iOS
{
    public class IOSAccessibleName : IAccessibleName
    {

        public void setAccessName(View v, string s)
        {
            v.GetRenderer().NativeView.AccessibilityLabel = s;
        }

        public void setAccessHint(View v, string s)
        {
            v.GetRenderer().NativeView.AccessibilityHint = s;
        }
    }
}
