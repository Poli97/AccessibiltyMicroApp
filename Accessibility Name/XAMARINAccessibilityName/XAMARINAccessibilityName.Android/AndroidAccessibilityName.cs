using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XAMARINAccessibilityName.Droid;

[assembly: Dependency(typeof(AndroidAccessibilityName))]
namespace XAMARINAccessibilityName.Droid
{
    public class AndroidAccessibilityName : IAccessibilityName
    {

        public void setName(View v, string s)
        {
            v.GetRenderer().View.ContentDescription = s;
        }
    }
}
