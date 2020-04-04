using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XAMARINAccessibilityName.iOS;

[assembly: Dependency(typeof(IosAccessiiblityName))]
namespace XAMARINAccessibilityName.iOS
{
    public class IosAccessiiblityName : IAccessibilityName
    {
        public void setName(View v, string s)
        {
            v.GetRenderer().NativeView.SetAccessibilityLabel(v.GetRenderer().Element, s);
        }
    }
}
